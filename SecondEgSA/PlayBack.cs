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
        string X, Y, Z;
        int Get_Plan_ID ;
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
            Get_Plan_ID = int.Parse(guna2TextBox1.Text);
            listBox1.Invoke((MethodInvoker)delegate
            {
            
            var result = EgSa.plan_result.Where(o => o.plan_id == Get_Plan_ID).ToList();
                foreach (var item in result)
                {
                    X = "";
                    Y = "";
                    Z = "";
                        if (item.value_result.Length > 2)
                        {
                            int i = 0;
                            while (true)
                            {
                                if (item.value_result[i] != ',')
                                {
                                    X += item.value_result[i].ToString();
                                    i++;
                                }
                                else
                                {
                                    i++;
                                    break;
                                }
                            }
                            while (true)
                            {
                                if (item.value_result[i] != ',')
                                {
                                    Y += item.value_result[i].ToString();
                                    i++;
                                }
                                else
                                {
                                    i++;
                                    break;
                                }
                            }
                            while (true)
                            {
                                if (item.value_result[i] != '-')
                                {
                                    Z += item.value_result[i].ToString();
                                    i++;
                                    if (i > item.value_result.Length - 1) break;
                                }
                                else
                                {
                                    i++;
                                    break;
                                }
                            }
                            i = 0;
                        }
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
                                        guna2DataGridView2.Rows.Add(X,Y,Z,TimeSpan.FromSeconds((int)item.time_value));
                                        break;
                                    case 1:
                                        guna2DataGridView3.Rows.Add(X, Y, Z, TimeSpan.FromSeconds((int)item.time_value));
                                        break;
                                    case 2:
                                        guna2DataGridView4.Rows.Add(X, TimeSpan.FromSeconds((int)item.time_value));
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

        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            guna2DataGridView5.Rows.Clear();
            guna2DataGridView2.Rows.Clear();
            guna2DataGridView1.Rows.Clear();
            guna2DataGridView3.Rows.Clear();
            guna2DataGridView4.Rows.Clear();
            X = "";
            Y = "";
            Z = "";
            label15.Text = guna2TrackBar1.Value.ToString();
            var Get_Data_tele_with_control = EgSa.plan_result.Where(o => o.time_value == guna2TrackBar1.Value).Where(o=>o.plan_id == Get_Plan_ID).FirstOrDefault();

            //var Get_Timer_of_Value =EgSa.plan_result.Where(o=>o.time_value==guna2TrackBar1.Value).FirstOrDefault();
            //var Get_last_time = EgSa.plan_result.Where(o => o.time_value == guna2TrackBar1.Value).FirstOrDefault().time_value;
            //for (int i = 0; i <= Get_last_time; i++)
            //{

            //}
            
            if (Get_Data_tele_with_control != null)
            {
                if (Get_Data_tele_with_control.value_result.Length > 2)
                {
                    int i = 0;
                    while(true)
                    {
                        if (Get_Data_tele_with_control.value_result[i] != ',')
                        {
                            X += Get_Data_tele_with_control.value_result[i].ToString();
                            i++;
                        }
                        else
                        {
                            i++;
                            break;
                        }
                    }
                    while (true)
                    {
                        if (Get_Data_tele_with_control.value_result[i] != ',')
                        {
                            Y += Get_Data_tele_with_control.value_result[i].ToString();
                            i++;
                        }
                        else
                        {
                            i++;
                            break;
                        }
                    }
                    while (true)
                    {
                        if (Get_Data_tele_with_control.value_result[i] != '-' )
                        {
                            Z += Get_Data_tele_with_control.value_result[i].ToString();
                            i++;
                            if(i > Get_Data_tele_with_control.value_result.Length -1 ) break;
                        }
                        else
                        {
                            i++;
                            break;
                        }
                    }
                    i = 0;
                }
                switch (Get_Data_tele_with_control.sub_id)
                {
                    case 0:
                        switch (Get_Data_tele_with_control.command_id)
                        {
                            case 0:
                                guna2DataGridView1.Rows.Add(Get_Data_tele_with_control.value_result, "", TimeSpan.FromSeconds((int)Get_Data_tele_with_control.time_value));
                                break;
                            case 1:
                                guna2DataGridView1.Rows.Add("", Get_Data_tele_with_control.value_result, TimeSpan.FromSeconds((int)Get_Data_tele_with_control.time_value));
                                break;

                        }
                        break;
                    case 1:
                        switch (Get_Data_tele_with_control.command_id)
                        {
                            case 0:
                                guna2DataGridView2.Rows.Add(X,Y,Z,TimeSpan.FromSeconds((int)Get_Data_tele_with_control.time_value));
                                break;
                            case 1:
                                guna2DataGridView3.Rows.Add(X, Y, Z, TimeSpan.FromSeconds((int)Get_Data_tele_with_control.time_value));
                                break;
                            case 2:
                                guna2DataGridView4.Rows.Add(X, Y, Z, TimeSpan.FromSeconds((int)Get_Data_tele_with_control.time_value));
                                break;
                        }
                        break;
                    case 2:
                        switch (Get_Data_tele_with_control.command_id)
                        {
                            case 0:
                                guna2DataGridView5.Rows.Add(Get_Data_tele_with_control.value_result, TimeSpan.FromSeconds((int)Get_Data_tele_with_control.time_value));
                                break;

                        }
                        break;
                }
            }
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (guna2TrackBar1.Value >= count)
            //{
            //    guna2TrackBar1.Value = count;
            //}
            //else
            //{
            //    guna2TrackBar1.Value = guna2TrackBar1.Value + 1;
            //}
            //var last_Plan_id = EgSa.plan_.AsEnumerable().LastOrDefault().plan_ID;
            //var time_value_result = EgSa.plan_result.AsEnumerable().Where(o => o.plan_id == last_Plan_id).LastOrDefault().time_value;
            //count = (int)time_value_result;
            //guna2TrackBar1.Maximum = count;
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
             Get_Plan_ID = int.Parse(guna2TextBox1.Text);
            var Max_Time_of_plan = EgSa.plan_result.Where(o => o.plan_id == Get_Plan_ID).AsEnumerable().LastOrDefault();
            guna2TrackBar1.Maximum = (int)Max_Time_of_plan.time_value;

        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
          
            //timer1.Start();
            //guna2TrackBar1.Maximum = count ;
        }
        int count = 0;

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
         
            //timer1.Start();
            


        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
