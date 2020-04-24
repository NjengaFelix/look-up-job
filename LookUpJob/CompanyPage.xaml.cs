using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;

namespace LookUpJob
{
    public partial class CompanyPage : PhoneApplicationPage
    {
        public CompanyPage()
        {
            InitializeComponent();   
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/VacanciesPage.xaml",UriKind.Relative));
        }

        

    }
}