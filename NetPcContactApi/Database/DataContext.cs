using Microsoft.EntityFrameworkCore;
using NetPcContactApi.Models.Categories;
using NetPcContactApi.Models.User;

namespace NetPcContactApi.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasIndex(u => u.Email)
             .IsUnique();
            modelBuilder.Entity<ContactCategory>().HasData(
                new ContactCategory { ContactCategoryId = 1, Name = "Prywatny" },
                new ContactCategory { ContactCategoryId = 2, Name = "Służbowy" },
                new ContactCategory { ContactCategoryId = 3, Name = "Inny" }
            );
            modelBuilder.Entity<ContactSubCategory>().HasKey(c => c.ContactSubCategoryId);
            modelBuilder.Entity<ContactSubCategory>().HasData(
         new ContactSubCategory { ContactSubCategoryId = 1, ContactCategoryId = 2, Name = "Szef" },
         new ContactSubCategory { ContactSubCategoryId = 2, ContactCategoryId = 2, Name = "Księgowa" },
         new ContactSubCategory { ContactSubCategoryId = 3, ContactCategoryId = 3, Name = "Nauczyciel" }
     );
            modelBuilder.Entity<ContactSubCategory>().HasIndex(c => c.ContactSubCategoryId).IsUnique();
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<ContactCategory> ContactCategories => Set<ContactCategory>();
        public DbSet<ContactSubCategory> SubContactCategories => Set<ContactSubCategory>();
    }
}
