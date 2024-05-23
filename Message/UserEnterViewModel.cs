using LibraryApp.Model;
using System.Net.Mail;
using System.Windows.Input;

namespace LibraryApp
{
    public class UserEnterViewModel : PropertyChangedNotification
    {
        int code;
        public static bool DialogResult;
        public static int userId;
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand CheckCommand { get; set; }

        public string ImageSource
        {
            get { return GetValue(() => ImageSource); }
            set { SetValue(() => ImageSource, value); }
        }
        public string Message
        {
            get { return GetValue(() => Message); }
            set { SetValue(() => Message, value); }
        }
        public string UserMessage
        {
            get { return GetValue(() => UserMessage); }
            set { SetValue(() => UserMessage, value);
                ErrorMessage = "";
            }
        }
        public Cursor Cursor
        {
            get { return GetValue(() => Cursor); }
            set { SetValue(() => Cursor, value); }
        }
        public string ErrorMessage
        {
            get { return GetValue(() => ErrorMessage); }
            set { SetValue(() => ErrorMessage, value); }
        }
        public UserEnterViewModel(string message, int code, string Info = "")
        {
            DialogResult = false;
            userId = -1;
            this.code = code;
            UserMessage = "";
            CloseCommand = new RelayCommand(Close);
            CheckCommand = new RelayCommand(Check);
            switch (message)
            {
                case "EnterCode":
                    Message = Settings.FindMessage("EnterCode") + " " + Info ?? "";
                    break;
                case "EnterCodeSMS":
                    Message = Settings.FindMessage("EnterCodeSMS") + " " + Info ?? "";
                    break;
                default:
                    Message = Settings.FindMessage(message);
                    break;
            }
        }
        private void Close()
        {
            WindowService.CloseWindow(WindowService.Windows.UserEnter);
        }
        private void Check()
        {
            try
            {
                    int userCode;
                    try
                    {
                        userCode = int.Parse(UserMessage);
                    }
                    catch (FormatException)
                    {
                        throw new ArgumentException("ErrorVerificationCode");
                    }

                    if (userCode == code)
                    {
                        DialogResult = true;
                        Close();
                    }
                    else
                        throw new Exception("ErrorVerificationCode");
            }
            catch (Exception ex)
            {
                ErrorMessage = Settings.FindMessage(ex.Message);
            }
        }
    }
}
