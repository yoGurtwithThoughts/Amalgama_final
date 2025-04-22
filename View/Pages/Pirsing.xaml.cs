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

namespace Amalgama.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для Pirsing.xaml
    /// </summary>
    public partial class Pirsing : Page
    {
        private DispatcherTimer _textTimer1;
        private DispatcherTimer _textTimer2;
        private DispatcherTimer _textTimer3;
        private DispatcherTimer _animationTimer;
        private bool _isAnimationStarted = false;

        private int _currentIndex1;
        private int _currentIndex2;
        private int _currentIndex3;

        private Button _activeButton;
        private bool _isAnimationRunning = false;

        private TextBlock[] _paragraphs3 = new TextBlock[]
        {
            new TextBlock
        {
        Inlines =
        {
            new Run("Что нельзя делать до пирсинга?") { FontWeight = FontWeights.Bold },
            new LineBreak(),
            new Run("\nЗа два дня до пирсинга нужно исключить алкоголь (алкоголь в крови может привести к сильному кровотечению)."),
            new LineBreak(),
            new Run("\nНе принимайте никаких лекарств, ухудшающих свертываемость крови (Аспирин, Ибупрофен, Напроксен, Витамин Е и т.д.)."),
            new LineBreak(),
            new Run("\nПеред визитом за 1-2 часа нужно поесть, чтобы уровень сахара в крови был стабильный и не было головокружения."),
            new LineBreak(),
            new Run("\nНе будет лишним хорошо отдохнуть перед пирсингом, выспаться, настроить себя на позитивный лад. От настроя и самочувствия в том числе зависит насколько легко пройдет процедура."),
            new LineBreak(),
            new Run("\nЗаранее подумайте о том, что вы наденете, если вам предстоит раздеваться; одевайтесь удобно и практично. Не надевайте одежду и аксессуары, которые будут тереться об украшение.")
        },
        TextWrapping = TextWrapping.Wrap
        }
        };
        private string[] _paragraphs1 = new string[]
       {
            "\n\n Пирсинг – это способ механической модификации тела человека, " +
           "процесс которого состоит непосредственно из " +
           "самого прокола и вставки украшения в это место."
       };
        private string[] _paragraphs2 = new string[]
       {    "Виды пирсинга:"
       };
        private int _currentParagraphIndex1;
        private int _currentParagraphIndex2;
        private int _currentParagraphIndex3;
        public Pirsing()
        {
            InitializeComponent();

            AnimatePanel();
            AnimateImage();
            AnimateButtonGrid();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            _animationTimer.Stop(); 

            if (!_isAnimationStarted) 
            {
                _isAnimationStarted = true; 
                AnimatePanel(); 
            }
        }
        private void AnimateImage()
        {
           
            var translateAnimation = new DoubleAnimation
            {
                From = -200, 
                To = 0,      
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut }
            };

            var opacityAnimation = new DoubleAnimation
            {
                From = 0, 
                To = 1,   
                Duration = TimeSpan.FromSeconds(0.7),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };
            var transform = (TranslateTransform)AnimatedImage.RenderTransform;
            transform.BeginAnimation(TranslateTransform.XProperty, translateAnimation);
            AnimatedImage.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }
        private void AnimatePanel()
        {
            if (_isAnimationRunning) 
                return;

            _isAnimationRunning = true;

            TranslateTransform translateTransform = new TranslateTransform();
            StackAnButton.RenderTransform = translateTransform;

            DoubleAnimation animation = new DoubleAnimation
            {
                From = -1000,
                To = 0,
                Duration = TimeSpan.FromSeconds(3), 
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseOut } 
            };

            
            animation.Completed += (s, e) =>
            {
                _isAnimationRunning = false; 
            };

            translateTransform.BeginAnimation(TranslateTransform.XProperty, animation);
        }
        private void Closer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _animationTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2) 
            };
            _animationTimer.Tick += AnimationTimer_Tick;
            _animationTimer.Start();
       

            _textTimer1 = new DispatcherTimer();
            _textTimer1.Interval = TimeSpan.FromMilliseconds(10);
            _textTimer1.Tick += TextTimer1_Tick;

            _textTimer2 = new DispatcherTimer();
            _textTimer2.Interval = TimeSpan.FromMilliseconds(10);
            _textTimer2.Tick += TextTimer2_Tick;

            _textTimer1.Start();
            _textTimer3 = new DispatcherTimer();
            _textTimer3.Interval = TimeSpan.FromMilliseconds(10);
            _textTimer3.Tick += TextTimer3_Tick;

        }
        private void TextTimer2_Tick(object sender, EventArgs e)
        {
            if (_currentParagraphIndex2 < _paragraphs2.Length)
            {
                string currentParagraph = _paragraphs2[_currentParagraphIndex2];

                if (_currentIndex2 < currentParagraph.Length)
                {
                    TxtWrite1.Text += currentParagraph[_currentIndex2];
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

                if (_textTimer3 != null && !_textTimer3.IsEnabled)
                {
                    _textTimer3.Start();
                }
            }
        }

        private void TextTimer3_Tick(object sender, EventArgs e)
        {
            if (_currentParagraphIndex3 < _paragraphs3.Length)
            {
                TextBlock currentParagraph = _paragraphs3[_currentParagraphIndex3];

             
                if (_currentIndex3 < currentParagraph.Inlines.Count)
                {
                    Inline inline = currentParagraph.Inlines.ElementAt(_currentIndex3);

                    TxtWriteTitle.Inlines.Add(inline);
                    _currentIndex3++;
                }
                else
                {
                    _currentIndex3 = 0;
                    _currentParagraphIndex3++;
                    TxtWriteTitle.Inlines.Add(new LineBreak());
                }
            }
            else
            {
                _textTimer3.Stop();
            }
        }

        private void TextTimer1_Tick(object sender, EventArgs e)
        {
            if (_currentParagraphIndex1 < _paragraphs1.Length)
            {
                string currentParagraph = _paragraphs1[_currentParagraphIndex1];

                if (_currentIndex1 < currentParagraph.Length)
                {
                    TxtWrite.Text += currentParagraph[_currentIndex1];
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

        private void Pirc_Nose_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStyles(Pirc_Nose, Pirc_Lips, EarsEyeBrow_Lips);
            CoreNavigate.NavigatorCore.Navigate(new NoseGalleryPirs());
        }

        private void Pirc_Lips_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStyles(Pirc_Lips, Pirc_Nose, EarsEyeBrow_Lips);
            CoreNavigate.NavigatorCore.Navigate(new LipsGalleryPirs());
        }

        private void EarsEyeBrow_Lips_Click(object sender, RoutedEventArgs e)
        {
            SetButtonStyles(EarsEyeBrow_Lips, Pirc_Nose, Pirc_Lips);
            CoreNavigate.NavigatorCore.Navigate(new EyebrowGalleryPirs());
        }
        private void SetButtonStyles(Button activeButton, Button button2, Button button3)
        {
            
            activeButton.Background = new SolidColorBrush(Color.FromRgb(95, 0, 0));
            activeButton.Foreground = new SolidColorBrush(Colors.White);

            button2.Background = new SolidColorBrush(Colors.Transparent);
            button2.Foreground = new SolidColorBrush(Colors.Black);

            button3.Background = new SolidColorBrush(Colors.Transparent);
            button3.Foreground = new SolidColorBrush(Colors.Black);
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

         
            var transform = (TranslateTransform)AnimatedGrid.RenderTransform;
            transform.BeginAnimation(TranslateTransform.YProperty, translateAnimation);
            AnimatedGrid.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
        }

        private void RCButtons_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new RecordPage());
        }

        private void ArrowBut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new YslugiPage());
        }
    }
}

