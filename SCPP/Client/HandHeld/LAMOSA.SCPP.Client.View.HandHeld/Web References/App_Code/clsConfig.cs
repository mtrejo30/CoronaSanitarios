using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using DT.CE;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public static class clsConfig
    {
        private static clsConexion oCnx = new clsConexion(getConnectionString(), edbTipo.dbSQLServer);

        public static String getConnectionString()
        {
            /*string sAppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            System.Xml.XmlDocument oxml = new System.Xml.XmlDocument();
            oxml.Load(sAppPath + "\\" + "config.xml");
            string strConnectionString = oxml.DocumentElement.ChildNodes[0].ChildNodes[0].Value;
            return strConnectionString;*/
            
            string sAppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string sMSSQLServerCE_ConnectionString = "Data Source = '" + sAppPath + @"\" + "lamosa.sdf'; Password = 'Perni_554411'; Enlist = False; max buffer size = 1024; max database size = 1024; Mode = ''; Persist Security Info = False";
            return sMSSQLServerCE_ConnectionString;
        }

        public static clsConexion getConection()
        {
            return oCnx;
        }

    }
}
