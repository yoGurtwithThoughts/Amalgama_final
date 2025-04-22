using Amalgama.Servis;
using Amalgama.View.Pages;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Amalgama.Core.Navigation;

namespace Amalgama.View.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для DataRecForAdmin.xaml
    /// </summary>
    public partial class DataRecForAdmin : Page
    {
        private bool isMenuOpen = false;
        public DataRecForAdmin()
        {
            InitializeComponent();
            LoadRecords();
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            DrawerHost.IsRightDrawerOpen = !DrawerHost.IsRightDrawerOpen;
            if (DrawerHost.IsRightDrawerOpen)
            {
                BlurOverlay.Visibility = Visibility.Visible;
                BackgroundBlur.Radius = 25; // Активируем размытие
            }
            else
            {
                BlurOverlay.Visibility = Visibility.Visible;
                BackgroundBlur.Radius = 0; // Активируем размытие
            }
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new StartPage());
        }

        private void Gallery_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new Gallery());
        }

        private void Masters_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new MastersPage());
        }

        private void QW_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new QwestionsPage());
        }

        private void LoadRecords()
        {
            string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database.db");
            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                // Выборка данных без ID
                string query = "SELECT FullName, PhoneNumber, Color, Description, Master, Date FROM Records"; // Исправлено на Records
                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        var records = new List<Record>();

                        while (reader.Read())
                        {
                            records.Add(new Record
                            {
                                FullName = reader.GetString(0),
                                PhoneNumber = reader.GetString(1),
                                Color = reader.GetString(2),
                                Description = reader.GetString(3),
                                Master = reader.GetString(4), // Добавлено поле Master
                                Date = reader.GetString(5)    // Добавлено поле Date
                            });
                        }

                        if (records.Count == 0)
                        {
                            MessageBox.Show("Нет записей для отображения."); // Отладочное сообщение
                        }
                        else
                        {
                            RecordsDataGrid.ItemsSource = records; // Привязка к DataGrid
                        }
                    }
                }
            }
    }
    }
}
