using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Projectcore.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ConfigEmail> ConfigEmails { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<CategoryDetails> CategoryDetails { get; set; }
        public DbSet<ConfigSms> ConfigSms { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Favorties> Favorties { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<PhotoGallery> PhotoGalleries { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Rules> Rules { get; set; }
        public DbSet<SaleAd> SaleAds { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Suport> Suports { get; set; }
        public DbSet<TypeOfAd> TypeOfAds { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Payment> payments { get; set; }
        public DbSet<CategoryDetailsAd> CategoryDetailsAd { get; set; }
    }
}