using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace SportsStore.Models
{
    public class Cart
    {
        [JsonInclude]
        public List<CartLine> LineCollection { get; private set; } = new List<CartLine>();

        public virtual void AddItem(Product product, int quantity)
        {
            CartLine? line = LineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                LineCollection.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) =>
            LineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);

        public virtual decimal ComputeTotalValue() =>
            LineCollection.Sum(e => e.Product.Price * e.Quantity);

        public virtual void Clear() => LineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => LineCollection;

        public virtual void UpdateQuantity(Product product, int quantity)
        {
            CartLine? line = LineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line != null)
            {
                if (quantity > 0)
                {
                    line.Quantity = quantity;
                }
                else
                {
                    RemoveLine(product);
                }
            }
        }
    }
}