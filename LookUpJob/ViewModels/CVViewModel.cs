using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace LookUpJob.ViewModels
{
    public class CVViewModel
    {
       
        public CVViewModel()
        {
            this.Items = new ObservableCollection<CVModel>();
        }

        public ObservableCollection<CVModel> Items { get; private set; }

        public bool isDataLoaded
        {
            get;
            private set;
        }

        public void LoadCV()
        {
            int UserID = User.loadUserId("user_id");
            //Get the cv of the user
            using(UserDataContext udt = new UserDataContext(UserDataContext.DBConnectionString))
            {
                try
                {
                    IQueryable<CV> query = from c in udt.CV where c.user_id == UserID select c;
                    foreach(var q in query)
                    {
                        this.Items.Add(new CVModel() 
                        { 
                            UserID = q.user_id,
                            ShortDescription = q.short_description, 
                            Occupation = q.occupation,
                            YearsOfExperience = q.years_of_experience,
                            EducationLevel = q.education_level
               
                        });
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                
            }

            this.isDataLoaded = true;
        }

    }
}
