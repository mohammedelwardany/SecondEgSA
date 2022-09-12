using SecondEgSA.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SecondEgSA.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SecondEgSA
{
    public partial class Form2 : Form
    {
        static int hex_convert(char Ch)
        {
            if (Char.IsDigit(Ch)) return Ch - 48;
            else if (char.IsLower(Ch)) return Ch - 87;
            else return Ch - 55;
        }

        int Value_ = 0;
        int langht = 0, sub = 0, comd = 0, time = 0; int segmentation_value = 0; int X = 0, Y = 0, Z = 0;

        public string packets;
        EgSa EgSa = new EgSa();

        void value(string string_)
        {
            //var string_ = "GZ100101ff0253GZ";

            sub = hex_convert(string_[4]) + hex_convert(string_[5]);
            comd = hex_convert(string_[6]) + hex_convert(string_[7]);
            time = hex_convert(string_[string_.Length - 3]) + hex_convert((string_[string_.Length - 4])) + hex_convert((string_[string_.Length - 5])) + hex_convert((string_[string_.Length - 6]));
            int segmen = 0;
            if (string_[0] == 'g' && string_[1] == 'z')
            {
                if (hex_convert(string_[2]) == '0') langht = hex_convert(string_[2]) + hex_convert(string_[3]);
                else langht = hex_convert(string_[2]) * 16 + hex_convert(string_[3]);
                if (langht < 20)
                {

                    double i = langht - 15;
                    for (int poniter = 8; poniter <= langht; poniter++)
                    {

                        segmen += hex_convert(string_[poniter]) * (int)(Math.Pow(16, i));
                        i--;

                        if (poniter == langht - 6) break;
                    }


                }
                else
                {

                    X = hex_convert(string_[9]) * 16 + hex_convert(string_[10]);
                    Y = hex_convert(string_[12]) * 16 + hex_convert(string_[13]);
                    Z = hex_convert(string_[15]) * 16 + hex_convert(string_[16]);
                    if (hex_convert(string_[8]) == 0 || hex_convert(string_[11]) == 0 || hex_convert(string_[14]) == 0)
                    {
                        X *= -1;
                        Y *= -1;
                        Z *= -1;
                    }

                }
            }
            
            Value_ = segmen;


        }
        public static SerialPort myport;

        public Form2()
        {
            InitializeComponent();
            //Form3 form3 = new Form3();





        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            //List<string> numbers = new List<string> {"1" ,"3" ,"4","5","6","7","7","7" };
            //for(int i =0; i<numbers.Count;i++)
            //{
            //    listView2.Items.Add(numbers[i])

            //}


        }
        private void btnExitProgram_Click(object sender, EventArgs e)
        {


            Application.Exit();
            //t.Abort();


        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        Thread t;
        public void guna2Button1_Click(object sender, EventArgs e)
        {

            t = new Thread(new ThreadStart(ArduinoRead));
            //t.IsBackground = true;
            //try
            //{
            t.Start();
            //    if (a=="gz0100gz")
            //    {
            //        t.Abort();
            //        MessageBox.Show("aborted");
            //    }
            //}
            //catch
            //{
            //    t.Abort();
            //    MessageBox.Show("aborted");
            //}
        }
        string a = "";
        public void ArduinoRead()
        {
            try
            {

                myport = new SerialPort();
                myport.BaudRate = 19200;
                myport.PortName = "COM3";
                myport.Handshake = Handshake.None;
                //myport.ReadTimeout = 500000;
                myport.Open();
                
               // myport.WriteLine(packet_class.Number_Packets+"GZ02000502GZ");
                myport.WriteLine("GZ01000502GZGZ01000502GZ");
                while (a != "gz0100gz\r")
                {


                    a = myport.ReadLine();
                    segmentation(a);

                    if (segmentation_value == 5)
                    {
                        myport.Close();
                        break;
                    }

                    listBox1.Invoke((MethodInvoker)delegate
                    {
                        listBox1.ForeColor = Color.White;

                       // var get_sensor_name = EgSa.Commands.Where(o => o.com_id == comd).FirstOrDefault().sensor_name;
                        // Running on the UI thread
                        listBox1.Items.Add(">> " + a);
                        if (segmentation(a) != -9999)
                        {
                            if (((comd == 0) && (sub == 1)) || ((comd == 1) && (sub == 1)) || ((comd == 2) && (sub == 1)))
                            {
                                switch (comd)
                                {
                                    case 0:
                                        guna2DataGridView2.Rows.Add(X, Y, Z);
                                        break;
                                    case 1:
                                        guna2DataGridView3.Rows.Add(X, Y, Z);
                                        break;
                                    case 2:
                                        guna2DataGridView4.Rows.Add(X);
                                        break;


                                }
                            }
                            else if((comd == 0) &&(sub==2))
                            {
                                guna2DataGridView5.Rows.Add(segmentation(a).ToString());

                            }
                            else
                            {
                                // if(segmentation_value == 1) guna2ShadowPanel10.FillColor = Color.Green;
                                //listView2.Items.Add(get_sensor_name + "       " + segmentation(a).ToString() + "        " + time);
                            }
                            //var last_reslut_id = EgSa.plan_result.AsEnumerable().LastOrDefault().plan_result_id;
                            //int i = 1;
                            //plan_result plan_Result = new plan_result
                            //{
                            //    plan_result_id = last_reslut_id + 1,
                            //    plan_id = EgSa.plan_.AsEnumerable().LastOrDefault().plan_ID,
                            //    command_id = comd,
                            //    sub_id = sub,
                            //    time_value = time,
                            //    sequance_id = i,
                            //    value_result = segmentation(a).ToString()
                            //};
                            //i++;
                            //EgSa.plan_result.Add(plan_Result);
                            //EgSa.SaveChanges();
                        }


                        //listBox1.Items.Add(">> " + a);
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        listBox1.SelectedIndex = -1;

                    });
                    //Thread.Sleep(200);
                    //MessageBox.Show(a);
                    //if (a == "gz0100gz")
                    //{
                    //    MessageBox.Show("ended");
                    //    break;
                    //}

                }
                myport.Close();
                t.Abort();
            }
            catch (Exception ex)
            {
                myport.Close();



                try
                {

                    listBox1.Invoke((MethodInvoker)delegate
                    {
                        listBox1.ForeColor = Color.Red;
                        listBox1.Items.Add(">> " + ex.Message);
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        listBox1.SelectedIndex = -1;
                    }
                    );
                    myport.Close();
                    t.Abort();
                }
                catch
                {
                    myport.Close();
                    t.Abort();
                }


                //myport.Close();
                //Application.ExitThread();   
                //MessageBox.Show("Error: " + ex.ToString(), "ERROR");


                // t.Abort();

            }


            int segmentation(string g)
            {

                //var g = "GZ0002GZ";
                if (g[2] == '0' && g[3] == '0' && g[4] == '0' && g[5] == '2')
                {
                    //Console.Write("command understood");
                    segmentation_value = 1;

                }
                else if (g[2] == '0' && g[3] == '0' && g[4] == '0' && g[5] == '0')
                {
                    //Console.Write("command executed correctly");
                    segmentation_value = 2;

                }
                else if (g[2] == '0' && g[3] == '1' && g[4] == '0' && g[5] == '0')
                {
                    //Console.Write("the all plan is executed succesfully");
                    segmentation_value = 3;
                }
                else if (g[2] == '0' && g[3] == '0' && g[4] == '0' && g[5] == '1')
                {
                    //Console.Write("error in command");
                    segmentation_value = 4;

                }
                else if (g[2] == '0' && g[3] == '1' && g[4] == '0' && g[5] == '1')
                {
                    //Console.Write("error in command");
                    segmentation_value = 5;

                }
                else
                {
                    value(g);
                    return Value_;
                }
                return -9999;
            }
        }


        private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Shapes1_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            t.Abort();
        }

        private void guna2ControlBox1_Click_1(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void guna2DataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
        }
    }
}
