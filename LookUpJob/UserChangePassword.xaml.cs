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
    public partial class UserChangePassword : PhoneApplicationPage
    {
        int userID = User.loadUserId("user_id");

        public UserChangePassword()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Get the old password from the database
            string oldPassword;
            using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                try
                {
                    var user = udt.Users.Single(u => u.user_id == userID);
                    oldPassword = user.password;


                    if (string.IsNullOrEmpty(txtOldPassword.Password))
                    {
                        MessageBox.Show("Field old password is empty!");
                        txtOldPassword.Focus();
                        return;
                    }
                    else if (string.IsNullOrEmpty(txtNewPass.Password))
                    {
                        MessageBox.Show("Field new password is empty!");
                        txtNewPass.Focus();
                        return;
                    }
                    else if (string.IsNullOrEmpty(txtConfirmPass.Password))
                    {
                        MessageBox.Show("Field confirm password is empty!");
                        txtConfirmPass.Focus();
                        return;
                    }
                    //Check old password
                    else if (!Regex.IsMatch(txtOldPassword.Password, oldPassword))
                    {
                        MessageBox.Show("Field old password is invalid!");
                        txtOldPassword.Focus();
                        return;
                    }
                    else if (!isPasswordValid(txtNewPass.Password))
                    {
                        return;
                    }
                    else if (!Regex.IsMatch(txtConfirmPass.Password, txtNewPass.Password))
                    {
                        MessageBox.Show("Field confirm password doesnt match field new password!");
                        txtConfirmPass.Focus();
                        return;
                    }

                    else
                    {
                        user.password = txtNewPass.Password;
                        udt.SubmitChanges();
                        MessageBox.Show("User password has been changed");
                    }

                }
                catch (Exception)
                {

                    throw;
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
                txtNewPass.Focus();
                return isPasswordValid;
            }
            else if(!hasUpperChar.IsMatch(password))
            {
                MessageBox.Show("New password requires at least an uppercase!");
                txtNewPass.Focus();
                return isPasswordValid;
            }
            else if(!hasMinimum8Chars.IsMatch(password))
            {
                MessageBox.Show("New password show have a minimum of 8 characters!");
                txtNewPass.Focus();
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