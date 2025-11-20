using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_OOP
{
    internal class Complain
    {
        #region Static Data Members
        static private int nextComplainID = 0;
        #endregion

        #region Constructors
        public Complain()
        {
            this.Id = ++nextComplainID;

        }
        public Complain(int customerID, int productID, string description) : this()   // :this() is calling the default constructor
        {
            this.CustomerId = customerID;
            this.ProductId = productID;
            this.Description = description;

        }
        #endregion

        #region private Fields (Data Members)
        #endregion

        #region public Properties (Data Members)
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public int ProductId { get; set; }

        public string Description { get; set; }
        #endregion

        #region Methods (Behavior)
        // Get Customer Name using Customer ID
        // return type of string --> customer name
        // Parameters:
        // collection (array) of customers   ArrayList 
        public string GetCustomerNameByID(ArrayList customers)
        {
            // TODO1: loop inside the customers array to get each customer object
            string customerName = string.Empty;
            foreach (Customer cust in customers)
            {
                // TODO2: foreach customer object,
                // get the customer id from the object
                //int custID= customer.Id;
                if (this.CustomerId == cust.Id)
                {
                    // TODO3: compare cutomerid inside the complain's object
                    // with customerid from array object
                    //if found, get back the customer name
                    //and break the loop
                    customerName = $"{cust.Name.firstName} {cust.Name.lastName}";
                }

            }
            // TODO4: if found, return back the customer name
            return customerName;
        }


        //Get Product Name using Product ID
        public string GetProductNameByID(ArrayList products)
        {
            // TODO1: loop inside the customers array to get each customer object
            string productName = string.Empty;
            foreach (Product prod in products)
            {
                // TODO2: foreach customer object,
                // get the customer id from the object
                //int custID= customer.Id;
                if (this.CustomerId == prod.Id)
                {
                    // TODO3: compare cutomerid inside the complain's object
                    // with customerid from array object
                    //if found, get back the customer name
                    //and break the loop
                    productName = $"{prod.Name}";
                }

            }
            // TODO4: if found, return back the customer name
            return productName;
        }


        #endregion  

    }
}
