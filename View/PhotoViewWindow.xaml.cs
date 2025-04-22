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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Amalgama.View
{
    /// <summary>
    /// Логика взаимодействия для PhotoViewWindow.xaml
    /// </summary>
    public partial class PhotoViewWindow : Window
    {
        private string[] _imagePaths;
        private int _currentImageIndex;

        public PhotoViewWindow(string[] imagePaths, int currentIndex)
        {
            InitializeComponent();
            _imagePaths = imagePaths;
            _currentImageIndex = currentIndex;
            DisplayedImage.Source = new BitmapImage(new Uri(_imagePaths[_currentImageIndex], UriKind.RelativeOrAbsolute));
        }

        private void DisplayedImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Close(); // Закрываем окно при клике на изображение
        }

        private void PreviousButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (_currentImageIndex > 0)
            {
                _currentImageIndex--;
                DisplayedImage.Source = new BitmapImage(new Uri(_imagePaths[_currentImageIndex], UriKind.RelativeOrAbsolute));
            }
        }

        private void NextButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (_currentImageIndex < _imagePaths.Length - 1)
            {
                _currentImageIndex++;
                DisplayedImage.Source = new BitmapImage(new Uri(_imagePaths[_currentImageIndex], UriKind.RelativeOrAbsolute));
            }
        }

        private void Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }

}
