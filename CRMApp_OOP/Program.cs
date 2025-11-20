using System.Collections;
using System.Net.Http.Headers;

namespace CRMApp_OOP
{

    internal class Program
    {
        #region Global Static Data Members
        static ArrayList customers = new ArrayList();
        static ArrayList products = new ArrayList();
        static ArrayList complains = new ArrayList();
        #endregion


        #region Application EntryPoint (Main Menue)
        static void Main(string[] args)
        {
            //Build CRM Menu
            // Build Menu System
            string welcomeMessage = "   Welcome to the CRM System";
            string decorativeLine = new string('*', welcomeMessage.Length + 3);
            //Console.ForegroundColor = ConsoleColor.Green;
            do
            {
                Console.WriteLine(decorativeLine);
                Console.WriteLine(welcomeMessage);
                Console.WriteLine(decorativeLine);
                Console.WriteLine("1. Add New Customer");
                Console.WriteLine("2. Update Customer");
                Console.WriteLine("3. Delete Customer");
                Console.WriteLine("4. Display All Customers");
                Console.WriteLine(decorativeLine);
                Console.WriteLine("5. Add New Product");
                Console.WriteLine("6. Display All Products");
                Console.WriteLine(decorativeLine);
                Console.WriteLine("7. Add New Complain");
                Console.WriteLine("8. Display All Complains");
                Console.WriteLine(decorativeLine);
                Console.WriteLine("9 Exit");
                Console.Write("Please select an option (1-9): ");
                try
                {
                    /////for demonstration purpose only
                    //int choice; //Decare choice variable
                    //bool isValidChoice = int.TryParse(Console.ReadLine(),out choice); //Try to parse user input
                    ///////////////////////
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            AddCustomer();
                            break;
                        case 2:
                            UpdateCustomerInfo();
                            break;
                        case 3:
                            DeleteCustomer();
                            break;
                        case 4:
                            DiplayCutomers();
                            break;
                        case 5:
                            AddProduct();
                            break;
                        case 6:
                            DisplayProducts();
                            break;
                        case 7:
                            AddComplain();
                            break;
                        case 8:
                            DisplayComplains();
                            break;
                        case 9:

                            Console.WriteLine("Goodbye!");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option (1-5).");
                            break;


                    }
                }
                catch (FormatException)  //Specific Exception Handler
                {
                    //Catch Format Exception and display user friendly message
                    Console.WriteLine("Input format is incorrect..Please make sure to type onlty digits");
                }
                catch (OverflowException)  //Specific Exception Handler
                {
                    //Catch Overflow Exception and display user friendly message
                    Console.WriteLine("Input number is too large or too small. ");
                }
                catch (IndexOutOfRangeException)
                {

                }
                catch (Exception ex)  //General Exception Handler
                {
                    //Cahtch any exception that occurs and display the Technical error message
                    // Laster we'll learn how to log this error to a file
                    Console.WriteLine("Error: " + ex.Message);
                }


                Console.WriteLine("Pess enter to continue ...");
                Console.ReadLine();
                Console.Clear();

            } while (true);
        }
        #endregion


        #region Customer CRUD Operations
        static void AddCustomer()
        {
            // TODO 1: Create new Customer object using Default Constructor

            // Use default constructor
            Customer custObj = new Customer();

            //use overload constructor
            //Customer custObj = new Customer(0,"emad","elfaramawi", 20);

            // TODO 2: Get customer info. form end user and populate
            //         the new customer object with data

            // increment customerId by 1
            // Access Static data member (nextCustomerID) by using the Class
            // name
            // Syntax to access static members:    classname.static_member_name
            Customer.nextCustomerID++;
            custObj.Id = Customer.nextCustomerID;

            Console.Write("Enter First Name name: ");
            //string firstName = Console.ReadLine();
            //custObj.name.firstName = firstName;
            custObj.Name.firstName = Console.ReadLine();


            Console.Write("Enter Last Name name: ");
            string lastName = Console.ReadLine();
            custObj.Name.lastName = lastName;


            Console.Write("Enter customer age: ");
            int age = int.Parse(Console.ReadLine());
            custObj.Age = age;


            // TODO 3: Add customer object to the array of customers
            customers.Add(custObj);

        }
        static void UpdateCustomerInfo()
        {
            //TODO1: get customer first name
            Console.Write("Ener Customer Name: ");
            string firstName = Console.ReadLine();

            //TODO2: Search in customers' array for the customer object that has first name

            bool isFound = false;
            foreach (Customer cust in customers)
            {
                if (cust.Name.firstName.ToLower() == firstName.ToLower())
                {
                    //TODO3: If found, ask for the new name and update the found object with the new name
                    isFound = true;
                    Console.Write("Ener New Customer Name: ");
                    cust.Name.firstName = Console.ReadLine();
                    Console.WriteLine($"Done Update Customer information");
                    break;

                }

            }
            if (!isFound)
            {
                //TODO4: If not found, notify the end user.
                Console.WriteLine("Sorry Customer Not Found ...");

            }


        }
        static void DeleteCustomer()
        {
            //TODO1: get customer first name
            Console.Write("Ener Customer Name: ");
            string firstName = Console.ReadLine();

            //TODO2: Search in customers' array for the customer object that has first name

            bool isFound = false;
            foreach (Customer cust in customers)
            {
                if (cust.Name.firstName.ToLower() == firstName.ToLower())
                {
                    //TODO3: If found, remove the found customer object form the customers array
                    isFound = true;

                    customers.Remove(cust);

                    Console.WriteLine($"Done Deleting Customer ");
                    break;

                }

            }
            if (!isFound)
            {
                //TODO4: If not found, notify the end user.
                Console.WriteLine("Sorry Customer Not Found ...");

            }
        }

        static void DiplayCutomers()
        {
            Console.WriteLine("Customer List:");


            ////Header Columns

            Console.WriteLine("".PadLeft(29, '-'));
            Console.Write("| ");
            Console.Write("ID".PadRight(5));
            Console.Write(" | ");
            Console.Write("Name".PadRight(10));
            Console.Write(" | ");
            Console.Write("Age".PadRight(5));
            Console.WriteLine("| ");
            Console.WriteLine("".PadLeft(29, '-'));


            foreach (Customer cust in customers)
            {
                ////Data Rows
                Console.Write("| ");

                Console.Write(cust.Id.ToString().PadRight(5));
                Console.Write(" | ");
                Console.Write(cust.Name.firstName.PadRight(10));
                Console.Write(" | ");
                Console.Write(cust.Age.ToString().PadRight(5));
                Console.WriteLine("| ");
            }
        }
        #endregion


        #region Product CRUD Operations
        static void AddProduct()
        {
            // TODO1: Collection information about
            //        Product from end user
            Console.Write("Please type Product Name: ");
            string prouctName = Console.ReadLine();

            Console.Write("Please type Product Price: ");
            decimal prouctPrice = decimal.Parse(Console.ReadLine());

            //TODO2: construct product object using overloaded constructor
            Product prodObj = new Product(prouctName, prouctPrice);

            //TODO3: Add new object to the Products Array
            products.Add(prodObj);
        }

        static void DisplayProducts()
        {

            ////Header Columns

            Console.WriteLine("".PadLeft(29, '-'));
            Console.Write("| ");
            Console.Write("ID".PadRight(5));
            Console.Write(" | ");
            Console.Write("Name".PadRight(10));
            Console.Write(" | ");
            Console.Write("Price".PadRight(10));
            Console.Write(" | ");
            Console.Write("Discounted Price".PadRight(10));
            Console.WriteLine("| ");
            Console.WriteLine("".PadLeft(29, '-'));


            foreach (Product prod in products)
            {
                ////Data Rows
                Console.Write("| ");

                Console.Write(prod.Id.ToString().PadRight(5));
                Console.Write(" | ");
                Console.Write(prod.Name.PadRight(10));
                Console.Write(" | ");
                Console.Write(prod.Price.ToString().PadRight(10));
                Console.Write("| ");
                Console.Write(prod.GetPriceAfterDiscount(0.2m).ToString().PadRight(10));
                Console.WriteLine("| ");
            }
        }
        #endregion


        #region Complain CRUD Operations
        static void AddComplain()
        {
            // TODO1: Collection information about
            //        Complain from end user
            Console.Write("Please type Customer ID: ");
            int customerID = int.Parse(Console.ReadLine());

            Console.Write("Please type Product ID: ");
            int prodcutID = int.Parse(Console.ReadLine());

            Console.Write("Please type complain description: ");
            string description = Console.ReadLine();


            //TODO2: construct Complain object using overloaded constructor
            Complain complainObj = new Complain(customerID, prodcutID, description);

            //TODO3: Add new object to the Compalins Array
            complains.Add(complainObj);
        }

        static void DisplayComplains()
        {

            ////Header Columns

            Console.WriteLine("".PadLeft(29, '-'));
            Console.Write("| ");
            Console.Write("ID".PadRight(5));
            Console.Write(" | ");
            Console.Write("Customer Name".PadRight(10));
            Console.Write(" | ");
            Console.Write("Product Name".PadRight(10));
            Console.Write(" | ");
            Console.Write("Description".PadRight(10));
            Console.WriteLine("| ");
            Console.WriteLine("".PadLeft(29, '-'));


            foreach (Complain comp in complains)
            {
                ////Data Rows
                Console.Write("| ");

                Console.Write(comp.Id.ToString().PadRight(5));
                Console.Write(" | ");
                Console.Write(comp.GetCustomerNameByID(customers).PadRight(10));
                Console.Write(" | ");
                Console.Write(comp.GetProductNameByID(products).PadRight(10));
                Console.Write("| ");
                Console.Write(comp.Description.PadRight(10));
                Console.WriteLine("| ");
            }
        }
        #endregion
    }
}
