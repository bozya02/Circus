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
    /// Interaction logic for ArtistPage.xaml
    /// </summary>
    public partial class ArtistPage : Page
    {
        public Artist Artist { get; set; }
        public List<Role> Roles { get; set; }
        public List<Animal> Animals { get; set; }

        public ArtistPage(Artist artist, bool isNew = false)
        {
            InitializeComponent();

            Artist = artist;
            Roles = DataAccess.GetRoles();
            Animals = DataAccess.GetAnimals();

            if (isNew)
                Title = $"Новый {Title}";
            else
                Title = $"{Title} {Artist.Patronymic}";

            this.DataContext = this;
        }
    }
}
