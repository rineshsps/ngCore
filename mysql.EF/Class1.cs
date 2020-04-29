using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace mysql.EF
{


    public class LibraryContext : DbContext

    {
     

        
        public DbSet<category> category { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("persistsecurityinfo=True;server=localhost;user id=root;password=password1;database=sakila;allowuservariables=True");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<category>(entity =>
        //    {
        //        entity.HasKey(e => e.category_id);
        //        entity.Property(e => e.name);
        //        entity.Property(d => d.last_update);
        //    });
        //}
    }
}
