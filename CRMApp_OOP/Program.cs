using System.Collections;

namespace CRMApp_OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // create an instance (object)
            Customer cust1obj = new Customer();

            cust1obj.id = 1;
            cust1obj.name.FirstName = "Tony";
            cust1obj.name.LastName = "Bell";
            cust1obj.age = 40;
            cust1obj.salary = 150m;
            cust1obj.isMarried = true;

            //Console.WriteLine($"ID: {cust1obj.id} First Name: {cust1obj.name.FirstName} "
            //    + $"Last Name: {cust1obj.name.LastName} Age: {cust1obj.age} Salary: {cust1obj.salary}K Married: {cust1obj.isMarried}" + "\n\n");

            Customer cust2obj = new Customer();

            cust2obj.id = 2;
            cust2obj.name.FirstName = "Flash";
            cust2obj.name.LastName = "Gordon";
            cust2obj.age = 48;
            cust2obj.salary = 200m;
            cust2obj.isMarried = true;

            Customer cust3obj = new Customer();
            cust3obj.id = 3;
            cust3obj.name.FirstName = "Dick";
            cust3obj.name.LastName = "Tracy";
            cust3obj.age = 39;
            cust3obj.salary = 180m;
            cust3obj.isMarried = false;

            // Static Array Example
            //Customer[] customers = new Customer[2];
            //customers[0] = cust1obj;
            //customers[1] = cust2obj;


            //foreach (Customer cust in customers)
            //{
            //    Console.WriteLine($"ID: {cust.id} First Name: {cust.name.FirstName} "
            //    + $"Last Name: {cust.name.LastName} Age: {cust.age} Salary: {cust.salary}K Married: {cust.isMarried}");
            //}

            //Create dynamic using builtin class ArrayList (Dynamic Array Example)
            ArrayList customers = new ArrayList();
            customers.Add(cust1obj);
            customers.Remove(cust2obj);
            customers.Add(cust3obj);

            // Update Customer1 info in the array
            Customer obj = (Customer)customers[0];
            obj.age = 23;

            foreach (Customer customer in customers)
            {
                Console.WriteLine($"ID: {customer.id} First Name: {customer.name.FirstName} "
                + $"Last Name: {customer.name.LastName} Age: {customer.age} Salary: {customer.salary}K Married: {customer.isMarried}");
            }



        }

        
    }
}
