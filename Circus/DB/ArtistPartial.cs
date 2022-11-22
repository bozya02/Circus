using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circus.DB
{
    public partial class Artist
    {
        public ICollection<AnimalArtist> NotDeletedAnimalArtists => AnimalArtists.ToList().FindAll(x => !x.IsDeleted);
    }
}
