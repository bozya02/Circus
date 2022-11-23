using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circus.DB
{
    public partial class Perfomance
    {
        public double Cost => ArtistPerfomances.Sum(x => (double)x.Artist.Salary) * 1.4;
        public double TicketPrice
        {
            get => Cost / TicketQuantity;
            set { }
        }
        public int TicketRemainder
        {
            get => TicketQuantity - Tickets.Count();
            set { }
        }
    }
}
