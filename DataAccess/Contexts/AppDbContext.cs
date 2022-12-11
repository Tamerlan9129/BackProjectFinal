using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contexts
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<HomeMainSlider> HomeMainSlider { get; set; }
        public DbSet<AboutPhoto> About { get; set; }
        public DbSet<AboutPhoto> AboutPhoto { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<MedicalDepartment> MedicalDepartments { get; set; }
        public DbSet<OurVision> OurVision { get; set; }
        public DbSet<WhyChooseUs> WhyChooseUs { get; set; }
        public DbSet<HomeVideo> HomeVideo { get; set; }
        public DbSet<LastestNews> LastestNews { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionCategory> QuestionCategories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Pricing> Pricing { get; set; }

        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
    }
}
