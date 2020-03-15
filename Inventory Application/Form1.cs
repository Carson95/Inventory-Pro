using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Inventory_Application.Controller;

namespace Inventory_Application
{
    public partial class Form1 : Form
    {
        private Model.DatabaseConnection db;

        List<Product> products;

        public Form1()
        {
            InitializeComponent();
            db = new Model.DatabaseConnection();
            loadInventoryItems();
            updateStatus(false);
        }

        private void updateStatus(bool status)
        {
            if (status)
            {
                btnAddProduct.Enabled = false;
                btnUpdate.Enabled = true;
                txtSku.Enabled = false;
            } else
            {
                btnUpdate.Enabled = false;
                btnAddProduct.Enabled = true;
                txtSku.Enabled = true;
            }
        }

        private void clearText()
        {
            txtSku.Text = "";
            txtName.Text = "";
            txtDetails.Text = "";
            txtQuantity.Text = "";
            txtPrice.Text = "";
        }

        private void loadInventoryItems()
        {
                products = db.getAllItems();
                lbInventoryItems.DataSource = products;
        }

        private void txtBxItemName_TextChanged(object sender, EventArgs e)
        {
            products = db.searchByName(txtBxItemName.Text);
            lbInventoryItems.DataSource = products;
            lbInventoryItems.Update();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            //details can not be longer than 50
            if (!string.IsNullOrEmpty(txtSku.Text) || !string.IsNullOrEmpty(txtName.Text) || !string.IsNullOrEmpty(txtDetails.Text) || !string.IsNullOrEmpty(txtQuantity.Text) || !string.IsNullOrEmpty(txtPrice.Text))
            {
                try
                {
                    db.postNewItemToDatabase(int.Parse(txtSku.Text), txtName.Text, txtDetails.Text, int.Parse(txtQuantity.Text), double.Parse(txtPrice.Text));
                    clearText();
                    loadInventoryItems();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            } else
            {
                MessageBox.Show("You must enter all fields.");
            }
        }

        private void lbInventoryItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbInventoryItems.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                int sku = products[index].SkuNumber;
                Product product = db.getSku(sku);
                txtSku.Text = product.SkuNumber.ToString();
                txtName.Text = product.ItemName.ToString();
                txtDetails.Text = product.ProductDescription.ToString();
                txtQuantity.Text = product.Quantity.ToString();
                txtPrice.Text = product.Price.ToString();
                updateStatus(true);
            }
            
        }

        /*
         * updates the item selected
         * needs validation checks
         */
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            db.update(int.Parse(txtSku.Text), txtName.Text, txtDetails.Text, int.Parse(txtQuantity.Text), double.Parse(txtPrice.Text));
            clearText();
            updateStatus(false);
        }
    }
}
