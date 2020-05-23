using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LookUpJob.ViewModels;


namespace LookUpJob
{
    public partial class VacancyDetailsPage : PhoneApplicationPage
    {
        int index = 0, UserID = User.loadUserId("user_id"), cvID, companyID;
        public VacancyDetailsPage()
        {
            InitializeComponent();
            
            ApplicationBar = new ApplicationBar();
            ApplicationBarIconButton addVacancy = new ApplicationBarIconButton();
            addVacancy.Text = "Apply vacancy";
            addVacancy.IconUri = new Uri("/resources/icons/add.png", UriKind.Relative);
            addVacancy.Click += addVacancy_Click;
            ApplicationBar.Buttons.Add(addVacancy);
        }

        void addVacancy_Click(object sender, EventArgs e)
        {
            //Check first if the user wants to add the vacancy
            MessageBoxButton mtns = new MessageBoxButton();
            mtns = MessageBoxButton.OKCancel;
            MessageBoxResult mbres = MessageBox.Show("Do you want to apply the vacancy?", "Apply vacancy", mtns);
            if(mbres == MessageBoxResult.Cancel)
            {
                //No changes will be made
                return;
            }
            else
            {
                using(UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
                {
                    try
                    {
                        //First check if the user had applied already
                        int query = (from v in udt.VacancyApplications where v.vacancy_id == index select v).Count();
                        if (query > 0)
                        {
                            MessageBox.Show("Cannot apply the vacancy twice!");
                            return;
                        }
                        else
                        {
                            //Check if the user has a CV
                            int queryCV = (from cv in udt.CV where cv.user_id == UserID select cv).Count();
                            if (queryCV == 0)
                            {
                                MessageBoxResult mbresults = MessageBox.Show("Do you want to add a CV?", "Add CV", mtns);
                                if (mbresults == MessageBoxResult.Cancel)
                                {
                                    return;
                                }
                                else
                                {
                                    //Navigate to the CV page
                                    NavigationService.Navigate(new Uri("/Views/JobSeekerViews/UserCV.xaml", UriKind.Relative));
                                    return;
                                }

                            }
                            else
                            {
                                VacancyApplication va = new VacancyApplication();
                                va.user_id = UserID;
                                var qcv = udt.CV.Single(cva => cva.user_id == UserID);
                                cvID = qcv.cv_id;
                                va.cv_id = cvID;
                                va.vacancy_id = index;
                                //CompanyID
                                string companyName = App.MyMainViewModel.Items[index - 1].CompanyName;
                                var company = udt.Company.Single(c => c.name == companyName);
                                companyID = company.company_id;
                                va.company_id = companyID;
                                udt.VacancyApplications.InsertOnSubmit(va);
                                udt.SubmitChanges();

                                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));

                            }
                        }
                    }

                    catch (Exception)
                    {

                        throw;
                    }
                }
            }

        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    index = int.Parse(selectedIndex);
                    //The db.Vacancy.ID is +1 greater than the Items[keyIndex]
                    //Therefore to match Vacancy.ID index with Items[keyIndex] less one from the Vacancy.ID
                    DataContext = App.MyMainViewModel.Items[index - 1];
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Get the current user
            int UserID = User.loadUserId("user_id");
            int companyID = 0;
           // int vacancyID = 0;
            //Insert details into db.VacancyApplication
            using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                //user can only apply for a vacancy once
                var vacancyApplied = udt.VacancyApplications.Single(v => v.vacancy_id == index);
                if (vacancyApplied.vacancy_id == 0)
                {
                    VacancyApplication va = new VacancyApplication();
                    va.vacancy_id = index;
                    //CV ID
                    va.user_id = UserID;
                    //CompanyID
                    string companyName = App.MyMainViewModel.Items[index - 1].CompanyName;
                    var company = udt.Company.Single(c => c.name == companyName);
                    companyID = company.company_id;
                    va.company_id = companyID;
                    udt.VacancyApplications.InsertOnSubmit(va);

                    try
                    {
                        udt.SubmitChanges();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Try again later!");
                    }

                    MessageBox.Show("The vacancy has been applied");
                    //Navigate to the applied vacancies page
                    //Send the Items[index] with the URL
                    NavigationService.Navigate(new Uri("/AppliedVacanciesPage.xaml", UriKind.Relative));

                }
                else
                {
                    MessageBox.Show("Vacancy has already been applied");
                    return;
                }
            }
            
                
        }
    }
}