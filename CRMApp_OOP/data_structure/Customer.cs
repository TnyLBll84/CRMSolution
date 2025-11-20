using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_OOP
{
    // Value Type Data Strcuture
    internal struct FullName
    {
        public string firstName;
        public string lastName;
    }

    internal class Customer
    {

        #region Constructors
        // default Constructor (its a method that has same class name)
        public Customer()
        {
            // this keyword referes to the new constructed object
            //this.Id = 0;
            //this.Age = 20;
            //this.Name = new FullName();
        }


        // overloaded Constructor
        public Customer(int id, string firstName, string lastName, int age)
        {
            // this keyword referes to the new constructed object
            this.Id = id;
            this.Age = age;
            this.Name.firstName = firstName;
            this.Name.lastName = lastName;
        }
        #endregion

        #region Static Data Members
        // Global Variable conrolled by Customer Class
        public static int nextCustomerID = 0;
        #endregion

        #region Private Instance Data Members (Fields)
        private FullName name;
        private int age;
        #endregion

        # region Public Instance Members (Properties)
        public int Id { get; set; }

        public ref FullName Name => ref name;

        public int Age
        {
            get { return age; }
            // Age Range [20,65]
            set
            {
                if (value < 20 || value > 65)
                {
                    //Generat Execption 
                    throw new ArgumentException("Age must be within Range 20 and 65");
                }
                else
                {
                    age = value;
                }
            }

        }
        #endregion
    }
}
