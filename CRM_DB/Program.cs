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
using System.IO;
using CRM_DB;
using System.Security.Cryptography;

namespace CRM_DB

{

    internal class Program
    {
        static CRMDBService cRMDBService = new CRMDBService();
        static CryptoService cryptoService = new CryptoService();

        static string logDirectoryPath = String.Empty;

        static string logFileName = String.Empty;
        static string csvFileName = String.Empty;
        static string logFilePath = String.Empty;

        static ConsoleLogger consoleLogger = new ConsoleLogger();
        static TextFileLogger txtFileLogger = new TextFileLogger();
        static CSVFileLogger csvFileLogger = new CSVFileLogger();

        #region Applicaton EntryPoint (Main Menu)
        static void Main(string[] args)
        {

            ////Testing Code
            //byte[] salt = RandomNumberGenerator.GetBytes(16);
            //string cipherText = cryptoService.Encrypt("hi every one", "P@ssw0rd", salt);
            //string plainText = cryptoService.Decrypt(cipherText, "P@ssw0rd", salt);

            //Configuration Setup
            // Create Log Directory if not exists
            string logDirectoryPath = @"C:\Users\tonyl\OneDrive\Desktop\MSSA\CRMSolution\CRM_DB\Logs\";
            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }

            // Text log file
            logFileName = "log.txt"; 
            logFilePath = Path.Combine(logDirectoryPath, logFileName);                     
            txtFileLogger.LogFilePath = logFilePath;

            // CSV log file string
            csvFileName = "log.csv"; 
            string csvFilePath = Path.Combine(logDirectoryPath, csvFileName); 
            csvFileLogger.LogFilePath = csvFilePath;

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
                    Console.Write("Enter your choice (1 - 5): ");
                    int mainChoice = int.Parse(Console.ReadLine());

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
                catch (FormatException fex) // Catch any unhandled exceptions from the menus
                {
                    consoleLogger.LogError("Input format is invalid. Please enter the correct data type.");
                    txtFileLogger.LogError("Format Exception: " + fex.Message);
                    csvFileLogger.LogError("Format Exception: " + fex.Message);
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (OverflowException oex) // Catch any unhandled exceptions from the menus
                {
                    consoleLogger.LogError("Input number is too large or too small.");
                    txtFileLogger.LogError("Overflow Exception: " + oex.Message);
                    csvFileLogger.LogError("Format Exception: " + oex.Message);
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (IndexOutOfRangeException iex) // Catch any unhandled exceptions from the menus
                {
                    consoleLogger.LogError("Input index is out of range.");
                    txtFileLogger.LogError("Index Out Of Range Exception: " + iex.Message);
                    csvFileLogger.LogError("Format Exception: " + iex.Message);
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (Exception catchAll) // Catch any unhandled exceptions from the menus
                {
                    // Step 21: Change the text color to red to highlight an error occurred
                    Console.ForegroundColor = ConsoleColor.Red;
                    consoleLogger.LogError("An unexpected error occurred. Please try again.");
                    Console.WriteLine("Error: " + catchAll.Message);
                    txtFileLogger.LogError("Index Out Of Range Exception: " + catchAll.Message);
                    csvFileLogger.LogError("Format Exception: " + catchAll.Message);
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
                Console.Write("Enter your choice (1 - 3): ");
                int option = int.Parse(Console.ReadLine());

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
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            Console.Write("Please Type Customer First Name: ");
            string customerFName = Console.ReadLine();

            Console.Write("Please Type Customer Last Name: ");
            string customerLName = Console.ReadLine();

            Console.Write("Please Type Customer Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Please Type Credit Card Number to be on file (ex: 123-123-123-123): ");
            string creditCard = Console.ReadLine();

            string cipherText = cryptoService.Encrypt(creditCard, "P@ssw0rd", salt);
            Console.WriteLine($"DEBUG: cipherText = {cipherText}");
            cRMDBService.AddCustomer(new Customer
            {
                FirstName = customerFName,
                LastName = customerLName,
                Age = age,
                CreditCard = cipherText,
                Salt = salt
            });

            Console.WriteLine("Done Adding New Customer\n");
        }

        private static void ViewAllCustomers()
        {
            Console.WriteLine("List Of Customers");
            Console.WriteLine("***************");
            var table = new ConsoleTable("Id", "First Name", "Last Name", "Age", "Credit Card Info.");

            foreach (var customer in cRMDBService.GetAllCustomers())
            {
                table.AddRow(
                    customer.CustomerId,
                    customer.FirstName,
                    customer.LastName,
                    customer.Age,
                    customer.CreditCard   // encrypted string shown as-is
                );
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
                Console.Write("Enter your choice (1 - 3): ");
                int option = int.Parse(Console.ReadLine());
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

            Console.Write("Please Type Product Price: ");
            int price = int.Parse(Console.ReadLine());

            cRMDBService.AddProduct(new Product
            {
                Name = productName,
                Price = price
            });

            Console.WriteLine("Done Adding New Product\n");
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
                Console.Write("Enter your choice (1 - 4): ");
                int option = int.Parse(Console.ReadLine());
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


            var table = new ConsoleTable("Id", "First Name", "Last Name", "Age");
            table.AddRow(cust.CustomerId, cust.FirstName, cust.LastName, cust.Age);
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
                Console.Write("Enter your choice (1 - 3): ");
                int option = int.Parse(Console.ReadLine());
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
    }
}
    

