using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace LibraryApp
{
    public class SpecValueToTemplate : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var Spec = values[0] as Tuple<int, string?>;
            if (values[1] is List<Tuple<int, string?>> valueSpecString)
            {
                StackPanel stackPanel = new StackPanel();

                // Создаем TextBlock
                TextBlock textBlock = new TextBlock();
                textBlock.Text = Spec.Item2;
                stackPanel.Children.Add(textBlock);
                foreach (var item in valueSpecString)
                {
                    StackPanel stack = new StackPanel();
                    stack.Orientation = Orientation.Horizontal;
                    TextBlock txt = new TextBlock();
                    txt.Text = item.Item2;
                    CheckBox checkBox = new CheckBox();
                    checkBox.Name = "chec" + Spec.Item1 + "_" + item.Item1;
                    stack.Children.Add(txt);
                    stack.Children.Add(checkBox);
                    stackPanel.Children.Add(stack);
                }
                return stackPanel;
            }
            else if (values[1] is Tuple<decimal?, decimal?> valueSpecInt)
            {

                StackPanel stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Horizontal;
                TextBlock text = new TextBlock();
                text.Text = Spec.Item2;
                TextBlock max = new TextBlock();
                max.Text = valueSpecInt.Item1.ToString();
                TextBlock min = new TextBlock();
                min.Text = valueSpecInt.Item2.ToString();
                stackPanel.Children.Add(text);
                stackPanel.Children.Add(max);
                stackPanel.Children.Add(min);
                return stackPanel;
            }
            return null;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
