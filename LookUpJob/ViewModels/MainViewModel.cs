using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace LookUpJob.ViewModels
{
    public class MainViewModel 
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<VacancyViewModel>();
        }

        public ObservableCollection<VacancyViewModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        //LoadData should throw exception to check if there's existing data in the DB or table Vacancy exists
        public void LoadData()
        {
            //Get data of db.Vacancy
            using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                try
                {
                    //Inner join variable query to get the columns we desire to display
                    var query = from v in udt.Vacancies
                                join c in udt.Company on v.company_id equals c.company_id
                                select new
                                //Getting the columns we require to display
                                {
                                    v.vacancies_id,
                                    v.short_description,
                                    v.position,
                                    v.years_of_experience,
                                    v.highest_level_of_education,
                                    v.vacancy_deadline_date,
                                    c.name
                                };
                                //Order the vacancies starting with the most recent
                           //    into x orderby x.vacancies_id descending select x;

                        foreach (var q in query)
                        {
                            this.Items.Add(new VacancyViewModel() { ID = q.vacancies_id, ShortDescription = q.short_description, Position = q.position, YearsOfExperience = q.years_of_experience, HighestLevelOfEducation = q.highest_level_of_education, VacancyDeadline = q.vacancy_deadline_date, CompanyName = q.name });
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
