using Amalgama.Servis;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Amalgama.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для RecordPage.xaml
    /// </summary>
    public partial class RecordPage : Page
    {
        private string selectedMaster = "Не указан"; // Переменная для хранения выбранного мастера
        private DispatcherTimer _timer;
        public RecordPage()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(5);
            _timer.Tick += Timer_Tick;
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            MessageTextBlock.Visibility = Visibility.Collapsed;
            _timer.Stop();
        }

        private void Imageclosed_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void AgeAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageTextBlock.Text = "Ваш возраст добавлен!";
            MessageTextBlock.Visibility = Visibility.Visible;


            _timer.Start();

        }

        private void RecCons_Click(object sender, RoutedEventArgs e)
        {
            RecCons.Background = new SolidColorBrush(Color.FromRgb(135, 0, 0));
            Seance.Background = new SolidColorBrush(Colors.Transparent);
            RecCons.Foreground = new SolidColorBrush(Colors.White);
            Seance.Foreground = new SolidColorBrush(Colors.Black);
            MessageTextBlock.Text = "Вы выбрали запись на консультацию";
            MessageTextBlock.Visibility = Visibility.Visible;


            _timer.Start();
        }

        private void Seance_Click(object sender, RoutedEventArgs e)
        {

            Seance.Background = new SolidColorBrush(Color.FromRgb(95, 0, 0));
            RecCons.Background = new SolidColorBrush(Colors.Transparent);
            RecCons.Foreground = new SolidColorBrush(Colors.Black);
            Seance.Foreground = new SolidColorBrush(Colors.White);
            MessageTextBlock.Text = "Вы выбрали запись на сеанс";
            MessageTextBlock.Visibility = Visibility.Visible;


            _timer.Start();
        }

        private void HideAllMasters()
        {
            // Скрываем всех мастеров
            TattooMasters.Visibility = Visibility.Collapsed;
            TattooMastersText.Visibility = Visibility.Collapsed;

            PiercingMasters.Visibility = Visibility.Collapsed;
            PiercingMastersText.Visibility = Visibility.Collapsed;

            RemoveTattooMasters.Visibility = Visibility.Collapsed;
            RemoveTattooMastersText.Visibility = Visibility.Collapsed;
        }

        private void Tatoo_Click(object sender, RoutedEventArgs e)
        {
            HideAllMasters(); // Сначала скрываем всех мастеров

            Tatoo.Background = new SolidColorBrush(Color.FromRgb(135, 0, 0));
            Pirc.Background = new SolidColorBrush(Colors.Transparent);
            Remove.Background = new SolidColorBrush(Colors.Transparent);

            Tatoo.Foreground = new SolidColorBrush(Colors.White);
            Pirc.Foreground = new SolidColorBrush(Colors.Black);
            Remove.Foreground = new SolidColorBrush(Colors.Black);

            MessageTextBlock.Text = "Вы выбрали мастера Maria";
            MessageTextBlock.Visibility = Visibility.Visible;

            // Показываем мастеров для татуировки
            TattooMasters.Visibility = Visibility.Visible;
            TattooMastersText.Visibility = Visibility.Visible;

            RemoveTattooMasters.Visibility = Visibility.Visible;
            RemoveTattooMastersText.Visibility = Visibility.Visible;
            RemoveTattooMasters.Margin = new Thickness(25, 0, 0, 0);

            _timer.Start();
        }

        private void Pirc_Click(object sender, RoutedEventArgs e)
        {
            HideAllMasters();

            Pirc.Background = new SolidColorBrush(Color.FromRgb(95, 0, 0));
            Tatoo.Background = new SolidColorBrush(Colors.Transparent);
            Remove.Background = new SolidColorBrush(Colors.Transparent);

            Pirc.Foreground = new SolidColorBrush(Colors.White);
            Tatoo.Foreground = new SolidColorBrush(Colors.Black);
            Remove.Foreground = new SolidColorBrush(Colors.Black);

            MessageTextBlock.Text = "Вы выбрали пирсинг";
            MessageTextBlock.Visibility = Visibility.Visible;

            // Показываем мастера для пирсинга
            PiercingMasters.Visibility = Visibility.Visible;
            PiercingMastersText.Visibility = Visibility.Visible;

            _timer.Start();
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            HideAllMasters();

            Remove.Background = new SolidColorBrush(Color.FromRgb(95, 0, 0));
            Tatoo.Background = new SolidColorBrush(Colors.Transparent);
            Pirc.Background = new SolidColorBrush(Colors.Transparent);

            Remove.Foreground = new SolidColorBrush(Colors.White);
            Pirc.Foreground = new SolidColorBrush(Colors.Black);
            Tatoo.Foreground = new SolidColorBrush(Colors.Black);

            MessageTextBlock.Text = "Вы выбрали сведение татуировки";
            MessageTextBlock.Visibility = Visibility.Visible;

            // Показываем мастера для сведения татуировки
            RemoveTattooMasters.Visibility = Visibility.Visible;
            RemoveTattooMastersText.Visibility = Visibility.Visible;
            RemoveTattooMasters.Margin = new Thickness(0);
            _timer.Start();
        }

        private void Txt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txt.Text) || Txt.Text == "Фамилия имя")
            {
                Txt.Text = string.Empty;
                Txt.Foreground = new SolidColorBrush(Colors.Black);
                Txt.Focus();
                Txt.SelectionStart = Txt.Text.Length;
            }

        }

        private void Txt1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txt.Text) || Txt.Text == "Номер телефона")
            {
                Txt.Text = string.Empty;
                Txt.Foreground = new SolidColorBrush(Colors.Black);
                Txt.Focus();
                Txt.SelectionStart = Txt.Text.Length;
            }
        }

        private void ColorTatoo_Click(object sender, RoutedEventArgs e)
        {
            ColorTatoo.Background = new SolidColorBrush(Color.FromRgb(135, 0, 0));
            Mono.Background = new SolidColorBrush(Colors.Transparent);
            ColorTatoo.Foreground = new SolidColorBrush(Colors.White);
            Mono.Foreground = new SolidColorBrush(Colors.Black);
            MessageTextBlock.Text = "Вы выбрали цветную тату";
            MessageTextBlock.Visibility = Visibility.Visible;


            _timer.Start();

        }

        private void Mono_Click(object sender, RoutedEventArgs e)
        {
            Mono.Background = new SolidColorBrush(Color.FromRgb(135, 0, 0));
            ColorTatoo.Background = new SolidColorBrush(Colors.Transparent);
            Mono.Foreground = new SolidColorBrush(Colors.White);
            ColorTatoo.Foreground = new SolidColorBrush(Colors.Black);
            MessageTextBlock.Text = "Вы выбрали ч/б тату";
            MessageTextBlock.Visibility = Visibility.Visible;


            _timer.Start();
        }

        private void Txt2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Txt2.Text) || Txt2.Text == "Номер телефона")
            {
                Txt2.Text = string.Empty;
                Txt2.Foreground = new SolidColorBrush(Colors.Black);
                Txt2.Focus();
                Txt2.SelectionStart = Txt.Text.Length;
            }
        }

       

        private void MastersSelect1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageTextBlock.Text = "Вы выбрали мастра Angelina";
            MessageTextBlock.Visibility = Visibility.Visible;


            _timer.Start();
        }
        private void MastersSelect2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageTextBlock.Text = "Вы выбрали мастра Arisha";
            MessageTextBlock.Visibility = Visibility.Visible;


            _timer.Start();
        }
        private void MastersSelect3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageTextBlock.Text = "Вы выбрали мастра Arisha";
            MessageTextBlock.Visibility = Visibility.Visible;


            _timer.Start();
        }

        private void RecSucces_Click(object sender, RoutedEventArgs e)
        {
            string dbPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "database.db");

            // Создание и инициализация базы данных
            DatabaseManager dbManager = new DatabaseManager(dbPath);
            dbManager.InitializeDatabase(); // Удаляем старую таблицу и создаем новую

            using (SQLiteConnection conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();

                // Проверка и обработка введенных данных
                string fullName = Txt.Text == "Фамилия имя" || string.IsNullOrWhiteSpace(Txt.Text) ? "Не указано" : Txt.Text;
                string phoneNumber = Txt1.Text == "Номер телефона" || string.IsNullOrWhiteSpace(Txt1.Text) ? "Не указан" : Txt1.Text;
                string description = Txt2.Text == "Опишите желаемый результат" || string.IsNullOrWhiteSpace(Txt2.Text) ? "Без описания" : Txt2.Text;

                // Определение цвета
                string color = ColorTatoo.IsPressed ? "Цветная" : Mono.IsPressed ? "Ч/Б" : "Не указан";

                // Получение имени мастера
                string master = selectedMaster; // Переменная, которая хранит имя выбранного мастера

                // Получение введённой пользователем даты
                string date = TxtDate.Text; // Получаем дату из текстового поля

                // Вставка данных
                string query = "INSERT INTO Records (FullName, PhoneNumber, Color, Description, Master, Date) VALUES (@FullName, @PhoneNumber, @Color, @Description, @Master, @Date)";

                using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@Color", color);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@Master", master); // Добавлено имя мастера
                    cmd.Parameters.AddWithValue("@Date", date); // Добавлено введенное пользователем значение даты
                    cmd.ExecuteNonQuery();
                }
            }

            MessageTextBlock.Text = "Вы успешно записаны!";
            MessageTextBlock.Visibility = Visibility.Visible;
            _timer.Start();
        }
        private void SelectM_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedMaster = "Arisha"; // Имя мастера
        }

        private void Select1M_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedMaster = "Maria"; // Имя мастера
        }
        private void Select2M_MouseDown(object sender, MouseButtonEventArgs e)
        {
            selectedMaster = "Angelina"; // Имя мастера
        }
        private void Txt3_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TxtDate_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TattooMasters_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
    
    

