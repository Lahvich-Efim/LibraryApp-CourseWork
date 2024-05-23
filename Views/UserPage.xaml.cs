using System.Windows;
using System.Windows.Controls;

namespace LibraryApp.Views
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            DataContext = new UserPageViewModel();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            (DataContext as UserPageViewModel).ShowPage();
        }
    }
}
