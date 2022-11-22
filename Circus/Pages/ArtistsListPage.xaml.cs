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
    /// Interaction logic for ArtistsListPage.xaml
    /// </summary>
    public partial class ArtistsListPage : Page
    {
        public List<Artist> Artists { get; set; }
        public List<Role> Roles { get; set; }

        public ArtistsListPage()
        {
            InitializeComponent();

            Artists = DataAccess.GetArtists();
            Roles = DataAccess.GetRoles();

            DataAccess.NewItemAddedEvent += DataAccess_NewItemAddedEvent;

            this.DataContext = this;
        }

        private void DataAccess_NewItemAddedEvent()
        {
            Artists = DataAccess.GetArtists();
            lvArtists.ItemsSource = Artists;
            lvArtists.Items.Refresh();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void cbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void ApplyFiltres()
        {
            var name = tbSearch.Text.ToLower();
            var role = cbRole.SelectedItem as Role;

            if (role == null)
                return;

            var artists = Artists.FindAll(x => (x.LastName.ToLower().Contains(name) ||
                                                x.FirstName.ToLower().Contains(name)) &&
                                                x.Role == role);

            lvArtists.ItemsSource = artists;
            lvArtists.Items.Refresh();
        }

        private void btnNewArtist_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ArtistPage(new Artist(), true));
        }

        private void lvArtists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var artist = lvArtists.SelectedItem as Artist;
            if (artist != null)
                NavigationService.Navigate(new ArtistPage(artist));
        }
    }
}
