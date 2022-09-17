using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Threading.Tasks;


namespace SecondEgSA
{
    public partial class Form6 : Form
    {
        Thread th;
        public Form6()
        {
            InitializeComponent();
            
      
        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void Form6_Load(object sender, EventArgs e)
        {

            Splash();
        }


        private async void Splash()
        {
            
            await Task.Delay(6830); //wait for 5 seconds asynchronously 
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        
    }
}
