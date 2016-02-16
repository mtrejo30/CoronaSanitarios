using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public static class HelperView
    {

        #region fields

        private static Color DefaultBGColor = Color.Transparent;
        private static Color DefaultFColor = Color.Black;
        private static Font DefaultFont = new Font("Tahoma", 9, FontStyle.Regular);
        private static Color MBGColor = Color.Red;
        private static Color MFColor = Color.Black;
        private static Font MFont = new Font("Tahoma", 9, FontStyle.Bold);

        #endregion fields

        #region SetDefaultMessageStyle
        public static void SetDefaultMessageStyle(Control control)
        {
            control.BackColor = DefaultBGColor;
            control.ForeColor = DefaultFColor;
            control.Font = DefaultFont;
        }
        #endregion SetDefaultMessageStyle
        #region SetMessageStyle
        public static void SetMessageStyle(Control control)
        {
            control.BackColor = MBGColor;
            control.ForeColor = MFColor;
            control.Font = MFont;
        }
        #endregion SetMessageStyle

        #region EstablecerEstadoConexion
        public static void EstablecerEstadoConexion(Control control, EstadoConexion ec)
        {
            try
            {
                if (ec == EstadoConexion.Offline)
                {
                    control.BackColor = Color.Red;
                    control.ForeColor = Color.Black;
                    control.Text = "Offline";
                }
                else if (ec == EstadoConexion.Online)
                {
                    control.BackColor = Color.Green;
                    control.ForeColor = Color.White;
                    control.Text = "Online";
                }
                else if (ec == EstadoConexion.Actualizando)
                {
                    control.BackColor = Color.Blue;
                    control.ForeColor = Color.White;
                    control.Text = "Actualizando";
                }
                else if (ec == EstadoConexion.Procesando)
                {
                    control.BackColor = Color.Blue;
                    control.ForeColor = Color.White;
                    control.Text = "Procesando";
                }
            }
            catch (Exception e) { throw e; }
        }
        #endregion EstablecerEstadoConexion

    }

    public enum EstadoConexion
    {
        Indeterminado = -1,
        Offline = 1,
        Online = 2,
        Actualizando = 3,
        Procesando = 4
    }
}
