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
    /// Interaction logic for TicketsListPage.xaml
    /// </summary>
    public partial class TicketsListPage : Page
    {
        public List<Ticket> Tickets { get; set; }

        public TicketsListPage()
        {
            InitializeComponent();

            Tickets = App.User.IsAdmin ? DataAccess.GetTickets() : DataAccess.GetTickets(App.User);

            this.DataContext = this;
        }

        private void lvTickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            new Windows.TicketWindow(lvTickets.SelectedItem as Ticket).ShowDialog();
        }
    }
}
