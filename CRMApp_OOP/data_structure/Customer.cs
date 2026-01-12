using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_OOP
{

    /*TASKS TO STILL BE DONE
     * I NEED TO ADD SALARIES
     * I NEED TO ADD MARRIED STATUS*/


    // Value Type Data Strcuture
    internal struct FullName
    {
        public string firstName;
        public string lastName;
    }

    internal class Customer : IComparable<Customer>
    {

        #region Static Data Members
        private static int nextCustomerID = 0;
        #endregion

        #region Private Instance
        #endregion

        #region Constructors
        public Customer()
        {
            // default ctor
        }

        public Customer(string firstName = "", string lastName = "", int age = 30, bool married = false, decimal salary = 0m)
        {
            Id = Interlocked.Increment(ref nextCustomerID);
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Married = married;
            Salary = salary;
        }
        #endregion

        #region IComparable Implementation
        public int CompareTo(Customer? other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other), "Compared customer cannot be null");
            }
            return this.FirstName.CompareTo(other.FirstName);
        }
        #endregion

        #region Properties
        public int Id { get; private set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        private int age;

        public int Age
        {
            get => age;
            set
            {
                if (value < 20 || value > 65)
                    throw new ArgumentException("Age must be within range 20 and 65");
                age = value;
            }
        }

        private decimal salary;

        public decimal Salary
        {
            get => salary;
            set
            {
                if (value < 0m)
                    throw new ArgumentException("Salary cannot be negative");
                salary = value;
            }
        }

        public bool Married { get; set; }


        #endregion
    }
}




