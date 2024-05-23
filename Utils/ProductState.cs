namespace LibraryApp
{
    public class ProductState : PropertyChangedNotification
    {
        public ProductState(int id, string image, string name, string price, string author, int rating)
        {
            Id = id;
            Image = image;
            Name = name;
            Author = author;
            Rating = rating;
        }
        public ProductState(int id, string image, string name, string price, string author, bool active, int rating)
        {
            Id = id;
            Image = image;
            Name = name;
            Author = author;
            IsActive = active;
            Rating = rating;
        }
        public ProductState() { }




        public int CollectionId
        {
            get { return GetValue(() => CollectionId); }
            set { SetValue(() => CollectionId, value); }
        }
        public bool InCart
        {
            get { return GetValue(() => InCart); }
            set { SetValue(() => InCart, value); }
        }
        public int Id
        {
            get { return GetValue(() => Id); }
            set { SetValue(() => Id, value); }
        }
        public int CatalogId
        {
            get { return GetValue(() => CatalogId); }
            set { SetValue(() => CatalogId, value); }
        }
        public string Image
        {
            get { return GetValue(() => Image); }
            set { SetValue(() => Image, value); }
        }
        public string Name
        {
            get { return GetValue(() => Name); }
            set { SetValue(() => Name, value); }

        }
        public string Author
        {
            get { return GetValue(() => Author); }
            set { SetValue(() => Author, value); }

        }
        public double Rating
        {
            get { return GetValue(() => Rating); }
            set { SetValue(() => Rating, value); }

        }
        public bool IsActive
        {
            get { return GetValue(() => IsActive); }
            set { SetValue(() => IsActive, value); }
        }
        public int Quantity
        {
            get { return GetValue(() => Quantity); }
            set { SetValue(() => Quantity, value); }
        }
        public int Year
        {
            get { return GetValue(() => Year); }
            set { SetValue(() => Year, value); }
        }

        public int NumberPages
        {
            get { return GetValue(() => NumberPages); }
            set { SetValue(() => NumberPages, value); }
        }
        public string TypeBook
        {
            get { return GetValue(() => TypeBook); }
            set { SetValue(() => TypeBook, value); }
        }

        public string Publisher
        {
            get { return GetValue(() => Publisher); }
            set { SetValue(() => Publisher, value); }
        }

    }
}
