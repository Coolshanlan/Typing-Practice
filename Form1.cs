using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace The_new_cool_game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<Button> bb = new List<Button>();
        Dictionary<string, Label> All_label = new Dictionary<string, Label>(); //Heart 愛心 Score 分數 Error_Count 錯誤計數 Star 星星
      //  Button Combo_bu;
        int width ;
        int height;
        int fast_i = 60;
        int fast = 0;
        int timer1_i = 1250;
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = timer1_i;
            fast = fast_i;
            stop = true;
            MessageBox.Show("Game Start");
            this.Size = new Size((SystemInformation.PrimaryMonitorSize.Width / 7) * 3, (SystemInformation.PrimaryMonitorSize.Height / 4) * 3);
            this.Location = new Point((SystemInformation.PrimaryMonitorSize.Width / 7) * 2, (SystemInformation.PrimaryMonitorSize.Height / 8));
            this.BackColor = Color.Black;
            width = this.Size.Width;
            height = this.Size.Height;
            All_label.Add("Star", new Label());
            All_label.Add("Heart", new Label());
            All_label.Add("Score", new Label());
            All_label.Add("Error_Count", new Label());
       
            All_label.Add("Combo", new Label());
           // Combo_bu = new Button();
            foreach (var k in All_label) {
                if(k.Key =="Score")
                k.Value.Font = new Font("Georgia", 24);
                else if(k.Key == "Heart")
                    k.Value.Font = new Font("微軟正黑體", 24);
                else if(k.Key == "Star")
                    k.Value.Font = new Font("微軟正黑體", 15);
                else
                    k.Value.Font = new Font("Georgia", 24);
                k.Value.AutoSize = true;
            }
            All_label["Heart"].ForeColor = Color.Red;
            All_label["Star"].ForeColor = Color.Yellow;
            All_label["Score"].ForeColor = Color.White;
            All_label["Error_Count"].ForeColor = Color.Orange;
            To_Set_Label_Dict();
            //All_label["Error_Count"].Location = new Point(width - All_label["Error_Count"].Width, 0);
            All_label["Error_Count"].Dock = DockStyle.Right;
            All_label["Star"].Dock = DockStyle.Bottom;
            All_label["Heart"].Dock = DockStyle.Bottom;
            //All_label["Heart"].Dock = DockStyle.Right;
            // All_label["Heart"].Top = All_label["Error_Count"].Height;
            //All_label["Star"].Location = new Point(0, height - All_label["Star"].Height);
           
            button1.TabStop = false;

            foreach (var k in All_label) Controls.Add(k.Value);
           // pictureBox1.Controls.Add(All_label["Combo"]);
            CheckForIllegalCrossThreadCalls = false;
            To_Set_Label_Dict();
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        Parallel.ForEach(bb, x =>
            //        {
            //            x.Location = new Point(x.Location.X, x.Location.Y + (stop == true?0:1));
            //        });
            //        Thread.Sleep(fast);
            //        for(int i =0; i<bb.Count; i++)
            //        {
            //             if (bb[i].Location.Y + bb[i].Size.Height >= this.Size.Height)
            //             {
            //                bb[i].Dispose();
            //                bb.Remove((Button)bb[i]);
            //                Heart--;
            //                Combo = 0;
            //                To_Set_Label_Dict();
            //             }
            //             else
            //             {
            //                 break;
            //             }

            //        }

            //        if (Heart <= 0 || Error_Count <= 0)
            //        {
            //            Error_Count = Error_Count<0?0: Error_Count;
            //            To_Set_Label_Dict();
            //            timer1.Enabled = false;
            //            gameover = true;
            //            MessageBox.Show("Game Over");
            //            return;
            //        }
            //        while (rem != 0) 
            //        {
            //            rem--;
            //            bb[0].Dispose();
            //            bb.RemoveAt(0);
            //            if(bb.Count>=1)
            //            bb[0].BackColor = Color.LightGreen;
            //        }

            //    }

            //});
            move.Interval = fast;
            move.Enabled = true;
            Thread.Sleep(100);
            stop = false;
        }
        Random rd = new Random();
        bool stop = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!stop)
            {
                bb.Add(new Button());
                bb[bb.Count - 1].TabStop = false;
                bb[bb.Count - 1].Size = new Size(50, 50);
                bb[bb.Count - 1].Tag = "b";
                bb[bb.Count - 1].BackColor = Color.LightPink;
                bb[bb.Count - 1].Location = new Point(rd.Next(0, this.Width - 50), 5);
                bb[bb.Count - 1].Font = new Font("Georgia", 25, FontStyle.Bold);
                bb[bb.Count - 1].Text = Convert.ToChar(rd.Next(65, 91)).ToString();
                if(bb.Count == 1) bb[bb.Count - 1].BackColor = Color.LightGreen;
                this.Controls.Add(bb[bb.Count - 1]);
            }

        }
        int rem = 0;
        bool gameover = false;
        bool stopgame = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           // MessageBox.Show(e.KeyValue.ToString());
            if (e.KeyCode == Keys.Escape) Application.Exit();
            else if (e.KeyCode == Keys.F5) Application.Restart();
            if (gameover) return;
            if (e.KeyCode == Keys.Space)
            {
                Star--;
                Task.Run(() =>
                {
                    stop = true;
                    Thread.Sleep(2000);
                    stop = false;
                });
            }
            else if(e.KeyValue.ToString() == "220")
            {
                stop = stop == true ? false : true;
                stopgame = stopgame == true ? false : true;
            }
            else if (stopgame) return;
            else if (bb.Count > 0 && bb[0].Text == ((char)e.KeyValue).ToString())
            {
                rem++;
                Combo++;
                if (Combo % 20 == 0) Star++;
                Score += Combo;
                if ((timer1_i - (Score)) > 250)
                    timer1.Interval = timer1_i - (Score);
                else timer1.Interval = 250;
                fast = fast_i - Score/10;
                if (fast < 30) fast = 30;
                move.Interval = fast;
                //if (Score > 1500) fast = 300;
                //if (bb.Count >= 25) fast = 200;

                GC.Collect();
            }
            else
            {
                Combo = 0;
                Error_Count--;
            }
            To_Set_Label_Dict();
            //stop = true;
            //timer1.Enabled = true;
        }
        int Heart=3, Score=0, Error_Count=10, Star=1,Combo=0;

        private void move_Tick(object sender, EventArgs e)
        {

                for(int  x =0;x<bb.Count;x++)
                    bb[x].Location = new Point(bb[x].Location.X, bb[x].Location.Y + (stop == true ? 0 : 3));
            //move.Interval = fast;
        
            for (int i = 0; i < bb.Count; i++)
            {
                if (bb[i].Location.Y + bb[i].Size.Height >= this.Size.Height)
                {
                    bb[i].Dispose();
                    bb.Remove((Button)bb[i]);
                    Heart--;
                    Combo = 0;
                    To_Set_Label_Dict();
                }
                else
                {
                    break;
                }

            }

            if (Heart <= 0 || Error_Count <= 0)
            {
                Error_Count = Error_Count < 0 ? 0 : Error_Count;
                To_Set_Label_Dict();
                timer1.Enabled = false;
                gameover = true;
                move.Enabled = false;
                MessageBox.Show("Game Over");
                return;
            }
            while (rem != 0)
            {
                rem--;
                bb[0].Dispose();
                bb.RemoveAt(0);
                if (bb.Count >= 1)
                    bb[0].BackColor = Color.LightGreen;
            }

        }

        void To_Set_Label_Dict()
        {
            try
            {
                All_label["Heart"].Text = "";
                for (int i = 0; i < Heart; i++)
                {
                    All_label["Heart"].Text += "❤";
                }
                All_label["Score"].Text = "Score:" + Score;
                All_label["Error_Count"].Text = "Error：" +(10- Error_Count);
                All_label["Star"].Text = "";
                for (int i = 0; i < Star; i++)
                {
                    All_label["Star"].Text += "★";
                }
                //All_label["Combo"].Text =Combo+" Combo";
                if (Combo == 0)
                {
                    button1.Hide();
                }
                else
                {
                    if (Combo % 10 == 0)
                        Task.Run(() =>
                        {
                            coolshandodo(1, Combo + 10);
                        });
                    else
                        Task.Run(() =>
                        {
                            coolshandodo(0, Combo + 20);
                        });
                    button1.Show();
                    button1.Text = Combo.ToString();
                }
                All_label["Score"].Location = new Point(width / 2 - All_label["Score"].Width / 2, 0);

            }
            catch
            {

            }
           // All_label["Combo"].Location = new Point(pictureBox1.Width / 2 - All_label["Combo"].Width / 2, pictureBox1.Height / 2 - All_label["Combo"].Height / 2);
        }
        bool dodomove = false;

        public void coolshandodo(int num,int dodo)
        {
            if (dodomove == true) return;
            else dodomove = true;
            if (dodo > 60) dodo = 60;
            if(num == 0)
            {
                int delay = 20;
                button1.Location = new Point(button1.Location.X- dodo, button1.Location.Y);
                Thread.Sleep(delay);
                button1.Location = new Point(button1.Location.X + dodo+dodo, button1.Location.Y);
                Thread.Sleep(delay);
                button1.Location = new Point(button1.Location.X - dodo, button1.Location.Y);
                Thread.Sleep(delay);
                //button1.Location = new Point(button1.Location.X - dodo, button1.Location.Y);
                //Thread.Sleep(delay);
                button1.Location = new Point(button1.Location.X , button1.Location.Y- dodo);
                Thread.Sleep(delay);
                button1.Location = new Point(button1.Location.X , button1.Location.Y + dodo + dodo);
                Thread.Sleep(delay);
                //button1.Location = new Point(button1.Location.X, button1.Location.Y + dodo);
                //Thread.Sleep(delay);
                button1.Location = new Point(button1.Location.X, button1.Location.Y - dodo);
                Thread.Sleep(delay);
            }
            else
            {
                int delay = 20;
                button1.Size = new Size(button1.Size.Width+80, button1.Size.Height+50);
                button1.Location = new Point(button1.Location.X - dodo, button1.Location.Y);
                Thread.Sleep(delay);
                button1.Location = new Point(button1.Location.X + dodo + dodo, button1.Location.Y);
                Thread.Sleep(delay);
                button1.Size = new Size(button1.Size.Width - 80, button1.Size.Height - 50);
                button1.Location = new Point(button1.Location.X - dodo, button1.Location.Y);
                Thread.Sleep(delay);
                //button1.Location = new Point(button1.Location.X - dodo, button1.Location.Y);
                //Thread.Sleep(delay);
                button1.Size = new Size(button1.Size.Width + 80, button1.Size.Height + 50);
                button1.Location = new Point(button1.Location.X, button1.Location.Y - dodo);
                Thread.Sleep(delay);
                button1.Location = new Point(button1.Location.X, button1.Location.Y + dodo + dodo);
                Thread.Sleep(delay);
                //button1.Location = new Point(button1.Location.X, button1.Location.Y + dodo);
                //Thread.Sleep(delay);
                button1.Location = new Point(button1.Location.X, button1.Location.Y - dodo);
                Thread.Sleep(delay);
                button1.Size = new Size(button1.Size.Width - 80, button1.Size.Height - 50);
            }
            dodomove = false;
        }
    }
}
