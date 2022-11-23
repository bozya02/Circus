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
                { "Название по возрастания", x => x.Name },
                { "Название по убыванию", x => x.Name },      //reverse
                { "Цена билета по возрастания", x => x.TicketPrice },
                { "Цена билета по убыванию", x => x.TicketPrice },      //reverse
                { "Количество мест по возрастания", x => x.TicketRemainder },
                { "Количество мест по убыванию", x => x.TicketRemainder },      //reverse
                { "Дата проведения по возрастания", x => x.Date },
                { "Дата проведения по убыванию", x => x.Date },     //reserse
            };

            Cities = DataAccess.GetCities();
            Cities.Insert(0, new City { Name = "Все города" });

            if (!App.User.IsAdmin)
                btnNewPerfomance.Visibility = Visibility.Collapsed;

            DataAccess.NewItemAddedEvent += DataAccess_NewItemAddedEvent;

            this.DataContext = this;
        }

        private void DataAccess_NewItemAddedEvent()
        {
            Perfomances = DataAccess.GetPerfomances();
            lvPerfomances.ItemsSource = Perfomances;
            lvPerfomances.Items.Refresh();
        }

        private void lvPerfomances_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var perfomance = lvPerfomances.SelectedItem as Perfomance;

            if (perfomance != null)
                NavigationService.Navigate(new PerfomancePage(perfomance));
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
            var isActual = (bool)checkActual.IsChecked;
            var sorting = Sortings[cbSorting.SelectedItem as string];
            var city = cbCity.SelectedItem as City;

            if (sorting == null || city == null)
                return;

            var perfomances = Perfomances.FindAll(x => x.Name.ToLower().Contains(perfomanceName));

            if (city.Name != "Все города")
                perfomances = perfomances.FindAll(x => x.City == city);

            if (isActual)
                perfomances = perfomances.FindAll(x => x.Date + x.StartTime > DateTime.Now);

            perfomances = (cbSorting.SelectedItem as string).ToLower().Contains("убыванию") ? 
                          perfomances.OrderByDescending(sorting).ToList() :
                          perfomances.OrderBy(sorting).ToList();


            lvPerfomances.ItemsSource = perfomances;
            lvPerfomances.Items.Refresh();
        }

        private void btnNewPerfomance_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PerfomancePage(new Perfomance(), true));
        }
    }
}
