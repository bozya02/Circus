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

namespace Circus.Pages
{
    /// <summary>
    /// Interaction logic for PerfomancesListPage.xaml
    /// </summary>
    public partial class PerfomancesListPage : Page
    {
        public PerfomancesListPage()
        {
            InitializeComponent();
        }

        private void lvPerfomances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void cbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void checkActual_Checked(object sender, RoutedEventArgs e)
        {
            ApplyFiltres();
        }

        private void ApplyFiltres()
        {
            throw new NotImplementedException();
        }
    }
}
