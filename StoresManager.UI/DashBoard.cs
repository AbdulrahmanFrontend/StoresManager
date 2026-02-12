using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoresManager
{
    public partial class frmMain : Form
    {
        private readonly Color ActiveColor = Color.FromArgb(52, 152, 219);
        private readonly Color InactiveColor = Color.FromArgb(32, 43, 54);

        public frmMain()
        {
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            this.DoubleBuffered = true;
            CustomSizeDesigning();

            lblDate.Text = DateTime.Now.ToLongDateString();

            HideSubMenu();

            SetButtonColors(btnDashboard, ActiveColor);

            pnlSlide.Height = btnDashboard.Height;
            pnlSlide.Top = btnDashboard.Top;
            pnlSlide.BringToFront();
        }

        private void CustomSizeDesigning()
        {
            pnlSettingSubMenu.Visible = false;
        }

        private void SetButtonColors(Button btn, Color color)
        {
            btn.BackColor = color;
            btn.ForeColor = Color.White;
        }

        private void HideSubMenu()
        {
            pnlSettingSubMenu.Visible = false;
        }

        private void ShowSubMenu(Panel submenu)
        {
            pnlSettingSubMenu.Visible = !submenu.Visible;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.lblTime.Text = DateTime.Now.ToString("hh:MM:ss tt");
        }

        private void pbMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pbMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState.ToString() == "Normal")
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            HideSubMenu();

            SetButtonColors(btnDashboard, ActiveColor);
            SetButtonColors(btnTransacions, InactiveColor);
            SetButtonColors(btnStores, InactiveColor);
            SetButtonColors(btnStoreStocks, InactiveColor);
            SetButtonColors(btnItems, InactiveColor);
            SetButtonColors(btnSettings, InactiveColor);

            pnlSlide.Height = btnDashboard.Height;
            pnlSlide.Top = btnDashboard.Top;
            pnlSlide.BringToFront();
        }

        private void btnTransacions_Click(object sender, EventArgs e)
        {
            HideSubMenu();

            SetButtonColors(btnDashboard, InactiveColor);
            SetButtonColors(btnTransacions, ActiveColor);
            SetButtonColors(btnStores, InactiveColor);
            SetButtonColors(btnStoreStocks, InactiveColor);
            SetButtonColors(btnItems, InactiveColor);
            SetButtonColors(btnSettings, InactiveColor);

            pnlSlide.Height = btnTransacions.Height;
            pnlSlide.Top = btnTransacions.Top;
            pnlSlide.BringToFront();
        }

        private void btnStores_Click(object sender, EventArgs e)
        {
            HideSubMenu();

            SetButtonColors(btnDashboard, InactiveColor);
            SetButtonColors(btnTransacions, InactiveColor);
            SetButtonColors(btnStores, ActiveColor);
            SetButtonColors(btnStoreStocks, InactiveColor);
            SetButtonColors(btnItems, InactiveColor);
            SetButtonColors(btnSettings, InactiveColor);

            pnlSlide.Height = btnStores.Height;
            pnlSlide.Top = btnStores.Top;
            pnlSlide.BringToFront();
        }

        private void btnStoreStocks_Click(object sender, EventArgs e)
        {
            HideSubMenu();

            SetButtonColors(btnDashboard, InactiveColor);
            SetButtonColors(btnTransacions, InactiveColor);
            SetButtonColors(btnStores, InactiveColor);
            SetButtonColors(btnStoreStocks, ActiveColor);
            SetButtonColors(btnItems, InactiveColor);
            SetButtonColors(btnSettings, InactiveColor);

            pnlSlide.Height = btnStoreStocks.Height;
            pnlSlide.Top = btnStoreStocks.Top;
            pnlSlide.BringToFront();
        }

        private void btnItems_Click(object sender, EventArgs e)
        {
            HideSubMenu();

            SetButtonColors(btnDashboard, InactiveColor);
            SetButtonColors(btnTransacions, InactiveColor);
            SetButtonColors(btnStores, InactiveColor);
            SetButtonColors(btnStoreStocks, InactiveColor);
            SetButtonColors(btnItems, ActiveColor);
            SetButtonColors(btnSettings, InactiveColor);

            pnlSlide.Height = btnItems.Height;
            pnlSlide.Top = btnItems.Top;
            pnlSlide.BringToFront();

        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ShowSubMenu(pnlSettingSubMenu);

            SetButtonColors(btnDashboard, InactiveColor);
            SetButtonColors(btnTransacions, InactiveColor);
            SetButtonColors(btnStores, InactiveColor);
            SetButtonColors(btnStoreStocks, InactiveColor);
            SetButtonColors(btnItems, InactiveColor);
            SetButtonColors(btnSettings, ActiveColor);

            pnlSlide.Height = btnSettings.Height;
            pnlSlide.Top = btnSettings.Top;
            pnlSlide.BringToFront();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            pnlSlide2.Height = btnUsers.Height;
            pnlSlide2.Top = btnUsers.Top;
            pnlSlide2.BringToFront();
        }

        private void btnUnits_Click(object sender, EventArgs e)
        {
            //frm frm = new frm();
            //frm.ShowDialog();

            pnlSlide2.Height = btnUnits.Height;
            pnlSlide2.Top = btnUnits.Top;
            pnlSlide2.BringToFront();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }




    }
}