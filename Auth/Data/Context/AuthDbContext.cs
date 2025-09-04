using Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Data.Context
{
    public class AuthDbContext:DbContext
    {
        #region contructor
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }
        #endregion

        #region properties
        public DbSet<User> Users { get; set; } 
        #endregion

        #region methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            foreach (var item in modelBuilder.Model.GetEntityTypes())
            { 
                modelBuilder.Entity(item.ClrType).Property(nameof(BaseEntity.Id)).HasDefaultValueSql("NEWID()");
            }

        } 
        #endregion
         
    }
}
