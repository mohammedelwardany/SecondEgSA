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
using SecondEgSA.Model1;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using RecorderVideo;
using Accord;
using Point = System.Drawing.Point;

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

        string Value_ = "";
        int langht = 0, sub = 0, comd = 0, time = 0; int segmentation_value = 0; int X = 0, Y = 0, Z = 0;
        TimeSpan ti;
        int Squ_increase_id = 0;
        int progress = 0;   
        List<plan_> plan = new List<plan_>();
        int? ack_check;
        

        public string packets;
        Model1.Model1 EgSa = new Model1.Model1();



        
        void value(string string_)
        {
            //var string_ = "GZ100101ff0253GZ";

            sub = hex_convert(string_[4]) + hex_convert(string_[5]);
            comd = hex_convert(string_[6]) + hex_convert(string_[7]);
            //var k = hex_convert(string_[string_.Length - 4]);
            time = hex_convert(string_[string_.Length - 4]) + hex_convert((string_[string_.Length - 5]))*16 + hex_convert((string_[string_.Length - 6])) *16*16+ hex_convert((string_[string_.Length - 7])) * 16 * 16 * 16;
            int segmen = 0;
            ti = TimeSpan.FromSeconds(time);
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
                    Value_ = segmen.ToString();


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
                    Value_ = X.ToString()+","+Y.ToString()+","+Z.ToString();
                }
            }
            
            


        }
        public static SerialPort myport;

        public Form2()
        {
            InitializeComponent();
            //Form3 form3 = new Form3();

            Loader = new Thread(new ThreadStart(LOADEE));
            Loader.Start();
            guna2PictureBox2.Hide();
            guna2ProgressBar1.Hide();
            startRecording.Show();
            endRecording.Hide();


        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }




        Thread Loader;
        async private void Form2_Load(object sender, EventArgs e)
        {
            //List<string> numbers = new List<string> {"1" ,"3" ,"4","5","6","7","7","7" };
            //for(int i =0; i<numbers.Count;i++)
            //{
            //    listView2.Items.Add(numbers[i])

            //}


        }


        public void LOADEE()
        {
            try
            {
                    var Get_plan = EgSa.plan_.Where(o => o.plan_ID == packet_class.Plan_id).ToList();
                    foreach (var items in Get_plan)
                    {
                        var Get_command = EgSa.Commands.Where(o => o.sub_ID == items.sub_ID).Where(o => o.com_id == items.com_ID).FirstOrDefault().com_description;
                        var GetSubname = EgSa.Subsystems.Where(o => o.Sub_ID == items.sub_ID).FirstOrDefault();
                         this.Invoke((MethodInvoker)delegate
                {
                        guna2DataGridView6.Rows.Add(items.squn_command, GetSubname.Sub_name, Get_command, items.repeat, items.delay, null);
                    if (Get_command == "OnGoningSession")
                    {
                        guna2DataGridView6.Rows.Clear();
                        listBox1.Items.Add(">> " + "No Plan To be excuted");
                        listBox1.ForeColor = Color.Red;
                        
                    } ;
                        label16.Text = packet_class.Plan_id.ToString();

                });       
                        progress += int.Parse(items.repeat) + int.Parse(items.delay);

                    }
                

                    progress = progress * 2;
                    Loader.Abort();
            }
            catch (Exception ex)    
            {
                Loader.Abort();
            }
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
        string mes = "";
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

                //myport.Handshake = Handshake.None;
            
                myport.Open();

                myport.WriteLine(packet_class.Number_Packets + packet_class.packet);
                packet_class.packet = "";
                packet_class.Number_Packets = "";
                packet_class.Plan_id = 0;

                SetPlan();
                timer1.Start();
                while (a != "gz010000gz\r")
                {
                    guna2ProgressBar1.Maximum = progress;
                    guna2ProgressBar1.Value = guna2ProgressBar1.Value + 1;





                    a = myport.ReadLine();
                    segmentation(a);
                    if (segmentation_value == 0)
                    {
                        myport.Close();
                        break;
                    }
                    SetPlan();
                    SetAck(plan);



                    listBox1.Invoke((MethodInvoker)delegate
                    {
                        listBox1.ForeColor = Color.White;

                        // var get_sensor_name = EgSa.Commands.Where(o => o.com_id == comd).FirstOrDefault().sensor_name;
                        // Running on the UI thread
                        listBox1.Items.Add(">> " + a);
                        if (segmentation(a) != "ZZZZ")
                        {
                            switch (sub)
                            {
                                case 0:
                                    switch (comd)
                                    {
                                        case 0:
                                            guna2DataGridView1.Rows.Add(segmentation(a).ToString(), "", ti.ToString());
                                            break;
                                        case 1:
                                            guna2DataGridView1.Rows.Add("", segmentation(a).ToString(), ti.ToString());
                                            break;

                                    }
                                    break;
                                case 1:
                                    switch (comd)
                                    {
                                        case 0:
                                            guna2DataGridView2.Rows.Add(X, Y, Z, ti.ToString());
                                            break;
                                        case 1:
                                            guna2DataGridView3.Rows.Add(X, Y, Z, ti.ToString());
                                            break;
                                        case 2:
                                            guna2DataGridView4.Rows.Add(X, ti.ToString());
                                            break;
                                    }
                                    break;
                                case 2:
                                    switch (comd)
                                    {
                                        case 0:
                                            guna2DataGridView5.Rows.Add(segmentation(a).ToString(), ti.ToString());
                                            break;

                                    }
                                    break;
                            }
                            var last_reslut_id = EgSa.plan_result.AsEnumerable().LastOrDefault().plan_result_id;
                            plan_result plan_Result = new plan_result
                            {
                                plan_result_id = last_reslut_id + 1,
                                plan_id = EgSa.plan_.AsEnumerable().LastOrDefault().plan_ID,
                                command_id = comd,
                                sub_id = sub,
                                time_value = time,
                                sequance_id = Squ_increase_id,
                                value_result = segmentation(a).ToString()

                            };
                            label16.Text=plan_Result.plan_result_id.ToString(); 


                            EgSa.plan_result.Add(plan_Result);
                            EgSa.SaveChanges();
                        }

                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        listBox1.SelectedIndex = -1;

                    });

                }
                if (segmentation_value == 0)
                {
                    myport.Close();
                    timer1.Stop();
                    
                    t.Abort();
                }
                else if (segmentation_value == 1)
                {
                    myport.Close();
                    mes = "success";
                    t.Abort();

                }
                else
                {
                    myport.Close();
                    timer1.Stop();
                    t.Abort();
                }
            }
            catch (Exception ex)
            {
                myport.Close();


                if (mes == "success")
                {
                    try
                    {
                        listBox1.Invoke((MethodInvoker)delegate
                        {
                            listBox1.ForeColor = Color.LightGreen;
                            listBox1.Items.Add(">> " + ex.Message);
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                            listBox1.SelectedIndex = -1;
                            guna2ProgressBar1.ProgressColor = Color.LightGreen;
                            guna2ProgressBar1.ProgressColor2 = Color.LightGreen;
                        }
                        );
                        myport.Close();
                        t.Abort();
                    }
                    catch
                    {
                        myport.Close();
                        timer1.Stop();

                        t.Abort();
                    }
                }
                else
                {

                    try
                    {
                        listBox1.Invoke((MethodInvoker)delegate
                        {
                            listBox1.ForeColor = Color.Red;
                            listBox1.Items.Add(">> " + ex.Message);
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                            listBox1.SelectedIndex = -1;
                            guna2ProgressBar1.ProgressColor = Color.Red;
                            guna2ProgressBar1.ProgressColor2 = Color.Red;
                        }
                        );
                        myport.Close();
                        timer1.Stop();

                        t.Abort();
                    }
                    catch
                    {
                        myport.Close();
                        timer1.Stop();

                        t.Abort();
                    }
                }




            }

            string segmentation(string g)
            {


                if (g[2] == '0' && g[3] == '0' && g[4] == '0' && g[5] == '2')
                {
                    //Console.Write("command understood");
                    segmentation_value = 1;

                    Squ_increase_id = hex_convert(g[6]) * 16 + hex_convert(g[7]);

                }
                else if (g[2] == '0' && g[3] == '0' && g[4] == '0' && g[5] == '0')
                {
                    //Console.Write("command executed correctly");
                    segmentation_value = 1;
                    Squ_increase_id = hex_convert(g[6]) * 16 + hex_convert(g[7]);

                }
                else if (g[2] == '0' && g[3] == '1' && g[4] == '0' && g[5] == '0')
                {
                    //Console.Write("the all plan is executed succesfully");
                    segmentation_value = 1;
                }
                else if (g[2] == '0' && g[3] == '0' && g[4] == '0' && g[5] == '1')
                {
                    //Console.Write("error in command");
                    segmentation_value = 0;
                    Squ_increase_id = hex_convert(g[6]) * 16 + hex_convert(g[7]);

                }
                else if (g[2] == '0' && g[3] == '1' && g[4] == '0' && g[5] == '1')
                {
                    //Console.Write("error in command");
                    segmentation_value = 0;

                }
                else
                {
                    value(g);
                    return Value_;
                }
                return "ZZZZ";
            }
            SetPlan();
            SetAck(plan);

        }

        string outputPath = "D:/RVedio";
        string finalVideName = DateTime.Now.ToString("yyyy’-‘MM’-‘dd’T’HH’:’mm’:’ss.fffffffK");
        ScreenRecorder screenRec = new ScreenRecorder(new Rectangle(), "");

        public void Record()
        {
            outputPath = "./recorded";
            Rectangle bounds;
            bounds = Screen.FromControl(this).WorkingArea;
            //Rectangle bounds = new Rectangle(Location=this.Location,Size=this.Size);
            //bounds = Screen.FromRectangle(new Rectangle(this.Location,(Width=this.Size.Width)));

            Size size = new Size(1526, 818);
            bounds.Location = new Point(this.Location.X + 80, this.Location.Y + 65);
            bounds.Size = size;

            //Screen bounds = ScreenCapture


            screenRec = new ScreenRecorder(bounds, outputPath);


        }
        private void tmrRecorder_Tick(object sender, EventArgs e)
        {
            screenRec.RecordAudio();
            screenRec.RecordVideo();
            screenRec.GetElapsed();
        }


        //private void button1_Click(object sender, EventArgs e)
        //{

        //    if (FolderSelected)
        //    {
        //        tmrRecorder.Start();
        //    }
        //    else
        //    {
        //        MessageBox.Show("You must select output folder before recorder, Error ");
        //    }
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    tmrRecorder.Stop();
        //    screenRec.stop();
        //    Application.Restart();
        //}



        private void SetPlan()
        {
            int lastId = EgSa.plan_.AsEnumerable().LastOrDefault().plan_ID;
            var selected = EgSa.plan_.OrderByDescending(p => p.plan_ID).Where(p => p.plan_ID == lastId).ToList();
            plan = selected;
            

        }
        string mes_ack="";
        private void SetAck(List<plan_> plan)
        {

            var command = plan.FirstOrDefault(p => p.squn_command == Squ_increase_id);

            var DbCommand = EgSa.plan_.FirstOrDefault(p => p.plan_ID == command.plan_ID && p.squn_command == command.squn_command);
            DbCommand.ack_id = segmentation_value;
            ack_check = DbCommand.ack_id;

            foreach (DataGridViewRow row in guna2DataGridView6.Rows)
                if (ack_check == 1)
                {
                    row.DefaultCellStyle.BackColor = Color.GreenYellow;
                }
                else if (ack_check == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }


            //if (ack_check == 1)
            //{
            //    mes_ack = "ACK";
            //}
            //else
            //{
            //    mes_ack = "NONACK";
            //}
            plan.Remove(command);

            EgSa.SaveChanges();
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

        public void proccessProgress()
        {
            //for guna2ProgressBar1
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

        private void guna2ProgressBar1_ValueChanged(object sender, EventArgs e)
        {

        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region progersse

            
            #endregion
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void guna2DataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void endRecording_Click(object sender, EventArgs e)
        {
            startRecording.Show();
            endRecording.Hide();

            MessageBox.Show("Test");
            tmrRecorder.Stop();
            screenRec.stop();
            guna2PictureBox2.Hide();
        }

        private void startRecording_Click(object sender, EventArgs e)
        {
            startRecording.Hide();
            endRecording.Show();

            tmrRecorder.Start();
            guna2PictureBox2.Show();
        }

        private void tmrRecorder_Tick_1(object sender, EventArgs e)
        {
            screenRec.RecordAudio();
            screenRec.RecordVideo();
            label1.Text = screenRec.GetElapsed();
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
