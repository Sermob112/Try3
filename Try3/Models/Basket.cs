using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Try3.Models
{
    public class Basket
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(place place, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Place.id == place.id)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Place = place,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(place place)
        {
            lineCollection.RemoveAll(l => l.Place.id == place.id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Place.price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public place Place { get; set; }
        public int Quantity { get; set; }
    }
}
