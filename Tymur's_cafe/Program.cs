using System.Diagnostics;

namespace Tymur_s_cafe
{
    internal class Program
    {

        const decimal GST = 5;

        static List<string> descriptions = new();
        static List<decimal> prices = new();

        static decimal tipAmount;
        static decimal netTotal;
        static decimal totalGST;
        static decimal totalAmount;
        static void Menu()
        {
            Console.WriteLine("╔═════════════════════════════╗");
            Console.WriteLine("║        Tymur's Cafe         ║");
            Console.WriteLine("║─────────────────────────────║");
            Console.WriteLine("║ 1. Add Item                 ║");
            Console.WriteLine("║ 2. Remove Item              ║");
            Console.WriteLine("║ 3. Add Tip                  ║");
            Console.WriteLine("║ 4. Display Bill             ║");
            Console.WriteLine("║ 5. Clear All                ║");
            Console.WriteLine("║ 6. Save to file             ║");
            Console.WriteLine("║ 7. Load from file           ║");
            Console.WriteLine("║ 0. Exit                     ║");
            Console.WriteLine("╚═════════════════════════════╝");

        }

        static void AddItem(string description, decimal price)
        {
            descriptions.Add(description);
            prices.Add(price);
        }

        static void AddItemMenu()
        {

            if (descriptions.Count == 5)
            {
                Console.WriteLine("There are 5 items in the bill. This is the maximum.\n");
                return;
            }

            string description;
            decimal price;

            Console.WriteLine();

            do
            {
                Console.Write("Enter description: ");
                description = Console.ReadLine();
                if (string.IsNullOrEmpty(description) || !(description.Length >= 3 && description.Length <= 20))
                    Console.WriteLine("Incorrect description: [3-20] symbols!");
            } while (string.IsNullOrEmpty(description) || !(description.Length >= 3 && description.Length <= 20));


            bool res;
            do
            {
                Console.Write("Enter price: ");
                res = decimal.TryParse(Console.ReadLine(), out price);
                if (!res || price <= 0)
                    Console.WriteLine("Incorrect price: more than zero $!");
            } while (!res || price <= 0);

            AddItem(description, price);

            Console.WriteLine("\nAdd item was successful.\n");

        }

        static void AddTipMenu()
        {

            if (descriptions.Count == 0)
            {
                Console.WriteLine("There are no items in the bill to add tip for.\n");
                return;
            }

            СalculateBill();

            Console.WriteLine($"\nNet Total: ${netTotal}");

            Console.WriteLine("\n1 - Tip Percentage");
            Console.WriteLine("2 - Tip Amount");
            Console.WriteLine("3 - No Tip\n");

            int choice;
            bool res;
            int percentage = 0;
            do
            {
                Console.Write("Enter Tip Method: ");
                res = int.TryParse(Console.ReadLine(), out choice);
                if (!res || choice < 1 || choice > 3) Console.WriteLine("Incorrect choice. Try again.\n");

            } while (!res || choice < 1 || choice > 3);

            Console.WriteLine();

            switch (choice)
            {
                case 1:
                    do
                    {
                        Console.Write("Enter tip percentage: ");
                        res = int.TryParse(Console.ReadLine(), out percentage);
                        if (!res || percentage < 1 || percentage > 100) Console.WriteLine("Incorrect tip percentage. Try again.\n");

                    } while (!res || percentage < 1 || percentage > 100);

                    tipAmount = netTotal * percentage / 100;

                    Console.WriteLine("\nTip was added successfully.\n");
                    break;

                case 2:
                    Console.Write("Enter tip amount: ");
                    do
                    {
                        res = decimal.TryParse(Console.ReadLine(), out tipAmount);
                        if (!res || tipAmount <= 0)
                            Console.WriteLine("Incorrect price: more than zero $!");
                    } while (!res || tipAmount <= 0);
                    Console.WriteLine("\nTip was added successfully.\n");
                    break;

                case 3:
                    tipAmount = 0;
                    break;
            }

        }

        static void DispalyAllItems()
        {
            Console.WriteLine($"{"\nItemNo",6}  {"Description",-20}  {"Price",10}");
            Console.Write(new string('-', 6));
            Console.Write("  ");
            Console.Write(new string('-', 20));
            Console.Write("  ");
            Console.Write(new string('-', 10));
            Console.Write('\n');

            for (int i = 0; i < descriptions.Count; i++)
            {
                Console.WriteLine($"{i + 1,6}  {descriptions[i],-20}  {"$" + prices[i].ToString("f2"),10}");
            }

        }

        static void RemoveItemMenu()
        {
            if (descriptions.Count == 0)
            {
                Console.WriteLine("There are no items in the bill to remove.\n");
                return;
            }


            DispalyAllItems();
            int countItems = descriptions.Count;

            int indexItem;
            bool res;

            do
            {
                Console.Write("\nEnter the item number to remove or 0 to cancel: ");
                res = int.TryParse(Console.ReadLine(), out indexItem);
                if (!res || indexItem < 0 || indexItem > countItems)
                    Console.WriteLine("Incorrect choice. Try again.");

            } while (!res || indexItem < 0 || indexItem > countItems);

            if (indexItem > 0)
            {
                descriptions.RemoveAt(indexItem - 1);
                prices.RemoveAt(indexItem - 1);
                tipAmount = 0;
                Console.WriteLine("\nRemove item was successful.\n");

                AddTipMenu();
            }


        }

        static void СalculateBill()
        {
            netTotal = 0;
            totalAmount = 0;

            for (int i = 0; i < descriptions.Count; i++)
            {
                netTotal += prices[i];
            }

            totalGST = netTotal * GST / 100;
            totalAmount = netTotal + totalGST + tipAmount;
        }

        static void DisplayBillMenu()
        {
            if (descriptions.Count == 0)
            {
                Console.WriteLine("There are no items in the bill to display.\n");
                return;
            }

            Console.WriteLine($"{"\nDescription",-20}  {"Price",10}");
            Console.Write(new string('-', 20));
            Console.Write("  ");
            Console.Write(new string('-', 10));
            Console.Write('\n');

            СalculateBill();

            for (int i = 0; i < descriptions.Count; i++)
            {
                Console.WriteLine($"{descriptions[i],-20}  {"$" + prices[i].ToString("f2"),10}");
            }

            Console.Write(new string('-', 20));
            Console.Write("  ");
            Console.Write(new string('-', 10));
            Console.Write('\n');

            Console.WriteLine($"{"Net Total",20}  {"$" + netTotal.ToString("f2"),10}");
            Console.WriteLine($"{"Tip Amount",20}  {"$" + tipAmount.ToString("f2"),10}");
            Console.WriteLine($"{"Total GST",20}  {"$" + totalGST.ToString("f2"),10}");
            Console.WriteLine($"{"Total Amount",20}  {"$" + totalAmount.ToString("f2"),10}\n");

        }


        static void ClearAllMenu()
        {
            if (descriptions.Count == 0)
            {
                Console.WriteLine("There are no items in the bill to clear.\n");
                return;
            }

            descriptions.Clear();
            prices.Clear();
            Console.WriteLine("All items have been cleared.\n");
        }

        static void SaveToFile(string fileName)
        {
            List<string> allLines = new();

            for (int i = 0; i < descriptions.Count; i++)
            {
                string str = $"{descriptions[i]}/{prices[i]}";
                allLines.Add(str);
            }

            File.WriteAllLines(fileName, allLines);
        }

        static void SaveToFileMenu()
        {
            if (descriptions.Count == 0)
            {
                Console.WriteLine("There are no items in the bill to save.\n");
                return;
            }

            string fileName, fileNameTrim;
            bool res;

            do
            {
                Console.Write("\nEnter the file path to save items to: ");
                fileName = Console.ReadLine();
                fileNameTrim = Path.GetFileNameWithoutExtension(fileName);
                if (string.IsNullOrEmpty(fileName) || fileNameTrim.Length < 1 || fileNameTrim.Length > 10)
                    Console.WriteLine("Incorrect file name: [1-10] symbols!");
            } while (string.IsNullOrEmpty(fileName) || fileNameTrim.Length < 1 || fileNameTrim.Length > 10);

            SaveToFile(fileName);

            Console.WriteLine($"Write to file {fileName} was successful.\n");
        }

        static void LoadFromFile(string fileName)
        {
            descriptions.Clear();
            prices.Clear();

            string[] allLines = File.ReadAllLines(fileName);

            foreach (string line in allLines)
            {
                string[] parts = line.Split('/');

                descriptions.Add(parts[0]);
                prices.Add(decimal.Parse(parts[1]));
            }

        }

        static void LoadFromFileMenu()
        {

            string fileName, fileNameTrim;
            bool res;

            do
            {
                Console.Write("\nEnter the file path to load items from: ");
                fileName = Console.ReadLine();
                fileNameTrim = Path.GetFileNameWithoutExtension(fileName);
                if (string.IsNullOrEmpty(fileName) || fileNameTrim.Length < 1 || fileNameTrim.Length > 10)
                    Console.WriteLine("Incorrect file name: [1-10] symbols!");
            } while (string.IsNullOrEmpty(fileName) || fileNameTrim.Length < 1 || fileNameTrim.Length > 10);

            try
            {
                LoadFromFile(fileName);
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("\nFile not found.\n");
                return;
            }

            Console.WriteLine($"Read from {fileName} was successful.\n");

            tipAmount = 0;

        }


        static void Main(string[] args)
        {
            
        }
    }
}
