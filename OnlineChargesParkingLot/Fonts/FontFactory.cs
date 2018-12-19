using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;

namespace OnlineChargesParkingLot.Fonts
{
    public class FontFactory
    {


        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
        
        public static PrivateFontCollection LedFont { get; set; }

        public static void InitiailseFont()
        {
            if (LedFont == null)
            {
                LedFont = new PrivateFontCollection();
                byte[] fontLedData = Properties.Resources.UnidreamLED;
                IntPtr pFontData = Marshal.AllocHGlobal(fontLedData.Length);
                try
                {
                    Marshal.Copy(fontLedData, 0, pFontData, fontLedData.Length);
                    LedFont.AddMemoryFont(pFontData, fontLedData.Length);
                    uint dummy = 0;
                    AddFontMemResourceEx(pFontData, (uint)fontLedData.Length, IntPtr.Zero, ref dummy);
                }
                catch (Exception ex)
                {
                    Marshal.FreeHGlobal(pFontData);
                }
            }
        }
    }
}
