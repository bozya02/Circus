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
        public static List<City> GetCities() => CircusEntities.GetContext().Cities.ToList();
        public static List<Animal> GetAnimals() => CircusEntities.GetContext().Animals.ToList();
        public static List<AnimalType> GetAnimalTypes() => CircusEntities.GetContext().AnimalTypes.ToList();
        public static List<Artist> GetArtists() => CircusEntities.GetContext().Artists.ToList();
        public static List<Role> GetRoles() => CircusEntities.GetContext().Roles.ToList();
    }
}
