namespace LibraryApp
{
    using System.Windows;
    public static class WaterMarkExtentions
    {
        #region Static Fields

        public static readonly DependencyProperty WaterMarkProperty = DependencyProperty.RegisterAttached(
            "WaterMark",
            typeof(string),
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(string.Empty));

        #endregion

        #region Public Methods and Operators

        public static string GetWaterMark(DependencyObject obj)
        {
            return (string)obj.GetValue(WaterMarkProperty);
        }

        public static void SetWaterMark(DependencyObject obj, string value)
        {
            obj.SetValue(WaterMarkProperty, value);
        }

        #endregion
    }
}
