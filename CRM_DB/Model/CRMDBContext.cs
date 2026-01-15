using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB
{
    internal class CRMDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Product> Products { get; set; }

        //Configuring ConnectionString that will be used by BloggingConctext to connect
        // to the backend database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=nomoremrwifi-su;Initial Catalog=CRM_DB;Integrated Security=True;Encrypt=False");
        }
    }
}
