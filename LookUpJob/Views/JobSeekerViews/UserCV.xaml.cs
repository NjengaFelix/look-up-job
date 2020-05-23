using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LookUpJob.Views.JobSeekerViews
{
    public partial class UserCV : PhoneApplicationPage
    {
        int userID = User.loadUserId("user_id");
        public UserCV()
        {
            InitializeComponent();
            //Check if the user has a CV first
            DataContext = App.MCVViewModel;
            ApplicationBar = new ApplicationBar();
            ApplicationBarIconButton editCV = new ApplicationBarIconButton();
            editCV.Text = "Add CV";
            editCV.IconUri = new Uri("/resources/icons/edit.png", UriKind.Relative);
            editCV.Click += editCV_Click;
            ApplicationBar.Buttons.Add(editCV);

            ApplicationBarIconButton saveCV = new ApplicationBarIconButton();
            saveCV.Text = "Save CV";
            saveCV.IconUri = new Uri("/resources/icons/save.png", UriKind.Relative);
            saveCV.Click += saveCV_Click;
            ApplicationBar.Buttons.Add(saveCV);


        }

        private void editCV_Click(object sender, EventArgs e)
        {
            //Make the textBoxes editable (isReadOnly == false)
            if(txtShortDesc.IsReadOnly && txtOccupation.IsReadOnly && txtExperience.IsReadOnly && txtEduLevel.IsReadOnly)
            {
                txtShortDesc.IsReadOnly = false;
                txtOccupation.IsReadOnly = false;
                txtExperience.IsReadOnly = false;
                txtEduLevel.IsReadOnly = false;
            }
            else
            {
                //Make the editBarIconButton as  toggle
                txtShortDesc.IsReadOnly = true;
                txtOccupation.IsReadOnly = true;
                txtExperience.IsReadOnly = true;
                txtEduLevel.IsReadOnly = true;
            }
        }

        void saveCV_Click(object sender, EventArgs e)
        {
            //Save the changes or the CV
            //Check first if the user wants to add the vacancy
            MessageBoxButton mtns = new MessageBoxButton();
            mtns = MessageBoxButton.OKCancel;
            MessageBoxResult mbres = MessageBox.Show("Do you want to save the CV?", "Save CV", mtns);
            if(mbres == MessageBoxResult.Cancel)
            {
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(txtShortDesc.Text))
                {
                    MessageBox.Show("Field short description is empty!");
                    return;
                }
                else if(string.IsNullOrEmpty(txtOccupation.Text))
                {
                    MessageBox.Show("Field occupation is empty!");
                    return;
                }
                else if(string.IsNullOrEmpty(txtExperience.Text))
                {
                    MessageBox.Show("Field experience is empty!");
                    return;
                }
                else if (string.IsNullOrEmpty(txtEduLevel.Text))
                {
                    MessageBox.Show("Field education level is empty!");
                    return;
                }
                else
                {
                    using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
                    {
                        try
                        {
                            int query = (from mcv in udt.CV where mcv.user_id == userID select mcv).Count();
                            if(query == 0)
                            {
                                //Save to a particular person
                                CV cv = new CV();
                                cv.user_id = userID;
                                cv.short_description = txtShortDesc.Text;
                                cv.occupation = txtOccupation.Text;
                                cv.years_of_experience = int.Parse(txtExperience.Text);
                                cv.education_level = txtEduLevel.Text;

                                udt.CV.InsertOnSubmit(cv);

                                udt.SubmitChanges();

                                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                            }
                            else
                            {
                                var queryCV = from cv in udt.CV where cv.user_id == userID select cv;
                                foreach(CV cv in queryCV)
                                {
                                    cv.user_id = userID;
                                    cv.short_description = txtShortDesc.Text;
                                    cv.occupation = txtOccupation.Text;
                                    cv.years_of_experience = int.Parse(txtExperience.Text);
                                    cv.education_level = txtEduLevel.Text;                                   
                                }
                                udt.SubmitChanges();
                                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                            }
                            
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if(!App.MCVViewModel.isDataLoaded)
            {
                App.MCVViewModel.LoadCV();

                if(App.MCVViewModel.Items.Count == 0)
                {
                    MessageBox.Show("Enter a new CV");
                    //Make all textBoxes editable (isReadonly == false)
                    txtShortDesc.IsReadOnly = false;
                    txtOccupation.IsReadOnly = false;
                    txtExperience.IsReadOnly = false;
                    txtEduLevel.IsReadOnly = false;
                    return;
                }
                else
                {
                    
                    IQueryable<ViewModels.CVModel> cv = App.MCVViewModel.Items.AsQueryable();
                    foreach (ViewModels.CVModel c in cv)
                    {
                        txtShortDesc.Text = c.ShortDescription;
                        txtOccupation.Text = c.Occupation;
                        txtExperience.Text = c.YearsOfExperience.ToString();
                        txtEduLevel.Text = c.EducationLevel;

                    }
                }
                
            } 
            else
            {
                App.MCVViewModel.LoadCV();
                IQueryable<ViewModels.CVModel> cv = App.MCVViewModel.Items.AsQueryable();
                foreach (ViewModels.CVModel c in cv)
                {
                    txtShortDesc.Text = c.ShortDescription;
                    txtOccupation.Text = c.Occupation;
                    txtExperience.Text = c.YearsOfExperience.ToString();
                    txtEduLevel.Text = c.EducationLevel;

                }
            }
        }

    }
}