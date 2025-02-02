using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace MyUI
{
    public class IPAddressInput : UserControl
    {
        public string IPAddress
        {
            get
            {
                return string.Join(".", ipTextBoxes[0].Text, ipTextBoxes[1].Text, ipTextBoxes[2].Text, ipTextBoxes[3].Text);
            }
            set
            {
                string[] ipParts = value.Split('.');
                if (ipParts.Length != 4)
                {
                    throw new ArgumentException("Invalid IP address format");
                }
                for (int i = 0; i < 4; i++)
                {
                    ipTextBoxes[i].Text = ipParts[i];
                }
            }
        }

        private TextBox[] ipTextBoxes = new TextBox[4];
        private Label[] dots = new Label[3];

        private int textBoxWidth = 50;

        [Category("Appearance"), Description("Sets the width of the IP address input boxes.")]
        public int TextBoxWidth
        {
            get { return textBoxWidth; }
            set
            {
                textBoxWidth = value;
                UpdateLayout();
            }
        }

        public IPAddressInput()
        {
            this.Size = new System.Drawing.Size(250, 30);
            this.BackColor = Color.Transparent;
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            this.Controls.Clear();
            int totalWidth = 4 * textBoxWidth + 3 * 10;
            int startX = (this.Width - totalWidth) / 2;

            for (int i = 0; i < 4; i++)
            {
                ipTextBoxes[i] = new TextBox()
                {
                    Width = textBoxWidth,
                    MaxLength = 3,
                    Location = new Point(startX + (i * (textBoxWidth + 15)), 0),
                    TextAlign = HorizontalAlignment.Center
                };
                ipTextBoxes[i].KeyPress += IpTextBox_KeyPress;
                this.Controls.Add(ipTextBoxes[i]);

                if (i < 3)
                {
                    dots[i] = new Label()
                    {
                        Text = ".",
                        Location = new Point(startX + (i * (textBoxWidth + 15)) + textBoxWidth, 5),
                        AutoSize = true,
                        Font = new Font("Arial", 12, FontStyle.Bold)
                    };
                    this.Controls.Add(dots[i]);
                }
            }
        }

        private void IpTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox currentBox = sender as TextBox;
            int currentIndex = Array.IndexOf(ipTextBoxes, currentBox);

            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Enter)
            {
                e.Handled = true;
                return;
            }

            if ((e.KeyChar == ' ' || e.KeyChar == (char)Keys.Enter || (currentBox.Text.Length == 3 && char.IsDigit(e.KeyChar))) && currentIndex < 3)
            {
                ipTextBoxes[currentIndex + 1].Focus();
                ipTextBoxes[currentIndex + 1].SelectAll();
                e.Handled = true;
            }
        }
    }
}
