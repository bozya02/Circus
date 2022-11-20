using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circus.DB
{
    public class DataAccess
    {
        
        public static List<Perfomance> GetPerfomances() => CircusEntities.GetContext().Perfomances.ToList();
    }
}
