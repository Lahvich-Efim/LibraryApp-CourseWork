using Newtonsoft.Json.Linq;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LibraryApp
{
    public class ImageToTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string imagePath = values[0] as string;
            string userName = values[1] as string;
            double r = System.Convert.ToDouble(values[2]);
            double b = 0;
            if (values[3] is Thickness thickness)
            {
                b = System.Convert.ToDouble(thickness.Top);
            }
            double radius = (r - 2 * b) / 2;

            if (!string.IsNullOrEmpty(imagePath))
            {

                Image image = new Image() { Stretch = Stretch.UniformToFill };
                BitmapImage ty = new BitmapImage();

                var tmp_img = new MemoryStream(File.ReadAllBytes(imagePath));
                ty.BeginInit();
                ty.StreamSource = tmp_img;
                ty.EndInit();

                image.SetCurrentValue(Image.SourceProperty, ty);
                EllipseGeometry ellipseGeometry = new EllipseGeometry(new Point(radius, radius), radius, radius);
                image.Clip = ellipseGeometry;
                return image;
            }

            if (!string.IsNullOrEmpty(userName))
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = GetFirstLetter(userName).ToUpper();
                textBlock.VerticalAlignment = VerticalAlignment.Center;
                textBlock.FontSize = radius * 0.8;
                textBlock.FontWeight = FontWeights.Bold;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                return textBlock;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private string GetFirstLetter(string input)
        {
            return input.Substring(0, 1);
        }
    }
}
