using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace MyUI
{
    public static class MyContextMenuStrip
    {
        static public Font Font = new Font("微軟正黑體", 12);
        public static ToolStripMenuItem GetToolStripMenuItem(string text)
        {
            return GetToolStripMenuItem(text, Font);
        }
        public static ToolStripMenuItem GetToolStripMenuItem(string text, Font font)
        {
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
            toolStripMenuItem.Text = text;
            toolStripMenuItem.Font = font;
            return toolStripMenuItem;
        }

        public static ToolStripMenuItem GetToolStripMenuItem(ToolStripMenuItem main_toolStripMenuItem, params ToolStripMenuItem[] toolStripMenuItems)
        {
            main_toolStripMenuItem.DropDownItems.AddRange(toolStripMenuItems);
            return main_toolStripMenuItem;
        }
        public static ContextMenuStrip GetContextMenuStrip(params ToolStripMenuItem[] toolStripMenuItems)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.AddRange(toolStripMenuItems);
            return contextMenuStrip;
        }
    }
}
