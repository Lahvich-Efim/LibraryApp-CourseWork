using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LibraryApp.UseControls
{
    /// <summary>
    /// Логика взаимодействия для RangeSlider.xaml
    /// </summary>
    public partial class RangeSlider : UserControl
    {
        #region  Private variable

        private static int _width = 150;  //  Initial width of drag bar
        private static int _height = 30;  //  height
        private static int _min = 0;      //  Minimum
        private static int _max = 100;    //  Max
        private static int _freq = 10;    //  The interval at which the scale appears

        #endregion

        //  Constructor
        public RangeSlider()
        {
            InitializeComponent();
        }

        #region  Private property

        /// <summary>
        ///  Crop matrix (head)
        /// </summary>
        /// 
        public ICommand MyCommand
        {
            get { return (ICommand)GetValue(MyCommandProperty); }
            set { SetValue(MyCommandProperty, value); }
        }

        public static readonly DependencyProperty MyCommandProperty =
                DependencyProperty.Register("MyCommand", typeof(ICommand), typeof(RangeSlider));
        private Rect StartRect
        {
            get { return (Rect)GetValue(StartRectProperty); }
            set { SetValue(StartRectProperty, value); }
        }
        private static readonly DependencyProperty StartRectProperty =
            DependencyProperty.Register("StartRect", typeof(Rect), typeof(RangeSlider));

        /// <summary>
        ///  Clipping matrix (tail)
        /// </summary>
        private Rect EndRect
        {
            get { return (Rect)GetValue(EndRectProperty); }
            set { SetValue(EndRectProperty, value); }
        }
        private static readonly DependencyProperty EndRectProperty =
            DependencyProperty.Register("EndRect", typeof(Rect), typeof(RangeSlider));

        #endregion

        #region  Public dependency properties

        /// <summary>
        ///  Scale interval, default is 10
        /// </summary>
        public int SliderTickFrequency
        {
            get { return (int)GetValue(SliderTickFrequencyProperty); }
            set { SetValue(SliderTickFrequencyProperty, value); }
        }
        public static readonly DependencyProperty SliderTickFrequencyProperty =
            DependencyProperty.Register("SliderTickFrequency", typeof(int), typeof(RangeSlider), new PropertyMetadata(_freq));

        /// <summary>
        ///  The height of the control, the default is 30
        /// </summary>
        public int SilderHeight
        {
            get { return (int)GetValue(SilderHeightProperty); }
            set { SetValue(SilderHeightProperty, value); }
        }
        public static readonly DependencyProperty SilderHeightProperty =
            DependencyProperty.Register("SilderHeight", typeof(int), typeof(RangeSlider), new PropertyMetadata(_height));

        /// <summary>
        ///  The width of the drag bar, the default is 150
        /// </summary>
        public int SilderWidth
        {
            get { return (int)GetValue(SilderWidthProperty); }
            set { SetValue(SilderWidthProperty, value); }
        }
        public static readonly DependencyProperty SilderWidthProperty =
            DependencyProperty.Register("SilderWidth", typeof(int), typeof(RangeSlider), new PropertyMetadata(_width));

        /// <summary>
        ///  The minimum value, the default is 0
        /// </summary>
        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(RangeSlider), new PropertyMetadata(_min));

        /// <summary>
        ///  Maximum value, default is 100
        /// </summary>
        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(RangeSlider), new PropertyMetadata(_max));

        /// <summary>
        ///  Select the starting value, the default is 0
        /// </summary>
        public int StartValue
        {
            get { return (int)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }
        public static readonly DependencyProperty StartValueProperty =
            DependencyProperty.Register("StartValue", typeof(int), typeof(RangeSlider));

        /// <summary>
        ///  Select the end value, the default is 100
        /// </summary>
        public int EndValue
        {
            get { return (int)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }
        public static readonly DependencyProperty EndValueProperty =
            DependencyProperty.Register("EndValue", typeof(int), typeof(RangeSlider), new PropertyMetadata(_max));

        #endregion

        #region  Front-end interaction

        /// <summary>
        ///  Crop the two drag bars
        /// </summary>
        private void ClipSilder()
        {
            int selectedValue = EndValue - StartValue;
            int totalValue = Maximum - Minimum;
            double sliderClipWidth = SilderWidth * (StartValue - Minimum + selectedValue / 2) / totalValue;
            //  Crop the first drag bar
            StartRect = new Rect(0, 0, sliderClipWidth, SilderHeight);
            //  Crop the second drag bar
            EndRect = new Rect(sliderClipWidth, 0, SilderWidth, SilderHeight);
        }

        /// <summary>
        ///  Initial crop
        /// </summary>
        private void UC_Arrange_Loaded(object sender, RoutedEventArgs e)
        {
            SL_Bat1.ValueChanged += SL_Bat1_ValueChanged;
            SL_Bat2.ValueChanged += SL_Bat2_ValueChanged;
            ClipSilder();
        }

        private void SL_Bat1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue > EndValue)    //  Check value range
                StartValue = EndValue;    //  Exceeded, reset to maximum
            ClipSilder();
        }

        private void SL_Bat2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue < StartValue)
                EndValue = StartValue;
            ClipSilder();
        }

        private void TextBox_KeyUp1(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)    //  Confirm input when pressing enter
                    StartValue = Convert.ToInt32(((TextBox)sender).Text);
            }
            catch
            {
            }
        }

        private void TextBox_KeyUp2(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                    EndValue = Convert.ToInt32(((TextBox)sender).Text);
            }
            catch
            {
            }
        }

        #endregion
    }
}
