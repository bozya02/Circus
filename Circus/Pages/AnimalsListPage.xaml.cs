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
    /// Interaction logic for AnimalsListPage.xaml
    /// </summary>
    public partial class AnimalsListPage : Page
    {
        public List<Animal> Animals { get; set; }
        public List<AnimalType> AnimalTypes { get; set; }

        public AnimalsListPage()
        {
            InitializeComponent();

            Animals = DataAccess.GetAnimals();
            AnimalTypes = DataAccess.GetAnimalTypes();

            AnimalTypes.Insert(0, new AnimalType { Name = "Все животные" });

            DataAccess.NewItemAddedEvent += DataAccess_NewItemAddedEvent;

            if (!App.User.IsAdmin)
                btnNewAnimal.Visibility = Visibility.Collapsed;

            this.DataContext = this;
        }

        private void DataAccess_NewItemAddedEvent()
        {
            Animals = DataAccess.GetAnimals();
            lvAnimals.ItemsSource = Animals;
            lvAnimals.Items.Refresh();
        }

        private void lvAnimals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var animal = lvAnimals.SelectedItem as Animal;
            if (animal != null)
                NavigationService.Navigate(new AnimalPage(animal));
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void cbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFiltres();
        }

        private void ApplyFiltres()
        {
            var animalName = tbSearch.Text.ToLower();
            var type = cbType.SelectedItem as AnimalType;

            if (type == null)
                return;

            var animals = Animals.FindAll(x => x.Name.ToLower().Contains(animalName));

            if (type.Name != "Все животные")
                animals = animals.FindAll(x => x.AnimalType == type);

            lvAnimals.ItemsSource = animals;
            lvAnimals.Items.Refresh();
        }

        private void btnNewAnimal_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AnimalPage(new Animal(), true));
        }
    }
}
