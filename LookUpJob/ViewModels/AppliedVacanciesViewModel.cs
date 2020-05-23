using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace LookUpJob.ViewModels
{
    public class AppliedVacanciesViewModel
    {
        public AppliedVacanciesViewModel()
        {
            this.ItemsAppliedVacancies = new ObservableCollection<VacancyApplicationModel>();
        }

        public ObservableCollection<VacancyApplicationModel> ItemsAppliedVacancies { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            //Loading data for a particular user
            int userID = User.loadUserId("user_id");

            using(UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                try
                {
                    //Inner join variable query to get the columns we desire to display
                    var query = from va in udt.VacancyApplications
                                join v in udt.Vacancies on va.vacancy_id equals v.vacancies_id
                                join c in udt.Company on va.company_id equals c.company_id
                                select new
                                {
                                    va.vacancy_application_id,
                                    va.user_id,
                                    c.name,
                                    v.position,
                                    v.vacancy_deadline_date
                                }
                                into x where x.user_id == userID select x;

                    foreach(var q in query)
                    {
                        this.ItemsAppliedVacancies.Add(new VacancyApplicationModel() {ID = q.vacancy_application_id, CompanyName = q.name, Position = q.position, VacancyDealine = q.vacancy_deadline_date });
                    }
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }

            this.IsDataLoaded = true;
        }
    }
}
