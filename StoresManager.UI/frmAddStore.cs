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
    public partial class frmAddStore : Form
    {
        public frmAddStore()
        {
            InitializeComponent();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void Alert(string msg, frmToast.enType type)
        {
            frmToast frm = new frmToast();
            frm.showAlert(msg, type);
        }


        /*
         this.Alert("User has been updated successfully", frmToast.enType.Success);
        */







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









        /*  We are make it general in public class !!
         *  
        private bool Drag;
        private int MouseX;
        private int MouseY;

        private void DragMouseDown(object sender, MouseEventArgs e)
        {
            Drag = true;
            MouseX = Cursor.Position.X - this.Left;
            MouseY = Cursor.Position.Y - this.Top;
        }

        private void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                this.Top = Cursor.Position.Y - MouseY;
                this.Left = Cursor.Position.X - MouseX;
            }
        }
        
        private void DragMouseUp(object sender, MouseEventArgs e)
        {
            Drag = false;
        }
        */



    }
}
