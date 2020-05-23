using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace LookUpJob
{
    public partial class AppliedVacanciesPage : PhoneApplicationPage
    {
        int index;
        public AppliedVacanciesPage()
        {
            InitializeComponent();

            DataContext = App.AppliedVacanciesViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(!App.AppliedVacanciesViewModel.IsDataLoaded)
            {
                App.AppliedVacanciesViewModel.LoadData();
            }
        }

        private void AppliedVacanciesLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}