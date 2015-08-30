using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyTop5.Models
{
    public class mytop5apiContext : IdentityDbContext<ApplicationUser>
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public mytop5apiContext() : base("name=mytop5apiContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public static mytop5apiContext Create()
        {
            return new mytop5apiContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // base.OnModelCreating( modelBuilder );

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            base.OnModelCreating(modelBuilder);
        }


        //public System.Data.Entity.DbSet<mytop5.api.Models.ApplicationUser> Users { get; set; }
        public System.Data.Entity.DbSet<MyTop5.Models.Top5List> Top5List { get; set; }
        public System.Data.Entity.DbSet<MyTop5.Models.Top5ListItem> Top5ListItem { get; set; }
        public System.Data.Entity.DbSet<MyTop5.Models.Tag> Tags { get; set; }
    }
}
