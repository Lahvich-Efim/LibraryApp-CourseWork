namespace LibraryApp
{
    public class LoginWindowViewModel : PropertyChangedNotification
    {
        DatabaseUnit db = new DatabaseUnit();

        public RelayCommand SwitchLangCommand { get; set; }
        public Uri SourcePage
        {
            get { return GetValue(() => SourcePage); }
            set { SetValue(() => SourcePage, value); }
        }
        public LoginWindowViewModel()
        {
            SourcePage = WindowService.GetPage(WindowService.Pages.SignIn);
        }
    }
}
