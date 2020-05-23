using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LookUpJob.ViewModels
{
    public class CVModel : INotifyPropertyChanged
    {
        private int _userID;
        public int UserID
        {
            get
            {
                return _userID;
            }

            set
            {
                if (value != _userID)
                {
                    _userID = value;
                    INotifyPropertyChanged("UserID");
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

            set
            {
                if (value != _shortDescription)
                {
                    _shortDescription = value;
                    INotifyPropertyChanged("ShortDescription");
                }
            }
        }

        private string _occupation;
        public string Occupation
        {
            get
            {
                return _occupation;
            }

            set
            {
                if (value != _occupation)
                {
                    _occupation = value;
                    INotifyPropertyChanged("Occupation");
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
                if (value != _yearsOfExperience)
                {
                    _yearsOfExperience = value;
                    INotifyPropertyChanged("YearsOfExperience");
                }
            }
        }

        private string _educationLevel;
        public string EducationLevel
        {
            get
            {
                return _educationLevel;
            }

            set
            {
                if (value != _educationLevel)
                {
                    _educationLevel = value;
                    INotifyPropertyChanged("EducationLevel");
                }
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void INotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
