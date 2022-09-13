using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecondEgSA.Model1;
//using SecondEgSA.Model;
namespace SecondEgSA
{
    public partial class Form3 : Form
    {
        #region Function
        static string Checklength_hex(int num)
        {
            string lol;
            if (num <= 15) return lol = "0" + Convert.ToString(num, 16);

            else return lol = Convert.ToString(num, 16);

        }

        static Boolean Checklength_temp(string text_1)
        {
            if (text_1.Length > 2)
            {
                MessageBox.Show("Enter number loweer than 2 ");
                return false;

            }


            else
            {
                return true;
            }
        }
        #endregion
        #region declare
        public string packet;
        int Suq = 0;
        string X_time;

        Model1.Model1 EgSA = new Model1.Model1();
        combob combobo1, combobo2;
        ComboBox Cb = new ComboBox();

        public SerialPort myport;
        #endregion
        public Form3()
        {
            InitializeComponent();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            combobo1 = (combob)guna2ComboBox1.SelectedItem;


            listBox1.Items.Clear();

            if (combobo1.id == 6)
            {
                var query_all = EgSA.Commands.ToList();
                foreach (var c in query_all)
                {
                    listBox1.Items.Add(new combob(c.com_id, c.com_description));
                }


            }
            else
            {
                var query = EgSA.Commands.Where(o => o.sub_ID == combobo1.id).ToList();
                foreach (var item in query)
                {
                    listBox1.Items.Add(new combob(item.com_id, item.com_description));
                }
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }
      //  public void 
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                combobo2 = (combob)listBox1.SelectedItem;
                //var g = comboBox1.SelectedIndex; // get index from combobox
                //g++;
                //z = combobo2.id;


                var H = EgSA.Commands.Where(o => o.com_id == combobo2.id).Where(o=>o.sub_ID == combobo1.id).FirstOrDefault(); // select first command when equal com_id  
                var m = EgSA.CoM_Param.Where(n => n.com_id == H.com_id).Where(o=>o.sub_Id == H.sub_ID).ToList();
                label5.Hide();
                label1.Hide();
                label6.Hide();
                guna2ComboBox3.Hide();

                guna2TextBox1.Hide();

                guna2DateTimePicker1.Hide();
                foreach (var n in m)
                {
                    var p = n.param_type;
                    switch (p)
                    {
                        case 1:
                            label1.Show();
                            guna2TextBox1.Show();
                            break;

                        case 2:
                            label6.Show();
                            guna2ComboBox3.Show();
                            break;

                        case 3:
                            label5.Show();
                            guna2DateTimePicker1.Show();
                            break;

                        default:
                            MessageBox.Show("No param");
                            break;
                    }
                    guna2ComboBox3.Items.Clear();
                    var Devices = EgSA.Param_Value.Where(o=>o.com_id == combobo2.id).Where(o=> o.sub_ID==combobo1.id).Select(o=>o.description).ToList();
                    foreach(var Device in Devices)
                    {
                        guna2ComboBox3.Items.Add(Device);
                    }




                }
            }
            catch
            {
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

            try
            {
                if(guna2DateTimePicker1.Visible == true)
                {
                    X_time = guna2DateTimePicker1.Value.ToString();
                }
                else
                {
                    X_time = "";
                }


                var H = EgSA.Commands.Where(o => o.com_id == combobo2.id).FirstOrDefault();


                var f = EgSA.CoM_Param.Where(o => o.com_id == H.com_id).FirstOrDefault();
                string get_delay, sub_name, com_name, get_repeat;
                var code = "GZ";// convert to hex to arduino 
                while (true)
                {
                    if (f != null)
                    {
                        if (Checklength_temp(guna2TextBox2.Text) == false || Checklength_temp(guna2TextBox3.Text) == false)
                            break;
                        //Checklength_temp(textBox2.Text);
                        //Checklength_temp(textBox3.Text);
                        Suq++;

                        guna2DataGridView1.Rows.Add(Suq, combobo1.name,
                            combobo2.name,
                            (guna2TextBox3.Text == "") ? guna2TextBox3.Text = "1" : guna2TextBox3.Text,
                           (guna2TextBox2.Text == "") ? guna2TextBox2.Text = "1" : guna2TextBox2.Text,
                            (guna2ComboBox3.SelectedItem == null) ? "" : guna2ComboBox3.SelectedItem.ToString(),
                            guna2TextBox1.Text,
                            X_time);


                        get_repeat = Checklength_hex(Convert.ToInt32(guna2TextBox2.Text));
                        get_delay = Checklength_hex(Convert.ToInt32(guna2TextBox3.Text));
                        //int Ind_2 = comboBox2.SelectedIndex + 1;
                        var load_Param_type = EgSA.param_TB_type.Where(o => o.param_ID == f.param_type).FirstOrDefault();


                        com_name = Checklength_hex(H.com_id);
                        sub_name = Checklength_hex(combobo1.id);



                        //if (Ind_2 <= 15) lol1 = "0" + Convert.ToString(Ind_2, 16); 
                        //else lol1 = Convert.ToString(Ind_2, 16);


                        // MessageBox.Show(code + "sd" + "sd" + com_name + sub_name + get_repeat + get_delay + code);
                        packet_class.packet += (code +  sub_name + com_name + get_repeat + get_delay + code);
                        break;
                    }
                    else
                    {

                        if (Checklength_temp(guna2TextBox2.Text) == false || Checklength_temp(guna2TextBox3.Text) == false)
                            break;
                        Suq++;

                        guna2DataGridView1.Rows.Add(Suq, combobo1.name,
                            combobo2.name,
                            (guna2TextBox3.Text == "") ? guna2TextBox3.Text = "1" : guna2TextBox3.Text,
                           (guna2TextBox2.Text == "") ? guna2TextBox2.Text = "1" : guna2TextBox2.Text,
                            (guna2ComboBox3.SelectedItem == null) ? "" : guna2ComboBox3.SelectedItem.ToString(),
                            guna2TextBox1.Text,
                            X_time);
                        //if (guna2TextBox2.Text)
                        //{
                        //    guna2TextBox2.Text = "1";
                        //}
                        //if (guna2TextBox3.Text == "")
                        //{
                        //    guna2TextBox3.Text = "1";
                        //}

                        get_repeat = Checklength_hex(Convert.ToInt32(guna2TextBox2.Text));
                        get_delay = Checklength_hex(Convert.ToInt32(guna2TextBox3.Text));

                        com_name = Checklength_hex(combobo2.id);
                        sub_name = Checklength_hex(combobo1.id);

                        //var anthor_serial_command = H.com_id;
                        if (combobo2.id <= 15 || combobo1.id <= 15) packet_class.packet += (code +   "0" + combobo1.id + "0" + combobo2.id + get_repeat + get_delay + code);
                        else packet_class.packet += (code +  sub_name + com_name + get_repeat + get_delay + code);

                        break;
                    }
                }
                packet_class.Number_Packets = "GZ"+Checklength_hex(Suq)+"GZ";
               // MessageBox.Show(packet_class.Number_Packets);
                guna2TextBox1.Clear();
                guna2TextBox2.Clear();
                guna2TextBox3.Clear();
                guna2ComboBox3.Items.Clear();

            }

            catch
            {
                //    MessageBox.Show("please select Commend " , "ERROR");
            }

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            var Get_last_planID = EgSA.plan_.AsEnumerable().LastOrDefault().plan_ID;
            
            int last_ID = Get_last_planID + 1;
            //var Get_Command = 
            //var Get_sub = EgSA.Subsystems.to




            for (int i = 0; i < guna2DataGridView1.Rows.Count; i++)
            {
                var Com_id = guna2DataGridView1.Rows[i].Cells["Column3"].Value.ToString();
                var Sub_name = guna2DataGridView1.Rows[i].Cells["Column2"].Value.ToString();
                Model1.plan_  p = new Model1.plan_
                {
                    com_ID = EgSA.Commands.Where(o => o.com_description == Com_id).FirstOrDefault().com_id,

                    sub_ID = EgSA.Subsystems.Where(o => o.Sub_name == Sub_name).FirstOrDefault().Sub_ID,
                    para1_desc = guna2DataGridView1.Rows[i].Cells["Column6"].Value.ToString(),
                    para2_desc = Convert.ToString(guna2DataGridView1.Rows[i].Cells["Column7"].Value),
                    para3_desc = guna2DataGridView1.Rows[i].Cells["Column8"].Value.ToString(),
                    squn_command = i + 1,
                    plan_ID = last_ID
                ,
                    delay = guna2DataGridView1.Rows[i].Cells["Column4"].Value.ToString(),
                    EX_time = guna2DateTimePicker1.Value.ToString(),
                    repeat = guna2DataGridView1.Rows[i].Cells["Column5"].Value.ToString()
                };

                EgSA.plan_.Add(p);
                EgSA.SaveChanges();

            }


            guna2ComboBox3.Hide();
            guna2DateTimePicker1.Hide();
            guna2TextBox1.Hide();

            
            MessageBox.Show(packet_class.packet);
            //myport = new SerialPort();
            //myport.BaudRate = 9600;
            //myport.PortName = "COM3";
            //myport.Open();
            //myport.WriteLine(packet);
            //myport.Close();
            //packet = "";
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            MessageBox.Show(packet_class.Number_Packets+packet_class.packet);
            Form2 form2 = new Form2();
            form2.Show();
            
            this.Hide();
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
        public void loader()
        {
            try
            {
                label5.Hide();
                label1.Hide();
                label6.Hide();
                guna2ComboBox3.Hide(); // hide combobox
                guna2TextBox1.Hide(); // hide textBox
                guna2DateTimePicker1.Hide(); // ......

                var G = EgSA.Commands.ToList(); // select all in table and put in list

                //foreach (var o in G)  // add all com_type in table 
                //{
                //    comboBox1.Items.Add(o.com_description);
                //}

                var get_devices = EgSA.Subsystems.ToList();
                // select all in table and put in list
                foreach (var f in get_devices)
                {

                    guna2ComboBox1.Invoke((MethodInvoker)delegate
                    {
                        //comboBox2.Items.Add(f.Sub_name);// add all subsystem
                        //comboBox2.Items.Insert(f.Sub_ID,f.Sub_name);
                        guna2ComboBox1.Items.Add(new combob(f.Sub_ID, f.Sub_name));
                    });

                }

                //var get_devices_Param = EgSA.Param_Value.Select(c => c.description).ToList();

                //foreach (var de in get_devices_Param)
                //{
                //    guna2ComboBox3.Items.Add(de);

                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("close?","error");
                loadar.Abort();
            }

        }
        Thread loadar;
        private void Form3_Load(object sender, EventArgs e)
        {

            


            loader();
          
            

        }
    }
}
