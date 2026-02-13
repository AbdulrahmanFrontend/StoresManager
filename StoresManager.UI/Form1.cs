using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoresManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                CustomMessageBox.ShowMessage("Please enter email!", "Inventory Management System Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                tbUsername.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(tbPassword.Text))
            {
                CustomMessageBox.ShowMessage("Please enter password!", "Inventory Management System Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                tbPassword.Focus();
                return;
            }
            
            this.Hide();
            frmMain das = new StoresManager.frmMain();
            das.ShowDialog();
            this.Show();
        }






        private void DragMouseDown(object sender, MouseEventArgs e)
        {
            clsPublicOperations.DragMouseDown(sender, e);
        }
        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            clsPublicOperations.DragMouseMove(sender, e);
        }
        private void DragMouseUp(object sender, MouseEventArgs e)
        {
            clsPublicOperations.DragMouseUp(sender, e);
        }





    }
}
