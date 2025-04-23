using Amalgama.Servis;
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
using System.Windows.Threading;
using static Amalgama.Core.Navigation;
using Path = System.IO.Path;

namespace Amalgama.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MasterRemoveTatoo.xaml
    /// </summary>
    public partial class MasterRemoveTatoo : Page
    {
        private Button AddImageButton2;
        private bool _isAdmin = true;
        private DispatcherTimer _textTimer;
        private int _currentIndex;
        private int _currentParagraphIndex;
        private TextBlock[] _textBlocksTitle;
        private TextBlock[] _textBlocksTitle2;
        private TextBlock[] _textBlocks; // Массив для хранения всех TextBlock
        private (string Text, bool IsBold)[][] _paragraphs;
        private (string Text, bool IsBold)[][] _paragraphstitle;
        private (string Text, bool IsBold)[][] _paragraphstitle2;
        
        string[] imagePaths =
           {
        "/Images/MastersPersonalDate/RemoweWorks/1.png",
        "/Images/MastersPersonalDate/RemoweWorks/2.png",
        "/Images/MastersPersonalDate/RemoweWorks/3.png",
        "/Images/MastersPersonalDate/RemoweWorks/4.png",
        "/Images/MastersPersonalDate/RemoweWorks/5.png",
        "/Images/MastersPersonalDate/RemoweWorks/6.png",
        "/Images/MastersPersonalDate/RemoweWorks/7.png",
        "/Images/MastersPersonalDate/RemoweWorks/8.png",
        "/Images/MastersPersonalDate/RemoweWorks/9.png",
        "/Images/MastersPersonalDate/RemoweWorks/10.png",
        "/Images/MastersPersonalDate/RemoweWorks/11.png",
        "/Images/MastersPersonalDate/RemoweWorks/12.png"
    };

        public MasterRemoveTatoo()
        {
            InitializeComponent();
            InitializeTextBlocksAndParagraphs();
            StartTypingAnimation(0);
            AnimateButtonGrid();
            AnimateImage();
            CreateAddImageButton(); // Сначала создаем кнопку
            LoadGallery(); // Затем загружаем галерею

        }
        private void CreateAddImageButton()
        {
            AddImageButton2 = new Button
            {
                Content = "+",
                Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0xC1, 0xC1, 0xC1)),
                BorderBrush = new SolidColorBrush(System.Windows.Media.Colors.White),
                Foreground = new SolidColorBrush(System.Windows.Media.Colors.White),
                FontSize = 25,
                Height = 185,
                Width = 185,
                Margin = new Thickness(10),
                Visibility = Visibility.Collapsed,
                Tag = "AddImageButton"
            };
            AddImageButton2.Click += AddImageButton_Click;
        }

        private void InitializeTextBlocksAndParagraphs()
        {
            // Связываем TextBlock'и с их параграфами
            _textBlocks = new TextBlock[] { TxtWrite };
            _textBlocksTitle = new TextBlock[] { TxtWriteTitle };

            // Инициализируем заголовок
            _paragraphstitle = new (string Text, bool IsBold)[][]
            {
        new (string, bool)[]
        {
            ("\n\t\tУдаление тату", false)
        }
            };

            // Инициализируем заголовок "Плюсы"
            _paragraphstitle2 = new (string Text, bool IsBold)[][]
            {
        new (string, bool)[]
        {
            ("\n\t\tПлюсы", false)
        }
            };

            // Инициализируем основной текст
            _paragraphs = new (string Text, bool IsBold)[][]
            {
        new (string, bool)[]
        {
            ("\nМастер лазерного удаления татуировок, имеющий опыт более 5 лет." +
             "Специализируется на безопасном и эффективном удалении татуировок." +
             "Использует современные лазерные системы — на данный момент это самый эффективный способ очистить кожу от нежелательного рисунка." +
             "Успешно работает с клиентами, обеспечивая высокий уровень обслуживания и удовлетворенности.", false)
        }
            };

            // Применяем текст к элементам интерфейса
            ApplyTextToTextBlocks();
        }

        private void ApplyTextToTextBlocks()
        {
            // Применяем заголовок
            foreach (var paragraph in _paragraphstitle)
            {
                foreach (var (text, isBold) in paragraph)
                {
                    TxtWriteTitle.Inlines.Add(new Run(text)
                    {
                        FontWeight = isBold ? FontWeights.Bold : FontWeights.Normal
                    });
                }
            }

            // Применяем заголовок "Плюсы" в TxtPluses
            foreach (var paragraph in _paragraphstitle2)
            {
                foreach (var (text, isBold) in paragraph)
                {
                    TxtWriteTitle2.Inlines.Add(new Run(text)
                    {
                        FontWeight = isBold ? FontWeights.Bold : FontWeights.Normal
                    });
                }
            }

            // Применяем основной текст
            foreach (var paragraph in _paragraphs)
            {
                foreach (var (text, isBold) in paragraph)
                {
                    TxtWrite.Inlines.Add(new Run(text)
                    {
                        FontWeight = isBold ? FontWeights.Bold : FontWeights.Normal
                    });
                }
            }
        }
        private void AnimateImage()
        {
            // Создаем анимацию для изменения прозрачности (Opacity)
            var fadeAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new PowerEase { Power = 3 }
            };

            // Создаем анимацию для смещения по оси X
            var translateAnimation = new DoubleAnimation
            {
                From = -150,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            // Убеждаемся, что RenderTransform является TranslateTransform
            if (!(AnimatedBack.RenderTransform is TranslateTransform))
            {
                AnimatedBack.RenderTransform = new TranslateTransform();
            }

            // Применяем анимации к элементу AnimatedBack
            Storyboard.SetTarget(fadeAnimation, AnimatedBack);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(UIElement.OpacityProperty));

            Storyboard.SetTarget(translateAnimation, AnimatedBack);
            Storyboard.SetTargetProperty(translateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            // Если нужно также анимировать ImgAnimated, создаем отдельные анимации
            if (ImgAnimated != null)
            {
                if (!(ImgAnimated.RenderTransform is TranslateTransform))
                {
                    ImgAnimated.RenderTransform = new TranslateTransform();
                }

                var fadeAnimationForImg = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = TimeSpan.FromSeconds(1),
                    EasingFunction = new PowerEase { Power = 3 }
                };

                var translateAnimationForImg = new DoubleAnimation
                {
                    From = -150,
                    To = 0,
                    Duration = TimeSpan.FromSeconds(1),
                    EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
                };

                Storyboard.SetTarget(fadeAnimationForImg, ImgAnimated);
                Storyboard.SetTargetProperty(fadeAnimationForImg, new PropertyPath(UIElement.OpacityProperty));

                Storyboard.SetTarget(translateAnimationForImg, ImgAnimated);
                Storyboard.SetTargetProperty(translateAnimationForImg, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

                // Добавляем анимации для ImgAnimated в Storyboard
                var storyboard = new Storyboard();
                storyboard.Children.Add(fadeAnimation);
                storyboard.Children.Add(translateAnimation);
                storyboard.Children.Add(fadeAnimationForImg);
                storyboard.Children.Add(translateAnimationForImg);

                storyboard.Begin();
            }
            else
            {
                // Если ImgAnimated отсутствует, запускаем анимацию только для AnimatedBack
                var storyboard = new Storyboard();
                storyboard.Children.Add(fadeAnimation);
                storyboard.Children.Add(translateAnimation);
                storyboard.Begin();
            }
        }

        private void StartTypingAnimation(int index)
        {
            if (index >= _textBlocks.Length) return;

            _currentIndex = 0;
            _currentParagraphIndex = 0;
            _textBlocks[index].Inlines.Clear();

            _textTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };
            _textTimer.Tick += (sender, e) => TextTimer_Tick(sender, e, index);
            _textTimer.Start();
        }

        private void TextTimer_Tick(object sender, EventArgs e, int index)
        {
            if (_currentParagraphIndex < _paragraphs[index].Length)
            {
                var (text, isBold) = _paragraphs[index][_currentParagraphIndex];

                if (_currentIndex < text.Length)
                {
                    char currentChar = text[_currentIndex];
                    var run = new Run(currentChar.ToString())
                    {
                        FontWeight = isBold ? FontWeights.Bold : FontWeights.Normal
                    };
                    _textBlocks[index].Inlines.Add(run);
                    _currentIndex++;
                }
                else
                {
                    _currentIndex = 0;
                    _currentParagraphIndex++;
                    _textBlocks[index].Inlines.Add(new LineBreak()); // Перенос строки
                }
            }
            else
            {
                _textTimer.Stop();
                StartTypingAnimation(index + 1); // Переход к следующему TextBlock
            }
        }
        private void AnimateButtonGrid()
        {
            var translateAnimation = new DoubleAnimation
            {
                From = 50,
                To = 0,
                Duration = TimeSpan.FromSeconds(0.7),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };


            var opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.7),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };


            var transform = (TranslateTransform)AnimatedBRD.RenderTransform;
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
            AnimatedBRD.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }
        private void Closer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ArrowBut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new MastersPage());
        }

        private void RecButtom_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new RecordPage());
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
                if (GalleryGrid.Children.Contains(AddImageButton2))
                {
                    GalleryGrid.Children.Remove(AddImageButton2);
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
                AddImageButton2.Visibility = Visibility.Visible;

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
                if (GalleryGrid.Children.Contains(AddImageButton2))
                {
                    GalleryGrid.Children.Remove(AddImageButton2);
                }

                // Добавляем кнопку в рассчитанную позицию
                Grid.SetRow(AddImageButton2, lastRow);
                Grid.SetColumn(AddImageButton2, lastColumn);
                GalleryGrid.Children.Add(AddImageButton2);
            }
            else
            {
                AddImageButton2.Visibility = Visibility.Collapsed;

                // Удаляем кнопку, если она существует
                if (GalleryGrid.Children.Contains(AddImageButton2))
                {
                    GalleryGrid.Children.Remove(AddImageButton2);
                }
            }
        }
        private void OpenPhotoViewWindow(string imagePath)
        {
            int currentIndex = Array.IndexOf(imagePaths, imagePath);

            if (currentIndex == -1)
            {
                MessageBox.Show("Изображение не найдено в массиве.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            PhotoViewWindow photoViewWindow = new PhotoViewWindow(imagePaths, currentIndex);
            photoViewWindow.ShowDialog();
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
    }
}
