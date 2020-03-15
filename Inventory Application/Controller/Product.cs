using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Our product data type
 */

namespace Inventory_Application.Controller
{
    class Product
    {

        public Product()
        {

        }

        public Product(int id, int skuNumber, string name, string description, int quantity, double price) 
        {
            this.id = id;
            this.skuNumber = skuNumber;
            this.ItemName = name;
            this.productDescription = description;
            this.quantity = quantity;
            this.price = price;
        }

        public override String ToString()
        {
            return "SKU: " + skuNumber + " - " + itemName;
        }

        private int id;
        private int skuNumber;
        private string itemName;
        private string productDescription;
        private int quantity;
        private double price;

        public int Id { get => id; set => id = value; }
        public int SkuNumber { get => skuNumber; set => skuNumber = value; }
        public string ItemName { get => itemName; set => itemName = value; }
        public string ProductDescription { get => productDescription; set => productDescription = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Price { get => price; set => price = value; }
    }
}
