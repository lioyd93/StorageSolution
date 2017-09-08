using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageSolution
{
    public class Bundle<T> where T : Item
    {
        HashSet<T> items;
        double size;
        int weight;

        public HashSet<T> Items
        {
            get => items;
        }

        public double Size
        {
            get => size;
        }

        public int Weight
        {
            get => weight;
        }

        public Bundle()
        {
            items = new HashSet<T>();
            size = 0;
            weight = 0;
        }

        public void Add(T item)
        {
            items.Add(item);
            size += item.SizeM3;
            weight += item.WeightInKg;
        }
    }
}
