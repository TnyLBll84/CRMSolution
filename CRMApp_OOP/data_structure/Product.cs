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
        private static int nextProductID = 0;
        #endregion

        #region Constructors
        public Product()
        {
            nextProductID++;
            Id = nextProductID;
        }

        public Product(string name, decimal price) : this()
        {
            Name = name;
            Price = price;
        }
        #endregion

        #region private Fields (Data Members)
        #endregion

        #region public Properties (Data Members
        public int Id { get; private set; }

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }
        #endregion

        #region Methods (Behavior)
        public decimal GetPriceAfterDiscount(decimal discountRate) => Price * (1 - discountRate);
        #endregion
    }
}
