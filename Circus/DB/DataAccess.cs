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
        public static List<User> GetUsers() => CircusEntities.GetContext().Users.ToList();


        public static User LoginUser(string login, string password)
        {
            return GetUsers().FirstOrDefault(x => x.Login == login && x.Password == password);
        }

        public static bool IsLoginUnique(string login) => GetUsers().Any(x => x.Login == login);

        public static void SaveUser(User user)
        {
            if (user.Id == 0)
                CircusEntities.GetContext().Users.Add(user);

            CircusEntities.GetContext().SaveChanges();
        }

        public static void SavePerfomance(Perfomance perfomance)
        {
            if (perfomance.Id == 0)
                CircusEntities.GetContext().Perfomances.Add(perfomance);

            CircusEntities.GetContext().SaveChanges();
        }

        public static void DeletePerfomance(Perfomance perfomance)
        {
            
        }

        public static void SaveAnimal(Animal animal)
        {
            if (animal.Id == 0)
                CircusEntities.GetContext().Animals.Add(animal);

            CircusEntities.GetContext().SaveChanges();
        }

        public static void DeleteAnimal(Animal animal)
        {

        }

        public static void SaveArtist(Artist artist)
        {
            if (artist.Id == 0)
                CircusEntities.GetContext().Artists.Add(artist);

            CircusEntities.GetContext().SaveChanges();
        }

        public static void DeleteArtist(Artist artist)
        {

        }
    }
}
