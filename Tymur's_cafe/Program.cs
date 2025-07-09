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
        static void Main(string[] args)
        {
            
        }
    }
}
