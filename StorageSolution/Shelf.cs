using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSolution
{
    [Serializable]
    // Shelf takes Generic type of Item
    public class Shelf<T> : ICloneable where T : Item
    {
        HashSet<T> items; // List of items stored on the shelf
        double sizeCapacity; // Size Capacity of shelf
        int weightCapacity;  // Weight Capacity of shelf
        double remainingSize; // Remaining Size the shelf can take
        int remainingWeight; // Remaining Weight the shelf can take
        int id; // To find the shelf

        public HashSet<T> Items
        {
            get => items;
        }

        public double SizeCapacity
        {
            get => sizeCapacity; set => sizeCapacity = value;
        }

        public int WeightCapacity
        {
            get => weightCapacity; set => weightCapacity = value;
        }

        public double RemainingSize
        {
            get => remainingSize; set => remainingSize = value;
        }

        public int RemainingWeight
        {
            get => remainingWeight; set => remainingWeight = value;
        }

        public int Id
        {
            get => id;
        }

        /// <summary>
        /// Shelf Constructor
        /// </summary>
        /// <param name="size"></param>
        /// <param name="weight"></param>
        public Shelf(int ID, double size, int weight)
        {
            items = new HashSet<T>();
            id = ID;
            sizeCapacity = size;
            weightCapacity = weight;
            remainingSize = size;
            remainingWeight = weight;
        }

        /// <summary>
        /// Takes Item and adds it to Shelf list. Then sets Remainig size and 
        /// weight the shelf can take Depending on the items size and weight
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            items.Add(item);
            remainingSize -= item.SizeM3;
            remainingWeight -= item.WeightInKg;
        }

        /// <summary>
        ///Takes Item and Removes it from Shelf list. 
        /// Then removes items size and weight from the shelf.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {
            items.Remove(item);
            remainingSize += item.SizeM3;
            remainingWeight += item.WeightInKg;
        }

        /// <summary>
        /// Loops through each item in 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find(string id)
        {
            foreach (T item in items)
            {
                if (item.Id.Equals(id))
                    return item;
            }
            return null;
        }

        // Currently unimplemented method which clears out the items set.
        public void Clear()
        {
            items = new HashSet<T>();
            remainingSize = sizeCapacity;
            remainingWeight = weightCapacity;
        }

        // Implements the Clone method required by the ICloneable interface. Creates a shallow clone.
        public Object Clone()
        {
            Shelf<T> clone = new Shelf<T>(id, sizeCapacity, weightCapacity);

            foreach (T item in items)
                clone.Add(item);

            return clone;
        }

        // Overrides the default ToString method inherited from Object.
        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append($"Shelf {id} - size capacity {sizeCapacity}, remaining {remainingSize}; weight capacity {weightCapacity}, remaining {remainingWeight}\n");

            foreach (Item item in items)
                s.Append(item.ToString() + "\n");

            return s.ToString();
        }
    }
}
