using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB.Model
{
    internal class CRMDBContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }


        //Configuring ConnectionString that will be used by BloggingConctext to connect
        // to the backend database
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=nomoremrwifi-su;Initial Catalog=CRMDB;Integrated Security=True;Encrypt=False");
        }
    }
}
