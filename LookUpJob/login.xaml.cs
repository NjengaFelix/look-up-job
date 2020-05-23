using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using System.Text.RegularExpressions;

namespace LookUpJob
{
    public partial class login : PhoneApplicationPage
    {
        public login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username, password;
            bool user_type = false;
            int company_id = 0, user_id = 0;
            username = txtuser.Text;
            password = txtpassword.Password;

            //Validation
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Field username is empty!");
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Field password is empty!");
                return;
            }
            else
            {
                //Getting userName and password from DB
                User user = new User();
                IList<User_details> users = user.loadUserProfile(username);
                if (users.Count == 0)
                {
                    //No list was returned
                    MessageBox.Show("Username does not exist");
                    return;
                }
                else
                {

                    string userPassword = "";
                    foreach (User_details ud in users)
                    {
                        user_id = ud.user_id;
                        user_type = ud.is_Employee_or_JobSeeker;
                        username = ud.user_name;
                        userPassword = ud.password;
                        company_id = ud.company_id;
                        
                    }
                    //Check password
                    if (!Regex.IsMatch(password, userPassword))
                    {
                        MessageBox.Show("Incorrect password");
                        return;
                    }
                    else 
                    {
                        //Create an IsolatedStorageSettings storage to save username and usertype (Session)
                        IsolatedStorageSettings.ApplicationSettings["user_id"] = user_id;
                        IsolatedStorageSettings.ApplicationSettings["username"] = username;
                        IsolatedStorageSettings.ApplicationSettings["user_type"] = user_type;
                        IsolatedStorageSettings.ApplicationSettings["company_id"] = company_id;
                        IsolatedStorageSettings.ApplicationSettings.Save();

                        if (user_type)
                        {
                            if(company_id == 0)
                            {
                                NavigationService.Navigate(new Uri("/NewCompanyPage.xaml", UriKind.Relative));
                                return;
                            }
                            else
                            {
                                //Navigate to the Employer to EmployerPage
                                NavigationService.Navigate(new Uri("/CompanyPage.xaml", UriKind.Relative));
                                return;
                            }
                            
                        }
                        else
                        {
                            //Navigate to the MainPage
                            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                        }
                        
                        
                        
                       
                    }

                }
            }
        }
    }
}