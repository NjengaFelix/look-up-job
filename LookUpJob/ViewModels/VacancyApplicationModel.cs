using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace LookUpJob.ViewModels
{
    public class VacancyApplicationModel : INotifyPropertyChanged
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

        private string _companyName;
        public string CompanyName
        {
            get
            {
                return _companyName;
            }

            set
            {
                if (value != _companyName)
                {
                    _companyName = value;
                    INotifyPropertyChanged("CompanyName");
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
                if (value != _position)
                {
                    _position = value;
                    INotifyPropertyChanged("Position");
                }
            }
        }

        private string _vacancyDeadline;
        public string VacancyDealine
        {
            get
            {
                return _vacancyDeadline;
            }

            set
            {
                if (value != _vacancyDeadline)
                {
                    _vacancyDeadline = value;
                    INotifyPropertyChanged("VacancyDeadline");
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
