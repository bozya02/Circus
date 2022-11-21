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
    /// Interaction logic for PerfomancePage.xaml
    /// </summary>
    public partial class PerfomancePage : Page
    {
        public Perfomance Perfomance { get; set; }
        public List<City> Cities { get; set; }
        public List<Artist> Artists { get; set; }

        public PerfomancePage(Perfomance perfomance, bool isNeww = false)
        {
            InitializeComponent();

            Perfomance = perfomance;
            Cities = DataAccess.GetCities();
            Artists = DataAccess.GetArtists();

            this.DataContext = this;
        }
    }
}
