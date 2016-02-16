using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
//using LAMOSA.SCPP.Client.View.HandHeld.User_Interface;
using System.Data;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{

    static class Program
    {

        #region Main
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            try
            {
                DA.ConfigDataAccess.ObtenerConexion().AbrirConexion();
                //LoginUsuario lu = new LoginUsuario();
                //lu.NomEmpleado = "Erwin Gomez Rivero";
                //lu.DesPuesto = "Desarrollador";
                //lu.DesTurno = "Mixto";
                //lu.DesPlanta = "Planta MTY";
                //lu.CodPieza = 50166;
                //lu.CodBarras = "0005929776";
                //lu.CodProceso = 3;
                //a04_Defectos frmObj = new a04_Defectos(lu, false);

                //a00_CargaDatos frmObj = new a00_CargaDatos();
                a01_Login frmObj = new a01_Login();
                /*HHsvc.SCPP_HH serviceSCPP = new LAMOSA.SCPP.Client.View.HandHeld.HHsvc.SCPP_HH();
                MessageBox.Show(serviceSCPP.ObtenerMensajeInicioSesion(), "Mensaje de inicio de sesión");
                serviceSCPP.Dispose();*/
                Application.Run(frmObj);
                //Application.Run(new frmBienvenida());
                DA.ConfigDataAccess.ObtenerConexion().CerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
        #endregion Main

        #region ProbarCargaDatos
        public static void ProbarCargaDatos()
        {
            List<DataSet> lds = new List<DataSet>();
            DataSet ds = null;
            DataTable dt = null;
            DataRow dr = null;

            ds = new DataSet();

            dt = new DataTable("InsTabla01");
            dt.Columns.Add("Columna01", Type.GetType("System.Int32"));
            dt.Columns.Add("Columna02", Type.GetType("System.Int64"));
            dt.Columns.Add("Columna03", Type.GetType("System.Double"));
            dt.Columns.Add("Columna04", Type.GetType("System.String"));
            dt.Columns.Add("Columna05", Type.GetType("System.DateTime"));
            dt.Columns.Add("Columna06", Type.GetType("System.DateTime"));
            for (int i = 0; i < 10; i++)
            {
                dr = dt.NewRow();
                dr["Columna01"] = (int)(1 + i);
                dr["Columna02"] = (long)(100 + i);
                dr["Columna03"] = (double)(1.5 + i);
                dr["Columna04"] = "valor" + (1 + i).ToString();
                dr["Columna05"] = DateTime.Now;
                dr["Columna06"] = DBNull.Value;
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            ds.Tables.Add(dt);
            //
            dt = new DataTable("UpdTabla01");
            dt.Columns.Add("Columna00", Type.GetType("System.Int32"));
            dt.Columns.Add("Columna01", Type.GetType("System.Int32"));
            dt.Columns.Add("Columna02", Type.GetType("System.Int64"));
            dt.Columns.Add("Columna03", Type.GetType("System.Double"));
            dt.Columns.Add("Columna04", Type.GetType("System.String"));
            dt.Columns.Add("Columna05", Type.GetType("System.DateTime"));
            dt.Columns.Add("Columna06", Type.GetType("System.DateTime"));
            for (int i = 0; i < 5; i++)
            {
                dr = dt.NewRow();
                dr["Columna00"] = (int)(1);
                dr["Columna01"] = (int)(1 + i);
                dr["Columna02"] = (long)(200 + i);
                dr["Columna03"] = (double)(3.5 + i);
                dr["Columna04"] = "valor" + (2 + i).ToString();
                dr["Columna05"] = DateTime.Now;
                dr["Columna06"] = DBNull.Value;
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            ds.Tables.Add(dt);
            //
            dt = new DataTable("DelTabla01");
            dt.Columns.Add("Columna01", Type.GetType("System.Int32"));
            for (int i = 5; i < 8; i++)
            {
                dr = dt.NewRow();
                dr["Columna01"] = (int)(1 + i);
                dt.Rows.Add(dr);
            }
            dt.AcceptChanges();
            ds.Tables.Add(dt);

            lds.Add(ds);

            c00_CargaDatos cObj = new c00_CargaDatos();
            //cObj.(lds);
        }
        #endregion ProbarCargaDatos

    }

}