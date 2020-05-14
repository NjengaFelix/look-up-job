using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LookUpJob.Resources;
using LookUpJob.ViewModels;

namespace LookUpJob
{
    public partial class CompanyPage : PhoneApplicationPage
    {
        public CompanyPage()
        {
            // Set the data context of the LongListSelector control to the Vacancy data
            InitializeComponent();
            DataContext = App.MCompanyViewModel;
        }

        //On navigation to the page, load the data on to the DataContext
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if(!App.MCompanyViewModel.IsDataLoaded)
            {
                App.MCompanyViewModel.LoadData();
            }
        }

        //Action when you click on a particular vacancy
        private void VacanciesLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Check if there is a selection made
            if (VacanciesLongListSelector.SelectedItem == null)
                return;

            //Passed variable selectedItem to the CompanyVacancyDetailPage
            NavigationService.Navigate(new Uri("/Views/CompanyViews/CompanyVacancyDetailPage.xaml?selectedItem="+ (VacanciesLongListSelector.SelectedItem as VacancyViewModel).ID, UriKind.Relative));

        }

        


        
    }
}