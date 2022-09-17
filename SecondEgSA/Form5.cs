using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecondEgSA
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            Splash();
            
        }
        private async void Splash()
        {

            await Task.Delay(8000); //wait for 5 seconds asynchronously 
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
