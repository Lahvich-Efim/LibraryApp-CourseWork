using LibraryApp.Model;
using Microsoft.Win32;
using Notification.Wpf;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace LibraryApp
{
    public class SignUpPageViewModel : PropertyChangedNotification
    {

        NotificationManager notificationManager = new NotificationManager();
        public RelayCommand OpenWindowCommand { get; set; }
        public RelayCommand RegisterCommand { get; set; }
        public RelayCommand OpenVerificationCommand { get; set; }
        public RelayCommand CloseVerificationCommand { get; set; }
        public RelayCommand CloseVerificationSMSCommand { get; set; }
        public RelayCommand ResentVerificationCommand { get; set; }
        public RelayCommand ResentVerificationSMSCommand { get; set; }
        public RelayCommand OpenInfoAccauntCommand { get; set; }
        public RelayCommand AddImageCommand { get; set; }
        public RelayCommand SignInPageCommand { get; set; }
        private User user;
        private int code = 0;
        private string filePath = "";
        private string[] defaultCollectionRus = { "Избранное", "Прочитано", "Брошено", "Запланировано" };
        private string[] defaultCollectionEn = { "Favorites", "Read", "Dropped", "Planned" };
        private static readonly Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);
        private static readonly Regex phoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$", RegexOptions.Compiled);
        private static bool verificate = false;

        public bool RegistrationVisability
        {
            get { return GetValue(() => RegistrationVisability); }
            set
            {
                SetValue(() => RegistrationVisability, value);
            }
        }

        public bool VerificationVisability
        {
            get { return GetValue(() => VerificationVisability); }
            set
            {
                SetValue(() => VerificationVisability, value);
            }
        }
        public bool VerificationSMSVisability
        {
            get { return GetValue(() => VerificationSMSVisability); }
            set
            {
                SetValue(() => VerificationSMSVisability, value);
            }
        }


        public bool InfoAccauntVisability
        {
            get { return GetValue(() => InfoAccauntVisability); }
            set
            {
                SetValue(() => InfoAccauntVisability, value);
            }
        }
        public string VerificationCode
        {
            get { return GetValue(() => VerificationCode); }
            set
            {
                SetValue(() => VerificationCode, value);
                ErrorVerificationVisability = false;
            }
        }
        public string VerificationSMSCode
        {
            get { return GetValue(() => VerificationSMSCode); }
            set
            {
                SetValue(() => VerificationSMSCode, value);
                ErrorVerificationSMSVisability = false;
            }
        }
        public string UserName
        {
            get { return GetValue(() => UserName); }
            set
            {
                SetValue(() => UserName, value);
                ErrorUsernameAlreadyExistsVisibility = false;
                ErrorEmptyUsernameVisibility = false;
                ErrorValidationUsernameVisibility = false;
            }
        }

        public string FirstName
        {
            get { return GetValue(() => FirstName); }
            set
            {
                SetValue(() => FirstName, value);
            }
        }

        public string LastName
        {
            get { return GetValue(() => LastName); }
            set
            {
                SetValue(() => LastName, value);
            }
        }
        public string FatherName
        {
            get { return GetValue(() => FatherName); }
            set
            {
                SetValue(() => FatherName, value);
            }
        }
        public string ImageSource
        {
            get { return GetValue(() => ImageSource); }
            set
            {
                SetValue(() => ImageSource, value);
            }
        }

        public string Address
        {
            get { return GetValue(() => Address); }
            set
            {
                SetValue(() => Address, value);
            }
        }

        public string Email
        {
            get { return GetValue(() => Email); }
            set
            {
                SetValue(() => Email, value);
                ErrorEmptyEmailVisibility = false;
                ErrorEmailAlreadyExistsVisibility = false;
                ErrorValidationEmailVisibility = false;
            }
        }
        public string Phone
        {
            get { return GetValue(() => Phone); }
            set
            {
                SetValue(() => Phone, value);
                verificate = false;
            }
        }
        public string Password
        {
            get { return GetValue(() => Password); }
            set
            {
                SetValue(() => Password, value);
                ErrorEmptyPasswordVisibility = false;
                ErrorIncorrectPasswordVisibility = false;
            }
        }
        public string ConfirmPassword
        {
            get { return GetValue(() => ConfirmPassword); }
            set
            {
                SetValue(() => ConfirmPassword, value);
                ErrorEmptyConfirmPasswordVisibility = false;
                ErrorPasswordMismatchVisibility = false;
            }
        }




        #region Errors
        public bool ErrorUsernameAlreadyExistsVisibility
        {
            get { return GetValue(() => ErrorUsernameAlreadyExistsVisibility); }
            set { SetValue(() => ErrorUsernameAlreadyExistsVisibility, value); }
        }
        public bool ErrorEmptyUsernameVisibility
        {
            get { return GetValue(() => ErrorEmptyUsernameVisibility); }
            set { SetValue(() => ErrorEmptyUsernameVisibility, value); }
        }
        public bool ErrorValidationUsernameVisibility
        {
            get { return GetValue(() => ErrorValidationUsernameVisibility); }
            set { SetValue(() => ErrorValidationUsernameVisibility, value); }
        }
        public bool ErrorEmptyEmailVisibility
        {
            get { return GetValue(() => ErrorEmptyEmailVisibility); }
            set { SetValue(() => ErrorEmptyEmailVisibility, value); }
        }
        public bool ErrorValidationEmailVisibility
        {
            get { return GetValue(() => ErrorValidationEmailVisibility); }
            set { SetValue(() => ErrorValidationEmailVisibility, value); }
        }
        public bool ErrorEmailAlreadyExistsVisibility
        {
            get { return GetValue(() => ErrorEmailAlreadyExistsVisibility); }
            set { SetValue(() => ErrorEmailAlreadyExistsVisibility, value); }
        }
        public bool ErrorEmptyPasswordVisibility
        {
            get { return GetValue(() => ErrorEmptyPasswordVisibility); }
            set { SetValue(() => ErrorEmptyPasswordVisibility, value); }
        }

        public bool ErrorEmptyConfirmPasswordVisibility
        {
            get { return GetValue(() => ErrorEmptyConfirmPasswordVisibility); }
            set { SetValue(() => ErrorEmptyConfirmPasswordVisibility, value); }
        }
        public bool ErrorIncorrectPasswordVisibility
        {
            get { return GetValue(() => ErrorIncorrectPasswordVisibility); }
            set { SetValue(() => ErrorIncorrectPasswordVisibility, value); }
        }
        public bool ErrorPasswordMismatchVisibility
        {
            get { return GetValue(() => ErrorPasswordMismatchVisibility); }
            set { SetValue(() => ErrorPasswordMismatchVisibility, value); }
        }
        public bool ErrorVerificationVisability
        {
            get { return GetValue(() => ErrorVerificationVisability); }
            set { SetValue(() => ErrorVerificationVisability, value); }
        }
        public bool ErrorVerificationSMSVisability
        {
            get { return GetValue(() => ErrorVerificationSMSVisability); }
            set { SetValue(() => ErrorVerificationSMSVisability, value); }
        }
        #endregion


        public SignUpPageViewModel()
        {
            RegisterCommand = new RelayCommand(Register);
            SignInPageCommand = new RelayCommand(ToSignInPage);
            OpenInfoAccauntCommand = new RelayCommand(OpenInfoAccaunt);
            OpenWindowCommand = new RelayCommand(OpenWindow);
            ResentVerificationCommand = new RelayCommand(ResentVerification);
            ResentVerificationSMSCommand = new RelayCommand(ResentVerificationSMS);
            CloseVerificationCommand = new RelayCommand(CloseVerification);
            CloseVerificationSMSCommand = new RelayCommand(CloseVerificationSMS);
            OpenVerificationCommand = new RelayCommand(OpenVerification);
            AddImageCommand = new RelayCommand(AddImage);
            UserName = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Address = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            VerificationCode = string.Empty;
            Password = string.Empty;
            ImageSource = string.Empty;
            ConfirmPassword = string.Empty;
            FatherName = string.Empty;
            RegistrationVisability = true;
            VerificationVisability = false;
            VerificationSMSVisability = false;
            InfoAccauntVisability = false;

        }

        private void ToSignInPage()
        {
            WindowService.LoginWindow.FormBox.Source = WindowService.GetPage(WindowService.Pages.SignIn);
        }

        private void AddImage()
        {
            try
            {
                var filePicker = new OpenFileDialog();

                filePicker.DefaultExt = ".jpg";
                filePicker.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";

                bool? result = filePicker.ShowDialog();

                if (result == true)
                {
                    filePath = filePicker.FileName;
                    ImageSource = filePath;
                }
            }
            catch
            {

            }
        }

        private void Register()
        {
            try
            {
                if (verificate || Phone == "")
                {
                    DatabaseUnit db = new DatabaseUnit();
                    Basket basket = new Basket();
                    Library lib = new Library();
                    user.FirstName = FirstName;
                    user.LastName = LastName;
                    user.Address = Address;
                    user.FatherName = FatherName;
                    var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                    var context = new ValidationContext(user);
                    if (Validator.TryValidateObject(user, context, results, true))
                    {
                        if (ImageSource != "")
                        {
                            string[] str = ImageSource.Split('\\');
                            string[] extensions = str[str.Length - 1].Split('.');
                            string extension = extensions[extensions.Length - 1];
                            user.Pavatar = user.UserId + "." + extension;
                            File.Copy(ImageSource, Settings.projectPath + "\\image\\Avatars\\" + user.Pavatar, true);
                        }
                        else
                        {
                            user.Pavatar = ImageSource;
                        }
                        db.Users.Update(user);
                        basket.UserId = user.UserId;
                        db.Baskets.Add(basket);
                        lib.UserId = user.UserId;
                        db.Libraries.Add(lib);
                        string tempHeader = "";
                        string tempMessage = "";
                        string Name = "";
                        if (user.FirstName == "")
                        {
                            Name = user.Username;
                        }
                        else
                        {
                            Name = user.FirstName;
                        }
                        switch (Settings.Lang)
                        {
                            case Settings.Languages.RU:
                                tempHeader = "Добро пожаловать в LibraryApp";
                                tempMessage = $"Уважаемый {Name}," +
                            "\r\n\r\nДобро пожаловать в наше библиотечное приложение! Мы рады приветствовать вас в пространстве, " +
                            "где собраны тысячи книг, журналов и других источников информации, готовых стать частью " +
                            "вашего интеллектуального и культурного обогащения." +
                            "\r\nСпасибо, что выбрали наше приложение. Желаем вам увлекательного и познавательного чтения!" +
                            "\r\n\r\nС наилучшими пожеланиями, LibraryApp";
                                for (int i = 0; defaultCollectionRus.Length > i; i++)
                                {
                                    Collection collection = new Collection();
                                    collection.LibraryId = lib.LibraryId;
                                    collection.Name = defaultCollectionRus[i];
                                    collection.IsDefault = true;
                                    db.Collections.Add(collection);
                                }
                                break;
                            case Settings.Languages.EN:
                                tempHeader = "Welcome to LibraryApp";
                                tempMessage = $"Dear {Name}," +
                                "\r\n\r\nWelcome to our library application! We are delighted to welcome you to a space " +
                                "where thousands of books, magazines, and other sources of information are gathered, ready to become " +
                                "part of your intellectual and cultural enrichment." +
                                "\r\nThank you for choosing our application. We wish you enjoyable and informative reading!" +
                                "\r\n\r\nBest regards, LibraryApp";
                                for (int i = 0; defaultCollectionEn.Length > i; i++)
                                {
                                    Collection collection = new Collection();
                                    collection.LibraryId = lib.LibraryId;
                                    collection.Name = defaultCollectionEn[i];
                                    collection.IsDefault = true;
                                    db.Collections.Add(collection);
                                }
                                break;
                        }
                        var content = new NotificationContent
                        {
                            Title = tempHeader,
                            Message = tempMessage,
                            Type = NotificationType.None,
                            TrimType = NotificationTextTrimType.Attach,
                            RowsCount = 3,
                            Background = new SolidColorBrush(Colors.White),
                            Foreground = new SolidColorBrush(Colors.Black),
                        };
                        notificationManager.Show(content);
                        Notify newNotify = new Notify();
                        newNotify.Message = tempMessage;
                        newNotify.DataNotify = DateTime.Now;
                        newNotify.IdUser = user.UserId;
                        newNotify.Header = tempHeader;
                        db.Notifies.Add(newNotify);
                        MainWindowViewModel.id = user.UserId;
                        WindowService.OpenWindow(WindowService.Windows.MainWindow, new MainWindowViewModel());
                        WindowService.CloseWindow(WindowService.Windows.Login);
                    }
                    else { }
                }
                else
                {
                    if (Phone != "" && phoneRegex.IsMatch(Phone))
                    {
                        VerificationSMSVisability = true;
                        InfoAccauntVisability = false;
                        user.Phone = Phone;
                        Random rnd = new Random();
                        code = rnd.Next(10000, 99999);
                        SendSMSHelper.SetToUser(Phone, user);
                        SendSMSHelper.SendMessadeVerification(code);
                    }
                    else if (Phone != "")
                    {

                    }
                    
                }
            }
            catch
            {

            }
        }
        private void OpenInfoAccaunt()
        {
            if (VerificationCode == code.ToString())
            {
                DatabaseUnit db = new DatabaseUnit();
                db.Users.Add(user);
                VerificationVisability = false;
                InfoAccauntVisability = true;
            }
            else
            {
                ErrorVerificationVisability = true;
            }
        }
        private void ResentVerification()
        {
            try
            {
                SendEmailHelper.InitMail(Email);
                Random rnd = new Random();
                code = rnd.Next(10000, 99999);
                SendEmailHelper.GenerateRegistrationCodeMessage(FirstName, code);
                SendEmailHelper.Send();
            }
            catch
            { }
        }
        private void ResentVerificationSMS()
        {
            try
            {
                Random rnd = new Random();
                code = rnd.Next(10000, 99999);
                SendSMSHelper.SendMessadeVerification(code);
            }
            catch
            { }
        }
        private void CloseVerification()
        {
            VerificationCode = string.Empty;
            RegistrationVisability = true;
            VerificationVisability = false;
        }
        private void CloseVerificationSMS()
        {
            VerificationSMSCode = string.Empty;
            InfoAccauntVisability = true;
            VerificationSMSVisability = false;
        }

        private void OpenWindow()
        {
            if(code.ToString() == VerificationSMSCode )
            {
                verificate = true;
                Register();
            }
            else
            {
                ErrorVerificationSMSVisability = true;
            }
        }
        private void OpenVerification()
        {
            try
            {
                DatabaseUnit db = new DatabaseUnit();
                user = new User();
                #region Login
                if (UserName.Length == 0)
                {
                    ErrorEmptyUsernameVisibility = true;
                }
                else if (UserName.Length == 0)
                {
                    ErrorValidationUsernameVisibility = true;
                }
                else if (db.Users.GetAll().Where(u => u.Username == UserName).Count() != 0)
                    ErrorUsernameAlreadyExistsVisibility = true;
                else
                    user.Username = UserName;
                #endregion
                #region Email
                if (Email.Length == 0)
                {
                    ErrorEmptyEmailVisibility = true;
                }
                else if (!emailRegex.IsMatch(Email))
                {
                    ErrorValidationEmailVisibility = true;
                }
                else if (db.Users.GetAll().Where(u => u.Email == Email).Count() != 0)
                {
                    ErrorEmailAlreadyExistsVisibility = true;
                }
                else
                {
                    user.Email = Email;
                }
                #endregion
                #region Password
                if (Password.Length == 0 || ConfirmPassword.Length == 0)
                {
                    if (Password.Length == 0)
                        ErrorEmptyPasswordVisibility = true;
                    if (ConfirmPassword.Length == 0)
                        ErrorEmptyConfirmPasswordVisibility = true;
                }
                else if (Password.Length == 0)
                {
                    ErrorIncorrectPasswordVisibility = true;
                }
                else if (ConfirmPassword != Password && Password.Length != 0)
                {
                    ErrorPasswordMismatchVisibility = true;
                }
                else
                {
                    SaltedHash pass = new SaltedHash(Password);
                    user.PasswordHash = pass.Hash;
                    user.Salt = pass.Salt;
                }
                #endregion
                user.RegistrationDate = DateTime.Now;
                user.IsAdmin = false;
                user.Theme = 0;
                var results = new List<System.ComponentModel.DataAnnotations.ValidationResult>();
                var context = new ValidationContext(user);
                if (Validator.TryValidateObject(user, context, results, true))
                {
                    SendEmailHelper.InitMail(Email);
                    Random rnd = new Random();
                    code = rnd.Next(10000, 99999);
                    SendEmailHelper.GenerateRegistrationCodeMessage(UserName, code);
                    SendEmailHelper.Send();
                    RegistrationVisability = false;
                    VerificationVisability = true;
                }
            }
            catch (SmtpException ex)
            {
                string tempHeader = Settings.Lang == Settings.Languages.EN ? "Error Send Email!" : "Ошибка с оправкой почты!";
                var content = new NotificationContent
                {
                    Title = tempHeader,
                    Message = Settings.FindMessage("ErrorSendEmail"),
                    Type = NotificationType.Error,
                };
                notificationManager.Show(content);
            }
            catch(Exception ex)
            {
                string tempHeader = Settings.Lang == Settings.Languages.EN ? "Error!" : "Ошибка";
                var content = new NotificationContent
                {
                    Title = tempHeader,
                    Message = Settings.FindMessage("ErrorEmail"),
                    Type = NotificationType.Error,
                };
                notificationManager.Show(content);
            }
        }
    }
}
