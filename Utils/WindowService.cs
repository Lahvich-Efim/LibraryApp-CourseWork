using LibraryApp.Views;
using System.Windows;

namespace LibraryApp
{
    public static class WindowService
    {
        public enum Windows
        {
            Login,
            MainWindow,
            AdminWindow,
            Message,
            ProductDialog,
            UserDialog,
            UserEnter,
            ChangePass,
        };
        public enum Pages
        {
            UserPage,
            SignIn,
            SignUp,
            CatalogPage,
            MainPage,
            ProductDetailsPage,
            ProductsPage,
            AdminProductsPage,
            OrderBoard,
            EditContentProduct,
        };
        #region Windows
        public static MainWindow MainWindow { get; set; }
        public static LoginWindow LoginWindow { get; set; }
        public static AdminWindow AdminWindow { get; set; }
        public static UserEnter UserEnter { get; set; }
        public static ProductDialog ProductDialog{ get; set; }
        #endregion


        static WindowService() { }
        public static void OpenWindow(Windows win, PropertyChangedNotification ViewModel, EventHandler onClosingFunc = null)
        {
            Window window = null;
            bool dialog = false;
            switch (win)
            {
                case Windows.MainWindow:
                    window = new MainWindow();
                    MainWindow = (MainWindow)window;
                    break;
                case Windows.UserEnter:
                    window = new UserEnter();
                    UserEnter = (UserEnter)window;
                    dialog = true;
                    break;
                case Windows.AdminWindow:
                    window = new AdminWindow();
                    AdminWindow = (AdminWindow)window;
                    break;
                case Windows.Login:
                    window = new LoginWindow();
                    LoginWindow = (LoginWindow)window;
                    break;
                case Windows.ProductDialog:
                    window = new ProductDialog();
                    ProductDialog = (ProductDialog)window;
                    break;
                default:
                    Console.WriteLine("No such Window");
                    break;
            }
            if (window != null)
            {
                window.DataContext = ViewModel;
                if (onClosingFunc != null)
                    window.Closed += onClosingFunc;
                if (dialog)
                    window.ShowDialog();
                else
                    window.Show();
            }
        }

        public static void CloseWindow(Windows win)
        {
            switch (win)
            {
                case Windows.MainWindow:
                    MainWindow.Close();
                    break;
                case Windows.AdminWindow:
                    AdminWindow.Close();
                    break;
                case Windows.Login:
                    LoginWindow.Close();
                    break;
                case Windows.UserEnter:
                    UserEnter.Close();
                    break;
                case Windows.ProductDialog:
                    ProductDialog.Close();
                    break;
                default:
                    Console.WriteLine("No such Window");
                    break;
            }
        }
        public static Uri GetPage(Pages page)
        {
            Uri uri;
            switch (page)
            {
                case Pages.ProductsPage:
                    uri = new Uri("pack://application:,,,/Views/ProductsPage.xaml");
                    break;
                case Pages.ProductDetailsPage:
                    uri = new Uri("pack://application:,,,/Views/ProductDetailsPage.xaml");
                    break;
                case Pages.MainPage:
                    uri = new Uri("pack://application:,,,/Views/MainPage.xaml");
                    break;
                case Pages.CatalogPage:
                    uri = new Uri("pack://application:,,,/Views/CatalogPage.xaml");
                    break;
                case Pages.SignUp:
                    uri = new Uri("pack://application:,,,/Views/SignUpPage.xaml");
                    break;
                case Pages.SignIn:
                    uri = new Uri("pack://application:,,,/Views/SignInPage.xaml");
                    break;
                case Pages.UserPage:
                    uri = new Uri("pack://application:,,,/Views/UserPage.xaml");
                    break;
                case Pages.AdminProductsPage:
                    uri = new Uri("pack://application:,,,/Views/AdminProductsPage.xaml");
                    break;
                case Pages.OrderBoard:
                    uri = new Uri("pack://application:,,,/Views/OrderBoard.xaml");
                    break;
                case Pages.EditContentProduct:
                    uri = new Uri("pack://application:,,,/Views/EditContentProduct.xaml");
                    break;
                default:
                    uri = null;
                    break;
            }
            return uri;
        }

    }
}
