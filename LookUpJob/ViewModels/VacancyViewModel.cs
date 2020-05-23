using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LookUpJob.ViewModels
{
    public class VacancyViewModel : INotifyPropertyChanged
    {
        private int _id;
        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                if(value !=_id)
                {
                    _id = value;
                    INotifyPropertyChanged("ID");
                }
            }
        }

        private string _shortDescription;
        public string ShortDescription
        {
            get
            {
                return _shortDescription;
            }

            set{
                if(value !=_shortDescription)
                {
                    _shortDescription = value;
                    INotifyPropertyChanged("ShortDescription");
                }
            }
        }

        private string _position;
        public string Position
        {
            get
            {
                return _position;
            }

            set
            {
                if(value !=_position)
                {
                    _position = value;
                    INotifyPropertyChanged("Position");
                }
            }
        }

        private int _yearsOfExperience;
        public int YearsOfExperience
        {
            get
            {
                return _yearsOfExperience;
            }

            set
            {
                if(value !=_yearsOfExperience)
                {
                    _yearsOfExperience = value;
                    INotifyPropertyChanged("YearOfExperience");
                }
            }
        }

        private string _highestLevelOfEducation;
        public string HighestLevelOfEducation
        {
            get
            {
                return _highestLevelOfEducation;
            }

            set
            {
                if(value !=_highestLevelOfEducation)
                {
                    _highestLevelOfEducation = value;
                    INotifyPropertyChanged("HighestLevelOfEducation");
                }
            }
        }

        private string _vacancyDeadline;
        public string VacancyDeadline
        {
            get 
            {
                return _vacancyDeadline;
            }

            set
            {
                if(value !=_vacancyDeadline)
                {
                    _vacancyDeadline = value;
                    INotifyPropertyChanged("VacancyDeadline");
                }
            }
        }

        private string _companyName;
        public string CompanyName
        {
            get 
            {
                return _companyName;
            }

            set
            {
                if(value !=_companyName)
                {
                    _companyName = value;
                    INotifyPropertyChanged("CompanyName");
                }
            }
        }

        private string _vacancyEditor;
        public string VacancyEditor
        {
            get
            {
                return _vacancyEditor;
            }

            set
            {
                if (value != _vacancyEditor)
                {
                    _vacancyEditor = value;
                    INotifyPropertyChanged("VacancyEditor");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void INotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(null !=handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
