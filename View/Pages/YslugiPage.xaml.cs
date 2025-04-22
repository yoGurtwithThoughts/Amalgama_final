using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
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
    /// Логика взаимодействия для YslugiPage.xaml
    /// </summary>
    public partial class YslugiPage : Page
    {
      
        public YslugiPage()
        {
            InitializeComponent();

            
           
            string multiLineText = "Наши татуировщики наносят не просто картинку, \n" +
                                   "а создают настоящее произведение искусства, \n" +
                                   "которое выражает внутреннее «Я» клиента.\n";

            TxtB.Text = multiLineText;
            string multiLineText1 = "Если тату утратила свою актуальность, была , \n" +
                                  "нанесена непрофессиональным татуировщиком,  \n" +
                                  "представляет собой партак, то изменить \n"+
                                  "ситуацию помогут наши мастера. \n";

            TxtB1.Text = multiLineText1;
            string multiLineText2 = "Наши услуги пирсинга - это возможность  \n" +
                                  "украсить свое тело необычными и красивыми  \n" +
                                  "украшениями. Мы предлагаем широкий выбор \n" +
                                  "услуг. \n";

            TxtB2.Text = multiLineText2;
        }



        private void Page_Loaded1(object sender, RoutedEventArgs e)
        {
            // Y1 Animation
            var borderTranslateAnimation1 = new DoubleAnimation
            {
                From = -100,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                EasingFunction = new ElasticEase { Oscillations = 1, Springiness = 3 }
            };

            Storyboard.SetTarget(borderTranslateAnimation1, Y1);
            Storyboard.SetTargetProperty(borderTranslateAnimation1, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            var brdstory = new Storyboard();
            brdstory.Children.Add(borderTranslateAnimation1);

            // Y2 Animation
            var border1TranslateAnimation = new DoubleAnimation
            {
                From = -150,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                EasingFunction = new ElasticEase { Oscillations = 1, Springiness = 3 }
            };

            Storyboard.SetTarget(border1TranslateAnimation, Y2);
            Storyboard.SetTargetProperty(border1TranslateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            var brdstory1 = new Storyboard();
            brdstory1.Children.Add(border1TranslateAnimation);

            // Y3 Animation
            var border2TranslateAnimation = new DoubleAnimation
            {
                From = -200,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                EasingFunction = new ElasticEase { Oscillations = 1, Springiness = 3 }
            };

            Storyboard.SetTarget(border2TranslateAnimation, Y3);
            Storyboard.SetTargetProperty(border2TranslateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            var brdstory2 = new Storyboard();
            brdstory2.Children.Add(border2TranslateAnimation);

            // img
            var imgFadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                EasingFunction = new PowerEase { Power = 3 } // Using PowerEase for fade-in
            };

            var img1TranslateAnimation = new DoubleAnimation
            {
                From = -150,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                EasingFunction = new ElasticEase { Oscillations = 1, Springiness = 3 }
            };

            Storyboard.SetTarget(imgFadeInAnimation, imgAinimated);
            Storyboard.SetTargetProperty(imgFadeInAnimation, new PropertyPath(UIElement.OpacityProperty));

            Storyboard.SetTarget(img1TranslateAnimation, imgAinimated);
            Storyboard.SetTargetProperty(img1TranslateAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));

            var img = new Storyboard();
            img.Children.Add(img1TranslateAnimation);
            img.Children.Add(imgFadeInAnimation);

            // Start the Storyboards
            img.Begin();
            brdstory.Begin();
            brdstory1.Begin();
            brdstory2.Begin();
        }
        private void ArrowButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new StartPage()); 
        }

        private void More_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new TatooDetales());
        }

        private void More1_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new TatooRemoval());
        }

        private void More2_Click(object sender, RoutedEventArgs e)
        {
            CoreNavigate.NavigatorCore.Navigate(new Pirsing());
        }

        

        private void Close_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
