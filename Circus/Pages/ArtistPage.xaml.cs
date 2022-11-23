using Circus.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ArtistPage.xaml
    /// </summary>
    public partial class ArtistPage : Page
    {
        public Artist Artist { get; set; }
        public List<Role> Roles { get; set; }
        public List<Animal> Animals { get; set; }

        public ArtistPage(Artist artist, bool isNew = false)
        {
            InitializeComponent();

            Artist = artist;
            Roles = DataAccess.GetRoles();
            Animals = DataAccess.GetAnimals();

            if (isNew)
            {
                Title = $"Новый {Title}";
                btnDelete.Visibility = Visibility.Collapsed;
            }
            else
                Title = $"{Title} {Artist.Nickname}";

            if (!App.User.IsAdmin)
                grid.IsEnabled = false;

            this.DataContext = this;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Artist.Nickname))
                sb.AppendLine("Сценическое имя не может быть пустым!");
            if (string.IsNullOrWhiteSpace(Artist.FirstName))
                sb.AppendLine("Фамилия не должна быть пустой!");
            if (string.IsNullOrWhiteSpace(Artist.LastName))
                sb.AppendLine("Имя не должно быть пустым!");
            if (Artist.Role == null)
                sb.AppendLine("Нужно выбрать тип артиста!");
            if (Artist.Salary <= 0)
                sb.AppendLine("Введен неверный оклад!");

            if (sb.Length != 0)
            {
                MessageBox.Show(sb.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataAccess.SaveArtist(Artist);
            NavigationService.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!DataAccess.CanDeleteArtist(Artist))
            {
                MessageBox.Show("Нельзя удалить этого артиста.\nУ него есть пердстоящие выступления.", "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить этого артиста?", "Предупреждение", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            try
            {
                DataAccess.DeleteArtist(Artist);
                NavigationService.GoBack();
            }
            catch
            {
                MessageBox.Show("Непредвиденная ошибка", "Ошибка");
            }
            
        }

        private void btnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "*.jpg|*.jpg|*.jpeg|*.jpeg|*.png|*.png"
            };

            if (fileDialog.ShowDialog().Value)
            {
                var image = File.ReadAllBytes(fileDialog.FileName);

                Artist.Image = image;

                imgArtist.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void cbAnimal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var animal = cbAnimal.SelectedItem as Animal;

            if (animal == null)
                return;


            if (Artist.AnimalArtists.Any(x => x.Animal.Id == animal.Id))
            {
                MessageBox.Show("Данное животное уже добавлено", "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }

            Artist.AnimalArtists.Add(new AnimalArtist
            {
                Animal = animal,
                Artist = Artist
            });

            lvAnimalArtists.ItemsSource = Artist.NotDeletedAnimalArtists;
            lvAnimalArtists.Items.Refresh();
        }

        private void lvAnimalArtists_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var animalArtist = lvAnimalArtists.SelectedItem as AnimalArtist;

            if (animalArtist == null)
                return;

            var result = MessageBox.Show($"Вы точно хотите убрать {animalArtist.Animal.Name} у {Artist.Nickname}?", "Предупреждение",
                                         MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            animalArtist.IsDeleted = true;

            lvAnimalArtists.ItemsSource = Artist.NotDeletedAnimalArtists;
            lvAnimalArtists.Items.Refresh();
        }
    }
}
