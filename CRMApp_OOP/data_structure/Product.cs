using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_OOP
{
    internal class Product
    {
        #region Static Data Members
        static private int nextProductID = 0;
        #endregion

        #region Constructors
        public Product()
        {
            this.Id = ++nextProductID;

        }
        public Product(string name, decimal price) : this()   // :this() is calling the default constructor
        {
            this.Name = name;
            this.Price = price;
        }
        #endregion

        #region private Fields (Data Members)
        #endregion

        #region public Properties (Data Members)
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        #endregion

        #region Methods (Behavior)
        public decimal GetPriceAfterDiscount(decimal discountRate)
        {
            return Price * (1 - discountRate);
        }
        #endregion

    }
}
