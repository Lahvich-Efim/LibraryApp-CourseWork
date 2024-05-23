using LibraryApp.Model;
using LibraryApp.Utils;
using System.Collections.ObjectModel;

namespace LibraryApp
{
    public class AdminProductPageViewModel : PropertyChangedNotification
    {
        public delegate void EventHandler(object sender, EventArgs e);
        private DatabaseUnit db = new DatabaseUnit();
        private static int SelectProductID = -1;
        public RelayCommand AddProductCommand { get; set; }
        public RelayCommand EditProductCommand { get; set; }
        public RelayCommand DeActiveProductCommand { get; set; }

        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ChangeRangerCommand { get; set; }
        public RelayCommandParam ChangeCategoryCommand { get; set; }
        public RelayCommandParam ChangeTypeCommand { get; set; }
        public RelayCommandParam ChangePublisherCommand { get; set; }
        public RelayCommandParam SelectProductCommand { get; set; }

        private static DistanceAlferov distanceAuthor { get; set; }
        private static DistanceAlferov distanceNameBook { get; set; }

        public string SearchName
        {
            get { return GetValue(() => SearchName); }
            set { SetValue(() => SearchName, value); }
        }
        public string SearchAuthor
        {
            get { return GetValue(() => SearchAuthor); }
            set { SetValue(() => SearchAuthor, value); }
        }

        public List<string> CheckedType
        {
            get { return GetValue(() => CheckedType); }
            set { SetValue(() => CheckedType, value); }
        }

        public List<string> CheckedPublisher
        {
            get { return GetValue(() => CheckedPublisher); }
            set { SetValue(() => CheckedPublisher, value); }
        }
        public int CatalogId
        {
            get { return GetValue(() => CatalogId); }
            set { SetValue(() => CatalogId, value); }
        }
        public int MinYear
        {
            get { return GetValue(() => MinYear); }
            set { SetValue(() => MinYear, value); }
        }
        public int MaxYear
        {
            get { return GetValue(() => MaxYear); }
            set { SetValue(() => MaxYear, value); }
        }

        public int MaxNumberPages
        {
            get { return GetValue(() => MaxNumberPages); }
            set { SetValue(() => MaxNumberPages, value); }
        }
        public int MinNumberPages
        {
            get { return GetValue(() => MinNumberPages); }
            set { SetValue(() => MinNumberPages, value); }
        }
        public bool Aviability
        {
            get { return GetValue(() => Aviability); }
            set { SetValue(() => Aviability, value); }
        }

        public ObservableCollection<CategoryState> Categories
        {
            get { return GetValue(() => Categories); }
            set { SetValue(() => Categories, value); }
        }
        public ObservableCollection<ProductState> Products
        {
            get { return GetValue(() => Products); }
            set { SetValue(() => Products, value); }
        }
        public ObservableCollection<ProductState> FilterProducts
        {
            get { return GetValue(() => FilterProducts); }
            set { SetValue(() => FilterProducts, value); }
        }
        public ObservableCollection<string> TypeBook
        {
            get { return GetValue(() => TypeBook); }
            set { SetValue(() => TypeBook, value); }
        }

        public ObservableCollection<string> Publisher
        {
            get { return GetValue(() => Publisher); }
            set { SetValue(() => Publisher, value); }
        }

        public void MyMethod(EventHandler handler)
        {

            handler(this, EventArgs.Empty);
        }

        public AdminProductPageViewModel()
        {
            ChangeTypeCommand = new RelayCommandParam((s) => ChangeType((string)s));
            ChangePublisherCommand = new RelayCommandParam((s) => ChangePublisher((string)s));
            ChangeRangerCommand = new RelayCommand(ChangeRanger);
            ChangeCategoryCommand = new RelayCommandParam((s) => ChangeCategory((string)s));
            CheckedType = new List<string>();
            AddProductCommand = new RelayCommand(AddProduct);
            EditProductCommand = new RelayCommand(EditProduct, () => SelectProductID != -1);
            DeActiveProductCommand = new RelayCommand(DeActiveProduct, () => SelectProductID != -1);
            CheckedPublisher = new List<string>();
            SearchCommand = new RelayCommand(ShowPage);
            Products = new ObservableCollection<ProductState>();
            SelectProductCommand = new RelayCommandParam((s) => SelectProduct((string)s));
            CatalogId = -1;
            SearchAuthor = string.Empty;
            SearchName = string.Empty;
            MaxYear = MaxNumberPages = 0;
            MinYear = MinNumberPages = 100000;
            ShowCategory();
            ShowFilter();
            GetProducts();
            Settings.changeLang += GetProducts;
            Settings.changeLang += ShowCategory;
            Settings.changeLang += ShowFilter;
        }


        private void AddProduct()
        {
            WindowService.OpenWindow(WindowService.Windows.ProductDialog, new ProductDialogViewModel());
            SelectProductID = -1;
            GetProducts();
        }

        private void EditProduct()
        {

            WindowService.OpenWindow(WindowService.Windows.ProductDialog, new ProductDialogViewModel(SelectProductID));
            SelectProductID = -1;
            GetProducts();
        }

        private void DeActiveProduct()
        {
            var product = db.Products.Get(SelectProductID);
            product.IsActive = !product.IsActive;
            db.Products.Update(product);
        }

        private void SelectProduct(string product)
        {
            SelectProductID = Int32.Parse(product.Replace("btn", ""));
        }

        private void ChangeCategory(string category)
        {
            int idCategory = int.Parse(category.Replace("cat", ""));
            CatalogId = idCategory;
            ShowPage();
        }


        private void ShowFilter()
        {
            TypeBook = new ObservableCollection<string>();
            Publisher = new ObservableCollection<string>();
            foreach (var typebook in db.TypeBook.GetAll())
            {
                switch (Settings.Lang)
                {
                    case Settings.Languages.RU:
                        TypeBook.Add(db.Dictionaries.Get(typebook.Pname).WordRus);
                        break;
                    case Settings.Languages.EN:
                        TypeBook.Add(db.Dictionaries.Get(typebook.Pname).WordEn);
                        break;
                }
            }
            foreach (var publisher in db.Publishers.GetAll())
            {
                Publisher.Add(publisher.Name);
            }
        }
        private void ChangeType(string type)
        {
            if (CheckedType.Contains(type))
                CheckedType.Remove(type);
            else
                CheckedType.Add(type);
            ShowPage();
        }

        private void ChangePublisher(string publisher)
        {
            if (CheckedPublisher.Contains(publisher))
                CheckedPublisher.Remove(publisher);
            else
                CheckedPublisher.Add(publisher);
            ShowPage();
        }

        private void ChangeRanger()
        {
            ShowPage();
        }

        private void ShowCategory()
        {
            Categories = new ObservableCollection<CategoryState>();
            foreach (var item in db.Categories.GetAll())
            {
                CategoryState category = new CategoryState();
                category.CategoryId = item.CategoryId;
                switch (Settings.Lang)
                {
                    case Settings.Languages.RU:
                        category.Name = db.Dictionaries.Get(item.Pname).WordRus;
                        break;
                    case Settings.Languages.EN:
                        category.Name = db.Dictionaries.Get(item.Pname).WordEn;
                        break;
                }
                category.Image = Settings.projectPath + "/image/Category/" + item.Pimage;
                Categories.Add(category);
            }
        }
        private void GetProducts()
        {
            Products = new ObservableCollection<ProductState>();
            distanceAuthor = new DistanceAlferov();
            distanceNameBook = new DistanceAlferov();
            foreach (var p in db.Products.GetAll())
            {
                Tuple<string, string> author = new Tuple<string, string>(db.Dictionaries.Get(db.Authors.Get(p.AuthorId).Palias).WordRus, db.Dictionaries.Get(db.Authors.Get(p.AuthorId).Palias).WordEn);
                Tuple<string, string> name = new Tuple<string, string>(db.Dictionaries.Get(p.Pname).WordRus, db.Dictionaries.Get(p.Pname).WordEn);
                distanceAuthor.Add(author);
                distanceNameBook.Add(name);
                ProductState state = new ProductState();
                switch (Settings.Lang)
                {
                    case Settings.Languages.RU:
                        state.Name = name.Item1;
                        state.Author = author.Item1;
                        state.TypeBook = db.Dictionaries.Get(db.TypeBook.Get(p.TypeBook).Pname).WordRus;
                        break;
                    case Settings.Languages.EN:
                        state.Name = name.Item2;
                        state.Author = author.Item2;
                        state.TypeBook = db.Dictionaries.Get(db.TypeBook.Get(p.TypeBook).Pname).WordEn;
                        break;
                }
                state.CatalogId = p.CatalogId;
                state.Year = (int)p.Year;
                state.NumberPages = (int)p.NumberPages;
                state.Id = p.ProductCode;
                state.Publisher = db.Publishers.Get(p.Publisher).Name;
                state.Image = Settings.projectPath + "/image/Products/" + p.Pimage;
                state.IsActive = p.IsActive;
                int count = 0;
                foreach (var basket in db.OrderInfos.GetAll())
                    foreach (var product in basket.ProductCodes)
                        if (product.ProductCode == p.ProductCode && basket.IsActiv)
                            count++;
                state.Quantity -= count;
                var reviews = db.Reviews.GetAll().Where(r => r.ProductCode == state.Id);
                if (reviews.Count() != 0)
                {
                    state.Rating = Math.Round(reviews.Average(r => r.Rating), 1); ;
                }
                else
                {
                    state.Rating = 0;
                }
                if (MaxNumberPages < p.NumberPages)
                    MaxNumberPages = (int)p.NumberPages;
                if (MinNumberPages > p.NumberPages)
                    MinNumberPages = (int)p.NumberPages;
                if (MinYear > p.Year)
                    MinYear = (int)p.Year;
                if (MaxYear < p.Year)
                    MaxYear = (int)p.Year;
                Products.Add(state);
            }
            ShowPage();
        }

        private bool ReleaseSearchName(ProductState productState, string search, int minCost)
        {
            var list = distanceNameBook.Search(search).OrderBy(x => x.Item3);
            foreach (Tuple<string, string, double, int> items in list)
            {
                if (productState.Name == items.Item1 || productState.Name == items.Item2)
                    return items.Item3 < minCost;
            }
            return false;
        }

        private bool ReleaseSearchAuthor(ProductState productState, string search, int minCost)
        {
            var list = distanceAuthor.Search(search).OrderBy(x => x.Item3);
            foreach (Tuple<string, string, double, int> items in list)
            {
                if (productState.Author == items.Item1 || productState.Author == items.Item2)
                    return items.Item3 < minCost;
            }
            return false;
        }

        private void ShowPage()
        {
            FilterProducts = new ObservableCollection<ProductState>();
            foreach (var p in Products)
            {
                if (
                    (MaxNumberPages >= p.NumberPages) &&
                    (MinNumberPages <= p.NumberPages) &&
                    (MaxYear >= p.Year) &&
                    (MinYear <= p.Year) &&
                    (!Aviability || p.IsActive) &&
                    (CatalogId == -1 || CatalogId == p.CatalogId) &&
                    (CheckedType.Count == 0 || CheckedType.Contains(p.TypeBook)) &&
                    (CheckedPublisher.Count == 0 || CheckedPublisher.Contains(p.Publisher)) &&
                    (SearchName == "" || ReleaseSearchName(p, SearchName, 400)) &&
                    (SearchAuthor == "" || ReleaseSearchAuthor(p, SearchAuthor, 400)))
                {
                    FilterProducts.Add(p);
                }
            }
        }

    }
}
