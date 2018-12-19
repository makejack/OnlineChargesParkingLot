using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace OnlineChargesParkingLot.ControlExpansion
{
    public class ButtonExpansion
    {
        public static void ClearFocus(Control.ControlCollection controls)
        {
            foreach (Control item in controls)
            {
                if (item is Button)
                {
                    PropertyInfo property = typeof(Button).GetProperty("Selectable");
                    property.SetValue(item, false, null);
                }
                else
                {
                    if (item.Controls.Count > 0)
                    {
                        ClearFocus(item.Controls);
                    }
                }
            }
        }
    }
}
