using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Text.RegularExpressions;

namespace LookUpJob.Views.CompanyViews
{
    public partial class CompanyVacancyDetailPage : PhoneApplicationPage
    {
        int index = 0;
        
        public CompanyVacancyDetailPage()
        {
            InitializeComponent();
            ApplicationBar = new ApplicationBar();
            ApplicationBarIconButton editVacancy = new ApplicationBarIconButton();
            editVacancy.Text = "Edit";
            editVacancy.IconUri = new Uri("/resources/icons/edit.png", UriKind.Relative);
            editVacancy.Click += editVacancies_Click;
            ApplicationBar.Buttons.Add(editVacancy);

            ApplicationBarIconButton saveVacancy = new ApplicationBarIconButton();
            saveVacancy.Text = "Save";
            saveVacancy.IconUri = new Uri("/resources/icons/save.png",UriKind.Relative);
            saveVacancy.Click += saveVacancy_Click;
            ApplicationBar.Buttons.Add(saveVacancy);

            ApplicationBarIconButton deleteVacancy = new ApplicationBarIconButton();
            deleteVacancy.Text = "Delete";
            deleteVacancy.IconUri = new Uri("/resources/icons/delete.png", UriKind.Relative);
            deleteVacancy.Click += deleteVacancy_Click;
            ApplicationBar.Buttons.Add(deleteVacancy);
            
        }

        void deleteVacancy_Click(object sender, EventArgs e)
        {
            //Confirm first if the user wants to change the vacancy details
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the vacancy?", "Delete vacancy", buttons);
            if (result == MessageBoxResult.Cancel)
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
                        var query = (from v in udt.Vacancies where v.vacancies_id == index select v).First();
                        udt.Vacancies.DeleteOnSubmit(query);
                        udt.SubmitChanges();
                        App.MCompanyViewModel.Items.RemoveAt(index - 1);
                        NavigationService.Navigate(new Uri("/CompanyPage.xaml", UriKind.Relative));
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to save changes! Try again later");
                    }
                    
                }
            }
        }

        void saveVacancy_Click(object sender, EventArgs e)
        {
            //Confirm first if the user wants to change the vacancy details
            MessageBoxButton buttons = MessageBoxButton.OKCancel;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to make the changes?", "Save changes", buttons);
            if(result == MessageBoxResult.Cancel)
            {
                //No changes will be made
                return;
            }
            else
            {
                //Check if any textbox is empty
                if(string.IsNullOrEmpty(txtShortDesc.Text))
                {
                    MessageBox.Show("Field short description is empty!");
                    return;
                }
                else if (!Regex.IsMatch(txtShortDesc.Text, @".{25,}"))
                {
                    MessageBox.Show("Short description requires a minimum of 25 characters!");
                    return;
                }
                else if(string.IsNullOrEmpty(txtVacancyPosition.Text))
                {
                    MessageBox.Show("Field position is empty!");
                    return;
                }
                else if (!Regex.IsMatch(txtVacancyPosition.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("Input position field requires letters only!");
                    return;
                }
                else if(string.IsNullOrEmpty(txtYearsofExperience.Text))
                {
                    MessageBox.Show("Field years of experience is empty!");
                    return;
                }
                else if (!Regex.IsMatch(txtYearsofExperience.Text, @"^[0-9]+$"))
                {
                    MessageBox.Show("Input experience is requires numbers only!");
                    return;
                }
                else if(string.IsNullOrEmpty(txtHLOEdu.Text))
                {
                    MessageBox.Show("Field years of highest level of education is empty!");
                    return;
                }
                else if (!Regex.IsMatch(txtHLOEdu.Text, @"^[a-zA-Z]+$"))
                {
                    MessageBox.Show("Input the highest level of education requires letters only");
                    return;
                }
                else if (string.IsNullOrEmpty(txtVacancyDeadline.Text))
                {
                    MessageBox.Show("Field years of vacancy deadline is empty!");
                    return;
                }
                
                else
                {
                    //Save changes to the Database
                    using(UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
                    {
                        try
                        {
                            //Selected VacancyID = index
                            //get the editor
                            int userID = User.loadUserId("user_id");
                            var query = from v in udt.Vacancies where v.vacancies_id == index select v;
                            foreach(Vacancy va in query)
                            {
                                va.short_description = txtShortDesc.Text;
                                va.position = txtVacancyPosition.Text;
                                va.years_of_experience = int.Parse(txtYearsofExperience.Text);
                                va.highest_level_of_education = txtHLOEdu.Text;
                                va.vacancy_deadline_date = txtVacancyDeadline.Text;
                                va.user_id = userID;
                               // v.publish_date = DateTime.Now;
                            }

                            udt.SubmitChanges();
                            txtShortDesc.IsReadOnly = true;
                            txtVacancyPosition.IsReadOnly = true;
                            txtYearsofExperience.IsReadOnly = true;
                            txtHLOEdu.IsReadOnly = true;
                            txtVacancyDeadline.IsReadOnly = true;

                            //To reload the data in the Observable list clear all data
                            App.MCompanyViewModel.Items.Clear();
                            App.MCompanyViewModel.LoadData();
                            NavigationService.Navigate(new Uri("/CompanyPage.xaml",UriKind.Relative));
                            
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Unable to save changes! Try again later");
                        }
                    }
                }
            }
            
            
            
        }

        void editVacancies_Click(object sender, EventArgs e)
        {
           //Toggle between IsReadOnly = false and IsReadonly = true for all textboxes
            if(txtShortDesc.IsReadOnly == true && txtVacancyPosition.IsReadOnly == true && txtYearsofExperience.IsReadOnly == true
                && txtHLOEdu.IsReadOnly == true && txtVacancyDeadline.IsReadOnly == true)
            {
                txtShortDesc.IsReadOnly = false;
                txtVacancyPosition.IsReadOnly = false;
                txtYearsofExperience.IsReadOnly = false;
                txtHLOEdu.IsReadOnly = false;
                txtVacancyDeadline.IsReadOnly = false;

            }
            else
            {
                txtShortDesc.IsReadOnly = true;
                txtVacancyPosition.IsReadOnly = true;
                txtYearsofExperience.IsReadOnly = true;
                txtHLOEdu.IsReadOnly = true;
                txtVacancyDeadline.IsReadOnly = true;
            }

        }

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
                    DataContext = App.MCompanyViewModel.Items[index - 1];
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}