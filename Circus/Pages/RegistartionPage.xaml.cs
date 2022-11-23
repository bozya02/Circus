using Circus.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for RegistartionPage.xaml
    /// </summary>
    public partial class RegistartionPage : Page
    {
        public RegistartionPage()
        {
            InitializeComponent();
        }

        private void btnRegistration_Click(object sender, RoutedEventArgs e)
        {
            User user = new User
            {
                Login = tbLogin.Text,
                Password = pbPassword.Password,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Patronymic = tbPatronymic.Text
            };

            StringBuilder sb = new StringBuilder();

            if (string.IsNullOrWhiteSpace(user.LastName))
                sb.AppendLine("Фамилия не должна быть пустой!");
            if (string.IsNullOrWhiteSpace(user.FirstName))
                sb.AppendLine("Имя не должно быть пустым!");
            if (string.IsNullOrWhiteSpace(user.Login))
                sb.AppendLine("Логин не должен быть пустым!");
            if (string.IsNullOrWhiteSpace(user.Password))
                sb.AppendLine("Пароль не должен быть пустым!");
            else if (user.Password.Length < 6)
                sb.AppendLine("Пароль должен содержать минимум 6 символов");
            if (DataAccess.IsLoginUnique(user.Login))
                sb.AppendLine("Этот логин занят, выберите другой!");
            if (user.Password != pbSecondPassword.Password)
                sb.AppendLine("Пароли не совпадают!");

            if (sb.Length != 0)
            {
                MessageBox.Show(sb.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                MessageBox.Show("Регистрация выполнена успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DataAccess.SaveUser(user);
                NavigationService.GoBack();
            }
        }
    }
}
