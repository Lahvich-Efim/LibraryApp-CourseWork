using LibraryApp.Model;
using Microsoft.Identity.Client.NativeInterop;
using Microsoft.Win32;
using Notification.Wpf;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace LibraryApp
{
    public class UserPageViewModel : PropertyChangedNotification
    {
        private DatabaseUnit db = new DatabaseUnit();
        private User user = null;
        public static int id;
        private int idcollection;
        private string textCollection;
        private static readonly Regex emailRegex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", RegexOptions.Compiled);
        private static readonly Regex phoneRegex = new Regex(@"^\+?[1-9]\d{1,14}$", RegexOptions.Compiled);

        public RelayCommand OrderProductCommand { get; set; }
        public RelayCommand ChangeImageCommand { get; set; }
        public RelayCommand EditUserCommand { get; set; }
        public RelayCommand ConfirmEditUserCommand { get; set; }
        public RelayCommand CloseEditUserCommand { get; set; }
        public RelayCommand ConfirmSettingCommand { get; set; }
        public RelayCommand DeleteCollectionCommand { get; set; }
        public RelayCommand EditNameCollectionCommand { get; set; }
        public RelayCommandParam EditCollectionCommand { get; set; }
        public RelayCommand AddCollectionCommand { get; set; }
        public RelayCommand ExitEditCollectionCommand { get; set; }
        public RelayCommandParam DeleteInCollectionCommand { get; set; }

        public RelayCommand OpenNotificationFeedCommand { get; set; }
        public RelayCommand OpenHistoryOrderCommand { get; set; }
        public RelayCommand OpenCartCommand { get; set; }
        public RelayCommand OpenLibraryCommand { get; set; }
        public RelayCommand OpenProfileCommand { get; set; }
        public RelayCommand OpenSettingCommand { get; set; }
        public RelayCommandParam OpenDetailsOrderCommand { get; set; }
        public RelayCommandParam DeleteInBasketCommand { get; set; }
        public RelayCommandParam AddQuantityCommand { get; set; }
        public RelayCommandParam DecreaseQuantityCommand { get; set; }


        public bool EnabledNotifyMode
        {
            get { return GetValue(() => EnabledNotifyMode); }
            set
            {
                SetValue(() => EnabledNotifyMode, value);
            }
        }
        public int SelectNotifyMode
        {
            get { return GetValue(() => SelectNotifyMode); }
            set { SetValue(() => SelectNotifyMode, value); }
        }
        public int SelectTheme
        {
            get { return GetValue(() => SelectTheme); }
            set { SetValue(() => SelectTheme, value); }
        }
        public int SelectLanguage
        {
            get { return GetValue(() => SelectLanguage); }
            set { SetValue(() => SelectLanguage, value); }
        }
        public int SelectMode
        {
            get { return GetValue(() => SelectMode); }
            set { SetValue(() => SelectMode, value); }
        }
        public List<string> Themes
        {
            get { return GetValue(() => Themes); }
            set { SetValue(() => Themes, value); }
        }
        public List<string> Mode
        {
            get { return GetValue(() => Mode); }
            set { SetValue(() => Mode, value); }
        }
        public List<string> NotifyMode
        {
            get { return GetValue(() => NotifyMode); }
            set { SetValue(() => NotifyMode, value); }
        }
        public List<string> Language
        {
            get { return GetValue(() => Language); }
            set { SetValue(() => Language, value); }
        }
        public ObservableCollection<Notify> NotifyFeed
        {
            get { return GetValue(() => NotifyFeed); }
            set { SetValue(() => NotifyFeed, value); }
        }
        public ObservableCollection<object> Library
        {
            get { return GetValue(() => Library); }
            set { SetValue(() => Library, value); }
        }
        public int IdSelectOrder
        {
            get { return GetValue(() => IdSelectOrder); }
            set { SetValue(() => IdSelectOrder, value); }
        }
        public bool EditUserVisibility
        {
            get { return GetValue(() => EditUserVisibility); }
            set { SetValue(() => EditUserVisibility, value); }
        }

        public ObservableCollection<ProductState> Basket
        {
            get { return GetValue(() => Basket); }
            set { SetValue(() => Basket, value); }
        }
        public ObservableCollection<OrderState> Orders
        {
            get { return GetValue(() => Orders); }
            set { SetValue(() => Orders, value); }
        }
        public bool SettingVisibility
        {
            get { return GetValue(() => SettingVisibility); }
            set { SetValue(() => SettingVisibility, value); }
        }
        public bool HistoryOrderVisibility
        {
            get { return GetValue(() => HistoryOrderVisibility); }
            set { SetValue(() => HistoryOrderVisibility, value); }
        }
        public bool CartVisibility
        {
            get { return GetValue(() => CartVisibility); }
            set { SetValue(() => CartVisibility, value); }
        }
        public bool LibraryVisibility
        {
            get { return GetValue(() => LibraryVisibility); }
            set { SetValue(() => LibraryVisibility, value); }
        }
        public bool ProfileVisibility
        {
            get { return GetValue(() => ProfileVisibility); }
            set { SetValue(() => ProfileVisibility, value); }
        }

        public bool NotificationFeedVisibility
        {
            get { return GetValue(() => NotificationFeedVisibility); }
            set { SetValue(() => NotificationFeedVisibility, value); }
        }

        public bool DetailsOrderVisibility
        {
            get { return GetValue(() => DetailsOrderVisibility); }
            set { SetValue(() => DetailsOrderVisibility, value); }
        }

        public string UserName
        {
            get { return GetValue(() => UserName); }
            set { SetValue(() => UserName, value); }
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
            }
        }
        public string Phone
        {
            get { return GetValue(() => Phone); }
            set
            {
                SetValue(() => Phone, value);
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

        public string RegistrationDate
        {
            get { return GetValue(() => RegistrationDate); }
            set
            {
                SetValue(() => RegistrationDate, value);
            }
        }
        public string SourceImage
        {
            get { return GetValue(() => SourceImage); }
            set { SetValue(() => SourceImage, value); }
        }
        public UserPageViewModel()
        {
            ConfirmSettingCommand = new RelayCommand(ConfirmSetting);
            OpenHistoryOrderCommand = new RelayCommand(OpenHistoryOrder);
            OpenCartCommand = new RelayCommand(OpenCart);
            OpenLibraryCommand = new RelayCommand(OpenLibrary);
            OpenProfileCommand = new RelayCommand(OpenProfile);
            OpenNotificationFeedCommand = new RelayCommand(OpenNotificationFeed);
            OpenSettingCommand = new RelayCommand(OpenSetting);
            ExitEditCollectionCommand = new RelayCommand(ExitEditCollection);
            ChangeImageCommand = new RelayCommand(ChangeImage);
            EditCollectionCommand = new RelayCommandParam((s) =>
            {
                idcollection = Int32.Parse((s as string).Replace("coll", ""));
                var temp = (Library.Where(c => c is CollectionState).FirstOrDefault(c => (c as CollectionState).CollectionId == idcollection) as CollectionState);
                temp.EditCollection = true;
                textCollection = temp.Name;
            }
            );
            AddCollectionCommand = new RelayCommand(AddCollection);
            EditNameCollectionCommand = new RelayCommand(EditNameCollection);
            OpenDetailsOrderCommand = new RelayCommandParam(s => OpenDetailsOrder(s as string));
            DeleteInBasketCommand = new RelayCommandParam(s => DeleteInBasket(s as string));
            DeleteCollectionCommand = new RelayCommand(DeleteCollection);
            DeleteInCollectionCommand = new RelayCommandParam(s => DeleteInCollection(s as string));
            EditUserCommand = new RelayCommand(EditUser);
            ConfirmEditUserCommand = new RelayCommand(ConfirmEditUser);
            CloseEditUserCommand = new RelayCommand(CloseEditUser);
            OrderProductCommand = new RelayCommand(OrderProduct, () => Basket.Count > 0);
            Settings.changeLang += ShowOrders;
            Settings.changeLang += ShowBaskets;
            Settings.changeLang += ShowLibrary;
            NotifyFeed = [.. db.Notifies.GetAll().Where(n => n.IdUser == id).OrderByDescending(n => n.DataNotify).Take(10)];
            ShowPage();
            user = db.Users.Get(id);
            if (user.Pavatar != "")
            {
                SourceImage = Settings.projectPath + "/image/Avatars/" + user.Pavatar;
            }
            else
            {
                SourceImage = user.Pavatar;
            }
            switch(Settings.Lang)
            {
                case Settings.Languages.EN:

                    Themes = new List<string>() { "Brown", "Lime", "Turquoise" };
                    Language = new List<string>() { "Russian", "English" };
                    Mode = new List<string>() { "Dark", "Light" };
                    NotifyMode = new List<string>() { "Email", "Telephone" };
                    break;
                case Settings.Languages.RU:
                    Themes = new List<string>() { "Коричневый", "Лаймовый", "Бирюзовый" };
                    Language = new List<string>() { "Русский", "Английский" };
                    Mode = new List<string>() { "Темная", "Светлая" };
                    NotifyMode = new List<string>() { "Почта", "Телефон" };
                    break;
            }
            UserName = user.Username;
            Address = user.Address;
            Phone = user.Phone;
            FirstName = user.FirstName;
            LastName = user.LastName;
            FatherName = user.FatherName;
            Email = user.Email;
            RegistrationDate = user.RegistrationDate.ToString();
            SelectTheme = user.Theme;
            SelectMode = user.Mode;
            SelectNotifyMode = user.ModeNotify;
            SelectLanguage = (int)Settings.Lang;
            SettingVisibility = false;
            HistoryOrderVisibility = false;
            CartVisibility = false;
            LibraryVisibility = false;
            DetailsOrderVisibility = false;
            ProfileVisibility = true;
            NotificationFeedVisibility = false;
            EditUserVisibility = false;
            EnabledNotifyMode = Phone == "" ? false : true;
        }

        private void OrderProduct()
        {
            OrderInfo order = new OrderInfo();
            var basket = db.Baskets.Get(id);
            order.UserId = id;
            order.Status = 0;
            order.IsActiv = true;
            order.DateOrder = DateTime.Now;
            order.DateEnd = order.DateOrder.AddDays(30);
            db.OrderInfos.Add(order);
            foreach (var item in Basket)
            {
                Product product = db.Products.Get(item.Id);
                order.ProductCodes.Add(product);
                basket.ProductCodes.Remove(basket.ProductCodes.FirstOrDefault(p=> p.ProductCode == product.ProductCode));
            }
            db.OrderInfos.Update(order);
            db.Baskets.Update(basket);
            Basket.Clear();
        }
        private void ConfirmEditUser()
        {
            try
            {
                if (Email != user.Email)
                {
                    if (Email != "" && emailRegex.IsMatch(Email))
                    {
                        int code = new Random().Next(10000, 99999);
                        SendEmailHelper.InitMail(Email);
                        SendEmailHelper.GenerateChangeEmailMessage(UserName, code);
                        WindowService.OpenWindow(WindowService.Windows.UserEnter, new UserEnterViewModel("EnterCode", code, Email));
                        if (UserEnterViewModel.DialogResult)
                        {
                            user.Email = Email;
                        }
                        else
                        {
                            Email = user.Email;
                        }
                    }
                    else
                    {
                        throw new FormatException("");
                    }
                }
                if (Phone != user.Phone)
                {
                    if (Phone != "" && phoneRegex.IsMatch(Phone))
                    {
                        Random rnd = new Random();
                        int code = rnd.Next(10000, 99999);
                        SendSMSHelper.SetToUser(Phone, user);
                        SendSMSHelper.SendMessadeVerification(code);
                        WindowService.OpenWindow(WindowService.Windows.UserEnter, new UserEnterViewModel("EnterCodeSMS", code, Email));
                        if (UserEnterViewModel.DialogResult)
                        {
                            user.Phone = Phone;
                        }
                        else
                        {
                            Phone = user.Phone;
                        }
                    }
                    else if(Phone != "")
                    {
                        throw new FormatException("");
                    }
                }
                if (UserName != user.Username)
                    user.Username = UserName;
                if (Address != user.Address)
                    user.Address = Address;
                if (FirstName != user.FirstName)
                    user.FirstName = FirstName;
                if (LastName != user.LastName)
                    user.LastName = LastName;
                if (FatherName != user.FatherName)
                    user.FatherName = FatherName;
                if (SourceImage != Settings.projectPath + "/image/Avatars/" + user.Pavatar || SourceImage == "")
                {
                    (WindowService.MainWindow.DataContext as MainWindowViewModel).SourceImage = null;
                    string[] str = SourceImage.Split('\\');
                    string[] extensions = str[str.Length - 1].Split('.');
                    string extension = extensions[extensions.Length - 1];
                    var temp = Settings.projectPath + "\\image\\Avatars\\" + user.Pavatar;
                    if (File.Exists(temp))
                    {
                        try
                        {
                            File.Delete(temp);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception($"Ошибка при удалении старого файла");
                        }
                    }
                    user.Pavatar = user.UserId + "." + extension;
                    File.Copy(SourceImage, Settings.projectPath + "\\image\\Avatars\\" + user.Pavatar, true);
                    (WindowService.MainWindow.DataContext as MainWindowViewModel).SourceImage = SourceImage;
                }
                EditUserVisibility = false;
                db.Users.Update(user);
                
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
                MainWindowViewModel.notificationManager.Show(content);
            }
            catch (FormatException ex)
            {

            }
            catch (Exception ex)
            {
                string tempHeader = Settings.Lang == Settings.Languages.EN ? "Error!" : "Ошибка";
                var content = new NotificationContent
                {
                    Title = tempHeader,
                    Message = ex.Message == "" ? Settings.FindMessage("Error") : ex.Message,
                    Type = NotificationType.Error,
                };
                MainWindowViewModel.notificationManager.Show(content);
            }
        }
        private void CloseEditUser()
        {
            EditUserVisibility = false;
            UserName = user.Username;
            Address = user.Address;
            Phone = user.Phone;
            FirstName = user.FirstName;
            LastName = user.LastName;
            FatherName = user.FatherName;
            Email = user.Email;
            SourceImage = user.Pavatar == "" ? "" : Settings.projectPath + "/image/Avatars/" + user.Pavatar;
        }

        private void ChangeImage()
        {
            var filePicker = new OpenFileDialog();

            filePicker.DefaultExt = ".jpg";
            filePicker.Filter = "JPG Files (*.jpg)|*.jpg|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png";

            bool? result = filePicker.ShowDialog();

            if (result == true)
            {
                SourceImage = filePicker.FileName;
            }
        }
        private void EditUser()
        {
            EditUserVisibility = true;
        }

        private void ConfirmSetting()
        {
            Application.Current.Resources.MergedDictionaries.Clear();   
            SetResources();
            int temp1 = SelectMode, temp2 = SelectNotifyMode, temp3 = SelectTheme, temp4 = SelectLanguage;
            switch (SelectTheme)
            {
                case 0:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryBrown);
                    break;
                case 1:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryLime);
                    break;
                case 2:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourcePrimaryCyan);
                    break;
                default:
                    break;
            }

            switch (SelectMode)
            {
                case 0:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceDark);
                    break;
                case 1  :
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceLight);
                    break;
                default:
                    break;
            }
            switch (SelectLanguage)
            {
                case 0:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceRusLang);
                    Settings.Lang = Settings.Languages.RU;
                    Themes = new List<string>() { "Коричневый", "Лаймовый", "Бирюзовый" };
                    Language = new List<string>() { "Русский", "Английский" };
                    Mode = new List<string>() { "Темная", "Светлая" };
                    NotifyMode = new List<string>() { "Почта", "Телефон" };
                    break;
                case 1:
                    Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceEnLang);
                    Settings.Lang = Settings.Languages.EN;
                    Themes = new List<string>() { "Brown", "Lime", "Turquoise" };
                    Language = new List<string>() { "Russian", "English" };
                    Mode = new List<string>() { "Dark", "Light" };
                    NotifyMode = new List<string>() { "Email", "Telephone" };
                    break;
                default:
                    break;
            }
            SelectMode = temp1;
            SelectTheme = temp3;
            SelectNotifyMode = temp2;
            SelectLanguage = temp4;
            user.Mode = (short)SelectMode;
            user.Theme = (short)SelectTheme;
            user.ModeNotify = (short)SelectNotifyMode;
            db.Users.Update(user);
        }

        private void SetResources()
        {
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceDefaults);
            Application.Current.Resources.MergedDictionaries.Add(Settings.ResourceStyles);
        }
        private void DeleteInCollection(string product)
        {
            string[] par = product.Split('_');
            int collid = Int32.Parse(par[0].Replace("btn", ""));
            int idProduct = Int32.Parse(par[1]);
            var temp = db.Collections.Get(collid);
            temp.ProductCodes.Remove(temp.ProductCodes.FirstOrDefault(p => p.ProductCode == idProduct));
            db.Collections.Update(temp);
            var collection = (Library.Where(c => c is CollectionState).FirstOrDefault(c => (c as CollectionState).CollectionId == collid) as CollectionState);
            collection.Products.Remove(collection.Products.FirstOrDefault(p => p.Id == idProduct));
            ShowLibrary();
        }

        private void ExitEditCollection()
        {
            var temp = (Library.Where(c => c is CollectionState).FirstOrDefault(c => (c as CollectionState).CollectionId == idcollection) as CollectionState);
            temp.Name = textCollection;
            temp.EditCollection = false;
            idcollection = -1;
        }

        private void EditNameCollection()
        {
            var temp = db.Collections.Get(idcollection);
            var temp2 = (Library.Where(c => c is CollectionState).FirstOrDefault(c => (c as CollectionState).CollectionId == idcollection) as CollectionState);
            temp.Name = temp2.Name;
            db.Collections.Update(temp);
            temp2.EditCollection = false;
            idcollection = -1;
        }

        private void DeleteCollection()
        {
            Library.Remove(Library.Where(c => c is CollectionState).FirstOrDefault(c => (c as CollectionState).CollectionId == idcollection));
            db.Collections.Delete(idcollection);
            idcollection = -1;
        }

        private void AddCollection()
        {
            CollectionState collectionstate = new CollectionState();
            collectionstate.Name = "Введите название";
            Collection collection = new Collection();
            collection.Name = collectionstate.Name;
            collection.LibraryId = db.Libraries.Get(id).LibraryId;
            collection.IsDefault = false;
            db.Collections.Add(collection);
            collectionstate.CollectionId = collection.CollectionId;
            collectionstate.LibraryId = collection.LibraryId;
            Library.Insert(Library.Count - 1, collectionstate);
        }

        private void ShowLibrary()
        {
            Library = new ObservableCollection<object>();
            foreach (var item in db.Collections.GetAll().Where(c => c.LibraryId == db.Libraries.Get(id).LibraryId).Where(c => (bool)c.IsDefault))
            {
                CollectionDefault collection = new CollectionDefault();
                collection.Name = item.Name;
                collection.Products = new ObservableCollection<ProductState>();
                foreach (var item2 in item.ProductCodes)
                {
                    ProductState product = new ProductState();
                    product.Id = item2.ProductCode;
                    product.CollectionId = item.CollectionId;
                    product.Author = db.Dictionaries.Get(db.Authors.Get((int)item2.AuthorId).Palias).WordRus;
                    product.Name = db.Dictionaries.Get(item2.Pname).WordRus;
                    product.Image = Settings.projectPath + "/image/Products/" + item2.Pimage;
                    product.Quantity = item2.Quantity;
                    var review = db.Reviews.GetAll().Where(r => r.ProductCode == item2.ProductCode);
                    if (review.Any())
                    {
                        product.Rating = Math.Round(review.Average(r => r.Rating), 1);
                    }
                    else
                    {
                        product.Rating = 0;
                    }
                    collection.Products.Add(product);
                }
                Library.Add(collection);
            }
            foreach (var item in db.Collections.GetAll().Where(c => c.LibraryId == db.Libraries.Get(id).LibraryId).Where(c => !(bool)c.IsDefault))
            {
                CollectionState collection = new CollectionState();
                collection.Name = item.Name;
                collection.LibraryId = item.LibraryId;
                collection.CollectionId = item.CollectionId;
                collection.Products = new ObservableCollection<ProductState>();
                foreach (var item2 in item.ProductCodes)
                {
                    ProductState product = new ProductState();
                    product.Id = item2.ProductCode;
                    product.CollectionId = item.CollectionId;
                    product.Author = db.Dictionaries.Get(db.Authors.Get((int)item2.AuthorId).Palias).WordRus;
                    product.Name = db.Dictionaries.Get(item2.Pname).WordRus;
                    product.Image = Settings.projectPath + "/image/Products/" + item2.Pimage;
                    product.Quantity = item2.Quantity;
                    var review = db.Reviews.GetAll().Where(r => r.ProductCode == item2.ProductCode);
                    if (review.Any())
                    {
                        product.Rating = Math.Round(review.Average(r => r.Rating), 1);
                    }
                    else
                    {
                        product.Rating = 0;
                    }
                    collection.Products.Add(product);
                }
                Library.Add(collection);
            }
            TabItem tab = new TabItem();

            Button button = new Button();
            button.Content = "+";
            button.Command = AddCollectionCommand;
            tab.Header = button;

            Library.Add(tab);
        }
        public void ShowPage()
        {
            ShowOrders();
            ShowBaskets();
            ShowLibrary();
        }

        public void ShowOrders()
        {
            Orders = new ObservableCollection<OrderState>();
            foreach (var item in db.OrderInfos.GetAll().Where(o => o.UserId == id))
            {
                OrderState order = new OrderState();
                order.Id = item.OrderId;
                order.DateOrder = item.DateOrder.ToShortDateString();
                order.CurrentOrderStatus = (OrderStatus)item.Status;
                var date = item.DateEnd.Date.Subtract(item.DateOrder.Date).Days;
                if (date == 1)
                    order.DateEnd = date.ToString() + " день";
                else if (date == 2)
                    order.DateEnd = date.ToString() + " дня";
                else
                    order.DateEnd = date.ToString() + " дней";
                order.IsActiv = item.IsActiv ? "Активно" : "Неактивно";
                order.Count = 0;
                order.ProductItem = new ObservableCollection<ProductState>();
                foreach (var items in db.OrderInfos.Get(order.Id).ProductCodes)
                {
                    order.Count++;
                    ProductState Item = new ProductState();
                    Item = new ProductState();
                    Item.Id = items.ProductCode;
                    Item.Author = db.Dictionaries.Get(db.Authors.Get((int)items.AuthorId).Palias).WordRus;
                    Item.Name = db.Dictionaries.Get(items.Pname).WordRus;
                    Item.Image = Settings.projectPath + "/image/Products/" + items.Pimage;
                    if (db.Reviews.GetAll().Where(r => r.ProductCode == items.ProductCode).Any())
                    {
                        Item.Rating = Math.Round(db.Reviews.GetAll().Where(r => r.ProductCode == items.ProductCode).Average(r => r.Rating), 1);
                    }
                    else
                    {
                        Item.Rating = 0;
                    }
                    order.ProductItem.Add(Item);
                }
                Orders.Add(order);
            }

        }

        public void ShowBaskets()
        {
            Basket = new ObservableCollection<ProductState>();
            foreach (var item in db.Baskets.Get(id).ProductCodes)
            {
                ProductState product = new ProductState();
                product.Id = item.ProductCode;
                product.Author = db.Dictionaries.Get(db.Authors.Get((int)item.AuthorId).Palias).WordRus;
                product.Name = db.Dictionaries.Get(item.Pname).WordRus;
                product.Image = Settings.projectPath + "/image/Products/" + item.Pimage;
                product.Quantity = item.Quantity;
                var review = db.Reviews.GetAll().Where(r => r.ProductCode == item.ProductCode);
                if (review.Any())
                {
                    product.Rating = Math.Round(review.Average(r => r.Rating), 1);
                }
                else
                {
                    product.Rating = 0;
                }
                Basket.Add(product);
            }
        }

        private void OpenHistoryOrder()
        {
            SettingVisibility = false;
            HistoryOrderVisibility = true;
            CartVisibility = false;
            LibraryVisibility = false;
            ProfileVisibility = false;
            NotificationFeedVisibility = false;
            DetailsOrderVisibility = false;
        }
        private void OpenCart()
        {
            SettingVisibility = false;
            HistoryOrderVisibility = false;
            CartVisibility = true;
            LibraryVisibility = false;
            ProfileVisibility = false;
            NotificationFeedVisibility = false;
            DetailsOrderVisibility = false;
        }
        private void OpenLibrary()
        {

            SettingVisibility = false;
            HistoryOrderVisibility = false;
            CartVisibility = false;
            LibraryVisibility = true;
            ProfileVisibility = false;
            NotificationFeedVisibility = false;
            DetailsOrderVisibility = false;
        }

        private void OpenProfile()
        {
            SettingVisibility = false;
            HistoryOrderVisibility = false;
            CartVisibility = false;
            LibraryVisibility = false;
            ProfileVisibility = true;
            NotificationFeedVisibility = false;
            DetailsOrderVisibility = false;
        }
        private void OpenNotificationFeed()
        {
            SettingVisibility = false;
            HistoryOrderVisibility = false;
            CartVisibility = false;
            LibraryVisibility = false;
            ProfileVisibility = false;
            NotificationFeedVisibility = true;
            DetailsOrderVisibility = false;
        }
        private void OpenSetting()
        {
            SettingVisibility = true;
            HistoryOrderVisibility = false;
            CartVisibility = false;
            LibraryVisibility = false;
            ProfileVisibility = false;
            NotificationFeedVisibility = false;
            DetailsOrderVisibility = false;
        }

        private void OpenDetailsOrder(string order)
        {
            
        }
        private void DeleteInBasket(string product)
        {
            int idProduct = Int32.Parse(product.Replace("btn", ""));
            MessageBoxResult result = MessageBox.Show("Вы точно хотите убрать?",
                                          "Confirmation",
                                          MessageBoxButton.YesNo,
                                          MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var temp = db.Baskets.Get(id);
                temp.ProductCodes.Remove(temp.ProductCodes.FirstOrDefault(product => product.ProductCode == idProduct));
                db.Baskets.Update(temp);
                var temp2 = db.Products.Get(idProduct);
                temp2.Quantity += 1;
                db.Products.Update(temp2);
                ShowBaskets();
            }
        }
    }
}
