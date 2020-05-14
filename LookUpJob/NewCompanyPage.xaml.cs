﻿using System;
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
    public partial class NewCompany : PhoneApplicationPage
    {
        public NewCompany()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string company_type = txtType.Text;
            string location = txtLocation.Text;
            string email = txtEmail.Text;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Input a company name");
                return;
            }
            else if(string.IsNullOrEmpty(company_type))
            {
                MessageBox.Show("Input a company type");
                return;
            }
            else if(string.IsNullOrEmpty(location))
            {
                MessageBox.Show("Input a company location");
                return;
            }
            else if(string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Input an email");
                return;
            }
            else
            {
                //Insert inputs to the db.company
                using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
                {
                    Company company = new Company();
                    company.name = name;
                    company.company_type = company_type;
                    company.location = location;
                    company.email = email;

                    udt.Company.InsertOnSubmit(company);

                    try
                    {
                        udt.SubmitChanges();
                    //    IsolatedStorageSettings.ApplicationSettings["company_id"] = company_id;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    //Update company for the user(super_user) and issuper_user=true
                    //Get current user id
                    int userId = User.loadUserId("user_id");
                    var query = from c in udt.Users where c.user_id == userId select c;

                    foreach(User_details u in query)
                    {
                        u.is_super_user = true;
                        u.company_id = company.company_id;
                    }

                    try
                    {
                        udt.SubmitChanges();
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex);
                    }
                }

                //Move to the company page
                
                NavigationService.Navigate(new Uri("/CompanyPage.xaml", UriKind.Relative));
            }
            
        }

        
    }
}