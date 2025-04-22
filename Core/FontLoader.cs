using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Amalgama.Core
{
    class FontLoader
    {
        public static void LoadFonts()
        {
          
            string fontUri = "pack://application:,,,/Amalgama;component/Fonts/#Jura.ttf";
                var typeface = new Typeface(new FontFamily(fontUri), FontStyles.Normal, FontWeights.Normal, FontStretches.Normal);

               
                var fontFamily = new FontFamily(fontUri);
                Application.Current.Resources.Add("CustomFont", fontFamily);
            
        }
    }
}
