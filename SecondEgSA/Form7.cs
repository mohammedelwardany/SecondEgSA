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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
            
        }


        string clicked = "";
        private async void Splash()
        {
           
                await Task.Delay(7500);
                PlayBack pk = new PlayBack();
                pk.Show();
                this.Hide();
         
        }
        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {
            clicked = "clk";
            PlayBack pk = new PlayBack();
            pk.Show();
            this.Hide();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            //  if(guna2PictureBox3_Click)
            

                Splash();
            

        }
    }
}
