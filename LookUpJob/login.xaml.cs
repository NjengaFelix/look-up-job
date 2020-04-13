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
                        username = ud.user_name;
                        userPassword = ud.password;
                        user_type = ud.is_Employee_or_JobSeeker;
                    }
                    //Check password
                    if (userPassword.Contains(password) == false)
                    {
                        MessageBox.Show("Incorrect password");
                        return;
                    }
                    else if (userPassword.Contains(password))
                    {
                        //Create an IsolatedStorageSettings storage to save username and usertype (Session)
                        IsolatedStorageSettings.ApplicationSettings["username"] = username;
                        IsolatedStorageSettings.ApplicationSettings["user_type"] = user_type;
                        IsolatedStorageSettings.ApplicationSettings.Save();
                        //Navigate to the MainPage
                        NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                       
                    }

                }
            }
        }
    }
}