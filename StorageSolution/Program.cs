using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StorageSolution
{
    public class Program
    {
        static bool ValidateString(string someString)
        {
            bool valid = false;
            Regex r1 = new Regex("^[A-Za-z0-9]+$");
            Match match = r1.Match(someString);
            if (someString.Length <= 50 && match.Success)
            {
                valid = true;
            }
            else
            {
                valid = false;
            }
            return valid;
        }

        static void DisplayStorages()
        {
            foreach (Storage storage in Facility.Storages.Values)
            {
                Console.WriteLine("Storage Area: " + storage.Id);
            }
        }


        static void DisplayStorageContent(Storage storage)
        {
            StringBuilder s = new StringBuilder();

            foreach (var idShelf in storage.Shelves)
            {
                Shelf<Item> shelf = idShelf.Value;
                s.Append(shelf.ToString() + "\n");
            }

            Console.Write(s);
        }

        static void DisplayMenu()
        {
            bool menu = true;
            while (menu)
            {
                Console.Clear();
                Console.WriteLine("~~~ Meny ~~~\n");
                Console.Write("0. Display Storages\n1. Add\n2. Remove\n3. Move\n4. Search\n5. Optimize\n6. Display\n7. Save\n8. Load\n9. Exit\n\n"); // menu
                string storageID;
                switch (Console.ReadLine())
                {
                    case "0":
                        Console.Clear();
                        DisplayStorages();
                        break;
                    case "1":
                        Console.Clear();
                        Console.Write("What do you want to add:\n\n1. Item\n2. Shelf\n3. Storage\n\n");
                        switch (Console.ReadLine())
                        {
                            case "1":
                                Console.Clear();
                                DisplayStorages();//Added this here to make it easier to choose storage item is added to.
                                Console.Write("Which storage do you want to add the item to? ");
                                storageID = Console.ReadLine();
                                if (Facility.Find(storageID) == null)
                                {
                                    Console.WriteLine("That storage was not found.");
                                    break;
                                }
                                Console.Write("Name: ");
                                string id = Console.ReadLine();
                                if (!ValidateString(id))
                                {
                                    Console.WriteLine("\nInput is invalid, try again with a maximum of fifty letters and numbers.");
                                    Console.ReadLine();
                                    break;
                                }
                                Console.Clear();
                                Console.Write("Enter size in m3: ");
                                string size = Console.ReadLine();
                                if (!double.TryParse(size, out double testSize))
                                {
                                    Console.WriteLine("\nInput is invalid, try again with only double type values.");
                                    Console.ReadLine();
                                    break;
                                }
                                Console.Clear();
                                Console.Write("Enter weight in kg: ");
                                string weight = Console.ReadLine();
                                if (int.TryParse(weight, out int testWeight))
                                {
                                    Storage storage = Facility.Find(storageID);
                                    Item item = new Item(id, testSize, testWeight);
                                    storage.Add(item);
                                    if (testWeight < 0)
                                    {
                                        Console.WriteLine("Anti Gravity Plating Added to Shelf.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nInput is invalid, try again with only integer type values.");
                                    Console.ReadLine();
                                    break;
                                }
                                break;
                            case "2":
                                Console.Clear();
                                Console.Write("Enter name of storage facility: ");
                                string storageName = Console.ReadLine();
                                if (Facility.Find(storageName) == null)
                                {
                                    Console.WriteLine("That storage was not found.");
                                    Console.ReadLine();
                                    break;
                                }
                                Console.Clear();
                                Console.Write("Enter shelf ID: ");
                                string shelfId = Console.ReadLine();
                                if (!Int32.TryParse(shelfId, out int testShelf))
                                {
                                    Console.WriteLine("\nInput is invalid, try again with a maximum of fifty letters and numbers.");
                                    Console.ReadLine();
                                    break;
                                }
                                Console.Clear();
                                Console.Write("Enter size in m3: ");
                                string size2 = Console.ReadLine();
                                if (!double.TryParse(size2, out double testSize1))
                                {
                                    Console.WriteLine("\nInput is invalid, try again with only double type values.");
                                    Console.ReadLine();
                                    break;
                                }
                                Console.Clear();
                                Console.Write("Enter weight in kg: ");
                                string weight2 = Console.ReadLine();
                                if (int.TryParse(weight2, out int testWeight1))
                                {
                                    Storage storage = Facility.Find(storageName);
                                    Shelf<Item> shelf = new Shelf<Item>(testShelf, testSize1, testWeight1);
                                }
                                else
                                {
                                    Console.WriteLine("\nInput is invalid, try again with only integer type values.");
                                    Console.ReadLine();
                                    break;
                                }
                                break;

                            case "3":
                                Console.Clear();
                                Console.Write("Add new storage facilities: \n\n1. Empty storage facility\n2. Storage facility with shelves\n\n");

                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        Console.Clear();
                                        Console.Write("What name do you want for your storage facility: ");
                                        string emptyStorageName = Console.ReadLine(); // miss exception
                                        if (ValidateString(emptyStorageName))
                                        {
                                            Storage storage = new Storage(emptyStorageName);
                                            Facility.Add(storage);
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nInput is invalid, try again with a maximum of fifty letters and numbers.");
                                            Console.ReadLine();
                                            break;
                                        }
                                        break;
                                    case "2":
                                        Console.Clear();
                                        Console.Write("What name do you want for your storage facility: ");
                                        string filledStorageName = Console.ReadLine(); // miss exception
                                        if (ValidateString(filledStorageName))
                                        {

                                            Console.Clear();
                                            Console.Write("Enter amount of shelves: ");
                                            string shelves = Console.ReadLine();
                                            if (int.TryParse(shelves, out int test21))
                                            {
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nInput is invalid, try again with only integer type values.");
                                                Console.ReadLine();
                                                break;
                                            }
                                            Console.Clear();
                                            Console.Write("Enter size in m3: ");
                                            string size3 = Console.ReadLine();
                                            if (double.TryParse(size3, out double test22))
                                            {
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nInput is invalid, try again with only double type values.");
                                                Console.ReadLine();
                                                break;
                                            }
                                            Console.Clear();
                                            Console.Write("Enter weight in kg: ");
                                            string weight3 = Console.ReadLine();
                                            if (int.TryParse(weight3, out int test23))
                                            {
                                            }
                                            else
                                            {
                                                Console.WriteLine("\nInput is invalid, try again with only integer type values.");
                                                Console.ReadLine();
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nInput is invalid, try again with a maximum of twenty letters and numbers.");
                                            Console.ReadLine();
                                            break;
                                        }
                                        break;

                                    default:
                                        Console.WriteLine("You wrote something that wasn't 1-2, try again.");
                                        Console.ReadLine();
                                        break;
                                }
                                break;
                            default:
                                Console.WriteLine("You wrote something that wasn't 1-3, try again.");
                                Console.ReadLine();
                                break;
                        }
                        break;



                    case "2":
                        Console.Clear();
                        DisplayStorages();
                        Console.Write("Which storage would you like to remove the item from? ");
                        storageID = Console.ReadLine();
                        if (Facility.Find(storageID) == null)
                        {
                            Console.WriteLine("That storage was not found.");
                            break;
                        }
                        Console.Write("Remove item:\n\nID: ");
                        string itemId = Console.ReadLine();
                        if (ValidateString(itemId))
                        {
                            Storage storage = Facility.Find(storageID);
                            Console.WriteLine($"Please remove the item {storage.Shelves[storage.Find(itemId)].Find(itemId).Id} from shelf {storage.Remove(itemId)}");
                        }
                        string remove = Console.ReadLine();

                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Move vehicle:\n");
                        Console.Write("Move item:\n\nItem name: ");
                        string moveitem = Console.ReadLine();
                        break;
                    case "4":
                        Console.Clear();
                        // List Storages Here
                        Console.Write("Which storage? ");
                        storageID = Console.ReadLine();
                        if (Facility.Find(storageID) != null)
                        {
                            Console.Write("Search item:\n\nItem name: ");
                            string searchitem = Console.ReadLine();
                            Storage storage = Facility.Find(storageID);
                            if (storage.Find(searchitem) != -1)
                            {
                                Console.WriteLine($"That item is stored in shelf {storage.Find(searchitem)}");
                            }
                        }
                        
                        
                        break;

                    case "5":
                        Console.Clear();
                        Console.Write("Which storage? ");
                        storageID = Console.ReadLine();
                        if (Facility.Find(storageID) != null)
                            Console.Write(Facility.Find(storageID).Optimish());
                        break;

                    case "6":
                        Console.Clear();
                        Console.Write("Which storage? ");
                        string storageID2 = Console.ReadLine();
                        if (Facility.Find(storageID2) != null)
                            DisplayStorageContent(Facility.Find(storageID2));
                        break;

                    case "7":
                        Facility.SaveStorages();
                        break;

                    case "8":
                        Facility.LoadStorages();
                        break;

                    case "9":
                        menu = false;
                        break;
                    default:
                        Console.WriteLine("You pressed something besides 1-9, try again.");
                        break;
                }
                if (menu)
                    Console.ReadKey();
            }
        }

        static void Main(string[] args)
        {
            Facility.RandomStorage();
            DisplayMenu();


        }
        
    }
}
