using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Inventory_Application.Controller;

/*
 * Here we will establish our connection to the database so we can load it on start up
 */

namespace Inventory_Application.Model
{
    class DatabaseConnection
    {
        public String errorCode;
        MySqlConnection con;
        
        String connectionString = "server=" + server + ";uid=" + uid + ";pwd=" + password + ";database=" + database + ";";

        /*
         * open a connection to our database
         */
        private void openConnection()
        {
            try
            {
                con = new MySql.Data.MySqlClient.MySqlConnection();
                con.ConnectionString = connectionString;
                con.Open();
            } catch (Exception ex) //add actual exceptions here
            {

            }
        }
        
        /*
         * close the connection to our database
         */
        private void closeConnection()
        {
            con.Close();
        }

        /*
         * update a record within our database
         */

        public bool update(int skuNumber, string itemName, string description, int quantity, double price)
        {
            bool success = false;
            openConnection();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "UPDATE productsku SET ITEMNAME = '" + itemName + "', DETAILS = '" + description + "', QUANTITY = " + quantity + ", PRICE = " + price + " WHERE SKUNUMBER = " + skuNumber + ";";
                cmd.Connection = con;
                MySqlDataReader reader = cmd.ExecuteReader();
                success = true;
            }
            catch (Exception ex)
            {
                errorCode = "POST: " + ex.Message;
                success = false;
            }
            closeConnection();
            return success;
        }

        /*
         * gets an item by a specific sku number
         */
        public Product getSku(int skuNum)
        {
            Product product = new Product();

            openConnection();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM productsku WHERE SKUNUMBER = " + skuNum;
            cmd.Connection = con;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32("ID");
                int sku = reader.GetInt32("SKUNUMBER");
                String name = reader.GetString("ITEMNAME");
                String details = reader.GetString("DETAILS");
                int quantity = reader.GetInt32("QUANTITY");
                double price = reader.GetDouble("PRICE");
                product = new Product(id, sku, name, details, quantity, price);
              
            }

            closeConnection();


            return product;
        }

        /*
        * returns a list of items that match the search results for a specific name
        */
        public List<Product> searchByName(string itemName)
        {
            openConnection();

            List<Product> inventoryItems = new List<Product>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM productsku WHERE ITEMNAME LIKE '%" + itemName +"%'";
            cmd.Connection = con;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32("ID");
                int sku = reader.GetInt32("SKUNUMBER");
                String name = reader.GetString("ITEMNAME");
                String details = reader.GetString("DETAILS");
                int quantity = reader.GetInt32("QUANTITY");
                double price = reader.GetDouble("PRICE");
                Product product = new Product(id, sku, name, details, quantity, price);
                inventoryItems.Add(product);
            }

            closeConnection();
            return inventoryItems;
        }


        /*
         * creates a list of all of the items currently stored in our system
         */
        public List<Product> getAllItems()
        {
            openConnection();

            List<Product> inventoryItems = new List<Product>();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM productsku";
            cmd.Connection = con;
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32("ID");
                int sku = reader.GetInt32("SKUNUMBER");
                String name = reader.GetString("ITEMNAME");
                String details = reader.GetString("DETAILS");
                int quantity = reader.GetInt32("QUANTITY");
                double price = reader.GetDouble("PRICE");
                Product product = new Product(id, sku, name, details, quantity, price);
                inventoryItems.Add(product);
            }

            closeConnection();

            return inventoryItems;
        }

        /*
         * Enter a brand new item into our database
         */
        public bool postNewItemToDatabase(int skuNumber, string itemName, string description, int quantity, double price)//String command)
        {
            bool success = false;
            openConnection();
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "insert into productsku(SKUNUMBER, ITEMNAME, DETAILS, QUANTITY, PRICE) values(" + skuNumber + ", '" + itemName + "', '" + description + "', " + quantity + ", " + price + ");";
                cmd.Connection = con;
                /*
                 * if (item doesnt already exist) {
                 * } else {
                 * return that an item with the same sku or name already exists;
                 * }
                 */
                MySqlDataReader reader = cmd.ExecuteReader();
                success = true;
            } catch (Exception ex)
            {
                errorCode = "POST: " + ex.Message;
                success = false;
            }
            closeConnection();
            return success;
        }

    }
}
