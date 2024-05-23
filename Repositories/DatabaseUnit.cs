using LibraryApp.Model;
using LibraryApp.Repositories;


// С помощью такой команды в консоли диспетчера пакетов можно произвести реинжиниринг из бд в модель классов(объектов):
// (хотя лучше как нормальные и адекватные программисты юзать ORM)
// Scaffold-DbContext "Server=PETYA;Database=LibraryApp;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -output Model -force

namespace LibraryApp
{
    public class DatabaseUnit
    {
        private LibraryAppContext db = new LibraryAppContext();
        private AuthorRepository authorRepository;
        private DictionaryRepository dictionaryRepository;
        private BasketRepository basketRepository;
        private ProductsRepository productsRepository;
        private UserRepository userRepository;
        private CategoriesRepository categoriesRepository;
        private CollectionRepository collectionRepository;
        private LibraryRepository libraryRepository;
        private OrderInfoRepository ordersInfoRepository;
        private ReviewRepository reviewRepository;
        private SpecificationRepository specificationRepository;
        private SpecificationValueRepository specificationValueRepository;
        private PublisherRepository publisherRepository;
        private TypeBookRepository typeBookRepository;
        private NotifyRepository notifyRepository;

        public NotifyRepository Notifies
        {
            get
            {
                if (notifyRepository == null)
                {
                    notifyRepository = new NotifyRepository();
                }
                return notifyRepository;
            }
        }
        public TypeBookRepository TypeBook
        {
            get
            {
                if (typeBookRepository == null)
                {
                    typeBookRepository = new TypeBookRepository();
                }
                return typeBookRepository;
            }
        }
        public PublisherRepository Publishers
        {
            get
            {
                if (publisherRepository == null)
                {
                    publisherRepository = new PublisherRepository();
                }
                return publisherRepository;
            }
        }
        public AuthorRepository Authors
        {
            get
            {
                if (authorRepository == null)
                {
                    authorRepository = new AuthorRepository();
                }
                return authorRepository;
            }
        }
        public DictionaryRepository Dictionaries
        {
            get
            {
                if (dictionaryRepository == null)
                {
                    dictionaryRepository = new DictionaryRepository();
                }
                return dictionaryRepository;
            }
        }
        public SpecificationValueRepository SpecificationValues
        {
            get
            {
                if (specificationValueRepository == null)
                {
                    specificationValueRepository = new SpecificationValueRepository();
                }
                return specificationValueRepository;
            }
        }
        public SpecificationRepository Specifications
        {
            get
            {
                if (specificationRepository == null)
                {
                    specificationRepository = new SpecificationRepository();
                }
                return specificationRepository;
            }
        }
        public ReviewRepository Reviews
        {
            get
            {
                if (reviewRepository == null)
                {
                    reviewRepository = new ReviewRepository();
                }
                return reviewRepository;
            }
        }
        public LibraryRepository Libraries
        {
            get
            {
                if (libraryRepository == null)
                {
                    libraryRepository = new LibraryRepository();
                }
                return libraryRepository;
            }
        }
        public CollectionRepository Collections
        {
            get
            {
                if (collectionRepository == null)
                {
                    collectionRepository = new CollectionRepository();
                }
                return collectionRepository;
            }
        }
        public CategoriesRepository Categories
        {
            get
            {
                if (categoriesRepository == null)
                {
                    categoriesRepository = new CategoriesRepository();
                }
                return categoriesRepository;
            }
        }
        public OrderInfoRepository OrderInfos
        {
            get
            {
                if (ordersInfoRepository == null)
                {
                    ordersInfoRepository = new OrderInfoRepository();
                }
                return ordersInfoRepository;
            }
        }
        public ProductsRepository Products
        {
            get
            {
                if (productsRepository == null)
                {
                    productsRepository = new ProductsRepository();
                }
                return productsRepository;
            }
        }
        public UserRepository Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository();
                }
                return userRepository;
            }
        }
        public BasketRepository Baskets
        {
            get
            {
                if (basketRepository == null)
                {
                    basketRepository = new BasketRepository();
                }
                return basketRepository;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }

    }
}
