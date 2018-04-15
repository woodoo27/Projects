using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication6
{
    public partial class Form1 : Form
    {
        Shop _shop;
        public Form1()
        {
            InitializeComponent();
            this._shop = new Shop();
            dataGridView1.DataSource = this._shop.Goods;
        }

        private void btnUpdate(object s,EventArgs e)
        { }
    }
}
