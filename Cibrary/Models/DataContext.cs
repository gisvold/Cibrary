using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace Cibrary.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DataContext")
        {
        }

        public DataContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        // This method ensures that user names are always unique
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if (entityEntry.State == EntityState.Added)
            {
                User user = entityEntry.Entity as User;
                // Check for uniqueness of user name
                if (user != null && Users.Where(u => u.UserName.ToUpper() == user.UserName.ToUpper()).Count() > 0)
                {
                    var result = new DbEntityValidationResult(entityEntry, new List<DbValidationError>());
                    result.ValidationErrors.Add(new DbValidationError("User", "Brukernavnet må være unikt."));
                    return result;
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserSecret> Secrets { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>().HasKey(q => new {q.BookId, q.UserId});
            
            modelBuilder.Entity<Book>()
            .HasMany(q => q.Categories)
            .WithMany(q => q.Books)
            .Map(q =>
            {
                q.ToTable("BookCategories");
                q.MapLeftKey("BookId");
                q.MapRightKey("CategoryId");
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Cibrary.Models.Book> Books { get; set; }

        public DbSet<Cibrary.Models.Category> Category { get; set; }
    }
}