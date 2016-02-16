using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c00_CargaDatos
    {

        #region fields

        private c00_Common co = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c00_CargaDatos()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c00_CargaDatos()
        {

        }
        #endregion Constructors and Destructor

        #region Common

        #region columnValue
        private String columnValue(Type type, String value)
        {
            if (type.FullName.Equals("System.DBNull"))
                value = "NULL";
            else if (value.ToUpper() == "TRUE")
                value = "1";
            else if (value.ToUpper() == "FALSE")
                value = "0";
            else if (type.FullName.Equals("System.String"))
                value = "'" + value + "'";
            else if (type.FullName.Equals("System.DateTime"))
            {
                DateTime d = Convert.ToDateTime(value);
                value = "'" + d.ToString("yyyyMMdd HH:mm:ss") + "'";
            }

            return value;
        }
        #endregion columnValue
        #region InsertarInformacion
        public bool InsertarInformacion(DataTable dt, string sNombreTabla)
        {
            SqlCeParameter[] pars = new SqlCeParameter[0];
            bool error = false;
            string sSentenciaParteInicial = string.Empty;
            string sSentencia = string.Empty;
            string sValor = string.Empty;
            try
            {
                if (sNombreTabla.ToLower() == "usuario")
                    EncriptarContrasenaUsuario(ref dt);
                sSentenciaParteInicial = "insert " + sNombreTabla + " (";
                foreach (DataColumn dc in dt.Columns)
                {
                    sSentenciaParteInicial += dc.ColumnName + ", ";
                }
                sSentenciaParteInicial = sSentenciaParteInicial.Substring(0, sSentenciaParteInicial.Length - 2) + ") values (";

                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        sSentencia = sSentenciaParteInicial;
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (System.Convert.IsDBNull(dr[dc]))
                            {
                                sValor = "null";
                                sSentencia += sValor;
                            }
                            else
                            {
                                sValor = Convert.ToString(dr[dc]);
                                sValor = sValor.Replace("'", "");
                                if (dc.DataType == Type.GetType("System.String"))
                                {
                                    sSentencia += "'" + sValor + "'";
                                }
                                else if (dc.DataType == Type.GetType("System.DateTime"))
                                {
                                    DateTime d = Convert.ToDateTime(dr[dc]);
                                    sSentencia += "'" + d.ToString("yyyyMMdd HH:mm:ss") + "'";
                                }
                                else if (dc.DataType == Type.GetType("System.Boolean"))
                                {
                                    if (sValor.ToUpper() == "TRUE")
                                    {
                                        sSentencia += "1";
                                    }
                                    else if (sValor.ToUpper() == "FALSE")
                                    {
                                        sSentencia += "0";
                                    }
                                }
                                else
                                {
                                    sSentencia += sValor;
                                }
                            }
                            sSentencia += ", ";

                        }
                        sSentencia = sSentencia.Substring(0, sSentencia.Length - 2) + ");";

                        DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(sSentencia, pars);
                    }
                    catch (Exception e)
                    {
                        if (sSentencia.IndexOf(".pk_") != -1)
                        {
                            HHsvc.SCPP_HH scpp = new HHsvc.SCPP_HH();
                            scpp.InsertaError(sSentencia, e.Message);
                            error = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return error;
        }
        #endregion
        #region ActualizarInformacion
        public bool ActualizarInformacion(DataTable dtUpd, String tableName)
        {
            if (tableName.ToLower() == "usuario")
                EncriptarContrasenaUsuario(ref dtUpd);
            SqlCeParameter[] pars = new SqlCeParameter[0];
            bool error = false;
            String[] columnName = new String[dtUpd.Columns.Count];
            String qry = "";
            for (int i = 0; i < columnName.Length; i++)
                columnName[i] = dtUpd.Columns[i].ColumnName;

            foreach (DataRow dr in dtUpd.Rows)
            {
                try
                {
                    qry = "update " + tableName + " set ";
                    for (int i = 1; i < columnName.Length; i++)
                        qry += (i == 1 ? "" : ", ") + columnName[i] + " = " + columnValue(dr[i].GetType(), dr[i].ToString());
                    String pk = columnName[0].Trim().Length > 0 ? columnName[0] + " = " : "";
                    qry += " where " + pk + dr[0].ToString();

                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(qry, pars);
                }
                catch (Exception e)
                {
                    HHsvc.SCPP_HH scpp = new HHsvc.SCPP_HH();
                    scpp.InsertaError(qry, e.Message);
                    error = true;
                }
            }
            return error;
        }
        #endregion
        #region ObtenerUpdate2
        private List<string> ObtenerUpdate2(DataTable dt, string sNombreTabla)
        {
            List<string> lUpd = new List<string>();
            string sSentenciaParteInicial = string.Empty;
            string sSentenciaParteWhere = string.Empty;
            string sSentencia = string.Empty;
            string sValor = string.Empty;
            int iNumCamposPK = -1;

            sSentenciaParteInicial = "update " + sNombreTabla + " set ";

            foreach (DataRow dr in dt.Rows)
            {
                iNumCamposPK = Convert.ToInt32(dr[0]);
                int contPK = 0;
                sSentencia = sSentenciaParteInicial;
                sSentenciaParteWhere = " where ";
                bool bEsPrimera = true;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (bEsPrimera)
                    {
                        bEsPrimera = false;
                        continue;
                    }
                    if (contPK < iNumCamposPK)
                    {
                        sSentenciaParteWhere += dc.ColumnName + " = ";
                        sValor = Convert.ToString(dr[dc]);
                        if (dc.DataType == Type.GetType("System.String") || dc.DataType == Type.GetType("System.DateTime"))
                        {
                            sSentenciaParteWhere += "'" + sValor + "'";
                        }
                        else
                        {
                            sSentenciaParteWhere += sValor;
                        }
                        sSentenciaParteWhere += " and ";
                    }
                    else
                    {
                        sSentencia += dc.ColumnName + " = ";
                        if (System.Convert.IsDBNull(dr[dc]))
                        {
                            sValor = "null";
                            sSentencia += sValor;
                        }
                        else
                        {
                            sValor = Convert.ToString(dr[dc]);
                            if (dc.DataType == Type.GetType("System.String") || dc.DataType == Type.GetType("System.DateTime"))
                            {
                                sSentencia += "'" + sValor + "'";
                            }
                            else
                            {
                                sSentencia += sValor;
                            }
                        }
                        sSentencia += ", ";
                    }
                    contPK++;
                }
                sSentencia = sSentencia.Substring(0, sSentencia.Length - 2) + sSentenciaParteWhere.Substring(0, sSentenciaParteWhere.Length - 5) + ";";
                lUpd.Add(sSentencia);
            }
            return lUpd;
        }
        #endregion ObtenerUpdate2
        #region EliminarInformacion
        private bool EliminarInformacion(DataTable dt, string sNombreTabla)
        {
            SqlCeParameter[] pars = new SqlCeParameter[0];
            bool error = false;
            string sSentenciaParteInicial = string.Empty;
            string sSentencia = string.Empty;
            string sValor = string.Empty;

            sSentenciaParteInicial = "delete from " + sNombreTabla + " where ";

            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    sSentencia = sSentenciaParteInicial;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sSentencia += dc.ColumnName + " = ";

                        if (System.Convert.IsDBNull(dr[dc]))
                        {
                            sValor = "null";
                            sSentencia += sValor;
                        }
                        else
                        {
                            sValor = Convert.ToString(dr[dc]);
                            if (dc.DataType == Type.GetType("System.String") || dc.DataType == Type.GetType("System.DateTime"))
                            {
                                sSentencia += "'" + sValor + "'";
                            }
                            else
                            {
                                sSentencia += sValor;
                            }
                        }
                        sSentencia += " and ";
                    }
                    sSentencia = sSentencia.Substring(0, sSentencia.Length - 5) + ";";

                    DA.ConfigDataAccess.ObtenerConexion().EjecutarConsulta(sSentencia, pars);
                }
                catch (Exception e)
                {
                    HHsvc.SCPP_HH scpp = new HHsvc.SCPP_HH();
                    scpp.InsertaError(sSentencia, e.Message);
                    error = true;
                }
            }
            return error;
        }
        #endregion

        #region CargarDatosALocal
        private Boolean CargarDatosALocal(List<DataSet> lDS)
        {
            Boolean error = false;
            string sNomTabla = string.Empty;
            HHsvc.SCPP_HH scpp = new HHsvc.SCPP_HH();
            String qry = "";
            DataTable dtIns = new DataTable();
            DataTable dtUpd = new DataTable();
            DataTable dtDel = new DataTable();
            string sDesIns = string.Empty;
            string sDesUpd = string.Empty;
            string sDesDel = string.Empty;

            bool bErrorIns = false;
            bool bErrorUpd = false;
            bool bErrorDel = false;
            try
            {
                if (lDS == null) return false;
                for (int i = 0; i < lDS.Count; i++)
                {
                    if (lDS[i] != null) continue;
                    lDS.Remove(lDS[i]);
                    i--;
                }
                foreach (DataSet ds in lDS)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        string sPrefijo = dt.TableName.Substring(0, 3).ToUpper();
                        sNomTabla = dt.TableName.Substring(3);
                        if (sPrefijo == "INS")
                        {
                            dtIns = dt;
                            sDesIns = sNomTabla;
                            //bErrorIns = this.ObtenerInsert(dt, sNomTabla);
                        }
                        else if (sPrefijo == "UPD")
                        {
                            dtUpd = dt;
                            sDesUpd = sNomTabla;
                            //bErrorUpd = this.ObtenerUpdate(dt, sNomTabla);
                        }
                        else if (sPrefijo == "DEL")
                        {
                            dtDel = dt;
                            sDesDel = sNomTabla;
                            //bErrorDel = this.ObtenerDelete(dt, sNomTabla);
                        }
                    }
                }
                bErrorDel = this.EliminarInformacion(dtDel, sDesDel);
                bErrorIns = this.InsertarInformacion(dtIns, sDesIns);
                bErrorUpd = this.ActualizarInformacion(dtUpd, sDesUpd);
                if (bErrorIns || bErrorUpd || bErrorDel)
                    error = true;
            }
            catch (Exception ex)
            {
                scpp.InsertaError("Table:" + sNomTabla + "; QRY:" + qry, ex.Message );
                error = true;
                throw new Exception(this.sClassName + ", CargarDatosALocal: " + ex.Message);
            }
            return error;
        }
        #endregion CargarDatosALocal
        #region ActualizarDatosCatalogos
        public Boolean ActualizarDatosCatalogos(String tableName)
        {
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
            string sError = string.Empty;
            Boolean error = false;
            try
            {

                List<DataSet> lds = new List<DataSet>();

                // Obtener fecha de ultima actualizacion de catalogos.
                DateTime dtFechaUltAct = this.co.ObtenerFechaUltimaActualizacion(0, 0);

                DateTime dtFechaDepuracionHistoria = co.ObtenerFechaDepuracionHistoria();
                co.BorrarInfoTablaTransaccional(tableName, dtFechaDepuracionHistoria);
                DateTime dtFechaActual = DateTime.Now;
                DataSet ds = null;
                try
                {
                    // Obtener datos del servicio WCF.
                    ds = proxy.SyncServHH(tableName,
                                            dtFechaUltAct, true,
                                            dtFechaUltAct, true,
                                            dtFechaUltAct, true);
                }
                catch (Exception e)
                {
                    throw e;
                }
                lds.Add(ds);
                // Actualizar los datos locales.
                error = this.CargarDatosALocal(lds);
            }
            catch (Exception ex)
            {
                error = true;
                proxy.InsertaError("Table:tableName", ex.Message);
            }
            return error;
        }
        public Boolean ActualizarDatosCatalogos(String tableName, int iCodigoPlanta, int iCodigoProceso)
        {
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
            string sError = string.Empty;
            Boolean error = false;
            try
            {

                List<DataSet> lds = new List<DataSet>();

                // Obtener fecha de ultima actualizacion de catalogos.
                DateTime dtFechaUltAct = this.co.ObtenerFechaUltimaActualizacion(0, 0);

                DateTime dtFechaDepuracionHistoria = co.ObtenerFechaDepuracionHistoria(iCodigoPlanta, iCodigoProceso);
                co.BorrarInfoTablaTransaccional(tableName, dtFechaDepuracionHistoria);
                DateTime dtFechaActual = DateTime.Now;
                DataSet ds = null;
                try
                {
                    // Obtener datos del servicio WCF.
                    ds = proxy.SyncServHH(tableName,
                                            dtFechaUltAct, true,
                                            dtFechaUltAct, true,
                                            dtFechaUltAct, true);
                }
                catch (Exception e)
                {
                    throw e;
                }
                lds.Add(ds);
                // Actualizar los datos locales.
                error = this.CargarDatosALocal(lds);
            }
            catch (Exception ex)
            {
                error = true;
                proxy.InsertaError("Table:tableName", ex.Message);
            }
            return error;
        }
        #endregion ActualizarDatosCatalogos;
        #region ActualizarDatosPorProceso
        public string ActualizarDatosPorProceso(String tableName, int planta, int proceso, int pantalla)
        {
            string sError = string.Empty;

            try
            {

                List<DataSet> lds = new List<DataSet>();

                // Obtener fecha de ultima actualizacion de catalogos.
                DateTime dtFechaUltAct = this.co.ObtenerFechaUltimaActualizacion(0, 0);
                DateTime dtFechaActual = DateTime.Now;
                DataSet ds = null;
                try
                {
                    // Obtener datos del servicio WCF.
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    ds = proxy.SyncServHH(tableName,
                                            dtFechaUltAct, true,
                                            dtFechaUltAct, true,
                                            dtFechaUltAct, true);
                }
                catch
                {
                    throw new Exception("Error de Conexion de red");
                }
                lds.Add(ds);
                // Actualizar los datos locales.
                this.CargarDatosALocal(lds);

                sError = string.Empty;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return sError;
        }
        #endregion ActualizarDatosPorProceso
        #region ActualizarDatosCatalogos
        public string ActualizarDatosCatalogos()
        {
            string sError = string.Empty;

            try
            {

                List<DataSet> lds = new List<DataSet>();

                // Obtener fecha de ultima actualizacion de catalogos.
                DateTime dtFechaUltAct = this.co.ObtenerFechaUltimaActualizacion(0, 0);
                DateTime dtFechaActual = DateTime.Now;

                DataSet[] ads = null;
                try
                {
                    // Obtener datos del servicio WCF.
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    ads = proxy.SyncServHHLDS(dtFechaUltAct, true,
                                                        dtFechaUltAct, true,
                                                        dtFechaUltAct, true);
                }
                catch
                {
                    throw new Exception("Error de Conexion de red");
                }

                foreach (DataSet ds in ads)
                {
                    lds.Add(ds);
                }

                // Actualizar los datos locales.
                this.CargarDatosALocal(lds);

                // Actualizar fecha de ultima actualizacion de catalogos.
                this.co.EstablecerFechaUltimaActualizacion(0, 0, dtFechaActual);

                sError = string.Empty;
            }
            catch (Exception ex)
            {
                sError = ex.Message;
            }
            return sError;
        }
        #endregion ActualizarDatosCatalogos
        #region ActualizarDatosPieza
        public Boolean InsertarPiezaLocal(int iCodPieza)
        {
            HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
            string sError = string.Empty;
            Boolean error = false;
            try
            {
                DataTable dt = proxy.ObtenerPieza(iCodPieza, true);
                this.InsertarInformacion(dt,"pieza");
            }
            catch (Exception ex)
            {
                error = true;
                throw ex;
            }
            return error;
        }
        #endregion


        #region ActualizarDatosPorProceso
        public bool ActualizarTablasCatalogos(String tableName, int planta, int proceso)
        {
            bool bError = true;

            try
            {

                List<DataSet> lds = new List<DataSet>();

                // Obtener fecha de ultima actualizacion de catalogos.
                DateTime dtFechaUltAct = this.co.ObtenerFechaUltimaActualizacion(0, proceso);


                DataSet ds = null;
                try
                {
                    // Obtener datos del servicio WCF.
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    ds = proxy.ActualizarCatalogos(tableName,
                                            planta, true,
                                            proceso, true,
                                            dtFechaUltAct, true);
                }
                catch
                {
                    throw new Exception("Error de Conexion de red");
                }
                lds.Add(ds);
                // Actualizar los datos locales.
                bError = this.CargarDatosALocal(lds);
            }
            catch (Exception ex)
            {
                bError = false;
                HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                proxy.InsertaError("Table:tableName", ex.Message);
            }
            return bError;
        }
        public bool ActualizarTablasTransaccionales(String tableName, int planta, int proceso)
        {
            bool bError = true;

            try
            {

                List<DataSet> lds = new List<DataSet>();

                // Obtener fecha de ultima actualizacion de catalogos.
                 DateTime dtFechaUltAct = this.co.ObtenerFechaUltimaActualizacion(proceso, 0);
                
                DataSet ds = null;
                DataSet ds2 = null;
                proceso = proceso == 1 ? 1 : co.ObtenerProcesoAnterior(proceso);
                DateTime dtFechaDepuracionHistoria = co.ObtenerFechaDepuracionHistoria(planta, proceso);
                co.BorrarInfoTablaTransaccional(tableName, dtFechaDepuracionHistoria);
                co.BorrarInfoTablaTransaccional("pieza_transaccion", dtFechaDepuracionHistoria);
                co.BorrarInfoTablaTransaccional("config_handheld", dtFechaDepuracionHistoria);
                co.BorrarInfoTablaTransaccional("pieza_reemplazo", dtFechaDepuracionHistoria);
                try
                {
                    // Obtener datos del servicio WCF.
                    HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                    ds = proxy.ActualizarCatalogos(tableName,
                                            planta, true,
                                            proceso, true,
                                            dtFechaUltAct, true);
                    
                    //Extraer pendiente
                    if (proceso == 1 && tableName == "pieza")
                    {
                        try
                        {
                            DataTable dt = co.ObtenerPendientesRevision(tableName);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                ds2 = proxy.ActualizarCatalogosPorPiezas(dt, tableName, proceso, true);

                                if (ds2 != null && ds2.Tables.Count > 0)
                                {
                                    string sPrefijo = "";
                                    string sNomTabla = "";
                                    foreach (DataTable dt2 in ds2.Tables)
                                    {
                                        sPrefijo = dt2.TableName.Substring(0, 3).ToUpper();
                                        sNomTabla = dt2.TableName.Substring(3);

                                        if (sPrefijo == "UPD")
                                        {
                                            this.ActualizarInformacion(dt2, sNomTabla);
                                        }

                                    }
                                }
                            }
                        }
                        catch { }
                    }
                    
                }
                catch(Exception ex)
                {
                    throw new Exception("Error de Conexion de red");
                }
                lds.Add(ds);
                
                // Actualizar los datos locales.
                bError = this.CargarDatosALocal(lds);


                
                

            }
            catch (Exception ex)
            {
                bError = false;
                HHsvc.SCPP_HH proxy = DA.ConfigDataAccess.ObtenerServiceProxy();
                proxy.InsertaError("Table:tableName", ex.Message);
            }
            return bError;
        }
        #endregion
        private void EncriptarContrasenaUsuario(ref DataTable dtUsuario)
        {
            try
            {
                if (dtUsuario == null) return;
                foreach (DataRow row in dtUsuario.Rows)
                    row["password"] = ((row["password"].ToString() == string.Empty) ? string.Empty : c00_Common.Encrypt(row["password"].ToString(), "Lamosa06"));
            }
            catch (ArgumentNullException ex)
            { throw ex; }
            catch (InvalidOperationException ex)
            { throw ex; }
            catch (InvalidCastException ex)
            { throw ex; }
            catch (Exception ex)
            { throw ex; }
        }
        #endregion Common

        #endregion methods

    }
}
