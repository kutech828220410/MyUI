using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
namespace ComponentSet
{
    /// <summary>
    /// 提供CustomUserControl在Design-Time過濾屬性以及事件的類別
    /// </summary>
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class JLabelExDesigner : ParentControlDesigner
    {
        /// <summary>
        /// 設定要過濾掉的的屬性
        /// </summary>
        static String[] DisabledProperty = new string[] 
        {
            "AccessibleDescription", "AccessibleName", "AccessibleRole",
            "AllowDrop", "Anchor", "AutoScroll", "AutoScrollMargin", "AutoScrollMinSize",
            "AutoSize", "AutoSizeMode", "AutoValidate", "BackColor", "BackgroundImage",
            "BackgroundImageLayout", "CausesValidation", "ContextMenuStrip",
            "Cursor","ForeColor", "ImeMode", "MaximumSize", "MinimumSize", "RightToLeft", 
            "Tag", "UseWaitCursor", "Lines"
        };

        /// <summary>
        /// 過濾屬性
        /// </summary>
        /// <param name="properties"></param>
        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            //針對UserControl每個屬性進行過濾
            foreach (var pi in typeof(System.Windows.Forms.UserControl).GetProperties())
            {
                if (properties.Contains(pi.Name))
                {
                    if (DisabledProperty.Contains(pi.Name))
                    {
                        properties.Remove(pi.Name);
                    }
                }
            }

            base.PreFilterProperties(properties);
        }
        protected override void PreFilterEvents(System.Collections.IDictionary events)
        {
            //針對UserControl每個事件進行過濾
            foreach (var ei in typeof(System.Windows.Forms.UserControl).GetEvents())
            {
                if (events.Contains(ei.Name))
                    events.Remove(ei.Name);
            }

            base.PreFilterEvents(events);
        }

    }
}
