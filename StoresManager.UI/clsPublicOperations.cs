using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoresManager
{
    public static class clsPublicOperations
    {
        public static bool Drag;
        public static int MouseX;
        public static int MouseY;

        public static void DragMouseDown(object sender, MouseEventArgs e)
        {
            Control ct = (Control)sender;
            Form frm = ct.FindForm();

            if (frm != null)
            {
                Drag = true;
                MouseX = Cursor.Position.X - frm.Left;
                MouseY = Cursor.Position.Y - frm.Top;
            }
        }

        public static void DragMouseMove(object sender, MouseEventArgs e)
        {
            if (Drag)
            {
                Control ct = (Control)sender;
                Form frm = ct.FindForm();

                if (frm != null)
                {
                    frm.Top = Cursor.Position.Y - MouseY;
                    frm.Left = Cursor.Position.X - MouseX;
                }
            }
        }

        public static void DragMouseUp(object sender, MouseEventArgs e)
        {
            Drag = false;
        }


    }
}
