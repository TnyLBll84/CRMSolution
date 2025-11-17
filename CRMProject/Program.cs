using System;
using System.ComponentModel.Design;
using System.Reflection;
using System.Xml.Linq;

namespace CRMProject
{
    internal class Program
    {

        // Global Arrays to hold customer information

        // These arrays are "global" because they are declared outside of any method.
        // This means every method in this class can access and modify them.
        // They store information about all customers in parallel, meaning:
        // customerIds[i], customerNames[i], customerAges[i], etc., all belong to the same customer at index i.

        // Array to store unique ID numbers for customers
        static int[] customerIds = new int[0]; // Start with 0 elements, will grow as customers are added

        // Array to store names of customers
        static string[] customerNames = new string[0]; // Initially empty

        // Array to store ages of customers
        static int[] customerAges = new int[0]; // Will hold integer ages

        // Array to store salaries of customers
        static decimal[] customerSalaries = new decimal[0]; // decimal type is used for money values

        // Array to store marital status of customers
        static bool[] customerIsMarried = new bool[0]; // true = married, false = not married


        // Auto-increment ID for new customers

        // This variable keeps track of the next ID to assign a new customer.
        // Each time a new customer is added, this number increases by 1 automatically.
        static int nextCustomerId = 0;

        #region Main Methods

        static void Main(string[] args)
        {

            // Build Menu System for CRM

            // This string stores the welcome message
            string welcomeMessage = " Welcome to the CRM System ";

            // Create a decorative line of asterisks (*) the same length as the welcome message
            string decorativeLine = new string('*', welcomeMessage.Length);

            // Change the text color in the console to green
            Console.ForegroundColor = ConsoleColor.Green;

            // Variable to store the user's menu choice
            int option = default; // default initializes it to 0


            // Display Menu to the User

            Console.WriteLine(decorativeLine); // Top decorative line
            Console.WriteLine(welcomeMessage); // Welcome message
            Console.WriteLine(decorativeLine); // Bottom decorative line

            Console.WriteLine("\tCRM Options:"); // Menu title
            Console.WriteLine("   1. Add New Customer");
            Console.WriteLine("   2. Update Customer Info");
            Console.WriteLine("   3. Delete Customer");
            Console.WriteLine("   4. Search Customer");
            Console.WriteLine("   5. View All Customers");
            Console.WriteLine("   6. Exit Program\n");

            // Setup exception handling

            // We'll only catch exceptions when the user provides input
            bool excutionCompleted = false; // Tracks if an exception occurred

            try
            {
                do
                {
                    // Ask the user to choose a menu option
                    Console.Write("Enter your choice from above (1 - 6): ");

                    // Read user input as a string, then convert it to an integer
                    option = int.Parse(Console.ReadLine());

                    // Handle menu selection using switch-case

                    switch (option)
                    {
                        case 1:
                            AddCustomer(); // Call method to add a new customer
                            break; // Exit this case
                        case 2:
                            UpdateCustomerInfo(); // Call method to update customer info
                            break;
                        case 3:
                            DeleteCustomer(); // Call method to delete a customer
                            break;
                        case 4:
                            SearchCustomer(); // Call method to search for a customer
                            break;
                        case 5:
                            ViewAllCustomers(); // Call method to display all customers
                            break;
                        case 6:
                            ExitApplication(); // Exit the program
                            break;
                        default:
                            // If user enters a number outside 1-6
                            Console.WriteLine("Invalid choice. Please select a listed option.\n");
                            break;
                    }

                    // Pause so the user can see results
                    Console.WriteLine("Press enter to continue ...");
                    Console.ReadLine();

                    // Clear the console for the next loop
                    Console.Clear();

                    // Loop continues until the user selects option 6 (Exit)
                } while (option != 6);
            }
            catch (FormatException formatEx)
            {
                // This exception occurs if the user enters something that cannot be converted to int
                excutionCompleted = true;
                Console.WriteLine("Input format is incorrect. Please enter the correct data type.\n");
                Console.WriteLine("Press enter to continue ...");
                Console.ReadLine();
            }
            catch (Exception coverAll)
            {
                // Catches any other unexpected exception
                excutionCompleted = true;
                Console.WriteLine("Input value could not be used. Please enter a valid value.\n");
                Console.WriteLine("Press enter to continue ...");
                Console.ReadLine();
            }
            finally
            {
                // This block always runs, whether an exception occurred or not
                if (excutionCompleted)
                {
                    // Clear console and restart the Main method
                    Console.WriteLine($"");
                    Console.Clear();
                    Program.Main(args); // Restart menu
                }
            }


        }

        static void AddCustomer()
        {
            // Increment the global customer ID for the new customer
            // This ensures every customer has a unique ID
            nextCustomerId++;

            // Use GetAvailableIndex() to find the first empty slot in our arrays
            // If no empty slot exists, this method will resize the arrays and return the new last index
            int index = GetAvailableIndex();

            // Assign the unique ID to the new customer at the found index
            customerIds[index] = nextCustomerId;

            // Prompt the user to enter the customer's name
            Console.Write("Enter customer name: ");
            string nameInput = Console.ReadLine(); // Read the name as a string
            customerNames[index] = nameInput;      // Store it in the array at the available index

            // Prompt the user to enter the customer's age
            Console.Write("Enter customer age: ");
            // Convert the input string to an integer using int.Parse
            // and store it in the customerAges array
            customerAges[index] = int.Parse(Console.ReadLine());

            // Prompt the user to enter the customer's salary
            Console.Write("Enter customer salary: ");
            // Convert the input string to a decimal using decimal.Parse
            // and store it in the customerSalaries array
            customerSalaries[index] = decimal.Parse(Console.ReadLine());

            // Prompt the user to indicate marital status
            Console.Write("Is the customer married? (yes/no): ");
            string userInput = Console.ReadLine().ToLower(); // Convert input to lowercase to handle YES/Yes/yes
            bool marriedInput = (userInput == "yes" || userInput == "y"); // True if 'yes' or 'y', false otherwise
            customerIsMarried[index] = marriedInput; // Store the boolean value in the array

            // Confirm to the user that the customer has been successfully added
            Console.WriteLine("\nCustomer added successfully.\n");
        }

        static void UpdateCustomerInfo()
        {
            bool updateCompleted = false;
            try
            {
                // Prompt user for the name of the customer to update
                Console.Write("Please type the name of the customer you want to update: ");
                string currentCustomer = Console.ReadLine();

                // Create a temporary uppercase array to make the search case-insensitive
                string[] customerNamesUpper = new string[customerNames.Length];
                for (int i = 0; i < customerNames.Length; i++)
                {
                    customerNamesUpper[i] = customerNames[i].ToUpper(); // make input name first letter uppercase
                }

                // Use Array.IndexOf to find the index
                int index = Array.IndexOf(customerNamesUpper, currentCustomer.ToUpper());

                if (index > -1) // Customer found
                {
                    Console.WriteLine($"\nUpdating info for {customerNames[index]} (ID: {customerIds[index]})...\n");

                    // Gather new values for this index
                    var (updatedName, updatedAge, updatedSalary, marriedInput) = GatherInformation(index);

                    // Update arrays at the found index (ID remains unchanged)
                    customerNames[index] = updatedName;
                    customerAges[index] = updatedAge;
                    customerSalaries[index] = updatedSalary;
                    customerIsMarried[index] = marriedInput;

                    updateCompleted = true;
                    Console.WriteLine($"\n{updatedName}'s information has been updated successfully.\n");
                }
                else
                {
                    Console.WriteLine($"\nCustomer '{currentCustomer}' not found in the database.\n");
                }
            }
            catch (FormatException)
            {
                updateCompleted = false;
                Console.WriteLine("Input format is incorrect. Please enter the correct data type.\n");
                Console.WriteLine("Press enter to continue ...");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }

        static void DeleteCustomer()
        {
            // Prompt the user to enter the name of the customer they want to delete
            Console.Write("Please type the name of the customer you want to delete: ");
            string currentCustomer = Console.ReadLine(); // Read user input as a string

            // Create a temporary array to store all customer names in uppercase
            // This makes the search case-insensitive (so "Alice" or "alice" will match)
            string[] customerNamesUpper = new string[customerNames.Length];
            for (int i = 0; i < customerNames.Length; i++)
            {
                // Convert each name to uppercase and store it in the temporary array
                customerNamesUpper[i] = customerNames[i].ToUpper();
            }

            // Search for the index of the customer name in the temporary uppercase array
            // Convert the input name to uppercase as well
            int index = Array.IndexOf(customerNamesUpper, currentCustomer.ToUpper());

            // Check if the customer was found in the array
            if (index > -1) // Customer exists
            {
                // "Delete" customer by resetting all their data at the found index
                customerIds[index] = 0;              // Reset customer ID to 0
                customerNames[index] = null;         // Clear the name
                customerAges[index] = default;       // Reset age to 0
                customerSalaries[index] = default;   // Reset salary to 0.0
                customerIsMarried[index] = default;  // Reset marital status to false

                Console.WriteLine($"\nCustomer '{currentCustomer}' has been deleted successfully.\n");
            }
            else // Customer not found
            {
                Console.WriteLine("\nThat name does not exist within our database.\n");
            }

            // Pause the program so the user can see the message before continuing
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        static void SearchCustomer()
        {
            // Prompt the user to enter the name of the customer they want to look up
            Console.Write("Please type the name of the customer you want to lookup: ");
            string nameSearch = Console.ReadLine(); // Read user input as a string

            // Loop through all customer names in the array
            for (int index = 0; index < customerNames.Length; index++)
            {
                // Check if the current customer name matches the input
                if (customerNames[index] == nameSearch)
                {
                    // If a match is found, display the name and the index
                    Console.WriteLine($"Customer '{nameSearch}' found within index {index}.\n");

                    // Exit the method early since we found the customer
                    return;
                }
            }

            // If the loop finishes without finding a match, inform the user
            Console.WriteLine($"Customer '{nameSearch}' not found.\n");
        }

        static void ViewAllCustomers()
            {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("".PadLeft(75, '-'));
            Console.Write("| ");
            Console.Write("ID".PadRight(2));
            Console.Write(" | ");
            Console.Write("Customer Name".PadRight(20));
            Console.Write(" | ");
            Console.Write("Age".PadRight(2));
            Console.Write(" | ");
            Console.Write("Salary".PadRight(20));
            Console.Write(" | ");
            Console.Write("Marital Status".PadRight(7));
            Console.Write(" | ");
            Console.WriteLine();
            Console.WriteLine("".PadLeft(75, '-'));

            for (int i = 0; i < customerNames.Length; i++)
            {
                if (customerNames[i] != null)
                {
                    //Data Columns
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("| ");
                    Console.Write($"{customerIds[i]}".PadRight(2));
                    Console.Write(" | ");
                    Console.Write($"{customerNames[i]}".PadRight(20));
                    Console.Write(" | ");
                    Console.Write($"{customerAges[i]}".PadRight(3));
                    Console.Write(" | ");
                    Console.Write($"{customerSalaries[i]:C}".PadRight(20));
                    Console.Write(" | ");
                    Console.Write($"{customerIsMarried[i]}".PadRight(15));
                    Console.Write("| ");
                    Console.WriteLine();
                    Console.WriteLine("".PadLeft(75, '-'));

                    //Console.WriteLine($"ID: {customerID[i]} | Customer Name: {customerName[i]} | Age: {customerAge[i]} | Salary: {customerSalary[i]:C} | Marital Status: {customerIsMarried[i]}.");

                }
            }
        }

        static void ExitApplication()
        {
            // Exit Application
            Console.Write(" Press 'Enter' to exit...");
            Console.ReadLine();
            Environment.Exit(0);
        }

        #endregion

        #region Helper Methods

  
        // GetAvailableIndex Method

        // This method finds the first available slot in the customer arrays.
        // If no slot is available, it resizes the arrays to create space.
        static int GetAvailableIndex()
        {
            // Check the 'customerNames' array for the first element that is null
            // Array.IndexOf returns the index of the first occurrence of 'null'
            int index = Array.IndexOf(customerNames, null);

            // If no null slot is found (IndexOf returns -1)
            if (index == -1)
            {
                // Resize all arrays by 1 element to make space for a new customer
                // Array.Resize automatically copies existing elements to the new array
                Array.Resize(ref customerIds, customerIds.Length + 1);              // Resize IDs array
                Array.Resize(ref customerNames, customerNames.Length + 1);          // Resize Names array
                Array.Resize(ref customerAges, customerAges.Length + 1);            // Resize Ages array
                Array.Resize(ref customerSalaries, customerSalaries.Length + 1);    // Resize Salaries array
                Array.Resize(ref customerIsMarried, customerIsMarried.Length + 1);  // Resize Marital Status array

                // Set 'index' to the last position of the resized array (new empty slot)
                index = customerIds.Length - 1;
            }

            // Return the index of the available slot
            // If there was no empty slot originally, this will now point to the new last element
            return index;
        }

        static (string updatedName, int updatedAge, decimal updatedSalary, bool marriedInput) GatherInformation(int index)
        {
            // Keep ID automated (don’t ask user to enter it)
            // The ID is already assigned and stored in the customerIds array
            int updatedId = customerIds[index];

            // Ask the user to enter a new customer name
            Console.Write("Enter new customer name: ");
            string updatedName = Console.ReadLine(); // Read user input as a string

            // Ask the user to enter a new customer age
            Console.Write("Enter new customer age: ");
            int updatedAge = int.Parse(Console.ReadLine());
            // Read input as string, then convert to integer using int.Parse()

            // Ask the user to enter a new customer salary
            Console.Write("Enter new customer salary: ");
            decimal updatedSalary = decimal.Parse(Console.ReadLine());
            // Read input as string, then convert to decimal using decimal.Parse()

            // Ask if the customer is married
            Console.Write("Is the customer married? (yes/no): ");
            string userInput = Console.ReadLine().ToLower();
            // Convert input to lowercase to make comparison easier

            // Convert the text input to a boolean value
            bool marriedInput = (userInput == "yes" || userInput == "y");
            // True if user typed "yes" or "y", otherwise false

            // Return all the collected information as a tuple
            return (updatedName, updatedAge, updatedSalary, marriedInput);
        }

        #endregion

    }
}
