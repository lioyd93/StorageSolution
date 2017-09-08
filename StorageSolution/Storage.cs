using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSolution
{
    [Serializable]
    public class Storage
    {
        SortedDictionary<int, Shelf<Item>> shelves;
        string id;

        public SortedDictionary<int, Shelf<Item>> Shelves
        {
            get => shelves;
        }

        public string Id
        {
            get => id;
        }

        public Storage(string ID)
        {
            id = ID;
            shelves = new SortedDictionary<int, Shelf<Item>>();
        }

        public Storage(string ID, int numberOfShelves, double sizeCapacity, int weightCapacity)
        {
            id = ID;
            shelves = new SortedDictionary<int, Shelf<Item>>();

            for (int i = 0; i < numberOfShelves; i++)
            {
                Shelf<Item> shelf = new Shelf<Item>(i + 1, sizeCapacity, weightCapacity);
                shelves.Add(i + 1, shelf);
            }
        }

        public int Add(Item item)
        {
            foreach (Shelf<Item> shelf in shelves.Values)
            {
                if (shelf.RemainingSize >= item.SizeM3 && shelf.RemainingWeight >= item.WeightInKg)
                {
                    shelf.Add(item);
                    return shelf.Id;
                }
            }

            return -1;
        }

        public TimeSpan TimeStored(string itemId)
        {
            int shelf = Find(itemId);

            if (shelf == -1)
            {
                return TimeSpan.MaxValue;
            }

            Item item = shelves[shelf].Find(itemId);
            DateTime added = item.Time;
            DateTime removed = DateTime.Now;
            shelves[shelf].Remove(item);
            return removed - added;
        }

        public int Remove(string itemId)
        {
            int shelf = Find(itemId);

            if (shelf == -1)
            {
                return -1;
            }

            Item item = shelves[shelf].Find(itemId);
            shelves[shelf].Remove(item);
            return shelf;
        }

        /// <summary>
        /// Loops through each  shelf and calls Find by ID function 
        /// that loops through each item in the shelf if it finds Item with matching ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Find(string itemId)
        {
            foreach (Shelf<Item> shelf in shelves.Values)
            {
                if (shelf.Find(itemId) != null)
                    return shelf.Id;
            }

            return -1;
        }

        /// <summary>
        /// Finds item by ID using Find() Function. Checks if destination shelf
        /// has enough space and weight to hold the item, then adds the item to
        /// that shelf and removes it from its original shelf.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public bool Move(string itemId, int destination)
        {
            if (!shelves.Keys.Contains(destination))
                return false;

            int oldShelf = Find(itemId);

            if (oldShelf == -1)
                return false;

            Item item = shelves[oldShelf].Find(itemId);

            if (shelves[destination].RemainingSize >= item.SizeM3 && shelves[destination].RemainingWeight >= item.WeightInKg)
            {
                shelves[destination].Add(item);
                shelves[oldShelf].Remove(item);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Finds a suitable bundle of items to store in the target shelf. Does not actually store the items, it only returns a suitable bundle.
        /// </summary>
        /// <param name="target">The target shelf.</param>
        /// <returns></returns>
        public Bundle<Item> Bundify(Shelf<Item> target)
        {
            Bundle<Item> bundle = new Bundle<Item>();

            while (true)
            {
                Item bestItem = null;
                double leastSpaceWeightRemaining = 1;

                foreach (Shelf<Item> shelf in shelves.Values)
                {
                    foreach (Item item in shelf.Items)
                    {
                        if (!bundle.Items.Contains(item) && shelf.SizeCapacity >= bundle.Size + item.SizeM3 && shelf.WeightCapacity >= bundle.Weight + item.WeightInKg)
                        {
                            double spaceWeightRemaining = 1 - (bundle.Size + item.SizeM3) / shelf.SizeCapacity * (bundle.Weight + item.WeightInKg) / shelf.WeightCapacity;
                            if (spaceWeightRemaining < leastSpaceWeightRemaining)
                            {
                                bestItem = item;
                                leastSpaceWeightRemaining = spaceWeightRemaining;
                            }
                        }
                    }
                }

                if (leastSpaceWeightRemaining < 1)
                    bundle.Add(bestItem);
                else
                    break;
            }

            return bundle;
        }

        /// <summary>
        /// Crudely optimizes the space-weight occupancy of the items stored in this storage facility.
        /// </summary>
        /// <returns>A list of commands for the user to manually move the items.</returns>
        public StringBuilder Optimish()
        {
            // Creates a clone of the dictionary, and also clones each individual shelf contained in the dictionary.
            Dictionary<int, Shelf<Item>> clone = shelves.ToDictionary(entry => entry.Key, entry => (Shelf<Item>) entry.Value.Clone());

            // Creates a sorted clone, and makes the unsorted clone eligible for garbage collection.
            SortedDictionary<int, Shelf<Item>> sortedClone = new SortedDictionary<int, Shelf<Item>>(clone);
            clone = null;

            // Initializes a dictionary of bundles.
            Dictionary<Shelf<Item>, Bundle<Item>> bundles = new Dictionary<Shelf<Item>, Bundle<Item>>();

            // Loops through each shelf, finds a suitable bundle of items to store in that shelf,
            // saves that bundle, and removes those items from their respective shelves.
            foreach (Shelf<Item> shelf in shelves.Values)
            {
                Bundle<Item> bundle = Bundify(shelf);
                bundles.Add(shelf, bundle);

                foreach (Item item in bundle.Items)
                {
                    Remove(item.Id);
                }
            }

            // Aborts the algorithm if we done fucked up, i.e. the bundles we created did not manage to contain all the items.
            foreach (Shelf<Item> shelf in shelves.Values)
            {
                if (shelf.Items.Count != 0)
                {
                    shelves = sortedClone;
                    return null;
                }
            }

            // Unpacks each bundle and adds its items to its designated shelf.
            foreach (var shelfBundle in bundles)
            {
                foreach (Item item in shelfBundle.Value.Items)
                    shelfBundle.Key.Add(item);
            }

            // Starts listing commands for the user to manually move the items.
            StringBuilder commands = new StringBuilder();

            // Loops through each shelf in the clone, and checks if its items have been moved.
            foreach (Shelf<Item> oldShelf in sortedClone.Values)
            {
                foreach (Item item in oldShelf.Items)
                {
                    int newShelfId = Find(item.Id);
                    if (oldShelf.Id != newShelfId)
                        commands.Append($"Please move item {item.Id} from shelf {oldShelf.Id} to shelf {newShelfId}\n");
                }
            }

            return commands;
        }
    }
}
