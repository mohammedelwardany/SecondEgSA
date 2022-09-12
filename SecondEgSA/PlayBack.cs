using SecondEgSA.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecondEgSA.Model;
using System.Threading;

namespace SecondEgSA
{
    public partial class PlayBack : Form
    {
        public PlayBack()
        {
            InitializeComponent();
        }
        #region delcare
        EgSa EgSa = new EgSa();
        #endregion

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        Thread d;
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            d = new Thread(new ThreadStart(getData));
            d.Start();
        }
        public void getData()
        {
            listBox1.Invoke((MethodInvoker)delegate
            {
            int planID = Int16.Parse(guna2TextBox1.Text);
            var result = EgSa.plan_result.Where(o => o.plan_id == planID).ToList();
            foreach (var item in result)
            {
                switch (item.sub_id)
                {
                    case 0:
                            guna2DataGridView4.Rows.Add(item.value_result, item.time_value);
                        break;
                    case 1:
                        
                        break;
                }
            }
            });
            d.Abort();

        }

        private void guna2DataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
