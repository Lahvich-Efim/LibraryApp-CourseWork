using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LibraryApp.UseControls
{
    /// <summary>
    /// Логика взаимодействия для ViewRaitingControl.xaml
    /// </summary>
    public partial class ViewRaitingControl : UserControl
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(ViewRaitingControl), new PropertyMetadata(0));
        public static readonly DependencyProperty BackgroundStarProperty =
            DependencyProperty.Register("BackgroundStar", typeof(Brush), typeof(ViewRaitingControl), new PropertyMetadata(Brushes.Coral));
        public static readonly DependencyProperty BackgroundValueStarProperty =
            DependencyProperty.Register("BackgroundValueStar", typeof(Brush), typeof(ViewRaitingControl), new PropertyMetadata(Brushes.Yellow));
        public static readonly DependencyProperty BackgroundEventStarProperty =
            DependencyProperty.Register("BackgroundEventStar", typeof(Brush), typeof(ViewRaitingControl), new PropertyMetadata(Brushes.Gray));
        public static readonly DependencyProperty BorderColorStarProperty =
            DependencyProperty.Register("BorderColorStar", typeof(Brush), typeof(ViewRaitingControl), new PropertyMetadata(Brushes.Black));
        public static readonly DependencyProperty StarThiknessProperty =
            DependencyProperty.Register("StarThikness", typeof(double), typeof(ViewRaitingControl), new PropertyMetadata(1.0));

        internal double StarThikness
        {
            get { return (double)GetValue(StarThiknessProperty); }
            set { SetValue(StarThiknessProperty, value); }
        }
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public Brush BackgroundStar
        {
            get { return (Brush)GetValue(BackgroundStarProperty); }
            set { SetValue(BackgroundStarProperty, value); }
        }
        public Brush BackgroundValueStar
        {
            get { return (Brush)GetValue(BackgroundValueStarProperty); }
            set { SetValue(BackgroundValueStarProperty, value); }
        }
        public Brush BackgroundEventStar
        {
            get { return (Brush)GetValue(BackgroundEventStarProperty); }
            set { SetValue(BackgroundEventStarProperty, value); }
        }
        public Brush BorderColorStar
        {
            get { return (Brush)GetValue(BorderColorStarProperty); }
            set { SetValue(BorderColorStarProperty, value); }
        }



        public ViewRaitingControl()
        {
            InitializeComponent();

        }

        private void Paths_MouseEnter(object sender, MouseEventArgs e)
        {
            int id = Int32.Parse((sender as Path).Name.Replace("star", ""));
            for (int i = 1; i <= ListStar.Children.Count; i++)
            {
                var item = (Path)ListStar.Children[i - 1];
                if (i <= id)
                    item.Fill = BackgroundEventStar;
                else if (i <= Value)
                    item.Fill = BackgroundValueStar;
                else
                    item.Fill = BackgroundStar;
            }
        }
        private void Paths_Click(object sender, MouseButtonEventArgs e)
        {
            Value = Int32.Parse((sender as Path).Name.Replace("star", ""));
            for (int i = 1; i <= ListStar.Children.Count; i++)
            {
                var item = (Path)ListStar.Children[i - 1];
                if (i <= Value)
                    item.Fill = BackgroundValueStar;
                else
                    item.Fill = BackgroundStar;
            }

        }
        private void Paths_MouseLeave(object sender, MouseEventArgs e)
        {
            int i = 1;
            foreach (UIElement item in ListStar.Children)
                if (item is Path)
                {
                    var pathitem = (Path)item;
                    if (i++ <= Value)
                        pathitem.Fill = BackgroundValueStar;
                    else
                        pathitem.Fill = BackgroundStar;
                }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (UIElement item in ListStar.Children)
                if (item is Path)
                {
                    var pathitem = (Path)item;
                    pathitem.Name = "star" + ++i;
                    if (i <= Value)
                        pathitem.Fill = BackgroundValueStar;
                    else
                        pathitem.Fill = BackgroundStar;

                }
        }

    }
}
