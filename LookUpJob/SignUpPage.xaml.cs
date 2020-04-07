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

            using (UserDataContext context = new UserDataContext(UserDataContext.DBConnectionString))
            {
                if (!context.DatabaseExists())
                    context.CreateDatabase();
            }
        }

        private void btnSignUpNow_Click(object sender, RoutedEventArgs e)
        {
            string firstName, lastName, email, userName, password;
            bool is_employee_or_jobSeeker = true;

            firstName = txtFirstName.Text;
            lastName = txtLastName.Text;
            email = txtEmail.Text;
            userName = txtUsername.Text;
            password = txtPassword.Password;

            addUser(firstName, lastName, email, is_employee_or_jobSeeker, userName, password);

        }

        private void addUser(string firstName, string lastName, string email, bool is_employee_or_jobSeeker, string userName, string password)
        {
            using (UserDataContext context = new UserDataContext(UserDataContext.DBConnectionString))
            {
                User_details ud = new User_details();
                ud.first_name = firstName;
                ud.last_name = lastName;
                ud.email = email;
                ud.is_Employee_or_JobSeeker = is_employee_or_jobSeeker;
                ud.user_name = userName;
                ud.password = password;

                context.Users.InsertOnSubmit(ud);
                context.SubmitChanges();
            }
        }



    }
}