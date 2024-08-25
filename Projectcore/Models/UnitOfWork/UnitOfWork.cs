using Domain;
using Projectcore.Data;
using Projectcore.Models.Repository;

namespace Projectcore.Models
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        

        public IBaseRepository<ConfigEmail> ConfigEmailRepository { get; private set; }
        public IBaseRepository<ConfigSms> ConfigSmsRepository { get; private set; }
        public IBaseRepository<AboutUs> AboutUsRepository { get; private set; }
        public IBaseRepository<Category> CategoryRepository { get; private set; }
        public IBaseRepository<CategoryDetails> CategoryDetailsRepository { get; private set; }
        public IBaseRepository<SubCategory> SubCategoryRepository { get; private set; }
        public IBaseRepository<ContactUs> ContactUsRepository { get; private set; }
        public IBaseRepository<Favorties> FavortiesRepository { get; private set; }
        public IBaseRepository<Menu> MenuRepository { get; private set; }
        public IBaseRepository<PhotoGallery> PhotoGalleryRepository { get; private set; }
        public IBaseRepository<ProductType> ProductTypeRepository { get; private set; }
        public IBaseRepository<Report> ReportRepository { get; private set; }
        public IBaseRepository<Rules> RulesRepository { get; private set; }
        public IBaseRepository<SaleAd> SaleAdRepository { get; private set; }
        public IBaseRepository<Suport> SuportRepository { get; private set; }
        public IBaseRepository<TypeOfAd> TypeOfAdRepository { get; private set; }

        public IBaseRepository<ApplicationUser> ApplicationUserRepository { get; private set; }
        public IBaseRepository<CategoryDetailsAd> CategoryDetailsAdRepository { get; private set; }
        public IBaseRepository<Payment> PaymentRepository { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ConfigEmailRepository = new BaseRepository<ConfigEmail>(_context);
            ConfigSmsRepository = new BaseRepository<ConfigSms>(_context);
            AboutUsRepository = new BaseRepository<AboutUs>(_context);
            CategoryRepository = new BaseRepository<Category>(_context);
            CategoryDetailsRepository = new BaseRepository<CategoryDetails>(_context);
            SubCategoryRepository = new BaseRepository<SubCategory>(_context);
            ContactUsRepository = new BaseRepository<ContactUs>(_context);
            SaleAdRepository = new BaseRepository<SaleAd>(_context);
            TypeOfAdRepository = new BaseRepository<TypeOfAd>(_context);
            ReportRepository = new BaseRepository<Report>(_context);
            SuportRepository = new BaseRepository<Suport>(_context);
            RulesRepository = new BaseRepository<Rules>(_context);
            ProductTypeRepository = new BaseRepository<ProductType>(_context);
            PhotoGalleryRepository = new BaseRepository<PhotoGallery>(_context);
            FavortiesRepository = new BaseRepository<Favorties>(_context);
            MenuRepository = new BaseRepository<Menu>(_context);
            ApplicationUserRepository = new BaseRepository<ApplicationUser>(_context);
            CategoryDetailsAdRepository = new BaseRepository<CategoryDetailsAd>(_context);
            PaymentRepository = new BaseRepository<Payment>(_context);
        }

        public UnitOfWork()
        {
        }

        public int save()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
