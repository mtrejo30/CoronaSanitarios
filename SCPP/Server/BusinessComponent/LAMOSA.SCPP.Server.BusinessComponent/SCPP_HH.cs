using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DA01 = Common.DataAccess;
using System.Data.SqlClient;
using Common.SolutionEntityFramework;
using BE = LAMOSA.SCPP.Server.BusinessEntity;
using System.IO;
using System.Security.Cryptography;
using LAMOSA.SCPP.Server.BusinessEntity;
using Lamosa.SCPP.Common.Entities.ClassImpl;
using Lamosa.SCPP.Common.Entities.Interfaces;
using Lamosa.SCPP.Common.Utileria.Entities.ClassImpl;
using Lamosa.SCPP.Common.Utileria.Entities.Interfaces;
using System.Net;
using System.Configuration;
using LAMOSA.SCPP.Server.BusinessEntity.Enums;
using System.Transactions;
using System.Transactions.Configuration;

namespace LAMOSA.SCPP.Server.BusinessComponent
{
    public class SCPP_HH
    {

        #region Fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #region Cadenas de conexion

        // Cadena de conexion SQL Server.
        private string sMSSQLServer_ConnectionString = string.Empty;//Properties.Settings.Default.MSSQLServer_ConnectionString;
        //private string sMSSQLServer_ConnectionString = Properties.Settings.Default.MSSQLServer_ConnectionStringLocal;

        #endregion Cadenas de conexion

        #endregion Fields

        #region Methods

        #region Constructors and Destructor
        public SCPP_HH()
        {
            sMSSQLServer_ConnectionString = ObtenerCadenaConexion();
            this.sClassName = this.GetType().FullName;
        }
        ~SCPP_HH()
        {

        }
        #endregion Constructors and Destructor

        #region Common
        #region Obtener Cadena Conexion
        private string ObtenerCadenaConexion()
        {
            ConnectionStringSettingsCollection connections = null;
            try
            {
                connections = ConfigurationManager.ConnectionStrings;
                if (connections.Count == 0)
                    throw new Exception("No se encontro la definicion de la seccion de configuracion referente a los connectionstring.");
                foreach (ConnectionStringSettings connectionString in connections)
                {
                    if (string.IsNullOrEmpty(connectionString.ConnectionString)) continue;
                    if (connectionString.Name != "csLamosaSCPP") continue;
                    return connectionString.ConnectionString;
                }
                throw new Exception("No se tiene asignado una cadena de conexion para ningun de los connectionstring configurado.");
            }
            catch (ConfigurationException) { throw; }
            catch (Exception) { throw; }
            finally
            {
                connections = null;
            }
        }
        #endregion
        // Control
        #region InsertarRegistroSolicitud
        private long InsertarRegistroSolicitud(string sDesMetodo, DateTime dtFechaHoraSolicitud, string sParametros)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString1 = null;
            SqlParameter[] pars1 = null;
            long lCodRegistro = -1;
            //
            try
            {

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #region Query

                queryString1 = new StringBuilder();
                queryString1.Append("HHRegistroSolicitudIns");

                #endregion Query

                // Parameters
                pars1 = new SqlParameter[4];
                pars1[0] = new SqlParameter("@DesMetodo", SqlDbType.NVarChar, 255);
                pars1[0].Value = sDesMetodo;
                pars1[1] = new SqlParameter("@FechaHoraSolicitud", SqlDbType.DateTime);
                pars1[1].Value = dtFechaHoraSolicitud;
                pars1[2] = new SqlParameter("@Parametros", SqlDbType.NVarChar, 1000);
                pars1[2].Value = sParametros;
                pars1[3] = new SqlParameter("@CodRegistro", SqlDbType.BigInt);
                pars1[3].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString1.ToString(), pars1);
                lCodRegistro = Convert.ToInt64(pars1[3].Value);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarRegistroSolicitud: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return lCodRegistro;
        }
        #endregion InsertarRegistroSolicitud
        #region ActualizarRegistroSolicitud
        private void ActualizarRegistroSolicitud(long lCodRegistro, bool bEjecucionExitosa, string sError)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            //
            try
            {

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #region Query

                queryString = new StringBuilder();
                queryString.Append("HHRegistroSolicitudUpd");

                #endregion Query

                // Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@CodRegistro", SqlDbType.BigInt);
                pars[0].Value = lCodRegistro;
                pars[1] = new SqlParameter("@EjecucionExitosa", SqlDbType.Bit);
                pars[1].Value = bEjecucionExitosa;
                pars[2] = new SqlParameter("@Error", SqlDbType.NVarChar, 2000);
                pars[2].Value = sError;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarRegistroSolicitud: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion ActualizarRegistroSolicitud

        // Acceso - Carga Datos
        #region ObtenerPlantas
        public StringReader[] ObtenerPlantas()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            StringReader[] xmlDS = new StringReader[2];
            DataSet ds = new DataSet();

            try
            {

                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPlantas", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Connection Configuration
                queryString = new StringBuilder();
                queryString.Append("HHVObtenerPlantas");

                // Parameters
                pars = new SqlParameter[0];

                // Query Execution
                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ObtenerPlantas: " + ex.Message);
            }
            return xmlDS;
        }
        #endregion ObtenerPlantas
        #region ObtenerProcesos
        public StringReader[] ObtenerProcesos()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            StringReader[] xmlDS = new StringReader[2];
            DataSet ds = new DataSet();

            try
            {

                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerProcesos", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Connection Configuration
                queryString = new StringBuilder();
                queryString.Append("HHVObtenerProcesos");

                // Parameters
                pars = new SqlParameter[0];

                // Query Execution
                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ObtenerProcesos: " + ex.Message);
            }
            return xmlDS;
        }
        #endregion ObtenerProcesos

        // Acceso - Login
        #region Login
        public StringReader[] Login(String user, String password)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            StringReader[] xmlDS = new StringReader[2];
            DataSet ds = new DataSet();
            try
            {
                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars += "User=" + user + ", ";
                sPars += "Password=" + password;
                lCodRegistro = this.InsertarRegistroSolicitud("Login", DateTime.Now, sPars);
                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                // Connection Configuration
                queryString = new StringBuilder();
                queryString.Append("spLoginHH");
                // Parameters
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@User", SqlDbType.NVarChar, 10);
                pars[index++].Value = user;
                pars[index] = new SqlParameter("@Password", SqlDbType.NVarChar, 255);
                if (password == "Lamosa06")
                    pars[index].Value = EncriptarContrasenaUsuario(password, password);
                else
                    pars[index].Value = EncriptarContrasenaUsuario(user, password);
                // Query Execution
                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", Login: " + ex.Message);
            }
            return xmlDS;
        }
        #endregion Login

        // Acceso - Seleccion Planta
        #region ObtenerPlantasRol
        public StringReader[] ObtenerPlantasRol(int iCodRol)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            StringReader[] xmlDS = new StringReader[2];
            DataSet ds = new DataSet();
            //
            try
            {

                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars += "iCodRol=" + iCodRol.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPlantasRol", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Connection Configuration
                queryString = new StringBuilder();
                queryString.Append("HHVObtenerPlantasRol");

                // Parameters
                int index = 0;
                pars = new SqlParameter[1];
                pars[index] = new SqlParameter("@CodRol", SqlDbType.Int);
                pars[index].Value = iCodRol;

                // Query Execution
                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", Login: " + ex.Message);
            }
            return xmlDS;
        }
        #endregion ObtenerPlantasRol

        // Acceso - Configuracion Inicial
        #region ObtenerTurnos
        public StringReader[] ObtenerTurnos()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            StringReader[] xmlDS = new StringReader[2];
            DataSet ds = new DataSet();
            //
            try
            {

                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerTurnos", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Connection Configuration
                queryString = new StringBuilder();
                queryString.Append("HHVObtenerTurnos");

                // Parameters
                pars = new SqlParameter[0];

                // Query Execution
                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ObtenerTurnos: " + ex.Message);
            }
            return xmlDS;
        }
        #endregion ObtenerTurnos
        #region ObtenerProcesos2
        public StringReader[] ObtenerProcesos2()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            StringReader[] xmlDS = new StringReader[2];
            DataSet ds = new DataSet();
            //
            try
            {

                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerProcesos2", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Connection Configuration
                queryString = new StringBuilder();
                queryString.Append("HHVObtenerProcesos2");

                // Parameters
                pars = new SqlParameter[0];

                // Query Execution
                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ObtenerProcesos2: " + ex.Message);
            }
            return xmlDS;
        }
        #endregion ObtenerProcesos2
        #region ObtenerPantallasProceso
        public StringReader[] ObtenerPantallasProceso(int iCodProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            StringReader[] xmlDS = new StringReader[2];
            DataSet ds = new DataSet();
            //
            try
            {

                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPantallasProceso", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Connection Configuration
                queryString = new StringBuilder();
                queryString.Append("HHVObtenerPantallasProceso");

                // Parameters
                int index = 0;
                pars = new SqlParameter[1];
                pars[index] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[index].Value = iCodProceso;

                // Query Execution
                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ObtenerPantallasProceso: " + ex.Message);
            }
            return xmlDS;
        }
        #endregion ObtenerPantallasProceso
        #region InsertarConfigHandHeld
        public long InsertarConfigHandHeld(int iCodUsuario, int iCodOperador, int iCodSupervisor,
                                            DateTime dtFecha, int iCodTurno, int iCodPlanta, int iCodProceso,
                                            int iCodConfigBanco, DateTime dtFechaRegistro)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            long lRes = -1;
            //
            try
            {

                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars += "CodUsuario=" + iCodUsuario.ToString() + ", ";
                sPars += "CodOperador=" + iCodOperador.ToString() + ", ";
                sPars += "CodSupervisor=" + iCodSupervisor.ToString() + ", ";
                sPars += "Fecha=" + dtFecha.ToString() + ", ";
                sPars += "CodTurno=" + iCodTurno.ToString() + ", ";
                sPars += "CodPlanta=" + iCodPlanta.ToString() + ", ";
                sPars += "CodProceso=" + iCodProceso.ToString() + ", ";
                sPars += "ConfigBanco=" + iCodConfigBanco.ToString() + ", ";
                sPars += "FechaRegistro=" + dtFechaRegistro.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("InsertarConfigHandHeld", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTInsertarConfigHandHeld");

                // Parameters
                int index = 0;
                pars = new SqlParameter[10];
                pars[index] = new SqlParameter("@CodUsuario", SqlDbType.Int);
                pars[index++].Value = iCodUsuario;
                pars[index] = new SqlParameter("@CodOperador", SqlDbType.Int);
                pars[index++].Value = iCodOperador;
                pars[index] = new SqlParameter("@CodSupervisor", SqlDbType.Int);
                pars[index++].Value = iCodSupervisor;
                pars[index] = new SqlParameter("@Fecha", SqlDbType.DateTime);
                pars[index++].Value = dtFecha;
                pars[index] = new SqlParameter("@CodTurno", SqlDbType.Int);
                pars[index++].Value = iCodTurno;
                pars[index] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[index++].Value = iCodPlanta;
                pars[index] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[index++].Value = iCodProceso;
                pars[index] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                pars[index++].Value = iCodConfigBanco;
                pars[index] = new SqlParameter("@FechaRegistro", SqlDbType.DateTime);
                pars[index++].Value = dtFechaRegistro;
                pars[index] = new SqlParameter("@CodConfigHandHeld", SqlDbType.BigInt);
                pars[index].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                lRes = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", InsertarConfigHandHeld: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return lRes;
        }
        #endregion InsertarConfigHandHeld
        #region ExisteInventarioProcesoActivo
        public int ExisteInventarioProcesoActivo()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars += "";
                lCodRegistro = this.InsertarRegistroSolicitud("ExisteInventarioProcesoActivo", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("spIsInventarioExists");

                // Parameters
                int index = 0;
                pars = new SqlParameter[1];
                pars[index] = new SqlParameter("@IdInventarioProceso", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ExisteInventarioProcesoActivo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion ExisteInventarioProcesoActivo

        // Varios - Captura Inicial
        #region ObtenerClaveEmpleadoMFG
        public int ObtenerClaveEmpleadoMFG(int empleado)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHVObtenerClaveEmpleadoMFG";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodEmpleado=" + empleado;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@CodEmpleado", SqlDbType.Int);
                pars[index++].Value = empleado;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        #endregion ObtenerClaveEmpleadoMFG
        #region ObtenerSupervisorPorDefecto
        public StringReader[] ObtenerSupervisorPorDefecto(int empleado)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerSupervisorPorDefecto";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@CodUsuario", SqlDbType.Int);
                parameters[i++].Value = empleado;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerSupervisorPorDefecto
        #region ValidarEmpleadoMFG
        public int ValidarEmpleadoMFG(int empleadoMFG)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHVValidarEmpleadoMFG";
            try
            {
                string sPars = string.Empty;
                sPars += "@ClaveEmpleadoMFG=" + empleadoMFG;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@ClaveEmpleadoMFG", SqlDbType.Int);
                pars[index++].Value = empleadoMFG;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        #endregion ValidarEmpleadoMFG
        #region ObtenerCentrosTrabajo
        public StringReader[] ObtenerCentrosTrabajo(int planta, int proceso)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerCentrosTrabajo";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                parameters[i++].Value = planta;
                parameters[i] = new SqlParameter("@CodProceso", SqlDbType.Int);
                parameters[i++].Value = proceso;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerCentrosTrabajo
        #region ObtenerMaquinas
        public StringReader[] ObtenerMaquinas(int planta, int proceso, int centroTrabajo)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerMaquinas";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[i] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                parameters[i++].Value = planta;
                parameters[i] = new SqlParameter("@CodProceso", SqlDbType.Int);
                parameters[i++].Value = proceso;
                parameters[i] = new SqlParameter("@CodCentroTrabajo", SqlDbType.Int);
                parameters[i++].Value = centroTrabajo;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerMaquinas
        #region ObtenerPiezaCarroHornos
        public StringReader[] ObtenerPiezaCarroHornos(int planta, int carro)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerPiezaCarroHornos";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@Planta", SqlDbType.Int);
                parameters[i++].Value = planta;
                parameters[i] = new SqlParameter("@Carro", SqlDbType.Int);
                parameters[i++].Value = carro;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion
        #region ObtenerProcesosPorRol
        public StringReader[] ObtenerProcesosPorRol(int rol)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerProcesosPorRol";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@Rol", SqlDbType.Int);
                parameters[i++].Value = rol;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion

        // Vaciado - Captura Vaciado
        #region ObtenerNumPosicionesBanco
        public int ObtenerNumPosicionesBanco(int configBanco)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHVObtenerNumPosicionesBanco";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodConfigBanco=" + configBanco;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                pars[index++].Value = configBanco;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        #endregion ObtenerNumPosicionesBanco
        #region SuperoLimiteVaciadas
        public Boolean SuperoLimiteVaciadas(int configBanco)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            Boolean res = false;
            String nameSP = "HHVSuperoLimiteVaciadas";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodConfigBanco=" + configBanco;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                pars[index++].Value = configBanco;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
                res = lCodRegistro == 1 ? true : false;
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return res;
        }
        #endregion SuperoLimiteVaciadas

        #region ObtenerPosicionesBanco
        public StringReader[] ObtenerPosicionesBanco(int configBanco)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerPosicionesBanco";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                parameters[i++].Value = configBanco;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerPosicionesBanco
        #region ObtenerArticulosMolde
        public StringReader[] ObtenerArticulosMolde(int molde)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerArticulosMolde";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@CodMolde", SqlDbType.Int);
                parameters[i++].Value = molde;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerArticulosMolde
        #region ObtenerColores
        public StringReader[] ObtenerColores()
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerColores";
                SqlParameter[] parameters = new SqlParameter[0];
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerColores
        #region ObtenerColor
        private DataTable ObtenerColor()
        {
            DataTable dt = null;
            try
            {
                String SPName = "HHColorSel";
                SqlParameter[] parameters = new SqlParameter[0];
                dt = this.ObtenerRespuestaDT(SPName, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        #endregion ObtenerColor
        #region ObtenerArticulo
        private DataTable ObtenerArticulo()
        {
            DataTable dt = null;
            try
            {
                String SPName = "HHArticuloSel";
                SqlParameter[] parameters = new SqlParameter[0];
                dt = this.ObtenerRespuestaDT(SPName, parameters);
            }
            catch (Exception)
            {
                throw;
            }
            return dt;
        }
        #endregion ObtenerArticulo
        #region ObtenerPruebas
        public StringReader[] ObtenerPruebas(int planta, int proceso)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerPruebas";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                parameters[i++].Value = planta;
                parameters[i] = new SqlParameter("@CodProceso", SqlDbType.Int);
                parameters[i++].Value = proceso;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerPruebas
        #region ActualizarVaciadasAcumuladas
        public int ActualizarVaciadasAcumuladas(int iCodConfigBanco)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {
                // Insertar Registro Solicitud
                string sPars = string.Empty;
                sPars += "CodConfigBanco=" + iCodConfigBanco.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ActualizarVaciadasAcumuladas", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTActualizarVaciadasAcumuladas");

                // Parameters
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                pars[index++].Value = iCodConfigBanco;
                pars[index] = new SqlParameter("@Res", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ActualizarVaciadasAcumuladas: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion ActualizarVaciadasAcumuladas
        #region ActualizarVaciadasAcumuladas2
        public int ActualizarVaciadasAcumuladas2(int iCodConfigBanco, int cant)
        {
            long res = -1;
            try
            {
                String SPName = "HHTActualizarVaciadasAcumuladas2";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[i] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                parameters[i++].Value = iCodConfigBanco;
                parameters[i] = new SqlParameter("@Cant", SqlDbType.Int);
                parameters[i++].Value = iCodConfigBanco;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ActualizarVaciadasAcumuladas2
        // Vaciado - Armado Carro Secador


        // Secado - Entrada Carro Secador


        // Esmaltado - Captura Esmaltado


        // Hornos - Captura Hornos


        //  Empaque - Captura Empaque
        #region ActualizarModeloPieza
        public int ActualizarModeloPieza(int pieza, int modelo)
        {
            long res = -1;
            try
            {
                String SPName = "HHTActualizarModeloPieza";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[i] = new SqlParameter("@Pieza", SqlDbType.Int);
                parameters[i++].Value = pieza;
                parameters[i] = new SqlParameter("@Modelo", SqlDbType.Int);
                parameters[i++].Value = modelo;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ActualizarModeloPieza
        #region ActualizarCalidadPieza
        public int ActualizarCalidadPieza(int pieza, int calidad)
        {
            long res = -1;
            try
            {
                String SPName = "HHTActualizarCalidadPieza";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[i] = new SqlParameter("@Pieza", SqlDbType.Int);
                parameters[i++].Value = pieza;
                parameters[i] = new SqlParameter("@Calidad", SqlDbType.Int);
                parameters[i++].Value = calidad;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ActualizarCalidadPieza

        // Auditoria - Captura Auditoria
        #region ExistePiezaEnTarima
        public int ExistePiezaEnTarima(int iPieza)
        {
            long res = -1;
            try
            {
                String SPName = "HHVExistePiezaEnTarima";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@Pieza", SqlDbType.Int);
                parameters[i++].Value = iPieza;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ExistePiezaEnTarima
        #region InsertarTarimaPieza
        public int InsertarTarimaPieza(int iTarima, int iPieza, Boolean iPaletizado, Boolean iRechazado, DateTime FechaRegistro)
        {
            long res = -1;
            try
            {
                int bPaletizado = iPaletizado ? 1 : 0;
                int bRechazado = iRechazado ? 1 : 0;
                String SPName = "HHTInsertarTarimaPieza";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[6];
                parameters[i] = new SqlParameter("@Tarima", SqlDbType.Int);
                parameters[i++].Value = iTarima;
                parameters[i] = new SqlParameter("@Pieza", SqlDbType.Int);
                parameters[i++].Value = iPieza;
                parameters[i] = new SqlParameter("@Paletizado", SqlDbType.Int);
                parameters[i++].Value = bPaletizado;
                parameters[i] = new SqlParameter("@Rechazado", SqlDbType.Int);
                parameters[i++].Value = bRechazado;
                parameters[i] = new SqlParameter("@FechaRegistro", SqlDbType.DateTime);
                parameters[i++].Value = FechaRegistro;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion InsertarTarimaPieza

        // Inventario - Captura Inventario


        // Reemplazo Etiqueta - Reemplazo Etiqueta


        // Defectos - Defectos


        // Varios


        // Sincronizacion


        // -------------------------------

        // Validaciones
        #region ObtenerCodPiezaCodBarras
        public int ObtenerCodPiezaCodBarras(string sCodBarras)
        {
            long res = -1;
            try
            {
                String SPName = "HHVObtenerCodPiezaCodBarras";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@CodBarras", SqlDbType.NVarChar, 15);
                parameters[i++].Value = sCodBarras;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ObtenerCodPiezaCodBarras
        #region EstaEnInventarioPocesoPieza
        public int EstaEnInventarioPocesoPieza(string sCodBarras)
        {
            long res = -1;
            try
            {
                String SPName = "HHVEstaEnInventarioPocesoPieza";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@CodBarras", SqlDbType.NVarChar, 15);
                parameters[i++].Value = sCodBarras;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion
        #region ObtenerEstadoPieza
        public SolutionEntityList<BE.HHEstadoPieza> ObtenerEstadoPieza(int iCodPieza)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.HHEstadoPieza> l_Res = new SolutionEntityList<BE.HHEstadoPieza>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "CodPieza=" + iCodPieza.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerEstadoPieza", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #region Query

                queryString = new StringBuilder();
                queryString.Append("HHVObtenerEstadoPieza");

                #endregion Query

                // Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;

                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                // Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.HHEstadoPieza(dr));
                }

            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ObtenerEstadoPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerEstadoPieza
        #region ObtenerUltimoProcesoPieza
        public SolutionEntityList<BE.HHProceso> ObtenerUltimoProcesoPieza(int iCodPieza)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.HHProceso> l_Res = new SolutionEntityList<BE.HHProceso>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "CodPieza=" + iCodPieza.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerUltimoProcesoPieza", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #region Query

                queryString = new StringBuilder();
                queryString.Append("HHVObtenerUltimoProcesoPieza");

                #endregion Query

                // Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;

                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                // Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.HHProceso(dr));
                }

            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ObtenerUltimoProcesoPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerUltimoProcesoPieza
        #region ObtenerPiezasCarro
        public SolutionEntityList<BE.HHPieza> ObtenerPiezasCarro(int iCodPlanta, int iCodProceso, int iCodCarro)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.HHPieza> l_Res = new SolutionEntityList<BE.HHPieza>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "CodPlanta=" + iCodPlanta.ToString() + ", ";
                sPars += "CodProceso=" + iCodProceso.ToString() + ", ";
                sPars += "CodCarro=" + iCodCarro.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPiezasCarro", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #region Query

                queryString = new StringBuilder();
                queryString.Append("HHVObtenerPiezasCarro");

                #endregion Query

                // Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlParameter("@CodCarro", SqlDbType.Int);
                pars[2].Value = iCodCarro;

                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                // Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.HHPieza(dr));
                }

            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ObtenerPiezasCarro: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerPiezasCarro
        #region ObtenerCodModeloPieza
        public int ObtenerCodModeloPieza(int iCodPieza)
        {
            long res = -1;
            try
            {
                String SPName = "HHVObtenerCodModeloPieza";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@CodPieza", SqlDbType.Int);
                parameters[i++].Value = iCodPieza;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ObtenerCodModeloPieza
        #region ExistePiezaEnCarro
        public int ExistePiezaEnCarro(int iCodPlanta, int iCodProceso, int iCodPieza)
        {
            long res = -1;
            try
            {
                String SPName = "HHVExistePiezaEnCarro";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[4];
                parameters[i] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                parameters[i++].Value = iCodPlanta;
                parameters[i] = new SqlParameter("@CodProceso", SqlDbType.Int);
                parameters[i++].Value = iCodProceso;
                parameters[i] = new SqlParameter("@CodPieza", SqlDbType.Int);
                parameters[i++].Value = iCodPieza;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ExistePiezaEnCarro
        #region ObtenerCalidadPieza
        public SolutionEntityList<BE.HHCalidad> ObtenerCalidadPieza(int iCodPieza)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.HHCalidad> l_Res = new SolutionEntityList<BE.HHCalidad>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "CodPieza=" + iCodPieza.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCalidadPieza", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #region Query

                queryString = new StringBuilder();
                queryString.Append("HHVObtenerCalidadPieza");

                #endregion Query

                // Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;

                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                // Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.HHCalidad(dr));
                }

            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ObtenerCalidadPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerCalidadPieza
        #region ObtenerCodMoldePieza
        public int ObtenerCodMoldePieza(int iCodPieza)
        {
            long res = -1;
            try
            {
                String SPName = "HHVObtenerCodMoldePieza";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@CodPieza", SqlDbType.Int);
                parameters[i++].Value = iCodPieza;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ObtenerCodMoldePieza
        #region ObtenerDefectosPiezaProceso
        public SolutionEntityList<BE.HHDefecto> ObtenerDefectosPiezaProceso(int iCodPieza, int iCodProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.HHDefecto> l_Res = new SolutionEntityList<BE.HHDefecto>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "CodProceso=" + iCodProceso.ToString() + ", ";
                sPars += "CodPieza=" + iCodPieza.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerDefectosPiezaProceso", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #region Query

                queryString = new StringBuilder();
                queryString.Append("HHVObtenerDefectosPiezaProceso");

                #endregion Query

                // Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iCodProceso;
                pars[1] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[1].Value = iCodPieza;

                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                // Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.HHDefecto(dr));
                }

            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ObtenerDefectosPiezaProceso: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerDefectosPiezaProceso

        // Transacciones
        #region InsertarPieza
        public int InsertarPieza(int iCodPlanta, string sCodBarras, int iCodConfigBanco, int iCodConsecutivo,
                                    int iPosicion, int iCodArticulo, int iCodUltimoProceso, int iCodUltimoEstado, DateTime fechaRegistro, int iMolde, int iBase)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodPlanta=" + iCodPlanta.ToString() + ", ";
                sPars += "CodBarras=" + sCodBarras + ", ";
                sPars += "CodConfigBanco=" + iCodConfigBanco.ToString() + ", ";
                sPars += "CodConsecutivo=" + iCodConsecutivo.ToString() + ", ";
                sPars += "Posicion=" + iPosicion.ToString() + ", ";
                sPars += "CodArticulo=" + iCodArticulo.ToString() + ", ";
                sPars += "CodUltimoProceso=" + iCodUltimoProceso.ToString() + ", ";
                sPars += "CodUltimoEstado=" + iCodUltimoEstado.ToString() + ", ";
                sPars += "FechaRegistro=" + fechaRegistro;
                sPars += "Molde=" + iMolde;
                sPars += "Base=" + iBase;
                lCodRegistro = this.InsertarRegistroSolicitud("InsertarPieza", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTInsertarPieza");

                // Parameters
                pars = new SqlParameter[12];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlParameter("@CodBarras", SqlDbType.NVarChar, 15);
                pars[1].Value = sCodBarras;
                pars[2] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                pars[2].Value = iCodConfigBanco;
                pars[3] = new SqlParameter("@CodConsecutivo", SqlDbType.Int);
                pars[3].Value = iCodConsecutivo;
                pars[4] = new SqlParameter("@Posicion", SqlDbType.Int);
                pars[4].Value = iPosicion;
                pars[5] = new SqlParameter("@CodArticulo", SqlDbType.Int);
                pars[5].Value = iCodArticulo;
                pars[6] = new SqlParameter("@CodUltimoProceso", SqlDbType.Int);
                pars[6].Value = iCodUltimoProceso;
                pars[7] = new SqlParameter("@CodUltimoEstado", SqlDbType.Int);
                pars[7].Value = iCodUltimoEstado;
                pars[8] = new SqlParameter("@FechaRegistro", SqlDbType.DateTime);
                pars[8].Value = fechaRegistro;
                pars[9] = new SqlParameter("@CodMolde", SqlDbType.Int);
                pars[9].Value = iMolde;
                pars[10] = new SqlParameter("@IdBase", SqlDbType.Int);
                pars[10].Value = iBase;
                pars[11] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[11].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[11].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", InsertarPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion InsertarPieza
        #region InsertarPiezaTransaccion
        public long InsertarPiezaTransaccion(long lCodConfigHandheld, int iCodPieza, DateTime dtFechaRegistro)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            long lRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodConfigHandheld=" + lCodConfigHandheld.ToString() + ", ";
                sPars += "CodPieza=" + iCodPieza.ToString() + ", ";
                sPars += "FechaRegistro=" + dtFechaRegistro.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("InsertarPiezaTransaccion", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTInsertarPiezaTransaccion");

                // Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@CodConfigHandheld", SqlDbType.BigInt);
                pars[0].Value = lCodConfigHandheld;
                pars[1] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[1].Value = iCodPieza;
                pars[2] = new SqlParameter("@FechaRegistro", SqlDbType.DateTime);
                pars[2].Value = dtFechaRegistro;
                pars[3] = new SqlParameter("@CodPiezaTransaccion", SqlDbType.BigInt);
                pars[3].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                lRes = Convert.ToInt64(pars[3].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", InsertarPiezaTransaccion: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return lRes;
        }
        #endregion InsertarPiezaTransaccion
        #region ActulizarUltimoProcesoPieza
        public int ActulizarUltimoProcesoPieza(int iCodPieza, int iCodUltimoProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "iCodPieza=" + iCodPieza.ToString() + ", ";
                sPars += "CodUltimoProceso=" + iCodUltimoProceso.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ActulizarUltimoProcesoPieza", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTActulizarUltimoProcesoPieza");

                // Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;
                pars[1] = new SqlParameter("@CodUltimoProceso", SqlDbType.Int);
                pars[1].Value = iCodUltimoProceso;
                pars[2] = new SqlParameter("@Res", SqlDbType.Int);
                pars[2].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ActulizarUltimoProcesoPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion ActulizarUltimoProcesoPieza
        #region EliminarCarro
        public int EliminarCarro(int iCodPlanta, int iCodProceso, int iCodCarro)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodPlanta=" + iCodPlanta.ToString() + ", ";
                sPars += "CodProceso=" + iCodProceso.ToString() + ", ";
                sPars += "CodCarro=" + iCodCarro.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("EliminarCarro", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTEliminarCarro");

                // Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlParameter("@CodCarro", SqlDbType.Int);
                pars[2].Value = iCodCarro;
                pars[3] = new SqlParameter("@Res", SqlDbType.Int);
                pars[3].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", EliminarCarro: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion EliminarCarro
        #region ActualizarConfigHandHeld
        public int ActualizarConfigHandHeld(long lCodConfigHandHeld, int iCodSupervisor, int iCodOperador, int iCodConfigBanco)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodConfigHandHeld=" + lCodConfigHandHeld.ToString() + ", ";
                sPars += "CodSupervisor=" + iCodSupervisor.ToString() + ", ";
                sPars += "CodOperador=" + iCodOperador.ToString() + ", ";
                sPars += "CodConfigBanco=" + iCodConfigBanco.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ActualizarConfigHandHeld", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTActualizarConfigHandHeld");

                // Parameters
                pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@CodConfigHandHeld", SqlDbType.BigInt);
                pars[0].Value = lCodConfigHandHeld;
                pars[1] = new SqlParameter("@CodSupervisor", SqlDbType.Int);
                pars[1].Value = iCodSupervisor;
                pars[2] = new SqlParameter("@CodOperador", SqlDbType.Int);
                pars[2].Value = iCodOperador;
                pars[3] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                pars[3].Value = iCodConfigBanco;
                pars[4] = new SqlParameter("@Res", SqlDbType.Int);
                pars[4].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[4].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ActualizarConfigHandHeld: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion ActualizarConfigHandHeld
        #region InsertarCarroPieza
        public int InsertarCarroPieza(int iCodPlanta, int iCodProceso, int iCodCarro, int iCodPieza, DateTime dFechaRegistro)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodPlanta=" + iCodPlanta.ToString() + ", ";
                sPars += "CodProceso=" + iCodProceso.ToString() + ", ";
                sPars += "CodCarro=" + iCodCarro.ToString() + ", ";
                sPars += "CodPieza=" + iCodPieza.ToString() + ", ";
                sPars += "FechaRegistro=" + dFechaRegistro.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("InsertarCarroPieza", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTInsertarCarroPieza");

                // Parameters
                pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlParameter("@CodCarro", SqlDbType.Int);
                pars[2].Value = iCodCarro;
                pars[3] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[3].Value = iCodPieza;
                pars[4] = new SqlParameter("@FechaRegistro", SqlDbType.DateTime);
                pars[4].Value = dFechaRegistro;
                pars[5] = new SqlParameter("@Res", SqlDbType.BigInt);
                pars[5].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[5].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", InsertarCarroPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion InsertarCarroPieza
        #region InsertarPiezaTransaccionSecador
        public int InsertarPiezaTransaccionSecador(long lCodPiezaTransaccion, DateTime dtHoraInicio, double dHorasSecado)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodPiezaTransaccion=" + lCodPiezaTransaccion.ToString() + ", ";
                sPars += "HoraInicio=" + dtHoraInicio.ToString() + ", ";
                sPars += "HorasSecado=" + dHorasSecado.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("InsertarPiezaTransaccionSecador", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTInsertarPiezaTransaccionSecador");

                // Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@CodPiezaTransaccion", SqlDbType.BigInt);
                pars[0].Value = lCodPiezaTransaccion;
                pars[1] = new SqlParameter("@HoraInicio", SqlDbType.DateTime);
                pars[1].Value = dtHoraInicio;
                pars[2] = new SqlParameter("@HorasSecado", SqlDbType.Float);
                pars[2].Value = dHorasSecado;
                pars[3] = new SqlParameter("@Res", SqlDbType.Int);
                pars[3].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[3].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", InsertarPiezaTransaccionSecador: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion InsertarPiezaTransaccionSecador
        #region ActualizarColorPieza
        public int ActualizarColorPieza(int iCodPieza, int iCodColor)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodPieza=" + iCodPieza.ToString() + ", ";
                sPars += "CodColor=" + iCodColor.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ActualizarColorPieza", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTActualizarColorPieza");

                // Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;
                pars[1] = new SqlParameter("@CodColor", SqlDbType.Int);
                pars[1].Value = iCodColor;
                pars[2] = new SqlParameter("@Res", SqlDbType.Int);
                pars[2].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ActualizarColorPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion ActualizarColorPieza
        #region InsertarCarroZonaPieza
        public int InsertarCarroZonaPieza(int iCodPlanta, int iCodPieza, int iCodCarro, string sCodZona)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodPlanta=" + iCodPlanta.ToString() + ", ";
                sPars += "CodPieza=" + iCodPieza.ToString() + ", ";
                sPars += "CodCarro=" + iCodCarro.ToString() + ", ";
                sPars += "CodZona=" + sCodZona;
                lCodRegistro = this.InsertarRegistroSolicitud("InsertarCarroZonaPieza", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTInsertarCarroZonaPieza");

                // Parameters
                pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[1].Value = iCodPieza;
                pars[2] = new SqlParameter("@CodCarro", SqlDbType.Int);
                pars[2].Value = iCodCarro;
                pars[3] = new SqlParameter("@CodZona", SqlDbType.NVarChar, 15);
                pars[3].Value = sCodZona;
                pars[4] = new SqlParameter("@Res", SqlDbType.Int);
                pars[4].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[4].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", InsertarCarroZonaPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion InsertarCarroZonaPieza
        #region InsertarPiezaDefecto
        public int InsertarPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto,
                                            int iCodEstadoDefecto, int iCodEmpleado, DateTime dtFechaUltimoMovimiento, DateTime dtFechaRegistro)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodPieza=" + iCodPieza.ToString() + ", ";
                sPars += "CodProceso=" + iCodProceso.ToString() + ", ";
                sPars += "CodDefecto=" + iCodDefecto.ToString() + ", ";
                sPars += "CodZonaDefecto=" + iCodZonaDefecto.ToString() + ", ";
                sPars += "CodEstadoDefecto=" + iCodEstadoDefecto.ToString() + ", ";
                sPars += "CodEmpleado=" + iCodEmpleado.ToString() + ", ";
                sPars += "FechaUltimoMovimiento=" + dtFechaUltimoMovimiento.ToString() + ", ";
                sPars += "FechaRegistro=" + dtFechaRegistro.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("InsertarPiezaDefecto", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTInsertarPiezaDefecto");

                // Parameters
                pars = new SqlParameter[9];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;
                pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlParameter("@CodDefecto", SqlDbType.Int);
                pars[2].Value = iCodDefecto;
                pars[3] = new SqlParameter("@CodZonaDefecto", SqlDbType.Int);
                pars[3].Value = iCodZonaDefecto;
                pars[4] = new SqlParameter("@CodEstadoDefecto", SqlDbType.Int);
                pars[4].Value = iCodEstadoDefecto;
                pars[5] = new SqlParameter("@CodEmpleado", SqlDbType.Int);
                pars[5].Value = iCodEmpleado;
                pars[6] = new SqlParameter("@FechaUltimoMovimiento", SqlDbType.DateTime);
                pars[6].Value = dtFechaUltimoMovimiento;
                pars[7] = new SqlParameter("@FechaRegistro", SqlDbType.DateTime);
                pars[7].Value = dtFechaRegistro;
                pars[8] = new SqlParameter("@Res", SqlDbType.Int);
                pars[8].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[8].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", InsertarPiezaDefecto: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion InsertarPiezaDefecto
        #region EliminarPiezaDefecto
        public int EliminarPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodPieza=" + iCodPieza.ToString() + ", ";
                sPars += "CodProceso=" + iCodProceso.ToString() + ", ";
                sPars += "CodDefecto=" + iCodDefecto.ToString() + ", ";
                sPars += "CodZonaDefecto=" + iCodZonaDefecto.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("EliminarPiezaDefecto", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTEliminarPiezaDefecto");

                // Parameters
                pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;
                pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlParameter("@CodDefecto", SqlDbType.Int);
                pars[2].Value = iCodDefecto;
                pars[3] = new SqlParameter("@CodZonaDefecto", SqlDbType.Int);
                pars[3].Value = iCodZonaDefecto;
                pars[4] = new SqlParameter("@Res", SqlDbType.Int);
                pars[4].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[4].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", EliminarPiezaDefecto: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion EliminarPiezaDefecto
        #region ActualizarPiezaDefecto
        public int ActualizarPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto,
                                            int iCodEstadoDefecto, int iCodEmpleado, DateTime dtFechaUltimoMovimiento)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "CodPieza=" + iCodPieza.ToString() + ", ";
                sPars += "CodProceso=" + iCodProceso.ToString() + ", ";
                sPars += "CodDefecto=" + iCodDefecto.ToString() + ", ";
                sPars += "CodZonaDefecto=" + iCodZonaDefecto.ToString() + ", ";
                sPars += "CodEstadoDefecto=" + iCodEstadoDefecto.ToString() + ", ";
                sPars += "CodEmpleado=" + iCodEmpleado.ToString() + ", ";
                sPars += "FechaUltimoMovimiento=" + dtFechaUltimoMovimiento.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ActualizarPiezaDefecto", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTActualizarPiezaDefecto");

                // Parameters
                pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;
                pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlParameter("@CodDefecto", SqlDbType.Int);
                pars[2].Value = iCodDefecto;
                pars[3] = new SqlParameter("@CodZonaDefecto", SqlDbType.Int);
                pars[3].Value = iCodZonaDefecto;
                pars[4] = new SqlParameter("@CodEstadoDefecto", SqlDbType.Int);
                pars[4].Value = iCodEstadoDefecto;
                pars[5] = new SqlParameter("@CodEmpleado", SqlDbType.Int);
                pars[5].Value = iCodEmpleado;
                pars[6] = new SqlParameter("@FechaUltimoMovimiento", SqlDbType.DateTime);
                pars[6].Value = dtFechaUltimoMovimiento;
                pars[7] = new SqlParameter("@Res", SqlDbType.Int);
                pars[7].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[7].Value);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ActualizarPiezaDefecto: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion ActualizarPiezaDefecto
        #region ActualizarPiezaUltimoEstado
        public int ActualizarPiezaUltimoEstado(int iCodPieza, int iCodUltimoEstado)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {

                // Insertar Registro Solicitud
                string sPars = string.Empty;
                sPars += "CodPieza=" + iCodPieza.ToString() + ", ";
                sPars += "CodUltimoEstado=" + iCodUltimoEstado.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ActualizarPiezaUltimoEstado", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTActualizarPiezaUltimoEstado");

                // Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;
                pars[1] = new SqlParameter("@CodUltimoEstado", SqlDbType.Int);
                pars[1].Value = iCodUltimoEstado;
                pars[2] = new SqlParameter("@Res", SqlDbType.Int);
                pars[2].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[2].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ActualizarPiezaUltimoEstado: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion ActualizarPiezaUltimoEstado

        // Auditoria
        #region ActualizarPiezaAuditada
        public int ActualizarPiezaAuditada(int iCodPieza, bool bAuditada)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {
                // Insertar Registro Solicitud
                string sPars = string.Empty;
                sPars += "CodPieza=" + iCodPieza.ToString() + ", ";
                sPars += "Auditada=" + bAuditada.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ActualizarPiezaAuditada", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("spPiezaActualizarAuditoria");

                // Parameters
                int index = 0;
                pars = new SqlParameter[3];
                pars[index] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[index++].Value = iCodPieza;
                pars[index] = new SqlParameter("@Auditada", SqlDbType.Bit);
                pars[index++].Value = bAuditada;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ActualizarPiezaAuditada: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion ActualizarPiezaAuditada
        #region ObtenerPiezasTarima
        public SolutionEntityList<BE.HHTarimaPieza> ObtenerPiezasTarima(int iCodTarima)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.HHTarimaPieza> l_Res = new SolutionEntityList<BE.HHTarimaPieza>();
            //
            try
            {

                // Insertar Registro Solicitud
                string sPars = "CodTarima=" + iCodTarima.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPiezasTarima", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHVObtenerPiezasTarima");

                // Parameters
                int index = 0;
                pars = new SqlParameter[1];
                pars[index] = new SqlParameter("@CodTarima", SqlDbType.Int);
                pars[index].Value = iCodTarima;

                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                // Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.HHTarimaPieza(dr));
                }

            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ObtenerPiezasTarima: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerPiezasTarima
        #region RechazarTarimaPieza
        public int RechazarTarimaPieza(int iCodTarima)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {
                // Insertar Registro Solicitud
                string sPars = string.Empty;
                sPars += "CodTarima=" + iCodTarima.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("RechazarTarimaPieza", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTRechazarTarimaPieza");

                // Parameters
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[index++].Value = iCodTarima;
                pars[index] = new SqlParameter("@Res", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", RechazarTarimaPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion RechazarTarimaPieza
        #region ActualizarTarimaPaletizado
        public int ActualizarTarimaPaletizado(int iCodTarima, bool bPaletizado)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {
                // Insertar Registro Solicitud
                string sPars = string.Empty;
                sPars += "CodTarima=" + iCodTarima.ToString() + ", ";
                sPars += "Paletizado=" + bPaletizado.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ActualizarTarimaPaletizado", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTActualizarTarimaPaletizado");

                // Parameters
                int index = 0;
                pars = new SqlParameter[3];
                pars[index] = new SqlParameter("@CodTarima", SqlDbType.Int);
                pars[index++].Value = iCodTarima;
                pars[index] = new SqlParameter("@Paletizado", SqlDbType.Bit);
                pars[index++].Value = bPaletizado;
                pars[index] = new SqlParameter("@Res", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ActualizarTarimaPaletizado: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion ActualizarTarimaPaletizado

        // Reemplazo de Etiqueta
        #region InsertarEtiquetaReemplazo
        public int InsertarEtiquetaReemplazo(int iCodPlanta, string sCodBarras, int iCodModelo, int iCodColor,
                                                int iCodCalidad, int iCodUltimoProceso, int iCodUltimoEstado,
                                                long lCodConfigHandheld, DateTime dtFechaRegistro, int iCodProcesoPiezaReem)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            int iRes = -1;
            //
            try
            {
                // Insertar Registro Solicitud
                string sPars = string.Empty;
                sPars += "CodPlanta=" + iCodPlanta.ToString() + ", ";
                sPars += "CodBarras=" + sCodBarras + ", ";
                sPars += "CodModelo=" + iCodModelo.ToString() + ", ";
                sPars += "CodColor=" + iCodColor.ToString() + ", ";
                sPars += "CodCalidad=" + iCodCalidad.ToString() + ", ";
                sPars += "CodUltimoProceso=" + iCodUltimoProceso.ToString() + ", ";
                sPars += "CodUltimoEstado=" + iCodUltimoEstado.ToString() + ", ";
                sPars += "CodConfigHandheld=" + lCodConfigHandheld.ToString() + ", ";
                sPars += "FechaRegistro=" + dtFechaRegistro.ToString() + ", ";
                sPars += "CodProcesoPiezaReem=" + iCodProcesoPiezaReem.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("InsertarEtiquetaReemplazo", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTInsertarEtiquetaReemplazo");

                // Parameters
                int index = 0;
                pars = new SqlParameter[12];
                pars[index] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[index++].Value = iCodPlanta;
                pars[index] = new SqlParameter("@CodBarras", SqlDbType.NVarChar, 15);
                pars[index++].Value = sCodBarras;
                pars[index] = new SqlParameter("@CodModelo", SqlDbType.Int);
                pars[index++].Value = iCodModelo;
                pars[index] = new SqlParameter("@CodColor", SqlDbType.Int);
                if (iCodColor == -1) pars[index++].Value = DBNull.Value; else pars[index++].Value = iCodColor;
                pars[index] = new SqlParameter("@CodCalidad", SqlDbType.Int);
                if (iCodCalidad == -1) pars[index++].Value = DBNull.Value; else pars[index++].Value = iCodCalidad;
                pars[index] = new SqlParameter("@CodUltimoProceso", SqlDbType.Int);
                pars[index++].Value = iCodUltimoProceso;
                pars[index] = new SqlParameter("@CodUltimoEstado", SqlDbType.Int);
                pars[index++].Value = iCodUltimoEstado;
                pars[index] = new SqlParameter("@CodConfigHandheld", SqlDbType.BigInt);
                pars[index++].Value = lCodConfigHandheld;
                pars[index] = new SqlParameter("@FechaRegistro", SqlDbType.DateTime);
                pars[index++].Value = dtFechaRegistro;
                pars[index] = new SqlParameter("@CodProcesoPiezaReem", SqlDbType.Int);
                pars[index++].Value = iCodProcesoPiezaReem;
                pars[index] = new SqlParameter("@CodPiezaTransaccion", SqlDbType.BigInt);
                pars[index++].Direction = ParameterDirection.Output;
                pars[index] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                iRes = Convert.ToInt32(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", InsertarEtiquetaReemplazo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        #endregion InsertarEtiquetaReemplazo

        public DateTime ObtenerFechaDepuracionHistoria()
        {

            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            int DiasHistoria = 30;
            try
            {
                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerConfiguracionDiasHistoriaSel");
                #endregion Query
                // Parameters
                pars = new SqlParameter[0];
                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                // Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow row in dtObj.Rows)
                    DiasHistoria = Convert.ToInt32(row["DiasHistoria"]);
                return DateTime.Now.AddDays(-1 * DiasHistoria);
            }
            catch
            {
                return DateTime.Now.AddDays(-1 * DiasHistoria);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
        }

        public DateTime ObtenerFechaDepuracionHistoria(int iCodigoPlanta, int iCodigoProceso)
        {

            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            int DiasHistoria = 30;
            try
            {
                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerConfiguracionDiasHistoriaSel");
                #endregion Query
                // Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodigoPlanta", SqlDbType.Int);
                pars[0].Value = iCodigoPlanta;
                pars[1] = new SqlParameter("@CodigoProceso", SqlDbType.Int);
                pars[1].Value = iCodigoProceso;
                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                // Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow row in dtObj.Rows)
                    DiasHistoria = Convert.ToInt32(row["DiasHistoria"]);
                return DateTime.Now.AddDays(-1 * DiasHistoria);
            }
            catch
            {
                return DateTime.Now.AddDays(-1 * DiasHistoria);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
        }

        // Sincronizacion
        #region Actualizacion de datos
        public StringReader[] ActualizarCatalogos(string tabla, int planta, int proceso, DateTime fecha)
        {
            StringReader[] xmlDS = new StringReader[2];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                ds = new DataSet();
                //Se Cargan toda la informacion de las tablas de catalogos.
                ds = ActualizarDatos(tabla.Replace("_", ""), planta, proceso, fecha);
                ds.Tables[0].TableName = "Ins" + tabla;
                ds.Tables[1].TableName = "Upd" + tabla;
                ds.Tables[2].TableName = "Del" + tabla;
                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", Sync: " + ex.Message);
            }
            return xmlDS;
        }
        public StringReader[] ActualizarTransacciones(string tabla, int planta, int proceso, DateTime fecha)
        {
            StringReader[] xmlDS = new StringReader[2];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                ds = new DataSet();
                //Se Cargan toda la informacion de las tablas de catalogos.
                ds = ActualizarDatos(tabla.Replace("_", ""), planta, proceso, fecha);
                ds.Tables[0].TableName = "Ins" + fecha;
                ds.Tables[1].TableName = "Upd" + fecha;
                ds.Tables[2].TableName = "Del" + fecha;
                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", Sync: " + ex.Message);
            }
            return xmlDS;
        }

        public StringReader[] ActualizarCatalogos(DataTable piezas, string tabla, int proceso)
        {
            StringReader[] xmlDS = new StringReader[2];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                ds = new DataSet();
                //Se Cargan toda la informacion de las tablas de catalogos.
                ds = ActualizarDatos(piezas, tabla.Replace("_", ""), proceso);
                ds.Tables[0].TableName = "Upd" + tabla;
                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", Sync: " + ex.Message);
            }
            return xmlDS;
        }
        private DataSet ActualizarDatos(string tabla, int planta, int proceso, DateTime fecha)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1;
            //
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars += "Tabla=" + tabla + ", ";
                sPars += "Planta=" + planta + ", ";
                sPars += "Proceso=" + proceso + ", ";
                sPars += "Fecha=" + fecha;
                lCodRegistro = this.InsertarRegistroSolicitud("ActualizarDatos", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                String nameSP = "HHSYNC" + tabla + "SEL";

                queryString = new StringBuilder();
                queryString.Append(nameSP);

                #endregion Query

                #region Parameters

                pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@FechaIns", SqlDbType.DateTime);
                pars[0].Value = fecha;
                pars[1] = new SqlParameter("@FechaUpd", SqlDbType.DateTime);
                pars[1].Value = fecha;
                pars[2] = new SqlParameter("@FechaDel", SqlDbType.DateTime);
                pars[2].Value = fecha;
                pars[3] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[3].Value = proceso;
                pars[4] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[4].Value = planta;

                #endregion

                #region Query Execution

                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if(ds != null && ds.Tables.Count > 0 && tabla.ToLower() == "usuario")
                {
                    string sContrasenaDefault = string.Empty;
                    foreach (DataTable tblUsuario in ds.Tables)
                    {
                        if (tblUsuario.Columns.Count == 1) continue;
                        if (string.IsNullOrEmpty(sContrasenaDefault))
                            sContrasenaDefault = ObtenerContrasenaDefault();
                        foreach (DataRow row in tblUsuario.Rows)
                        {
                            if (row["password"].ToString() == sContrasenaDefault)
                                row["password"] = DesencriptarContrasenaUsuario("Lamosa06", sContrasenaDefault);
                            else
                                row["password"] = DesencriptarContrasenaUsuario(row["login"].ToString(), row["password"].ToString());
                        }
                    }
                }
                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", Sync: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return ds;
        }
        private DataSet ActualizarDatos(DataTable piezas, string tabla, int proceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1;
            //
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars += "Tabla=" + tabla + ", ";
                sPars += "Proceso=" + proceso + ", ";
                sPars += "Piezas=" + piezas.Rows.Count.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ActualizarDatos", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration


                #region Query
                String nameSP = "HHSYNCRevision" + tabla + "SEL";

                queryString = new StringBuilder();
                queryString.Append(nameSP);

                #endregion Query



                #region Parameters

                pars = new SqlParameter[2];

                pars[0] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = proceso;
                pars[1] = new SqlParameter("@Piezas", piezas);
                pars[1].Value = piezas;


                #endregion

                #region Query Execution

                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", Sync: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return ds;
        }
        #endregion

        #region Metodos para la Sincronizacion

        public DataSet SyncServHH(String NombreSPTabla, DateTime fechaIns, DateTime fechaUpd, DateTime fechaDel)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "NombreSPTabla=" + NombreSPTabla + ", ";
                sPars += "FechaIns=" + fechaIns + ", ";
                sPars += "FechaUpd=" + fechaUpd + ", ";
                sPars += "FechaDel=" + fechaDel;
                lCodRegistro = this.InsertarRegistroSolicitud("SyncServHH", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                String nameSP = "HHSYNC" + NombreSPTabla + "SEL";

                queryString = new StringBuilder();
                queryString.Append(nameSP);

                #endregion Query

                #region Parameters

                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@FechaIns", SqlDbType.DateTime);
                pars[0].Value = fechaIns;
                pars[1] = new SqlParameter("@FechaUpd", SqlDbType.DateTime);
                pars[1].Value = fechaUpd;
                pars[2] = new SqlParameter("@FechaDel", SqlDbType.DateTime);
                pars[2].Value = fechaDel;

                #endregion

                #region Query Execution

                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", Sync: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return ds;
        }

        public StringReader[] SyncServHHSR(String tableName, DateTime fechaIns, DateTime fechaUpd, DateTime fechaDel)
        {
            StringReader[] xmlDS = new StringReader[2];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                ds = new DataSet();
                //Se Cargan toda la informacion de las tablas de catalogos.
                ds = this.SyncServHH(tableName.Replace("_", ""), fechaIns, fechaUpd, fechaDel);
                ds.Tables[0].TableName = "Ins" + tableName;
                ds.Tables[1].TableName = "Upd" + tableName;
                ds.Tables[2].TableName = "Del" + tableName;
                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", Sync: " + ex.Message);
            }
            return xmlDS;
        }

        #region Carga las tablas de catalogos
        public StringReader[] TablasHH(int planta, int proceso, int pantalla)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "spTablasHH";
                SqlParameter[] parameters = new SqlParameter[3];
                int index = 0;
                parameters[index] = new SqlParameter("@Planta", SqlDbType.Int);
                parameters[index++].Value = planta;
                parameters[index] = new SqlParameter("@Proceso", SqlDbType.Int);
                parameters[index++].Value = proceso;
                parameters[index] = new SqlParameter("@Pantalla", SqlDbType.Int);
                parameters[index++].Value = pantalla;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion Carga las tablas de catalogos
        public List<StringReader[]> SyncServHH(DateTime fechaIns, DateTime fechaUpd, DateTime fechaDel)
        {
            List<StringReader[]> lsr = new List<StringReader[]>();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            try
            {
                dt = this.TablasCatalogos(-1, -1, -1); //Se cargan todas las tablas de Catalogos
                foreach (DataRow dr in dt.Rows)
                {
                    String tableName = dr[0].ToString();
                    ds.Clear();
                    ds = new DataSet();
                    //Se Cargan toda la informacion de las tablas de catalogos.
                    ds = this.SyncServHH(tableName.Replace("_", ""), fechaIns, fechaUpd, fechaDel);
                    ds.Tables[0].TableName = "Ins" + tableName;
                    ds.Tables[1].TableName = "Upd" + tableName;
                    ds.Tables[2].TableName = "Del" + tableName;
                    StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                    StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                    StringReader[] xmlDS = new StringReader[2];
                    xmlDS[0] = xmlSchema;
                    xmlDS[1] = xmlInfo;
                    lsr.Add(xmlDS);
                }
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", Sync: " + ex.Message);
            }
            return lsr;
        }
        public StringReader[] SyncServHHS(String NombreSPTabla, DateTime fechaIns, DateTime fechaUpd, DateTime fechaDel)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            StringReader[] xmlDS = new StringReader[2];
            long lCodRegistro = -1L;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = string.Empty;
                sPars += "NombreSPTabla=" + NombreSPTabla + ", ";
                sPars += "FechaIns=" + fechaIns + ", ";
                sPars += "FechaUpd=" + fechaUpd + ", ";
                sPars += "FechaDel=" + fechaDel;
                lCodRegistro = this.InsertarRegistroSolicitud("SyncServHH", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append(NombreSPTabla);

                #endregion Query

                #region Parameters

                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@FechaIns", SqlDbType.DateTime);
                pars[0].Value = fechaIns;
                pars[1] = new SqlParameter("@FechaUpd", SqlDbType.DateTime);
                pars[1].Value = fechaUpd;
                pars[2] = new SqlParameter("@FechaDel", SqlDbType.DateTime);
                pars[2].Value = fechaDel;

                #endregion

                #region Query Execution

                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());

                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
                #endregion Query Execution
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", InsertarPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return xmlDS;
        }

        public int HHSyncCarroPiezaQuemadoIns(int codPlanta, int codPieza, int codCarro, int codZona)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncCarroPiezaQuemadoIns";
            try
            {
                string sPars = string.Empty;
                sPars += "CodPlanta=" + codPlanta + ", ";
                sPars += "CodCarro=" + codCarro + ", ";
                sPars += "CodPieza=" + codPieza + ", ";
                sPars += "CodZona=" + codZona;
                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);

                pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = codPlanta;
                pars[1] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[1].Value = codPieza;
                pars[2] = new SqlParameter("@CodCarro", SqlDbType.Int);
                pars[2].Value = codCarro;
                pars[3] = new SqlParameter("@CodZona", SqlDbType.Int);
                pars[3].Value = codZona;
                pars[4] = new SqlParameter("@CodIns", SqlDbType.Int);
                pars[4].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[4].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int HHSyncCarroPiezaIns(int codPlanta, int codProceso, int codPieza, int codCarro, int codZona)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncCarroPiezaIns";
            try
            {
                string sPars = string.Empty;
                sPars += "CodPlanta=" + codPlanta + ", ";
                sPars += "CodProceso=" + codPlanta + ", ";
                sPars += "CodCarro=" + codCarro + ", ";
                sPars += "CodPieza=" + codPieza + ", ";
                sPars += "CodZona=" + codZona;
                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);

                pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = codPlanta;
                pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = codProceso;
                pars[2] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[2].Value = codPieza;
                pars[3] = new SqlParameter("@CodCarro", SqlDbType.Int);
                pars[3].Value = codCarro;
                pars[4] = new SqlParameter("@CodZona", SqlDbType.Int);
                pars[4].Value = codZona;
                pars[5] = new SqlParameter("@CodIns", SqlDbType.Int);
                pars[5].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[4].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int HHSyncConfigHandHeldIns(int codTurno, int codUsuario, int codSupervisor, int codOperador, int codConfigBanco, int codProceso, int codPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncConfigHandHeldIns";
            try
            {
                string sPars = string.Empty;
                sPars += "CodTurno=" + codTurno + ", ";
                sPars += "CodUsuario=" + codUsuario + ", ";
                sPars += "CodSupervisor=" + codSupervisor + ", ";
                sPars += "CodOperador=" + codOperador + ", ";
                sPars += "CodConfigBanco=" + codConfigBanco + ", ";
                sPars += "CodProceso=" + codProceso + ", ";
                sPars += "codPlanta=" + codPlanta;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[8];
                pars[index] = new SqlParameter("@CodTurno", SqlDbType.Int);
                pars[index++].Value = codTurno;
                pars[index] = new SqlParameter("@CodUsuario", SqlDbType.Int);
                pars[index++].Value = codUsuario;
                pars[index] = new SqlParameter("@CodSupervisor", SqlDbType.Int);
                pars[index++].Value = codSupervisor;
                pars[index] = new SqlParameter("@CodOperador", SqlDbType.Int);
                pars[index++].Value = codOperador;
                pars[index] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                pars[index++].Value = codConfigBanco;
                pars[index] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[index++].Value = codProceso;
                pars[index] = new SqlParameter("@codPlanta", SqlDbType.Int);
                pars[index++].Value = codPlanta;
                pars[index] = new SqlParameter("@CodConfigHH", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int HHSyncPiezaIns(int codBarras, int codConfigBanco, int codConsecutivo, int posicion, int codArticulo, int codColor,
                                    int codCalidad, int codPlanta, int codUltimoEstado, int codUltimoProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncPiezaIns";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodBarras=" + codBarras + ", ";
                sPars += "@CodConfigBanco=" + codConfigBanco + ", ";
                sPars += "@CodConsecutivo=" + codConsecutivo + ", ";
                sPars += "@Posicion=" + posicion + ", ";
                sPars += "@CodArticulo=" + codArticulo + ", ";
                sPars += "@CodColor=" + codColor + ", ";
                sPars += "@CodCalidad=" + codCalidad + ", ";
                sPars += "@CodPlanta=" + codPlanta + ", ";
                sPars += "@CodUltimoEstado=" + codUltimoEstado + ", ";
                sPars += "@CodUltimoProceso=" + codUltimoProceso + ", ";

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[11];
                pars[index] = new SqlParameter("@CodBarras", SqlDbType.Int);
                pars[index++].Value = codBarras;
                pars[index] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                pars[index++].Value = codConfigBanco;
                pars[index] = new SqlParameter("@CodConsecutivo", SqlDbType.Int);
                pars[index++].Value = codConsecutivo;
                pars[index] = new SqlParameter("@Posicion", SqlDbType.Int);
                pars[index++].Value = posicion;
                pars[index] = new SqlParameter("@CodArticulo", SqlDbType.Int);
                pars[index++].Value = codArticulo;
                pars[index] = new SqlParameter("@CodColor", SqlDbType.Int);
                pars[index++].Value = codColor;
                pars[index] = new SqlParameter("@CodCalidad", SqlDbType.Int);
                pars[index++].Value = codCalidad;
                pars[index] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[index++].Value = codPlanta;
                pars[index] = new SqlParameter("@CodUltimoEstado", SqlDbType.Int);
                pars[index++].Value = codUltimoEstado;
                pars[index] = new SqlParameter("@CodUltimoProceso", SqlDbType.Int);
                pars[index++].Value = codUltimoProceso;
                pars[index] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int HHSyncPiezaDefectoIns(int codPieza, int codProceso, int codDefecto, int codZonaDefecto, int codEstadoDefecto,
                                        int codPiezaDefectoDet, int codPiezaDefectoDetalle, int codEmpleado)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncPiezaDefectoIns";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodPieza=" + codPieza + ", ";
                sPars += "@CodProceso=" + codProceso + ", ";
                sPars += "@CodDefecto=" + codDefecto + ", ";
                sPars += "@CodZonaDefecto=" + codZonaDefecto + ", ";
                sPars += "@CodEstadoDefecto=" + codEstadoDefecto + ", ";
                sPars += "@CodPiezaDefectoDet=" + codPiezaDefectoDet + ", ";
                sPars += "@CodPiezaDefectoDetalle=" + codPiezaDefectoDetalle + ", ";
                sPars += "@CodEmpleado=" + codEmpleado;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[9];
                pars[index] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[index++].Value = codPieza;
                pars[index] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[index++].Value = codProceso;
                pars[index] = new SqlParameter("@CodDefecto", SqlDbType.Int);
                pars[index++].Value = codDefecto;
                pars[index] = new SqlParameter("@CodZonaDefecto", SqlDbType.Int);
                pars[index++].Value = codZonaDefecto;
                pars[index] = new SqlParameter("@CodEstadoDefecto", SqlDbType.Int);
                pars[index++].Value = codEstadoDefecto;
                pars[index] = new SqlParameter("@CodPiezaDefectoDet", SqlDbType.Int);
                pars[index++].Value = codPiezaDefectoDet;
                pars[index] = new SqlParameter("@CodPiezaDefectoDetalle", SqlDbType.Int);
                pars[index++].Value = codPiezaDefectoDetalle;
                pars[index] = new SqlParameter("@CodEmpleado", SqlDbType.Int);
                pars[index++].Value = codEmpleado;
                pars[index] = new SqlParameter("@CodIns", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int HHSyncPiezaReemplazoIns(int codPieza, int codProceso, int codPiezaAnterior)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncPiezaReemplazoIns";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodPieza=" + codPieza + ", ";
                sPars += "@CodProceso=" + codProceso + ", ";
                sPars += "@CodPiezaAnterior=" + codPiezaAnterior;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[4];
                pars[index] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[index++].Value = codPieza;
                pars[index] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[index++].Value = codProceso;
                pars[index] = new SqlParameter("@CodPiezaAnterior", SqlDbType.Int);
                pars[index++].Value = codPiezaAnterior;
                pars[index] = new SqlParameter("@CodIns", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int HHSyncPiezaTransaccionIns(int codConfigHH, int CodPieza)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncPiezaTransaccionIns";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodConfigHH=" + codConfigHH + ", ";
                sPars += "@CodPieza=" + CodPieza;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[3];
                pars[index] = new SqlParameter("@CodConfigHH", SqlDbType.Int);
                pars[index++].Value = codConfigHH;
                pars[index] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[index++].Value = CodPieza;
                pars[index] = new SqlParameter("@CodIns", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int HHSyncPiezaTransaccionSecadorIns(int codPiezaTranzaccion, DateTime horaInicio, float horasSecado)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncPiezaTransaccionSecadorIns";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodPiezaTranzaccion=" + codPiezaTranzaccion + ", ";
                sPars += "@HoraInicio=" + horaInicio + ", ";
                sPars += "@HorasSecado=" + codPiezaTranzaccion;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[4];
                pars[index] = new SqlParameter("@CodPiezaTranzaccion", SqlDbType.Int);
                pars[index++].Value = codPiezaTranzaccion;
                pars[index] = new SqlParameter("@HoraInicio", SqlDbType.Date);
                pars[index++].Value = horaInicio;
                pars[index] = new SqlParameter("@HorasSecado", SqlDbType.Float);
                pars[index++].Value = horasSecado;
                pars[index] = new SqlParameter("@CodIns", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }

        public int BloquearUsuario(int codUsuario)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncUsuarioBloquearUpd";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodUsuario=" + codUsuario;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@CodUsuario", SqlDbType.Int);
                pars[index++].Value = codUsuario;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int IncrementarContadorIntentos(int codUsuario)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncUsuarioIncrementarContadorIntentosUpd";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodUsuario=" + codUsuario;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@CodUsuario", SqlDbType.Int);
                pars[index++].Value = codUsuario;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int ReiniciarContadorIntentos(int codUsuario)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "HHSyncUsuarioReiniciarContadorIntentosUpd";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodUsuario=" + codUsuario;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@CodUsuario", SqlDbType.Int);
                pars[index++].Value = codUsuario;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }

        #region TablasCatalogos
        public DataTable TablasCatalogos(int planta, int proceso, int pantalla)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = new DataTable();
            long lCodRegistro = -1L;
            String nameSP = "spTablasHH";
            try
            {
                string sPars = string.Empty;
                sPars += "@Planta=" + planta +
                        "@Proceso" + proceso +
                        "@Pantalla" + pantalla;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);


                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                pars = new SqlParameter[3];
                int index = 0;
                pars[index] = new SqlParameter("@Planta", SqlDbType.Int);
                pars[index++].Value = planta;
                pars[index] = new SqlParameter("@Proceso", SqlDbType.Int);
                pars[index++].Value = proceso;
                pars[index] = new SqlParameter("@Pantalla", SqlDbType.Int);
                pars[index++].Value = pantalla;


                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return dtObj;
        }
        #endregion TablasCatalogos
        #endregion Metodos para la Sincronizacion

        public int TarimaPiezaInsUpd(int codTarima, int codPieza, int paletizado)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "spTarimaPiezaInsUpd";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodTarima=" + codTarima +
                        "@CodPieza" + codPieza +
                        "@Paletizado" + paletizado;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[4];
                pars[index] = new SqlParameter("@CodTarima", SqlDbType.Int);
                pars[index++].Value = codTarima;
                pars[index] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[index++].Value = codPieza;
                pars[index] = new SqlParameter("@Paletizado", SqlDbType.Int);
                pars[index++].Value = paletizado;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }

        public int ObtenerPiezaInventario(int codPieza)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "spInventarioPiezaProceso";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodBarras=" + codPieza;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@CodBarras", SqlDbType.Int);
                pars[index++].Value = codPieza;
                pars[index] = new SqlParameter("@IdProceso", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int ActualizarPiezaInventario(int codPieza)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "spInventarioPiezaAut";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodPieza=" + codPieza;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[2];
                pars[index] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[index++].Value = codPieza;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int PiezaInventarioIns(String barras, int planta, int proceso, int configBanco, int consecutivo,
                                    int posicion, int articulo, int color, int calidad, int ultimoEstado)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "spInventarioPiezaIns";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodBarras=" + barras +
                    "@Planta=" + planta +
                    "@Proceso=" + proceso +
                    "@ConfigBanco=" + configBanco +
                    "@Consecutivo=" + consecutivo +
                    "@Posicion=" + posicion +
                    "@Articulo=" + articulo +
                    "@Color=" + color +
                    "@Calidad=" + calidad +
                    "@UltimoEstado=" + ultimoEstado;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[11];
                pars[index] = new SqlParameter("@CodBarras", SqlDbType.NVarChar, 20);
                pars[index++].Value = barras;
                pars[index] = new SqlParameter("@Planta", SqlDbType.Int);
                pars[index++].Value = planta;
                pars[index] = new SqlParameter("@Proceso", SqlDbType.Int);
                pars[index++].Value = proceso;
                pars[index] = new SqlParameter("@ConfigBanco", SqlDbType.Int);
                pars[index++].Value = configBanco;
                pars[index] = new SqlParameter("@Consecutivo", SqlDbType.Int);
                pars[index++].Value = consecutivo;
                pars[index] = new SqlParameter("@Posicion", SqlDbType.Int);
                pars[index++].Value = posicion;
                pars[index] = new SqlParameter("@Articulo", SqlDbType.Int);
                pars[index++].Value = articulo;
                pars[index] = new SqlParameter("@Color", SqlDbType.Int);
                pars[index++].Value = color;
                pars[index] = new SqlParameter("@Calidad", SqlDbType.Int);
                pars[index++].Value = calidad;
                pars[index] = new SqlParameter("@UltimoEstado", SqlDbType.Int);
                pars[index++].Value = ultimoEstado;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }
        public int InsertaPiezaReemplazo(int codPieza, int codProceso, int codPiezaAnterior)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            String nameSP = "spPiezaReemplazoIns";
            try
            {
                string sPars = string.Empty;
                sPars += "@CodPieza=" + codPieza +
                          "@CodProceso=" + codProceso +
                          "@CodPiezaAnterior=" + codPiezaAnterior;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[4];
                pars[index] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[index++].Value = codPieza;
                pars[index] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[index++].Value = codProceso;
                pars[index] = new SqlParameter("@CodPiezaAnterior", SqlDbType.Int);
                pars[index++].Value = codPiezaAnterior;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return (int)lCodRegistro;
        }

        public Boolean ExisteCambioEnProceso(int proceso, int pantalla, DateTime fechaUltActualizacion)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            Boolean change = false;

            String nameSP = "spExisteCambioEnProceso";
            try
            {
                string sPars = string.Empty;
                sPars += "@Proceso=" + proceso +
                          "@Pantalla=" + pantalla +
                          "@FechaUltActualizacion=" + fechaUltActualizacion;

                lCodRegistro = this.InsertarRegistroSolicitud(nameSP, DateTime.Now, sPars);

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[4];
                pars[index] = new SqlParameter("@Proceso", SqlDbType.Int);
                pars[index++].Value = proceso;
                pars[index] = new SqlParameter("@Pantalla", SqlDbType.Int);
                pars[index++].Value = pantalla;
                pars[index] = new SqlParameter("@FechaUltActualizacion", SqlDbType.DateTime);
                pars[index++].Value = fechaUltActualizacion;
                pars[index] = new SqlParameter("@Cod", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lCodRegistro = Convert.ToInt64(pars[index].Value);
                change = lCodRegistro > 0 ? true : false;
            }
            catch (Exception ex)
            {
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return change;
        }

        public StringReader[] EnvioDT(StringReader[] xmlDS)
        {
            DataSet ds = new DataSet();
            try
            {
                //ds.ReadXmlSchema(xmlDS[0]);
                //ds.ReadXml(xmlDS[1], XmlReadMode.Auto);
                SqlParameter[] parameters = new SqlParameter[0];
                xmlDS = ObtenerRespuestaSR("HHVObtenerCalidades", parameters);
                //DataSet ds = new DataSet();
                ds.ReadXmlSchema(xmlDS[0]);
                ds.ReadXml(xmlDS[1], XmlReadMode.Auto);
            }
            catch { }
            return xmlDS;
        }

        #region InsertaError
        public void InsertaError(String parameters, String errorMessage)
        {
            long lCodRegistro = -1L;
            try
            {
                string sPars = string.Empty;
                lCodRegistro = this.InsertarRegistroSolicitud("SyncRegistroTablas", DateTime.Now, parameters);
                this.ActualizarRegistroSolicitud(lCodRegistro, false, errorMessage);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion InsertaError
        #region ObtenerRespuestaLong
        public long ObtenerRespuestaLong(String SPName, SqlParameter[] parameters)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            long lCodRegistro = -1L;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = SPName;
                foreach (SqlParameter sqlp in parameters)
                {
                    sPars += ", " + sqlp.ParameterName + "=" + sqlp.Value;
                }

                lCodRegistro = this.InsertarRegistroSolicitud("CargarLongGenerico", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append(SPName);

                #endregion Query

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
                lCodRegistro = Convert.ToInt64(parameters[(parameters.Length - 1)].Value);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", CargarLongGenerico: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return lCodRegistro;
        }
        #endregion ObtenerRespuestaLong


        /////////////////////
        //Empaque
        #region ObtenerModelos2
        public StringReader[] ObtenerModelos2(int molde)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerModelos2";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@CodMolde", SqlDbType.Int);
                parameters[i++].Value = molde;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerModelos2

        //Defectos
        #region ObtenerDefectos
        public DataTable ObtenerDefectos(int iProceso)
        {
            DataTable dtObj = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "HHVObtenerDefectos";
                        cmd.Parameters.AddWithValue("@CodigoProceso", iProceso);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtObj = new DataTable("Defectos");
                            da.Fill(dtObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerDefectos: " + ex.Message);
            }
            return dtObj == null ? new DataTable("Defectos") : dtObj;

        }
        #endregion ObtenerDefectos
        #region ObtenerZonasDefecto
        public DataTable ObtenerZonasDefecto(int iTipoArticulo)
        {
            DataTable dtObj = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "HHVObtenerZonasDefecto";
                        cmd.Parameters.AddWithValue("@CodigoTipoArticulo", iTipoArticulo);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtObj = new DataTable("Zona");
                            da.Fill(dtObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerZonas: " + ex.Message);
            }
            return dtObj == null ? new DataTable("Zona") : dtObj;
        }
        #endregion ObtenerZonasDefecto
        #region ObtenerEstadosDefecto
        public StringReader[] ObtenerEstadosDefecto(int estadoDefecto1, int estadoDefecto2)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerEstadosDefecto";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@CodEstadoDefecto1", SqlDbType.Int);
                parameters[i++].Value = estadoDefecto1;
                parameters[i] = new SqlParameter("@CodEstadoDefecto2", SqlDbType.Int);
                parameters[i++].Value = estadoDefecto2;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerEstadosDefecto

        //Varios
        #region InsertarPiezaTransaccion2
        //public DataTable InsertarPiezaTransaccion2(int empleado)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        BC.SCPP_HH bcObj = new BC.SCPP_HH();
        //        StringReader[] xmlDS = bcObj.InsertarPiezaTransaccion2(empleado);
        //        dt = ConvertToDatatable(xmlDS);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return dt;
        //}
        #endregion InsertarPiezaTransaccion2
        #region ObtenerCalidades
        public StringReader[] ObtenerCalidades()
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerCalidades";
                SqlParameter[] parameters = new SqlParameter[0];
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerCalidades
        #region ExisteModelo
        public int ExisteModelo(String claveArticulo)
        {
            long res = -1;
            try
            {
                String SPName = "HHVExisteModelo";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@ClaveArticulo", SqlDbType.NVarChar, 20);
                parameters[i++].Value = claveArticulo;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ExisteModelo
        #region ObtenerDesProceso
        public String ObtenerDesProceso(int proceso)
        {
            String res = "";
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerDesProceso";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@CodProceso", SqlDbType.Int);
                parameters[i++].Value = proceso;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(xmlDS[0]);
                ds.ReadXml(xmlDS[1], XmlReadMode.Auto);
                res = ds.Tables[0].Rows[0][0].ToString();
            }
            catch (Exception)
            {
            }
            return res;
        }
        #endregion ObtenerDesProceso
        #region ObtenerTiposModelo
        public StringReader[] ObtenerTiposModelo()
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerTiposModelo";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[0];
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerTiposModelo
        #region ObtenerModelos
        public StringReader[] ObtenerModelos(int tipoModelo)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerModelos";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@CodTipoModelo", SqlDbType.Int);
                parameters[i++].Value = tipoModelo;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerModelos
        #region ExisteModeloHastaRevisado
        public int ExisteModeloHastaRevisado(String claveArticulo)
        {
            long res = -1;
            try
            {
                String SPName = "HHVExisteModeloHastaRevisado";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@ClaveArticulo", SqlDbType.NVarChar, 20);
                parameters[i++].Value = claveArticulo;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ExisteModeloHastaRevisado
        #region ExisteModeloDesdeEsmaltado
        public int ExisteModeloDesdeEsmaltado(String claveArticulo)
        {
            long res = -1;
            try
            {
                String SPName = "HHVExisteModeloDesdeEsmaltado";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@ClaveArticulo", SqlDbType.NVarChar, 20);
                parameters[i++].Value = claveArticulo;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return (int)res;
        }
        #endregion ExisteModeloDesdeEsmaltado

        /////////////////////////////////////////
        #region ObtenerRespuesta
        public StringReader[] ObtenerRespuestaSR(String SPName, SqlParameter[] parameters)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            DataSet ds = new DataSet();
            long lCodRegistro = -1L;
            StringReader[] xmlDS = new StringReader[2];

            try
            {

                // InsertarRegistroSolicitud
                string sPars = SPName;
                foreach (SqlParameter sqlp in parameters)
                {
                    sPars += ", " + sqlp.ParameterName + "=" + sqlp.Value.ToString();
                }
                lCodRegistro = this.InsertarRegistroSolicitud("CargarDTGenerico", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append(SPName);

                // Query Execution
                ds = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
                StringReader xmlSchema = new StringReader(ds.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(ds.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ObtenerRespuestaSR: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return xmlDS;
        }
        public DataTable ObtenerRespuestaDT(String SPName, SqlParameter[] parameters)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            DataTable dtObj = new DataTable();
            long lCodRegistro = -1L;

            try
            {

                // InsertarRegistroSolicitud
                string sPars = SPName;
                foreach (SqlParameter sqlp in parameters)
                {
                    sPars += ", " + sqlp.ParameterName + "=" + sqlp.Value.ToString();
                }
                lCodRegistro = this.InsertarRegistroSolicitud("CargarDTGenerico", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append(SPName);

                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                throw new Exception(this.sClassName + ", ObtenerRespuestaDT: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return dtObj;
        }
        #endregion ObtenerRespuesta

        #region ObtenerModeloTipoPieza
        public StringReader[] ObtenerModeloTipoPieza(int articulo)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerModeloTipoPieza";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@CodArticulo", SqlDbType.Int);
                parameters[i++].Value = articulo;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception)
            {
            }
            return xmlDS;
        }
        #endregion ObtenerModeloTipoPieza
        #region ObtenerModeloTipoPieza2
        public StringReader[] ObtenerModeloTipoPieza2(int iCodPieza)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerModeloTipoPieza2";
                int i = 0;
                SqlParameter[] pars = new SqlParameter[1];
                pars[i] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[i++].Value = iCodPieza;
                xmlDS = this.ObtenerRespuestaSR(SPName, pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerModeloTipoPieza2: " + ex.Message);
            }
            return xmlDS;
        }
        #endregion ObtenerModeloTipoPieza2
        #region ObtenerColorPieza
        public SolutionEntityList<BE.HHColor> ObtenerColorPieza(int iCodPieza)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.HHColor> l_Res = new SolutionEntityList<BE.HHColor>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "CodPieza=" + iCodPieza.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerColorPieza", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #region Query

                queryString = new StringBuilder();
                queryString.Append("HHVObtenerColorPieza");

                #endregion Query

                // Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = iCodPieza;

                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                // Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.HHColor(dr));
                }

            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ObtenerColorPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerColorPieza
        #region ValidarPieza
        public SolutionEntityList<BE.HHValidarPieza> ValidarPieza(string sCodBarras, int iCodProcesoAct)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.HHValidarPieza> l_Res = new SolutionEntityList<BE.HHValidarPieza>();

            try
            {
                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars += "CodBarras=" + sCodBarras + ", ";
                sPars += "CodProcesoAct=" + iCodProcesoAct.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ValidarPieza", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHVValidarPieza");

                // Parameters
                pars = new SqlParameter[7];
                pars[0] = new SqlParameter("@CodBarras", SqlDbType.NVarChar, 15);
                pars[0].Value = sCodBarras;
                pars[1] = new SqlParameter("@CodProcesoAct", SqlDbType.Int);
                pars[1].Value = iCodProcesoAct;
                pars[2] = new SqlParameter("@ValProcesoExitosa", SqlDbType.Bit);
                pars[2].Direction = ParameterDirection.Output;
                pars[3] = new SqlParameter("@ValNoDefDespExitosa", SqlDbType.Bit);
                pars[3].Direction = ParameterDirection.Output;
                pars[4] = new SqlParameter("@MensajeValidacion", SqlDbType.NVarChar, 50);
                pars[4].Direction = ParameterDirection.Output;
                pars[5] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[5].Direction = ParameterDirection.Output;
                pars[6] = new SqlParameter("@CodProcesoPieza", SqlDbType.Int);
                pars[6].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                // Mapear el DataTable o Resultado en una lista de BusinessEntity.
                bool bValProcesoExitosa = Convert.ToBoolean(pars[2].Value);
                bool bValNoDefDespExitosa = Convert.ToBoolean(pars[3].Value);
                string sMensajeValidacion = Convert.ToString(pars[4].Value);
                int iCodPieza = Convert.ToInt32(pars[5].Value);
                int iCodProceso = Convert.ToInt32(pars[6].Value);
                l_Res.Load(new BE.HHValidarPieza(bValProcesoExitosa, bValNoDefDespExitosa, sMensajeValidacion, iCodPieza, iCodProceso));
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ValidarPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ValidarPieza

        #region ValidarTarimaPieza
        public SolutionEntityList<BE.HHValidarPieza> ValidarTarimaPieza(int iTarima, int sCodPieza, int sCodPiezaPadre)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            bool bInsertado = false;
            String sValidacion = "";
            SolutionEntityList<BE.HHValidarPieza> l_Res = new SolutionEntityList<BE.HHValidarPieza>();
            try
            {
                // InsertarRegistroSolicitud
                string sPars = string.Empty;
                sPars += "iTarima=" + iTarima + ", ";
                sPars += "sCodBarras=" + sCodPieza + ", ";
                sPars += "sCodBarrasPadre=" + sCodPiezaPadre;
                lCodRegistro = this.InsertarRegistroSolicitud("ValidarPieza", DateTime.Now, sPars);

                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("HHTValidarTarimaPieza");

                // Parameters

                pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@Tarima", SqlDbType.Int);
                pars[0].Value = iTarima;
                pars[1] = new SqlParameter("@CodBarras", SqlDbType.Int);
                pars[1].Value = sCodPieza;
                pars[2] = new SqlParameter("@CodBarrasPadre", SqlDbType.Int);
                pars[2].Value = sCodPiezaPadre;
                pars[3] = new SqlParameter("@Insertado", SqlDbType.Bit);
                pars[3].Direction = ParameterDirection.Output;
                pars[4] = new SqlParameter("@Validacion", SqlDbType.NVarChar, 150);
                pars[4].Direction = ParameterDirection.Output;

                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                // Mapear el DataTable o Resultado en una lista de BusinessEntity.
                bInsertado = Convert.ToBoolean(pars[3].Value);
                sValidacion = Convert.ToString(pars[4].Value);

                BE.HHValidarPieza vPieza = new BE.HHValidarPieza();
                vPieza.ValNoDefDespExitosa = bInsertado;// Variable ocupada para identificar si fue insertada o no a la tarima
                vPieza.MensajeValidacion = sValidacion;
                l_Res.Add(vPieza);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", ValidarTarimaPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region EstaEnInventarioPocesoPieza
        public int DeleteTarimaPieza(int iTarima, int iPieza)
        {
            long res = -1;
            try
            {
                String SPName = "HHTTarimaPiezaDel";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[i] = new SqlParameter("@Tarima", SqlDbType.Int);
                parameters[i++].Value = iTarima;
                parameters[i] = new SqlParameter("@Pieza", SqlDbType.Int);
                parameters[i++].Value = iPieza;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                res = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception e)
            {
                throw e;
            }
            return (int)res;
        }
        #endregion
        #region EsCasetaTanque
        public bool EsCasetaTanque(int iCodCaseta, int iCodTanque, int iCodProceso, int iCodPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            try
            {
                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #region Query
                queryString = new StringBuilder();
                queryString.Append("HHVEsCasetaTanque");
                #endregion Query
                // Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@CodCaseta", SqlDbType.Int);
                pars[0].Value = iCodCaseta;
                pars[1] = new SqlParameter("@CodTanque", SqlDbType.Int);
                pars[1].Value = iCodTanque;
                pars[2] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[2].Value = iCodTanque;
                pars[3] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[3].Value = iCodTanque;
                // Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if (dtObj == null) return false;
                // Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj.Rows.Count <= 0) return false;
                return Convert.ToBoolean(dtObj.Rows[0]["EsCasetaTanque"]);
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                throw new Exception(this.sClassName + ", EsCasetaTanque: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion EsCasetaTanque
        #region EstaServicioDisponible
        public bool EstaServicioDisponible()
        {
            DA01.MSSQLServer dbObj = null;
            try
            {
                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                Boolean bStatusConnection = dbObj.CompruebaConexion();
                return bStatusConnection;
            }
            catch (Exception ex)
            {
                // ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", EstaServicioDisponible: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region ObtenerCalidad
        private DataTable ObtenerCalidadEmpaque(string sClaveCalidad)
        {
            DataTable dt = null;
            try
            {
                String SPName = "spObtenerCalidadEmpaque";
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@ClaveCalidad", SqlDbType.VarChar, 255);
                parameters[0].Value = sClaveCalidad;
                dt = this.ObtenerRespuestaDT(SPName, parameters);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCalidadEmpaque: " + ex.Message);
            }
            finally
            {
                dt = null;
            }
        }
        #endregion
        #region ObtenerColor
        private DataTable ObtenerColores(string sClaveColor)
        {
            DataTable dt = null;
            try
            {
                String SPName = "spObtenerColores";
                SqlParameter[] parameters = new SqlParameter[0];
                dt = this.ObtenerRespuestaDT(SPName, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerColores: " + ex.Message);
            }
            return dt;
        }
        #endregion ObtenerColor
        #region ImprimirEtiqueta
        public void ImprimirEtiqueta(BE.ConfigImpresora cConfigImpresora, BE.Etiqueta eEtiqueta)
        {
            if (cConfigImpresora == null)
                throw new Exception("Favor de especificar la ubicacion de la impresora segun sea; planta, centro de trabajo, maquina.");
            if (eEtiqueta == null)
                throw new Exception("Favor de especificar la etiqueta.");
            if (eEtiqueta.TipoEtiqueta == (int)TipoEtiqueta.Pieza)
                ImprimirEtiquetaPieza(cConfigImpresora, eEtiqueta);
            else if (eEtiqueta.TipoEtiqueta == (int)TipoEtiqueta.Tarima)
                ImprimirEtiquetaTarima(cConfigImpresora, eEtiqueta);
        }
        private void ImprimirEtiquetaPieza(BE.ConfigImpresora cConfigImpresora, BE.Etiqueta eEtiqueta)
        {
            BE.ConfigImpresora configImpresora = null;
            BE.Etiqueta etiqueta = null;
            ZebraPrinter zebraPrinter = null;
            PrintNetworkZebraPrinter printerBehavior = null;
            ReadFromFileZPLCode readBehavior = null;
            string sClaveModeloCalidad = string.Empty;
            int iNumeroImpresiones = 0;
            int iNumeroImpresionesEtiqueta = 0;
            //IList<Campo> listCampoVariable = null;
            try
            {
                if (cConfigImpresora == null)
                    throw new Exception("Favor de especificar la ubicacion de la impresora segun sea; planta, centro de trabajo, maquina.");
                if (cConfigImpresora.CodPlanta <= 0 | cConfigImpresora.CodMaquina <= 0 | cConfigImpresora.CodCentroTrabajo <= 0)
                    throw new Exception("Favor de especificar la ubicacion de la impresora segun sea; planta, centro de trabajo, maquina.");
                if (eEtiqueta == null)
                    throw new Exception("Favor de especificar la etiqueta segun sea; modelo, color, calidad.");
                if (string.IsNullOrEmpty(eEtiqueta.Clave))
                    throw new Exception("Favor de especificar la etiqueta segun sea; modelo, color, calidad.");
                configImpresora = ObtenerConfiguracionImpresora(cConfigImpresora.CodPlanta, cConfigImpresora.CodCentroTrabajo, cConfigImpresora.CodMaquina, eEtiqueta.TipoEtiqueta);
                if (configImpresora == null)
                    throw new Exception("No se encontro la configuracion de la impresora.");
                if (string.IsNullOrEmpty(configImpresora.IpAddress))
                    throw new Exception("No se encontro la ubicacion de la impresora.");
                sClaveModeloCalidad = (eEtiqueta.Clave.Substring(0, eEtiqueta.Clave.IndexOf('-')) + eEtiqueta.Clave.Substring(eEtiqueta.Clave.LastIndexOf('-'))).Trim();
                etiqueta = ObtenerConfiguracionEtiqueta(sClaveModeloCalidad, eEtiqueta.TipoEtiqueta);
                if (etiqueta == null)
                    throw new Exception("No se encontro la configuracion de etiqueta.");
                etiqueta.Template = ObtenerUbicacionTemplateEtiqueta(sClaveModeloCalidad, eEtiqueta.TipoEtiqueta);
                if (string.IsNullOrEmpty(etiqueta.Template))
                    throw new Exception("No se encontro la ubicacion del template de etiqueta.");
                etiqueta.UPC = ObtenerCodigoBarrasUPC(eEtiqueta.Clave);
                if (string.IsNullOrEmpty(etiqueta.UPC))
                    throw new Exception("No se encontro el codigo de barras para UPC.");
                zebraPrinter = new ZebraPrinter();
                printerBehavior = new PrintNetworkZebraPrinter();
                printerBehavior.SetPrinter(IPAddress.Parse(configImpresora.IpAddress), configImpresora.Puerto);
                readBehavior = new ReadFromFileZPLCode();
                readBehavior.SetRutaNombreArchivo(etiqueta.Template);
                zebraPrinter.SetReadBehavior(readBehavior);
                zebraPrinter.Read();
                //listCampoVariable = ObtenerCampoVariable();
                foreach (Campo campo in etiqueta.Campo)
                {
                    if (campo.Tipo == 2)
                    {
                        DataTable dt = null;
                        DataRow[] rows = null;
                        switch (campo.Nombre)
                        {
                            case "&DescNombre&":
                                dt = ObtenerArticulo();
                                if (dt == null) continue;
                                rows = dt.Select("ClaveArticulo = '" + eEtiqueta.Clave + "'");
                                if (rows == null) continue;
                                if (rows.Length <= 0) continue;
                                zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, Convert.ToString(rows[0]["DesArticulo"]));
                                rows = null;
                                dt.Dispose();
                                break;
                            case "&DescParte&":
                                zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, sClaveModeloCalidad);
                                break;
                            case "&DescColor&":
                                string sClaveCalidad = eEtiqueta.Clave.Substring(eEtiqueta.Clave.LastIndexOf('-') + 1);
                                DataTable dtCalidad = ObtenerCalidadEmpaque(sClaveCalidad);
                                if (dtCalidad == null) continue;
                                if (dtCalidad.Rows.Count == 0) continue;
                                int iCodigoTipoCalidad = Convert.ToInt32(dtCalidad.Rows[0]["CodigoTipoCalidad"]);
                                dt = ObtenerColores(string.Empty);
                                if (dt == null) continue;
                                string sClaveColor = eEtiqueta.Clave.Substring(eEtiqueta.Clave.IndexOf('-') + 1, (eEtiqueta.Clave.LastIndexOf('-') - eEtiqueta.Clave.IndexOf('-') - 1)).Trim();
                                rows = dt.Select("ClaveColor = '" + sClaveColor + "'");
                                if (rows == null) continue;
                                if (rows.Length <= 0) continue;
                                zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, (iCodigoTipoCalidad == 1) ? Convert.ToString(rows[0]["Descripcion"]) : Convert.ToString(rows[0]["Descripcion2"]));
                                rows = null;
                                dt.Dispose();
                                break;
                            case "&CodigoBarraPZA&":
                                zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, eEtiqueta.Pieza);
                                break;
                            case "&CodigoBarraUPC&":
                                zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, etiqueta.UPC);
                                break;
                        }
                        zebraPrinter.SetZPLCode();
                        continue;
                    }
                    zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, campo.Valor);
                    zebraPrinter.SetZPLCode();
                }
                zebraPrinter.SetDataBytes(zebraPrinter.Data);
                zebraPrinter.SetPrintBehavior(printerBehavior);
                iNumeroImpresiones = ObtenerNumeroImpresiones(sClaveModeloCalidad);
                int iCodigoPieza = ObtenerCodPiezaCodBarras(eEtiqueta.Pieza);
                iNumeroImpresionesEtiqueta = ObtenerNumeroImpresionesEtiqueta(iCodigoPieza);
                for (int i = 0; i < iNumeroImpresiones; i++)
                {
                    if (iNumeroImpresionesEtiqueta == 0)
                        AgregarLogImpresionEtiqueta(iCodigoPieza);
                    else
                        ActualizarLogImpresionEtiqueta(iCodigoPieza);
                    iNumeroImpresionesEtiqueta += 1;
                    zebraPrinter.Print();
                }
                printerBehavior.Dispose();
                readBehavior.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void ImprimirEtiquetaTarima(BE.ConfigImpresora cConfigImpresora, BE.Etiqueta eEtiqueta)
        {
            BE.ConfigImpresora configImpresora = null;
            BE.Etiqueta etiqueta = null;
            ZebraPrinter zebraPrinter = null;
            PrintNetworkZebraPrinter printerBehavior = null;
            ReadFromFileZPLCode readBehavior = null;
            string sClaveModeloCalidad = string.Empty;
            //int iNumeroImpresiones = 0;
            //int iNumeroImpresionesEtiqueta = 0;
            //IList<Campo> listCampoVariable = null;
            try
            {
                if (cConfigImpresora == null)
                    throw new Exception("Favor de especificar la ubicacion de la impresora segun sea; planta, centro de trabajo, maquina.");
                if (cConfigImpresora.CodPlanta <= 0 | cConfigImpresora.CodMaquina <= 0 | cConfigImpresora.CodCentroTrabajo <= 0)
                    throw new Exception("Favor de especificar la ubicacion de la impresora segun sea; planta, centro de trabajo, maquina.");
                if (eEtiqueta == null)
                    throw new Exception("Favor de especificar la etiqueta segun sea; modelo, color, calidad.");
                /*if (string.IsNullOrEmpty(eEtiqueta.Clave))
                    throw new Exception("Favor de especificar la etiqueta segun sea; modelo, color, calidad.");*/
                DataTable pieza = ObtenerPieza(eEtiqueta.Pieza);
                eEtiqueta.Clave = pieza.Rows[0]["ClaveArticulo"].ToString() + "-" + pieza.Rows[0]["ClaveCalidad"].ToString();
                configImpresora = ObtenerConfiguracionImpresora(cConfigImpresora.CodPlanta, cConfigImpresora.CodCentroTrabajo, cConfigImpresora.CodMaquina, eEtiqueta.TipoEtiqueta);
                if (configImpresora == null)
                    throw new Exception("No se encontro la configuracion de la impresora.");
                if (string.IsNullOrEmpty(configImpresora.IpAddress))
                    throw new Exception("No se encontro la ubicacion de la impresora.");
                sClaveModeloCalidad = (eEtiqueta.Clave.Substring(0, eEtiqueta.Clave.IndexOf('-')) + eEtiqueta.Clave.Substring(eEtiqueta.Clave.LastIndexOf('-'))).Trim();
                etiqueta = ObtenerConfiguracionEtiqueta(sClaveModeloCalidad, eEtiqueta.TipoEtiqueta);
                if (etiqueta == null)
                    throw new Exception("No se encontro la configuracion de etiqueta.");
                etiqueta.Template = ObtenerUbicacionTemplateEtiqueta(sClaveModeloCalidad, eEtiqueta.TipoEtiqueta);
                if (string.IsNullOrEmpty(etiqueta.Template))
                    throw new Exception("No se encontro la ubicacion del template de etiqueta.");
                etiqueta.UPC = eEtiqueta.Tarima;
                if (string.IsNullOrEmpty(etiqueta.UPC))
                    throw new Exception("No se encontro el codigo de barras para UPC.");
                zebraPrinter = new ZebraPrinter();
                printerBehavior = new PrintNetworkZebraPrinter();
                printerBehavior.SetPrinter(IPAddress.Parse(configImpresora.IpAddress), configImpresora.Puerto);
                readBehavior = new ReadFromFileZPLCode();
                readBehavior.SetRutaNombreArchivo(etiqueta.Template);
                zebraPrinter.SetReadBehavior(readBehavior);
                zebraPrinter.Read();
                DateTime dtFechaHora = this.ObtenerFechaServidor();
                int indexImpresionNT = 0;
                foreach (Campo campo in etiqueta.Campo)
                {
                    
                    if (campo.Tipo == 2)
                    {
                        switch (campo.Nombre)
                        {
                            case "&NumeroTarima&":
                                if (indexImpresionNT == 0)
                                {
                                    zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, string.Empty);
                                    break;
                                }
                                indexImpresionNT += 1;
                                zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, etiqueta.UPC);
                                break;
                            case "&DescPlanta&":
                                zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, pieza.Rows[0]["DescPlanta"].ToString());
                                break;
                            case "&HoraImpresion&":
                                zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, Convert.ToDateTime(dtFechaHora).ToString("hh:mm:ss"));
                                break;
                            case "&FechaImpresion&":
                                zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, Convert.ToDateTime(dtFechaHora).ToString("dd/MM/yyyy"));
                                break;
                            case "&CodigoBarra&":
                                zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, etiqueta.UPC);
                                break;
                        }
                        zebraPrinter.SetZPLCode();
                        continue;
                    }
                    if (campo.Nombre == "&TextoTarima&")
                        zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, string.Empty);
                    else if (campo.Nombre == "&TextoHora&")
                        zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, string.Empty);
                    else 
                        zebraPrinter.ReplaceTextValueInZplCode(campo.Nombre, campo.Valor);
                    zebraPrinter.SetZPLCode();
                }
                zebraPrinter.SetDataBytes(zebraPrinter.Data);
                zebraPrinter.SetPrintBehavior(printerBehavior);
                //iNumeroImpresiones = ObtenerNumeroImpresiones(sClaveModeloCalidad);
                //int iCodigoPieza = ObtenerCodPiezaCodBarras(eEtiqueta.Pieza);
                //iNumeroImpresionesEtiqueta = ObtenerNumeroImpresionesEtiqueta(iCodigoPieza);
                //for (int i = 0; i < iNumeroImpresiones; i++)
                //{
                //    if (iNumeroImpresionesEtiqueta == 0)
                //        AgregarLogImpresionEtiqueta(iCodigoPieza);
                //    else
                //        ActualizarLogImpresionEtiqueta(iCodigoPieza);
                //    iNumeroImpresionesEtiqueta += 1;
                //    zebraPrinter.Print();
                //}
                zebraPrinter.Print();
                printerBehavior.Dispose();
                readBehavior.Dispose();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        #endregion
        #region ObtenerCampoVariable
        private IList<Campo> ObtenerCampoVariable()
        {
            DataTable dtRes = null;
            IList<Campo> listCampo = null;
            try
            {
                String SPName = "dbo.spObtenerCampoVariable";
                int i = 0;
                SqlParameter[] pars = new SqlParameter[0];
                dtRes = this.ObtenerRespuestaDT(SPName, pars);
                if (dtRes == null) return listCampo;
                if (dtRes.Rows.Count <= 0) return listCampo;
                listCampo = new List<Campo>();
                foreach (DataRow dr in dtRes.Rows)
                {
                    Campo campo = new Campo();
                    campo.Cod = Convert.ToInt32(dr["IdCampo"]);
                    campo.Nombre = Convert.ToString(dr["Nombre"]);
                    campo.Tipo = Convert.ToInt32(dr["IdTipoCampo"]);
                    listCampo.Add(campo);
                }
                return listCampo;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCampoVariable: " + ex.Message);
            }
            finally
            {
                if (dtRes != null) dtRes.Dispose();
            }
        }
        #endregion ObtenerCampoVariable
        #region ObtenerUbicacionTemplateEtiqueta
        private string ObtenerUbicacionTemplateEtiqueta(string sClave, int iCodTipoEtiqueta)
        {
            DataTable dtRes = null;
            try
            {
                String SPName = "spObtenerUbicacionTemplateEtiquetaSel";
                int i = 0;
                SqlParameter[] pars = new SqlParameter[2];
                pars[i] = new SqlParameter("@Clave", SqlDbType.VarChar, 50);
                pars[i++].Value = sClave;
                pars[i] = new SqlParameter("@CodTipoEtiqueta", SqlDbType.Int);
                pars[i++].Value = iCodTipoEtiqueta;
                dtRes = this.ObtenerRespuestaDT(SPName, pars);
                if (dtRes == null) return string.Empty;
                if (dtRes.Rows.Count <= 0) return string.Empty;
                return Convert.ToString((dtRes.Rows[0]["Ubicacion"] == DBNull.Value) ? string.Empty : dtRes.Rows[0]["Ubicacion"]);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerUbicacionTemplateEtiqueta: " + ex.Message);
            }
            finally
            {
                if (dtRes != null) dtRes.Dispose();
            }
        }
        #endregion
        #region ObtenerUbicacionTemplateEtiqueta
        private int ObtenerNumeroImpresiones(string sClave)
        {
            DataTable dtRes = null;
            try
            {
                String SPName = "spObtenerNumeroImpresiones";
                int i = 0;
                SqlParameter[] pars = new SqlParameter[1];
                pars[i] = new SqlParameter("@Clave", SqlDbType.VarChar, 50);
                pars[i++].Value = sClave;
                dtRes = this.ObtenerRespuestaDT(SPName, pars);
                if (dtRes == null) return 0;
                if (dtRes.Rows.Count <= 0) return 0;
                return Convert.ToInt32((dtRes.Rows[0]["NumeroImpresiones"] == DBNull.Value) ? 0 : dtRes.Rows[0]["NumeroImpresiones"]);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerNumeroImpresiones: " + ex.Message);
            }
            finally
            {
                if (dtRes != null) dtRes.Dispose();
            }
        }
        #endregion
        #region ObtenerConfiguracionEtiqueta
        private Etiqueta ObtenerConfiguracionEtiqueta(string sClave, int iCodTipoEtiqueta)
        {
            DataTable dtRes = new DataTable();
            Etiqueta etiqueta = null;
            try
            {
                String SPName = "spObtenerConfiguracionEtiquetaSel";
                int i = 0;
                SqlParameter[] pars = new SqlParameter[2];
                pars[i] = new SqlParameter("@Clave", SqlDbType.VarChar, 50);
                pars[i++].Value = sClave;
                pars[i] = new SqlParameter("@CodTipoEtiqueta", SqlDbType.Int);
                pars[i++].Value = iCodTipoEtiqueta;
                dtRes = this.ObtenerRespuestaDT(SPName, pars);
                if (dtRes == null) return etiqueta;
                if (dtRes.Rows.Count <= 0) return etiqueta;
                etiqueta = new Etiqueta();
                IList<Campo> ilCampo = new List<Campo>();
                foreach (DataRow dr in dtRes.Rows)
                {
                    Campo campo = new Campo();
                    campo.Cod = Convert.ToInt32(dr["IdCampo"]);
                    campo.Nombre = Convert.ToString(dr["NombreCampo"]);
                    campo.Tipo = Convert.ToInt32(dr["IdTipoCampo"]);
                    campo.Valor = Convert.ToString(dr["ValorCampo"]);
                    ilCampo.Add(campo);
                }
                etiqueta.Campo = ilCampo;
                return etiqueta;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerConfiguracionEtiqueta: " + ex.Message);
            }
            finally
            {
                if (dtRes != null) dtRes.Dispose();
            }
        }
        #endregion
        #region ObtenerConfiguracionImpresora
        private BE.ConfigImpresora ObtenerConfiguracionImpresora(int iCodPlanta, int iCodCentroTrabajo, int iCodMaquina, int iCodTipoEtiqueta)
        {
            DataTable dtRes = new DataTable();
            BE.ConfigImpresora configImpresora = null;
            try
            {
                String SPName = "spObtenerConfiguracionImpresoraSel";
                int i = 0;
                SqlParameter[] pars = new SqlParameter[4];
                pars[i] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[i++].Value = iCodPlanta;
                pars[i] = new SqlParameter("@CodCentroTrabajo", SqlDbType.Int);
                pars[i++].Value = iCodCentroTrabajo;
                pars[i] = new SqlParameter("@CodMaquina", SqlDbType.Int);
                pars[i++].Value = iCodMaquina;
                pars[i] = new SqlParameter("@CodTipoEtiqueta", SqlDbType.Int);
                pars[i++].Value = iCodTipoEtiqueta;
                dtRes = this.ObtenerRespuestaDT(SPName, pars);
                if (dtRes == null) return null;
                if (dtRes.Rows.Count <= 0) return null;
                configImpresora = new BE.ConfigImpresora();
                configImpresora.CodCentroTrabajo = iCodCentroTrabajo;
                configImpresora.CodMaquina = iCodMaquina;
                configImpresora.CodPlanta = iCodPlanta;
                configImpresora.IpAddress = Convert.ToString((dtRes.Rows[0]["IPAddress"] == DBNull.Value) ? string.Empty : dtRes.Rows[0]["IPAddress"]);
                configImpresora.Puerto = Convert.ToInt32((dtRes.Rows[0]["Puerto"] == DBNull.Value) ? 9100 : dtRes.Rows[0]["Puerto"]);
                return configImpresora;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerConfiguracionImpresora: " + ex.Message);
            }
            finally
            {
                if (dtRes != null) dtRes.Dispose();
            }
        }
        #endregion
        #region ObtenerCodigoBarrasUPC
        private string ObtenerCodigoBarrasUPC(string sClave)
        {
            DataTable dtRes = null;
            try
            {
                String SPName = "ObtenerCodigoBarrasUPCSel";
                int i = 0;
                SqlParameter[] pars = new SqlParameter[1];
                pars[i] = new SqlParameter("@Clave", SqlDbType.VarChar, 20);
                pars[i++].Value = sClave;
                dtRes = this.ObtenerRespuestaDT(SPName, pars);
                if (dtRes == null) return string.Empty;
                if (dtRes.Rows.Count <= 0) return string.Empty;
                return Convert.ToString((dtRes.Rows[0]["CodigoBarrasUPC"] == DBNull.Value) ? string.Empty : dtRes.Rows[0]["CodigoBarrasUPC"]);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCodigoBarrasUPC: " + ex.Message);
            }
            finally
            {
                if (dtRes != null) dtRes.Dispose();
            }
        }
        #endregion

        #region ObtenerPiezas
        public StringReader[] ObtenerPieza(int iCodPieza)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "PiezaSel";
                int i = 0;
                SqlParameter[] pars = new SqlParameter[1];
                pars[i] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[i++].Value = iCodPieza;
                xmlDS = this.ObtenerRespuestaSR(SPName, pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPieza: " + ex.Message);
            }
            return xmlDS;
        }
        public StringReader[] ObtenerPiezasRequeme(int iCodPlanta)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "PiezaRequemeSel";
                int i = 0;
                SqlParameter[] pars = new SqlParameter[1];
                pars[i] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[i++].Value = iCodPlanta;
                xmlDS = this.ObtenerRespuestaSR(SPName, pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasRequeme: " + ex.Message);
            }
            return xmlDS;
        }
        #endregion

        public StringReader[] ProcesarBatchEsmaltadoPiezas(StringReader[] xmlDS)
        {
            DataSet ds = new DataSet();
            StringReader[] xmlDSRes = new StringReader[2];
            try
            {
                ds.ReadXmlSchema(xmlDS[0]);
                ds.ReadXml(xmlDS[1], XmlReadMode.Auto);
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[3];
                parameters[i++] = new SqlParameter("@tblPiezaTVP", ds.Tables[0]);
                parameters[i++] = new SqlParameter("@tblPiezaTrasaccionTVP", ds.Tables[1]);
                parameters[i++] = new SqlParameter("@tblPruebaProcesoTVP", ds.Tables[2]);

                xmlDSRes = ObtenerRespuestaSR("spProcesarBatchEsmaltadoPiezasUpd", parameters);
            }
            catch (Exception e)
            {
                this.InsertaError("BC:ProcesarBatchEsmaltadoPiezas", e.Message);
                new Exception(this.sClassName + ", BC:ProcesarBatchEsmaltadoPiezas: " + e.Message);
            }
            return xmlDSRes;
        }

        #endregion Common
        public DataSet ProcesarBatchVaciadoPieza(DataTable dtPieza, DataTable dtPiezaTransaccion, DataTable dtVaciadas, DataTable dtCarroPieza, DataTable dtPiezaDefecto, DataTable dtEstadoPieza, DataTable dtProcesoPieza, DataTable dtPuebaProceso)
        {
            DataSet ds = null;
            try
            {
                if (dtPieza == null) throw new Exception("Estructura no definida para la tabla Pieza");
                if (dtPiezaTransaccion == null) throw new Exception("Estructura no definida para la tabla Pieza Transaccion");
                if (dtVaciadas == null) throw new Exception("Estructura no definida para la tabla Configuracion Vaciadas");
                if (dtCarroPieza == null) throw new Exception("Estructura no definida para la tabla Carro Pieza");
                if (dtPiezaDefecto == null) throw new Exception("Estructura no definida para la tabla Pieza Defecto");
                if (dtEstadoPieza == null) throw new Exception("Estructura no definida para la tabla Estado Pieza");
                if (dtProcesoPieza == null) throw new Exception("Estructura no definida para la tabla Proceso Pieza");
                if (dtPuebaProceso == null) throw new Exception("Estructura no definida para la tabla Prueba Proceso");
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spProcesarBatchVaciadoPiezasIns";
                        cmd.Parameters.AddWithValue("@tblPiezaTVP", dtPieza);
                        cmd.Parameters.AddWithValue("@tblPiezaTrasaccionTVP", dtPiezaTransaccion);
                        cmd.Parameters.AddWithValue("@tblVaciadasTVP", dtVaciadas);
                        cmd.Parameters.AddWithValue("@tblCarroPiezaTVP", dtCarroPieza);
                        cmd.Parameters.AddWithValue("@tblPiezaDefectoTVP", dtPiezaDefecto);
                        cmd.Parameters.AddWithValue("@tblEstadoPiezaTVP", dtEstadoPieza);
                        cmd.Parameters.AddWithValue("@tblProcesoPiezaTVP", dtProcesoPieza);
                        cmd.Parameters.AddWithValue("@tblPruebaProcesoTVP", dtPuebaProceso);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet("ProcesoVaciado");
                            da.Fill(ds);
                        }
                    }
                }
                return (ds == null) ? new DataSet("ProcesoVaciado") : ds;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ProcesarBatchVaciadoPieza: " + ex.Message);
            }
            finally
            {
                if (ds != null) ds.Dispose();
            }
        }
        public DataSet ProcesarBatchSecadoPieza(DataTable dtPieza, DataTable dtPiezaTransaccion, DataTable dtPiezaTransaccionSecador, DataTable dtCarroPieza)
        {
            DataSet ds = null;
            try
            {
                if (dtPieza == null) throw new Exception("Estructura no definida para la tabla Pieza");
                if (dtPiezaTransaccion == null) throw new Exception("Estructura no definida para la tabla Pieza Transaccion");
                if (dtPiezaTransaccionSecador == null) throw new Exception("Estructura no definida para la tabla Pieza Transaccion Secador");
                if (dtCarroPieza == null) throw new Exception("Estructura no definida para la tabla Carro Pieza");
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spProcesarBatchSecadoPiezasIns";
                        cmd.Parameters.AddWithValue("@tblPiezaTVP", dtPieza);
                        cmd.Parameters.AddWithValue("@tblPiezaTrasaccionTVP", dtPiezaTransaccion);
                        cmd.Parameters.AddWithValue("@tblPiezaTrasaccionSecadorTVP", dtPiezaTransaccionSecador);
                        cmd.Parameters.AddWithValue("@tblCarroPiezaDelTVP", dtCarroPieza);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet("ProcesoSecado");
                            da.Fill(ds);
                        }
                    }
                }
                return (ds == null) ? new DataSet("ProcesoSecado") : ds;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ProcesarBatchSecadoPieza: " + ex.Message);
            }
            finally
            {
                if (ds != null) ds.Dispose();
            }
        }
        public DataSet ProcesarBatchRevisadoPieza(DataSet dsRevisado)
        {
            DataSet ds = null;
            try
            {
                if (dsRevisado == null) throw new Exception("Estructura no definida para el envio de información");
                if (dsRevisado.Tables[0] == null) throw new Exception("Estructura no definida para la tabla Proceso Pieza");
                if (dsRevisado.Tables[1] == null) throw new Exception("Estructura no definida para la tabla Pieza Transaccion");
                if (dsRevisado.Tables[2] == null) throw new Exception("Estructura no definida para la tabla Pieza Defecto");
                if (dsRevisado.Tables[3] == null) throw new Exception("Estructura no definida para la tabla Estado Pieza");
                if (dsRevisado.Tables[4] == null) throw new Exception("Estructura no definida para la tabla Prueba Proceso");
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spProcesarBatchRevisadoPiezasUpd";
                        cmd.Parameters.AddWithValue("@tblPiezaTVP", dsRevisado.Tables[0]);
                        cmd.Parameters.AddWithValue("@tblPiezaTrasaccionTVP", dsRevisado.Tables[1]);
                        cmd.Parameters.AddWithValue("@tblPiezaDefectoTVP", dsRevisado.Tables[2]);
                        cmd.Parameters.AddWithValue("@tblEstadoPiezaTVP", dsRevisado.Tables[3]);
                        cmd.Parameters.AddWithValue("@tblPruebaProcesoTVP", dsRevisado.Tables[4]);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet("ProcesoRevisado");
                            da.Fill(ds);
                        }
                    }
                }
                return (ds == null) ? new DataSet("ProcesoRevisado") : ds;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ProcesarBatchRevisadoPieza: " + ex.Message);
            }
            finally
            {
                if (ds != null) ds.Dispose();
            }
        }
        public DataSet ProcesarBatchHornosPieza(DataSet dsHornos)
        {
            DataSet ds = null;
            try
            {
                if (dsHornos == null) throw new Exception("Estructura no definida para el envio de información");
                if (dsHornos.Tables[0] == null) throw new Exception("Estructura no definida para la tabla Proceso Pieza");
                if (dsHornos.Tables[1] == null) throw new Exception("Estructura no definida para la tabla Pieza Transaccion");
                if (dsHornos.Tables[2] == null) throw new Exception("Estructura no definida para la tabla Pieza Defecto");
                if (dsHornos.Tables[3] == null) throw new Exception("Estructura no definida para la tabla Estado Pieza");
                if (dsHornos.Tables[4] == null) throw new Exception("Estructura no definida para la tabla Carro Pieza Quemado");
                if (dsHornos.Tables[5] == null) throw new Exception("Estructura no definida para la tabla Prueba Proceso");
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spProcesarBatchHornosPiezasUpd";
                        cmd.Parameters.AddWithValue("@tblPiezaTVP", dsHornos.Tables[0]);
                        cmd.Parameters.AddWithValue("@tblPiezaTrasaccionTVP", dsHornos.Tables[1]);
                        cmd.Parameters.AddWithValue("@tblPiezaDefectoTVP", dsHornos.Tables[2]);
                        cmd.Parameters.AddWithValue("@tblEstadoPiezaTVP", dsHornos.Tables[3]);
                        cmd.Parameters.AddWithValue("@tblCarroPiezaQuemadoTVP", dsHornos.Tables[4]);
                        cmd.Parameters.AddWithValue("@tblPruebaProcesoTVP", dsHornos.Tables[5]);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet("ProcesoHornos");
                            da.Fill(ds);
                        }
                    }
                }
                return (ds == null) ? new DataSet("ProcesoHornos") : ds;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ProcesarBatchHornosPieza: " + ex.Message);
            }
            finally
            {
                if (ds != null) ds.Dispose();
            }
        }
        public DateTime ObtenerFechaServidor()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet ds = new DataSet();
            DateTime dtFecha;
            String nameSP = "spObtenerFechaServidor";
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                int index = 0;
                pars = new SqlParameter[1];
                pars[index] = new SqlParameter("@Fecha", SqlDbType.DateTime);
                pars[index].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                dtFecha = Convert.ToDateTime(pars[index].Value);
            }
            catch (Exception ex)
            {
                this.InsertaError("ObtenerFechaServidor", ex.Message);
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return dtFecha;
        }
        public int ObtenerConfigBancoCasetaTanque(int iCodMaquina, int iCodTanque, int iCodProceso, int iCodPlanta)
        {
            long lRes = -1;
            try
            {
                String SPName = "spObtenerConfigBancoCasetaTanque";
                SqlParameter[] parameters = new SqlParameter[5];
                int i = 0;
                parameters[i] = new SqlParameter("@CodMaquina", SqlDbType.Int);
                parameters[i++].Value = iCodMaquina;
                parameters[i] = new SqlParameter("@CodTanque", SqlDbType.Int);
                parameters[i++].Value = iCodTanque;
                parameters[i] = new SqlParameter("@CodProceso", SqlDbType.Int);
                parameters[i++].Value = iCodProceso;
                parameters[i] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                parameters[i++].Value = iCodPlanta;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i].Direction = ParameterDirection.Output;
                lRes = this.ObtenerRespuestaLong(SPName, parameters);
            }
            catch (Exception e)
            {
                throw new Exception(this.sClassName + ", ObtenerConfigBancoCasetaTanque: " + e.Message);
            }
            return (int)lRes;
        }
        #region ObtenerEmpleado
        public StringReader[] ObtenerOperador(int iOperador)
        {
            StringReader[] xmlDS = null;
            try
            {
                String SPName = "HHVObtenerEmpleado";
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@CodEmpleado", SqlDbType.Int);
                parameters[i++].Value = iOperador;
                xmlDS = this.ObtenerRespuestaSR(SPName, parameters);
            }
            catch (Exception e)
            {
                throw e;
            }
            return xmlDS;
        }
        #endregion
        public DataSet LogIns(DataSet dsLog)
        {
            DataSet ds = null;
            try
            {
                if (dsLog == null) return null;
                if (dsLog.Tables[0] == null) return null;
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spLogIns";
                        cmd.Parameters.AddWithValue("@tblLogTVP", dsLog.Tables[0]);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            ds = new DataSet("Log");
                            da.Fill(ds);
                        }
                    }
                }
                return (ds == null) ? new DataSet("Log") : ds;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", LogIns: " + ex.Message);
            }
            finally
            {
                if (ds != null) ds.Dispose();
            }
        }
        #region ObtenerTipoArticuloPieza
        public DataTable ObtenerTipoArticuloPieza(int iCodigoPieza)
        {
            DataTable dtObj = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerTipoArticuloPieza";
                        cmd.Parameters.AddWithValue("@CodigoPieza", iCodigoPieza);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtObj = new DataTable("TipoArticuloPieza");
                            da.Fill(dtObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", TipoArticuloPieza: " + ex.Message);
            }
            return dtObj == null ? new DataTable("TipoArticuloPieza") : dtObj;
        }
        #endregion
        #region ObtenerKardexPieza
        public DataSet ObtenerKardexPieza(int? iCodigoPieza, string sCodigoBarras)
        {
            DataSet dsObj = null;
            try
            {
                if (!string.IsNullOrEmpty(sCodigoBarras) && this.ObtenerCodPiezaCodBarras(sCodigoBarras) < 1)
                    throw new Exception("Pieza no existe");
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerKardexPiezaSintetisado";
                        if (iCodigoPieza.HasValue) cmd.Parameters.AddWithValue("@CodigoPieza", iCodigoPieza.Value);
                        if (!string.IsNullOrEmpty(sCodigoBarras)) cmd.Parameters.AddWithValue("@CodigoBarras", sCodigoBarras);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dsObj = new DataSet("KardexPieza");
                            da.Fill(dsObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", KardexPieza: " + ex.Message);
            }
            return dsObj == null ? new DataSet("KardexPieza") : dsObj;
        }
        #endregion
        #region ObtenerCarrosPendientesSecador
        public DataTable ObtenerCarrosPendientesSecador(int iCodigoPlanta)
        {
            DataTable dtObj = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerCarrosPendientesSecador";
                        cmd.Parameters.AddWithValue("@CodigoPlanta", iCodigoPlanta);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtObj = new DataTable("CarrosPendientesSecador");
                            da.Fill(dtObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", CarrosPendientesSecador: " + ex.Message);
            }
            return dtObj == null ? new DataTable("CarrosPendientesSecador") : dtObj;
        }
        #endregion
        #region ObtenerCarrosPendientesSecadorDetalle
        public DataTable ObtenerCarrosPendientesSecadorDetalle(int iCarro)
        {
            DataTable dtObj = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerCarrosPendientesSecadorDetalle";
                        cmd.Parameters.AddWithValue("@Carro", iCarro);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtObj = new DataTable("CarrosPendientesSecadorDetalle");
                            da.Fill(dtObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", CarrosPendientesSecadorDetalle: " + ex.Message);
            }
            return dtObj == null ? new DataTable("CarrosPendientesSecadorDetalle") : dtObj;
        }
        #endregion
        #region ObtenerProduccionUsuario
        public DataTable ObtenerProduccion(int iCodigoOperador, int iCodigoProceso)
        {
            DataTable dtObj = null;
            DateTime fecha = DateTime.Today;
            try
            {
                fecha = ObtenerFechaServidor();
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerProduccionEmpleado";
                        cmd.Parameters.AddWithValue("@CodigoOperador", iCodigoOperador);
                        cmd.Parameters.AddWithValue("@CodigoProceso", iCodigoProceso);
                        cmd.Parameters.AddWithValue("@Fecha", fecha);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtObj = new DataTable("ProduccionEmpleado");
                            da.Fill(dtObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ProduccionUsuario: " + ex.Message);
            }
            return dtObj == null ? new DataTable("ProduccionUsuario") : dtObj;
        }
        #endregion
        #region CambiarContrasenaLogin
        public string CambiarPassword(string sUsuario, string sContrasenaAnterior, string sContrasenaNueva)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            try
            {
                if (!ValidarPoliticaContrasena(sContrasenaNueva))
                {
                    string sMensaje = "La contraseña no cumple con las politicas de seguridad, por favor verifique:\n";
                    sMensaje += "\t- Longitud minima de la contraseña.\n";
                    sMensaje += "\t- La contraseña debe incluir almenos uno de los caracteres entre A-Z, a-z, 0-9.";
                    return sMensaje;
                }
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spCambioContrasenaLogin");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@Usuario", SqlDbType.VarChar, 10);
                pars[0].Value = sUsuario;
                //
                if (sContrasenaAnterior == "Lamosa06")
                {
                    pars[1] = new SqlParameter("@ContrasenaAnt", SqlDbType.VarChar, 255);
                    pars[1].Value = EncriptarContrasenaUsuario(sContrasenaAnterior, sContrasenaAnterior);
                }
                else
                {
                    pars[1] = new SqlParameter("@ContrasenaAnt", SqlDbType.VarChar, 255);
                    pars[1].Value = EncriptarContrasenaUsuario(sUsuario, sContrasenaAnterior);
                }
                if (sContrasenaNueva == "Lamosa06")
                {
                    pars[2] = new SqlParameter("@ContrasenaNueva", SqlDbType.VarChar, 255);
                    pars[2].Value = EncriptarContrasenaUsuario(sContrasenaNueva, sContrasenaNueva);
                }
                else
                {
                    pars[2] = new SqlParameter("@ContrasenaNueva", SqlDbType.VarChar, 255);
                    pars[2].Value = EncriptarContrasenaUsuario(sUsuario, sContrasenaNueva);
                }
                //
                pars[3] = new SqlParameter("@mensaje", SqlDbType.VarChar, 255);
                pars[3].Direction = ParameterDirection.Output;
                #endregion
                #region Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                return pars[3].Value.ToString();
                #endregion Query Execution
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", CambiarPassword: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
        }
        #endregion
        private static string EncriptarContrasenaUsuario(string CuentaUsuario, string ContrasenaUsuario)
        {
            //string plainText = "Lamosa2014";    // original plaintext --Contrasena de Usuario
            string passPhrase = "LamosaSanitariosS.A.deC.V.";        // can be any string
            //string saltValue = "rcapetillo@sasant";        // can be any string --Cuenta de Usuario
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 2;                  // can be any number
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            int keySize = 256;                // can be 192 or 128
            try
            {
                return Encrypt(ContrasenaUsuario, passPhrase, CuentaUsuario, hashAlgorithm, passwordIterations, initVector, keySize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string DesencriptarContrasenaUsuario(string CuentaUsuario, string ContrasenaUsuario)
        {
            //string plainText = "Lamosa2014";    // original plaintext --Contrasena de Usuario
            string passPhrase = "LamosaSanitariosS.A.deC.V.";        // can be any string
            //string saltValue = "rcapetillo@sasant";        // can be any string --Cuenta de Usuario
            string hashAlgorithm = "SHA1";             // can be "MD5"
            int passwordIterations = 2;                  // can be any number
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            int keySize = 256;                // can be 192 or 128
            try
            {
                return Decrypt(ContrasenaUsuario, passPhrase, CuentaUsuario, hashAlgorithm, passwordIterations, initVector, keySize);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm
        /// and returns a base64-encoded result.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be encrypted.
        /// </param>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived. The
        /// derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that this
        /// passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used along with passphrase to generate password. Salt can
        /// be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hash algorithm used to generate password. Allowed values are: "MD5" and
        /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to generate password. One or two iterations
        /// should be enough.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (or IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be 
        /// exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        /// Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
        /// Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        /// Encrypted value formatted as a base64-encoded string.
        /// </returns>
        public static string Encrypt(string plainText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);
            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();
            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;
            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream();
            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            // Finish encrypting.
            cryptoStream.FlushFinalBlock();
            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();
            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();
            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);
            // Return encrypted string.
            return cipherText;
        }
        /// <summary>
        /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-formatted ciphertext value.
        /// </param>
        /// <param name="passPhrase">
        /// Passphrase from which a pseudo-random password will be derived. The
        /// derived password will be used to generate the encryption key.
        /// Passphrase can be any string. In this example we assume that this
        /// passphrase is an ASCII string.
        /// </param>
        /// <param name="saltValue">
        /// Salt value used along with passphrase to generate password. Salt can
        /// be any string. In this example we assume that salt is an ASCII string.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Hash algorithm used to generate password. Allowed values are: "MD5" and
        /// "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
        /// </param>
        /// <param name="passwordIterations">
        /// Number of iterations used to generate password. One or two iterations
        /// should be enough.
        /// </param>
        /// <param name="initVector">
        /// Initialization vector (or IV). This value is required to encrypt the
        /// first block of plaintext data. For RijndaelManaged class IV must be
        /// exactly 16 ASCII characters long.
        /// </param>
        /// <param name="keySize">
        /// Size of encryption key in bits. Allowed values are: 128, 192, and 256.
        /// Longer keys are more secure than shorter keys.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        /// <remarks>
        /// Most of the logic in this function is similar to the Encrypt
        /// logic. In order for decryption to work, all parameters of this function
        /// - except cipherText value - must match the corresponding parameters of
        /// the Encrypt function which was called to generate the
        /// ciphertext.
        /// </remarks>
        public static string Decrypt(string cipherText, string passPhrase, string saltValue, string hashAlgorithm, int passwordIterations, string initVector, int keySize)
        {
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(keySize / 8);
            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = new RijndaelManaged();
            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;
            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            // Define cryptographic stream (always use Read mode for encryption).
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();
            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            // Return decrypted string.   
            return plainText;
        }
        public static bool ValidarPoliticaContrasena(string contrasena)
        {
            if (string.IsNullOrEmpty(contrasena) || contrasena.Length < 8) return false;
            int iSumaCodigo = (from cpwd in contrasena.ToCharArray()
                               where (cpwd >= 48 & cpwd <= 57) | (cpwd >= 65 & cpwd <= 90) | (cpwd >= 97 & cpwd <= 122)
                               select ((cpwd >= 48 & cpwd <= 57) ? 1 : (cpwd >= 65 & cpwd <= 90) ? 2 : (cpwd >= 97 & cpwd <= 122) ? 3 : 0)).Distinct<int>().Sum();
            return (iSumaCodigo == 6) ? true : false;
        }
        public string ObtenerMensajeInicioSesion()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            try
            {
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerMensajeInicioSesion");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[0];
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if (dtObj == null || dtObj.Rows.Count == 0)
                    throw new Exception("No se tiene configurado el mensaje de aviso de inicio de sesión.");
                #endregion Query Execution
                return dtObj.Rows[0]["MensajeInicioSesion"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
                if (dtObj != null) dtObj.Dispose();
            }
        }
        private string ObtenerContrasenaDefault()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            string sContrasena = string.Empty;
            try
            {
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerContrasenaDefaultSel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[0];
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null || dtObj.Rows.Count == 0)
                    throw new Exception("No se encontró contraseña default.");
                foreach (DataRow dr in dtObj.Rows)
                    sContrasena = dr["ContrasenaDefault"].ToString();
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                return sContrasena;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerContrasenaDefault: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
                if (dtObj != null) dtObj.Dispose();
            }
        }
        public bool HabilitarImpresionEtiqueta(int CodigoPieza)
        {
            int iConfiguracionNumeroImpresiones = 0;
            int iNumeroImpresionesEtiqueta = 0;
            bool bHalitarImpresionEtiqueta = false;
            try
            {
                iConfiguracionNumeroImpresiones = ObtenerConfiguracionNumeroImpresiones();
                iNumeroImpresionesEtiqueta = ObtenerNumeroImpresionesEtiqueta(CodigoPieza);
                bHalitarImpresionEtiqueta = (iNumeroImpresionesEtiqueta > iConfiguracionNumeroImpresiones) ? false : true;
                return bHalitarImpresionEtiqueta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int ObtenerConfiguracionNumeroImpresiones()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            int iNumeroImpresiones = 0;
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("spObtenerConfiguracionNumeroImpresiones");
                pars = new SqlParameter[0];
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if (dtObj == null || dtObj.Rows.Count == 0)
                    throw new Exception("No se encontró la configuración del número de impresiones permitidas.");
                foreach (DataRow dr in dtObj.Rows)
                    iNumeroImpresiones = Convert.ToInt32(dr["valor_configuracion"]);
                return iNumeroImpresiones;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerConfiguracionNumeroImpresiones: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
                if (dtObj != null) dtObj.Dispose();
            }
        }
        private int ObtenerNumeroImpresionesEtiqueta(int CodigoPieza)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            int iNumeroImpresionesEtiqueta = 0;
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("spObtenerNumeroImpresionesEtiqueta");
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodigoPieza", CodigoPieza);
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if (dtObj == null || dtObj.Rows.Count == 0) return 0;
                foreach (DataRow dr in dtObj.Rows)
                    iNumeroImpresionesEtiqueta = Convert.ToInt32(dr["NumeroImpresiones"]);
                return iNumeroImpresionesEtiqueta;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerNumeroImpresionesEtiqueta: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
                if (dtObj != null) dtObj.Dispose();
            }
        }
        private void AgregarLogImpresionEtiqueta(int CodigoPieza)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            String nameSP = "spLogImpresionEtiquetaIns";
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodigoPieza", SqlDbType.Int);
                pars[0].Value = CodigoPieza;
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
        }
        private void ActualizarLogImpresionEtiqueta(int CodigoPieza)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            String nameSP = "spLogImpresionEtiquetaUpd";
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append(nameSP);
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodigoPieza", SqlDbType.Int);
                pars[0].Value = CodigoPieza;
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", " + nameSP + ": " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
        }
        public bool TienePermisoReImpresion(int CodigoRol)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("sp_getActionsByScreen");
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_rol", CodigoRol);
                pars[1] = new SqlParameter("@uri", 7);
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if (dtObj == null || dtObj.Rows.Count == 0) return false;
                foreach (DataRow dr in dtObj.Rows)
                    if (Convert.ToInt32(dr["cod_accion"]) == 6)
                        return true;
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", TienePermisoReImpresion: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
                if (dtObj != null) dtObj.Dispose();
            }
        }
        public DataTable ObtenerPieza(string sCodigoBarra)
        {
            try
            {
                if (string.IsNullOrEmpty(sCodigoBarra))
                    throw new Exception("Número de código de barra incorrecto ó vacio.");
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerPiezaSel";
                        cmd.Parameters.AddWithValue("@CodigoBarra", sCodigoBarra);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtPieza = new DataTable("Pieza");
                            da.Fill(dtPieza);
                            return dtPieza;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPieza: " + ex.Message);
            }
        }
        public DataTable ObtenerCapacidadTarima(int? iCodigoArticulo, int? iCodigoCalidad)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerCapacidadTarima";
                        cmd.Parameters.AddWithValue("@CodigoArticulo", iCodigoArticulo);
                        cmd.Parameters.AddWithValue("@CodigoCalidad", iCodigoCalidad);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtCapacidadTarima = new DataTable("CapacidadTarima");
                            da.Fill(dtCapacidadTarima);
                            return dtCapacidadTarima;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCapacidadTarima: " + ex.Message);
            }
        }
        public int EnTarimarPieza(string sCodigoBarraPieza, int iCodigoMaquina, int iCodigoCentroTrabajo, bool bImprimeEtiqueta)
        {
            try
            {
                //, dtCentroTrabajoPorConfigBanco = null, dtConstraintOperacionCentroTrabajo = null
                DataTable pieza = ObtenerPieza(sCodigoBarraPieza), tarima = null;
                if(pieza == null || pieza.Rows.Count == 0) throw new Exception("No se encontró la pieza que se desea entarimar.");
                if(Convert.ToInt32(pieza.Rows[0]["CodigoUltimoProceso"]) == 1) throw new Exception("La pieza no tiene estado en proceso.");
                //dtCentroTrabajoPorConfigBanco = ObtenerCentroTrabajoPorConfigBanco((pieza.Rows[0]["CodigoConfiguracionBanco"] == DBNull.Value) ? 0 : Convert.ToInt32(pieza.Rows[0]["CodigoConfiguracionBanco"]));
                //if (dtCentroTrabajoPorConfigBanco != null && dtCentroTrabajoPorConfigBanco.Rows.Count > 0)
                //    dtConstraintOperacionCentroTrabajo = ObtenerConstraintOperacionCentroTrabajo(Convert.ToInt32(pieza.Rows[0]["CodigoPlanta"]), Convert.ToInt32(dtCentroTrabajoPorConfigBanco.Rows[0]["CodigoCentroTrabajo"]));
                int iTarima = ObtenerTarimaPieza(sCodigoBarraPieza);
                if (iTarima != 0) throw new Exception("La pieza ya esta entarimada en la tarima:" + iTarima.ToString() + " .");
                DataTable capacidadTarima = ObtenerCapacidadTarima(Convert.ToInt32(pieza.Rows[0]["CodigoArticulo"]), Convert.ToInt32(pieza.Rows[0]["CodigoCalidad"]));
                if (capacidadTarima == null || capacidadTarima.Rows.Count == 0) throw new Exception("No se encontró la capacidad de la tarima.");                
                //if(dtConstraintOperacionCentroTrabajo!= null && dtConstraintOperacionCentroTrabajo.Rows.Count > 0)
                //Convert.ToInt32(dtConstraintOperacionCentroTrabajo.Rows[0]["CodigoCentroTrabajo"])
                tarima = ObtenerTarimaDisponible(Convert.ToInt32(pieza.Rows[0]["CodigoPlanta"]), Convert.ToInt32(pieza.Rows[0]["CodigoArticulo"]), Convert.ToInt32(pieza.Rows[0]["CodigoColor"]), Convert.ToInt32(pieza.Rows[0]["CodigoCalidad"]), iCodigoMaquina, Convert.ToInt32(pieza.Rows[0]["CodigoConfiguracionBanco"]));
                //else
                    //tarima = ObtenerTarimaDisponible(Convert.ToInt32(pieza.Rows[0]["CodigoPlanta"]), Convert.ToInt32(pieza.Rows[0]["CodigoArticulo"]), Convert.ToInt32(pieza.Rows[0]["CodigoColor"]), Convert.ToInt32(pieza.Rows[0]["CodigoCalidad"]), iCodigoMaquina, null);
                if (tarima == null || tarima.Rows.Count == 0)
                { 
                    tarima = EntarimarPieza(null, Convert.ToInt32(pieza.Rows[0]["CodigoPieza"]), iCodigoMaquina);
                    if (tarima == null || tarima.Rows.Count == 0)
                        throw new Exception("No se puede entarimar la pieza.");
                }
                else
                {
                    int iPiezasEntarimadas = Convert.ToInt32(tarima.Rows[0]["PiezasEntarimadas"]);
                    tarima = EntarimarPieza(Convert.ToInt32(tarima.Rows[0]["CodigoTarima"]), Convert.ToInt32(pieza.Rows[0]["CodigoPieza"]), iCodigoMaquina);
                    if (tarima == null || tarima.Rows.Count == 0) throw new Exception("No se puede entarimar la pieza.");
                    iPiezasEntarimadas += 1;
                    if (iPiezasEntarimadas >= Convert.ToInt32(capacidadTarima.Rows[0]["Capacidad"]))
                    {
                        if (CerrarTarima(Convert.ToInt32(tarima.Rows[0]["CodigoTarima"])) & bImprimeEtiqueta)
                        {
                            Etiqueta etiqueta = ObtenerConfiguracionEtiqueta(pieza, Convert.ToInt32(tarima.Rows[0]["CodigoTarima"]));
                            ConfigImpresora configImpresora = ObtenerConfiguracionImpresora(Convert.ToInt32(pieza.Rows[0]["CodigoPlanta"]), iCodigoMaquina, iCodigoCentroTrabajo);
                            if (etiqueta == null || configImpresora == null) return Convert.ToInt32(tarima.Rows[0]["CodigoTarima"]);
                            etiqueta.TipoEtiqueta = 2;
                            ImprimirEtiqueta(configImpresora, etiqueta);
                        }
                    }
                }
                return Convert.ToInt32(tarima.Rows[0]["CodigoTarima"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CerrarTarima(int iCodigoTarima)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spCerrarTarima";
                        cmd.Parameters.AddWithValue("@CodigoTarima", iCodigoTarima);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTarima = new DataTable("Tarima");
                            da.Fill(dtTarima);
                            return (dtTarima == null || dtTarima.Rows.Count == 0) ? false : Convert.ToBoolean(dtTarima.Rows[0]["Estado"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", CerrarTarima: " + ex.Message);
            }
        }
        public bool AbrirTarima(int iCodigoTarima)
        {
            try
            {
                DataTable tarima = ObtenerTarimaPieza(iCodigoTarima, null);
                if (tarima == null || tarima.Rows.Count == 0)
                    throw new Exception("No existe la tarima capturada.");
                DataTable pieza = ObtenerPieza(tarima.Rows[0]["CodigoBarra"].ToString());
                if (pieza == null || pieza.Rows.Count == 0)
                    throw new Exception("Problemas al consultar piezas de la tarima: " + tarima.Rows[0]["CodigoTarima"].ToString());
                DataTable capacidadTarima = ObtenerCapacidadTarima(Convert.ToInt32(pieza.Rows[0]["CodigoArticulo"]), Convert.ToInt32(pieza.Rows[0]["CodigoCalidad"]));
                if (capacidadTarima == null || capacidadTarima.Rows.Count == 0)
                    throw new Exception("No se encontró la capacidad de la tarima.");
                //if (!(Convert.ToInt32(capacidadTarima.Rows[0]["Capacidad"]) > tarima.Rows.Count))
                //    throw new Exception("La tarima ya tiene su capacidad maxima de piezas(Capacidad = " + capacidadTarima.Rows[0]["Capacidad"].ToString() + ").");
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spAbrirTarima";
                        cmd.Parameters.AddWithValue("@CodigoTarima", iCodigoTarima);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTarima = new DataTable("Tarima");
                            da.Fill(dtTarima);
                            return (dtTarima == null || dtTarima.Rows.Count == 0) ? false : Convert.ToBoolean(dtTarima.Rows[0]["Estado"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ObtenerTarimaPieza(string sCodigoBarraPieza)
        {
            try
            {
                if (string.IsNullOrEmpty(sCodigoBarraPieza))
                    throw new Exception("Número de código de barra incorrecto ó vacio.");
                DataTable pieza = ObtenerPieza(sCodigoBarraPieza);
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerTarimaPieza";
                        cmd.Parameters.AddWithValue("@CodigoPieza", Convert.ToInt32(pieza.Rows[0]["CodigoPieza"]));
                        pieza.Dispose();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTarima = new DataTable("Tarima");
                            da.Fill(dtTarima);
                            return (dtTarima == null || dtTarima.Rows.Count == 0) ? 0 : Convert.ToInt32(dtTarima.Rows[0]["CodigoTarima"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTarimaPieza: " + ex.Message);
            }
        }
        public bool EsTarimaValida(int iCodigoTarima)
        {
            try
            {
                DataTable tarima = ObtenerTarimaPieza(iCodigoTarima, null);
                if (tarima == null || tarima.Rows.Count == 0)
                    throw new Exception("No existe la tarima capturada.");
                DataTable pieza = ObtenerPieza(tarima.Rows[0]["CodigoBarra"].ToString());
                if (pieza == null || pieza.Rows.Count == 0)
                    throw new Exception("Problemas al consultar piezas de la tarima: " + tarima.Rows[0]["CodigoTarima"].ToString());
                DataTable capacidadTarima = ObtenerCapacidadTarima(Convert.ToInt32(pieza.Rows[0]["CodigoArticulo"]), Convert.ToInt32(pieza.Rows[0]["CodigoCalidad"]));
                if (capacidadTarima == null || capacidadTarima.Rows.Count == 0)
                    throw new Exception("No se encontró la capacidad de la tarima.");
                if (!(Convert.ToInt32(capacidadTarima.Rows[0]["Capacidad"]) > tarima.Rows.Count))
                    throw new Exception("La tarima ya tiene su capacidad maxima de piezas(Capacidad = " + capacidadTarima.Rows[0]["Capacidad"].ToString() + ").");
                if (Convert.ToInt32(tarima.Rows[0]["Estado"]) == 0)
                    throw new Exception("Tarima cerrada.");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DesEnTarimar(int iCodigoPieza)
        {
            try
            {
                DataTable tarima = ObtenerTarimaPieza(null, iCodigoPieza);
                if (tarima == null || tarima.Rows.Count == 0)
                    throw new Exception("La pieza no se encuentra entarimada.");
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spDesEntarimar";
                        cmd.Parameters.AddWithValue("@CodigoTarima", Convert.ToInt32(tarima.Rows[0]["CodigoTarima"]));
                        cmd.Parameters.AddWithValue("@CodigoPieza", iCodigoPieza);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTarima = new DataTable("Tarima");
                            da.Fill(dtTarima);
                            return (dtTarima == null || dtTarima.Rows.Count == 0) ? false : Convert.ToBoolean(dtTarima.Rows[0]["DesEntarimado"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private Etiqueta ObtenerConfiguracionEtiqueta(DataTable pieza, int iCodigoTarima)
        {
            if (pieza == null || pieza.Rows.Count == 0) return new Etiqueta();
            Etiqueta eEtiqueta = new Etiqueta();
            eEtiqueta.Clave = pieza.Rows[0]["ClaveArticulo"].ToString() + "-" + pieza.Rows[0]["ClaveCalidad"].ToString();
            eEtiqueta.Cod = Convert.ToInt32(pieza.Rows[0]["CodigoArticulo"]);
            eEtiqueta.Pieza = pieza.Rows[0]["CodigoBarra"].ToString();
            eEtiqueta.Tarima = iCodigoTarima.ToString();
            return eEtiqueta;
        }
        private ConfigImpresora ObtenerConfiguracionImpresora(int iCodigoPlanta, int iCodigoMaquina, int iCodigoCentroTrabajo)
        {
            ConfigImpresora cConfigImpresora = new ConfigImpresora();
            cConfigImpresora.CodPlanta = iCodigoPlanta;
            cConfigImpresora.CodMaquina = iCodigoMaquina;
            cConfigImpresora.CodCentroTrabajo = iCodigoCentroTrabajo;
            return cConfigImpresora;
        }
        private DataTable ObtenerTarimaPieza(int? iCodigoTarima, int? iCodigoPieza)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerTarimaPieza";
                        if (iCodigoTarima.HasValue)
                            cmd.Parameters.AddWithValue("@CodigoTarima", iCodigoTarima.Value);
                        if(iCodigoPieza.HasValue)
                            cmd.Parameters.AddWithValue("@CodigoPieza", iCodigoPieza.Value);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTarima = new DataTable("Tarima");
                            da.Fill(dtTarima);
                            return dtTarima;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTarimaPieza: " + ex.Message);
            }
        }
        private DataTable ObtenerTarimaPieza(int iCodigoPlanta, int iCodigoArticulo, int iCodigoColor, int iCodigoCalidad)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerTarimaPieza";
                        cmd.Parameters.AddWithValue("@CodigoPlanta", iCodigoPlanta);
                        cmd.Parameters.AddWithValue("@CodigoArticulo", iCodigoArticulo);
                        cmd.Parameters.AddWithValue("@CodigoColor", iCodigoColor);
                        cmd.Parameters.AddWithValue("@CodigoCalidad", iCodigoCalidad);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTarima = new DataTable("Tarima");
                            da.Fill(dtTarima);
                            return dtTarima;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTarimaPieza: " + ex.Message);
            }
        }
        private DataTable ObtenerTarimaDisponible(int iCodigoPlanta, int iCodigoArticulo, int iCodigoColor, int iCodigoCalidad, int iCodigoMaquina, int iCodigoConfiguracionBanco)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerTarimaDisponible";
                        cmd.Parameters.AddWithValue("@CodigoPlanta", iCodigoPlanta);
                        cmd.Parameters.AddWithValue("@CodigoArticulo", iCodigoArticulo);
                        cmd.Parameters.AddWithValue("@CodigoColor", iCodigoColor);
                        cmd.Parameters.AddWithValue("@CodigoCalidad", iCodigoCalidad);
                        cmd.Parameters.AddWithValue("@CodigoMaquina", iCodigoMaquina);
                        //if (iCodigoConfiguracionBanco.HasValue)
                        cmd.Parameters.AddWithValue("@CodigoConfiguracionBanco", iCodigoConfiguracionBanco);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTarima = new DataTable("Tarima");
                            da.Fill(dtTarima);
                            return dtTarima;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTarimaDisponible: " + ex.Message);
            }
        }
        private DataTable EntarimarPieza(int? iCodigoTarima, int iCodigoPieza, int iCodigoMaquina)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spEnTarimarPieza";
                        if (iCodigoTarima.HasValue)
                            cmd.Parameters.AddWithValue("@CodigoTarima", iCodigoTarima.Value);
                        cmd.Parameters.AddWithValue("@CodigoPieza", iCodigoPieza);
                        cmd.Parameters.AddWithValue("@CodigoMaquina", iCodigoMaquina);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTarima = new DataTable("Tarima");
                            da.Fill(dtTarima);
                            return dtTarima;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTarima: " + ex.Message);
            }
        }
        public bool EsValidaTarimaImportar(int iCodigoTarima)
        {
            DataTable dtTarima = null;
            int iCantidad = 0, iCapacidad = 0;
            try
            {
                dtTarima = ObtenerTarima(iCodigoTarima, false, 0);
                if (dtTarima == null || dtTarima.Rows.Count == 0)
                    throw new Exception("La tarima " + iCodigoTarima.ToString() + " no existe.");
                iCantidad = Convert.ToInt32(dtTarima.Rows[0]["Cantidad"]);
                iCapacidad = Convert.ToInt32(dtTarima.Rows[0]["Capacidad"]);
                if (iCantidad >= iCapacidad)
                    throw new Exception("La tarima " + iCodigoTarima.ToString() + " no tiene capacidad disponible.");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                dtTarima = null;
            }
        }
        public DataTable ObtenerPiezaEnTarima(int iCodigoTarima)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerPiezaEnTarima";
                        if (iCodigoTarima <= 0) return null;
                        cmd.Parameters.AddWithValue("@CodigoTarima", iCodigoTarima);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTarima = new DataTable("Tarima");
                            da.Fill(dtTarima);
                            return dtTarima;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezaEnTarima: " + ex.Message);
            }
        }
        public DataTable ObtenerTarima(int? iCodigoTarima, bool AplicaFiltro, int iCodigoPlanta)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerTarima";
                        if (iCodigoTarima.HasValue)
                            cmd.Parameters.AddWithValue("@CodigoTarima", iCodigoTarima.Value);
                        cmd.Parameters.AddWithValue("@Filtro", (AplicaFiltro) ? 1 : 0);
                        if(iCodigoPlanta > 0)
                            cmd.Parameters.AddWithValue("@CodigoPlanta", iCodigoPlanta);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTarima = new DataTable("Tarima");
                            da.Fill(dtTarima);
                            return dtTarima;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerTarima: " + ex.Message);
            }
        }
        public bool ImportarTarima(int iCodigoTarima01, int iCodigoTarima02, int iCodigoTarimaDestino)
        {
            DataTable dtTarima01 = null, dtTarima02 = null, dtTarimaDetalleOrigen = null, dtTarimaDetalleDestino = null, dtTarimaDestino = null;
            int iCapacidad = 0, iCantidadOrigen = 0, iCantidadDestino = 0, iCodigoMaquina = 0;
            bool bflag = false;
            try
            {
                bflag = EsValidaTarimaImportar(iCodigoTarima01);
                if (!bflag)
                    throw new Exception("La importación no se ejecutó, debido a que la tarima " + iCodigoTarima01.ToString() + " no es valida.");
                bflag = EsValidaTarimaImportar(iCodigoTarima02);
                if (!bflag)
                    throw new Exception("La importación no se ejecutó, debido a que la tarima " + iCodigoTarima02.ToString() + " no es valida.");
                bflag = false;
                dtTarima01 = ObtenerTarima(iCodigoTarima01, false, 0);
                dtTarima02 = ObtenerTarima(iCodigoTarima02, false, 0);
                if (dtTarima01 == null || dtTarima02 == null || dtTarima01.Rows.Count == 0 || dtTarima02.Rows.Count == 0)
                    throw new Exception("La importación no se ejecutó, debido a que una de las tarimas " + iCodigoTarima01.ToString() + ", " + iCodigoTarima02.ToString() + " no es valida.");
                if (dtTarima01.Rows[0]["ClaveSKU"].ToString() != dtTarima02.Rows[0]["ClaveSKU"].ToString())
                    throw new Exception("La importación no se ejecutó, debido que no coinciden los productos de las tarimas, tarima 1.- " + iCodigoTarima01.ToString() + " - producto: " + dtTarima01.Rows[0]["ClaveSKU"].ToString() + ", tarima 2.- " + iCodigoTarima02.ToString() + " - producto: " + dtTarima02.Rows[0]["ClaveSKU"].ToString() + ".");
                if (iCodigoTarima01 == iCodigoTarimaDestino)
                {
                    iCantidadDestino = Convert.ToInt32(dtTarima01.Rows[0]["Cantidad"]);
                    iCantidadOrigen = Convert.ToInt32(dtTarima02.Rows[0]["Cantidad"]);
                    iCapacidad = Convert.ToInt32(dtTarima01.Rows[0]["Capacidad"]);
                    dtTarimaDetalleDestino = ObtenerPiezaEnTarima(iCodigoTarima01);
                    if(dtTarimaDetalleDestino == null || dtTarimaDetalleDestino.Rows.Count == 0)
                        throw new Exception("La tarima destino " + iCodigoTarima01.ToString() + " debe contener almenos una pieza entarimada.");
                    dtTarimaDestino = ObtenerTarima(iCodigoTarima01, false, 0);
                    if (dtTarimaDestino == null || dtTarimaDestino.Rows.Count == 0)
                        throw new Exception("La tarima destino " + iCodigoTarima01.ToString() + " no existe.");
                    iCodigoMaquina = Convert.ToInt32(dtTarimaDestino.Rows[0]["CodigoMaquina"]);
                    dtTarimaDetalleOrigen = ObtenerPiezaEnTarima(iCodigoTarima02);
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        foreach (DataRow row in dtTarimaDetalleOrigen.Rows)
                        {
                            if (iCantidadDestino < iCapacidad)
                            {
                                int iCodigoPieza = ObtenerCodPiezaCodBarras(row["CodigoBarra"].ToString());
                                DesEnTarimar(iCodigoPieza);
                                EntarimarPieza(iCodigoTarima01, iCodigoPieza, iCodigoMaquina);
                                iCantidadDestino += 1;
                                iCantidadOrigen -= 1;
                                if (iCantidadDestino == iCapacidad)
                                    CerrarTarima(iCodigoTarima01);
                                bflag = true;
                                continue;
                            }
                            break;
                        }
                        if (iCantidadOrigen == 0)
                            CerrarTarima(iCodigoTarima02);
                        if (bflag)
                        {
                            transactionScope.Complete();
                            return bflag;
                        }
                    }
                }
                else if (iCodigoTarima02 == iCodigoTarimaDestino)
                {
                    iCantidadDestino = Convert.ToInt32(dtTarima02.Rows[0]["Cantidad"]);
                    iCantidadOrigen = Convert.ToInt32(dtTarima01.Rows[0]["Cantidad"]);
                    iCapacidad = Convert.ToInt32(dtTarima02.Rows[0]["Capacidad"]);
                    dtTarimaDetalleDestino = ObtenerPiezaEnTarima(iCodigoTarima02);
                    if (dtTarimaDetalleDestino == null || dtTarimaDetalleDestino.Rows.Count == 0)
                        throw new Exception("La tarima destino " + iCodigoTarima02.ToString() + " debe contener almenos una pieza entarimada.");
                    dtTarimaDestino = ObtenerTarima(iCodigoTarima02, false, 0);
                    if (dtTarimaDestino == null || dtTarimaDestino.Rows.Count == 0)
                        throw new Exception("La tarima destino " + iCodigoTarima02.ToString() + " no existe.");
                    iCodigoMaquina = Convert.ToInt32(dtTarimaDestino.Rows[0]["CodigoMaquina"]);
                    dtTarimaDetalleOrigen = ObtenerPiezaEnTarima(iCodigoTarima01);
                    using (TransactionScope transactionScope = new TransactionScope())
                    {
                        foreach (DataRow row in dtTarimaDetalleOrigen.Rows)
                        {
                            if (iCantidadDestino < iCapacidad)
                            {
                                int iCodigoPieza = ObtenerCodPiezaCodBarras(row["CodigoBarra"].ToString());
                                DesEnTarimar(iCodigoPieza);
                                EntarimarPieza(iCodigoTarima02, iCodigoPieza, iCodigoMaquina);
                                iCantidadDestino += 1;
                                iCantidadOrigen -= 1;
                                if (iCantidadDestino == iCapacidad)
                                    CerrarTarima(iCodigoTarima02);
                                bflag = true;
                                continue;
                            }
                            break;
                        }
                        if (iCantidadOrigen == 0)
                            CerrarTarima(iCodigoTarima01);
                        if (bflag)
                        {
                            transactionScope.Complete();
                            return bflag;
                        }
                    }
                }
                else
                {
                    throw new Exception("No coincide ninguna de las tarimas " + iCodigoTarima01 + ", " + iCodigoTarima02 + " con la tarima destino " + iCodigoTarimaDestino + ".");
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EnTarimadoPieza(int iCodigoTarima, string sCodigoBarraPieza)
        {
            try
            {
                DataTable pieza = ObtenerPieza(sCodigoBarraPieza);
                DataTable dtTarima = ObtenerTarima(iCodigoTarima, false, 0);
                if (dtTarima == null || dtTarima.Rows.Count == 0)
                    throw new Exception("No se encontró la tarima " + iCodigoTarima.ToString() +  ", en la que se desea entarimar.");
                int iCodigoMaquina = Convert.ToInt32(dtTarima.Rows[0]["CodigoMaquina"]);
                int capacidadTarima = Convert.ToInt32(dtTarima.Rows[0]["Capacidad"]);
                int iPiezasEntarimadas = Convert.ToInt32(dtTarima.Rows[0]["Cantidad"]);
                if (pieza == null || pieza.Rows.Count == 0)
                    throw new Exception("No se encontró la pieza que se desea entarimar.");
                if (Convert.ToInt32(pieza.Rows[0]["CodigoUltimoProceso"]) == 1)
                    throw new Exception("La pieza no tiene estado en proceso.");
                if (capacidadTarima <= 0)
                    throw new Exception("No se encontró la capacidad de la tarima.");
                if(iPiezasEntarimadas >= capacidadTarima)
                    throw new Exception("No se puede exceder el limite de capacidad de la tarima.\n Tarima: " + iCodigoTarima.ToString() + ", Capacidad: " + capacidadTarima.ToString() + ", Piezas en tarima: " + iPiezasEntarimadas.ToString() + "." );
                int iTarima = ObtenerTarimaPieza(sCodigoBarraPieza);
                if (iTarima != 0)
                    throw new Exception("La pieza ya esta entarimada en la tarima:" + iTarima.ToString() + ".");
                dtTarima = EntarimarPieza(iCodigoTarima, Convert.ToInt32(pieza.Rows[0]["CodigoPieza"]), iCodigoMaquina);
                if (dtTarima == null || dtTarima.Rows.Count == 0)
                    throw new Exception("No se puede entarimar la pieza.");
                iPiezasEntarimadas += 1;
                if (iPiezasEntarimadas >= capacidadTarima)
                    CerrarTarima(iCodigoTarima);
                return iCodigoTarima;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DataTable ObtenerConstraintOperacionCentroTrabajo(int? iCodigoPlanta, int? iCodigoCentroTrabajo)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerConstraintOperacionCentroTrabajo";
                        if (iCodigoPlanta.HasValue)
                            cmd.Parameters.AddWithValue("@CodigoPlanta", iCodigoPlanta);
                        if (iCodigoCentroTrabajo.HasValue)
                            cmd.Parameters.AddWithValue("@CodigoCentroTrabajo", iCodigoCentroTrabajo);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtConstraintOperacionCentroTrabajo = new DataTable("ConstraintCentroTrabajo");
                            da.Fill(dtConstraintOperacionCentroTrabajo);
                            return dtConstraintOperacionCentroTrabajo;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerConstraintOperacionCentroTrabajo: " + ex.Message);
            }
        }
        private DataTable ObtenerCentroTrabajoPorConfigBanco(int? iCodigoConfigBanco)
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerCentroTrabajoPorConfigBanco";
                        if (iCodigoConfigBanco.HasValue)
                            cmd.Parameters.AddWithValue("@CodigoConfigBanco", iCodigoConfigBanco);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtCentroTrabajoPorConfigBanco = new DataTable("CentroTrabajoPorConfigBanco");
                            da.Fill(dtCentroTrabajoPorConfigBanco);
                            return dtCentroTrabajoPorConfigBanco;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCentroTrabajoPorConfigBanco: " + ex.Message);
            }
        }
        public int ObtenerTiempoEnMinutosCapturaColor()
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerTiempoColorEsmaltado";
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtTiempoEnMinutosCapturaColor = new DataTable("TiempoColorEsmaltado");
                            return (dtTiempoEnMinutosCapturaColor != null && dtTiempoEnMinutosCapturaColor.Rows.Count > 0) ? Convert.ToInt32(dtTiempoEnMinutosCapturaColor.Rows[0]["TiempoColorEsmaltado"]) : 20;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Methods
    }
}
