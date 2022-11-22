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
                Title = $"Новое {Title}";
            else
                Title = $"{Title} {Animal.Name}";

            this.DataContext = this;
        }
    }
}
