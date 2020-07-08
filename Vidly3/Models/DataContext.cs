using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Vidly3.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("Vidly")
        {
            Database.SetInitializer<DataContext>(null);
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}