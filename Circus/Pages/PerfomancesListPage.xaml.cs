using Circus.DB;
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
        public List<Perfomance> Perfomances { get; set; }

        public PerfomancesListPage()
        {
            InitializeComponent();

            Perfomances = DataAccess.GetPerfomances();

            this.DataContext = this;
        }

        private void lvPerfomances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void cbSorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void checkActual_Click(object sender, RoutedEventArgs e)
        {
            ApplyFiltres();
        }

        private void ApplyFiltres()
        {
            throw new NotImplementedException();
        }
    }
}
