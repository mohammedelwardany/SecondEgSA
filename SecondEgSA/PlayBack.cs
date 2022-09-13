using SecondEgSA.Model1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecondEgSA.Model1;
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
        Model1.Model1 EgSa = new Model1.Model1();
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
                            switch (item.command_id)
                            {
                                case 0:
                                    guna2DataGridView1.Rows.Add(item.value_result, "", TimeSpan.FromSeconds((int)item.time_value));
                                    break;
                                case 1:
                                    guna2DataGridView1.Rows.Add("", item.value_result, TimeSpan.FromSeconds((int)item.time_value));
                                    break;

                            }
                            break;
                        case 1:
                            switch (item.command_id)
                            {
                                case 0:
                                    guna2DataGridView2.Rows.Add( TimeSpan.FromSeconds((int)item.time_value));
                                    break;
                                case 1:
                                    guna2DataGridView3.Rows.Add( TimeSpan.FromSeconds((int)item.time_value));
                                    break;
                                case 2:
                                    guna2DataGridView4.Rows.Add( TimeSpan.FromSeconds((int)item.time_value));
                                    break;
                            }
                            break;
                        case 2:
                            switch (item.command_id)
                            {
                                case 0:
                                    guna2DataGridView5.Rows.Add(item.value_result, TimeSpan.FromSeconds((int)item.time_value));
                                    break;

                            }
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
