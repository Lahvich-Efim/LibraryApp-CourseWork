using System.Windows.Controls;

namespace LibraryApp.Views
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
            DataContext = new SignUpPageViewModel();
        }

    }
}
