using Amalgama.Servis;
using com.sun.tools.javac.file;
using Microsoft.Win32;
using Nuke.Common.IO;
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
        private Button AddImageButton;
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
            CreateAddImageButton(); // Создаем кнопку "Добавить фото"
            LoadGallery(); // Загружаем галерею для текущей категории
        }

        private void CreateAddImageButton()
        {
            AddImageButton = new Button
            {
                Content = "+",
                Background = new SolidColorBrush(Color.FromRgb(0xC1, 0xC1, 0xC1)), // #C1C1C1
                BorderBrush = new SolidColorBrush(Colors.White),
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 25,
                Height = 185,
                Width = 185,
                Margin = new Thickness(10),
                Visibility = Visibility.Collapsed, // По умолчанию скрыта
                Tag = "AddImageButton" // Для идентификации
            };

            // Привязываем событие клика
            AddImageButton.Click += AddImageButton_Click;
        }

        private void LoadGallery()
        {
            int columns = 4;
            GalleryGrid.Columns = columns;
            GalleryGrid.Children.Clear(); // Очищаем галерею перед загрузкой

            if (imagePaths == null || imagePaths.Length == 0)
            {
                MessageBox.Show("Нет доступных изображений.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            foreach (var path in imagePaths)
            {
                var image = CreateGalleryImage(path);
                GalleryGrid.Children.Add(image);
            }

            UpdateAddImageButtonVisibility();
        }
        private Image CreateGalleryImage(string path)
        {
            var image = new Image
            {
                Source = new BitmapImage(new Uri(path, UriKind.Relative)),
                Stretch = Stretch.UniformToFill,
                Width = 250,
                Height = 250,
                Margin = new Thickness(2),
                Style = (Style)FindResource("PfotoContainer"),
                Tag = path
            };

            image.MouseLeftButtonDown += (sender, e) => OpenPhotoViewWindow(path);

            if (IsUserAdminOrSuperAdmin())
            {
                var contextMenu = new ContextMenu();
                var deleteMenuItem = new MenuItem { Header = "Удалить изображение" };
                deleteMenuItem.Click += (sender, e) => DeleteImage(path);
                contextMenu.Items.Add(deleteMenuItem);
                image.ContextMenu = contextMenu;
            }

            return image;
        }
        private void DeleteImage(string imagePath)
        {
            if (!IsUserAdminOrSuperAdmin())
            {
                MessageBox.Show("У вас нет прав на удаление изображений!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                string appDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string fullPath = System.IO.Path.Combine(appDirectory, imagePath.TrimStart('/').Replace('/', '\\'));

                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    imagePaths = imagePaths.Where(p => p != imagePath).ToArray();
                    LoadGallery();
                    MessageBox.Show("Изображение успешно удалено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Файл не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении изображения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool IsUserAdminOrSuperAdmin()
        {
            return SessionManager.IsAdministrator || SessionManager.IsSuperAdmin;
        }

        private bool IsUserSuperAdmin()
        {
            return SessionManager.IsSuperAdmin;
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            if (!IsUserSuperAdmin())
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

                // Создаем временную ссылку на изображение
                var bitmapImage = new BitmapImage(new Uri(selectedPath, UriKind.Absolute));

                // Создаем новое изображение для галереи
                var image = new Image
                {
                    Source = bitmapImage,
                    Stretch = Stretch.UniformToFill,
                    Width = 250,
                    Height = 250,
                    Margin = new Thickness(2),
                    Style = (Style)FindResource("PfotoContainer"),
                    Tag = selectedPath // Сохраняем полный путь для контекстного меню
                };

                image.MouseLeftButtonDown += (sender, e) => OpenPhotoViewWindow(selectedPath);

                if (IsUserAdminOrSuperAdmin())
                {
                    var contextMenu = new ContextMenu();
                    var deleteMenuItem = new MenuItem { Header = "Удалить изображение" };
                    deleteMenuItem.Click += (sender, e) => DeleteImageFromGallery(image); // Удаляем только из галереи
                    contextMenu.Items.Add(deleteMenuItem);
                    image.ContextMenu = contextMenu;
                }

                // Удаляем кнопку из галереи
                if (GalleryGrid.Children.Contains(AddImageButton))
                {
                    GalleryGrid.Children.Remove(AddImageButton);
                }

                // Добавляем новое изображение
                GalleryGrid.Children.Add(image);

                // Обновляем видимость кнопки добавления
                UpdateAddImageButtonVisibility();
            }
            else
            {
                MessageBox.Show("Файл не выбран.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void DeleteImageFromGallery(Image image)
        {
            if (!IsUserAdminOrSuperAdmin())
            {
                MessageBox.Show("У вас нет прав на удаление изображений!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (GalleryGrid.Children.Contains(image))
            {
                GalleryGrid.Children.Remove(image);
                MessageBox.Show("Изображение успешно удалено.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // Обновляем видимость кнопки добавления
            UpdateAddImageButtonVisibility();
        }
        private void UpdateAddImageButtonVisibility()
        {
            if (IsUserSuperAdmin())
            {
                AddImageButton.Visibility = Visibility.Visible;

                int totalImages = GalleryGrid.Children.Count; // Количество элементов в галерее
                int columns = 4;

                // Вычисляем строку и столбец для кнопки
                int lastRow = (int)Math.Ceiling((double)(totalImages + 1) / columns); // +1 для учета кнопки
                int lastColumn = totalImages % columns; // Остаток от деления определяет столбец

                if (lastColumn == 0)
                {
                    lastRow--;
                    lastColumn = columns - 1;
                }
                else
                {
                    lastColumn--; // Индексация столбцов начинается с 0
                }

                // Удаляем кнопку, если она уже существует
                if (GalleryGrid.Children.Contains(AddImageButton))
                {
                    GalleryGrid.Children.Remove(AddImageButton);
                }

                // Добавляем кнопку в рассчитанную позицию
                Grid.SetRow(AddImageButton, lastRow);
                Grid.SetColumn(AddImageButton, lastColumn);
                GalleryGrid.Children.Add(AddImageButton);
            }
            else
            {
                AddImageButton.Visibility = Visibility.Collapsed;

                // Удаляем кнопку, если она существует
                if (GalleryGrid.Children.Contains(AddImageButton))
                {
                    GalleryGrid.Children.Remove(AddImageButton);
                }
            }
        }

        private void OpenPhotoViewWindow(string imagePath)
        {
            int currentIndex = Array.IndexOf(imagePaths, imagePath);
            PhotoViewWindow photoViewWindow = new PhotoViewWindow(imagePaths, currentIndex);
            photoViewWindow.ShowDialog();
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
            currentCategory = "Tatoo";
            LoadGallery();

            UpdateButtonStyles(Tatoo, Pirc, Color.FromRgb(135, 0, 0));
        }

        private void Pirc_Click(object sender, RoutedEventArgs e)
        {
            currentCategory = "Pirs";
            LoadGallery();

            UpdateButtonStyles(Pirc, Tatoo, Color.FromRgb(95, 0, 0));
        }

        private void UpdateButtonStyles(Button activeButton, Button inactiveButton, Color activeColor)
        {
            activeButton.Background = new SolidColorBrush(activeColor);
            inactiveButton.Background = new SolidColorBrush(Colors.Transparent);
            activeButton.Foreground = new SolidColorBrush(Colors.White);
            inactiveButton.Foreground = new SolidColorBrush(Colors.Black);
        }
    }
}
