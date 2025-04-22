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
    /// Логика взаимодействия для TatooRemoval.xaml
    /// </summary>
    public partial class TatooRemoval : Page
    {
        private DispatcherTimer _textTimer;
        private int _currentIndex;
        private int _currentParagraphIndex;
        private TextBlock[] _textBlocks; // Массив для хранения всех TextBlock
        private (string Text, bool IsBold)[][] _paragraphs;

        public TatooRemoval()
        {
            InitializeComponent();
            InitializeTextBlocksAndParagraphs();
            StartTypingAnimation(0);
            AnimateButtonGrid();
            StartBorderAnimations();
            AnimateImage();
        }
        private void StartBorderAnimations()
        {
            // Устанавливаем начальное значение Opacity для маркеров
            Marker1.Opacity = 0;
            Marker2.Opacity = 0;
            Marker3.Opacity = 0;
            Marker4.Opacity = 0;

            // Параметры задержки анимации маркеров
            int delay1 = 1000; // 1 секунда для первого маркера
            int delay2 = 14000; // 14 секунд для второго
            int delay3 = 21000; // 21 секунда для третьего
            int delay4 = 28000; // 28 секунд для четвертого

            AnimateBorder(Marker1, delay1);
            AnimateBorder(Marker2, delay2);
            AnimateBorder(Marker3, delay3);
            AnimateBorder(Marker4, delay4);
        }

        private void AnimateBorder(Border border, int delay)
        {
            // Убедимся, что Border инициализирован
            if (border == null) return;

            // Устанавливаем начальные свойства
            border.Opacity = 0; // Устанавливаем начальную прозрачность
            var scaleTransform = new ScaleTransform(0.8, 0.8);
            border.RenderTransform = scaleTransform;
            border.RenderTransformOrigin = new Point(0.5, 0.5);

            // Анимация для Opacity
            var opacityAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(500),
                BeginTime = TimeSpan.FromMilliseconds(delay) // Начало анимации с задержкой
            };

            // Анимация для ScaleX
            var scaleXAnimation = new DoubleAnimation
            {
                From = 0.8,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(500),
                BeginTime = TimeSpan.FromMilliseconds(delay)
            };

            // Анимация для ScaleY
            var scaleYAnimation = new DoubleAnimation
            {
                From = 0.8,
                To = 1,
                Duration = TimeSpan.FromMilliseconds(500),
                BeginTime = TimeSpan.FromMilliseconds(delay)
            };

            // Запуск анимаций
            border.BeginAnimation(UIElement.OpacityProperty, opacityAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleXProperty, scaleXAnimation);
            scaleTransform.BeginAnimation(ScaleTransform.ScaleYProperty, scaleYAnimation);
        }
        private void InitializeTextBlocksAndParagraphs()
        {
            // Связываем TextBlock'и с их параграфами
            _textBlocks = new TextBlock[] { TxtWrite, TxtWrite1, TxtWrite2, TxtWrite3, TxtWrite4 };

            // Инициализируем текст и форматирование
            _paragraphs = new (string Text, bool IsBold)[][]
            {
                new (string, bool)[]
                {
                    ("\n\tУдаление татуировки – это процесс, который требует времени, терпения и стоит немало денег." +
                    " Мы вкратце познакомим вас с основными положениями о" +
                    " процессе лазерного удаления татуировок, который является самым эффективным " +
                    "на данный момент способом очистить кожу от нежеланного рисунка.", false)
                },
                new (string, bool)[]
                {
                    ("варианты удаления татуировки", true), // Жирный текст
                    ("\nДермабразия – косметологическая процедура, суть которой состоит в «соскабливании» поверхностного"+
                    "слоя кожи. Манипуляции осуществляются на специальном аппарате с фрезой из абразивных материалов."+
                    "Насадки, шлифовальные диски косметолог подбирает в зависимости от желаемого результата и типа кожи.", false)
                },
                new (string, bool)[]
                {
                    ("Химический пилинг (эксфолиация) – косметическая процедура, которая позволяет обновить поверхностные слои кожи. " +
                    "С помощью слабых кислот производится растворение отмирающих клеток и " +
                    "стимуляция естественных восстановительных процессов.", false)
                },
                new (string, bool)[]
                {
                    ("Иссечение – разновидность хирургической операции, при которой происходит вырезание нежелательного участка кожи", false)
                },
                new (string, bool)[]
                {
                    ("Криохирургия – вид хирургического лечения посредством низкотемпературного воздействия на аномальные или " +
                    "поражённые заболеванием биологические ткани с целью разрушения, уменьшения, " +
                    "удаления того или иного участка ткани или органа.", false)
                }
            };
        }
        private void AnimateImage()
        {
            var fadeAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new PowerEase { Power = 3 } 
            };

            var translateAnimation = new DoubleAnimation
            {
                From = -150,
                To = 0,
                Duration = TimeSpan.FromSeconds(1),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            Storyboard.SetTarget(fadeAnimation, ImgAnimated);
            Storyboard.SetTargetProperty(fadeAnimation, new PropertyPath(UIElement.OpacityProperty));

            Storyboard.SetTarget(translateAnimation, ImgAnimated);
            Storyboard.SetTargetProperty(translateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            var storyboard = new Storyboard();
            storyboard.Children.Add(fadeAnimation);
            storyboard.Children.Add(translateAnimation);
            storyboard.Begin();
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

        private void ArrowBut_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new YslugiPage());
        }

        private void Closer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
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
    }
}
