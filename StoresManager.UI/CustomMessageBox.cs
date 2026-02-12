using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoresManager
{
    public static class CustomMessageBox
    {
        private static readonly frmCustomMessageBox _messageBox = new frmCustomMessageBox();

        public static DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon Icon)
        {
            _messageBox.Initialize(message, caption, buttons, Icon);
            return _messageBox.ShowDialog();
        }

        public static DialogResult ShowConfirmation(string message, string caption)
        {
            _messageBox.InitializeConfirmation(message, caption);
            return _messageBox.ShowDialog();
        }

        public static DialogResult ShowError(string message, string caption, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Error)
        {
            _messageBox.InitializeError(message, caption, buttons, icon);
            return _messageBox.ShowDialog();
        }


        /* How TO USE !! Active it
        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTxt.Text))
            {
                CustomMessageBox.ShowMessage("Please enter email!", "Inventory Management System Validation", MessageBoxButtons.OK, MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtLogin.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                CustomMessageBox.ShowMessage("Please enter password!", "Inventory Management System Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                txtPassword.Focus();
                return;
            }
        }
        */

    }
}
