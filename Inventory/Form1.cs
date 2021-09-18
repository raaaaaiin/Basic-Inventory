using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory
{

    public partial class frmAddProduct : Form
    {
        public frmAddProduct()
        {
            InitializeComponent();
        }

        private void frmAddProduct_Load(object sender, EventArgs e)
        {
            string[] ListOfProductCategory = new string[] { "Beverages", "Breead/Bakery", "Canned/Jarred Goods", "Dairy", "Frozen Goods", "Meat", "Personal Care", "Other" };
            foreach (string item in ListOfProductCategory)
            {
                cbCategory.Items.Add(item);
            }

        }

        //

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
            throw new StringFormatException("Wrong String Format!");
            return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
            throw new NumberFormatException("Wrong Number Format!");
            return Convert.ToInt32(qty);
        }
        public double SellingPrice(string price)
        {

            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
            throw new CurrencyFormatException("Wrong Currency Format!");
            
            //Exception here
            return Convert.ToDouble(price);
        }



        private int _Quantity;
        private double _SellPrice;
        private string _ProductName, _Category, _MfgDate, _ExpDate, _Description;
        BindingSource showProductList = new BindingSource();
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {

            
            _ProductName = Product_Name(txtProductName.Text);
            _Category = cbCategory.Text;
            _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
            _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
            _Description = richTxtDescription.Text;
            _Quantity = Quantity(txtQuantity.Text);
            _SellPrice = SellingPrice(txtSellPrice.Text);
            showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate,
            _ExpDate, _SellPrice, _Quantity, _Description));
            gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridViewProductList.DataSource = showProductList;
            }catch(CurrencyFormatException)
            {
                MessageBox.Show("Invalid Format of currency");
            }
            catch (NumberFormatException a)
            {
                MessageBox.Show("Invalid number input (Numbr only)");
            }
            catch (StringFormatException a)
            {
                MessageBox.Show("Invalid Name of product (Numbers are not allowed)");
            }
        }
    }
   
    public class NumberFormatException : Exception
    {
        public NumberFormatException(string number) : base(number)
        {
        }
    }
    public class StringFormatException : Exception
    {
        public StringFormatException(string String) : base(String)
        {
        }
    }
    public class CurrencyFormatException : Exception
    {
        public CurrencyFormatException(string currency) : base(currency)
        {
        }
    }
}
