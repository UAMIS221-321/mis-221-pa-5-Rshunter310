using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

// (Include the previously provided classes: Trainer, Listing, Booking, and FileManager here)
namespace TrainLikeAChampion
{
  class Program
  {
    //set up file names
    private const string TrainersFileName = "trainers.txt";
    private const string ListingsFileName = "listings.txt";
    private const string TransactionsFileName = "transactions.txt";
//start main
    static void Main(string[] args) 
    {
        while (true)
        {
            DisplayLoadingScreen();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"╔══════════════════════════════════════════════╗");
            Console.WriteLine(@"║                                              ║");
            Console.WriteLine(@"║   Train Like A Champion - Personal Fitness   ║");
            Console.WriteLine(@"║                                              ║");
            Console.WriteLine(@"╚══════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.ResetColor();

            Console.WriteLine("1. Manage Trainer Data");
            Console.WriteLine("2. Manage Listing Data");
            Console.WriteLine("3. Manage Customer Booking Data");
            Console.WriteLine("4. Run Reports");
            Console.WriteLine("5. Exit");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter your choice: ");
            Console.ResetColor();

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        ManageTrainerData();
                        break;
                    case 2:
                        ManageListingData();
                        break;
                    case 3:
                        ManageCustomerBookingData();
                        break;
                    case 4:
                        RunReports();
                        break;
                    case 5:
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
            Console.ReadKey();
        }
    }
    //end main
    //cool extra loading screen
    private static void DisplayLoadingScreen()
    {
        Console.Clear();
        int progressBarWidth = 30;
        string fitnessQuote = "Strength comes from overcoming the things you once thought you couldn't.";
        int quoteLineLength = Math.Max(0, (Console.WindowWidth - fitnessQuote.Length) / 2);

        for (int progress = 0; progress <= progressBarWidth; progress++)
        {
            Console.Clear();
            Console.WriteLine("\n\n");

            Console.WriteLine(new string(' ', quoteLineLength) + fitnessQuote);
            Console.WriteLine("\n\n");

            Console.Write(new string(' ', (Console.WindowWidth - progressBarWidth) / 2));
            Console.Write("[");
            Console.Write(new string('=', progress));
            Console.Write(">");
            Console.Write(new string(' ', progressBarWidth - progress));
            Console.Write("]");

            Thread.Sleep(100);
        }
        Console.Clear();
    }
    private static void ManageTrainerData() //menu option for manage trainer data
    {
        List<Trainer> trainers;

        if (File.Exists(TrainersFileName))
        {
            trainers = FileManager.LoadFromFile(TrainersFileName, Trainer.FromString);
        }
        else
        {
            trainers = new List<Trainer>();
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage Trainer Data");
            Console.WriteLine("1. Add Trainer");
            Console.WriteLine("2. Edit Trainer");
            Console.WriteLine("3. Delete Trainer");
            Console.WriteLine("4. View All Trainers");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice: ");

            int choice; //getting error here tried changing it
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.Write("Enter your choice: ");
            }

            //gets choice
            switch (choice)
            {
                case 1:
                    AddTrainer(trainers);
                    break;
                case 2:
                    EditTrainer(trainers);
                    break;
                case 3:
                    DeleteTrainer(trainers);
                    break;
                case 4:
                    ViewAllTrainers(trainers);
                    break;
                case 5:
                    FileManager.SaveToFile(trainers, TrainersFileName);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    private static readonly Random random = new Random(); //This is to generate random IDs for trainers

    private static void AddTrainer(List<Trainer> trainers)
    {
        Console.Clear();
        Console.WriteLine("Add Trainer");

        Console.Write("Trainer Name: ");
        string name = Console.ReadLine();
        Console.Write("Mailing Address: ");
        string address = Console.ReadLine();
        Console.Write("Email Address: ");
        string email = Console.ReadLine();

        int id;
        bool uniqueId;

        do
        {
            Console.Write("Generating Trainer ID...");
            id = random.Next(1000, 9999);
            uniqueId = !trainers.Any(t => t.TrainerID == id);
        } while (!uniqueId); //checks if ID exists

        trainers.Add(new Trainer { TrainerID = id, TrainerName = name, MailingAddress = address, EmailAddress = email });
        Console.WriteLine($"Trainer added successfully with ID {id}. Press any key to continue...");
        Console.ReadKey();
    }

    private static void EditTrainer(List<Trainer> trainers)
    {
        Console.Clear();
        Console.WriteLine("Edit Trainer");

        Console.Write("Enter the Trainer ID to edit: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid input. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        Trainer trainer = trainers.Find(t => t.TrainerID == id);
        //error management
        if (trainer == null)
        {
            Console.WriteLine("Trainer not found. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        Console.Write("Trainer Name (leave blank to keep unchanged): ");
        string name = Console.ReadLine();
        Console.Write("Mailing Address (leave blank to keep unchanged): ");
        string address = Console.ReadLine();
        Console.Write("Email Address (leave blank to keep unchanged): ");
        string email = Console.ReadLine();

        if (!string.IsNullOrEmpty(name)) trainer.TrainerName = name;
        if (!string.IsNullOrEmpty(address)) trainer.MailingAddress = address;
        if (!string.IsNullOrEmpty(email)) trainer.EmailAddress = email;

        Console.WriteLine("Trainer updated successfully. Press any key to continue...");
        Console.ReadKey();
    }

    private static void DeleteTrainer(List<Trainer> trainers)
    {
        Console.Clear();
        Console.WriteLine("Delete Trainer");

        Console.Write("Enter the Trainer ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid input. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        Trainer trainer = trainers.Find(t => t.TrainerID == id);

        if (trainer == null) //error handling
        {
            Console.WriteLine("Trainer not found. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        trainers.Remove(trainer);
        Console.WriteLine("Trainer deleted successfully. Press any key to continue...");
        Console.ReadKey();
    }

    private static void ViewAllTrainers(List<Trainer> trainers)
    {
        Console.Clear();
        Console.WriteLine("View All Trainers");
        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("Trainer ID\tTrainer Name\tMailing Address\t\tEmail Address");
        Console.WriteLine("-----------------------------------------------------------------");

        if (trainers.Count == 0)
        {
            Console.WriteLine("No trainers found.");
        }
        else
        {
            foreach (Trainer trainer in trainers)
            {
                Console.WriteLine($"{trainer.TrainerID}\t\t{trainer.TrainerName}\t\t{trainer.MailingAddress}\t\t{trainer.EmailAddress}");
            }
        }

        Console.WriteLine("-----------------------------------------------------------------");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    private static void ManageListingData()
    {
        List<Listing> listings;

        if (File.Exists(ListingsFileName))
        {
            listings = FileManager.LoadFromFile(ListingsFileName, Listing.FromString);
        }
        else
        {
            listings = new List<Listing>();
            FileManager.SaveToFile(listings, ListingsFileName);
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage Listing Data");
            Console.WriteLine("1. Add Listing");
            Console.WriteLine("2. Edit Listing");
            Console.WriteLine("3. Delete Listing");
            Console.WriteLine("4. View All Listings");
            Console.WriteLine("5. Back to Main Menu");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddListing(listings);
                    break;
                case 2:
                    EditListing(listings);
                    break;
                case 3:
                    DeleteListing(listings);
                    break;
                case 4:
                    ViewAllListings(listings);
                    break;
                case 5:
                    FileManager.SaveToFile(listings, ListingsFileName);
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void AddListing(List<Listing> listings)
    {
        Console.Clear();
        Console.WriteLine("Add Listing");

        int id = GenerateUniqueListingID(listings);

        Console.Write("Trainer Name: ");
        string trainerName = Console.ReadLine();

        DateTime sessionDate;
        string input;
        do
        {
            Console.Write("Date of the Session (yyyy-MM-dd): ");
            input = Console.ReadLine();
        } while (!DateTime.TryParse(input, out sessionDate) && InvalidInputMessage());

        TimeSpan sessionTime;
        do
        {
            Console.Write("Time of the Session (hh:mm): ");
            input = Console.ReadLine();
        } while (!TimeSpan.TryParse(input, out sessionTime) && InvalidInputMessage());

        decimal cost;
        do
        {
            Console.Write("Cost of the Session: ");
            input = Console.ReadLine();
        } while (!decimal.TryParse(input, out cost) && InvalidInputMessage());

        bool isTaken;
        do
        {
            Console.Write("Is the listing taken? (yes/no): ");
            input = Console.ReadLine().ToLower();
        } while (!IsValidYesNo(input, out isTaken) && InvalidInputMessage());

        listings.Add(new Listing { ListingID = id, TrainerName = trainerName, SessionDate = sessionDate, SessionTime = sessionTime, SessionCost = cost, IsTaken = isTaken });
        Console.WriteLine("Listing added successfully. Press any key to continue...");
        Console.ReadKey();
    }

    private static int GenerateUniqueListingID(List<Listing> listings)
    {
        int id;
        Random random = new Random();
        do
        {
            id = random.Next(1000, 9999);
        } while (listings.Any(l => l.ListingID == id));

        return id;
    }

    private static void EditListing(List<Listing> listings)
    {
        Console.Clear();
        Console.WriteLine("Edit Listing");

        int id;
        string input;

        do
        {
            Console.Write("Enter the Listing ID to edit: ");
            input = Console.ReadLine();
        } while (!int.TryParse(input, out id));

        Listing listing = listings.Find(l => l.ListingID == id);

        if (listing == null)
        {
            Console.WriteLine("Listing not found. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        Console.Write("Trainer Name (leave blank to keep unchanged): ");
        string trainerName = Console.ReadLine();
        Console.Write("Date of the Session (yyyy-MM-dd, leave blank to keep unchanged): ");
        string sessionDateString = Console.ReadLine();
        Console.Write("Time of the Session (hh:mm, leave blank to keep unchanged): ");
        string sessionTimeString = Console.ReadLine();
        Console.Write("SessionCost of the Session (leave blank to keep unchanged): ");
        string costString = Console.ReadLine();
        Console.Write("Is the listing taken? (yes/no, leave blank to keep unchanged): ");
        string isTakenString = Console.ReadLine();

        if (!string.IsNullOrEmpty(trainerName)) listing.TrainerName = trainerName;

        DateTime sessionDate;
        if (!string.IsNullOrEmpty(sessionDateString) && DateTime.TryParse(sessionDateString, out sessionDate))
        {
            listing.SessionDate = sessionDate;
        }

        TimeSpan sessionTime;
        if (!string.IsNullOrEmpty(sessionTimeString) && TimeSpan.TryParse(sessionTimeString, out sessionTime))
        {
            listing.SessionTime = sessionTime;
        }

        decimal cost;
        if (!string.IsNullOrEmpty(costString) && decimal.TryParse(costString, out cost))
        {
            listing.SessionCost = cost;
        }

        if (!string.IsNullOrEmpty(isTakenString))
        {
            listing.IsTaken = isTakenString.ToLower() == "yes";
        }

        Console.WriteLine("Listing updated successfully. Press any key to continue...");
        Console.ReadKey();
    }

    private static void DeleteListing(List<Listing> listings)
    {
        Console.Clear();
        Console.WriteLine("Delete Listing");

        int id;
        string input;

        do
        {
            Console.Write("Enter the Listing ID to delete: ");
            input = Console.ReadLine();
        } while (!int.TryParse(input, out id));

        Listing listing = listings.Find(l => l.ListingID == id);

        if (listing == null)
        {
            Console.WriteLine("Listing not found. Press any key to continue...");
            Console.ReadKey();
            return;
        }

        listings.Remove(listing);
        Console.WriteLine("Listing deleted successfully. Press any key to continue...");
        Console.ReadKey();
    }

    private static void ViewAllListings(List<Listing> listings)
    {
        Console.Clear();
        Console.WriteLine("View All Listings");
        Console.WriteLine("-------------------------------------------------------------------------------------------");
        Console.WriteLine("Listing ID\tTrainer Name\tSession Date\tSession Time\tCost\t\tIs Taken");
        Console.WriteLine("-------------------------------------------------------------------------------------------");

        if (listings.Count == 0)
        {
            Console.WriteLine("No listings found.");
        }
        else
        {
            foreach (Listing listing in listings)
            {
                Console.WriteLine($"{listing.ListingID}\t\t{listing.TrainerName}\t\t{listing.SessionDate.ToShortDateString()}\t{listing.SessionTime}\t\t{listing.SessionCost+"$"}\t\t{listing.IsTaken}");
            }
        }

        Console.WriteLine("-------------------------------------------------------------------------------------------");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    private static bool InvalidInputMessage()
    {
        Console.WriteLine("Invalid input. Please try again.");
        return false;
    }

    private static bool IsValidYesNo(string input, out bool result)
    {
        if (input == "yes")
        {
            result = true;
            return true;
        }
        else if (input == "no")
        {
            result = false;
            return true;
        }
        else
        {
            result = false;
            return false;
        }
    }

    private static void ManageCustomerBookingData()
    {
        List<Transaction> transactions;

        if (File.Exists(TransactionsFileName))
        {
            transactions = FileManager.LoadFromFile(TransactionsFileName, Transaction.FromString);
        }
        else
        {
            transactions = new List<Transaction>();
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage Customer Booking Data");
            Console.WriteLine("1. View Available Training Sessions");
            Console.WriteLine("2. Book a Session");
            Console.WriteLine("3. Back to Main Menu");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please try again.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    ViewAvailableTrainingSessions();
                    break;
                case 2:
                    BookSession(transactions);
                    break;
                case 3:
                    FileManager.SaveToFile(transactions, TransactionsFileName);
                    return;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static void ViewAvailableTrainingSessions()
    {
        Console.Clear();
        Console.WriteLine("View Available Training Sessions");

        if (!File.Exists(ListingsFileName))
        {
            Console.WriteLine("Listings file not found. Creating a new empty listings file.");
            FileManager.SaveToFile(new List<Listing>(), ListingsFileName);
        }

        List<Listing> listings = FileManager.LoadFromFile(ListingsFileName, Listing.FromString);
        List<Listing> availableListings = listings.Where(l => !l.IsTaken).ToList();

        Console.WriteLine("-------------------------------------------------------------------------------------------");
        Console.WriteLine("Listing ID\tTrainer Name\tSession Date\tSession Time\tCost");
        Console.WriteLine("-------------------------------------------------------------------------------------------");

        foreach (Listing listing in availableListings)
        {
            Console.WriteLine($"{listing.ListingID}\t\t{listing.TrainerName}\t\t{listing.SessionDate.ToShortDateString()}\t{listing.SessionTime}\t\t{listing.SessionCost}");
        }

        Console.WriteLine("-------------------------------------------------------------------------------------------");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private static void BookSession(List<Transaction> transactions)
    {
      Console.Clear();
      Console.WriteLine("Book a Session");

      Console.Write("Enter Listing ID to book: ");
      int listingId = int.Parse(Console.ReadLine());

      List<Listing> listings = FileManager.LoadFromFile(ListingsFileName, Listing.FromString);
      Listing listing = listings.FirstOrDefault(l => l.ListingID == listingId && !l.IsTaken);

      if (listing == null)
      {
        Console.WriteLine("Listing not found or already taken. Press any key to continue...");
        Console.ReadKey();
        return;
      }

      Console.Write("Customer Name: ");
      string customerName = Console.ReadLine();
      Console.Write("Customer Email: ");
      string customerEmail = Console.ReadLine();

      listing.IsTaken = true;
      FileManager.SaveToFile(listings, ListingsFileName);

      Transaction transaction = new Transaction
      {
        SessionID = listing.ListingID,
        CustomerName = customerName,
        CustomerEmail = customerEmail,
        TrainingDate = listing.SessionDate,
        TrainerID = listing.TrainerID, // Assuming the trainer name is unique
        TrainerName = listing.TrainerName,
        Status = "Booked"
      };

      transactions.Add(transaction);

      Console.WriteLine("Session booked successfully. Press any key to continue...");
      Console.ReadKey();
    }

    private static void RunReports()
    {
        List<Transaction> transactions;

        if (File.Exists(TransactionsFileName))
        {
            transactions = FileManager.LoadFromFile(TransactionsFileName, Transaction.FromString);
        }
        else
        {
            transactions = new List<Transaction>();
        }

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Run Reports");
            Console.WriteLine("1. Individual Customer Sessions");
            Console.WriteLine("2. Historical Customer Sessions");
            Console.WriteLine("3. Historical Revenue Report");
            Console.WriteLine("4. Back to Main Menu");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please try again.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    IndividualCustomerSessions(transactions);
                    break;
                case 2:
                    HistoricalCustomerSessions(transactions);
                    break;
                case 3:
                    HistoricalRevenueReport(transactions);
                    break;
                case 4:
                    return;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    private static void IndividualCustomerSessions(List<Transaction> transactions)
    {
      Console.Clear();
      Console.WriteLine("Individual Customer Sessions");

      Console.Write("Enter the customer email: ");
      string customerEmail = Console.ReadLine();

      List<Transaction> customerTransactions = transactions
          .Where(t => t.CustomerEmail == customerEmail)
          .OrderBy(t => t.TrainingDate)
          .ToList();

      DisplayTransactions(customerTransactions, "Individual Customer Sessions");

      Console.Write("Do you want to save this report to a file? (yes/no): ");
      if (Console.ReadLine().ToLower() == "yes")
      {
        FileManager.SaveToFile(customerTransactions, $"Individual_Customer_Sessions_{customerEmail.Replace('@', '_').Replace('.', '_')}.txt");
        Console.WriteLine("Report saved successfully. Press any key to continue...");
      }
      else
      {
        Console.WriteLine("Press any key to continue...");
      }
      Console.ReadKey();
    }

    private static void HistoricalCustomerSessions(List<Transaction> transactions)
    {
      Console.Clear();
      Console.WriteLine("Historical Customer Sessions");

      List<Transaction> sortedTransactions = transactions
          .OrderBy(t => t.CustomerEmail)
          .ThenBy(t => t.TrainingDate)
          .ToList();

      DisplayTransactions(sortedTransactions, "Historical Customer Sessions");

      Console.Write("Do you want to save this report to a file? (yes/no): ");
      if (Console.ReadLine().ToLower() == "yes")
      {
        FileManager.SaveToFile(sortedTransactions, "Historical_Customer_Sessions.txt");
        Console.WriteLine("Report saved successfully. Press any key to continue...");
      }
      else
      {
        Console.WriteLine("Press any key to continue...");
      }
      Console.ReadKey();
    }

    private static void HistoricalRevenueReport(List<Transaction> transactions)
    {
      Console.Clear();
      Console.WriteLine("Historical Revenue Report");

      var revenueByMonth = transactions
          .GroupBy(t => new { t.TrainingDate.Year, t.TrainingDate.Month })
          .Select(g => new { Year = g.Key.Year, Month = g.Key.Month, Revenue = g.Sum(t => t.SessionCost) })
          .OrderBy(x => x.Year)
          .ThenBy(x => x.Month);

      Console.WriteLine("--------------------------------------------------");
      Console.WriteLine("Year\tMonth\tRevenue");
      Console.WriteLine("--------------------------------------------------");

      foreach (var item in revenueByMonth)
      {
        Console.WriteLine($"{item.Year}\t{item.Month}\t{item.Revenue}");
      }

      Console.WriteLine("--------------------------------------------------");

      Console.Write("Do you want to save this report to a file? (yes/no): ");
      if (Console.ReadLine().ToLower() == "yes")
      {
        using (StreamWriter sw = new StreamWriter("Historical_Revenue_Report.txt"))
        {
          sw.WriteLine("Historical Revenue Report");
          sw.WriteLine("--------------------------------------------------");
          sw.WriteLine("Year\tMonth\tRevenue");
          sw.WriteLine("--------------------------------------------------");

          foreach (var item in revenueByMonth)
          {
            sw.WriteLine($"{item.Year}\t{item.Month}\t{item.Revenue}");
          }

          sw.WriteLine("--------------------------------------------------");
        }

        Console.WriteLine("Report saved successfully. Press any key to continue...");
      }
      else
      {
        Console.WriteLine("Press any key to continue...");
      }
      Console.ReadKey();
    }
    private static void DisplayTransactions(List<Transaction> transactions, string title)
    {
      Console.WriteLine($"---------- {title} ----------");
      Console.WriteLine("-------------------------------------------------------------------------------------------");
      Console.WriteLine("Session ID\tCustomer Name\tCustomer Email\t\tTraining Date\tTrainer ID\tTrainer Name");
      Console.WriteLine("-------------------------------------------------------------------------------------------");

      foreach (Transaction transaction in transactions)
      {
        Console.WriteLine($"{transaction.SessionID}\t\t{transaction.CustomerName}\t\t{transaction.CustomerEmail}\t\t{transaction.TrainingDate.ToShortDateString()}\t{transaction.TrainerID}\t\t{transaction.TrainerName}");
      }

      Console.WriteLine("-------------------------------------------------------------------------------------------");
    }
  }
}
