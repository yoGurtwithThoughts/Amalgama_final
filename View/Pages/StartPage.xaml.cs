
using Amalgama.View.AdminPages;
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
    /// Логика взаимодействия для StartPage.xaml
    /// </summary>
    public partial class StartPage : Page
    {
        private DispatcherTimer _timer;
        private int _contentIndex;
        private List<Ellipse> _dots;
        private List<UIElement> _contentItems;
        private string[] _messages = { "1", "2", "3", "4" };
        private bool isMenuOpen = false;


        public StartPage()
        {
            InitializeComponent();
            InitializeDots();
            InitializeTimer();
            InitializeContentItems();
            TextBlock textBlock = new TextBlock();
        }
        private void InitializeDots()
        {
            _dots = new List<Ellipse>();
            DotsPanel.Children.Clear();  

            int totalDots = 5; 

            // Создаем точки
            for (int i = 0; i < totalDots; i++)
            {
                Ellipse dot = new Ellipse
                {
                    Width = 10,
                    Height = 10,
                    Stroke = (Brush)new BrushConverter().ConvertFrom("#A6A6A6"),
                    StrokeThickness = 2,  
                    Margin = new Thickness(5) 
                };

                _dots.Add(dot); 
                DotsPanel.Children.Add(dot); 
            }

           
            if (_dots.Count > 0)
            {
                _dots[0].Fill = (Brush)new BrushConverter().ConvertFrom("#840000"); 
            }
        }

        private void UpdateDots()
        {
            for (int i = 0; i < _dots.Count; i++)
            {
               
                _dots[i].Stroke = (Brush)new BrushConverter().ConvertFrom("#A6A6A6"); 

               
                if (i == _contentIndex)
                {
                    _dots[i].Fill = (Brush)new BrushConverter().ConvertFrom("#840000"); 
                }
                else
                {
                    _dots[i].Fill = Brushes.Transparent; 
                }
            }
        }


        private void StartTimer()
            {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2); 
            _timer.Tick += UpdateText;
            _timer.Start();
          

        }
        
        private void UpdateText(object? sender, EventArgs e)
        {
            ContentTxt.Text = _messages[_contentIndex];
            _contentIndex = (_contentIndex + 1) % _messages.Length;
            UpdateDots();
        }


        private void InitializeContentItems()
        {
            Style titleMainStyle = (Style)FindResource("SliderBar");

            _contentItems = new List<UIElement> {
            new TextBlock { TextWrapping = TextWrapping.Wrap,
            Style = titleMainStyle,
            Inlines =
            {
                new Run("Когда стоит делать тату?") { FontWeight = FontWeights.Bold },
                new LineBreak(),
                new Run("\nТатуировщики рекомендуют делать татуировки, а также пирсинг, не в жаркое время года, т. е. осенью, зимой, весной!")
            }
        },
      new TextBlock { TextWrapping = TextWrapping.Wrap,
            Style = titleMainStyle,
            Inlines =
            {
                new Run("После сеанса") { FontWeight = FontWeights.Bold },
                new LineBreak(),
                new Run("\nПосле сеанса не стоит планировать дел, так как процесс нанесения тату " +
                "- скорее всего организм будет испытывать стресс и вам нужно будет восстановиться")
            }
        },
        new TextBlock { TextWrapping = TextWrapping.Wrap,
            Style = titleMainStyle,
            Inlines =
            {
                new Run("Подготовка к сеансу") { FontWeight = FontWeights.Bold },
                new LineBreak(),
                new Run("\nНе стоит идти к мастеру уставшим, обезательно хорошо выспитесь перед сеансом.")
            }
        },
         new TextBlock { TextWrapping = TextWrapping.Wrap,
            Style = titleMainStyle,
            Inlines =
            {
                new Run("Что можно, а что нельзя есть?") { FontWeight = FontWeights.Bold },
                new LineBreak(),
                new Run("\nПеред сеансом обязательно плотно покушать, можно взять что то сладкое на перекус; перед сеансом категорически " +
                "запрещается принимать алкогольные напитки и кофе в больших количествах.")
            }
        },
          new TextBlock { TextWrapping = TextWrapping.Wrap,
            Style = titleMainStyle,
            Inlines =
            {
                new Run("Самое главное!") { FontWeight = FontWeights.Bold },
                new LineBreak(),
                new Run("\nСлушать и соблюдать все указания мастера, ведь для него заживление вашей татуировки так же важно.")
            }
        },
            };

        }  

        private void InitializeTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(7.5); 
            _timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _contentIndex = (_contentIndex + 1) % _dots.Count;
            Dispatcher.BeginInvoke(new Action(() => UpdateDots()));
            _contentIndex = (_contentIndex + 1) % _contentItems.Count;
            Contentstack.Children.Clear();
            Contentstack.Children.Add(_contentItems[_contentIndex]);
            _contentIndex = (_contentIndex + 1) % _contentItems.Count;
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _contentIndex = 0;
            Contentstack.Children.Clear();
            Contentstack.Children.Add(_contentItems[_contentIndex]);
            _timer.Start();
            var textBlockFadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            var textBlockTranslateAnimation = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            Storyboard.SetTarget(textBlockFadeInAnimation, AnimatedBlock);
            Storyboard.SetTargetProperty(textBlockFadeInAnimation, new PropertyPath(TextBlock.OpacityProperty));

            Storyboard.SetTarget(textBlockTranslateAnimation, AnimatedBlock);
            Storyboard.SetTargetProperty(textBlockTranslateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            var textBlockStoryboard = new Storyboard();
            textBlockStoryboard.Children.Add(textBlockFadeInAnimation);
            textBlockStoryboard.Children.Add(textBlockTranslateAnimation);

            var textBlockFadeInAnimation1 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            var textBlockTranslateAnimation1 = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            Storyboard.SetTarget(textBlockFadeInAnimation1, Contentstack);
            Storyboard.SetTargetProperty(textBlockFadeInAnimation1, new PropertyPath(TextBlock.OpacityProperty));

            Storyboard.SetTarget(textBlockTranslateAnimation1, Contentstack);
            Storyboard.SetTargetProperty(textBlockTranslateAnimation1, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
            var textBlockFadeInAnimation2 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            var textBlockTranslateAnimation2 = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            Storyboard.SetTarget(textBlockFadeInAnimation2, Contentstack);
            Storyboard.SetTargetProperty(textBlockFadeInAnimation2, new PropertyPath(TextBlock.OpacityProperty));

            Storyboard.SetTarget(textBlockTranslateAnimation2, Contentstack);
            Storyboard.SetTargetProperty(textBlockTranslateAnimation1, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
            var textBlockStoryboard1 = new Storyboard();
            textBlockStoryboard.Children.Add(textBlockFadeInAnimation1);
            textBlockStoryboard.Children.Add(textBlockTranslateAnimation1);

            var backgroundFadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                BeginTime = TimeSpan.FromSeconds(1)
            };

            var backgroundTranslateAnimation = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                BeginTime = TimeSpan.FromSeconds(1)
            };

            Storyboard.SetTarget(backgroundFadeInAnimation, Animated_Background);
            Storyboard.SetTargetProperty(backgroundFadeInAnimation, new PropertyPath(Image.OpacityProperty));

            Storyboard.SetTarget(backgroundTranslateAnimation, Animated_Background);
            Storyboard.SetTargetProperty(backgroundTranslateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));

            var backgroundStoryboard = new Storyboard();
            backgroundStoryboard.Children.Add(backgroundFadeInAnimation);
            backgroundStoryboard.Children.Add(backgroundTranslateAnimation);

            var bannerFadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                BeginTime = TimeSpan.FromSeconds(2)
            };

            var bannerTranslateAnimation = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                BeginTime = TimeSpan.FromSeconds(2)
            };

            Storyboard.SetTarget(bannerFadeInAnimation, animated_Banner);
            Storyboard.SetTargetProperty(bannerFadeInAnimation, new PropertyPath(Image.OpacityProperty));

            Storyboard.SetTarget(bannerTranslateAnimation, animated_Banner);
            Storyboard.SetTargetProperty(bannerTranslateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));

            var bannerStoryboard = new Storyboard();
            bannerStoryboard.Children.Add(bannerFadeInAnimation);
            bannerStoryboard.Children.Add(bannerTranslateAnimation);

            var textBlockTranslateAnimation3 = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            Storyboard.SetTarget(textBlockFadeInAnimation2, Other);
            Storyboard.SetTargetProperty(textBlockFadeInAnimation2, new PropertyPath(TextBlock.OpacityProperty));

            Storyboard.SetTarget(textBlockTranslateAnimation2, Other);
            Storyboard.SetTargetProperty(textBlockTranslateAnimation2, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
            var textBlockStoryboard3 = new Storyboard();
            textBlockStoryboard.Children.Add(textBlockFadeInAnimation2);
            textBlockStoryboard.Children.Add(textBlockTranslateAnimation2);
            var moreButtonFadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                BeginTime = TimeSpan.FromSeconds(3)
            };

            var moreButtonTranslateAnimation = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                BeginTime = TimeSpan.FromSeconds(3)
            };

            Storyboard.SetTarget(moreButtonFadeInAnimation, moreButton);
            Storyboard.SetTargetProperty(moreButtonFadeInAnimation, new PropertyPath(TextBlock.OpacityProperty));

            Storyboard.SetTarget(moreButtonTranslateAnimation, moreButton);
            Storyboard.SetTargetProperty(moreButtonTranslateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            var moreButtonStoryboard = new Storyboard();
            moreButtonStoryboard.Children.Add(moreButtonFadeInAnimation);
            moreButtonStoryboard.Children.Add(moreButtonTranslateAnimation);
            


            var borderFadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                BeginTime = TimeSpan.FromSeconds(3)
            };

           
            var borderTranslateAnimation = new DoubleAnimation
            {
                From = -50,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                BeginTime = TimeSpan.FromSeconds(3)
            };

            
            Storyboard.SetTarget(borderFadeInAnimation, Animated_Service);
            Storyboard.SetTargetProperty(borderFadeInAnimation, new PropertyPath(Border.OpacityProperty));

            
            Storyboard.SetTarget(borderTranslateAnimation, Animated_Service);
            Storyboard.SetTargetProperty(borderTranslateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));

            var borderStoryboard = new Storyboard();
            borderStoryboard.Children.Add(borderFadeInAnimation);
            borderStoryboard.Children.Add(borderTranslateAnimation);
            //strart animation
            textBlockStoryboard.Begin(this);
            backgroundStoryboard.Begin(this);
            bannerStoryboard.Begin(this);
            moreButtonStoryboard.Begin(this);
            backgroundStoryboard.Begin(this);
            borderStoryboard.Begin(this);


        }

        private void SignButton_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new RecordPage());
        }

        private void VK_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void TG_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Service_button_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new YslugiPage());
        }

        private void SButton_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new MastersPage());
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
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
        private void Gallery_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStyles(Gallery, Masters, QW, Record);
            CoreNavigate.NavigatorCore.Navigate(new Gallery());
        }

        private void Masters_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStyles(Masters, Gallery, QW, Record);
            CoreNavigate.NavigatorCore.Navigate(new MastersPage());
        }

        private void QW_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStyles(QW, Record, Masters, Gallery);
            CoreNavigate.NavigatorCore.Navigate(new QwestionsPage());
        }

        private void Record_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStyles(Record, QW, Masters, Gallery);
            CoreNavigate.NavigatorCore.Navigate(new RecordPage());
        }

        private void SetButtonStyles(Button activeButton, Button button2, Button button3, Button button4)
        {
            // Устанавливаем стиль для активной кнопки
            activeButton.Style = (Style)FindResource("ButtonSt1");

            // Устанавливаем стиль LightButton для неактивных кнопок
            button2.Style = (Style)FindResource("LightButton");
            button3.Style = (Style)FindResource("LightButton");
            button4.Style = (Style)FindResource("LightButton");
        }

        private void SignAdmin_Click(object sender, RoutedEventArgs e)
        {
            AdminSigIn adminSignInWindow = new AdminSigIn();

            adminSignInWindow.ShowDialog();


        }
    }
}
