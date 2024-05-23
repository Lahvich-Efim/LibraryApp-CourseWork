using System.Collections.ObjectModel;

namespace LibraryApp
{
    public class CollectionState : PropertyChangedNotification
    {
        public int CollectionId
        {
            get { return GetValue(() => CollectionId); }
            set { SetValue(() => CollectionId, value); }
        }
        public string Name
        {
            get { return GetValue(() => Name); }
            set { SetValue(() => Name, value); }
        }
        public int LibraryId { get; set; }
        public bool IsChecked
        {
            get { return GetValue(() => IsChecked); }
            set { SetValue(() => IsChecked, value); }
        }

        public ObservableCollection<ProductState> Products
        {
            get { return GetValue(() => Products); }
            set { SetValue(() => Products, value); }
        }
        public bool EditCollection
        {
            get { return GetValue(() => EditCollection); }
            set { SetValue(() => EditCollection, value); }
        }



    }

    public class CollectionDefault : PropertyChangedNotification
    {
        public string Icon
        {
            get { return GetValue(() => Icon); }
            set { SetValue(() => Icon, value); }
        }
        public int CollectionId
        {
            get { return GetValue(() => CollectionId); }
            set { SetValue(() => CollectionId, value); }
        }
        public int LibraryId { get; set; }
        public string Name { get; set; } = null!;
        public ObservableCollection<ProductState> Products
        {
            get { return GetValue(() => Products); }
            set { SetValue(() => Products, value); }
        }
        public bool IsChecked
        {
            get { return GetValue(() => IsChecked); }
            set { SetValue(() => IsChecked, value); }
        }
    }
}
