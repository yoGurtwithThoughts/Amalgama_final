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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Amalgama.Core.Navigation;
using Path = System.IO.Path;

namespace Amalgama.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Gallery.xaml
    /// </summary>
    public partial class Gallery : Page
    {
        private string currentCategory = "Tatoo";
        private bool _isAdmin = true; // Уровень доступа
        private string[] imagePaths =
        {
        "/Images/MastersPersonalDate/TatooWorks/1.png",
        "/Images/MastersPersonalDate/TatooWorks/2.png",
        "/Images/MastersPersonalDate/TatooWorks/3.png",
        "/Images/MastersPersonalDate/TatooWorks/4.png",
        "/Images/MastersPersonalDate/TatooWorks/5.png",
        "/Images/MastersPersonalDate/TatooWorks/6.png",
        "/Images/MastersPersonalDate/TatooWorks/7.png",
        "/Images/MastersPersonalDate/TatooWorks/8.png",
        "/Images/MastersPersonalDate/TatooWorks/9.png",
        "/Images/MastersPersonalDate/TatooWorks/10.png",
        "/Images/MastersPersonalDate/RemoweWorks/1.png",
        "/Images/MastersPersonalDate/RemoweWorks/2.png",
        "/Images/MastersPersonalDate/RemoweWorks/3.png",
        "/Images/MastersPersonalDate/RemoweWorks/4.png",
        "/Images/MastersPersonalDate/RemoweWorks/5.png",
        "/Images/MastersPersonalDate/RemoweWorks/6.png",
        "/Images/MastersPersonalDate/RemoweWorks/7.png",
        "/Images/MastersPersonalDate/RemoweWorks/8.png",
        "/Images/MastersPersonalDate/RemoweWorks/9.png",
        "/Images/MastersPersonalDate/PirsWorks/1.png",
        "/Images/MastersPersonalDate/PirsWorks/2.png",
        "/Images/MastersPersonalDate/PirsWorks/1.png",
        "/Images/MastersPersonalDate/PirsWorks/3.png",
        "/Images/MastersPersonalDate/PirsWorks/4.png",
        "/Images/MastersPersonalDate/PirsWorks/5.png",
        "/Images/MastersPersonalDate/PirsWorks/6.png",
        "/Images/MastersPersonalDate/PirsWorks/7.png",
        "/Images/MastersPersonalDate/PirsWorks/8.png",
        "/Images/MastersPersonalDate/PirsWorks/9.png",
        "/Images/MastersPersonalDate/PirsWorks/10.png",
        "/Images/MastersPersonalDate/PirsWorks/11.png",
        "/Images/MastersPersonalDate/PirsWorks/15.png",
        "/Images/MastersPersonalDate/PirsWorks/12.png",
        "/Images/MastersPersonalDate/PirsWorks/13.png",
        "/Images/MastersPersonalDate/PirsWorks/14.png",
        "/Images/MastersPersonalDate/PirsWorks/16.png",
        "/Images/MastersPersonalDate/PirsWorks/17.png",
        "/Images/MastersPersonalDate/PirsWorks/18.png",
        "/Images/MastersPersonalDate/PirsWorks/19.png",
        "/Images/MastersPersonalDate/PirsWorks/20.png",
        "/Images/MastersPersonalDate/PirsWorks/21.png",
        "/Images/MastersPersonalDate/PirsWorks/22.png",
        "/Images/MastersPersonalDate/PirsWorks/23.png",
        "/Images/MastersPersonalDate/PirsWorks/24.png",
        "/Images/MastersPersonalDate/PirsWorks/25.png",
        "/Images/MastersPersonalDate/PirsWorks/26.png",
    };

        private int currentImageIndex = 0;
        public Gallery()
        {
            InitializeComponent();
            currentCategory = "Tatoo";
            LoadGallery(imagePaths.Where(p => p.Contains("TatooWorks")).ToArray());
        }

        private void LoadGallery(string[] filteredPaths) // Обновлено для приема массива строк
        {
            GalleryGrid.Children.Clear(); // Очищаем галерею

            foreach (var path in filteredPaths)
            {
                var image = new Image
                {
                    Source = new BitmapImage(new Uri(path, UriKind.Relative)),
                    Stretch = Stretch.UniformToFill,
                    Width = 250,
                    Height = 250,
                    Margin = new Thickness(1),
                    Style = (Style)FindResource("PfotoContainer"),
                    Tag = path
                };

                image.MouseLeftButtonDown += (sender, e) => OpenPhotoViewWindow(path);
                GalleryGrid.Children.Add(image);
            }

            // Показываем кнопку "Добавить фото" только для админа
            AddImageButton.Visibility = _isAdmin ? Visibility.Visible : Visibility.Collapsed;
        }
        private void OpenPhotoViewWindow(string imagePath)
        {
            // Получаем индекс текущего изображения
            int currentIndex = Array.IndexOf(imagePaths, imagePath);

            // Открываем окно просмотра изображений
            PhotoViewWindow photoViewWindow = new PhotoViewWindow(imagePaths, currentIndex);
            photoViewWindow.ShowDialog(); // Используйте ShowDialog, чтобы открыть окно модально
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isAdmin)
            {
                MessageBox.Show("У вас нет прав на добавление изображений!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Выберите изображение"
            };

            if (openFileDialog.ShowDialog() == true && !string.IsNullOrEmpty(openFileDialog.FileName))
            {
                string selectedPath = openFileDialog.FileName;

                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string categoryPath = currentCategory == "Tatoo" ? "TatooWorks" : "PirsWorks";
                string targetDirectory = Path.Combine(appDirectory, $"Images/MastersPersonalDate/{categoryPath}");

                // Создаем целевую директорию, если она не существует
                if (!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                string targetPath = Path.Combine(targetDirectory, Path.GetFileName(selectedPath));
                string relativePath = $"/Images/MastersPersonalDate/{categoryPath}/{Path.GetFileName(selectedPath)}";

                try
                {
                    File.Copy(selectedPath, targetPath, true);

                    // Добавляем в массив изображений
                    imagePaths = imagePaths.Append(relativePath).ToArray();

                    // Фильтруем изображения по текущей категории
                    var filteredImages = imagePaths.Where(p => p.Contains($"/{categoryPath}/")).ToArray();

                    // Обновляем галерею с новыми изображениями
                    LoadGallery(filteredImages); // Передаем отфильтрованные изображения
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Файл не выбран.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void ArrowBut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new StartPage());
        }

        private void CloseButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Tatoo_Click(object sender, RoutedEventArgs e)
        {
            // Фильтруем только тату
            var filteredImages = imagePaths.Where(p => p.Contains("TatooWorks")).ToArray();
            LoadGallery(filteredImages);

            // Меняем стили кнопок
            Tatoo.Background = new SolidColorBrush(Color.FromRgb(135, 0, 0));
            Pirc.Background = new SolidColorBrush(Colors.Transparent);
            Tatoo.Foreground = new SolidColorBrush(Colors.White);
            Pirc.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void Pirc_Click(object sender, RoutedEventArgs e)
        {
            var filteredImages = imagePaths.Where(p => p.Contains("PirsWorks")).ToArray();
            LoadGallery(filteredImages);

            // Меняем стили кнопок
            Pirc.Background = new SolidColorBrush(Color.FromRgb(95, 0, 0));
            Tatoo.Background = new SolidColorBrush(Colors.Transparent);
            Pirc.Foreground = new SolidColorBrush(Colors.White);
            Tatoo.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
