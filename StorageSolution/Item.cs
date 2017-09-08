using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSolution
{
    [Serializable]
    public class Item
    {
        protected string id; // Identifier
        protected double sizeM3; // Size in Cubic Meters
        protected int weightInKg; // Weight of the item
        protected DateTime time; // When item was created

        // Getters
        public string Id
        {
            get => id;
        }

        public double SizeM3
        {
            get => sizeM3;
        }

        public int WeightInKg
        {
            get => weightInKg;
        }

        public DateTime Time
        {
            get => time;
        }

        // Constructor
        public Item(string ID, double SizeM3, int WeightInKg)
        {
            id = ID;
            sizeM3 = SizeM3;
            weightInKg = WeightInKg;
            time = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Item {id}, size {sizeM3}, weight {weightInKg}";
        }
    }
}