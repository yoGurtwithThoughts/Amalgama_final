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
    /// Логика взаимодействия для TatooDetales.xaml
    /// </summary>
    public partial class TatooDetales : Page
    {
        private DispatcherTimer _textTimer1;
        private DispatcherTimer _textTimer2;
        private DispatcherTimer _textTimer3;
        private int _currentIndex1;
        private int _currentIndex2;
        private int _currentIndex3;
        private string[] _paragraphs1 = new string[]
        {
            "\tНаши татуировщики наносят не просто картинку, а создают настоящее произведение искусства, которое выражает внутреннее «Я» клиента.",
            "Мы выполняем рисунки различных стилей и сложности, одно- и многоцветные.",
            "Мастера хорошо разбираются во всех направлениях и владеют разными техниками нанесения художественного тату."
        };
        private string[] _paragraphs2 = new string[]
        {
            "\tОбеспечивается индивидуальный подход к каждому клиенту. " +
            "Заказчик может выбрать готовый эскиз из каталога " +
            "и мастер внесет в него изменения так, что получится эксклюзивная татуировка. " +
            "Выполняются работы по фотографии, эскизу, предоставленному клиентом, " +
            "а также разрабатываются уникальные рисунки в соответствии с пожеланием заказчика."
        };
        private string[] _paragraphs3 = new string[]
        {
            "\tСтоимость нанесения художественной татуировки зависит от сложности, " +
            "стиля, размера, детализации, плотности цветов и уровня мастера. " +
            "Также на итоговую цену влияет выбранный для нанесения татуировки участок тела"
        };
        private int _currentParagraphIndex1;
        private int _currentParagraphIndex2;
        private int _currentParagraphIndex3;

        public TatooDetales()
        {
            InitializeComponent();
            var storyboard = (Storyboard)StackGrid.Resources["GridSlideUpAnimation"];
            storyboard.Begin(StackGrid);

           
        }
        private void AnimateGrid()
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

            var transform = (TranslateTransform)AnimatedGrid.RenderTransform;
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
            AnimatedGrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _textTimer1 = new DispatcherTimer();
            _textTimer1.Interval = TimeSpan.FromMilliseconds(10);
            _textTimer1.Tick += TextTimer1_Tick;

            _textTimer2 = new DispatcherTimer();
            _textTimer2.Interval = TimeSpan.FromMilliseconds(10);
            _textTimer2.Tick += TextTimer2_Tick;

            _textTimer3 = new DispatcherTimer();
            _textTimer3.Interval = TimeSpan.FromMilliseconds(10);
            _textTimer3.Tick += TextTimer3_Tick;

            _textTimer1.Start();
            AnimateGrid();

            var imgFadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            var img1TranslateAnimation = new DoubleAnimation
            {
                From = -150,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            Storyboard.SetTarget(imgFadeInAnimation, ImgAn);
            Storyboard.SetTargetProperty(imgFadeInAnimation, new PropertyPath(Image.OpacityProperty));

            Storyboard.SetTarget(img1TranslateAnimation, ImgAn);
            Storyboard.SetTargetProperty(img1TranslateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            var imgStoryboard = new Storyboard();
            imgStoryboard.Children.Add(img1TranslateAnimation);
            imgStoryboard.Children.Add(imgFadeInAnimation);

            imgStoryboard.Begin();

            var imgFadeInAnimation1 = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            var img1TranslateAnimation1 = new DoubleAnimation
            {
                From = -150,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            Storyboard.SetTarget(imgFadeInAnimation1, ImgAn1);
            Storyboard.SetTargetProperty(imgFadeInAnimation1, new PropertyPath(Image.OpacityProperty));

            Storyboard.SetTarget(img1TranslateAnimation1, ImgAn1);
            Storyboard.SetTargetProperty(img1TranslateAnimation1, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            var imgStoryboard1 = new Storyboard();
            imgStoryboard1.Children.Add(img1TranslateAnimation1);
            imgStoryboard1.Children.Add(imgFadeInAnimation1);

            imgStoryboard1.Begin();
        }

        private void TextTimer1_Tick(object sender, EventArgs e)
        {
            if (_currentParagraphIndex1 < _paragraphs1.Length)
            {
                string currentParagraph = _paragraphs1[_currentParagraphIndex1];

                if (_currentIndex1 < currentParagraph.Length)
                {
                    WriteTxt.Text += currentParagraph[_currentIndex1];
                    _currentIndex1++;
                }
                else
                {
                    _currentIndex1 = 0;
                    _currentParagraphIndex1++;
                }
            }
            else
            {
                _textTimer1.Stop();
                _textTimer2.Start();
            }
        }

        private void TextTimer2_Tick(object sender, EventArgs e)
        {
            if (_currentParagraphIndex2 < _paragraphs2.Length)
            {
                string currentParagraph = _paragraphs2[_currentParagraphIndex2];

                if (_currentIndex2 < currentParagraph.Length)
                {
                    WriteTxt1.Text += currentParagraph[_currentIndex2];
                    _currentIndex2++;
                }
                else
                {
                    
                    _currentIndex2 = 0;
                    _currentParagraphIndex2++;
                }
            }
            else
            {
                _textTimer2.Stop();
                _textTimer3.Start(); 
            }
        }

        private void TextTimer3_Tick(object sender, EventArgs e)
        {
            if (_currentParagraphIndex3 < _paragraphs3.Length)
            {
                string currentParagraph = _paragraphs3[_currentParagraphIndex3];

                if (_currentIndex3 < currentParagraph.Length)
                {
                    WriteTxt2.Text += currentParagraph[_currentIndex3];
                    _currentIndex3++;
                }
                else
                {
                    
                    _currentIndex3 = 0;
                    _currentParagraphIndex3++;
                }
            }
            else
            {
                _textTimer3.Stop();
            }
        }
        private void AnimatePanel()
        { 
            Grid parentGrid = (Grid)this.FindName("StackGrid"); 
            TranslateTransform translateTransform = new TranslateTransform();
            parentGrid.RenderTransform = translateTransform;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = parentGrid.ActualHeight,
                To = 0, 
                Duration = TimeSpan.FromSeconds(1), 
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } 
            };

            translateTransform.BeginAnimation(TranslateTransform.YProperty, animation);
        }
    
        private void PricewWork_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PriceWork_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PriceA_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void PriceE_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Closes_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void RCButton_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new RecordPage());
        
         }

        private void ArrowButt_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new YslugiPage());
        }
    }
}
