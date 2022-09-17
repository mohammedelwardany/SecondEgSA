using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecondEgSA
{
    public partial class selectOption : Form
    {
        public selectOption()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Form6 form3 = new Form6();
            form3.Show();
            this.Close();
        }

        private void selectOption_Load(object sender, EventArgs e)
        {

        }

        private void roundButton1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form7 pk =new Form7();
            pk.Show();
            this.Hide();    
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
