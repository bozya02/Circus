using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Circus.Pages;

namespace Circus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CircusWindow : Window
    {
        public CircusWindow()
        {
            InitializeComponent();

            frame.NavigationService.Navigate(new AuthorizationPage());

            frame.Navigated += Frame_Navigated;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            var content = frame.Content as Page;

            if (content is AuthorizationPage)
            {
                btnBack.Visibility = Visibility.Hidden;
                btnForward.Visibility = Visibility.Collapsed;
                btnExit.Visibility = Visibility.Collapsed;
                spNavigation.Visibility = Visibility.Hidden;
            }
            else if (content is RegistartionPage)
            {
                btnBack.Visibility = Visibility.Visible;
                btnForward.Visibility = Visibility.Collapsed;
                spNavigation.Visibility = Visibility.Hidden;
            }
            else
            {
                btnBack.Visibility = Visibility.Visible;
                btnForward.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Visible;
                spNavigation.Visibility = Visibility.Visible;
            }

            tbTitle.Text = content.Title;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoBack)
                frame.GoBack();
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (frame.CanGoForward)
                frame.GoForward();
        }

        private void btnPerfomances_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new PerfomancesListPage());
        }

        private void btnAnimals_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new AnimalsListPage());
        }

        private void btnArtists_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new ArtistsListPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(new AuthorizationPage());
        }

        private void btnTickets_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
