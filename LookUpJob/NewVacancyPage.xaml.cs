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
    public partial class VacanciesPage : PhoneApplicationPage
    {
        public VacanciesPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string ShortDesc, position, vacancyDeadline, highestEduLevel;
            int experience = 0;
            ShortDesc = txtShortDesc.Text;
            position = txtPosition.Text;
            try
            {
                experience = Int32.Parse(txtExperience.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("Input the number of years of experience(eg 10)");
                return;
            }
            
            //Date should not be greater than the publish date
            vacancyDeadline = txtVacancyDeadline.Text;
            highestEduLevel = txtHLOEdu.Text;

            //Validation
            if(string.IsNullOrEmpty(ShortDesc))
            {
                MessageBox.Show("Input short description field");
                return;
            }
            else if(string.IsNullOrEmpty(position))
            {
                MessageBox.Show("Input position field");
                return;
            }
            else if(string.IsNullOrEmpty(vacancyDeadline))
            {
                MessageBox.Show("Input vacancy deadline field");
                return;
            }
            else if(string.IsNullOrEmpty(highestEduLevel))
            {
                MessageBox.Show("Input the highest level of education");
                return;
            }
            else
            {
                //Get the userID
                int userID = User.loadUserId("user_id");
                int companyID = 0;
                //Insert into the db.Vacancy
                    using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
                    {
                       Vacancy vacancy = new Vacancy();
                        vacancy.short_description = ShortDesc;
                        vacancy.position = position;
                        vacancy.years_of_experience = experience;
                        vacancy.vacancy_deadline_date = vacancyDeadline;
                        vacancy.highest_level_of_education = highestEduLevel;
                        vacancy.user_id = userID;
                        vacancy.publish_date = DateTime.Now.ToShortDateString();
                        //Get companyID
                        var company = udt.Users.Single(u => u.user_id == userID);
                        companyID = company.company_id;
                        vacancy.company_id = companyID;

                        udt.Vacancies.InsertOnSubmit(vacancy);

                    try
                    {
                        udt.SubmitChanges();
                        App.MCompanyViewModel.LoadData();
                        NavigationService.Navigate(new Uri("/CompanyPage.xaml", UriKind.Relative));
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        MessageBox.Show("Can't update vacancy at the moment, Try later!");
                    }

                    }
            
            }

        }
    }
}