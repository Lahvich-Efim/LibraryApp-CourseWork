using System.IO;
using System.Windows;

namespace LibraryApp
{
    public static class Settings
    {
        private static Languages _lang;
        public static event Action changeLang;
        public static readonly string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public enum Languages
        {
            RU,
            EN
        }
        public static Languages Lang
        {
            get
            {
                return _lang;
            }
            set
            {
                _lang = value;
                changeLang?.Invoke();
            }
        }
        public static ResourceDictionary ResourceLight = new ResourceDictionary();
        public static ResourceDictionary ResourceDark = new ResourceDictionary();
        public static ResourceDictionary ResourceToggleButton = new ResourceDictionary();
        public static ResourceDictionary ResourceTextBox = new ResourceDictionary();
        public static ResourceDictionary ResourcePrimaryBrown = new ResourceDictionary();
        public static ResourceDictionary ResourcePrimaryCyan = new ResourceDictionary();
        public static ResourceDictionary ResourcePrimaryLime = new ResourceDictionary();
        public static ResourceDictionary ResourceStyles = new ResourceDictionary();
        public static ResourceDictionary ResourceEnLang = new ResourceDictionary();
        public static ResourceDictionary ResourceRusLang = new ResourceDictionary();
        public static ResourceDictionary ResourceButton = new ResourceDictionary();
        public static ResourceDictionary ResourceCheckBox = new ResourceDictionary();
        public static ResourceDictionary ResourceSlider = new ResourceDictionary();
        public static ResourceDictionary ResourceComboBox = new ResourceDictionary();
        public static ResourceDictionary ResourceListBox = new ResourceDictionary();
        public static ResourceDictionary ResourceDefaults = new ResourceDictionary();
        
        static Settings()
        {
            ResourcePrimaryBrown.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Brown.xaml");
            ResourcePrimaryCyan.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Cyan.xaml");
            ResourcePrimaryLime.Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.Lime.xaml");
            ResourceLight.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Light.xaml");
            ResourceDark.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Dark.xaml");    
            ResourceRusLang.Source = new Uri("pack://application:,,,/Resource/StringResources.Rus.xaml");
            ResourceEnLang.Source = new Uri("pack://application:,,,/Resource/StringResources.En.xaml");
            ResourceStyles.Source = new Uri("pack://application:,,,/Styles/Styles.xaml");
            ResourceToggleButton.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.ToggleButton.xaml");
            ResourceTextBox.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.TextBox.xaml");
            ResourceSlider.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Slider.xaml");
            ResourceComboBox.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.ComboBox.xaml");
            ResourceListBox.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.ListBox.xaml");
            ResourceCheckBox.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.CheckBox.xaml");
            ResourceButton.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Button.xaml");
            ResourceDefaults.Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Defaults.xaml");
        }
        public static string FindMessage(string message)
        {
            if (Application.Current.Resources[message] != null)
                return Application.Current.Resources[message].ToString();
            else
                return message;
        }
    }


}
