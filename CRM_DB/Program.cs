using ConsoleTables;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection.Metadata;

namespace CRM_DB

{

    internal class Program
    {
        static CRMDBService cRMDBService = new CRMDBService();

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
        #region Customer Menu
        static void CustomerMenu()
        {
            // Step 1: Begin a loop so the customer menu keeps repeating until the user chooses to exit
            while (true)
            {
                // Step 2: Display the customer information menu title with spacing for readability
                Console.WriteLine("\n\t CUSTOMER INFORMATION\n");

                // Step 3: Show menu option 1 which allows adding a new customer
                Console.WriteLine("   1. Add New Customer");

                // Step 6: Show menu option 4 which searches for specific customer details
                Console.WriteLine("   2. View All Customers");

                // Step 8: Show menu option 6 which exits this menu and returns to main menu
                Console.WriteLine("   \n3. Return to Main Menu\n");

                // Step 9: Read the user's input and convert it to an integer
                int option = ReadInt("Enter your choice (1 - 3): ");

                // Step 10: Clear the screen after reading input so the next output is clean
                Console.Clear();

                // Step 11: Use a switch to determine which menu operation the user selected
                switch (option)
                {
                    // Step 12: If user selected 1, call AddCustomer()
                    case 1:
                        AddCustomer();
                        break;

                    // Step 16: If user selected 5, call ViewAllCustomers()
                    case 2:
                        ViewAllCustomers();
                        break;

                    // Step 17: If user selected 6, exit this menu and return to main menu
                    case 3:
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

        private static void AddCustomer()
        {
            Console.Write("Please Type Customer Name: ");
            string customerName = Console.ReadLine();
            cRMDBService.AddCustomer(
                    new Customer
                    {
                        Name = customerName,
                        Age = ReadInt("Please Type Customer Age: ")
                    });

            Console.WriteLine("Done Adding New Customer\n");
        }

        private static void ViewAllCustomers()
        {
            Console.WriteLine("List Of Customers");
            Console.WriteLine("***************");
            var table = new ConsoleTable("Id", "Name", "Age");

            foreach (var customer in cRMDBService.GetAllCustomers())
            {
                table.AddRow(customer.CustomerId, customer.Name, customer.Age);
            }
            table.Write();
        }
        #endregion


        #region Product Menu
        static void ProductMenu()
        {
            // Step 1: Begin an infinite loop so this menu stays active until the user chooses to exit
            while (true)
            {
                // Step 2: Display the product details menu heading
                Console.WriteLine("\n\t PRODUCT DETAILS\n");

                // Step 3: Show option 1 which allows adding a product for a customer
                Console.WriteLine("   1. Add Customer Product");

                // Step 5: Show option 3 which displays all products
                Console.WriteLine("   2. View All Products");

                // Step 6: Show option 4 which exits back to the main menu
                Console.WriteLine("   \n3. Return to Main Menu\n");

                // Step 7: Read user choice and convert it to an integer
                int option = ReadInt("Enter your choice (1 - 3): ");

                // Step 8: Clear the screen to prepare for next action
                Console.Clear();

                // Step 9: Use a switch to process the selected option
                switch (option)
                {
                    // Step 10: If user selected 1, run AddProduct()
                    case 1:
                        AddProduct();
                        break;
                    // Step 12: If user selected 3, run ViewAllProducts()
                    case 2:
                        ViewAllProducts();
                        break;

                    // Step 13: If user selected 4, exit this menu by returning
                    case 3:
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

        private static void AddProduct()
        {
            Console.Write("Please Type Product Name: ");
            string productName = Console.ReadLine();
            cRMDBService.AddProduct(
                    new Product
                    {
                        Name = productName,
                        Price = ReadInt("Please Type Product Price: ")
                    });

            Console.WriteLine("Done Adding New Customer\n");

        }

        private static void ViewAllProducts()
        {
            Console.WriteLine("List Of Products");
            Console.WriteLine("***************");
            var table = new ConsoleTable("Id", "Name", "Price");

            foreach (var product in cRMDBService.GetAllProducts())
            {
                table.AddRow(product.ProductId, product.Name, product.Price);
            }
            table.Write();

        }

        #endregion


        #region Complaint Menu
        static void ComplaintMenu()
        {
            // Step 1: Begin a continuous loop so the complaint menu stays active until the user selects the return option
            while (true)
            {
                // Step 2: Display the complaint section heading with spacing for readability
                Console.WriteLine("\n\t COMPLAINT DETAILS\n");

                // Step 3: Show menu option 1 which allows adding a new customer complaint
                Console.WriteLine("   1. Add Customer Complaint");
                // Step 5: Show menu option 3 which displays all complaints in the system
                Console.WriteLine("   2. View All Complaints");


                Console.WriteLine("   3. View All By Customer ID");
                // Step 7: Show menu option 5 which returns from this menu to the main menu
                Console.WriteLine("   \n4. Return to Main Menu\n");

                // Step 8: Read the user's choice for options 1 through 5 and convert the input to an integer
                int option = ReadInt("Enter your choice (1 - 4): ");

                // Step 9: Clear the console screen to prepare for the next action display
                Console.Clear();

                // Step 10: Use a switch statement to handle whichever option the user selected
                switch (option)
                {
                    // Step 11: If the user chooses option 1, call the method that adds a new complaint
                    case 1:
                        AddComplaint();
                        break;
                    // Step 13: If the user chooses option 3, call the method that displays all complaints
                    case 2:
                        ViewAllComplaints();
                        break;
                    case 3:
                        GetComplaintsByCustID();
                        break;
                    // Step 15: If the user chooses option 5, return to exit this menu and return to the main menu
                    case 4:
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


        private static void AddComplaint()
        {
            Console.Write("Please type Customer ID: ");
            int customerID = int.Parse(Console.ReadLine());

            Console.Write("Please type Product ID: ");
            int productID = int.Parse(Console.ReadLine());

            Console.Write("Please type Complaint Description: ");
            string description = Console.ReadLine();


            cRMDBService.AddComplaint(new Complaint()
            {
                CustomerId = customerID,
                ProductId = productID,
                Status = "Opened",
                Description = description


            });

            Console.WriteLine($"Done Adding Complaint information");

        }

        private static void ViewAllComplaints()
        {
            #region Third Party Table Version
            var table = new ConsoleTable("Id", "Customer ID", "Product ID", "Status", "Description");

            foreach (Complaint comp in cRMDBService.GetAllComplaints())
            {
                table.AddRow(comp.ComplaintId, comp.CustomerId, comp.ProductId, comp.Status, comp.Description);
            }

            table.Write();
            #endregion

        }

        private static void GetComplaintsByCustID()
        {
            Console.Write("Please type Customer ID: ");
            int customerID = int.Parse(Console.ReadLine());

            Customer cust = cRMDBService.GetCustomerByID(customerID);


            var table = new ConsoleTable("Id", "Name", "Age");
            table.AddRow(cust.CustomerId, cust.Name, cust.Age);
            table.Write();


            table = new ConsoleTable("Id", "Customer ID", "Product ID", "Status", "Description");

            foreach (Complaint comp in cust.Complaints)
            {
                table.AddRow(comp.ComplaintId, comp.CustomerId, comp.ProductId, comp.Status, comp.Description);
            }

            table.Write();
        }

        #endregion


        #region Appointment Menu
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

                // Step 7: Show menu option 5 which returns from this menu to the main menu
                Console.WriteLine("   \n3. Return to Main Menu\n");

                // Step 8: Read the user's choice for options 1 through 5 and convert the input to an integer
                int option = ReadInt("Enter your choice (1 - 3): ");

                // Step 9: Clear the console screen to prepare for the next action display
                Console.Clear();

                // Step 10: Use a switch statement to handle whichever option the user selected
                switch (option)
                {
                    // Step 11: If the user chooses option 1, call the method that adds a new complaint
                    case 1:
                        ScheduleAppointment();
                        break;
                    // Step 13: If the user chooses option 3, call the method that displays all complaints
                    case 2:
                        ViewAllAppointments();
                        break;
                    // Step 15: If the user chooses option 5, return to exit this menu and return to the main menu
                    case 3:
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

        private static void ViewAllAppointments()
        {
            throw new NotImplementedException();
        }

        private static void ScheduleAppointment()
        {
            throw new NotImplementedException();
        }

        #endregion
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
    }
}
    

