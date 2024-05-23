using LibraryApp.Model;
using System.Collections.ObjectModel;

namespace LibraryApp
{
    public class OrderState : PropertyChangedNotification
    {
        public OrderState(int id, string dateOrder, string dateEnd, int status, string activ)
        {
            Id = id;
            DateOrder = dateOrder;
            DateEnd = dateEnd;
            CurrentOrderStatus = (OrderStatus)status;
            IsActiv = activ;
        }
        public OrderState() { }
        public User User
        {
            get { return GetValue(() => User); }
            set { SetValue(() => User, value); }
        }

        public ObservableCollection<ProductState> ProductItem
        {
            get { return GetValue(() => ProductItem); }
            set { SetValue(() => ProductItem, value); }
        }

        public int Count
        {
            get { return GetValue(() => Count); }
            set { SetValue(() => Count, value); }
        }
        
        public int Id
        {
            get { return GetValue(() => Id); }
            set { SetValue(() => Id, value); }
        }
        public string DateOrder
        {
            get { return GetValue(() => DateOrder); }
            set { SetValue(() => DateOrder, value); }
        }
        public string DateEnd
        {
            get { return GetValue(() => DateEnd); }
            set { SetValue(() => DateEnd, value); }
        }
        public OrderStatus CurrentOrderStatus
        {
            get => GetValue(() => CurrentOrderStatus);
            set
            {
                if (!EqualityComparer<OrderStatus>.Default.Equals(CurrentOrderStatus, value))
                {
                    SetValue(() => CurrentOrderStatus, value);
                    NotifyPropertyChanged(() => CurrentOrderStatus);
                    NotifyPropertyChanged(() => CurrentOrderStatusDescription);
                }
            }
        }
        public string CurrentOrderStatusDescription => OrderStatusHelper.GetStatusDescription(CurrentOrderStatus);
        public string IsActiv
        {
            get { return GetValue(() => IsActiv); }
            set { SetValue(() => IsActiv, value); }
        }
    }
}
