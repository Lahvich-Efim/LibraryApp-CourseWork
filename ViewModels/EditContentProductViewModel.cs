using LibraryApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp
{


    public class EditContentProductViewModel : PropertyChangedNotification
    {
        DatabaseUnit db = new DatabaseUnit();
        public ObservableCollection<Tuple<string, int>> Categories
        {
            get { return GetValue(() => Categories); }
            set { SetValue(() => Categories, value); }
        }
        public ObservableCollection<Tuple<string, int>> TypeBooks
        {
            get { return GetValue(() => TypeBooks); }
            set { SetValue(() => TypeBooks, value); }
        }
        public ObservableCollection<Tuple<string, int>> Authors
        {
            get { return GetValue(() => Authors); }
            set { SetValue(() => Authors, value); }
        }
        public ObservableCollection<Tuple<string, int>> Publishers
        {
            get { return GetValue(() => Publishers); }
            set { SetValue(() => Publishers, value); }
        }
        public int SelectTypeBook
        {
            get { return GetValue(() => SelectTypeBook); }
            set { SetValue(() => SelectTypeBook, value); }
        }
        public int SelectPublisher
        {
            get { return GetValue(() => SelectPublisher); }
            set { SetValue(() => SelectPublisher, value); }
        }
        public int SelectAuthor
        {
            get { return GetValue(() => SelectAuthor); }
            set { SetValue(() => SelectAuthor, value); }
        }
        public int SelectCatalog
        {
            get { return GetValue(() => SelectCatalog); }
            set { SetValue(() => SelectCatalog, value); }
        }

        public EditContentProductViewModel()
        {
            try
            {
                getData();
                SelectCatalog = -1;
                SelectAuthor = -1;
                SelectPublisher = -1;
                SelectTypeBook = -1;
            }
            catch { }
        }

        public void getData()
        {
            TypeBooks = new ObservableCollection<Tuple<string, int>>();
            Categories = new ObservableCollection<Tuple<string, int>>();
            Authors = new ObservableCollection<Tuple<string, int>>();
            Publishers = new ObservableCollection<Tuple<string, int>>();
            foreach (var item in db.TypeBook.GetAll())
            {
                switch (Settings.Lang)
                {
                    case Settings.Languages.EN:
                        TypeBooks.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Pname).WordEn, item.IdType));
                        break;
                    case Settings.Languages.RU:
                        TypeBooks.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Pname).WordRus, item.IdType));
                        break;

                }
            }
            foreach (var item in db.Authors.GetAll())
            {
                switch (Settings.Lang)
                {
                    case Settings.Languages.EN:
                        Authors.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Palias).WordEn, item.AuthorId));
                        break;
                    case Settings.Languages.RU:
                        Authors.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Palias).WordRus, item.AuthorId));
                        break;

                }
            }
            foreach (var item in db.Publishers.GetAll())
            {
                Publishers.Add(new Tuple<string, int>(item.Name, item.IdPublisher));
            }
            foreach (var item in db.Categories.GetAll())
            {
                switch (Settings.Lang)
                {
                    case Settings.Languages.EN:
                        Categories.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Pname).WordEn, item.CategoryId));
                        break;
                    case Settings.Languages.RU:
                        Categories.Add(new Tuple<string, int>(db.Dictionaries.Get(item.Pname).WordRus, item.CategoryId));
                        break;

                }
            }
        }
    }
}
