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

        public void LoadData()
        {
            //Get data of db.Vacancy
            using (UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                try
                {
                    IQueryable<Vacancy> query = from v in udt.Vacancies select v;
                    IList<Vacancy> vacancies = query.ToList();

                        foreach (Vacancy v in vacancies)
                        {
                            this.Items.Add(new VacancyViewModel() { ID = v.vacancies_id, ShortDescription = v.short_description, Position = v.position, HighestLevelOfEducation = v.highest_level_of_education });
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
