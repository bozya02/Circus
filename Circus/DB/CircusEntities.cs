using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circus.DB
{
    public partial class CircusEntities
    {
        private static CircusEntities _context;

        public static CircusEntities GetContext()
        {
            if (_context == null)
                _context = new CircusEntities();

            return _context;
        }
    }
}
