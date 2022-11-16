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

            frameNav.NavigationService.Navigate(new AuthorizationPage());

            frameNav.Navigated += FrameNav_Navigated;
        }

        private void FrameNav_Navigated(object sender, NavigationEventArgs e)
        {
            var content = frameNav.Content as Page;

            if (content is AuthorizationPage)
            {
                btnBack.Visibility = Visibility.Collapsed;
                btnForward.Visibility = Visibility.Collapsed;
            }
            else if (content is RegistartionPage)
            {
                btnBack.Visibility = Visibility.Visible;
                btnForward.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnBack.Visibility = Visibility.Visible;
                btnForward.Visibility = Visibility.Visible;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (frameNav.CanGoForward)
                frameNav.GoForward();
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (frameNav.CanGoBack)
                frameNav.GoBack();
        }
    }
}
