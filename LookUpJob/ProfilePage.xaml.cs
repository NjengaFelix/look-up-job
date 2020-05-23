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
    public partial class ProfilePage : PhoneApplicationPage
    {
        string firstName, lastName, email, username;
        public ProfilePage()
        {
            InitializeComponent();
            //Create application bar for editing and deleting profile
            ApplicationBar = new ApplicationBar();
            //Adding icons
            ApplicationBarIconButton editProfile = new ApplicationBarIconButton();
            editProfile.Text = "Edit";
            editProfile.IconUri = new Uri("/resources/icons/edit.png", UriKind.Relative);
            editProfile.Click += editProfile_Click;
            ApplicationBar.Buttons.Add(editProfile);
                
        }
            

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Fetch the username from the IsolateSettingsStorage
            if (string.IsNullOrEmpty(User.loadUsername("username")))
            {
                MessageBox.Show("Login in to check user details");
                NavigationService.Navigate(new Uri("/login.xaml", UriKind.Relative));
                return;
            }
            else
            {
                username = User.loadUsername("username");
                //Gather information where the username is equal to the isolatedstoragesetting
                //It should return one profile only
                User user = new User();
                IList<User_details> users = user.loadUserProfile(username);

                if (users.Count == 0)
                {
                    //Login once more
                    MessageBox.Show("Login in to check user details");
                }
                else
                {
                    foreach (User_details ud in users)
                    {
                        firstName = ud.first_name;
                        lastName = ud.last_name;
                        email = ud.email;
                        username = ud.user_name;
                    }

                    txtFirstName.Text = firstName;
                    txtLastName.Text = lastName;
                    txtEmail.Text = email;
                    txtUsername.Text = username;
                }
            }
        }

        //}

        void editProfile_Click(object sender, EventArgs e)
        {
            if (txtFirstName.IsReadOnly && txtLastName.IsReadOnly && txtEmail.IsReadOnly && txtUsername.IsReadOnly)
            {

                txtFirstName.IsReadOnly = false;
                txtLastName.IsReadOnly = false;
                txtEmail.IsReadOnly = false;
                txtUsername.IsReadOnly = true;

            }
            else
            {
                txtFirstName.IsReadOnly = true;
                txtLastName.IsReadOnly = true;
                txtEmail.IsReadOnly = true;
                txtUsername.IsReadOnly = true;
            }

        }

        private void btnSaveProfile_Click(object sender, RoutedEventArgs e)
        {
            //Validation
            string firstName, lastName, email, username;
            firstName = txtFirstName.Text;
            lastName = txtLastName.Text;
            email = txtEmail.Text;
            username = txtUsername.Text;

            if(string.IsNullOrEmpty(firstName))
            {
                MessageBox.Show("Field first name is empty");
                return;
            }
            else if (!Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
            {
                MessageBox.Show("Field first name requires letters only");
                return;
            }
            else if(string.IsNullOrEmpty(lastName))
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
            else if (!Regex.IsMatch(email, @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$"))
            {
                MessageBox.Show("Field email is invalid");
                return;
            }
            else if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Field username is empty");
                return;
            }
            else
            {
                using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
                {
                    try
                    {
                        IQueryable<User_details> query = from u in udt.Users where u.user_name == txtUsername.Text select u;

                        foreach(User_details ud in query)
                        {
                        ud.first_name = txtFirstName.Text;
                        ud.last_name = txtLastName.Text;
                        ud.email = txtEmail.Text;
                        }

                        udt.SubmitChanges();
                        MessageBox.Show("User profile changes have been applied");
                        
                    }
                    catch (Exception)
                    {
                        
                        throw;
                    }
                }
                
            }

            
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/UserChangePassword.xaml",UriKind.Relative));
        }



    }
}