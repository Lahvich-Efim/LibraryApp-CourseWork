using LibraryApp.Model;

namespace LibraryApp
{
    public class AdminWindowViewModel : PropertyChangedNotification
    {
        private DatabaseUnit db = new DatabaseUnit();
        private static User user;
        public static int id;

        public RelayCommand OpenOrderBoardCommand { get; set; }
        public RelayCommand OpenCatalogPagesCommand { get; set; }
        public RelayCommand OpenEditContentProductCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public string Name
        {
            get { return GetValue(() => Name); }
            set { SetValue(() => Name, value); }
        }
        public Uri Page
        {
            get { return GetValue(() => Page); }
            set { SetValue(() => Page, value); }
        }

        public string? SourceImage
        {
            get { return GetValue(() => SourceImage); }
            set { SetValue(() => SourceImage, value); }
        }
        public AdminWindowViewModel()
        {
            OpenOrderBoardCommand = new RelayCommand(OpenOrderBoard);
            OpenCatalogPagesCommand = new RelayCommand(OpenCatalogPages);
            CloseCommand = new RelayCommand(Close);
            OpenEditContentProductCommand = new RelayCommand(OpenEditContentProduct);
            user = db.Users.Get(id);
            if (user.Pavatar != null)
            {
                SourceImage = Settings.projectPath + "/image/Avatars/" + user.Pavatar;
            }
            else
            {
                SourceImage = null;
            }
            Name = user.Username;
            Page = WindowService.GetPage(WindowService.Pages.AdminProductsPage);
        }
        private void Close()
        {
            WindowService.OpenWindow(WindowService.Windows.Login, new LoginWindowViewModel());
            WindowService.CloseWindow(WindowService.Windows.AdminWindow);
        }
        private void OpenOrderBoard()
        {
            WindowService.AdminWindow.AdminFrame.Source = WindowService.GetPage(WindowService.Pages.OrderBoard);
        }
        private void OpenCatalogPages()
        {
            WindowService.AdminWindow.AdminFrame.Source = WindowService.GetPage(WindowService.Pages.AdminProductsPage);
        }
        private void OpenEditContentProduct()
        {
            WindowService.AdminWindow.AdminFrame.Source = WindowService.GetPage(WindowService.Pages.EditContentProduct);
        }
    }
}
