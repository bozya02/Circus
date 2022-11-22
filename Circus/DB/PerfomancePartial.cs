﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circus.DB
{
    public partial class Perfomance
    {
        public double Cost => ArtistPerfomances.Sum(x => (double)x.Artist.Salary) * 1.4;
        public double TicketPrice => Cost / TicketQuantity;
        public int TicketRemainder => TicketQuantity - Tickets.Count();
    }
}
