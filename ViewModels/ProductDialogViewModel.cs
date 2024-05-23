using LibraryApp.Model;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LibraryApp
{
    public class ProductDialogViewModel : PropertyChangedNotification
    {
        DatabaseUnit db = new DatabaseUnit();
        private int id = -1;

        public RelayCommand AddComand { get; set; }
        public RelayCommand EditPictureComand { get; set; }

        public ObservableCollection<Tuple<string, int>> Category
        {
            get { return GetValue(() => Category); }
            set { SetValue(() => Category, value); }
        }
        public ObservableCollection<Tuple<string, int>> TypeBook
        {
            get { return GetValue(() => TypeBook); }
            set { SetValue(() => TypeBook, value); }
        }
        public ObservableCollection<Tuple<string, int>> Author
        {
            get { return GetValue(() => Author); }
            set { SetValue(() => Author, value); }
        }
        public ObservableCollection<Tuple<string, int>> Publisher
        {
            get { return GetValue(() => Publisher); }
            set { SetValue(() => Publisher, value); }
        }
        public ObservableCollection<SpecificationState> Specifications
        {
            get { return GetValue(() => Specifications); }
            set { SetValue(() => Specifications, value); }
        }

        public SpecificValueState SelectedSpecValue
        {
            get { return GetValue(() => SelectedSpecValue); }
            set { SetValue(() => SelectedSpecValue, value); }
        }
        public ObservableCollection<SpecificValueState> SpecificValues
        {
            get { return GetValue(() => SpecificValues); }
            set { SetValue(() => SpecificValues, value); }
        }
        public int SelectCategory
        {
            get { return GetValue(() => SelectCategory); }
            set { 
                SetValue(() => SelectCategory, value);
                if (value == -1){
                    ErrorIncorrectCategoryVisibility = true;
                    throw new Exception("Невыбран каталог изданий. Будьте внимательны");
                }
                else 
                    ErrorIncorrectCategoryVisibility = false;
            }
        }
        public int SelectTypeBook
        {
            get { return GetValue(() => SelectTypeBook); }
            set
            {
                SetValue(() => SelectTypeBook, value);
                if (value == -1){
                    ErrorIncorrectTypeBookVisibility = true;
                    throw new Exception("Невыбран тип изданий. Будьте внимательны");
                }
                else
                    ErrorIncorrectTypeBookVisibility = false;
            }
        }
        public int SelectAuthor
        {
            get { return GetValue(() => SelectAuthor); }
            set
            {
                SetValue(() => SelectAuthor, value);
                if (value == -1){
                    ErrorIncorrectAuthorVisibility = true;
                    throw new Exception("Невыбран автора изданий. Будьте внимательны");
                }
                else
                    ErrorIncorrectAuthorVisibility = false;
            }
        }
        public int SelectPublisher
        {
            get { return GetValue(() => SelectPublisher); }
            set
            {
                SetValue(() => SelectPublisher, value);
                if (value == -1){
                    ErrorIncorrectPublisherVisibility = true;
                    throw new Exception("Невыбран издателя изданий. Будьте внимательны");
                }
                else
                    ErrorIncorrectPublisherVisibility = false; }
        }

        public string DescriptionEn
        {
            get { return GetValue(() => DescriptionEn); }
            set { SetValue(() => DescriptionEn, value); }
        }
        public string DescriptionRus
        {
            get { return GetValue(() => DescriptionRus); }
            set { SetValue(() => DescriptionRus, value); }
        }
        public string NameRus
        {
            get { return GetValue(() => NameRus); }
            set { SetValue(() => NameRus, value);
                if (value == ""){
                    ErrorIncorrectNameRusVisibility = true;
                    throw new Exception("Невведен название изданий. Будьте внимательны");
                }
                else
                ErrorIncorrectNameRusVisibility = false;
            }
        }
        public string NameEn
        {
            get { return GetValue(() => NameEn); }
            set { SetValue(() => NameEn, value);
                if (value == "")
                {
                    ErrorIncorrectNameEnVisibility = true;
                    throw new Exception("Невведен название изданий. Будьте внимательны");
                }
                else
                    ErrorIncorrectNameEnVisibility = false;
            }
        }

        public string Year
        {
            get { return GetValue(() => Year); }
            set { SetValue(() => Year, value);
                 try
                {
                    int quantity = int.Parse(Year);
                    if (quantity < 0 || quantity > 100000)
                        throw new ArgumentException();
                    ErrorIncorrectYearVisibility = false;
                }
                catch (FormatException)
                {
                    ErrorIncorrectYearVisibility = true;
                    throw new Exception("Некоректно введен год изданий. Будьте внимательны");
                }
                catch (ArgumentNullException)
                {
                    ErrorIncorrectYearVisibility = true;                    
                    throw new Exception("Невведен год изданий. Будьте внимательны");
                }
                catch (ArgumentException)
                {
                    ErrorIncorrectYearVisibility = true;                    
                    throw new Exception("Возникла ошибка в год изданий. Будьте внимательны");
                }
            }
        }

        public string Quantity
        {
            get { return GetValue(() => Quantity); }
            set { SetValue(() => Quantity, value);
                 try
                {
                    int quantity = int.Parse(Quantity);
                    if (quantity < 0 || quantity > 100000)
                        throw new ArgumentException();
                    ErrorIncorrectQuantityVisibility = false;
                }
                catch (FormatException)
                {
                    ErrorIncorrectQuantityVisibility = true;
                    throw new Exception("Некоректно введено количество изданий. Будьте внимательны");
                }
                catch (ArgumentNullException)
                {
                    ErrorIncorrectQuantityVisibility = true;
                    throw new Exception("Невведено количество изданий. Будьте внимательны");
                }
                catch (ArgumentException)
                {
                    ErrorIncorrectQuantityVisibility = true;
                    throw new Exception("Возникла ошибка в количестве изданий. Будьте внимательны");
                }
            }
        }
        public string NumberPage
        {
            get { return GetValue(() => NumberPage); }
            set { SetValue(() => NumberPage, value);
                try
                {
                    int quantity = int.Parse(NumberPage);
                    if (quantity < 0 || quantity > 100000)
                        throw new ArgumentException();
                    ErrorIncorrectNumberPagesVisibility = false;
                }
                catch (FormatException)
                {
                    ErrorIncorrectNumberPagesVisibility = true;
                    throw new Exception("Некоректно введено число страниц. Будьте внимательны");
                }
                catch (ArgumentNullException)
                {
                    ErrorIncorrectNumberPagesVisibility = true;
                    throw new Exception("Не введено число страниц. Будьте внимательны");
                }
                catch (ArgumentException)
                {
                    ErrorIncorrectNumberPagesVisibility = true;
                    throw new Exception("Возникла ошибка в количестве страниц. Будьте внимательны");
                }
            }
        }

        public bool Aviability
        {
            get { return GetValue(() => Aviability); }
            set { SetValue(() => Aviability, value); }
        }
        public string SourceImage
        {
            get { return GetValue(() => SourceImage); }
            set { SetValue(() => SourceImage, value); }
        }

        public bool ErrorIncorrectNameRusVisibility
        {
            get { return GetValue(() => ErrorIncorrectNameRusVisibility); }
            set { SetValue(() => ErrorIncorrectNameRusVisibility, value); }
        }
        
        public bool ErrorIncorrectNameEnVisibility
        {
            get { return GetValue(() => ErrorIncorrectNameEnVisibility); }
            set { SetValue(() => ErrorIncorrectNameEnVisibility, value); }
        }
        public bool ErrorIncorrectAuthorVisibility
        {
            get { return GetValue(() => ErrorIncorrectAuthorVisibility); }
            set { SetValue(() => ErrorIncorrectAuthorVisibility, value); }
        }
        public bool ErrorIncorrectPublisherVisibility
        {
            get { return GetValue(() => ErrorIncorrectPublisherVisibility); }
            set { SetValue(() => ErrorIncorrectPublisherVisibility, value); }
        }

        public bool ErrorIncorrectCategoryVisibility
        {
            get { return GetValue(() => ErrorIncorrectCategoryVisibility); }
            set { SetValue(() => ErrorIncorrectCategoryVisibility, value); }
        }
        public bool ErrorIncorrectYearVisibility
        {
            get { return GetValue(() => ErrorIncorrectYearVisibility); }
            set { SetValue(() => ErrorIncorrectYearVisibility, value); }
        }

        public bool ErrorIncorrectQuantityVisibility
        {
            get { return GetValue(() => ErrorIncorrectQuantityVisibility); }
            set { SetValue(() => ErrorIncorrectQuantityVisibility, value); }
        }
        public bool ErrorIncorrectTypeBookVisibility
        {
            get { return GetValue(() => ErrorIncorrectTypeBookVisibility); }
            set { SetValue(() => ErrorIncorrectTypeBookVisibility, value); }
        }
        
        public bool ErrorIncorrectNumberPagesVisibility
        {
            get { return GetValue(() => ErrorIncorrectNumberPagesVisibility); }
            set { SetValue(() => ErrorIncorrectNumberPagesVisibility, value); }
        }
        public void getData()
        {
            TypeBook = new ObservableCollection<Tuple<string, int>>();
            Category = new ObservableCollection<Tuple<string, int>>();
            Author = new ObservableCollection<Tuple<string, int>>();
            Publisher = new ObservableCollection<Tuple<string, int>>();
            foreach (var item in db.TypeBook.GetAll())
            {
                switch (Settings.Lang)
                {
                    case Settings.Languages.EN:
                        TypeBook.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Pname).WordEn, item.IdType));
                        break;
                    case Settings.Languages.RU:
                        TypeBook.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Pname).WordRus, item.IdType));
                        break;

                }
            }
            foreach (var item in db.Authors.GetAll())
            {
                switch (Settings.Lang)
                {
                    case Settings.Languages.EN:
                        Author.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Palias).WordEn, item.AuthorId));
                        break;
                    case Settings.Languages.RU:
                        Author.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Palias).WordRus, item.AuthorId));
                        break;

                }
            }
            foreach (var item in db.Publishers.GetAll())
            {
                Publisher.Add(new Tuple<string, int>(item.Name, item.IdPublisher));
            }
            foreach (var item in db.Categories.GetAll())
            {
                switch (Settings.Lang)
                {
                    case Settings.Languages.EN:
                        Category.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Pname).WordEn, item.CategoryId));
                        break;
                    case Settings.Languages.RU:
                        Category.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Pname).WordRus, item.CategoryId));
                        break;

                }
            }
        }
        public ProductDialogViewModel() {
            try
            {
                AddComand = new RelayCommand(Add);
                EditPictureComand = new RelayCommand(EditPicture);
                getData();
                Aviability = false;
                NumberPage = string.Empty;
                Year = string.Empty;
                Quantity = string.Empty;
                NameEn = string.Empty;
                NameRus = string.Empty;
                DescriptionEn = string.Empty;
                DescriptionRus = string.Empty;
                SourceImage = string.Empty;
                SelectCategory = -1;
                SelectAuthor = -1;
                SelectPublisher = -1;
                SelectTypeBook = -1;
                SpecificValues = new ObservableCollection<SpecificValueState>();
                Specifications = new ObservableCollection<SpecificationState>();
            }
            catch (Exception ex) { }
        }

        public ProductDialogViewModel(int id) {
            try
            {
                getData();
                this.id = id;
                var product = db.Products.Get(id);
                Aviability = product.IsActive;
                NumberPage = product.NumberPages.ToString();
                Year = product.Year.ToString();
                Quantity = product.Quantity.ToString();
                NameEn = db.Dictionaries.Get(product.Pname).WordEn;
                NameRus = db.Dictionaries.Get(product.Pname).WordRus;
                DescriptionEn = product.DescriptionEn;
                DescriptionRus = product.DescriptionRus;
                SourceImage = Settings.projectPath + "/image/Products/" + product.Pimage;
                SelectPublisher = Publisher.IndexOf(Publisher.FirstOrDefault(i => i.Item2 == product.Publisher));
                SelectCategory = Category.IndexOf(Category.FirstOrDefault(i => i.Item2 == product.CatalogId));
                SelectAuthor = Author.IndexOf(Author.FirstOrDefault(i => i.Item2 == product.AuthorId));
                SelectTypeBook = TypeBook.IndexOf(TypeBook.FirstOrDefault(i => i.Item2 == product.TypeBook));
                var dictionary = db.Dictionaries;
                SpecificValues = new ObservableCollection<SpecificValueState>();
                Specifications = new ObservableCollection<SpecificationState>();
                getSpecifications(product);
            }
            catch (Exception ex) { }
        }

        private void getSpecifications(Product product)
        {
            foreach (var sp in db.Specifications.GetSpecForProduct(product))
            {
                string Name = db.Dictionaries.Get(sp.PspecName).WordRus;
                int id = sp.SpecId;
                foreach (var specValList in sp.SpecValueForProductLists)
                {
                    SpecificValueState valueState = new SpecificValueState();
                    SpecificationState specificationState = new SpecificationState();
                    var speVal = db.SpecificationValues.Get(specValList.SpecValueId);
                    if (speVal.PvalueString != null)
                    {
                        valueState.ValueType = "string";
                        var dictionary = db.Dictionaries.Get(speVal.SpecValueId);
                        valueState.ValueInt = 0.0M;
                        valueState.ValueStringRu = dictionary.WordRus;
                        valueState.ValueStringEn = dictionary.WordEn;
                        valueState.SpecValueId = speVal.SpecValueId;
                        SpecificValues.Add(valueState);
                        specificationState.SpecId = id;
                        specificationState.NameRu = Name;
                        Specifications.Add(specificationState);

                    }
                    else
                    {
                        valueState.ValueType = "int";
                        valueState.ValueInt = (decimal)speVal.ValueInt;
                        valueState.ValueStringRu = "";
                        valueState.ValueStringEn = "";
                        valueState.SpecValueId = speVal.SpecValueId;
                        SpecificValues.Add(valueState);
                        specificationState.SpecId = id;
                        specificationState.NameRu = Name;
                        Specifications.Add(specificationState);
                    }

                }
            }
        }
        private void Add()
        {
            Product product = new Product();
            Dictionary dictionaryName = new Dictionary();
            dictionaryName.WordRus = NameRus;
            dictionaryName.WordEn = NameEn;
            db.Dictionaries.Add(dictionaryName);
            product.Pname = dictionaryName.WordId;
            product.DescriptionRus = DescriptionRus;
            product.DescriptionEn = DescriptionEn;
            product.AuthorId = Author[SelectAuthor].Item2;
            product.TypeBook = TypeBook[SelectTypeBook].Item2;
            product.Publisher = Publisher[SelectPublisher].Item2;
            product.CatalogId = Category[SelectCategory].Item2;
            product.Quantity = int.Parse(Quantity);
            product.Year = int.Parse(Year);
            product.NumberPages = int.Parse(NumberPage);
            product.IsActive = Aviability;
            product.Pimage = "";
            db.Products.Add(product);
            if (SourceImage != "") {
                string[] str = SourceImage.Split('\\');
                string[] extensions = str[str.Length - 1].Split('.');
                string extension = extensions[extensions.Length - 1];
                product.Pimage = product.ProductCode + "." + extension;
                File.Copy(SourceImage, Settings.projectPath + "\\image\\Products\\" + product.Pimage, true);
                product.Pimage = AdminWindowViewModel.id + "." + extension;
            }
            db.Products.Update(product);
        }


        private void EditPicture()
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
    }
}
