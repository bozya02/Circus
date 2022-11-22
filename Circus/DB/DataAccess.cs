using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circus.DB
{
    public class DataAccess
    {
        public delegate void NewItemAdded();
        public static event NewItemAdded NewItemAddedEvent;

        public static List<Perfomance> GetPerfomances() => CircusEntities.GetContext().Perfomances.ToList().FindAll(x => !x.IsDeleted);
        public static List<City> GetCities() => CircusEntities.GetContext().Cities.ToList();
        public static List<Animal> GetAnimals() => CircusEntities.GetContext().Animals.ToList().FindAll(x => !x.IsDeleted);
        public static List<AnimalType> GetAnimalTypes() => CircusEntities.GetContext().AnimalTypes.ToList();
        public static List<Artist> GetArtists() => CircusEntities.GetContext().Artists.ToList().FindAll(x => !x.IsDeleted);
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
            NewItemAddedEvent?.Invoke();
        }

        public static void SavePerfomance(Perfomance perfomance)
        {
            if (perfomance.Id == 0)
                CircusEntities.GetContext().Perfomances.Add(perfomance);

            CircusEntities.GetContext().SaveChanges();
            NewItemAddedEvent?.Invoke();
        }

        public static bool CanDeletePerfomance(Perfomance perfomance)
        {
            return perfomance.Date > DateTime.Now && perfomance.Tickets.Count == 0;
        }

        public static void DeletePerfomance(Perfomance perfomance)
        {
            perfomance.IsDeleted = true;

            SavePerfomance(perfomance);
        }

        public static void SaveAnimal(Animal animal)
        {
            if (animal.Id == 0)
                CircusEntities.GetContext().Animals.Add(animal);

            CircusEntities.GetContext().SaveChanges();
            NewItemAddedEvent?.Invoke();
        }

        public static bool CanDeleteAnimal(Animal animal)
        {
            var canDelete = false;
            foreach (var animalArtist in animal.AnimalArtists)
            {
                foreach (var artistPerfomance in animalArtist.ArtistPerfomances)
                {
                    if (CanDeletePerfomance(artistPerfomance.Perfomance))
                    {
                        canDelete = true;
                        break;
                    }    
                }
                if (canDelete)
                    break;
            }

            return canDelete;
        }

        public static void DeleteAnimal(Animal animal)
        {
            animal.IsDeleted = true;

            foreach (var animalArtist in animal.AnimalArtists)
            {
                animalArtist.IsDeleted = true;
            }

            SaveAnimal(animal);
        }

        public static void SaveArtist(Artist artist)
        {
            if (artist.Id == 0)
                CircusEntities.GetContext().Artists.Add(artist);

            CircusEntities.GetContext().SaveChanges();
            NewItemAddedEvent?.Invoke();
        }

        public static bool CanDeleteArtist(Artist artist)
        {
            var canDelete = false;

            foreach (var artistPerfomance in artist.ArtistPerfomances)
            {
                if (CanDeletePerfomance(artistPerfomance.Perfomance))
                {
                    canDelete = true;
                    break;
                }
            }

            return canDelete;
        }

        public static void DeleteArtist(Artist artist)
        {
            artist.IsDeleted = true;

            foreach (var animalArtist in artist.AnimalArtists)
            {
                animalArtist.IsDeleted = true;
            }

            SaveArtist(artist);
        }

        public static void DeleteArtistPerfomance(ArtistPerfomance artistPerfomance)
        {
            CircusEntities.GetContext().ArtistPerfomances.Remove(artistPerfomance);
            CircusEntities.GetContext().SaveChanges();
        }

        public static void DeleteTicket(Ticket ticket)
        {
            CircusEntities.GetContext().Tickets.Remove(ticket);
            CircusEntities.GetContext().SaveChanges();
            NewItemAddedEvent?.Invoke();
        }
    }
}
