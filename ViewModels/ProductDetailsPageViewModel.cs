using LibraryApp.Model;
using LibraryApp.Utils;
using System.Collections.ObjectModel;

namespace LibraryApp
{
    public class ProductDetailsPageViewModel : PropertyChangedNotification
    {
        DatabaseUnit db = new DatabaseUnit();
        private static int _productId = -1;
        private static event Action SetProductState;


        public RelayCommand ChangeBasketCommand { get; set; }
        public RelayCommandParam ChangeSelectedCollectionCommand { get; set; }
        public RelayCommand SendReviewCommand { get; set; }
        public RelayCommand AddRewiewCommand { get; set; }
        public RelayCommand EditInBasketCommand { get; set; }

        public static int ProductId
        {
            get { return _productId; }
            set
            {
                _productId = value;
                SetProductState?.Invoke();
            }
        }


        public bool HasReviews
        {
            get { return Reviews.Any(); }
        }
        public bool ButtonReview
        {
            get { return GetValue(() => ButtonReview); }
            set { SetValue(() => ButtonReview, value); }
        }
        public bool isErrorReview
        {
            get { return GetValue(() => isErrorReview); }
            set { SetValue(() => isErrorReview, value); }
        }
        public bool isReview
        {
            get { return GetValue(() => isReview); }
            set { SetValue(() => isReview, value); }
        }
        public string Comment
        {
            get { return GetValue(() => Comment); }
            set
            {
                SetValue(() => Comment, value);
                isErrorReview = false;
            }
        }

        public string ImageSource
        {
            get { return GetValue(() => ImageSource); }
            set { SetValue(() => ImageSource, value); }
        }
        public string Description
        {
            get { return GetValue(() => Description); }
            set { SetValue(() => Description, value); }
        }
        public string Name
        {
            get { return GetValue(() => Name); }
            set {   SetValue(() => Name, value);
            }
        }
        public string Author
        {
            get { return GetValue(() => Author); }
            set { SetValue(() => Author, value); }
        }
        public string Category
        {
            get { return GetValue(() => Category); }
            set { SetValue(() => Category, value); }
        }
        public string Year
        {
            get { return GetValue(() => Year); }
            set { SetValue(() => Year, value); }
        }
        public string NumberPages
        {
            get { return GetValue(() => NumberPages); }
            set { SetValue(() => NumberPages, value); }
        }
        public string Publisher
        {
            get { return GetValue(() => Publisher); }
            set { SetValue(() => Publisher, value); }
        }
        public string TypeBook
        {
            get { return GetValue(() => TypeBook); }
            set { SetValue(() => TypeBook, value); }
        }
        public string isActivity
        {
            get { return GetValue(() => isActivity); }
            set { SetValue(() => isActivity, value); }
        }
        public string Quantity
        {
            get { return GetValue(() => Quantity); }
            set { SetValue(() => Quantity, value); }
        }

        public double RatingBook
        {
            get { return GetValue(() => RatingBook); }
            set { SetValue(() => RatingBook, value); }
        }
        public int RatingUser
        {
            get { return GetValue(() => RatingUser); }
            set
            {
                SetValue(() => RatingUser, value);
                isErrorReview = false;
            }
        }

        public bool InCart
        {
            get { return GetValue(() => InCart); }
            set
            {
                SetValue(() => InCart, value);
            }
        }

        public Dictionary<string, string> Specification
        {
            get { return GetValue(() => Specification); }
            set { SetValue(() => Specification, value); }
        }

        public ObservableCollection<ReviewState> Reviews
        {
            get { return GetValue(() => Reviews); }
            set { SetValue(() => Reviews, value); }
        }
        public ObservableCollection<CollectionDefault> ListDefault
        {
            get { return GetValue(() => ListDefault); }
            set { SetValue(() => ListDefault, value); }
        }
        public ObservableCollection<CollectionState> ListState
        {
            get { return GetValue(() => ListState); }
            set { SetValue(() => ListState, value); }
        }



        public ProductDetailsPageViewModel()
        {
            ChangeBasketCommand = new RelayCommand(ChangeBasket);
            SendReviewCommand = new RelayCommand(SendReview);
            AddRewiewCommand = new RelayCommand(() => { isReview = true; });
            ChangeSelectedCollectionCommand = new RelayCommandParam((s) => ChangeSelectedCollection((string)s));
            GetProduct();
            SetCollection();
            SetProductState += GetProduct;
            Settings.changeLang += GetProduct;
            Settings.changeLang += SetCollection;
            InCart = db.Baskets.Get(MainWindowViewModel.id).ProductCodes.Any(p=> p.ProductCode == _productId);
        }
        private void SetCollection()
        {
            ListDefault = new ObservableCollection<CollectionDefault>();
            ListState = new ObservableCollection<CollectionState>();
            int id = MainWindowViewModel.id;
            foreach (var item in db.Collections.GetAll().Where(c => c.LibraryId == db.Libraries.Get(id).LibraryId).Where(c => (bool)c.IsDefault))
            {
                CollectionDefault collection = new CollectionDefault();
                collection.Name = item.Name;
                collection.LibraryId = item.LibraryId;
                collection.CollectionId = item.CollectionId;
                collection.IsChecked = item.ProductCodes.Any(p => p.ProductCode == ProductId);
                switch (item.Name)
                {
                    case "Избранное":
                    case "Favorites":
                          collection.Icon = "Heart";
                          break;
                    case "Read":
                    case "Прочитано":
                        collection.Icon = "Eye";
                        break;
                    case "Брошено":
                    case "Dropped":
                    collection.Icon = "TrashCanOutline";
                            break;
                    case "Запланировано":
                    case "Planned":
                    collection.Icon = "Library";
                            break;
                }

                ListDefault.Add(collection);
            }
            foreach (var item in db.Collections.GetAll().Where(c => c.LibraryId == db.Libraries.Get(id).LibraryId).Where(c => !(bool)c.IsDefault))
            {
                CollectionState collection = new CollectionState();
                collection.Name = item.Name;
                collection.LibraryId = item.LibraryId;
                collection.CollectionId = item.CollectionId;
                collection.IsChecked = false;
                ListState.Add(collection);
            }
        }

        private void ChangeBasket()
        {
            var basket = db.Baskets.Get(MainWindowViewModel.id);
            if (InCart)
            {
                basket.ProductCodes.Remove(basket.ProductCodes.FirstOrDefault(p => p.ProductCode == _productId));
                db.Baskets.Update(basket);
            }
            else
            {
                basket.ProductCodes.Add(db.Products.Get(_productId));
                db.Baskets.Update(basket);
            }
            InCart = !InCart;
        }

        private void ChangeSelectedCollection(string coll)
        {
            DatabaseUnit db = new DatabaseUnit();
            int col = Int32.Parse(coll.Replace("coll", ""));
            var product = db.Products.GetWithCollections(ProductId);

            foreach (var item in ListState)
            {
                if (item.IsChecked == true && item.CollectionId == col)
                    return;

                if (item.IsChecked == true)
                {
                    item.IsChecked = false;
                    product.Collections.Remove(product.Collections.FirstOrDefault(c => c.CollectionId == item.CollectionId));
                }

                if (item.CollectionId == col)
                    item.IsChecked = true;

            }

            foreach (var item in ListDefault)
            {
                if (item.IsChecked == true && item.CollectionId == col)
                    return;

                if (item.IsChecked == true)
                {
                    item.IsChecked = false;
                    product.Collections.Remove(product.Collections.FirstOrDefault(c => c.CollectionId == item.CollectionId));
                }

                if (item.CollectionId == col)
                    item.IsChecked = true;
            }
            product.Collections.Add(db.Collections.GetNoWithProduct(col));
            db.Products.Update(product);
        }

        private void SendReview()
        {
            if (RatingUser > 0)
            {
                Review review = new Review();
                Product product = db.Products.Get(ProductId);
                review.ProductCode = product.ProductCode;
                review.Comment = Comment;
                review.Rating = (byte)RatingUser;
                review.ReviewDate = DateTime.Now;
                review.UserId = MainWindowViewModel.id;
                db.Reviews.Add(review);
                ButtonReview = false;
                isReview = false;
                GetProduct();
            }
            else
            {
                isErrorReview = true;
            }
        }
        private void GetProduct()
        {
            Product product = db.Products.Get(ProductId);
            ImageSource = Settings.projectPath + "/image/Products/" + product.Pimage;
            Year = product.Year.ToString();
            NumberPages = product.NumberPages.ToString();
            Quantity = product.Quantity.ToString();
            Publisher = db.Publishers.Get(product.Publisher).Name;
            Reviews = new ObservableCollection<ReviewState>();
            foreach (var item in db.Reviews.GetAll().Where(r => r.ProductCode == _productId))
            {
                ReviewState reviewState = new ReviewState();
                reviewState.ProductCode = item.ProductCode;
                reviewState.ReviewDate = item.ReviewDate;
                reviewState.Rating = item.Rating;
                reviewState.Comment = item.Comment;
                reviewState.ReviewId = item.ReviewId;
                reviewState.UserId = item.UserId;
                var user = db.Users.Get(reviewState.UserId);
                reviewState.User = new UserState();
                reviewState.User.Username = user.Username;
                reviewState.User.Pavatar = Settings.projectPath + "/image/Avatars/" + user.Pavatar;
                Reviews.Add(reviewState);
            }
            if (Reviews.Any())
            {
                RatingBook = Math.Round(Reviews.Average(r => r.Rating), 1);
            }
            else
            {
                RatingBook = 0;
            }
            ButtonReview = !Reviews.Any(r => r.UserId == MainWindowViewModel.id);
            var temp = new Dictionary<string, string>();
            switch (Settings.Lang)
            {
                case Settings.Languages.RU:
                    Name = db.Dictionaries.Get(product.Pname).WordRus;
                    Author = db.Dictionaries.Get(db.Authors.Get((int)product.AuthorId).Palias).WordRus;
                    Category = db.Dictionaries.Get(db.Categories.Get((int)product.CatalogId).Pname).WordRus;
                    TypeBook = db.Dictionaries.Get(db.TypeBook.Get((int)product.TypeBook).Pname).WordRus;

                    Description = product.DescriptionRus;
                    foreach (var sp in db.Specifications.GetSpecForProduct(product))
                    {
                        string Name = db.Dictionaries.Get(sp.PspecName).WordRus;
                        var Cat = db.Categories.Get(1);
                        string Value = "";
                        bool w = false;
                        foreach (var specValList in sp.SpecValueForProductLists)
                        {
                            var speVal = db.SpecificationValues.Get(specValList.SpecValueId);
                            Value += (w == true) ? ", " : "";
                            Value += speVal.PvalueString != null ? db.Dictionaries.Get((int)speVal.PvalueString).WordRus : speVal.ValueInt.ToString();
                            w = true;
                        }
                        temp.Add(Name, Value);
                    }
                    isActivity = product.IsActive ? "есть" : "нет";
                    break;
                case Settings.Languages.EN:
                    Name = db.Dictionaries.Get(product.Pname).WordEn;
                    Author = db.Dictionaries.Get(db.Authors.Get((int)product.AuthorId).Palias).WordEn;
                    Category = db.Dictionaries.Get(db.Categories.Get((int)product.CatalogId).Pname).WordEn;
                    TypeBook = db.Dictionaries.Get(db.TypeBook.Get((int)product.TypeBook).Pname).WordEn;
                    Description = product.DescriptionEn;
                    foreach (var sp in db.Specifications.GetSpecForProduct(product))
                    {
                        string Name = db.Dictionaries.Get(sp.PspecName).WordEn;
                        string Value = "";
                        bool w = false;
                        foreach (var specValList in sp.SpecValueForProductLists)
                        {
                            var speVal = db.SpecificationValues.Get(specValList.SpecValueId);
                            Value += (w = true) ? ", " : "";
                            Value += speVal.PvalueString != null ? db.Dictionaries.Get((int)speVal.PvalueString).WordEn : speVal.ValueInt.ToString();
                        }
                        temp.Add(Name, Value);
                    }
                    isActivity = product.IsActive ? "yes" : "no";
                    break;
            }
            Specification = temp;
        }
    }
}
