using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace CarShopForms
{
    public partial class Form1 : Form
    {

        Store myStore = new Store();
        BindingSource carInventoryBindingSource = new BindingSource();
        BindingSource cartBindingSource = new BindingSource();
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            Car c = new Car(txt_Make.Text, txt_model.Text, decimal.Parse(txt_price.Text));
            //MessageBox.Show(c.ToString());
            myStore.CarList.Add(c);
            carInventoryBindingSource.ResetBindings(false);
            txt_Make.Text = "";
            txt_model.Text = "";
            txt_price.Text = "";
            
            System.IO.File.WriteAllText(@"D:\carForms_data.json", JsonConvert.SerializeObject(c));
        }

        private void btn_addtocart_Click(object sender, EventArgs e)
        {
            //Envanter de seçilmiş olan ürünü alır.
            Car selected = (Car)lst_inventory.SelectedItem;
            
            // Bu ürünü sepete ekler.
            myStore.ShoppingList.Add(selected);

            //Liste kutusunu günceller.
            cartBindingSource.ResetBindings(false);
        }

        private void btn_checkout_Click(object sender, EventArgs e)
        {
            decimal total = myStore.Checkout();
            lbl_total.Text = "tl" + total.ToString();

            cartBindingSource.ResetBindings(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            carInventoryBindingSource.DataSource = myStore.CarList;

            cartBindingSource.DataSource = myStore.ShoppingList;

            lst_inventory.DataSource = carInventoryBindingSource;
            lst_inventory.DisplayMember = ToString();

            lst_cart.DataSource = cartBindingSource;
            lst_cart.DisplayMember = ToString();
        }

        private void btn_get_order_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Siparişiniz alındı. Teşekürler!");
        }
    }
}
