using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.IO.IsolatedStorage;

namespace LookUpJob.ViewModels
{
    public class CompanyViewModel
    {
        public CompanyViewModel()
        {
            this.Items = new ObservableCollection<VacancyViewModel>();
        }

        public ObservableCollection<VacancyViewModel> Items { get; private set; }


        public bool IsDataLoaded
        {
            get;
            set;
        }
            
        public void LoadData()
        {
            //Get data of db.Vacancy
            using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                try
                {
                    //get the companyID of the user
                    int companyID = User.loadCompanyId("company_id"); 
                    //Inner join variable query to get the columns we desire to display
                    var query = from v in udt.Vacancies
                                join c in udt.Company on v.company_id equals c.company_id
                                join u in udt.Users on v.user_id equals u.user_id
                                select new
                                //Getting the columns we require to display
                                {
                                    v.vacancies_id,
                                    v.short_description,
                                    v.position,
                                    v.years_of_experience,
                                    v.highest_level_of_education,
                                    v.vacancy_deadline_date,
                                    c.company_id,
                                    c.name,
                                    u.user_name
                                }
                                //Order the vacancies starting with the most recent
                               into x where x.company_id == companyID select x;

                        foreach (var q in query)
                        {
                            this.Items.Add(new VacancyViewModel() { ID = q.vacancies_id, ShortDescription = q.short_description,
                                Position = q.position, YearsOfExperience = q.years_of_experience,
                                HighestLevelOfEducation = q.highest_level_of_education, VacancyDeadline = q.vacancy_deadline_date,
                                CompanyName = q.name, VacancyEditor =  q.user_name });
                        }
                }
                catch (Exception ex)
                {
                    //No Data to be displayed
                    Console.WriteLine(ex);
                }
            }

            this.IsDataLoaded = true;
        }
        
    }
}
