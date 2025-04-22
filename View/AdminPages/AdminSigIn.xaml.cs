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
using Amalgama.Servis;
using Amalgama.View.Pages;
using MaterialDesignThemes.Wpf;
using Microsoft.EntityFrameworkCore;
using static Amalgama.Core.Navigation;
using static Amalgama.Servis.DBConnection;

namespace Amalgama.View.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AdminSigIn.xaml
    /// </summary>
    public partial class AdminSigIn : Window
    {
        private bool _isAdmin;
        private string _realPassword = string.Empty;
        private string _previousText = string.Empty;
        private bool _isUpdatingText = false;
        private DBConnection.ApplicationDbContext _db = new DBConnection.ApplicationDbContext();

        public AdminSigIn()
            {
                InitializeComponent();
               
            }

            private void AdminSigIn_Loaded(object sender, RoutedEventArgs e)
            {
                // Отображаем матовый фон при загрузке
                Overlay.Visibility = Visibility.Visible;
            }
        private void Password_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isUpdatingText)
                return;

            try
            {
                string currentText = Password.Text;

                // Проверяем разницу между введённым текстом и предыдущим состоянием
                if (currentText.Length < _previousText.Length)
                {
                    // Удаление символа
                    _realPassword = _realPassword.Substring(0, Math.Max(0, _realPassword.Length - (_previousText.Length - currentText.Length)));
                }
                else if (currentText.Length > _previousText.Length)
                {
                    // Добавление символа
                    int addedLength = currentText.Length - _previousText.Length;
                    _realPassword += currentText.Substring(_previousText.Length, addedLength);
                }

                _isUpdatingText = true;

                // Заменяем вводимые символы на точки
                Password.Text = new string('●', _realPassword.Length);
                Password.CaretIndex = Password.Text.Length;

                _previousText = Password.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке текста: {ex.Message}");
            }
            finally
            {
                _isUpdatingText = false;
            }
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
            {
                this.Close();
            }

        private void SignUp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Login.Text) || string.IsNullOrWhiteSpace(_realPassword))
            {
                MessageBox.Show("Логин и пароль не могут быть пустыми.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string username = Login.Text.Trim();
            string password = _realPassword; // Используем введённый пользователем пароль

            try
            {
                if (_db == null || _db.Users == null)
                {
                    MessageBox.Show("Ошибка инициализации базы данных.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = _db.Users.FirstOrDefault(x => x.Login == username && x.Password == password);

                if (user != null)
                {
                    // Устанавливаем пользователя в сессию
                    SessionManager.SetUser(user);

                    if (!user.IsAdmin)
                    {
                        MessageBox.Show("Доступ разрешён только администраторам.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    // Навигация для админа
                    CoreNavigate.NavigatorCore.Navigate(new DataRecForAdmin());

                    Window currentWindow = Window.GetWindow(this);
                    currentWindow?.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка ввода данных: неверный логин или пароль.",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Login.Text = string.Empty;
                    Password.Text = string.Empty;
                    _realPassword = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");

                MessageBox.Show($"Произошла ошибка при работе с базой данных: {ex.Message}",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
