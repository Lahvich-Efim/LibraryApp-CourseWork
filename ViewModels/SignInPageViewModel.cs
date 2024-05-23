using System.Text.RegularExpressions;

namespace LibraryApp
{
    public class SignInPageViewModel : PropertyChangedNotification
    {
        DatabaseUnit db = new DatabaseUnit();
        public RelayCommand LoginCommand { get; set; }
        /*public RelayCommand RecoverCommand { get; set; }*/
        public RelayCommand SignUpPageCommand { get; set; }
        public string UserName
        {
            get { return GetValue(() => UserName); }
            set
            {
                SetValue(() => UserName, value);
                if (UserName.Length == 0)
                    ErrorEmptyUsernameVisibility = true;
                else
                    ErrorEmptyUsernameVisibility = false;
                ErrorNonExistentLoginErrorVisibility = false;
            }
        }
        public string Password
        {
            get { return GetValue(() => Password); }
            set
            {
                SetValue(() => Password, value);
                if (Password.Length == 0)
                    ErrorEmptyPasswordVisibility = true;
                else
                    ErrorEmptyPasswordVisibility = false;

                ErrorIncorrectPasswordVisibility = false;
            }
        }


        #region Errors
        public bool ErrorEmptyUsernameVisibility
        {
            get { return GetValue(() => ErrorEmptyUsernameVisibility); }
            set { SetValue(() => ErrorEmptyUsernameVisibility, value); }
        }
        public bool ErrorNonExistentLoginErrorVisibility
        {
            get { return GetValue(() => ErrorNonExistentLoginErrorVisibility); }
            set { SetValue(() => ErrorNonExistentLoginErrorVisibility, value); }
        }
        public bool ErrorEmptyPasswordVisibility
        {
            get { return GetValue(() => ErrorEmptyPasswordVisibility); }
            set { SetValue(() => ErrorEmptyPasswordVisibility, value); }
        }
        public bool ErrorIncorrectPasswordVisibility
        {
            get { return GetValue(() => ErrorIncorrectPasswordVisibility); }
            set { SetValue(() => ErrorIncorrectPasswordVisibility, value); }
        }
        #endregion
        public SignInPageViewModel()
        {
            LoginCommand = new RelayCommand(LogIn);
            SignUpPageCommand = new RelayCommand(ToSignUpPage);
            UserName = "Admin";
            Password = "admin1234";
        }

        private void ToSignUpPage()
        {
            WindowService.LoginWindow.FormBox.Source = WindowService.GetPage(WindowService.Pages.SignUp);
        }
        private void LogIn()
        {
            try
            {
                var bAdmin = db.Users.GetAll().Any(o => o.Username == UserName && SaltedHash.Verify(o.Salt, o.PasswordHash, Password) && o.IsAdmin);
                var bUser = db.Users.GetAll().Any(o => o.Username == UserName && SaltedHash.Verify(o.Salt, o.PasswordHash, Password));

                if (!db.Users.GetAll().Any(o => o.Username == UserName))
                    ErrorNonExistentLoginErrorVisibility = true;
                else if (UserName.Length != 0 && !ErrorNonExistentLoginErrorVisibility && !bUser)
                    ErrorIncorrectPasswordVisibility = true;
                else if (bAdmin)
                {
                    AdminWindowViewModel.id = db.Users.GetAll().First(o => o.Username == UserName && SaltedHash.Verify(o.Salt, o.PasswordHash, Password) && o.IsAdmin).UserId;
                    WindowService.OpenWindow(WindowService.Windows.AdminWindow, new AdminWindowViewModel());
                    WindowService.CloseWindow(WindowService.Windows.Login);

                }
                else if (bUser)
                {
                    MainWindowViewModel.id = db.Users.GetAll().First(o => o.Username == UserName && SaltedHash.Verify(o.Salt, o.PasswordHash, Password)).UserId;
                    WindowService.OpenWindow(WindowService.Windows.MainWindow, new MainWindowViewModel());
                    WindowService.CloseWindow(WindowService.Windows.Login);

                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {

            }
        }
    }
}
