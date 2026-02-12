using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StoresManager
{
    public partial class frmCustomMessageBox : Form
    {
        public frmCustomMessageBox()
        {
            InitializeComponent();
        }

        public void Initialize(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            lblMessageCaption.Text = caption;

            lblMessage.Text = InsertLineBreaks(message);
            SetButtonsVisibility(buttons);
            SetIcon(icon);
        }

        public void InitializeConfirmation(string message, string caption)
        {
            // Implement the initialization logic for confirmation dialog
            // Set, labels, buttons, and icon for confirmation dialog
            lblMessageCaption.Text = caption;
            lblMessage.Text = message;
            SetButtonsVisibility(MessageBoxButtons.YesNo);
            SetIcon(MessageBoxIcon.Question);
        }

        public void InitializeError(string message, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            // Implement the initialization logic for error dialog
            // Set labels, buttons, and icon for error dialog
            lblMessageCaption.Text = caption;
            lblMessage.Text = message;
            SetButtonsVisibility(buttons);
            SetIcon(icon);
        }

        private void SetButtonsVisibility(MessageBoxButtons buttons)
        {
            btnOK.Visible = false;
            btnYes.Visible = false;
            btnNo.Visible = false;
            btnCancel.Visible = false;
            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    btnOK.Visible = true;
                    break;
                case MessageBoxButtons.OKCancel:
                    btnOK.Visible = true;
                    btnCancel.Visible = true;
                    break;
                case MessageBoxButtons.YesNo:
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                    btnCancel.Visible = true;
                    break;
            }
        }

        private void SetIcon(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Information:
                    pbIcon.Image = SystemIcons.Information.ToBitmap();

                    break;
                case MessageBoxIcon.Warning:
                    pbIcon.Image = SystemIcons.Warning.ToBitmap();
                    break;
                case MessageBoxIcon.Error:
                    pbIcon.Image = SystemIcons.Error.ToBitmap();
                    break;
                case MessageBoxIcon.Question:
                    pbIcon.Image = SystemIcons.Question.ToBitmap();
                    break;
                // Add cases for other icon configurations if needed...
                default:
                    pbIcon.Image = null; // No icon
                    break;
            }
        }

        private string InsertLineBreaks(string message)
        {
            // Insert line breaks after a certain number of characters
            int maxCharsPerLine = 50; // Adjust as needed
            StringBuilder sb = new StringBuilder();
            int charCount = 0;

            foreach (char c in message)
            {
                sb.Append(c);
                charCount++;

                if (charCount >= maxCharsPerLine && c == ' ')
                {
                    sb.AppendLine();
                    charCount = 0;
                }
            }
            
            return sb.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }
}