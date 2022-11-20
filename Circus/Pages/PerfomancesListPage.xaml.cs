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
        public List<City> Cities { get; set; }
        public Dictionary<string, Func<Perfomance, object>> Sortings { get; set; }

        public PerfomancesListPage()
        {
            InitializeComponent();

            Perfomances = DataAccess.GetPerfomances();
            Sortings = new Dictionary<string, Func<Perfomance, object>>
            {
                { "Без сортировки", x => x.Id },
                { "Название по возрастания", x => x.Tickets },
                { "Название по убыванию", x => x.Tickets },      //reverse
                { "Цена билета по возрастания", x => x.Tickets },
                { "Цена билета по убыванию", x => x.Tickets },      //reverse
                { "Количество мест по возрастания", x => x.Tickets },
                { "Количество мест по убыванию", x => x.City },      //reverse
                { "Дата проведения по возрастания", x => x.Date },
                { "Дата проведения по убыванию", x => x.Date },     //reserse
            };

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

        private void cbCity_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void ApplyFiltres()
        {
            var perfomanceName = tbSearch.Text.ToLower();
            var isActual = checkActual.IsChecked;
            var sorting = Sortings[cbSorting.SelectedItem as string];
            var city = cbCity.SelectedItem as City;

            if (sorting == null || city == null)
                return;

            var perfomances = Perfomances.FindAll(x => x.Name.ToLower().Contains(perfomanceName) &&
                                                       x.Date > DateTime.Now && x.City == city);

            perfomances = (cbSorting.SelectedItem as string).ToLower().Contains("убыванию") ? 
                          perfomances.OrderBy(sorting).ToList() :
                          perfomances.OrderByDescending(sorting).ToList();


            lvPerfomances.ItemsSource = perfomances;
            lvPerfomances.Items.Refresh();
        }
    }
}
