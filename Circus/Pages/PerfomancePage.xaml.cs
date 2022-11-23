using Circus.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for PerfomancePage.xaml
    /// </summary>
    public partial class PerfomancePage : Page
    {
        public Perfomance Perfomance { get; set; }
        public List<City> Cities { get; set; }
        public List<Artist> Artists { get; set; }

        public PerfomancePage(Perfomance perfomance, bool isNew = false)
        {
            InitializeComponent();

            Perfomance = perfomance;
            Cities = DataAccess.GetCities();
            Artists = DataAccess.GetArtists();

            if (isNew)
            {
                Title = $"Новое {Title}";
                btnDelete.Visibility = Visibility.Collapsed;
                checkSaleReady.Visibility = Visibility.Hidden;
                Perfomance.Date = DateTime.Now;
            }
            else
            {
                Title = $"{Title} {Perfomance.Name}";
                checkSaleReady.Visibility = Visibility.Visible;
            }

            dpDate.DisplayDateStart = DateTime.Now;
            tpStartTime.SelectedTime = DateTime.Today + Perfomance.StartTime;
            tpEndTime.SelectedTime = DateTime.Today + Perfomance.EndTime;

            ChangeEnable();

            this.DataContext = this;
        }

        private void ChangeEnable()
        {
            var enable = !Perfomance.IsSaleReady && App.User.IsAdmin;

            tbName.IsEnabled = enable;
            dpDate.IsEnabled = enable;
            tpStartTime.IsEnabled = enable;
            tpEndTime.IsEnabled = enable;
            cbCity.IsEnabled = enable;
            btnSelectImage.IsEnabled = enable;
            checkSaleReady.IsEnabled = enable;
            tbTicketQuantity.IsEnabled = enable;
            cbArtist.IsEnabled = enable;
            lvArtistPerfomance.IsEnabled = enable;
            
            if (!App.User.IsAdmin)
            {
                btnDelete.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;
            }

            if (Perfomance.IsSaleReady)
            {
                tbTicketQuantity.Visibility = Visibility.Hidden;
                tbTicketRemainder.Visibility = Visibility.Visible;
                btnBuyTicket.Visibility = Visibility.Visible;
            }    
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Perfomance.Name))
                sb.AppendLine("Название не может быть пустым!");
            if (Perfomance.StartTime < DateTime.Now.TimeOfDay && Perfomance.Date.Date == DateTime.Today.Date)
                sb.AppendLine("Некорректное время начала!");
            if (Perfomance.EndTime < Perfomance.StartTime)
                sb.AppendLine("Некорректное время окончания!");
            if (Perfomance.EndTime - Perfomance.StartTime < TimeSpan.FromHours(1))
                sb.AppendLine("Продолжительность выступления должна быть минимум 1 час!");
            if (Perfomance.City == null)
                sb.AppendLine("Город не выбран!");
            if (Perfomance.ArtistPerfomances.Count == 0)
                sb.AppendLine("Добавьте минимум одного артиста!");

            if (sb.Length != 0)
            {
                MessageBox.Show(sb.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataAccess.SavePerfomance(Perfomance);
            NavigationService.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!DataAccess.CanDeletePerfomance(Perfomance))
            {
                MessageBox.Show("Нельзя удалить это выступление!\nУ него есть проданные билеты.", "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить это выступление?", "Предупреждение",
                                MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            try
            {
                DataAccess.DeletePerfomance(Perfomance);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка");
            }
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "*.jpg|*.jpg|*.jpeg|*.jpeg|*.png|*.png"
            };

            if (fileDialog.ShowDialog().Value)
            {
                var image = File.ReadAllBytes(fileDialog.FileName);

                Perfomance.Image = image;

                ingPerfomance.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void btnBuyTicket_Click(object sender, RoutedEventArgs e)
        {
            if (Perfomance.Tickets.Count() >= Perfomance.TicketQuantity)
            {
                MessageBox.Show($"Все билеты проданы!", "Ошибка",
                                         MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            var result = MessageBox.Show($"Вы точно хотите купить билет?", "Предупреждение",
                                         MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            Ticket ticket = new Ticket
            {
                Perfomance = Perfomance,
                User = App.User
            };

            Perfomance.Tickets.Add(ticket);
            DataAccess.SavePerfomance(Perfomance);
        }

        private void cbArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var artist = cbArtist.SelectedItem as Artist;

            if (artist == null)
                return;

            Perfomance.ArtistPerfomances.Add(new ArtistPerfomance
            {
                Artist = artist
            });

            lvArtistPerfomance.ItemsSource = Perfomance.ArtistPerfomances;
            lvArtistPerfomance.Items.Refresh();

            tbTickerPrice.Text = Perfomance.TicketPrice.ToString();
        }

        private void lvArtistPerfomance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Perfomance.IsSaleReady || !App.User.IsAdmin)
                return;

            var artistPerfomance = lvArtistPerfomance.SelectedItem as ArtistPerfomance;

            if (artistPerfomance == null)
                return;

            var result = MessageBox.Show($"Вы точно хотите убрать {artistPerfomance.Artist.Nickname} из выступдения?", "Предупреждение",
                                         MessageBoxButton.YesNoCancel, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            Perfomance.ArtistPerfomances.Remove(artistPerfomance);
            DataAccess.DeleteArtistPerfomance(artistPerfomance);

            lvArtistPerfomance.ItemsSource = Perfomance.ArtistPerfomances;
            lvArtistPerfomance.Items.Refresh();

            tbTickerPrice.Text = $"{Perfomance.TicketPrice} руб.";
        }

        private void tpStartTime_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (tpStartTime.SelectedTime == null)
                return;
            Perfomance.StartTime = tpStartTime.SelectedTime.Value.TimeOfDay;
        }

        private void tpEndTime_SelectedTimeChanged(object sender, RoutedPropertyChangedEventArgs<DateTime?> e)
        {
            if (tpEndTime.SelectedTime == null)
                return;
            Perfomance.EndTime = tpEndTime.SelectedTime.Value.TimeOfDay;
        }

        private void tbTicketQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            Perfomance.TicketQuantity = int.Parse(tbTicketQuantity.Text);
            tbTickerPrice.Text = $"{Perfomance.TicketPrice} руб.";
        }

        private void cbAnimal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var animalArtist = e.AddedItems[0] as AnimalArtist;

            ((sender as ComboBox).DataContext as ArtistPerfomance).AnimalArtist = animalArtist;
        }
    }
}
