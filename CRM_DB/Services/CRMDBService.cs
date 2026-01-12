using Microsoft.EntityFrameworkCore;
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

        #region CRM CRUD Operations
        public void AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("Customer is empty");

            this._context.Customers.Add(customer);
            this._context.SaveChanges();
        }
        public List<Customer> GetAllCustomers()
        {

            // Recall: select * from customers (SQL)

            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Customer> query = from customer in this._context.Customers
                                     select customer;

            //Execute the Query statement
            List<Customer> customers = query.ToList();

            return customers;
        }

        public Customer GetCustomerByID(int id)
        {
            // LINQ Query
            // Construct Query Statement in C# using LINQ
            Customer customerObj = this._context
                                       .Customers
                                       .Include("Complaints") // Using Navigatinal Property
                                       .Where(c => c.CustomerId == id).FirstOrDefault();


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

        #region CRM Product Operations
        public void AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException("Product is empty");

            this._context.Products.Add(product);
            this._context.SaveChanges();
        }
        public List<Product> GetAllProducts()
        {

            // Recall: select * from products (SQL)

            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Product> query = from product in this._context.Products
                                         select product;

            //Execute the Query statement
            List<Product> products = query.ToList();

            return products;
        }

        public Product GetProductByID(int id)
        {
            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Product> query = from product in this._context.Products
                                        where product.ProductId == id
                                        select product;

            //Execute the Query statement
            Product productObj = query.FirstOrDefault();

            return productObj;
        }
        public void UpdateProduct(Product newProduct)
        {
            this._context.Products.Update(newProduct);
            this._context.SaveChanges();
        }
        public void DeleteProduct(Product product)
        {
            this._context.Products.Remove(product);
            this._context.SaveChanges();
        }
        #endregion

        #region CRM Appointment Operations
        public void AddAppointment(Appointment appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException("Appointment is empty");

            this._context.Appointments.Add(appointment);
            this._context.SaveChanges();
        }
        public List<Appointment> GetAllAppointments()
        {

            // Recall: select * from Appointments (SQL)

            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Appointment> query = from appointment in this._context.Appointments
                                            select appointment;

            //Execute the Query statement
            List<Appointment> appointments = query.ToList();

            return appointments;
        }
        public Appointment GetAppointmentByID(int id)
        {
            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Appointment> query = from appointment in this._context.Appointments
                                            where appointment.AppointmentId == id
                                            select appointment;

            //Execute the Query statement
            Appointment appointmentObj = query.FirstOrDefault();

            return appointmentObj;

        }
        public void UpdateAppointment(Appointment newAppointment)
        {
            this._context.Appointments.Update(newAppointment);
            this._context.SaveChanges();
        }
        public void DeleteAppointment(Appointment appointment)
        {
            this._context.Appointments.Remove(appointment);
            this._context.SaveChanges();
        }
        #endregion

        #region CRM Complaint Operations
        public void AddComplaint(Complaint complaint)
        {
            if (complaint == null)
                throw new ArgumentNullException("Complaint is empty");

            this._context.Complaints.Add(complaint);
            this._context.SaveChanges();
        }
        public List<Complaint> GetAllComplaints()
        {

            // Recall: select * from Complaints (SQL)

            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Complaint> query = from complaint in this._context.Complaints
                                          select complaint;

            //Execute the Query statement
            List<Complaint> complaints = query.ToList();

            return complaints;
        }

        public Complaint GetComplaintByID(int id)
        {
            // LINQ Query
            // Construct Query Statement in C# using LINQ
            IQueryable<Complaint> query = from complaint in this._context.Complaints
                                          where complaint.ComplaintId == id
                                          select complaint;

            //Execute the Query statement
            Complaint complaintObj = query.FirstOrDefault();

            return complaintObj;

        }
        public void UpdateComplaint(Complaint newComplaint)
        {
            this._context.Complaints.Update(newComplaint);
            this._context.SaveChanges();
        }
        public void DeleteComplaint(Complaint complaint)
        {
            this._context.Complaints.Remove(complaint);
            this._context.SaveChanges();
        }
        #endregion


    }
}