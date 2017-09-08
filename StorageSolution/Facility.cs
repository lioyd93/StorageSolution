using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace StorageSolution
{
    public static class Facility
    {
        //Dictionary of storages 
        static Dictionary<string, Storage> storages = new Dictionary<string, Storage>();
        //File to save and load storages 
        private static string filename = "generic";
        //method to create random storage and fill it with items , max weight and size for item will be 10;
        public static void RandomStorage()
        {
            Storage storage = new Storage("Auto", 1000, 100, 100);
            storages.Add(storage.Id, storage);
            Random random = new Random();
            int appendix = 1;
            while (true)
            {
                string itemId = "ABC" + appendix;
                appendix++;
                int itemSize = random.Next(10) + 1;
                int itemWeight = random.Next(10) + 1;
                Item item = new Item(itemId, itemSize, itemWeight);
                if (storage.Add(item) == -1)
                    break;
            }
        }

        public static Dictionary<string, Storage> Storages
        {
            get => storages;
        }

        // Add storage through storage id 
        public static void Add(Storage storage)
        {
            storages.Add(storage.Id, storage);
        }

        //Find storage through storage id 
        public static Storage Find(string storageID)
        {// go through all storages if it exist return this storage
            foreach (Storage storage in storages.Values)
            {
                if (storage.Id == storageID)
                    return storage;
            }
            return null;
        }

        public static void SaveStorages()
        {
            // Persist to file
            using (System.IO.FileStream stream = File.Create(filename))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, Storages);
            }
            Console.WriteLine("The storages are saved.");
        }

        public static void LoadStorages()
        {
            using (FileStream stream = File.OpenRead(filename))
            {
                // Read binary file
                var d = new BinaryFormatter();
                // Interpret file as a storage dictionary
                Dictionary<string, Storage> result = (Dictionary<string, Storage>)d.Deserialize(stream);
                //put result dictionary in storages
                storages = result;
            } // end using Filestream
            Console.WriteLine("Storages have been successfully loaded.");
        }
    }
}
