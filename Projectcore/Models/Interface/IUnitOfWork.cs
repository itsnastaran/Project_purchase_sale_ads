using Domain;
using Projectcore.Data;

namespace Projectcore.Models
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<ConfigEmail> ConfigEmailRepository { get; }
        IBaseRepository<ConfigSms> ConfigSmsRepository { get; }
        IBaseRepository<AboutUs> AboutUsRepository { get; }
        IBaseRepository<Category> CategoryRepository { get; }
        IBaseRepository<CategoryDetails> CategoryDetailsRepository { get; }
        IBaseRepository<SubCategory> SubCategoryRepository { get; }
        IBaseRepository<ContactUs> ContactUsRepository { get; }
        IBaseRepository<Favorties> FavortiesRepository { get; }
        IBaseRepository<Menu> MenuRepository { get; }
        IBaseRepository<PhotoGallery> PhotoGalleryRepository { get; }
        IBaseRepository<ProductType> ProductTypeRepository { get; }
        IBaseRepository<Report> ReportRepository { get; }
        IBaseRepository<Rules> RulesRepository { get; }
        IBaseRepository<SaleAd> SaleAdRepository { get; }
        IBaseRepository<Suport> SuportRepository { get; }
        IBaseRepository<TypeOfAd> TypeOfAdRepository { get; }
        IBaseRepository<ApplicationUser> ApplicationUserRepository { get; }
        IBaseRepository<CategoryDetailsAd> CategoryDetailsAdRepository { get; }
        IBaseRepository<Payment> PaymentRepository { get; }

        int save();
    }
}
