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
    /// Interaction logic for AnimalPage.xaml
    /// </summary>
    public partial class AnimalPage : Page
    {
        public Animal Animal { get; set; }
        public List<AnimalType> AnimalTypes { get; set; }

        public AnimalPage(Animal animal, bool isNew = false)
        {
            InitializeComponent();
            
            Animal = animal;
            AnimalTypes = DataAccess.GetAnimalTypes();

            if (isNew)
            {
                Title = $"Новое {Title}";
                btnDelete.Visibility= Visibility.Collapsed;
            }
            else
                Title = $"{Title} {Animal.Name}";

            grid.IsEnabled = App.User.IsAdmin;

            this.DataContext = this;
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

                Animal.Image = image;

                imgAnimal.Source = new BitmapImage(new Uri(fileDialog.FileName));
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Animal.Name))
                sb.AppendLine("Имя живтоного не может быть пустым!");
            if (Animal.AnimalType == null)
                sb.AppendLine("Нужно выбрать тип животного!");

            if (sb.Length != 0)
            {
                MessageBox.Show(sb.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DataAccess.SaveAnimal(Animal);
            NavigationService.GoBack();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (DataAccess.CanDeleteAnimal(Animal))
            {
                MessageBox.Show("Нельзя удалить это животное.\nУ него есть пердстоящие выступления.", "Ошибка", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                return;
            }
            if (MessageBox.Show("Вы точно хотите удалить это животное?", "Предупреждение", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                return;

            DataAccess.DeleteAnimal(Animal);
            NavigationService.GoBack();
        }
    }
}
