using System.Windows.Controls;

namespace LibraryApp.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminProductPage.xaml
    /// </summary>
    public partial class AdminProductPage : Page
    {
        public AdminProductPage()
        {
            InitializeComponent();
            DataContext = new AdminProductPageViewModel();
        }

    }
}

