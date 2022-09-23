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
using Gst;
using Application = System.Windows.Forms.Application;
using Task = System.Threading.Tasks.Task;
using Guna.UI2.WinForms;

namespace SecondEgSA
{
    public partial class PlayBack : Form
    {
        public PlayBack()
        {
            InitializeComponent();
            Start.Show();
            stop.Hide();
        }
        #region delcare
        Model1.Model1 EgSa = new Model1.Model1();
        string X, Y, Z;
        int Get_Plan_ID;
        #endregion
        public void Get_result()
        {
            try
            {
            this.Invoke((MethodInvoker)delegate
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
               
            });

            var Get_Data_tele_with_control = EgSa.plan_result.Where(o => o.time_value == guna2TrackBar1.Value).Where(o => o.plan_id == Get_Plan_ID).FirstOrDefault();


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
                    while (true)
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
                        if (Get_Data_tele_with_control.value_result[i] != '-')
                        {
                            Z += Get_Data_tele_with_control.value_result[i].ToString();
                            i++;
                            if (i > Get_Data_tele_with_control.value_result.Length - 1) break;
                        }
                        else
                        {
                            i++;
                            break;
                        }
                    }
                    i = 0;
                }
                this.Invoke((MethodInvoker)delegate
                {
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
                                    guna2DataGridView2.Rows.Add(X, Y, Z, TimeSpan.FromSeconds((int)Get_Data_tele_with_control.time_value));
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
                });
            }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        Thread d;
        private void guna2Button4_Click(object sender, EventArgs e)
        {
            guna2DataGridView6.Rows.Clear();    

            d = new Thread(new ThreadStart(getData));
            d.Start();

        }
        public void getData()
        {
            Get_Plan_ID = int.Parse(guna2TextBox1.Text);
            this.Invoke((MethodInvoker)delegate
            {
                var Get_plan = EgSa.plan_.Where(o => o.plan_ID == Get_Plan_ID).ToList();
                foreach (var items in Get_plan)
                {
                    var GetCommandName = EgSa.Commands.Where(o => o.com_id == items.com_ID).FirstOrDefault();
                    var GetSubName = EgSa.Subsystems.Where(o => o.Sub_ID == items.sub_ID).FirstOrDefault();
                    guna2DataGridView6.Rows.Add(items.squn_command, GetSubName.Sub_name, GetCommandName.com_description, items.repeat, items.delay);

                }

                Thread.Sleep(1000);
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
                                    guna2DataGridView2.Rows.Add(X, Y, Z, TimeSpan.FromSeconds((int)item.time_value));
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
        int timeeee;
        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {

            Get_result();

        }
  
        private void timer1_Tick(object sender, EventArgs e)
        {
            

        }
        string msg = "";
        private void guna2Button7_Click(object sender, EventArgs e)
        {
            stop.Hide();
            Start.Show();
            msg = "stop";

        }
        Thread handleResult;
        private void guna2Button6_Click(object sender, EventArgs e)
        {

            stop.Show();
            Start.Hide();
            guna2DataGridView6.Rows.Clear();
            var Get_plan = EgSa.plan_.Where(o => o.plan_ID == Get_Plan_ID).ToList();
            foreach (var items in Get_plan)
            {
                var GetCommandName = EgSa.Commands.Where(o => o.com_id == items.com_ID).FirstOrDefault();
                var GetSubName = EgSa.Subsystems.Where(o => o.Sub_ID == items.sub_ID).FirstOrDefault();
                guna2DataGridView6.Rows.Add(items.squn_command, GetSubName.Sub_name, GetCommandName.com_description, items.repeat, items.delay);

            }
            handleResult = new Thread(new ThreadStart(SetValue));
            handleResult.Start();
            Thread.Sleep(1000);


        }


        async public void SetValue()
        {
            try
            {

            Get_Plan_ID = int.Parse(guna2TextBox1.Text);
            var Max_Time_of_plan = EgSa.plan_result.Where(o => o.plan_id == Get_Plan_ID).AsEnumerable().LastOrDefault();
            guna2TrackBar1.Maximum = (int)Max_Time_of_plan.time_value;
            guna2TrackBar1.Minimum = 0;
            for (int i = guna2TrackBar1.Value; i <= guna2TrackBar1.Maximum; i++)
            {

                guna2TrackBar1.Value = i;
                Get_result();
                await Task.Delay(1000);
                if (msg == "stop")
                {
                    break;
                }
            }
            msg = "";
            handleResult.Abort();
            }
            catch
            {
                MessageBox.Show("please enter plan id");
            }

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {

         
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            selectOption se = new selectOption();
            se.Show();
            this.Hide();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        async private void guna2Button1_Click(object sender, EventArgs e)
        {

    



        }

        

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
