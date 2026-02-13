using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static StoresManager.frmToast;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace StoresManager
{
    public partial class frmToast : Form
    {
        public frmToast()
        {
            InitializeComponent();
            this.TopMost = true;
        }

        public enum enAction
        {
            wait,
            start,
            close
        }
        public enum enType
        {
            Success,
            Warning,
            Error,
            Info
        }

        private frmToast.enAction action;

        private int x, y;

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (this.action)
            {
                case enAction.wait:
                    timer1.Interval = 2000;
                    action = enAction.close;
                    break;
                case frmToast.enAction.start:
                    this.timer1.Interval = 1;
                    this.Opacity += 0.1;
                    if (this.x < this.Location.X)
                    {
                        this.Left--;
                    }
                    else
                    {
                        if (this.Opacity == 1.0)
                        {
                            action = frmToast.enAction.wait;
                        }
                    }
                    break;
                case enAction.close:
                    timer1.Interval = 1;
                    this.Opacity -= 0.1;

                    this.Left -= 3;
                    if (base.Opacity == 0.0)
                    {
                        base.Close();
                        timer1.Stop();
                    }
                    break;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void showAlert(string msg, enType type)
        {
            Opacity = 0.0;
            this.StartPosition = FormStartPosition.Manual;
            string fname;

            for (int i = 1; i < 10; i++)
            {
                fname = "alert" + i.ToString();
                frmToast frm = (frmToast)Application.OpenForms[fname];

                if (frm == null)
                {

                    this.Name = fname;
                    this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                    this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i;
                    this.Location = new Point(this.x, this.y);
                    break;
                }
            }

            this.x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;

            switch (type)
            {
                case enType.Success:
                    this.pictureBox1.Image = StoresManager.Properties.Resources.icons8_checkmark_36;
                    this.BackColor = Color.SeaGreen;
                    break;
                case enType.Error:
                    this.pictureBox1.Image = StoresManager.Properties.Resources.icons8_close_36;
                    this.BackColor = Color.DarkRed;
                    break;
                case enType.Info:
                    this.pictureBox1.Image = StoresManager.Properties.Resources.icons8_information_50;
                    this.BackColor = Color.RoyalBlue;
                    break;
                case enType.Warning:
                    this.pictureBox1.Image = StoresManager.Properties.Resources.icons8_brake_warning_36;
                    this.BackColor = Color.DarkOrange;
                    break;
            }

            this.lblMsg.Text = msg;

            this.Show();
            this.action = enAction.start;
            this.timer1.Interval = 1;
            this.timer1.Start();
        }



        /* To Active it:: 

        public void Alert(string msg, frmToast.enType type)
        {
            frmToast frm = new frmToast();
            frm.showAlert(msg, type);
        }


        */




    }
}