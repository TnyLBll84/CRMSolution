using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMApp_OOP
{
    internal struct FullName
    {
        public string FirstName;
        public string LastName;
    }
    internal class Customer
    {
        // Data Members
        public int id;
        public FullName name;
        public int age;
        public decimal salary;
        public bool isMarried;

    }
}
