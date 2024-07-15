using Microsoft.EntityFrameworkCore;

namespace ProductManagmentApp.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = "de", Name = "Dairy, eggs"},   
                new Category { Id = "bp", Name = "Bio products"},   
                new Category { Id = "df", Name = "Dry food"},   
                new Category { Id = "ab", Name = "Alcoholic beverage"},   
                new Category { Id = "nad", Name = "Non-alcoholic drink"},   
                new Category { Id = "ss", Name = "Sweets & snacks"},   
                new Category { Id = "hy", Name = "Hygiene"},   
                new Category { Id = "hi", Name = "Household items"},   
                new Category { Id = "pf", Name = "Pet food"}
            );
        }
    }
}
