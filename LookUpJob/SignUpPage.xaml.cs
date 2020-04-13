using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LookUpJob
{
    public partial class SignUpPage : PhoneApplicationPage
    {
        public SignUpPage()
        {
            InitializeComponent();
        }

        private void btnSignUpNow_Click(object sender, RoutedEventArgs e)
        {
            string firstName, lastName, email, username, password;
            bool is_employee_or_jobSeeker = false;

            firstName = txtFirstName.Text;
            lastName = txtLastName.Text;
            email = txtEmail.Text;
            username = txtUsername.Text;
            password = txtPassword.Password;

            if (string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Field first name is empty");
                return;
            }
            else if (string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Field last name is empty");
                return;
            }
            else if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Field email is empty");
                return;
            }
            else if (rdoEmployer.IsChecked == false && rdoJobSeeker.IsChecked == false)
            {
                MessageBox.Show("Chooser either employer or job seeker");
                return;
            }
            else if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Field username is empty");
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Field password is empty");
                return;
            }
            else
            {
                //In the database, employee is tinyInt 1 while jobseeker is tinyInt 0
                if (rdoEmployer.IsChecked == true)
                {
                    is_employee_or_jobSeeker = true;

                } else if(rdoJobSeeker.IsChecked == true) 
                {
                    is_employee_or_jobSeeker = false;
                }

                //Only one username should exist in the Database
                User user = new User();
                if (user.loadUserProfile(username).Count > 0)
                {
                    MessageBox.Show("Username exists");
                    return;
                }
                else
                {
                    //create the user profile
                    user.createNewUser(firstName, lastName, email, is_employee_or_jobSeeker, username, password);
                    //Navigate to login
                    NavigationService.Navigate(new Uri("/login.xaml", UriKind.Relative));
                }
            
               
                

            }

            

        }



    }
}