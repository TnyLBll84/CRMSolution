using CRM_DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CRM_DB
{
    internal class CRMDBService
    {
        private readonly CRMDBContext _context;
        public CRMDBService()
        {
            _context = new CRMDBContext();
        }

        #region Blog CRUD Operations
        public void AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("Blog is empty");

            this._context.Customers.Add(customer);
            this._context.SaveChanges();
        }
        public List<Customer> GetAllCustomers()
        {

            // Recall: select * from blogs (SQL)

            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Customer> query = from blog in this._context.Customers
                                     select blog;

            //Execute the Query statement
            List<Customer> blogs = query.ToList();

            return blogs;
        }

        public Customer GetCustomerByID(int id)
        {
            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Customer> query = from customer in this._context.Customers
                                     where customer.CustId == id
                                     select customer;

            //Execute the Query statement
            Customer customerObj = query.FirstOrDefault();

            return customerObj;

        }
        public void UpdateCustomer(Customer newCustomer)
        {
            this._context.Customers.Update(newCustomer);
            this._context.SaveChanges();
        }
        public void DeleteCustomer(Customer customer)
        {
            this._context.Customers.Remove(customer);
            this._context.SaveChanges();
        }
        #endregion


    }
}