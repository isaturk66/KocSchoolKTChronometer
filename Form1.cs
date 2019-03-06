using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace YatiliKronometre

{


    
    public partial class Form1 : Form
    {


        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);

        int sn =00,mm=00,hr = 00;
        string snfm, mmfm, hrfm;
        int ms;
        bool runnnin = false,focused = false;
        public Form1()
        {
            InitializeComponent();


            int FirstHotkeyId = 1;
            // Set the Hotkey triggerer the F9 key 
            // Expected an integer value for F9: 0x78, but you can convert the Keys.KEY to its int value
            // See: https://msdn.microsoft.com/en-us/library/windows/desktop/dd375731(v=vs.85).aspx
            int FirstHotKeyKey = (int)Keys.K;
            // Register the "F9" hotkey
            Boolean F9Registered = RegisterHotKey(
                this.Handle, FirstHotkeyId, 0x0000, FirstHotKeyKey
            );

            // Repeat the same process but with F10
            int SecondHotkeyId = 2;
            int SecondHotKeyKey = (int)Keys.L;
            Boolean F10Registered = RegisterHotKey(
                this.Handle, SecondHotkeyId, 0x0000, SecondHotKeyKey
            );

            // 4. Verify if both hotkeys were succesfully registered, if not, show message in the console
            if (!F9Registered)
            {

                Console.WriteLine("Global Hotkey F9 couldn't be registered !");
            }

            if (!F10Registered)
            {
                Console.WriteLine("Global Hotkey F10 couldn't be registered !");
            }

        }



 
            private void Form1_Load(object sender, EventArgs e)
        {
            this.TransparencyKey = Color.Turquoise;
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 2;
            label1.Text = "00:00:00";
            this.KeyPreview = true;
            
            

        }
        protected override void WndProc(ref Message m)
        {
            // 5. Catch when a HotKey is pressed !
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();
                // MessageBox.Show(string.Format("Hotkey #{0} pressed", id));

                // 6. Handle what will happen once a respective hotkey is pressed
                switch (id)
                {
                    case 1:
                        timer1.Stop();
                        runnnin = false;
                        sn = 00;
                        mm = 00;
                        hr = 00;
                        label1.Text = "00:00:00";
                        break;
                    case 2:
                        if (runnnin == false)
                        {
                            timer1.Start();
                            runnnin = true;

                        }
                        else
                        {
                            timer1.Stop();
                            runnnin = false;
                        }

                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            runnnin = false;
            sn = 00;
            mm = 00;
            hr = 00;
            label1.Text = "00:00:00";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (focused == false)
            {
                this.BackColor = Color.Turquoise;
                this.FormBorderStyle = FormBorderStyle.None;
                button1.Visible = false;
                button2.Visible = false;
                pictureBox1.Visible = false;
                button3.Text = "Unfocus";
                focused = true;
            }
            else
            {
                this.BackColor = SystemColors.Control;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                button1.Visible = true;
                button2.Visible = true;
                pictureBox1.Visible = true;
                button3.Text = "Focus";
                focused = false;
            }
            

           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (runnnin == false)
            {
                timer1.Start();
                runnnin = true;

            }
            else
            {
                timer1.Stop();
                runnnin = false;
            }
            

        }

        private void timer1_Tick(object sender, EventArgs e)
        {


            increase();
            reformat();

            
        }


        public void reformat()
        {

            if (sn <10 || mm<10|| hr <10)
            {

                if (hr<10)
                {
                    hrfm = "0" + hr;
                }
                else { hrfm = hr.ToString(); }
                if (mm <10)
                {
                    mmfm = "0" + mm;
                }
                else { mmfm = mm.ToString(); }
                if (sn<10)
                {
                    snfm = "0" + sn;
                }
                else { snfm = sn.ToString(); }

                label1.Text = hrfm + ":" + mmfm + ":" + snfm;


            }
            else
            {
                label1.Text = hr + ":" + mm + ":" + sn;
            }
           


        }
        public void increase()
        {
            if (sn != 59)
            {
                sn++;

            }
            else
            {
                if (mm !=59)
                {
                    sn = 00;
                    mm++;
                }
                else
                {
                    mm = 00;
                    sn = 00;
                    hr++;
                }
            }
        }
    }
}
