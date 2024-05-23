using System.Windows.Controls;

namespace LibraryApp
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductDetailsPage : Page
    {
        //private Product product;
        public ProductDetailsPage()
        {
            InitializeComponent();
            DataContext = new ProductDetailsPageViewModel();
        }

    }
}
