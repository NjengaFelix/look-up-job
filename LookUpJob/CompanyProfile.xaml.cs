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
    public partial class CompanyProfile : PhoneApplicationPage
    {
        int companyID = User.loadCompanyId("company_id");
        
        public CompanyProfile()
        {
            InitializeComponent();
            ApplicationBar = new ApplicationBar();
            ApplicationBarIconButton edit = new ApplicationBarIconButton();
            edit.Text = "Edit";
            edit.IconUri = new Uri("/resources/icons/edit.png", UriKind.Relative);
            edit.Click += edit_Click;
            ApplicationBar.Buttons.Add(edit);

            ApplicationBarIconButton add = new ApplicationBarIconButton();
            add.Text = "Add";
            add.IconUri = new Uri("/resources/icons/add.png", UriKind.Relative);
            add.Click += add_Click;
            ApplicationBar.Buttons.Add(add);
        }

        void add_Click(object sender, EventArgs e)
        {
            //Using the database get the company profile
            using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                try
                {
                    var company = udt.Company.Single(c => c.company_id == companyID);
                    if(string.IsNullOrEmpty(txtCompanyName.Text))
                    {
                        MessageBox.Show("Field company name is empty!");
                        txtCompanyName.Focus();
                        return;
                    }
                    else if (!Regex.IsMatch(txtCompanyName.Text, @"^[a-zA-Z]+$"))
                    {
                        MessageBox.Show("Input letters in company name!");
                        return;
                    }
                    else if(string.IsNullOrEmpty(txtCompanyType.Text))
                    {
                        MessageBox.Show("Field company type is empty!");
                        txtCompanyType.Focus();
                        return;
                    }
                    else if (!Regex.IsMatch(txtCompanyType.Text, @"^[a-zA-Z]+$"))
                    {
                        MessageBox.Show("Input letters in company type!");
                        return;
                    }
                    else if(string.IsNullOrEmpty(txtLocation.Text))
                    {
                        MessageBox.Show("Field company location is empty!");
                        txtLocation.Focus();
                        return;
                    }
                    else if (!Regex.IsMatch(txtLocation.Text, @"^[a-zA-Z]+$"))
                    {
                        MessageBox.Show("Input letters in company location!");
                        return;
                    }
                    else if(string.IsNullOrEmpty(txtEmail.Text))
                    {
                        MessageBox.Show("Field company email is empty!");
                        txtEmail.Focus();
                        return;
                    }
                    else if (!Regex.IsMatch(txtEmail.Text, @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$"))
                    {
                        MessageBox.Show("Input a valid email");
                        return;
                    }
                    else
                    {
                        company.name = txtCompanyName.Text;
                        company.company_type = txtCompanyType.Text;
                        company.location = txtLocation.Text;
                        company.email = txtEmail.Text;

                        udt.SubmitChanges();

                        MessageBox.Show("Company profile changes have been applied");
                    }

                }
                catch (Exception)
                {
                    
                    throw;
                }

                

            }
        }

        void edit_Click(object sender, EventArgs e)
        {
            if(txtCompanyName.IsReadOnly && txtCompanyType.IsReadOnly && txtLocation.IsReadOnly && txtEmail.IsReadOnly)
            {
                txtCompanyName.IsReadOnly = false;
                txtCompanyType.IsReadOnly = false;
                txtLocation.IsReadOnly = false;
                txtEmail.IsReadOnly = false;
            }
            else
            {
                txtCompanyName.IsReadOnly = true;
                txtCompanyType.IsReadOnly = true;
                txtLocation.IsReadOnly = true;
                txtEmail.IsReadOnly = true;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //Using the database get the company profile
            using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                try
                {

                    var company = udt.Company.Single(c => c.company_id == companyID);
                    txtCompanyName.Text = company.name;
                    txtCompanyType.Text = company.company_type;
                    txtLocation.Text = company.location;
                    txtEmail.Text = company.email;
                }
                catch (Exception)
                {

                    throw;
                }



            }
        }
    }
}