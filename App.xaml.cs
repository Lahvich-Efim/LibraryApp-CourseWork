using System.Windows;
using System.Windows.Input;

namespace LibraryApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            AdminWindowViewModel.id = 6;
            WindowService.OpenWindow(WindowService.Windows.AdminWindow, new AdminWindowViewModel());
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            // Получаем ресурс курсора из ресурсов приложения
            Cursor customCursor = (Cursor)FindResource("CustomCursor");

            // Устанавливаем курсор по умолчанию для всего приложения
            Mouse.OverrideCursor = customCursor;

            Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
            Settings.Lang = Settings.Languages.RU;
        }
    }

}
