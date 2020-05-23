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
            else if (!Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Field first name requires letters only");
                return;
            }
            else if (string.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Field last name is empty");
                return;
            }
            else if (!Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Field last name requires letters only");
                return;
            }
            else if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Field email is empty");
                return;
            }
            else if(!Regex.IsMatch(email, @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" ))
            {
                MessageBox.Show("Field email is invalid");
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
            else if (!Regex.IsMatch(username, @".{4,}"))
            {
                MessageBox.Show("Username requires a minimum characters of 4");
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Field password is empty");
                return;
            }
            else if(!isPasswordValid(password))
            {
                return;
            }
            else if (string.IsNullOrEmpty(txtConfirmPass.Password))
            {
                MessageBox.Show("Field confirm password is empty");
                return;
            }
            else if (!Regex.IsMatch(txtConfirmPass.Password, password))
            {
                MessageBox.Show("Field confirm password doesn't match field password!");
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


        private bool isPasswordValid(string password)
        {
            bool isPasswordValid = false;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            if (!hasNumber.IsMatch(password))
            {
                MessageBox.Show("New password requires at least a number!");
                txtPassword.Focus();
                return isPasswordValid;
            }
            else if (!hasUpperChar.IsMatch(password))
            {
                MessageBox.Show("New password requires at least an uppercase!");
                txtPassword.Focus();
                return isPasswordValid;
            }
            else if (!hasMinimum8Chars.IsMatch(password))
            {
                MessageBox.Show("New password show have a minimum of 8 characters!");
                txtPassword.Focus();
                return isPasswordValid;
            }
            else
            {
                isPasswordValid = true;
            }

            return isPasswordValid;
        }



    }
}