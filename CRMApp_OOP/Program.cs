using System.Globalization;
using System.Collections;
using System.Net.Http.Headers;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Microsoft.VisualBasic;
using System.ComponentModel.Design;

namespace CRMApp_OOP
{
    internal class Program
    {
        #region Global Static Data Members
        // Create a list to store all customer objects
        static List<Customer> customers = new List<Customer>();

        // Create a list to store all product objects
        static List<Product> products = new List<Product>();

        // Create a list to store all complaint objects
        static List<Complaint> complaints = new List<Complaint>();

        // Create a logger instance so the program can output logs to the console
        static ILogger Logger = new ConsoleLogger();

        //static Queue<Complaint> normalComplaintQueue = new Queue<Complaint>();
        static PriorityQueue<Complaint, int> urgentComplaintQueue = new PriorityQueue<Complaint, int>();

        static LinkedList<Appointment> appointmentList = new LinkedList<Appointment>();

        #endregion

        #region Applicaton EntryPoint (Main Menu)
        static void Main(string[] args)
        {
            // Step 1: Set the text color to green so the menu looks visually appealing
            Console.ForegroundColor = ConsoleColor.Green;

            // Step 2: Create the welcome message text
            string welcomeMessage = "   Welcome to the CRM System";

            // Step 3: Build a decorative line of asterisks that matches the message length + 4 extra for padding
            string decorativeLine = new string('*', welcomeMessage.Length + 4);

            // Step 4: Begin an infinite loop that keeps the main menu running until user exits
            while (true)
            {
                try
                {
                    // Step 5: Display the decorative line above the welcome header
                    Console.WriteLine(decorativeLine);

                    // Step 6: Display the centered welcome message
                    Console.WriteLine(welcomeMessage);

                    // Step 7: Display the decorative line below the welcome header
                    Console.WriteLine(decorativeLine);

                    // Step 8: Display the main menu section title with spacing
                    Console.WriteLine("\n\t MAIN MENU:\n");

                    // Step 9: Show menu option 1 for customer operations
                    Console.WriteLine("   1. Customer Information");

                    // Step 10: Show menu option 2 for product operations
                    Console.WriteLine("   2. Product Details");

                    // Step 11: Show menu option 3 for complaint operations
                    Console.WriteLine("   3. Complaint Details");

                    // Step 12: Show menu option 4 which exits the program
                    Console.WriteLine("   4. Appointment Information\n");

                    // Step 12: Show menu option 4 which exits the program
                    Console.WriteLine("   5. Exit Program\n");

                    // Step 13: Read the user's menu selection and convert it into an integer
                    int mainChoice = ReadInt("Enter your choice (1 - 5): ");

                    // Step 14: Clear the screen so the next menu or result displays cleanly
                    Console.Clear();

                    // Step 15: Determine which menu or action to open based on user's choice
                    switch (mainChoice)
                    {
                        // Step 16: If the user selects 1, open the customer menu
                        case 1:
                            CustomerMenu();
                            break;

                        // Step 17: If the user selects 2, open the product menu
                        case 2:
                            ProductMenu();
                            break;

                        // Step 18: If the user selects 3, open the complaint menu
                        case 3:
                            ComplaintMenu();
                            break;

                        case 4:
                            AppointmentMenu();
                            break;

                        // Step 19: If the user selects 4, print goodbye and return to exit the program
                        case 5:
                            Console.WriteLine("Goodbye!");
                            return;
                    }

                    // Step 20: Clear the screen after completing any menu option
                    Console.Clear();
                }
                catch (Exception catchAll)
                {
                    // Step 21: Change the text color to red to highlight an error occurred
                    Console.ForegroundColor = ConsoleColor.Red;

                    // Step 22: Display the caught error message to the user
                    Console.WriteLine("Error: " + catchAll.Message);

                    // Step 23: Reset console colors back to normal
                    Console.ResetColor();

                    // Step 24: Wait for user to press Enter before continuing
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();

                    // Step 25: Clear the console to return to a clean state
                    Console.Clear();



                }
            }
        }
        #endregion

        #region Front Menu Choices
        static void CustomerMenu()
        {
            // Step 1: Begin a loop so the customer menu keeps repeating until the user chooses to exit
            while (true)
            {
                // Step 2: Display the customer information menu title with spacing for readability
                Console.WriteLine("\n\t CUSTOMER INFORMATION\n");

                // Step 3: Show menu option 1 which allows adding a new customer
                Console.WriteLine("   1. Add New Customer");

                // Step 4: Show menu option 2 which allows updating existing customer data
                Console.WriteLine("   2. Update Customer Info");

                // Step 5: Show menu option 3 which removes a customer record
                Console.WriteLine("   3. Search Customer");

                // Step 6: Show menu option 4 which searches for specific customer details
                Console.WriteLine("   4. View All Customers");

                // Step 7: Show menu option 5 which displays all customers
                Console.WriteLine("   5. Delete Customer");

                // Step 8: Show menu option 6 which exits this menu and returns to main menu
                Console.WriteLine("   \n6. Return to Main Menu\n");

                // Step 9: Read the user's input and convert it to an integer
                int option = ReadInt("Enter your choice (1 - 6): ");

                // Step 10: Clear the screen after reading input so the next output is clean
                Console.Clear();

                // Step 11: Use a switch to determine which menu operation the user selected
                switch (option)
                {
                    // Step 12: If user selected 1, call AddCustomer()
                    case 1:
                        AddCustomer();
                        break;

                    // Step 13: If user selected 2, call UpdateCustomerInfo()
                    case 2:
                        UpdateCustomerInfo();
                        break;

                    // Step 14: If user selected 3, call DeleteCustomer()
                    case 3:
                        DeleteCustomer();
                        break;

                    // Step 15: If user selected 4, call SearchCustomer()
                    case 4:
                        SearchCustomer();
                        break;

                    // Step 16: If user selected 5, call ViewAllCustomers()
                    case 5:
                        ViewAllCustomers();
                        break;

                    // Step 17: If user selected 6, exit this menu and return to main menu
                    case 6:
                        return;
                }

                // Step 18: Pause to allow the user to read the results of their action
                Console.WriteLine("\nPress Enter to continue...");

                // Step 19: Wait for user to press Enter
                Console.ReadLine();

                // Step 20: Clear the screen before looping back to show the menu again
                Console.Clear();
            }
        }

        static void ProductMenu()
        {
            // Step 1: Begin an infinite loop so this menu stays active until the user chooses to exit
            while (true)
            {
                // Step 2: Display the product details menu heading
                Console.WriteLine("\n\t PRODUCT DETAILS\n");

                // Step 3: Show option 1 which allows adding a product for a customer
                Console.WriteLine("   1. Add Customer Product");

                // Step 4: Show option 2 which allows searching for a customer product
                Console.WriteLine("   2. Customer Product Search");

                // Step 5: Show option 3 which displays all products
                Console.WriteLine("   3. View All Products");

                // Step 6: Show option 4 which exits back to the main menu
                Console.WriteLine("   \n4. Return to Main Menu\n");

                // Step 7: Read user choice and convert it to an integer
                int option = ReadInt("Enter your choice (1 - 4): ");

                // Step 8: Clear the screen to prepare for next action
                Console.Clear();

                // Step 9: Use a switch to process the selected option
                switch (option)
                {
                    // Step 10: If user selected 1, run AddProduct()
                    case 1:
                        AddProduct();
                        break;

                    // Step 11: If user selected 2, run SearchProduct()
                    case 2:
                        SearchProduct();
                        break;

                    // Step 12: If user selected 3, run ViewAllProducts()
                    case 3:
                        ViewAllProducts();
                        break;

                    // Step 13: If user selected 4, exit this menu by returning
                    case 4:
                        return;
                }

                // Step 14: Pause so the user can read the results of the selected operation
                Console.WriteLine("\nPress Enter to continue...");

                // Step 15: Wait for enter key before continuing
                Console.ReadLine();

                // Step 16: Clear the console before re-displaying the menu
                Console.Clear();
            }
        }

        static void ComplaintMenu()
        {
            // Step 1: Begin a continuous loop so the complaint menu stays active until the user selects the return option
            while (true)
            {
                // Step 2: Display the complaint section heading with spacing for readability
                Console.WriteLine("\n\t COMPLAINT DETAILS\n");

                // Step 3: Show menu option 1 which allows adding a new customer complaint
                Console.WriteLine("   1. Add Customer Complaint");

                // Step 4: Show menu option 2 which allows searching for a customer complaint
                Console.WriteLine("   2. Search Customer Complaint");

                // Step 5: Show menu option 3 which displays all complaints in the system
                Console.WriteLine("   3. View All Complaints");

                // Step 6: Show menu option 4 which displays all complaints grouped by a specific customer
                Console.WriteLine("   4. View All Complaints By Customer");

                Console.WriteLine("   5. Process Complaint(s)");

                // Step 7: Show menu option 5 which returns from this menu to the main menu
                Console.WriteLine("   \n6. Return to Main Menu\n");

                // Step 8: Read the user's choice for options 1 through 5 and convert the input to an integer
                int option = ReadInt("Enter your choice (1 - 6): ");

                // Step 9: Clear the console screen to prepare for the next action display
                Console.Clear();

                // Step 10: Use a switch statement to handle whichever option the user selected
                switch (option)
                {
                    // Step 11: If the user chooses option 1, call the method that adds a new complaint
                    case 1:
                        AddComplaint();
                        break;

                    // Step 12: If the user chooses option 2, call the method that searches complaints
                    case 2:
                        ComplaintSearch();
                        break;

                    // Step 13: If the user chooses option 3, call the method that displays all complaints
                    case 3:
                        ViewAllComplaints();
                        break;

                    // Step 14: If the user chooses option 4, call the method that displays all complaints for a specific customer
                    case 4:
                        ViewAllComplaintByCustomer();
                        break;

                    case 5:
                        ProcessComplaints();
                        break;

                    // Step 15: If the user chooses option 5, return to exit this menu and return to the main menu
                    case 6:
                        return;
                }

                // Step 16: Tell the user to press Enter so they can read the output before the menu refreshes
                Console.WriteLine("\nPress Enter to continue...");

                // Step 17: Wait for the user to press Enter before continuing
                Console.ReadLine();

                // Step 18: Clear the console to display the menu again cleanly on the next loop iteration
                Console.Clear();
            }
        }

        static void AppointmentMenu()
        {
            // Step 1: Begin a continuous loop so the complaint menu stays active until the user selects the return option
            while (true)
            {
                // Step 2: Display the complaint section heading with spacing for readability
                Console.WriteLine("\n\t APPOINTMENT DETAILS\n");

                // Step 3: Show menu option 1 which allows adding a new customer complaint
                Console.WriteLine("   1. Schedule Appointment");

                // Step 4: Show menu option 2 which allows searching for a customer complaint
                Console.WriteLine("   2. Navigate Appointment(s)");

                //// Step 5: Show menu option 3 which displays all complaints in the system
                //Console.WriteLine("   3. View All Complaints");

                //// Step 6: Show menu option 4 which displays all complaints grouped by a specific customer
                //Console.WriteLine("   4. View All Complaints By Customer");


                //Console.WriteLine("   5. Process Complaint(s)");

                // Step 7: Show menu option 5 which returns from this menu to the main menu
                Console.WriteLine("   \n6. Return to Main Menu\n");

                // Step 8: Read the user's choice for options 1 through 5 and convert the input to an integer
                int option = ReadInt("Enter your choice (1 - 6): ");

                // Step 9: Clear the console screen to prepare for the next action display
                Console.Clear();

                // Step 10: Use a switch statement to handle whichever option the user selected
                switch (option)
                {
                    // Step 11: If the user chooses option 1, call the method that adds a new complaint
                    case 1:
                        ScheduleAppointment();
                        break;

                    // Step 12: If the user chooses option 2, call the method that searches complaints
                    case 2:
                        ModifyAppointment();
                        break;

                    // Step 13: If the user chooses option 3, call the method that displays all complaints
                    case 3:
                        ViewAllAppointments();
                        break;

                    // Step 14: If the user chooses option 4, call the method that displays all complaints for a specific customer
                    case 4:
                        CancelAppointment();
                        break;

                    //case 5:
                    //    ProcessComplaints();
                    //    break;

                    // Step 15: If the user chooses option 5, return to exit this menu and return to the main menu
                    case 6:
                        return;
                }

                // Step 16: Tell the user to press Enter so they can read the output before the menu refreshes
                Console.WriteLine("\nPress Enter to continue...");

                // Step 17: Wait for the user to press Enter before continuing
                Console.ReadLine();

                // Step 18: Clear the console to display the menu again cleanly on the next loop iteration
                Console.Clear();
            }
        }

        #endregion

        #region Input Helpers
        private static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            // Step 1: Declare a variable to hold the converted integer value
            int value = 0;

            // Step 2: Create a boolean flag that will control the input validation loop
            bool valid = false;

            // Step 3: Begin the loop that continues until a valid integer is entered
            while (!valid)
            {
                // Step 4: Display the prompt message and wait for user input
                Console.Write(prompt);

                // Step 5: Read the user’s raw text input from the console
                string input = Console.ReadLine();

                try
                {
                    // Step 6: Attempt to convert the user’s input into an integer
                    value = Convert.ToInt32(input);

                    // Step 7: Check if the integer is within the allowed range
                    if (value >= min && value <= max)
                    {
                        // Step 8: Mark the input as valid since it meets the range requirement
                        valid = true;
                    }
                    else
                    {
                        // Step 9: Inform the user that the number is out of range
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Please enter a number between {min} and {max}.");
                        Console.ResetColor();
                    }
                }
                catch
                {
                    // Step 10: If conversion fails, notify the user that the input is not a valid number
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid number. Please try again.");
                    Console.ResetColor();
                }
            }

            // Step 11: Return the validated integer value
            return value;
        }

        private static decimal ReadDecimal(string prompt, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            // Step 1: Declare a decimal variable to store the converted result
            decimal value = 0;

            // Step 2: Create a boolean flag to control validation
            bool valid = false;

            // Step 3: Start the input validation loop
            while (!valid)
            {
                // Step 4: Display prompt and collect user input
                Console.Write(prompt);
                string input = Console.ReadLine();

                try
                {
                    // Step 5: Convert the user input into a decimal value
                    value = Convert.ToDecimal(input, CultureInfo.InvariantCulture);

                    // Step 6: Check whether the value is within the allowed min and max range
                    if (value >= min && value <= max)
                    {
                        // Step 7: Mark validated input so loop can exit
                        valid = true;
                    }
                    else
                    {
                        // Step 8: Show range error message in red
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Please enter a decimal between {min} and {max}.");
                        Console.ResetColor();
                    }
                }
                catch
                {
                    // Step 9: Notify user that the input is not a valid decimal
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid decimal. Please try again.");
                    Console.ResetColor();
                }
            }

            // Step 10: Return the validated decimal value
            return value;
        }

        private static bool ReadBool(string prompt)
        {
            // Step 1: Begin an infinite loop until the user enters a recognized true or false response
            while (true)
            {
                // Step 2: Display the prompt and read the user’s input
                Console.Write(prompt);
                string input = (Console.ReadLine() ?? string.Empty).Trim().ToLowerInvariant();

                // Step 3: Check if the user typed an affirmative response and return true if so
                if (input == "y" || input == "yes") return true;

                // Step 4: Check if the user typed a negative response and return false if so
                if (input == "n" || input == "no") return false;

                // Step 5: Inform the user that only yes or no responses are allowed
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please answer 'y' or 'n'.");
                Console.ResetColor();
            }
        }

        private static string ReadString(string prompt)
        {
            // Step 1: Display the prompt message before reading user input
            Console.Write(prompt);

            // Step 2: Read the user’s textual response and return an empty string if null
            return Console.ReadLine() ?? string.Empty;
        }
        #endregion

        #region Customer CRUD Operations
        static void AddCustomer()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Add New Customer";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            string firstName = ReadString("Enter First Name: ");
            string lastName = ReadString("Enter Last Name: ");
            int age = ReadInt("Enter customer age: ", 20, 65);
            decimal salary = ReadDecimal("Enter customer salary (0 if none): ", 0m);
            bool married = ReadBool("Is the customer married? (y/n): ");

            Customer cust = new Customer(firstName, lastName, age, married, salary);
            customers.Add(cust);

            Console.WriteLine($"Customer added with ID {cust.Id}");
        }

        static void UpdateCustomerInfo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Update Customer Info";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            #region Old Code Using Find and Lambda Expression
            //string search = ReadString("Enter Customer First Name to update: ");
            //// Step 1: Ask the user to enter the first name of the customer they want to update.
            //// Step 2: The ReadString method collects a text input from the user and stores it in the variable 'search'.
            //// Step 3: This name will be used to search inside the customers list.

            //Customer cust = customers.Find((Customer c) => c.FirstName.Equals(search, StringComparison.OrdinalIgnoreCase));
            //// Step 4: Use the List.Find method to search for a customer object whose FirstName matches the user's input.
            //// Step 5: The lambda expression (Customer c) => ... allows the search to check each customer's first name.
            //// Step 6: The comparison uses StringComparison.OrdinalIgnoreCase to ignore uppercase/lowercase differences.
            //// Step 7: If no matching customer is found, the Find method returns null.

            //if (cust == null)
            //{
            //    Console.WriteLine("Sorry Customer Not Found ...");
            //    return;
            //    // Step 8: If the customer was not found, display a message and exit this method early.
            //    // Step 9: The return statement stops further execution because there is no customer to update.
            //}

            //cust.FirstName = ReadString("Enter New First Name: ");
            //// Step 10: Ask the user for the new first name they want to assign to the customer.
            //// Step 11: The ReadString method reads the user input and assigns it to the customer's FirstName property.

            //cust.LastName = ReadString("Enter New Last Name: ");
            //// Step 12: Ask the user for the new last name and update the LastName property.
            //// Step 13: This ensures the customer's full name is updated properly.

            //cust.Age = ReadInt("Enter New Age: ", 20, 65);
            //// Step 14: Ask the user for a new age using ReadInt.
            //// Step 15: ReadInt validates that the input is an integer AND between the range 20 and 65.
            //// Step 16: If the user enters an invalid age, the method will re-prompt until a valid age is entered.

            //cust.Salary = ReadDecimal("Enter New Salary: ", 0m);
            //// Step 17: Ask the user to input a new salary amount using ReadDecimal.
            //// Step 18: The minimum allowed salary is 0m which prevents negative salary values.
            //// Step 19: ReadDecimal also ensures that the input is a valid decimal.

            //cust.Married = ReadBool("Is the customer married? (y/n): ");
            //// Step 20: Ask the user a yes/no question and update the Married property.
            //// Step 21: ReadBool only accepts “y”, “yes”, “n”, or “no”, continuing to ask until a valid input is provided.
            //// Step 22: The property is updated as true or false depending on the user’s answer.

            //Console.WriteLine($"{cust.FirstName} {cust.LastName} has been updated in our database; Thank you!");
            //// Step 23: Display a confirmation message showing the updated customer’s full name.
            //// Step 24: This informs the user that the update process has completed successfully.
            #endregion

            //Can be used for Searching and Sorting as well as deleting
            string search = ReadString("Enter Customer First Name to update: ");
            customers.Sort();
            int index = customers.BinarySearch(new Customer(firstName: search));
            if (index <= 0)
            {
                Console.WriteLine("Enter New Customer First Name to update: ");
                customers[index].FirstName = Console.ReadLine();
                Console.Write("Enter New Customer Last Name to update: ");
                customers[index].LastName = Console.ReadLine();
                Console.Write("Enter New Customer Age to update: ");
                customers[index].Age = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter New Customer Salary to update: ");
                customers[index].Salary = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Is the customer married? (y/n): ");
                string marriedInput = Console.ReadLine().ToLower();
                Console.Write(marriedInput);
                Console.WriteLine($"{customers[index].FirstName} {customers[index].LastName} has been updated in our database; Thank you!");
            }
            else
            {
                Console.WriteLine("Sorry Customer Not Found ...");
            }
            return;
        }

        static void SearchCustomer()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Search Customer";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            // Step 1: Prompt the user to enter the FIRST NAME of the customer they want to search for.
            // Note: Console.Write displays a message without a newline; Console.ReadLine reads the full user input.
            Console.Write("Please type the FIRST NAME of the customer: ");
            string nameSearch = Console.ReadLine();

            // Step 2: Create a Predicate<Customer> that defines the search logic.
            // Note: A Predicate<T> is simply a function returning true/false.
            //       Here it checks if the customer's FirstName matches the user's input,
            //       ignoring differences in uppercase/lowercase using OrdinalIgnoreCase.
            Predicate<Customer> match = (Customer c) =>
                c.FirstName.Equals(nameSearch, StringComparison.OrdinalIgnoreCase);

            // Step 3: Use List<T>.Find to locate the FIRST matching customer in the list.
            // Note: Find will return the first Customer where match(customer) == true.
            //       If none match, it returns null.
            Customer cust = customers.Find(match);

            // Step 4: Check whether a matching customer was found.
            // Note: Always check for null to avoid NullReferenceException.
            if (cust != null)
            {
                // Step 5: Display all customer details to the user in a readable format.
                // Note: {cust.Salary:C} formats the salary as currency automatically.
                //       Ternary operator (cust.Married ? "Yes" : "No") converts bool into readable text.
                Console.WriteLine("\nCustomer Found:");
                Console.WriteLine($"ID: {cust.Id}");
                Console.WriteLine($"Name: {cust.FirstName} {cust.LastName}");
                Console.WriteLine($"Age: {cust.Age}");
                Console.WriteLine($"Salary: {cust.Salary:C}");
                Console.WriteLine($"Married: {(cust.Married ? "Yes" : "No")}\n");
            }
            else
            {
                // Step 6: Inform the user that no customer matched the entered name.
                Console.WriteLine("Sorry Customer Not Found ...");
            }

        }

        static void ViewAllCustomers()
        {
            Console.Clear();
            Console.WriteLine("Customer List:");
            Console.WriteLine("".PadLeft(80, '-'));

            Console.Write("| ");
            Console.Write("ID".PadRight(5));
            Console.Write(" | ");
            Console.Write("Name".PadRight(25));
            Console.Write(" | ");
            Console.Write("Age".PadRight(5));
            Console.Write(" | ");
            Console.Write("Salary".PadRight(12));
            Console.Write(" | ");
            Console.Write("Married".PadRight(7));
            Console.WriteLine(" |");

            Console.WriteLine("".PadLeft(80, '-'));

            foreach (var cust in customers)
            {
                Console.Write("| ");
                Console.Write(cust.Id.ToString().PadRight(5));
                Console.Write(" | ");
                var fullName = $"{cust.FirstName} {cust.LastName}";
                Console.Write(fullName.PadRight(25));
                Console.Write(" | ");
                Console.Write(cust.Age.ToString().PadRight(5));
                Console.Write(" | ");
                Console.Write(cust.Salary.ToString("C").PadRight(12));
                Console.Write(" | ");
                Console.Write((cust.Married ? "Yes" : "No").PadRight(7));
                Console.WriteLine(" |");
            }
        }

        static void DeleteCustomer()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Delete Customer";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            // Step 1: Ask the user to type the first name of the customer they want to delete.
            // Note: ReadString shows a prompt and returns the typed text as a string.
            string search = ReadString("Enter Customer First Name to delete: ");

            // Step 2: Remove all customers whose FirstName matches the search text (case-insensitive).
            // Note: RemoveAll takes a predicate (a function that returns true/false).
            //       For each customer 'c', the predicate checks if c.FirstName equals the search text
            //       while ignoring upper/lower case differences by using StringComparison.OrdinalIgnoreCase.
            //       RemoveAll returns the number of elements that were removed from the list.
            int removed = customers.RemoveAll(c =>
                c.FirstName.Equals(search, StringComparison.OrdinalIgnoreCase)
            );

            // Step 3: Check how many customers were removed and inform the user.
            // Note: If removed > 0 we know at least one matching customer was deleted.
            //       If removed == 0 no matching customer was found so we inform the user.
            if (removed > 0)
            {
                // Step 4: Confirm deletion to the user and show how many were deleted.
                // Note: You could extend this to show the exact names deleted if you collected them beforehand.
                Console.WriteLine($"Done Deleting Customer(s). {removed} record(s) removed.");
            }
            else
            {
                // Step 5: Tell the user that no matching customer was found.
                Console.WriteLine("Sorry Customer Not Found ...");
            }
        }
        #endregion

        #region Product CRUD Operations
        static void AddProduct()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Add Customer Product";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);
            string productName = ReadString("Please type Product Name: ");
            decimal productPrice = ReadDecimal("Please type Product Price: ", 0m);

            Product prod = new Product(productName, productPrice);
            products.Add(prod);

            Console.WriteLine($"Product added with ID {prod.Id}");
        }

        static void SearchProduct()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Search Product";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            // Step 1: Ask how the user wants to search
            Console.WriteLine("Search products by:");
            Console.WriteLine("1. Product ID");
            Console.WriteLine("2. Product Name");
            int searchChoice = ReadInt("Choose (1-2): ", 1, 2);

            Product matchedProduct = null;

            if (searchChoice == 1)
            {
                // Search by Product ID
                int productIdSearch = ReadInt("Enter Product ID: ", 1);
                matchedProduct = products.Find((Product p) => p.Id == productIdSearch);
            }
            else
            {
                // Search by Product Name
                string nameSearch = ReadString("Enter Product Name: ");
                matchedProduct = products.Find((Product p) =>
                    p.Name.Equals(nameSearch, StringComparison.OrdinalIgnoreCase));
            }

            // Step 2: Display results
            if (matchedProduct == null)
            {
                Console.WriteLine("No product found with the specified search.");
                return;
            }

            Console.WriteLine("\nProduct Found:");
            Console.WriteLine($"ID: {matchedProduct.Id}");
            Console.WriteLine($"Name: {matchedProduct.Name}");
            Console.WriteLine($"Price: {matchedProduct.Price:C}");
            Console.WriteLine($"Discounted Price (20% off): {matchedProduct.GetPriceAfterDiscount(0.2m):C}");
        }

        static void ViewAllProducts()
        {
            Console.Clear();
            Console.WriteLine("\t\tCustomer Product Information");
            Console.WriteLine(string.Empty.PadLeft(65, '-'));

            Console.Write("| ");
            Console.Write("ID".PadRight(5));
            Console.Write(" | ");
            Console.Write("Name".PadRight(20));
            Console.Write(" | ");
            Console.Write("Price".PadRight(12));
            Console.Write(" | ");
            Console.Write("Discounted Price".PadRight(10));
            Console.WriteLine(" |");

            Console.WriteLine(string.Empty.PadLeft(65, '-'));

            foreach (Product prod in products)
            {
                Console.Write("| ");
                Console.Write(prod.Id.ToString().PadRight(5));
                Console.Write(" | ");
                Console.Write(prod.Name.PadRight(20));
                Console.Write(" | ");
                Console.Write(prod.Price.ToString("C").PadRight(12));
                Console.Write(" | ");
                Console.Write(prod.GetPriceAfterDiscount(0.2m).ToString("C").PadRight(15));
                Console.WriteLine(" |");
            }
        }
        #endregion

        #region Complaint CRUD Operations

        static void AddComplaint()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Add Customer Complaint";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            // Step 1: Identify customer
            Console.WriteLine("Identify customer by:");
            Console.WriteLine("1. Customer ID");
            Console.WriteLine("2. Customer Name");
            int idChoice = ReadInt("Choose (1-2): ", 1, 2);

            Customer matchedCustomer = null;

            if (idChoice == 1)
            {
                int customerID = ReadInt("Enter Customer ID: ", 1);
                matchedCustomer = customers.Find((Customer customer) => customer.Id == customerID);
            }
            else
            {
                string customerName = ReadString("Enter Customer First Name: ");
                matchedCustomer = customers.Find((Customer customer) =>
                    customer.FirstName.Equals(customerName, StringComparison.OrdinalIgnoreCase));
            }

            if (matchedCustomer == null)
            {
                Console.WriteLine("Customer not found. Cannot add complaint.");
                return;
            }

            // Step 2: Choose complaint type
            Console.WriteLine("\n\t Complaint Types:");
            Console.WriteLine("1. Product Complaint");
            Console.WriteLine("2. Service Complaint");

            int choiceType = ReadInt("Choose complaint type (1-2): ", 1, 2);
            string typeDescription = ReadString("Please type complaint description: ");

            Complaint complaintObj = null;

            switch (choiceType)
            {
                case 1:
                    int productID = ReadInt("Please type Product ID: ", 1);
                    complaintObj = new ProductComplaint(matchedCustomer.Id, typeDescription, productID, Logger);
                    break;

                case 2:
                    complaintObj = new ServiceComplaint(matchedCustomer.Id, typeDescription, Logger);
                    break;
            }

            if (complaintObj != null)
            {
                complaintObj.ComplainStatusChanged += (msg) => { Console.WriteLine($"Notification: {msg}"); };

            }
            // Step 2: Choose complaint type
            Console.WriteLine("\n\t Complaint Priority:");
            Console.WriteLine("1. Normal");
            Console.WriteLine("2. Urgent");

            int priorityLevel = ReadInt("Choose complaint type (1-2): ", 1, 2);

            //TODO3: Add new object to the Priroty Queue
            if (priorityLevel == 1) //Normal
            {
                urgentComplaintQueue.Enqueue(complaintObj, 2);
            }
            else //Urgent
            {
                urgentComplaintQueue.Enqueue(complaintObj, 1);
            }
            Console.WriteLine($"\nComplaint successfully added for customer '{matchedCustomer.FirstName} {matchedCustomer.LastName}'.");
            complaintObj.ComplainStatusChanged += (string msg) =>
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Notification: " + msg);
                Console.ForegroundColor = ConsoleColor.White;
            };
        }

        static void ViewAllComplaints()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   View All Complaints";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            if (urgentComplaintQueue.Count > 0)
            {
                Console.WriteLine("Complaints current in queue, waiting to be processed.");
                return;
            }
            else
            {
                Console.WriteLine("No complaints to be processed...");
                return;
            }
            // OLD SYNTAX
            //foreach (Complaint comp in complaints)
            //{
            //    comp.GetComplaintDetails();
            //}

            // LAMBDA EXPRESSION
            complaints.ForEach(comp =>
            {
                comp.UpdateComplaintStatus(Status.Closed);
                comp.GetComplaintDetails();
            });
        }

        static void ComplaintSearch()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Complaint Search";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            Console.WriteLine("Search complaints by:");
            Console.WriteLine("1. Customer ID");
            Console.WriteLine("2. Customer First Name");
            int searchChoice = ReadInt("Choose (1-2): ", 1, 2);

            List<Complaint> matchingComplaints = new List<Complaint>();

            if (searchChoice == 1)
            {
                int customerIdSearch = ReadInt("Enter Customer ID: ", 1);
                matchingComplaints = complaints.FindAll((Complaint c) => c.CustomerId == customerIdSearch);
            }
            else
            {
                string nameSearch = ReadString("Enter Customer First Name: ");
                Customer matchedCustomer = customers.Find((Customer c) =>
                    c.FirstName.Equals(nameSearch, StringComparison.OrdinalIgnoreCase));

                if (matchedCustomer == null)
                {
                    Console.WriteLine($"Customer '{nameSearch}' not found.\n");
                    return;
                }

                matchingComplaints = complaints.FindAll((Complaint c) => c.CustomerId == matchedCustomer.Id);
            }

            if (matchingComplaints.Count == 0)
            {
                Console.WriteLine("No complaints found for the specified customer.");
                return;
            }

            Console.WriteLine($"\nFound {matchingComplaints.Count} complaint(s):\n");
            foreach (Complaint comp in matchingComplaints)
            {
                comp.GetComplaintDetails();
            }
        }

        static void ViewAllComplaintByCustomer()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   View Complaints By Customer";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            Console.WriteLine("Search complaints by:");
            Console.WriteLine("1. Customer ID");
            Console.WriteLine("2. Customer First Name");
            int searchChoice = ReadInt("Choose (1-2): ", 1, 2);

            List<Complaint> matchingComplaints = new List<Complaint>();

            if (searchChoice == 1)
            {
                int customerIdSearch = ReadInt("Enter Customer ID: ", 1);
                matchingComplaints = complaints.FindAll((Complaint complaintItem) =>
                    complaintItem.CustomerId == customerIdSearch);
            }
            else
            {
                string nameSearch = ReadString("Enter Customer First Name: ");
                Customer matchedCustomer = customers.Find((Customer customerItem) =>
                    customerItem.FirstName.Equals(nameSearch, StringComparison.OrdinalIgnoreCase));

                if (matchedCustomer == null)
                {
                    Console.WriteLine($"Customer '{nameSearch}' not found.\n");
                    return;
                }

                matchingComplaints = complaints.FindAll((Complaint complaintItem) =>
                    complaintItem.CustomerId == matchedCustomer.Id);
            }

            if (matchingComplaints.Count == 0)
            {
                Console.WriteLine("No complaints found for the specified customer.");
                return;
            }

            Console.WriteLine($"\nFound {matchingComplaints.Count} complaint(s):\n");
            foreach (Complaint complaintItem in matchingComplaints)
            {
                complaintItem.GetComplaintDetails();
            }


            ////version1: constructing the Predicate delegate using anonymous method
            //Predicate<Complaint> predicate = delegate (Complaint comp) {
            //    return comp.CustomerId == customerIdSearch;
            //};
            //// List of Complaints
            //List<Complaint> output=complaints.FindAll(predicate);


            ////version2: constructing the Predicate delegate using Lambda Expression
            //output=complaints.FindAll(comp => comp.CustomerId == customerIdSearch);
        }

        static void ProcessComplaints()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Process Complaint(s)";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            if (urgentComplaintQueue.Count == 0)
            {
                Console.WriteLine("No Complaints to process ...");
                return;
            }
            else
            {
                Complaint comp = urgentComplaintQueue.Dequeue();
                Console.WriteLine("Processing the following complaint:");
                comp.GetComplaintDetails();
                Console.WriteLine("*********************************");
                Console.WriteLine("Set Complaint Status");
                Console.WriteLine("*********************************");
                Console.WriteLine("1. Closed");
                Console.WriteLine("2. Resolved");
                Console.WriteLine("*********************************");
                Console.Write("Please Select a number (1-2): ");
                int status = int.Parse(Console.ReadLine());

                comp.UpdateComplaintStatus(status == 1 ? Status.Closed : Status.Resolved);
                complaints.Add(comp);
            }
        }
        #endregion

        #region Appointment CRUD Operations
        static void ScheduleAppointment()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Schedule Appointment";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);

            Console.WriteLine("Please enter Customer ID: ");
            int customerID = int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter Appointment Date (YYYY-MM-DD): ");
            string dateInput = Console.ReadLine();

            string[] dateArray = dateInput.Split('-');

            if (dateArray.Length != 3)
            {
                Console.WriteLine("Invalid date format. Use YYYY-MM-DD.");
                return;
            }

            int year = int.Parse(dateArray[0]);
            int month = int.Parse(dateArray[1]);
            int day = int.Parse(dateArray[2]);

            Console.WriteLine("Please enter Appointment Time (HH:MM): ");
            string timeInput = Console.ReadLine();

            string[] timeArray = timeInput.Split(':');

            if (timeArray.Length != 2)
            {
                Console.WriteLine("Invalid time format. Use HH:MM.");
                return;
            }

            int hour = int.Parse(timeArray[0]);
            int minute = int.Parse(timeArray[1]);

            DateTime appointmentTime;

            try
            {
                appointmentTime = new DateTime(year, month, day, hour, minute, 0);
            }
            catch
            {
                Console.WriteLine("Invalid date or time values.");
                return;
            }

            Appointment newAppointment = new Appointment(customerID, appointmentTime);

            if (appointmentList.First == null)
            {
                appointmentList.AddFirst(newAppointment);
            }
            else
            {
                LinkedListNode<Appointment> current = appointmentList.First;
                while (current != null && current.Value.AppointmentTime < newAppointment.AppointmentTime)
                {
                    current = current.Next;
                }
                if (current == null)
                {
                    appointmentList.AddLast(newAppointment);
                }
                else
                {
                    appointmentList.AddBefore(current, newAppointment);
                }
            }

            Console.WriteLine($"\nAppointment for customer ID {customerID} has been booked at {newAppointment.AppointmentTime}");
        }

        static void ModifyAppointment()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   ModifyAppointment";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);

            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);


            Console.Clear();

            if (appointmentList.Count == 0)
            {
                Console.WriteLine("No Appointments Found!");
                return;
            }

            Console.Write("Please Enter Customer ID: ");
            int customerID = int.Parse(Console.ReadLine());

            LinkedListNode<Appointment> current = appointmentList.First;


            if (current.Value.CustomerId == customerID)
            {
                appointmentList.Remove(current);
                Console.WriteLine($"Appointment for Customer {customerID} Canceled ...");
                return;
            }

            LinkedListNode<Appointment> last = appointmentList.Last;
            if (last.Value.CustomerId == customerID)
            {
                appointmentList.Remove(last);
                Console.WriteLine($"Appointment for Customer {customerID} Canceled ...");
                return;
            }


            while (true)
            {
                //new next appointmnt is greater or equal to current appointment .. make it current
                if (current.Next != null &&
                    current.Next.Value.CustomerId != customerID)
                {
                    current = current.Next;
                    continue;
                }
                else if (current.Next == null)
                {
                    Console.WriteLine($"Appointment for Customer {customerID} Not Found ...");
                    break;
                }
                else // Next is greater than current
                {
                    appointmentList.Remove(current);
                    Console.WriteLine($"Appointment for Customer {customerID} Canceled ...");
                    break;
                }
            }

            static void NavigateAppointments()
            {
                if (appointmentList.Count == 0)
                {
                    Console.WriteLine("No appointments scheduled.");
                    return;
                }

                LinkedListNode<Appointment> current = appointmentList.First;

                char ch;  // n->next, p->previous, e->exit
                do
                {

                    Console.Clear();

                    Console.WriteLine($"Appointment ID: {current.Value.Id}");
                    Console.WriteLine($"Customer ID: {current.Value.CustomerId}");
                    Console.WriteLine($"Appointment DateTime: {current.Value.AppointmentTime}");
                    Console.WriteLine($"");

                    ConsoleColor original = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    if (current.Previous != null && current.Next == null)
                    {
                        Console.Write("Press (P)revious or (E)xit");
                    }
                    else if (current.Previous != null && current.Next != null)
                    {
                        Console.Write("Press (N)ext or (P)revious or (E)xit");
                    }
                    else
                    {
                        Console.Write("Press (N)ext or (E)xit");
                    }
                    Console.ForegroundColor = original;

                    ch = Console.ReadKey().KeyChar;
                    switch (ch)
                    {
                        case 'n':
                            current = current.Next != null ? current.Next : current;
                            break;
                        case 'p':
                            current = current.Previous != null ? current.Previous : current;
                            break;
                    }
                } while (ch != 'e');

                Console.WriteLine();
            }

            static void CancelAppointments()
            {
                //get the ID for the appointment we are canceling
                Console.WriteLine("Enter ID for the appointment you want to cancel: ");
                int customerId = int.Parse(Console.ReadLine());


                //starting off at current which would be the first
                LinkedListNode<Appointment> current = appointmentList.First;

                while (current != null)
                {
                    if (current.Value.Id == customerId)
                    {
                        appointmentList.Remove(current);
                        Console.WriteLine("Appointment has been canceled. ");
                        return;
                    }
                    current = current.Next;
                }
                Console.WriteLine("Appointment not found. ");
            }


        }

        static void ViewAllAppointments()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   View All Appointments";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);
            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);
            if (appointmentList.Count == 0)
            {
                Console.WriteLine("No Appointments Scheduled ...");
                return;
            }
            foreach (var appointment in appointmentList)
            {
                Console.WriteLine($"Appointment ID: {appointment.Id}");
                Console.WriteLine($"Customer ID: {appointment.CustomerId}");
                Console.WriteLine($"Appointment DateTime: {appointment.AppointmentTime}");
                Console.WriteLine($"-----------------------------------");
            }
        }

        static void CancelAppointment()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var welcomeMessage = "   Cancel Appointment";
            var decorativeLine = new string('*', welcomeMessage.Length + 4);
            Console.WriteLine(decorativeLine);
            Console.WriteLine(welcomeMessage);
            Console.WriteLine(decorativeLine);
            Console.Write("Please Enter Customer ID: ");
            int customerID = int.Parse(Console.ReadLine());
            LinkedListNode<Appointment> current = appointmentList.First;

        }
        #endregion
    }
}

