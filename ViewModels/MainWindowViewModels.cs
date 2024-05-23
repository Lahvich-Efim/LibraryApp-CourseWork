using LibraryApp.Model;
using Notification.Wpf;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LibraryApp
{
    public class MainWindowViewModel : PropertyChangedNotification
    {
        private DatabaseUnit db = new DatabaseUnit();
        private static User user;
        public static int id;

        public static NotificationManager notificationManager = new NotificationManager();
        public RelayCommand GoBackCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand OpenMainCommand { get; set; }
        public RelayCommand OpenUserCommand { get; set; }

        public ObservableCollection<Notify> Notifies
        {
            get { return GetValue(() => Notifies); }
            set { SetValue(() => Notifies, value); }
        }

        public bool CanGoBack
        {
            get { return GetValue(() => CanGoBack); }
            set { SetValue(() => CanGoBack, value); }
        }
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

        public MainWindowViewModel()
        {
            if(db.Notifies.GetAll().Where(n => n.IdUser == id).Any(n=> n.IsRead == false))
            {
                foreach (var item in db.Notifies.GetAll().Where(n => n.IdUser == id && n.IsRead == false))
                {
                    var content = new NotificationContent
                    {
                        Title = item.Header,
                        Message = item.Message,
                        Type = NotificationType.None,
                        TrimType = NotificationTextTrimType.Attach,
                        RowsCount = 3,
                        Background = new SolidColorBrush(Colors.White),
                        Foreground = new SolidColorBrush(Colors.Black),
                    };
                    item.IsRead = true;
                    db.Notifies.Update(item);
                    notificationManager.Show(content);
                }
            }

            user = db.Users.Get(id);
            if (user.Pavatar != "")
            {
                SourceImage = Settings.projectPath + "/image/Avatars/" + user.Pavatar;
            }
            else
            {
                SourceImage = "";
            }
            Name = user.Username;
            Page = WindowService.GetPage(WindowService.Pages.MainPage);
            OpenMainCommand = new RelayCommand(OpenMain);
            OpenUserCommand = new RelayCommand(OpenUser);
            //GoBackCommand = new RelayCommand(GoBack);
            Application.Current.Resources.MergedDictionaries.Clear();
            SetResources();
            SwitchLang();
            SwitchTheme();
            SwitchModeTheme();
        }


        //private void CheckCanGoBack()
        //{
        //    CanGoBack = navigationService.CanGoBack;
        //}

        //private void GoBack()
        //{
        //    navigationService.GoBack();
        //    CheckCanGoBack();
        //}

        private void ToRussian()
        {
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
            Settings.Lang = Settings.Languages.RU;
        }

        private void ToEnglish()
        {
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
            Settings.Lang = Settings.Languages.EN;
        }
        private void SwitchLang()
        {
            if (Settings.Languages.EN == Settings.Lang)
                ToEnglish();
            else
                ToRussian();
        }

        private void SwitchTheme()
        {
            if (user.Theme == 0)
            {
                Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryBrown);
            }
            else if (user.Theme == 1)
            {
                Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryLime);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryCyan);
            }
        }
        private void SwitchModeTheme()
        {
            if (user.Mode == 1)
            {
                Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceLight);

            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceDark);

            }
        }


        private void OpenMain()
        {
            WindowService.MainWindow.myFrame.Source = WindowService.GetPage(WindowService.Pages.MainPage);
        }

        private void OpenUser()
        {
            UserPageViewModel.id = id;
            WindowService.MainWindow.myFrame.Source = WindowService.GetPage(WindowService.Pages.UserPage);
        }
        private void SetResources()
        {
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceDefaults);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceStyles);
        }
    }
}
