using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LAMOSA.SCPP.Client.View.HandHeld.DataAccess
{

    public static class ConfigDataAccess
    {

        #region fields

        private static MSSQLce oMSSQLce = new MSSQLce(ObtenerCadenaConexion());
        private static HHsvc.SCPP_HH ServiceProxy = new HHsvc.SCPP_HH();

        #endregion fields

        #region methods

        #region ObtenerCadenaConexion
        public static String ObtenerCadenaConexion()
        {
            string sAppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string sMSSQLServerCE_ConnectionString = "Data Source = '" + sAppPath + @"\DataAccess\" + "lamosa.sdf'; Password = 'p12345'; Enlist = False; max buffer size = 1024; max database size = 1024; Mode = ''; Persist Security Info = False";
            return sMSSQLServerCE_ConnectionString;
        }
        #endregion ObtenerCadenaConexion
        #region ObtenerConexion
        public static MSSQLce ObtenerConexion()
        {
            return oMSSQLce;
        }
        #endregion ObtenerConexion
        #region ObtenerServiceProxy
        public static HHsvc.SCPP_HH ObtenerServiceProxy()
        {
            return ServiceProxy;
        }
        #endregion ObtenerServiceProxy

        #endregion methods

    }

    public enum eTipoConexion
    {
        Indeterminada = -1,
        Local = 1,
        Servicio = 2
    }

}
