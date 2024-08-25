using Domain;
using Projectcore.Data;
using Projectcore.Models;
using Projectcore.Models.Repository;

namespace Projectcore
{
    public class UnitOfWord:IDisposable
    {
        ApplicationDbContext context=null;
        public IBaseRepository<ApplicationUser> ApplicationUserRepository { get; private set; }
        public UnitOfWord(ApplicationDbContext Context)
        {
            this.context=Context;
            ApplicationUserRepository = new BaseRepository<ApplicationUser>(context);
        }

        private GenericRepository<AboutUs> AboutUsgenericrepository;
        public GenericRepository<AboutUs> AboutUsGenericRepository
        {
            get
            {
                if (this.AboutUsgenericrepository == null)
                    this.AboutUsgenericrepository = new GenericRepository<AboutUs>(context);
                return AboutUsgenericrepository;
            }
        }
        private GenericRepository<Category> Categorygenericrepository;
        public GenericRepository<Category> CategoryGenericRepository
        {
            get
            {
                if (this.Categorygenericrepository == null)
                    this.Categorygenericrepository = new GenericRepository<Category>(context);
                return Categorygenericrepository;
            }
        }
        private GenericRepository<CategoryDetails> CategoryDetailsgenericrepository;
        public GenericRepository<CategoryDetails> CategoryDetailsGenericRepository
        {
            get
            {
                if (this.CategoryDetailsgenericrepository == null)
                    this.CategoryDetailsgenericrepository = new GenericRepository<CategoryDetails>(context);
                return CategoryDetailsgenericrepository;
            }
        }
        private GenericRepository<CategoryDetailsAd> CategoryDetailsAdgenericrepository;
        public GenericRepository<CategoryDetailsAd> CategoryDetailsAdGenericRepository
        {
            get
            {
                if (this.CategoryDetailsAdgenericrepository == null)
                    this.CategoryDetailsAdgenericrepository = new GenericRepository<CategoryDetailsAd>(context);
                return CategoryDetailsAdgenericrepository;
            }
        }
        private GenericRepository<ConfigEmail> ConfigEmailgenericrepository;
        public GenericRepository<ConfigEmail> ConfigEmailGenericRepository
        {
            get
            {
                if (this.ConfigEmailgenericrepository == null)
                    this.ConfigEmailgenericrepository = new GenericRepository<ConfigEmail>(context);
                return ConfigEmailgenericrepository;
            }
        }
        private GenericRepository<ConfigSms> ConfigSmsgenericrepository;
        public GenericRepository<ConfigSms> ConfigSmsGenericRepository
        {
            get
            {
                if (this.ConfigSmsgenericrepository == null)
                    this.ConfigSmsgenericrepository = new GenericRepository<ConfigSms>(context);
                return ConfigSmsgenericrepository;
            }
        }
        private GenericRepository<ContactUs> ContactUsgenericrepository;
        public GenericRepository<ContactUs> ContactUsGenericRepository
        {
            get
            {
                if (this.ContactUsgenericrepository == null)
                    this.ContactUsgenericrepository = new GenericRepository<ContactUs>(context);
                return ContactUsgenericrepository;
            }
        }
        private GenericRepository<Favorties> Favortiesgenericrepository;
        public GenericRepository<Favorties> FavortiesGenericRepository
        {
            get
            {
                if (this.Favortiesgenericrepository == null)
                    this.Favortiesgenericrepository = new GenericRepository<Favorties>(context);
                return Favortiesgenericrepository;
            }
        }
        private GenericRepository<Menu> Menugenericrepository;
        public GenericRepository<Menu> MenuGenericRepository
        {
            get
            {
                if (this.Menugenericrepository == null)
                    this.Menugenericrepository = new GenericRepository<Menu>(context);
                return Menugenericrepository;
            }
        }
        private GenericRepository<Payment> Paymentgenericrepository;
        public GenericRepository<Payment> PaymentGenericRepository
        {
            get
            {
                if (this.Paymentgenericrepository == null)
                    this.Paymentgenericrepository = new GenericRepository<Payment>(context);
                return Paymentgenericrepository;
            }
        }
        private GenericRepository<PhotoGallery> PhotoGallerygenericrepository;
        public GenericRepository<PhotoGallery> PhotoGalleryGenericRepository
        {
            get
            {
                if (this.PhotoGallerygenericrepository == null)
                    this.PhotoGallerygenericrepository = new GenericRepository<PhotoGallery>(context);
                return PhotoGallerygenericrepository;
            }
        }
        private GenericRepository<ProductType> ProductTypegenericrepository;
        public GenericRepository<ProductType> ProductTypeGenericRepository
        {
            get
            {
                if (this.ProductTypegenericrepository == null)
                    this.ProductTypegenericrepository = new GenericRepository<ProductType>(context);
                return ProductTypegenericrepository;
            }
        }
        private GenericRepository<Report> Reportgenericrepository;
        public GenericRepository<Report> ReportGenericRepository
        {
            get
            {
                if (this.Reportgenericrepository == null)
                    this.Reportgenericrepository = new GenericRepository<Report>(context);
                return Reportgenericrepository;
            }
        }
        private GenericRepository<Rules> Rulesgenericrepository;
        public GenericRepository<Rules> RulesGenericRepository
        {
            get
            {
                if (this.Rulesgenericrepository == null)
                    this.Rulesgenericrepository = new GenericRepository<Rules>(context);
                return Rulesgenericrepository;
            }
        }
        private GenericRepository<SaleAd> SaleAdgenericrepository;
        public GenericRepository<SaleAd> SaleAdGenericRepository
        {
            get
            {
                if (this.SaleAdgenericrepository == null)
                    this.SaleAdgenericrepository = new GenericRepository<SaleAd>(context);
                return SaleAdgenericrepository;
            }
        }
        private GenericRepository<SubCategory> SubCategorygenericrepository;
        public GenericRepository<SubCategory> SubCategoryGenericRepository
        {
            get
            {
                if (this.SubCategorygenericrepository == null)
                    this.SubCategorygenericrepository = new GenericRepository<SubCategory>(context);
                return SubCategorygenericrepository;
            }
        }
        private GenericRepository<Suport> Suportgenericrepository;
        public GenericRepository<Suport> SuportGenericRepository
        {
            get
            {
                if (this.Suportgenericrepository == null)
                    this.Suportgenericrepository = new GenericRepository<Suport>(context);
                return Suportgenericrepository;
            }
        }
        private GenericRepository<TypeOfAd> TypeOfAdgenericrepository;
        public GenericRepository<TypeOfAd> TypeOfAdGenericRepository
        {
            get
            {
                if (this.TypeOfAdgenericrepository == null)
                    this.TypeOfAdgenericrepository = new GenericRepository<TypeOfAd>(context);
                return TypeOfAdgenericrepository;
            }
        }

        public int save()
        {
            return context.SaveChanges();
        }

        private bool Disposed = false;
        protected virtual void dispose(bool disposing)
        {
            if(!this.Disposed)
            {
                if(disposing)
                {
                    context.Dispose();
                }
            }
            this.Disposed = true;
        }
        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
