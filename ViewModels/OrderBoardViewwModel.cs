using LibraryApp;
using LibraryApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.ViewModels
{
    public class OrderBoardViewModel : PropertyChangedNotification
    {
        DatabaseUnit db = new DatabaseUnit();

        public RelayCommandParam SelectOrderCommand { get; set; }
        public RelayCommand EditStatusCommand { get; set; }
        public int SelectOrderId { get; set; }

        public ObservableCollection<OrderState> Orders
        {
            get { return GetValue(() => Orders); }
            set { SetValue(() => Orders, value); }
        }
        public ObservableCollection<string> ListStatus
        {
            get { return GetValue(() => ListStatus); }
            set { SetValue(() => ListStatus, value); }

        }

        public int SelectStatus
        {
            get { return GetValue(() => SelectStatus); }
            set { SetValue(() => SelectStatus, value); }

        }
        public OrderBoardViewModel()
        {
            SelectOrderId = -1;
            SelectStatus = -1;
            EditStatusCommand = new RelayCommand(EditStatus, () => SelectOrderId != -1);
            SelectOrderCommand = new RelayCommandParam((s) => SelectOrder(s as string));
            if (Settings.Lang == Settings.Languages.EN)
            {
                ListStatus = new ObservableCollection<string>() { "Waiting", "Collected", "Received", "Completed", "Cancelled" };
            }
            else
            {
                ListStatus = new ObservableCollection<string>() { "Ожидание", "Собрано", "Получено", "Выполнено", "Отказано" };
            }
            Orders = new ObservableCollection<OrderState>();
            foreach (var order in db.OrderInfos.GetAll().Where(o => o.IsActiv)) 
            {

                OrderState state = new OrderState();
                state.Id = order.OrderId;
                state.User = db.Users.Get(order.UserId);
                state.User.Pavatar = Orders.Where(user => user.User.UserId == state.User.UserId).Count() > 0 ? Settings.projectPath + "\\image\\Avatars\\" + state.User.Pavatar : state.User.Pavatar;
                state.CurrentOrderStatus = (OrderStatus)order.Status;
                state.DateOrder = order.DateOrder.ToShortDateString();
                var date = order.DateEnd.Date.Subtract(order.DateOrder.Date).Days;
                switch(Settings.Lang)
                {
                    case Settings.Languages.EN:
                        state.DateEnd = date.ToString() + " day";
                        break;
                    case Settings.Languages.RU:

                        if (date == 1)
                            state.DateEnd = date.ToString() + " день";
                        else if (date == 2)
                            state.DateEnd = date.ToString() + " дня";
                        else
                            state.DateEnd = date.ToString() + " дней";
                        break;
                }

                state.ProductItem = new ObservableCollection<ProductState>();
                foreach(var product in order.ProductCodes)
                {
                    ProductState prod = new ProductState();
                    prod.Id = product.ProductCode;
                    prod.Image = Settings.projectPath + "\\image\\Products\\" + product.Pimage;
                    var reviews = db.Reviews.GetAll().Where(r => r.ProductCode == prod.Id);
                    if (reviews.Count() != 0)
                    {
                        prod.Rating = Math.Round(reviews.Average(r => r.Rating), 1);
                    }
                    else
                    {
                        prod.Rating = 0;
                    }
                    switch(Settings.Lang)
                    {
                        case Settings.Languages.RU:
                            prod.Name = db.Dictionaries.Get(product.Pname).WordRus;
                            prod.Author = db.Dictionaries.Get(db.Authors.Get(product.AuthorId).Palias).WordRus;
                            break;
                        case Settings.Languages.EN:
                            prod.Name = db.Dictionaries.Get(product.Pname).WordEn;
                            prod.Author = db.Dictionaries.Get(db.Authors.Get(product.AuthorId).Palias).WordEn;
                            break;
                    }
                    state.ProductItem.Add(prod);
                }
                Orders.Add(state);
            }
        }

        private void SelectOrder(string order)
        {
            SelectOrderId = int.Parse(order.Replace("ord", ""));
        }

        private void EditStatus()
        {
            var orders = db.OrderInfos.Get(SelectOrderId);
            orders.Status = (byte)SelectStatus;
            db.OrderInfos.Update(orders);
            var temp = Orders.FirstOrDefault(p => p.Id == SelectOrderId);
            temp.CurrentOrderStatus = (OrderStatus)SelectStatus;
            if (ListStatus[SelectStatus] == "Completed" || ListStatus[SelectStatus] == "Выполнено")
            {
                string firstname = db.Users.Get(temp.User.UserId).FirstName;
                string Nickname = db.Users.Get(temp.User.UserId).FirstName;
                var result = firstname != "" ? firstname : Nickname;
                string header = "";
                string body = "";
                switch (Settings.Lang)
                {
                    case Settings.Languages.RU:
                        header = "Ваш заказ готов: Книга готова к выдаче";
                        body = $@"
Уважаемый(ая) {result},

Ваш заказ {temp.Id} на аренду книги выполнен и готов к выдаче. Пожалуйста, посетите нашу библиотеку для получения книги.

Мы рады, что вы выбрали нашу библиотеку, и надеемся, что книга принесет вам удовольствие.

Если у вас есть вопросы или вам нужна дополнительная информация, пожалуйста, свяжитесь с нами по по электронной почте libraryappsup@gmail.com.

С уважением, LibraryApp";

                        break;

                    case Settings.Languages.EN:
                        header = "Your Order is Ready: The Book is Ready for Pickup";
                        body = $@"
Dear {result},

Your order {temp.Id} for renting the book has been completed and is ready for pickup. Please visit our library to collect your book.

We are pleased that you chose our library and hope you enjoy the book.

If you have any questions or need further information, please contact us via email at libraryappsup@gmail.com.

Sincerely, LibraryApp";
                        break;
                }

                Notify notify = new Notify();
                notify.Header = header;
                notify.Message = body;
                notify.DataNotify = DateTime.Now;
                notify.IdUser = temp.User.UserId;
                notify.IsRead = false;
                db.Notifies.Add(notify);
    }
            var tempList = new ObservableCollection<string>([.. ListStatus]);
            for (int i = 0; 0 < SelectStatus; i++)
                tempList.RemoveAt(0);
            ListStatus = tempList;


        }
    }
}
