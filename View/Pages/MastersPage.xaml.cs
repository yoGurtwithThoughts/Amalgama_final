using Amalgama.View;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Amalgama.Core.Navigation;

namespace Amalgama.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MastersPage.xaml
    /// </summary>
    public partial class MastersPage : Page
    {
        public MastersPage()
        {
            InitializeComponent();
            Loaded += Page_Loaded;
            MyGrid.Loaded += (sender, args) =>
            {
                DoubleAnimation fadeInAnimation = new DoubleAnimation
                {
                    From = 0, // Начальная прозрачность
                    To = 1,   // Конечная прозрачность
                    Duration = TimeSpan.FromSeconds(1) // Длительность анимации (1 секунда)
                };

                // Запуск анимации
                MyGrid.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
            };
        
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100) // Задержка 100 миллисекунд
            };

            timer.Tick += (s, args) =>
            {
                timer.Stop(); // Останавливаем таймер после первого срабатывания

                // Показываем и анимируем первое изображение
                MasterTatoo.Visibility = Visibility.Visible;
                AnimateImage(MasterTatoo, -750, 0);

                // Показываем и анимируем второе изображение
                MasterDeleteTatoo.Visibility = Visibility.Visible;
                AnimateImage(MasterDeleteTatoo, -750, 0);

                // Показываем и анимируем третье изображение
                MasterPirc.Visibility = Visibility.Visible;
                AnimateImage(MasterPirc, -750, 0);
            };

            timer.Start(); // Запускаем таймер
        
        DoubleAnimation imageSlideIn = new DoubleAnimation
            {
                From = -350,
                To = 0,
                Duration = TimeSpan.FromSeconds(1.5),
                BeginTime = TimeSpan.FromSeconds(0.5),
                EasingFunction = new ElasticEase { Oscillations = 1, Springiness = 3 }
        };
            BackgroundImage.RenderTransform.BeginAnimation(TranslateTransform.YProperty, imageSlideIn);

            // Анимация прозрачности изображения
            DoubleAnimation imageFadeIn = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(1.5),
                BeginTime = TimeSpan.FromSeconds(0.5)
            };
            BackgroundImage.BeginAnimation(UIElement.OpacityProperty, imageFadeIn);

            // Анимация перемещения текста
            DoubleAnimation textSlideIn = new DoubleAnimation
            {
                From = -75,
                To = 0,
                Duration = TimeSpan.FromSeconds(1.5),
                BeginTime = TimeSpan.FromSeconds(1.0),
                EasingFunction = new ElasticEase { EasingMode = EasingMode.EaseOut, Oscillations = 2, Springiness = 3 }
            };
            HeaderText.RenderTransform.BeginAnimation(TranslateTransform.YProperty, textSlideIn);

            // Анимация прозрачности текста
            DoubleAnimation textFadeIn = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(1.5),
                BeginTime = TimeSpan.FromSeconds(1.0)
            };
            HeaderText.BeginAnimation(UIElement.OpacityProperty, textFadeIn);
            DoubleAnimation textSlideIn1 = new DoubleAnimation
            {
                From = -75,
                To = 0,
                Duration = TimeSpan.FromSeconds(1.5),
                BeginTime = TimeSpan.FromSeconds(1.0),
                EasingFunction = new ElasticEase { EasingMode = EasingMode.EaseOut, Oscillations = 2, Springiness = 3 }
            };
            Txt1_Line1.RenderTransform.BeginAnimation(TranslateTransform.YProperty, textSlideIn1);

            // Анимация прозрачности текста
            DoubleAnimation textFadeIn1 = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(1.5),
                BeginTime = TimeSpan.FromSeconds(1.0)
            };
            Txt1_Line1.BeginAnimation(UIElement.OpacityProperty, textFadeIn1);
            DoubleAnimation textSlideIn2 = new DoubleAnimation
            {
                From = -75,
                To = 0,
                Duration = TimeSpan.FromSeconds(1.5),
                BeginTime = TimeSpan.FromSeconds(1.0),
                EasingFunction = new ElasticEase { EasingMode = EasingMode.EaseOut, Oscillations = 2, Springiness = 3 }
            };
            Txt1_Line2.RenderTransform.BeginAnimation(TranslateTransform.YProperty, textSlideIn1);

            // Анимация прозрачности текста
            DoubleAnimation textFadeIn2 = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(1.5),
                BeginTime = TimeSpan.FromSeconds(1.0)
            };
            Txt1_Line2.BeginAnimation(UIElement.OpacityProperty, textFadeIn1);

            //MasterTatoo.Visibility = Visibility.Visible;
            //AnimateImage(MasterTatoo, -750, 0);

            //MasterDeleteTatoo.Visibility = Visibility.Visible;
            //AnimateImage(MasterDeleteTatoo, -750, 0);

            //MasterPirc.Visibility = Visibility.Visible;
            //AnimateImage(MasterPirc, -750, 0);
        }

        private void AnimateImage(Image image, double fromY, double toY)
        {
            if (image.RenderTransform is TransformGroup transformGroup)
            {
                var translateTransform = transformGroup.Children.OfType<TranslateTransform>().FirstOrDefault();
                if (translateTransform != null)
                {
                    // Анимация перемещения
                    DoubleAnimation slideIn = new DoubleAnimation
                    {
                        From = fromY,
                        To = toY,
                        Duration = TimeSpan.FromSeconds(1.5),
                        BeginTime = TimeSpan.FromSeconds(0.5),
                        EasingFunction = new ElasticEase { Oscillations = 1, Springiness = 3 }
                    };
                    translateTransform.BeginAnimation(TranslateTransform.YProperty, slideIn);

                    // Анимация обрезки (clip)
                    var clipGeometry = new RectangleGeometry { Rect = new Rect(0, 0, image.ActualWidth, 0) };
                    image.Clip = clipGeometry;

                    // Создаем RectAnimation для анимации Rect
                    RectAnimation clipAnimation = new RectAnimation
                    {
                        From = new Rect(0, 0, image.ActualWidth, 0), // Начальная высота равна 0
                        To = new Rect(0, 0, image.ActualWidth, image.ActualHeight), // Конечная высота равна высоте изображения
                        Duration = TimeSpan.FromSeconds(1.5),
                        BeginTime = TimeSpan.FromSeconds(0.5),
                         EasingFunction = new ElasticEase { Oscillations = 1, Springiness = 3 }
                    };
                    clipGeometry.BeginAnimation(RectangleGeometry.RectProperty, clipAnimation);
                }
            }
            else
            {
                var translateTransform = new TranslateTransform { Y = fromY };
                image.RenderTransform = translateTransform;

                // Анимация перемещения
                DoubleAnimation slideIn = new DoubleAnimation
                {
                    From = fromY,
                    To = toY,
                    Duration = TimeSpan.FromSeconds(1.5),
                    BeginTime = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new CircleEase { EasingMode = EasingMode.EaseInOut }
                };
                translateTransform.BeginAnimation(TranslateTransform.YProperty, slideIn);

                // Анимация обрезки (clip)
                var clipGeometry = new RectangleGeometry { Rect = new Rect(0, 0, image.ActualWidth, 0) };
                image.Clip = clipGeometry;

                // Создаем RectAnimation для анимации Rect
                RectAnimation clipAnimation = new RectAnimation
                {
                    From = new Rect(0, 0, image.ActualWidth, 0), // Начальная высота равна 0
                    To = new Rect(0, 0, image.ActualWidth, image.ActualHeight), // Конечная высота равна высоте изображения
                    Duration = TimeSpan.FromSeconds(1.5),
                    BeginTime = TimeSpan.FromSeconds(0.5)
                };
                clipGeometry.BeginAnimation(RectangleGeometry.RectProperty, clipAnimation);
            }
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is Image image && image.RenderTransform is TransformGroup transformGroup)
            {
                
                var scaleTransform = transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault();
                if (scaleTransform != null)
                {
                    DoubleAnimation scaleUp = new DoubleAnimation
                    {
                        From = 1.0,
                        To = 1.2,
                        Duration = TimeSpan.FromSeconds(0.3),
                        EasingFunction = new ElasticEase { EasingMode = EasingMode.EaseOut, Oscillations = 1, Springiness = 5 }
                    };
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleUp);
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleUp);
                }
            }
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is Image image && image.RenderTransform is TransformGroup transformGroup)
            {
                var scaleTransform = transformGroup.Children.OfType<ScaleTransform>().FirstOrDefault();
                if (scaleTransform != null)
                {
                    DoubleAnimation scaleDown = new DoubleAnimation
                    {
                        From = 1.2,
                        To = 1.0,
                        Duration = TimeSpan.FromSeconds(0.3),
                        EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                    };
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleDown);
                    scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleDown);
                }
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MasterDeleteTatoo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new MasterRemoveTatoo());
        }

        private void MasterTatoo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new MasterTatoo());
        }

        private void MasterPirc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new MasterPirs());
        }

        private void ArrowButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new StartPage());
        }
    }
}
