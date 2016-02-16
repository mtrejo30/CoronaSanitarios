using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using DA01 = Common.DataAccess;
using System.Data.SqlClient;

using Common.SolutionEntityFramework;
using BE = LAMOSA.SCPP.Server.BusinessEntity;
using System.Configuration;
using LAMOSA.SCPP.Server.BusinessEntity;
using System.Transactions;
using System.Transactions.Configuration;

namespace LAMOSA.SCPP.Server.BusinessComponent
{
    public class SCPP
    {

        #region Fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #region Cadenas de conexion

        // Cadena de conexion SQL Server.
        private string sMSSQLServer_ConnectionString = string.Empty; // Properties.Settings.Default.MSSQLServer_ConnectionString;

        #endregion Cadenas de conexion

        #endregion Fields

        #region Methods

        #region Constructors and Destructor
        public SCPP()
        {
            sMSSQLServer_ConnectionString = ObtenerCadenaConexion();
            this.sClassName = this.GetType().FullName;
        }
        ~SCPP()
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
                    if (!(connectionString.Name == "csLamosaSCPP" | connectionString.Name == "lamosaConnectionString")) continue;
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

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString1 = new StringBuilder();
                queryString1.Append("sp_RegistroSolicitud_Ins");

                #endregion Query

                #region Parameters

                pars1 = new SqlParameter[4];
                pars1[0] = new SqlParameter("@DesMetodo", SqlDbType.NVarChar, 255);
                pars1[0].Value = sDesMetodo;
                pars1[1] = new SqlParameter("@FechaHoraSolicitud", SqlDbType.DateTime);
                pars1[1].Value = dtFechaHoraSolicitud;
                pars1[2] = new SqlParameter("@Parametros", SqlDbType.NVarChar, 1000);
                pars1[2].Value = sParametros;
                pars1[3] = new SqlParameter("@CodRegistro", SqlDbType.BigInt);
                pars1[3].Direction = ParameterDirection.Output;

                #endregion Parameters

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString1.ToString(), pars1);
                lCodRegistro = Convert.ToInt64(pars1[3].Value);

                #endregion Query Execution

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

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_RegistroSolicitud_Upd");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@CodRegistro", SqlDbType.BigInt);
                pars[0].Value = lCodRegistro;
                pars[1] = new SqlParameter("@EjecucionExitosa", SqlDbType.Bit);
                pars[1].Value = bEjecucionExitosa;
                pars[2] = new SqlParameter("@Error", SqlDbType.NVarChar, 2000);
                pars[2].Value = sError;

                #endregion Parameters

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

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

        #region ObtenerTurnos
        public SolutionEntityList<BE.Turno> ObtenerTurnos()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Turno> l_Res = new SolutionEntityList<BE.Turno>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerTurnos", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_Turno_Lis");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Turno(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerTurnos: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerTurnos
        #region ObtenerCalidades
        public SolutionEntityList<BE.Calidad> ObtenerCalidades()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Calidad> l_Res = new SolutionEntityList<BE.Calidad>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCalidades", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_Calidad_Lis");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Calidad(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerCalidades: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerCalidades
        #region ObtenerCalidadesCbo
        public SolutionEntityList<BE.Calidad> ObtenerCalidadesCbo()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Calidad> l_Res = new SolutionEntityList<BE.Calidad>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCalidadesCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_Calidad_Cbo");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Calidad(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerCalidadesCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerCalidadesCbo
        #region ObtenerPlantaCalidadCbo
        public SolutionEntityList<BE.PlantaCalidad> ObtenerPlantaCalidadCbo(int CodPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.PlantaCalidad> l_Res = new SolutionEntityList<BE.PlantaCalidad>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPlantaCalidadCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("spPlantaCalidad_Cbo");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = CodPlanta;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.PlantaCalidad(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerPlantaCalidadCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerPlantaCalidadCbo
        #region ObtenerDefectosCbo
        public SolutionEntityList<BE.DefectoCbo> ObtenerDefectoCbo(int iCodProceso, int iCodTipoDefecto)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.DefectoCbo> l_Res = new SolutionEntityList<BE.DefectoCbo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerDefectoCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("spDefecto_Cbo");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodProceso", iCodProceso == -1 ? DBNull.Value : (object)iCodProceso);
                pars[1] = new SqlParameter("@CodTipoDefecto", iCodTipoDefecto == -1 ? DBNull.Value : (object)iCodTipoDefecto);

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.DefectoCbo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerDefectoCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerDefectosCbo
        #region ObtenerZonaDefectoCbo
        public SolutionEntityList<BE.ZonaDefectoCbo> ObtenerZonaDefectoCbo()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.ZonaDefectoCbo> l_Res = new SolutionEntityList<BE.ZonaDefectoCbo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerZonaDefectoCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("spZonaDefecto_Cbo");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.ZonaDefectoCbo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerZonaDefectoCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerZonaDefectoCbo
        #region ObtenerColores
        public SolutionEntityList<BE.Color> ObtenerColores()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Color> l_Res = new SolutionEntityList<BE.Color>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerColores", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_Color_Lis");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Color(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerColores: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerColores
        #region ObtenerArticulos
        public SolutionEntityList<BE.Articulo> ObtenerArticulos(BE.ArticuloPars articuloPars)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Articulo> l_Res = new SolutionEntityList<BE.Articulo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                sPars += "CodTipoArticulo=" + articuloPars.CodTipoArticulo.ToString();
                sPars += ", CodMolde=" + articuloPars.CodMolde.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerArticulos", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_Articulo_Lis");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodTipoArticulo", SqlDbType.Int);
                pars[0].Value = articuloPars.CodTipoArticulo;
                pars[1] = new SqlParameter("@CodMolde", SqlDbType.Int);
                pars[1].Value = articuloPars.CodMolde;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Articulo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerArticulos: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerArticulos
        #region ObtenerArticulosCbo
        public SolutionEntityList<BE.ArticuloCbo> ObtenerArticulosCbo(int tipoarticulo)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.ArticuloCbo> l_Res = new SolutionEntityList<BE.ArticuloCbo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                sPars += "CodTipoArticulo=" + tipoarticulo.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerArticulosCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_Articulo_Cbo");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodTipoArticulo", SqlDbType.Int);
                pars[0].Value = tipoarticulo;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.ArticuloCbo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerArticulosCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion

        #region ObtenerMoldesCbo
        public SolutionEntityList<BE.ArticuloCbo> ObtenerModelosCbo(int tipoarticulo)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.ArticuloCbo> l_Res = new SolutionEntityList<BE.ArticuloCbo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                sPars += "CodTipoArticulo=" + tipoarticulo.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerModelosCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("spGetArticuloCbo");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodTipoArticulo", SqlDbType.Int);
                pars[0].Value = tipoarticulo;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.ArticuloCbo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerModelosCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion

        #region ObtenerTiposArticuloCbo
        public SolutionEntityList<BE.TipoArticuloCbo> ObtenerTiposArticuloCbo()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.TipoArticuloCbo> l_Res = new SolutionEntityList<BE.TipoArticuloCbo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerTiposArticuloCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_TipoArticulo_Cbo");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.TipoArticuloCbo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerTiposArticuloCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerTiposArticuloCbo
        #region ObtenerModelo
        public string ObtenerModelo(string sCodBarras, int iPlanta, int iProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            string l_Res = string.Empty;
            DataRow drUltimoProcesoPieza = null;
            DataTable dtPieza = null;
            BE.Proceso ProcesoAnterior = null;
            int iUltimoCodigoProceso = 0;
            string sCalidad = string.Empty;
            try
            {
                drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(sCodBarras, iPlanta);
                dtPieza = ObtenerPieza(sCodBarras);
                ProcesoAnterior = ObtenerProcesoAnterior(iProceso);
                if (drUltimoProcesoPieza == null) return "0|0|" + "No existe pieza." + "|";
                if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No existe pieza." + "|";
                iUltimoCodigoProceso = Convert.ToInt32(drUltimoProcesoPieza["CodProceso"]);
                if (dtPieza != null && dtPieza.Rows.Count != 0 && dtPieza.Rows[0]["Calidad"] != DBNull.Value)
                    sCalidad = Convert.ToString(dtPieza.Rows[0]["Calidad"]);
                if (ProcesoAnterior == null) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No se tiene definido proceso anterior." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if (ProcesoAnterior.CodProceso <= 0) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No se tiene definido proceso anterior." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if(!(iUltimoCodigoProceso == 6 & string.IsNullOrEmpty(sCalidad)))
                    if ((iUltimoCodigoProceso < ProcesoAnterior.CodProceso | iUltimoCodigoProceso > ProcesoAnterior.CodProceso))
                        return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + Convert.ToString(drUltimoProcesoPieza["DesProceso"]) + "." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);                
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("spModelo_Sel");
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodArticulo", SqlDbType.Int);
                pars[0].Value = Convert.ToInt32(drUltimoProcesoPieza["CodArticulo"]);
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if (dtObj == null) throw new Exception("No se puede determinar el modelo para la pieza; Codigo Pieza: " + iUltimoCodigoProceso.ToString() + ", Codigo Articulo: " + Convert.ToString(drUltimoProcesoPieza["CodArticulo"]) + ".");
                if (dtObj.Rows.Count <= 0) throw new Exception("No se puede determinar el modelo para la pieza; Codigo Pieza: " + iUltimoCodigoProceso.ToString() + ", Codigo Articulo: " + Convert.ToString(drUltimoProcesoPieza["CodArticulo"]) + ".");
                return "1|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + Convert.ToString(dtObj.Rows[0]["DesArticulo"]) + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerModelo: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
                if (dtPieza != null) dtPieza.Dispose();
            }
        }
        #endregion
        #region ObtenerUltimoProcesoPieza
        private DataRow ObtenerUltimoProcesoPieza(string sCodBarras, int? iPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerUltimoProcesoPieza", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spUltimoProcesoPieza_Sel");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iPlanta;
                pars[1] = new SqlParameter("@CodBarras", SqlDbType.VarChar);
                pars[1].Value = sCodBarras;
                #endregion Parameters

                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null) return null;
                if (dtObj.Rows.Count > 0)
                    return dtObj.Rows[0];
                else
                    return dtObj.NewRow();
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerUltimoProcesoPieza: " + ex.Message);
            }
            finally
            {
                if (dbObj != null)
                    dbObj.Dispose();
            }
        }
        #endregion
        #region ObtenerProcesoAnterior
        public BE.Proceso ObtenerProcesoAnterior(int iProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            BE.Proceso l_Res = null;
            //
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerProcesoAnterior", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spProcesoAnterior_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iProceso;
                #endregion Parameters
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res = new BE.Proceso(dr);
                    break;
                }
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerProcesoAnterior: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerTipo
        public string ObtenerTipo(string sCodBarras, int iPlanta, int iProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            string l_Res = string.Empty;
            DataRow drUltimoProcesoPieza = null;
            DataTable dtPieza = null;
            BE.Proceso ProcesoAnterior = null;
            int iUltimoCodigoProceso = 0;
            string sCalidad = string.Empty;
            try
            {
                drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(sCodBarras, iPlanta);
                dtPieza = ObtenerPieza(sCodBarras);
                ProcesoAnterior = ObtenerProcesoAnterior(iProceso);
                if (drUltimoProcesoPieza == null) return "0|0|" + "No existe pieza." + "|0";
                if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No existe pieza." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                iUltimoCodigoProceso = Convert.ToInt32(drUltimoProcesoPieza["CodProceso"]);
                if (dtPieza != null && dtPieza.Rows.Count != 0 && dtPieza.Rows[0]["Calidad"] != DBNull.Value)
                    sCalidad = Convert.ToString(dtPieza.Rows[0]["Calidad"]);
                if (ProcesoAnterior == null) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No se tiene definido proceso anterior." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if (ProcesoAnterior.CodProceso <= 0) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No se tiene definido proceso anterior." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if (!(iUltimoCodigoProceso == 6 & string.IsNullOrEmpty(sCalidad)))
                    if (iUltimoCodigoProceso < ProcesoAnterior.CodProceso | iUltimoCodigoProceso > ProcesoAnterior.CodProceso)
                        return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + Convert.ToString(drUltimoProcesoPieza["DesProceso"]) + "." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerTipo", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spModelo_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodArticulo", SqlDbType.Int);
                pars[0].Value = Convert.ToInt32(drUltimoProcesoPieza["CodArticulo"]);
                #endregion Parameters
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null) throw new Exception("No se puede determinar el color para la pieza; Codigo Pieza: " + iUltimoCodigoProceso.ToString() + ", Codigo Articulo: " + Convert.ToString(drUltimoProcesoPieza["CodArticulo"]) + ".");
                if (dtObj.Rows.Count <= 0) throw new Exception("No se puede determinar el color para la pieza; Codigo Pieza: " + iUltimoCodigoProceso.ToString() + ", Codigo Articulo: " + Convert.ToString(drUltimoProcesoPieza["CodArticulo"]) + ".");
                return "1|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + Convert.ToString(dtObj.Rows[0]["DesTipoArticulo"]) + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerTipo: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
                if (dtPieza != null) dtPieza.Dispose();
            }
        }
        #endregion
        #region ObtenerColor
        public string ObtenerColor(string sCodBarras, int iPlanta, int iProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            DataTable dtPieza = null;
            long lCodRegistro = -1;
            string l_Res = string.Empty;
            DataRow drUltimoProcesoPieza = null;
            BE.Proceso ProcesoAnterior = null;
            int iUltimoCodigoProceso;
            string sCalidad = string.Empty;
            try
            {
                drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(sCodBarras, iPlanta);
                dtPieza = ObtenerPieza(sCodBarras);
                ProcesoAnterior = ObtenerProcesoAnterior(iProceso);
                if (drUltimoProcesoPieza == null) return "0|0|" + "No existe pieza." + "|0";
                if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No existe pieza." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                iUltimoCodigoProceso = Convert.ToInt32(drUltimoProcesoPieza["CodProceso"]);
                if (dtPieza != null && dtPieza.Rows.Count != 0 && dtPieza.Rows[0]["Calidad"] != DBNull.Value)
                    sCalidad = Convert.ToString(dtPieza.Rows[0]["Calidad"]);
                if (ProcesoAnterior == null) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No se tiene definido proceso anterior." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if (ProcesoAnterior.CodProceso <= 0) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No se tiene definido proceso anterior." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if (!(iUltimoCodigoProceso == 6 & string.IsNullOrEmpty(sCalidad)))
                    if (iUltimoCodigoProceso < ProcesoAnterior.CodProceso | iUltimoCodigoProceso > ProcesoAnterior.CodProceso)
                        return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + Convert.ToString(drUltimoProcesoPieza["DesProceso"]) + "." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerColor", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spColor_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iPlanta;
                pars[1] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[1].Value = Convert.ToInt32(drUltimoProcesoPieza["CodPieza"]);
                #endregion Parameters
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null) throw new Exception("No se puede determinar el color para la pieza; Codigo Pieza: " + iUltimoCodigoProceso.ToString() + ", Codigo Articulo: " + Convert.ToString(drUltimoProcesoPieza["CodArticulo"]) + ".");
                if (dtObj.Rows.Count <= 0) throw new Exception("No se puede determinar el color para la pieza; Codigo Pieza: " + iUltimoCodigoProceso.ToString() + ", Codigo Articulo: " + Convert.ToString(drUltimoProcesoPieza["CodArticulo"]) + ".");
                return "1|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + Convert.ToString(dtObj.Rows[0]["DesColor"]) + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerColor: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
                if (dtPieza != null) dtPieza.Dispose();
            }
        }
        #endregion
        #region ObtenerRequeme
        public string ObtenerRequeme(string sCodBarras, int iPlanta, int iProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            DataTable dtPieza = null;
            long lCodRegistro = -1;
            string l_Res = string.Empty;
            DataRow drUltimoProcesoPieza = null;
            BE.Proceso ProcesoAnterior = null;
            int iUltimoCodigoProceso;
            string sCalidad = string.Empty;
            try
            {
                drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(sCodBarras, iPlanta);
                dtPieza = ObtenerPieza(sCodBarras);
                ProcesoAnterior = ObtenerProcesoAnterior(iProceso);
                if (drUltimoProcesoPieza == null) return "0|0|" + "No existe pieza." + "|0";
                if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No existe pieza." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                iUltimoCodigoProceso = Convert.ToInt32(drUltimoProcesoPieza["CodProceso"]);
                if (dtPieza != null && dtPieza.Rows.Count != 0 && dtPieza.Rows[0]["Calidad"] != DBNull.Value)
                    sCalidad = Convert.ToString(dtPieza.Rows[0]["Calidad"]);
                if (ProcesoAnterior == null) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No se tiene definido proceso anterior." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if (ProcesoAnterior.CodProceso <= 0) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No se tiene definido proceso anterior." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if (!(iUltimoCodigoProceso == 6 & string.IsNullOrEmpty(sCalidad)))
                    if (iUltimoCodigoProceso < ProcesoAnterior.CodProceso | iUltimoCodigoProceso > ProcesoAnterior.CodProceso)
                        return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + Convert.ToString(drUltimoProcesoPieza["DesProceso"]) + "." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                //#region InsertarRegistroSolicitud
                //string sPars = "";
                //lCodRegistro = this.InsertarRegistroSolicitud("ObtenerRequeme", DateTime.Now, sPars);
                //#endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spNumeroRequemeSel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iPlanta;
                pars[1] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[1].Value = Convert.ToInt32(drUltimoProcesoPieza["CodPieza"]);
                #endregion Parameters
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null) throw new Exception("No se puede determinar el color para la pieza; Codigo Pieza: " + iUltimoCodigoProceso.ToString() + ", Codigo Articulo: " + Convert.ToString(drUltimoProcesoPieza["CodArticulo"]) + ".");
                if (dtObj.Rows.Count <= 0) throw new Exception("No se puede determinar el color para la pieza; Codigo Pieza: " + iUltimoCodigoProceso.ToString() + ", Codigo Articulo: " + Convert.ToString(drUltimoProcesoPieza["CodArticulo"]) + ".");
                return "1|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + Convert.ToString(dtObj.Rows[0]["NumRequeme"]) + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerRequeme: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
                if (dtPieza != null) dtPieza.Dispose();
            }
        }
        #endregion
        #region ObtenerCalidad
        public IList<BE.Calidad> ObtenerCalidad(int iCodPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            IList<BE.Calidad> listCalidad = null;
            try
            {
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerPlantaCalidadSel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                #endregion Parameters
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null) throw new Exception("No se ha capturado el catalogo de calidades para la planta seleccionada.");
                if (dtObj.Rows.Count <= 0) throw new Exception("No se ha capturado el catalogo de calidades para la planta seleccionada.");
                listCalidad = new List<BE.Calidad>();
                foreach (DataRow row in dtObj.Rows)
                {
                    BE.Calidad oCalidad = new BE.Calidad();
                    oCalidad.ClaveCalidad = Convert.ToString(row["Calidad"]);
                    listCalidad.Add(oCalidad);
                }
                return listCalidad;
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerCalidad: " + ex.Message);
            }
            finally
            {
                if (dbObj != null)
                    dbObj.Dispose();
            }
        }
        #endregion
        #region ObtenerEstadoPieza
        private DataRow ObtenerEstadoPieza(string sCodBarras, int iPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            try
            {
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerEstadoPiezaSel");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iPlanta;
                pars[1] = new SqlParameter("@CodBarras", SqlDbType.VarChar);
                pars[1].Value = sCodBarras;
                #endregion Parameters

                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null) return null;
                if (dtObj.Rows.Count > 0)
                    return dtObj.Rows[0];
                else
                    return dtObj.NewRow();
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerEstadoPieza: " + ex.Message);
            }
            finally
            {
                if (dbObj != null)
                    dbObj.Dispose();
            }
        }
        #endregion
        #region ObtenerEstatusPieza
        public string ObtenerEstatusPieza(string sCodBarras, int iPlanta, int iProceso)
        {
            long lCodRegistro = -1;
            string l_Res = string.Empty;
            DataRow drUltimoProcesoPieza = null;
            DataTable dtPieza = null;
            int? iEstadoDesperdicio = 0;
            BE.Proceso ProcesoAnterior = null;
            int iUltimoCodigoProceso;
            string sCalidad = string.Empty;
            try
            {
                drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(sCodBarras, iPlanta);
                iEstadoDesperdicio = this.ObtenerAccionDesperdicio();
                dtPieza = ObtenerPieza(sCodBarras);
                ProcesoAnterior = ObtenerProcesoAnterior(iProceso);
                if (drUltimoProcesoPieza == null) return "0|0|" + "No existe pieza." + "|0";
                if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No existe pieza." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                iUltimoCodigoProceso = Convert.ToInt32(drUltimoProcesoPieza["CodProceso"]);
                if (iEstadoDesperdicio.HasValue)
                    if (Convert.ToInt32(drUltimoProcesoPieza["CodUltimoEstado"]) == iEstadoDesperdicio.Value)
                        return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "Pieza en desperdicio." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if (dtPieza != null && dtPieza.Rows.Count != 0 && dtPieza.Rows[0]["Calidad"] != DBNull.Value)
                    sCalidad = Convert.ToString(dtPieza.Rows[0]["Calidad"]);
                if (ProcesoAnterior == null) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No se tiene definido proceso anterior." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if (ProcesoAnterior.CodProceso <= 0) return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + "No se tiene definido proceso anterior." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                if (!(iUltimoCodigoProceso == 6 & string.IsNullOrEmpty(sCalidad)))
                    if (iUltimoCodigoProceso < ProcesoAnterior.CodProceso | iUltimoCodigoProceso > ProcesoAnterior.CodProceso)
                        return "0|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + Convert.ToString(drUltimoProcesoPieza["DesProceso"]) + "." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
                return "1|" + Convert.ToString(drUltimoProcesoPieza["CodPieza"]) + "|" + Convert.ToString(drUltimoProcesoPieza["DesProceso"]) + "." + "|" + Convert.ToString(drUltimoProcesoPieza["CodConfigBanco"]);
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerEstatusPieza: " + ex.Message);
            }
        }
        #endregion

        #region ObtenerTiposDefectoCbo
        public SolutionEntityList<BE.TipoDefecto> ObtenerTiposDefectoCbo()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.TipoDefecto> l_Res = new SolutionEntityList<BE.TipoDefecto>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerTiposDefectoCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("spGetTipoDefectoCbo");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[0];

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.TipoDefecto(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerTiposArticuloCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerTiposDefectoCbo

        #region ObtenerMoldesCbo
        public SolutionEntityList<BE.MoldeCbo> ObtenerMoldesCbo(BE.ArticuloPars articuloPars)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.MoldeCbo> l_Res = new SolutionEntityList<BE.MoldeCbo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                sPars += "CodTipoArticulo=" + articuloPars.CodTipoArticulo.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerMoldesCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_Molde_Cbo");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodTipoArticulo", SqlDbType.Int);
                pars[0].Value = articuloPars.CodTipoArticulo;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.MoldeCbo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerMoldesCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerMoldesCbo
        #region ObtenerMoldes
        public SolutionEntityList<BE.Molde> ObtenerMoldes(BE.ArticuloPars articuloPars)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Molde> l_Res = new SolutionEntityList<BE.Molde>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                sPars += "CodTipoArticulo=" + articuloPars.CodTipoArticulo.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerMoldes", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_Molde_Lis");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodTipoArticulo", SqlDbType.Int);
                pars[0].Value = articuloPars.CodTipoArticulo;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Molde(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerMoldes: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion ObtenerMoldes
        #region Login
        public BE.LoginUsuario Login(BE.LoginUsuario lu)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                sPars += "login=" + lu.Login;
                sPars += ", Password=" + lu.Password;
                lCodRegistro = this.InsertarRegistroSolicitud("Login", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_Login");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@Login", SqlDbType.NVarChar, 10);
                pars[0].Value = lu.Login;
                pars[1] = new SqlParameter("@Password", SqlDbType.NVarChar, 255);
                if (lu.Password == "Lamosa06")
                    pars[1].Value = EncriptarContrasenaUsuario(lu.Password, lu.Password);
                else
                    pars[1].Value = EncriptarContrasenaUsuario(lu.Login, lu.Password);
                pars[2] = new SqlParameter("@CodUsuario", SqlDbType.Int);
                pars[2].Direction = ParameterDirection.Output;
                pars[3] = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 255);
                pars[3].Direction = ParameterDirection.Output;

                #endregion Parameters

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                lu.CodUsuario = Convert.ToInt32(pars[2].Value);
                lu.Mensaje = Convert.ToString(pars[3].Value);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                //foreach (DataRow dr in dtObj.Rows)
                //{
                //    l_Res.Load(new BE.Turno(dr));
                //}

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", Login: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
            return lu;
        }
        #endregion Login

        #region ObtenerPruebas
        public SolutionEntityList<BE.Prueba> ObtenerPruebas(int Proceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Prueba> l_Res = new SolutionEntityList<BE.Prueba>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPruebas", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_Prueba_Lis");

                #endregion Query

                #region Parameters

                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@proceso", SqlDbType.Int);
                pars[0].Value = Proceso;

                #endregion Parameters

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Prueba(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerPruebas: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        public SolutionEntityList<BE.Prueba> ObtenerPruebas(int iCodPlanta, int iCodProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Prueba> l_Res = new SolutionEntityList<BE.Prueba>();
            //
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPruebas", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spPrueba_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iCodProceso;
                pars[1] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[1].Value = iCodPlanta;
                #endregion Parameters
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Prueba(dr));
                }
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerPruebas: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerDefectos
        public SolutionEntityList<BE.Defecto> ObtenerDefectos(int CodProceso, int CodPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Defecto> l_Res = new SolutionEntityList<BE.Defecto>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerDefectos", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query

                queryString = new StringBuilder();
                queryString.Append("sp_TipoDefecto_Lis");

                #endregion Query

                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_proceso", SqlDbType.Int);
                pars[0].Value = CodProceso;
                pars[1] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[1].Value = CodPlanta;

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Defecto(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerDefectos: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerZonaDefecto
        public SolutionEntityList<BE.ZonaDefecto> ObtenerZonaDefecto(int CodTipoArticulo)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.ZonaDefecto> l_Res = new SolutionEntityList<BE.ZonaDefecto>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerZonaDefecto", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_zonadefecto_lis");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@cod_tipo_articulo", SqlDbType.Int);
                pars[0].Value = CodTipoArticulo;

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.ZonaDefecto(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerZonaDefecto: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #region ObtenerDefecto
        public SolutionEntityList<BE.Defecto> ObtenerDefecto(int iCodProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Defecto> l_Res = new SolutionEntityList<BE.Defecto>();
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerDefecto", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spDefecto_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[0].Value = iCodProceso;
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                    l_Res.Load(new BE.Defecto(dr));
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerDefecto: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerEstatusClasificacionDefecto
        public IList<BE.Accion> ObtenerEstatusClasificacionDefecto()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Accion> l_Res = new SolutionEntityList<BE.Accion>();
            IList<BE.Accion> list = new List<BE.Accion>();
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerEstatusClasificacionDefecto", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerEstatusClasificacionDefecto_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[0];
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                    l_Res.Load(new BE.Accion(dr));
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (l_Res == null) return list;
                foreach (BE.Accion item in l_Res) list.Add(item);
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerEstatusClasificacionDefecto: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return list;
        }
        #endregion
        #region ObtenerZona
        public SolutionEntityList<BE.Zona> ObtenerZona()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Zona> l_Res = new SolutionEntityList<BE.Zona>();
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerZona", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spZonaDefecto_Sel");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[0];
                #endregion

                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Zona(dr));
                }
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerZona: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        public SolutionEntityList<BE.Zona> ObtenerZona(int? CodigoTipoArticulo)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Zona> l_Res = new SolutionEntityList<BE.Zona>();
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerZona", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spZonaDefecto_Sel");
                #endregion Query

                #region Parameters
                if (!CodigoTipoArticulo.HasValue)
                    pars = new SqlParameter[0];
                else
                {
                    pars = new SqlParameter[1];
                    pars[0] = new SqlParameter("@CodigoTipoArticulo", CodigoTipoArticulo.Value);
                }
                #endregion

                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Zona(dr));
                }
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerZona: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerAccionDesperdicio
        public int? ObtenerAccionDesperdicio()
        {
            DA01.MSSQLServer dbObj = null;
            SqlParameter[] pars = null;
            StringBuilder queryString = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerAccionDesperdicio", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerEstatusDesperdicio");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[0];
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                    return Convert.ToInt32(dr["EstatusDesperdicio"]);
                return 0;
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerAccionDesperdicio: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion ObtenerAccionDesperdicio
        #region ObtenerAccionReparado
        public int? ObtenerAccionReparado()
        {
            DA01.MSSQLServer dbObj = null;
            SqlParameter[] pars = null;
            StringBuilder queryString = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            try
            {
                //#region InsertarRegistroSolicitud
                //string sPars = "";
                //lCodRegistro = this.InsertarRegistroSolicitud("ObtenerAccionReparado", DateTime.Now, sPars);
                //#endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerEstatusReparado");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[0];
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                    return Convert.ToInt32(dr["EstatusReparado"]);
                return 0;
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerAccionReparado: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion ObtenerAccionReparado
        #region ObtenerAccion
        public SolutionEntityList<BE.Accion> ObtenerAccion()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Accion> l_Res = new SolutionEntityList<BE.Accion>();
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerAccion", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spEstado_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[0];
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows) l_Res.Load(new BE.Accion(dr));
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerAccion: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerDefectoPieza
        public SolutionEntityList<BE.DefectoPieza> ObtenerDefectoPieza(string sCodBarras, int iPlanta, int iProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.DefectoPieza> l_Res = new SolutionEntityList<LAMOSA.SCPP.Server.BusinessEntity.DefectoPieza>();
            DataRow drUltimoProcesoPieza = null;
            try
            {
                drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(sCodBarras, iPlanta);
                if (drUltimoProcesoPieza == null) return null;
                if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return null;
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerDefectoPieza", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spDefectoPieza_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[0].Value = Convert.ToInt32(drUltimoProcesoPieza["CodPieza"]);
                pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iProceso;
                #endregion Parameters
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null) return null;
                if (dtObj.Rows.Count <= 0) return null;
                foreach (DataRow dr in dtObj.Rows) l_Res.Load(new BE.DefectoPieza(dr));
                return l_Res;
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerDefectoPieza: " + ex.Message);
            }
            finally
            {
                if (dbObj != null)
                    dbObj.Dispose();
            }
        }
        #endregion
        #region ObtenerImagen
        public IList<BE.Imagen> ObtenerImagen(string sCodBarras, int iPlanta, int iProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Imagen> l_Res = new SolutionEntityList<BE.Imagen>();
            IList<BE.Imagen> listImagen = new List<BE.Imagen>();
            DataRow drUltimoProcesoPieza = null;
            try
            {
                drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(sCodBarras, iPlanta);
                if (drUltimoProcesoPieza == null) return null;
                if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return null;
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerImagen", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerImagen_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodModelo", SqlDbType.Int);
                pars[0].Value = Convert.ToInt32(drUltimoProcesoPieza["CodArticulo"]);
                pars[1] = new SqlParameter("@CodImagen", SqlDbType.Int);
                pars[1].Value = DBNull.Value;
                #endregion Parameters
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null) return null;
                if (dtObj.Rows.Count <= 0) return null;
                foreach (DataRow dr in dtObj.Rows) l_Res.Load(new BE.Imagen(dr));
                if (l_Res == null) return listImagen;
                foreach (BE.Imagen item in l_Res) listImagen.Add(item);
                return listImagen;
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerImagen: " + ex.Message);
            }
            finally
            {
                if (dbObj != null)
                    dbObj.Dispose();
            }
        }
        #endregion
        #region ObtenerLocalizacionDefecto
        public IList<BE.LocalizacionDefecto> ObtenerLocalizacionDefecto(int CodPiezaDefectoDetalle)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.LocalizacionDefecto> l_Res = new SolutionEntityList<BE.LocalizacionDefecto>();
            IList<BE.LocalizacionDefecto> listLocalizacionDefecto = new List<BE.LocalizacionDefecto>();
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerLocalizacionDefecto", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spPiezaDefectoDetalle_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodPiezaDefectoDetalle", SqlDbType.Int);
                pars[0].Value = CodPiezaDefectoDetalle;
                #endregion Parameters
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null) return null;
                if (dtObj.Rows.Count <= 0) return null;
                foreach (DataRow dr in dtObj.Rows) l_Res.Load(new BE.LocalizacionDefecto(dr));
                if (l_Res == null) return listLocalizacionDefecto;
                foreach (BE.LocalizacionDefecto item in l_Res) listLocalizacionDefecto.Add(item);
                return listLocalizacionDefecto;
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerLocalizacionDefecto: " + ex.Message);
            }
            finally
            {
                if (dbObj != null)
                    dbObj.Dispose();
            }
        }
        #endregion

        #endregion
        #region ObtenerAlmacen
        public SolutionEntityList<BE.Almacen> ObtenerAlmacen()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Almacen> l_Res = new SolutionEntityList<BE.Almacen>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerAlmacen", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Almacen_Lis");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[0];

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Almacen(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerAlmacen: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerAlmacenCbo
        public SolutionEntityList<BE.Almacen> ObtenerAlmacenCbo()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Almacen> l_Res = new SolutionEntityList<BE.Almacen>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerAlmacenCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Almacen_Cbo");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[0];

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Almacen(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerAlmacenCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerPlanta
        public SolutionEntityList<BE.Planta> ObtenerPlanta(int Almacen)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Planta> l_Res = new SolutionEntityList<BE.Planta>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPlanta", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Planta_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@almacen", SqlDbType.Int);
                pars[0].Value = Almacen;

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Planta(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerPlanta: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        public SolutionEntityList<BE.PlantaCbo> ObtenerPlanta(BE.Rol info)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.PlantaCbo> l_Res = new SolutionEntityList<BE.PlantaCbo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPlanta", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Planta_Rol");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodRol", SqlDbType.Int);
                pars[0].Value = info.ClaveRol;

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.PlantaCbo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerPlanta: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerCentroTrabajo
        public SolutionEntityList<BE.CentroTrabajo> ObtenerCentroTrabajo(int Planta, int Proceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.CentroTrabajo> l_Res = new SolutionEntityList<BE.CentroTrabajo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCentroTrabajo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_centrotrabajo_lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@planta", SqlDbType.Int);
                pars[0].Value = Planta;
                pars[1] = new SqlParameter("@proceso", SqlDbType.Int);
                pars[1].Value = Proceso;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.CentroTrabajo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerCentroTrabajo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerCentroTrabajoCbo
        public SolutionEntityList<BE.CentroTrabajo> ObtenerCentroTrabajoCbo(int Planta, int Proceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.CentroTrabajo> l_Res = new SolutionEntityList<BE.CentroTrabajo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCentroTrabajoCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_CentroTrabajo_Cbo");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@planta", SqlDbType.Int);
                pars[0].Value = Planta;
                pars[1] = new SqlParameter("@proceso", SqlDbType.Int);
                pars[1].Value = Proceso;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.CentroTrabajo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerCentroTrabajoCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerArea
        public SolutionEntityList<BE.Area> ObtenerArea(int CT)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Area> l_Res = new SolutionEntityList<BE.Area>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerArea", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Area_Cbo");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CT", SqlDbType.Int);
                pars[0].Value = CT;


                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Area(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerArea: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerEstructuraPlanta
        public SolutionEntityList<BE.EstructuraPlanta> ObtenerEstructuraPlanta(int Planta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.EstructuraPlanta> l_Res = new SolutionEntityList<BE.EstructuraPlanta>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerEstructuraPlanta", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_EstructuraPlanta_Lis");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@planta", SqlDbType.Int);
                pars[0].Value = Planta;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.EstructuraPlanta(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerEstructuraPlanta: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion

        #region ObtenerProcesoCbo
        public SolutionEntityList<BE.ProcesoCbo> ObtenerProcesoCbo(int Planta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            //long lCodRegistro = -1;
            SolutionEntityList<BE.ProcesoCbo> l_Res = new SolutionEntityList<BE.ProcesoCbo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                //string sPars = "";
                //lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCentroTrabajo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_ProcesoPlanta_Cbo");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@planta", SqlDbType.Int);
                pars[0].Value = Planta;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.ProcesoCbo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                //this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerProcesoCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerMaquinaCbo
        public SolutionEntityList<BE.MaquinaCbo> ObtenerMaquinaCbo(int codArea, int codCT)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            //long lCodRegistro = -1;
            SolutionEntityList<BE.MaquinaCbo> l_Res = new SolutionEntityList<BE.MaquinaCbo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                //string sPars = "";
                //lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCentroTrabajo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Maquina_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_area", SqlDbType.Int);
                pars[0].Value = codArea;
                pars[1] = new SqlParameter("@cod_centro_trabajo", SqlDbType.Int);
                pars[1].Value = codCT;

                #endregion



                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.MaquinaCbo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                //this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerMaquinaCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        public SolutionEntityList<BE.MaquinaCbo> ObtenerMaquinas(int codigoArea, int codigoCentroTrabajo, int codigoPlanta, int codigoProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            SolutionEntityList<BE.MaquinaCbo> l_Res = new SolutionEntityList<BE.MaquinaCbo>();
            try
            {
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Maquina_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@cod_area", SqlDbType.Int);
                pars[0].Value = codigoArea;
                pars[1] = new SqlParameter("@cod_centro_trabajo", SqlDbType.Int);
                pars[1].Value = codigoCentroTrabajo;
                pars[2] = new SqlParameter("@CodigoPlanta", SqlDbType.Int);
                pars[2].Value = codigoPlanta;
                pars[3] = new SqlParameter("@CodigoProceso", SqlDbType.Int);
                pars[3].Value = codigoProceso;
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.MaquinaCbo(dr));
                }
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                //this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerMaquinas: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerMaquina
        public SolutionEntityList<BE.Maquina> ObtenerMaquina(int CodPlanta, int CodProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            //long lCodRegistro = -1;
            SolutionEntityList<BE.Maquina> l_Res = new SolutionEntityList<BE.Maquina>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                //string sPars = "";
                //lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCentroTrabajo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Maquinas_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[0].Value = CodPlanta;
                pars[1] = new SqlParameter("@cod_proceso", SqlDbType.Int);
                pars[1].Value = CodProceso;

                #endregion



                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Maquina(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                //this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerMaquina: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        public SolutionEntityList<BE.Maquina> ObtenerMaquina(int iCodPlanta, int iCodProceso, int iCodCentroTrabajo)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Maquina> l_Res = new SolutionEntityList<BE.Maquina>();

            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerMaquina", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spMaquinas_Sel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[0].Value = iCodPlanta;
                pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[1].Value = iCodProceso;
                pars[2] = new SqlParameter("@CodCentroTrabajo", SqlDbType.Int);
                pars[2].Value = iCodCentroTrabajo;
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Maquina(dr));
                }
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                //this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerMaquina: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerProceso
        public SolutionEntityList<BE.Proceso> ObtenerProceso()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Proceso> l_Res = new SolutionEntityList<BE.Proceso>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerProceso", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Proceso_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[0];

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Proceso(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerProceso: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerRutaProceso
        public SolutionEntityList<BE.RutaProceso> ObtenerRutaProceso(int Planta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.RutaProceso> l_Res = new SolutionEntityList<BE.RutaProceso>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerRutaProceso", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("SP_RutaProceso_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@planta", SqlDbType.Int);
                pars[0].Value = Planta;

                #endregion
                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.RutaProceso(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerRutaProceso: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerRol
        public SolutionEntityList<BE.Rol> ObtenerRol()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Rol> l_Res = new SolutionEntityList<BE.Rol>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerRol", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Rol_Lis");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[0];

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Rol(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerRol: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerRolCbo
        public SolutionEntityList<BE.Rol> ObtenerRolCbo()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Rol> l_Res = new SolutionEntityList<BE.Rol>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerRolCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Rol_Cbo");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[0];

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Rol(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerRolCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerEmpleado
        public SolutionEntityList<BE.Empleado> ObtenerEmpleado(int CodPlanta, int CodPuesto)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Empleado> l_Res = new SolutionEntityList<BE.Empleado>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerEmpleado", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Empleado_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[0].Value = CodPlanta;
                pars[1] = new SqlParameter("@cod_puesto", SqlDbType.Int);
                pars[1].Value = CodPuesto;


                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Empleado(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerEmpleado: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerEmpleadoBusqueda
        public SolutionEntityList<BE.EmpleadoBusqueda> ObtenerEmpleadoBusqueda(int CodEmpleado, string NombreEmpleado, int Rol)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.EmpleadoBusqueda> l_Res = new SolutionEntityList<BE.EmpleadoBusqueda>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                sPars += "CodEmpleado=" + CodEmpleado + ", ";
                sPars += "NombreEmpleado=" + NombreEmpleado + ", ";
                sPars += "Rol=" + Rol;
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerEmpleadoBusqueda", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_EmpleadoBusqueda_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@CodEmpleado", SqlDbType.Int);
                pars[0].Value = CodEmpleado;
                pars[1] = new SqlParameter("@NombreEmplado", SqlDbType.NVarChar, 255);
                pars[1].Value = NombreEmpleado;
                pars[2] = new SqlParameter("@Rol", SqlDbType.Int);
                pars[2].Value = Rol;

                #endregion



                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.EmpleadoBusqueda(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerEmpleadoBusqueda: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        public SolutionEntityList<BE.Empleado> ObtenerEmpleadoMFG(int iEmpleado)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Empleado> l_Res = new SolutionEntityList<BE.Empleado>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerEmpleadoMFG", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("spEmpleadoMFG_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@ClaveEmpleadoMFG", SqlDbType.Int);
                pars[0].Value = iEmpleado;


                #endregion



                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Empleado(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerEmpleadoMFG: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        public string ObtenerMFGEmpleado(int iCodEmpleado)
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
                queryString.Append("spObtenerEmpleadoSel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodEmpleado", SqlDbType.Int);
                pars[0].Value = iCodEmpleado;
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                if (dtObj == null) return string.Empty;
                foreach (DataRow dr in dtObj.Rows)
                    return Convert.ToString(dr["clave_empleado_MFG"]);
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerMFGEmpleado: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
        }
        #endregion
        #region ObtenerPlantaCbo
        public SolutionEntityList<BE.PlantaCbo> ObtenerPlantaCbo()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.PlantaCbo> l_Res = new SolutionEntityList<BE.PlantaCbo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPlantaCbo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Planta_Cbo");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[0];

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.PlantaCbo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerPlantaCbo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerPuesto
        public SolutionEntityList<BE.Puesto> ObtenerPuesto()
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Puesto> l_Res = new SolutionEntityList<BE.Puesto>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPuesto", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Puesto_Cbo");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[0];

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Puesto(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerPuesto: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerUsuarios
        public SolutionEntityList<BE.Usuario> ObtenerUsuarios(int Planta, int Rol, string Usuario)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Usuario> l_Res = new SolutionEntityList<BE.Usuario>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerUsuarios", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Usuario_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@planta", SqlDbType.Int);
                pars[0].Value = Planta;
                pars[1] = new SqlParameter("@Rol", SqlDbType.Int);
                pars[1].Value = Rol;
                pars[2] = new SqlParameter("@usuario", SqlDbType.NVarChar, 10);
                if(!string.IsNullOrEmpty(Usuario))
                    pars[2].Value = Usuario;
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
                if (dtObj == null || dtObj.Rows.Count == 0)
                { 
                    //throw new Exception("No se encontró usuarios para los filtros seleccionados."); 
                    return l_Res;
                }                
                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Usuario(dr));
                    string sContrasena = dr["Contrasena"].ToString();
                    string sUsuario = dr["NombreUsuario"].ToString();
                    if (sContrasena == ObtenerContrasenaDefault())
                        (l_Res[l_Res.Count - 1] as BE.Usuario).Contrasena = DesencriptarContrasenaUsuario("Lamosa06", sContrasena);
                    else
                        (l_Res[l_Res.Count - 1] as BE.Usuario).Contrasena = DesencriptarContrasenaUsuario(sUsuario, sContrasena);
                }
                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerUsuarios: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
            return l_Res;
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
        #endregion
        #region ObtenerCondicionEsmalte
        public SolutionEntityList<BE.CondicionEsmalte> ObtenerCondicionEsmalte(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.CondicionEsmalte> l_Res = new SolutionEntityList<BE.CondicionEsmalte>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCondicionEsmalte", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_CondicionEsmalte_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@fechaInicio", SqlDbType.DateTime);
                pars[0].Value = FechaInicio;
                pars[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime);
                pars[1].Value = FechaFin;
                pars[2] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[2].Value = CodPlanta;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    CondicionEsmalte condicionEsalte = new BE.CondicionEsmalte(dr);
                    condicionEsalte.ListaMaquina = ObtenerMaqiunaCondicionEsmalte(condicionEsalte.CodCondicionEsmalte);
                    l_Res.Load(condicionEsalte);
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerCondicionEsmalte: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        private IList<Maquina> ObtenerMaqiunaCondicionEsmalte(int iCodigoCondicionEsmalte)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            IList<Maquina> lstMaquina = new List<Maquina>();
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("spObtenerMaquinasCondicionEsmalte");
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@CodigoCondicionEsmalte", SqlDbType.Int);
                parameters[i++].Value = iCodigoCondicionEsmalte;
                DataTable dtMaquinas = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
                foreach (DataRow drMaquina in dtMaquinas.Rows)
                {
                    lstMaquina.Add(new Maquina(drMaquina));
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dbObj.Dispose();
            }
            return lstMaquina;
        }
        #endregion
        #region ObtenerCondicionPastaExportar
        public DataTable ObtenerCondicionEsmalteExportar(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            DataTable dtObj = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spCondicionEsmalteExportar";
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        cmd.Parameters.AddWithValue("@CodigoPlanta", CodPlanta);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtObj = new DataTable("CondicionEsmalte");
                            da.Fill(dtObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCondicionPastaExportar: " + ex.Message);
            }
            return dtObj;
        }
        #endregion
        #region ObtenerCondicionOperacion
        public SolutionEntityList<BE.CondicionOperacion> ObtenerCondicionOperacion(int CodPlanta, int CodProceso, int CodArea, DateTime FechaInicio, DateTime FechaFin)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.CondicionOperacion> l_Res = new SolutionEntityList<BE.CondicionOperacion>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCondicionOperacion", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_CondicionOperacion_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[0].Value = CodPlanta;
                pars[1] = new SqlParameter("@cod_proceso", SqlDbType.Int);
                pars[1].Value = CodProceso;
                pars[2] = new SqlParameter("@cod_area", SqlDbType.Int);
                pars[2].Value = CodArea;
                pars[3] = new SqlParameter("@fechaInicio", SqlDbType.DateTime);
                pars[3].Value = FechaInicio;
                pars[4] = new SqlParameter("@fechaFin", SqlDbType.DateTime);
                pars[4].Value = FechaFin;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.CondicionOperacion(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerCondicionOperacion: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion

        #region ObtenerInventarioEnProceso
        public SolutionEntityList<BE.InventarioEnProceso> ObtenerInventarioEnProceso(int CodAlmacen, int CodPlanta, int CodProceso, int CodTipoArticulo, int CodArticulo, DateTime FechaInicio, DateTime FechaFin, int Opcion)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.InventarioEnProceso> l_Res = new SolutionEntityList<BE.InventarioEnProceso>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerInventarioEnProceso", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Inventario_en_proceso_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[8];
                pars[0] = new SqlParameter("@cod_almacen", SqlDbType.Int);
                pars[0].Value = CodAlmacen;
                pars[1] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[1].Value = CodPlanta;
                pars[2] = new SqlParameter("@cod_proceso", SqlDbType.Int);
                pars[2].Value = CodProceso;
                pars[3] = new SqlParameter("@cod_tipo_articulo", SqlDbType.Int);
                pars[3].Value = CodTipoArticulo;
                pars[4] = new SqlParameter("@cod_articulo", SqlDbType.Int);
                pars[4].Value = CodArticulo;
                pars[5] = new SqlParameter("@fecha_inicio", SqlDbType.DateTime);
                pars[5].Value = FechaInicio;
                pars[6] = new SqlParameter("@fecha_fin", SqlDbType.DateTime);
                pars[6].Value = FechaFin;
                pars[7] = new SqlParameter("@Opcion", SqlDbType.Int);
                pars[7].Value = Opcion;

                #endregion
                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.InventarioEnProceso(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerInventarioEnProceso: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerInventario
        public SolutionEntityList<BE.Inventario> ObtenerInventario(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Inventario> l_Res = new SolutionEntityList<BE.Inventario>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "planta=" + CodPlanta.ToString() + ",fechaini=" + FechaInicio.ToShortDateString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerInventario", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Inventario_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[0].Value = CodPlanta;
                pars[1] = new SqlParameter("@fecha_inicio", SqlDbType.Variant);
                if (FechaInicio == null) FechaInicio = DateTime.MinValue;
                pars[1].Value = FechaInicio;
                pars[2] = new SqlParameter("@fecha_fin", SqlDbType.Variant);
                if (FechaFin == null) FechaFin = DateTime.MaxValue;
                pars[2].Value = FechaFin;

                #endregion
                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Inventario(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerInventario: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerPiezasReemplazo
        public SolutionEntityList<BE.PiezaReemplazo> ObtenerPiezasReemplazo(bool SinPiezasreemplazadas)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.PiezaReemplazo> l_Res = new SolutionEntityList<BE.PiezaReemplazo>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "sin piezas=" + SinPiezasreemplazadas.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerPiezasReemplazo", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Piezas_Reemplazo_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@SinPiezaOrigen", SqlDbType.Bit);
                pars[0].Value = SinPiezasreemplazadas ? 1 : 0;

                #endregion
                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.PiezaReemplazo(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerPiezasReemplazo: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion

        #region ObtenerConfig
        public SolutionEntityList<BE.Configuracion> ObtenerConfig(int CodConfiguracion)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.Configuracion> l_Res = new SolutionEntityList<BE.Configuracion>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerConfig", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_configuracion_Lis");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodConfig", SqlDbType.Int);
                pars[0].Value = CodConfiguracion; ;

                #endregion

                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.Configuracion(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerConfig: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion

        #region ObtenerConfiguracionBanco
        public SolutionEntityList<BE.ConfigBancos> ObtenerConfiguracionBanco(int planta, int ct, int maquina, int activo)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.ConfigBancos> l_Res = new SolutionEntityList<BE.ConfigBancos>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerConfiguracionBanco", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_ConfigBanco_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@planta", SqlDbType.Int);
                pars[0].Value = planta;
                pars[1] = new SqlParameter("@ct", SqlDbType.Int);
                pars[1].Value = ct;
                pars[2] = new SqlParameter("@maq", SqlDbType.Int);
                pars[2].Value = maquina;
                pars[3] = new SqlParameter("@activo", SqlDbType.Int);
                pars[3].Value = activo;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.ConfigBancos(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerConfiguracionBanco: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerConfiguracionBancoDetalle
        public SolutionEntityList<BE.ConfigBancoDetalle> ObtenerConfiguracionBancoDetalle(int CodConfigBanco)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.ConfigBancoDetalle> l_Res = new SolutionEntityList<BE.ConfigBancoDetalle>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerConfiguracionBancoDetalle", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_ConfigBancoDetalle_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@cod_config_banco", SqlDbType.Int);
                pars[0].Value = CodConfigBanco;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.ConfigBancoDetalle(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerConfiguracionBancoDetalle: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion

        #region ObtenerRepCapInstalada
        public SolutionEntityList<BE.RepCapInstalada> ObtenerRepCapInstalada(int Opcion, int Planta, int CT, int Banco, int TipoArt, int Molde)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.RepCapInstalada> l_Res = new SolutionEntityList<BE.RepCapInstalada>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerRepCapInstalada", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_RepCapacidadInstalada_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@Opcion", SqlDbType.Int);
                pars[0].Value = Opcion;
                pars[1] = new SqlParameter("@Planta", SqlDbType.Int);
                pars[1].Value = Planta;
                pars[2] = new SqlParameter("@CT", SqlDbType.Int);
                pars[2].Value = CT;
                pars[3] = new SqlParameter("@Banco", SqlDbType.Int);
                pars[3].Value = Banco;
                pars[4] = new SqlParameter("@TipoArt", SqlDbType.Int);
                pars[4].Value = TipoArt;
                pars[5] = new SqlParameter("@Molde", SqlDbType.Int);
                pars[5].Value = Molde;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.RepCapInstalada(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {

                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerRepCapInstalada: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerMetasProd
        public SolutionEntityList<BE.MetasProd> ObtenerMetasProd(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.MetasProd> l_Res = new SolutionEntityList<BE.MetasProd>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerMetasProd", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_MetasProd_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@fechaInicio", SqlDbType.DateTime);
                pars[0].Value = FechaInicio;
                pars[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime);
                pars[1].Value = FechaFin;
                pars[2] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[2].Value = CodPlanta;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.MetasProd(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerMetasProd: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerKardexProductoBusqueda
        public SolutionEntityList<BE.KardexProductoBusqueda> ObtenerKardexProductoBusqueda(string Codigo)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.KardexProductoBusqueda> l_Res = new SolutionEntityList<BE.KardexProductoBusqueda>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerKardexProductoBusqueda", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_KardexProducto_Bus");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@Codigo", SqlDbType.NVarChar);
                pars[0].Value = Codigo;

                #endregion



                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.KardexProductoBusqueda(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerKardexProductoBusqueda: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerKardexProducto
        public SolutionEntityList<BE.KardexProducto> ObtenerKardexProducto(int Codigo)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.KardexProducto> l_Res = new SolutionEntityList<BE.KardexProducto>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerKardexProducto", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_KardexProducto_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@Codigo", SqlDbType.Int);
                pars[0].Value = Codigo;

                #endregion



                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.KardexProducto(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerKardexProducto: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion
        #region ObtenerKardexProductoDefecto
        public SolutionEntityList<BE.KardexProductoDefecto> ObtenerKardexProductoDefecto(int Codigo)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.KardexProductoDefecto> l_Res = new SolutionEntityList<BE.KardexProductoDefecto>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerKardexProductoDefecto", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_KardexProducto_Det");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@Codigo", SqlDbType.Int);
                pars[0].Value = Codigo;

                #endregion



                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.KardexProductoDefecto(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerKardexProductoDefecto: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion

        #region GuardarPlanta
        public void GuardarPlanta(BE.Planta Planta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardarPlanta", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Planta_Sav");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[0].Value = Planta.ClavePlanta;
                pars[1] = new SqlParameter("@des_planta", SqlDbType.VarChar, 100);
                pars[1].Value = Planta.DescripcionPlanta;
                pars[2] = new SqlParameter("@cod_almacen", SqlDbType.Int);
                pars[2].Value = Planta.ClaveAlmacen;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerPuesto: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region GuardarPrueba
        public void GuardarPrueba(BE.Prueba Prueba, int iCodPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardarPrueba", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Prueba_Sav");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[6];
                pars[0] = new SqlParameter("@cod_prueba", SqlDbType.Int);
                pars[0].Value = Prueba.ClavePrueba;
                pars[1] = new SqlParameter("@des_prueba", SqlDbType.VarChar, 100);
                pars[1].Value = Prueba.DesPrueba;
                pars[2] = new SqlParameter("@cod_proceso", SqlDbType.Int);
                pars[2].Value = Prueba.CodProceso;
                pars[3] = new SqlParameter("@cod_proceso_fin", SqlDbType.Int);
                pars[3].Value = Prueba.CodProcesoFin;
                pars[4] = new SqlParameter("@residencia_max", SqlDbType.Int);
                pars[4].Value = Prueba.ResidenciaMax;
                pars[5] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[5].Value = iCodPlanta;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardarPrueba: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region GuardarUsuario
        public BE.Usuario GuardarUsuario(BE.Usuario Usuario)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {
                if (!ValidarPoliticaContrasena(Usuario.Contrasena))
                {
                    string sMensaje = "La contraseña no cumple con las politicas de seguridad, por favor verifique:\n";
                    sMensaje += "\t- Longitud minima de la contraseña.\n";
                    sMensaje += "\t- La contraseña debe incluir almenos uno de los caracteres entre A-Z, a-z, 0-9.";
                    Usuario.ExceptionMessage = sMensaje;
                    return Usuario;
                }
                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardarUsuario", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Usuario_Sav");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[9];
                pars[0] = new SqlParameter("@login", SqlDbType.VarChar, 10);
                pars[0].Value = Usuario.NombreUsuario;
                pars[1] = new SqlParameter("@password", SqlDbType.VarChar, 255);
                if (Usuario.Contrasena == "Lamosa06")
                    pars[1].Value = EncriptarContrasenaUsuario(Usuario.Contrasena, Usuario.Contrasena);
                else
                    pars[1].Value = EncriptarContrasenaUsuario(Usuario.NombreUsuario, Usuario.Contrasena);
                pars[2] = new SqlParameter("@cod_empleado", SqlDbType.Int);
                pars[2].Value = Usuario.CodEmpleado;
                pars[3] = new SqlParameter("@cod_rol", SqlDbType.Int);
                pars[3].Value = Usuario.CodRol;
                pars[4] = new SqlParameter("@cod_supervisor", SqlDbType.Int);
                pars[4].Value = Usuario.CodSupervisor;
                pars[5] = new SqlParameter("@bloqueado", SqlDbType.Bit);
                pars[5].Value = Usuario.Bloqueado ? 1 : 0;
                pars[6] = new SqlParameter("@email", SqlDbType.VarChar, 255);
                pars[6].Value = Usuario.Email;
                pars[7] = new SqlParameter("@cod_usuario", SqlDbType.Int);
                pars[7].Value = Usuario.CodUsuario;
                pars[8] = new SqlParameter("@mensaje", SqlDbType.VarChar, 255);
                pars[8].Direction = ParameterDirection.Output;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                Usuario.ExceptionMessage = pars[8].Value.ToString();
                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardarUsuario: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
            return Usuario;
        }
        #endregion
        #region GuardaRutaProceso
        public BE.RutaProceso GuardaRutaProceso(BE.RutaProceso RutaProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {
                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardaRutaProceso", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_RutaProceso_Sav");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@planta", SqlDbType.Int);
                pars[0].Value = RutaProceso.CodPlanta;
                pars[1] = new SqlParameter("@proceso", SqlDbType.Int);
                pars[1].Value = RutaProceso.CodProceso;
                pars[2] = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 255);
                pars[2].Direction = ParameterDirection.Output;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                RutaProceso.ExceptionMessage = Convert.ToString(pars[2].Value);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardaRutaProceso: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return RutaProceso;
        }
        #endregion
        #region GuardarTurno
        public void GuardarTurno(BE.Turno Turno)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardarTurno", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Turno_Sav");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@cod_turno", SqlDbType.Int);
                pars[0].Value = Turno.CodTurno;
                pars[1] = new SqlParameter("@des_turno", SqlDbType.VarChar, 100);
                pars[1].Value = Turno.DesTurno;
                pars[2] = new SqlParameter("@hora_inicio", SqlDbType.DateTime);
                pars[2].Value = Turno.HoraInicio;
                pars[3] = new SqlParameter("@hora_fin", SqlDbType.DateTime);
                pars[3].Value = Turno.HoraFin;
                pars[4] = new SqlParameter("@activo", SqlDbType.Bit);
                pars[4].Value = Turno.Activo;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardarTurno: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region GuardarCondicionOperacion
        public void GuardarCondicionOperacion(BE.CondicionOperacionGuarda CO)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardarCondicionOperacion", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_CondicionOperacion_Sav");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@cod_condicion_operacion", SqlDbType.Int);
                pars[0].Value = CO.CodCondicionOperacion;
                pars[1] = new SqlParameter("@cod_area", SqlDbType.Int);
                pars[1].Value = CO.CodArea;
                pars[2] = new SqlParameter("@valor_temperatura", SqlDbType.Float);
                pars[2].Value = CO.Temperatura;
                pars[3] = new SqlParameter("@valor_humedad", SqlDbType.Float);
                pars[3].Value = CO.Humedad;


                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardarCondicionOperacion: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region GuardaRol
        public void GuardaRol(BE.rolplanta RolPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardaRol", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Rol_Sav");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@cod_rol", SqlDbType.Int);
                pars[0].Value = RolPlanta.ClaveRol;
                pars[1] = new SqlParameter("@des_rol", SqlDbType.VarChar, 100);
                pars[1].Value = RolPlanta.DescripcionRol;
                pars[2] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[2].Value = RolPlanta.CodPlanta;
                pars[3] = new SqlParameter("@activo", SqlDbType.Bit);
                pars[3].Value = RolPlanta.Activo;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardaRol: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion

        #region GuardarCondicionEsmalte
        public void GuardarCondicionEsmalte(BE.CondicionEsmalte CE)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardarCondicionEsmalte", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_CondicionEsmalte_Sav");
                #endregion Query
                #region Parameters
                int index = 0;
                pars = new SqlParameter[14];
                pars[index] = new SqlParameter("@cod_condicion_esmalte", SqlDbType.Int);
                pars[index++].Value = CE.CodCondicionEsmalte;
                pars[index] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[index++].Value = CE.CodPlanta;
                pars[index] = new SqlParameter("@valor_espejo", SqlDbType.Float);
                pars[index++].Value = CE.TiempoEspejo;
                pars[index] = new SqlParameter("@valor_viscosidad", SqlDbType.Float);
                pars[index++].Value = CE.Viscosidad;
                pars[index] = new SqlParameter("@valor_densidad", SqlDbType.Float);
                pars[index++].Value = CE.Densidad;
                pars[index] = new SqlParameter("@valor_espesor", SqlDbType.Float);
                pars[index++].Value = CE.Espesor;
                pars[index] = new SqlParameter("@CodigoTurno", SqlDbType.Int);
                pars[index++].Value = CE.CodigoTurno;
                pars[index] = new SqlParameter("@CodigoColor", SqlDbType.Int);
                pars[index++].Value = CE.CodigoColor;
                pars[index] = new SqlParameter("@NumeroLote", SqlDbType.VarChar, 20);
                pars[index++].Value = CE.NumeroLote;
                pars[index] = new SqlParameter("@TamanoLote", SqlDbType.Float);
                pars[index++].Value = CE.TamanoLote;
                pars[index] = new SqlParameter("@CantidadGoma", SqlDbType.Float);
                pars[index++].Value = CE.CantidadGoma;
                pars[index] = new SqlParameter("@Molino", SqlDbType.Int);
                pars[index++].Value = CE.Molino;
                pars[index] = new SqlParameter("@Granulometria", SqlDbType.Float);
                pars[index++].Value = CE.Granulometria;

                pars[index] = new SqlParameter("@CodigoCondicionEsmalte", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;
                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                int iCodigoCondicionEsmalte = int.Parse(pars[index].Value.ToString());
                foreach (Maquina maquina in CE.ListaMaquina)
                    GuardarRelacionEsmalteMaquinas(iCodigoCondicionEsmalte, maquina.CodMaquina);
                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardarCondicionEsmalte: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        private bool GuardarRelacionEsmalteMaquinas(int iCodigoCondicionEsmalte, int iCodigoMaquina)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("spInsertarEsmalteMaquina");
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@CodigoCondicionEsmalte", SqlDbType.Int);
                parameters[i++].Value = iCodigoCondicionEsmalte;
                parameters[i] = new SqlParameter("@CodigoMaquina", SqlDbType.Int);
                parameters[i].Value = iCodigoMaquina;
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
                return true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dbObj.Dispose();
            }
            return false;
        }
        #endregion
        #region GuardarCondicionPasta
        public void GuardarCondicionPasta(BE.CondicionPasta CP)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardarCondicionPasta", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                queryString = new StringBuilder();
                queryString.Append("sp_CondicionPasta_Sav");
                pars = new SqlParameter[11];
                int index = 0;
                pars[index] = new SqlParameter("@cod_condicion_pasta", SqlDbType.Int);
                pars[index++].Value = CP.CodCondicionPasta;
                pars[index] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[index++].Value = CP.CodPlanta;
                pars[index] = new SqlParameter("@valor_densidad", SqlDbType.Float);
                pars[index++].Value = CP.Densidad;
                pars[index] = new SqlParameter("@valor_BU", SqlDbType.Float);
                pars[index++].Value = CP.Bu;
                pars[index] = new SqlParameter("@CodigoBaroi", SqlDbType.Int);
                pars[index++].Value = CP.CodigoBaroi;
                pars[index] = new SqlParameter("@CodigoTurno", SqlDbType.Int);
                pars[index++].Value = CP.CodigoTurno;
                pars[index] = new SqlParameter("@Deposito", SqlDbType.Int);
                pars[index++].Value = CP.Deposito;
                pars[index] = new SqlParameter("@PerdidaBrillo", SqlDbType.DateTime);
                pars[index++].Value = CP.PerdidaBrillo;
                pars[index] = new SqlParameter("@Viscosidad", SqlDbType.Int);
                pars[index++].Value = CP.Viscosidad;
                pars[index] = new SqlParameter("@CodigoProveedor", SqlDbType.Int);
                pars[index++].Value = CP.CodigoProveedor;

                pars[index] = new SqlParameter("@CodigoCondicionPasta", SqlDbType.Int);
                pars[index].Direction = ParameterDirection.Output;

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                int iCodigoConfigPasta = int.Parse(pars[index].Value.ToString());
                foreach (Area area in CP.ListaArea)
                    GuardarRelacionPastaArea(iCodigoConfigPasta, area.CodArea);
                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardarCondicionPasta: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        private bool GuardarRelacionPastaArea(int iCodigoConfigPasta, int iCodigoArea)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("spInsertarPastaArea");
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@CodigoConfigPasta", SqlDbType.Int);
                parameters[i++].Value = iCodigoConfigPasta;
                parameters[i] = new SqlParameter("@CodigoArea", SqlDbType.Int);
                parameters[i].Value = iCodigoArea;
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
                return true;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dbObj.Dispose();
            }
            return false;
        }
        #endregion
        #region ObtenerCondicionPasta
        public SolutionEntityList<BE.CondicionPasta> ObtenerCondicionPasta(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.CondicionPasta> l_Res = new SolutionEntityList<BE.CondicionPasta>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCondicionPasta", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_CondicionPasta_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@fechaInicio", SqlDbType.DateTime);
                pars[0].Value = FechaInicio;
                pars[1] = new SqlParameter("@fechaFin", SqlDbType.DateTime);
                pars[1].Value = FechaFin;
                pars[2] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[2].Value = CodPlanta;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    CondicionPasta condicionPasta = new BE.CondicionPasta(dr);
                    condicionPasta.ListaArea = ObtenerAreasCondicionPasta(condicionPasta.CodCondicionPasta);
                    l_Res.Load(condicionPasta);
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerCondicionPasta: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        private IList<Area> ObtenerAreasCondicionPasta(int iCodigoCondicionPasta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            IList<Area> lstArea = new List<Area>();
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("spObtenerAreasCondicionPasta");
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[i] = new SqlParameter("@CodigoConfigPasta", SqlDbType.Int);
                parameters[i++].Value = iCodigoCondicionPasta;
                DataTable dtAreas = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
                foreach (DataRow drArea in dtAreas.Rows)
                {
                    lstArea.Add(new Area(drArea));
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dbObj.Dispose();
            }
            return lstArea;
        }
        #endregion
        #region ObtenerCondicionPastaExportar
        public DataTable ObtenerCondicionPastaExportar(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            DataTable dtObj = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spCondicionPastaExportar";
                        cmd.Parameters.AddWithValue("@FechaInicio", FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", FechaFin);
                        cmd.Parameters.AddWithValue("@CodigoPlanta", CodPlanta);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtObj = new DataTable("CondicionPasta");
                            da.Fill(dtObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerCondicionPastaExportar: " + ex.Message);
            }
            return dtObj;
        }
        #endregion
        //#region GuardarConfigBanco
        //public void GuardarConfigBanco(BE.ConfigBancos CB)
        //{
        //    DA01.MSSQLServer dbObj = null;
        //    StringBuilder queryString = null;
        //    SqlParameter[] pars = null;
        //    long lCodRegistro = -1;
        //    //
        //    try
        //    {

        //        #region InsertarRegistroSolicitud

        //        string sPars = "";
        //        lCodRegistro = this.InsertarRegistroSolicitud("GuardarConfigBanco", DateTime.Now, sPars);

        //        #endregion InsertarRegistroSolicitud

        //        #region Connection Configuration

        //        dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

        //        #endregion Connection Configuration

        //        #region Query
        //        queryString = new StringBuilder();
        //        queryString.Append("sp_ConfigBanco_Sav");
        //        #endregion Query
        //        #region Parameters
        //        pars = new SqlParameter[5];
        //        pars[0] = new SqlParameter("@cod_maquina", SqlDbType.Int);
        //        pars[0].Value = CB.CodMaquina;
        //        pars[1] = new SqlParameter("@cod_usuario_alta", SqlDbType.Int);
        //        pars[1].Value = CB.CodUsuarioAlta;
        //        pars[2] = new SqlParameter("@fecha_inicio", SqlDbType.DateTime);
        //        pars[2].Value = CB.FechaInicio;
        //        pars[3] = new SqlParameter("@fecha_fin", SqlDbType.DateTime);
        //        pars[3].Value = CB.FechaFin;
        //        pars[4] = new SqlParameter("@cod_config_banco", SqlDbType.Int);
        //        pars[4].Value = CB.CodConfigBanco;

        //        #endregion



        //        #region Query Execution

        //        dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

        //        #endregion Query Execution


        //    }
        //    catch (Exception ex)
        //    {
        //        #region ActualizarRegistroSolicitud

        //        this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

        //        #endregion ActualizarRegistroSolicitud
        //        throw new Exception(this.sClassName + ", GuardarConfigBanco: " + ex.Message);
        //    }
        //    finally
        //    {
        //        dbObj.Dispose();
        //    }
        //}
        //#endregion
        #region GuardarConfigBancoRegistro
        public int GuardarConfigBanco(BE.ConfigBancoResgistro CBR)
        {
            int res = 0;
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardarConfigBancoRegistro", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_ConfigBancoReg_Sav");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[10];
                pars[0] = new SqlParameter("@cod_maquina", SqlDbType.Int);
                pars[0].Value = CBR.CodMaquina;
                pars[1] = new SqlParameter("@cod_molde", SqlDbType.Int);
                pars[1].Value = CBR.CodMolde;
                pars[2] = new SqlParameter("@limitevaciadas", SqlDbType.Int);
                pars[2].Value = CBR.Limitevaciadas;
                pars[3] = new SqlParameter("@vaciadasdia", SqlDbType.Int);
                pars[3].Value = CBR.Vaciadasdia;
                pars[4] = new SqlParameter("@CantMoldes", SqlDbType.Int);
                pars[4].Value = CBR.CantMoldes;
                pars[5] = new SqlParameter("@cod_usuario_alta", SqlDbType.Int);
                pars[5].Value = CBR.CodUsuarioAlta;
                pars[6] = new SqlParameter("@Activo", SqlDbType.Bit);
                pars[6].Value = CBR.Activo ? 1 : 0;

                pars[7] = new SqlParameter("@NumeroImpresiones", SqlDbType.Int);
                pars[7].Value = CBR.NumeroImpresiones;
                pars[8] = new SqlParameter("@cod_config_banco", SqlDbType.Int);
                pars[8].Value = CBR.CodConfigBanco;

                pars[9] = new SqlParameter("@codigo_configBanco", SqlDbType.Int);
                pars[9].Direction = ParameterDirection.Output;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                res = int.Parse(pars[9].Value.ToString());
                #endregion Query Execution
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardarConfigBancoRegistro: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return res;
        }
        #endregion
        //#region GuardarConfigBancoDetalle
        //public void GuardarConfigBancoDetalle(BE.ConfigBancoDetalle CBD)
        //{
        //    DA01.MSSQLServer dbObj = null;
        //    StringBuilder queryString = null;
        //    SqlParameter[] pars = null;
        //    long lCodRegistro = -1;
        //    //
        //    try
        //    {

        //        #region InsertarRegistroSolicitud

        //        string sPars = "";
        //        lCodRegistro = this.InsertarRegistroSolicitud("GuardarConfigBancoDetalle", DateTime.Now, sPars);

        //        #endregion InsertarRegistroSolicitud

        //        #region Connection Configuration

        //        dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

        //        #endregion Connection Configuration

        //        #region Query
        //        queryString = new StringBuilder();
        //        queryString.Append("sp_ConfigMoldeBancoDet_Sav");
        //        #endregion Query
        //        #region Parameters
        //        pars = new SqlParameter[5];
        //        pars[0] = new SqlParameter("@cod_config_banco", SqlDbType.Int);
        //        pars[0].Value = CBD.CodConfigBanco;
        //        pars[1] = new SqlParameter("@cod_molde", SqlDbType.Int);
        //        pars[1].Value = CBD.CodMolde;
        //        pars[2] = new SqlParameter("@limitevaciadas", SqlDbType.Int);
        //        pars[2].Value = CBD.LimiteVaciadas;
        //        pars[3] = new SqlParameter("@vaciadasdia", SqlDbType.Int);
        //        pars[3].Value = CBD.VaciadasDiarias;
        //        pars[4] = new SqlParameter("@CantMoldes", SqlDbType.Int);
        //        pars[4].Value = CBD.CantidadMoldes;

        //        #endregion


        //        #region Query Execution

        //        dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

        //        #endregion Query Execution


        //    }
        //    catch (Exception ex)
        //    {
        //        #region ActualizarRegistroSolicitud

        //        this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

        //        #endregion ActualizarRegistroSolicitud
        //        throw new Exception(this.sClassName + ", GuardarConfigBancoDetalle: " + ex.Message);
        //    }
        //    finally
        //    {
        //        dbObj.Dispose();
        //    }
        //}
        //#endregion
        #region GuardarDefectoPieza
        public bool GuardarDefectoPieza(IList<BE.DefectoPieza> list, int iCodProceso, int iCodPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            DataRow drUltimoProcesoPieza = null;
            int? iEstatusDesperdicio = 0;
            try
            {
                iEstatusDesperdicio = ObtenerAccionDesperdicio();
                if (list == null) return false;
                foreach (BE.DefectoPieza item in list)
                {
                    if (drUltimoProcesoPieza == null)
                        drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(item.CodBarra, iCodPlanta);
                    if (drUltimoProcesoPieza == null) return false;
                    if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return false;
                    #region InsertarRegistroSolicitud
                    //iCodProceso = (item.CodAccion == iEstatusDesperdicio) ? iCodProceso:Convert.ToInt32(drUltimoProcesoPieza["CodProceso"]);
                    string sPars = "";
                    lCodRegistro = this.InsertarRegistroSolicitud("GuardarDefectoPieza", DateTime.Now, sPars);
                    #endregion InsertarRegistroSolicitud
                    #region Connection Configuration
                    dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                    #endregion Connection Configuration
                    #region Query
                    queryString = new StringBuilder();
                    queryString.Append("spDefectoPieza_Ins");
                    #endregion Query
                    if (item.LocalizacionDefecto == null)
                    {
                        #region Parameters
                        pars = new SqlParameter[13];
                        pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                        pars[0].Value = Convert.ToInt32(drUltimoProcesoPieza["CodPieza"]);
                        pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                        pars[1].Value = iCodProceso;
                        pars[2] = new SqlParameter("@CodDefecto", SqlDbType.Int);
                        pars[2].Value = item.ClaveUnica;
                        pars[3] = new SqlParameter("@CodZonaDefecto", SqlDbType.Int);
                        pars[3].Value = item.CodZona;
                        pars[4] = new SqlParameter("@CodEstadoDefecto", SqlDbType.Int);
                        pars[4].Value = item.CodAccion;
                        pars[5] = new SqlParameter("@CodPiezaDefectoDetalle", SqlDbType.Int);
                        if (item.CodPiezaDefectoDetalle.HasValue)
                            pars[5].Value = item.CodPiezaDefectoDetalle.Value;
                        else
                            pars[5].Value = DBNull.Value;
                        pars[6] = new SqlParameter("@CodModelo", SqlDbType.Int);
                        pars[6].Value = drUltimoProcesoPieza["CodArticulo"];
                        pars[7] = new SqlParameter("@CodImagen", SqlDbType.Int);
                        if (item.CodImagen.HasValue)
                            pars[7].Value = item.CodImagen.Value;
                        else
                            pars[7].Value = DBNull.Value;
                        pars[8] = new SqlParameter("@PosicionX", SqlDbType.Int);
                        if (item.CodZonaDefectoX.HasValue)
                            pars[8].Value = item.CodZonaDefectoX;
                        else
                            pars[8].Value = DBNull.Value;
                        pars[9] = new SqlParameter("@PosicionY", SqlDbType.Int);
                        if (item.CodZonaDefectoY.HasValue)
                            pars[9].Value = item.CodZonaDefectoY;
                        else
                            pars[9].Value = DBNull.Value;
                        pars[10] = new SqlParameter("@CodEmpleado", SqlDbType.Int);
                        pars[10].Value = item.CodEmpleado;
                        pars[11] = new SqlParameter("@FechaAlta", SqlDbType.DateTime);
                        pars[11].Value = DateTime.Today;
                        pars[12] = new SqlParameter("@EstatusOperacion", SqlDbType.Int);
                        pars[12].Value = 0;
                        #endregion
                        #region Query Execution
                        dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                        #endregion Query Execution
                    }
                    else
                    {
                        if (item.LocalizacionDefecto.Count == 0)
                        {
                            #region Parameters
                            pars = new SqlParameter[13];
                            pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                            pars[0].Value = Convert.ToInt32(drUltimoProcesoPieza["CodPieza"]);
                            pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                            pars[1].Value = iCodProceso;
                            pars[2] = new SqlParameter("@CodDefecto", SqlDbType.Int);
                            pars[2].Value = item.ClaveUnica;
                            pars[3] = new SqlParameter("@CodZonaDefecto", SqlDbType.Int);
                            pars[3].Value = item.CodZona;
                            pars[4] = new SqlParameter("@CodEstadoDefecto", SqlDbType.Int);
                            pars[4].Value = item.CodAccion;
                            pars[5] = new SqlParameter("@CodPiezaDefectoDetalle", SqlDbType.Int);
                            if (item.CodPiezaDefectoDetalle.HasValue)
                                pars[5].Value = item.CodPiezaDefectoDetalle.Value;
                            else
                                pars[5].Value = DBNull.Value;
                            pars[6] = new SqlParameter("@CodModelo", SqlDbType.Int);
                            pars[6].Value = drUltimoProcesoPieza["CodArticulo"];
                            pars[7] = new SqlParameter("@CodImagen", SqlDbType.Int);
                            if (item.CodImagen.HasValue)
                                pars[7].Value = item.CodImagen.Value;
                            else
                                pars[7].Value = DBNull.Value;
                            pars[8] = new SqlParameter("@PosicionX", SqlDbType.Int);
                            if (item.CodZonaDefectoX.HasValue)
                                pars[8].Value = item.CodZonaDefectoX;
                            else
                                pars[8].Value = DBNull.Value;
                            pars[9] = new SqlParameter("@PosicionY", SqlDbType.Int);
                            if (item.CodZonaDefectoY.HasValue)
                                pars[9].Value = item.CodZonaDefectoY;
                            else
                                pars[9].Value = DBNull.Value;
                            pars[10] = new SqlParameter("@CodEmpleado", SqlDbType.Int);
                            pars[10].Value = item.CodEmpleado;
                            pars[11] = new SqlParameter("@FechaAlta", SqlDbType.DateTime);
                            pars[11].Value = DateTime.Today;
                            pars[12] = new SqlParameter("@EstatusOperacion", SqlDbType.Int);
                            pars[12].Value = 0;
                            #endregion
                            #region Query Execution
                            dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                            #endregion Query Execution
                        }
                        else
                        {
                            foreach (BE.LocalizacionDefecto localizacion in item.LocalizacionDefecto)
                            {
                                #region Parameters
                                pars = new SqlParameter[13];
                                pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                                pars[0].Value = Convert.ToInt32(drUltimoProcesoPieza["CodPieza"]);//item.CodPieza;
                                pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                                pars[1].Value = iCodProceso;
                                pars[2] = new SqlParameter("@CodDefecto", SqlDbType.Int);
                                pars[2].Value = item.ClaveUnica;
                                pars[3] = new SqlParameter("@CodZonaDefecto", SqlDbType.Int);
                                pars[3].Value = localizacion.CodZona;
                                pars[4] = new SqlParameter("@CodEstadoDefecto", SqlDbType.Int);
                                pars[4].Value = item.CodAccion;
                                pars[5] = new SqlParameter("@CodPiezaDefectoDetalle", SqlDbType.Int);
                                if (item.CodPiezaDefectoDetalle.HasValue)
                                    pars[5].Value = item.CodPiezaDefectoDetalle.Value;
                                else
                                    pars[5].Value = DBNull.Value;
                                pars[6] = new SqlParameter("@CodModelo", SqlDbType.Int);
                                pars[6].Value = drUltimoProcesoPieza["CodArticulo"];
                                pars[7] = new SqlParameter("@CodImagen", SqlDbType.Int);
                                if (localizacion.CodImagen.HasValue)
                                    pars[7].Value = localizacion.CodImagen;
                                else
                                    pars[7].Value = DBNull.Value;
                                pars[8] = new SqlParameter("@PosicionX", SqlDbType.Int);
                                if (localizacion.PosicionH.HasValue)
                                    pars[8].Value = localizacion.PosicionH.Value;
                                else
                                    pars[8].Value = DBNull.Value;
                                pars[9] = new SqlParameter("@PosicionY", SqlDbType.Int);
                                if (localizacion.PosicionV.HasValue)
                                    pars[9].Value = localizacion.PosicionV.Value;
                                else
                                    pars[9].Value = DBNull.Value;
                                pars[10] = new SqlParameter("@CodEmpleado", SqlDbType.Int);
                                pars[10].Value = item.CodEmpleado;
                                pars[11] = new SqlParameter("@FechaAlta", SqlDbType.DateTime);
                                pars[11].Value = DateTime.Today;
                                pars[12] = new SqlParameter("@EstatusOperacion", SqlDbType.Int);
                                pars[12].Value = 0;
                                #endregion
                                #region Query Execution
                                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                                #endregion Query Execution
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardarDefectoPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region EliminarDefectoPieza
        public bool EliminarDefectoPieza(IList<BE.DefectoPieza> list, int iCodProceso, int iCodPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            DataRow drUltimoProcesoPieza = null;
            try
            {
                if (list == null) return false;
                foreach (BE.DefectoPieza item in list)
                {
                    if (drUltimoProcesoPieza == null)
                        drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(item.CodBarra, iCodPlanta);
                    if (drUltimoProcesoPieza == null) return false;
                    if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return false;
                    #region InsertarRegistroSolicitud
                    string sPars = "";
                    lCodRegistro = this.InsertarRegistroSolicitud("EliminarDefectoPieza", DateTime.Now, sPars);
                    #endregion InsertarRegistroSolicitud
                    #region Connection Configuration
                    dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                    #endregion Connection Configuration
                    #region Query
                    queryString = new StringBuilder();
                    queryString.Append("dbo.spDefectoPieza_Del");
                    #endregion Query
                    #region Parameters
                    pars = new SqlParameter[11];
                    pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = Convert.ToInt32(drUltimoProcesoPieza["CodPieza"]);//item.CodPieza;
                    pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;
                    pars[2] = new SqlParameter("@CodDefecto", SqlDbType.Int);
                    pars[2].Value = item.ClaveUnica;
                    pars[3] = new SqlParameter("@CodZonaDefecto", SqlDbType.Int);
                    pars[3].Value = item.CodZona;
                    pars[4] = new SqlParameter("@CodEstadoDefecto", SqlDbType.Int);
                    pars[4].Value = item.CodAccion;
                    pars[5] = new SqlParameter("@CodPiezaDefectoDetalle", SqlDbType.Int);
                    pars[5].Value = item.CodPiezaDefectoDetalle;
                    pars[6] = new SqlParameter("@CodModelo", SqlDbType.Int);
                    pars[6].Value = item.CodModelo;
                    pars[7] = new SqlParameter("@CodImagen", SqlDbType.Int);
                    pars[7].Value = item.CodImagen;
                    pars[8] = new SqlParameter("@PosicionX", SqlDbType.Int);
                    pars[8].Value = item.CodZonaDefectoX;
                    pars[9] = new SqlParameter("@PosicionY", SqlDbType.Int);
                    pars[9].Value = item.CodZonaDefectoY;
                    pars[10] = new SqlParameter("@EstatusOperacion", SqlDbType.Int);
                    pars[10].Value = 0;
                    #endregion
                    #region Query Execution
                    dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                    #endregion Query Execution
                }
                return true;
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", EliminarDefectoPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region DesperdiciarDefectoPieza
        public bool DesperdiciarDefectoPieza(IList<BE.DefectoPieza> list, int iCodProceso, int iCodPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            DataRow drUltimoProcesoPieza = null;
            try
            {
                if (list == null) return false;
                foreach (BE.DefectoPieza item in list)
                {
                    if (drUltimoProcesoPieza == null)
                        drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(item.CodBarra, iCodPlanta);
                    if (drUltimoProcesoPieza == null) return false;
                    if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return false;
                    #region InsertarRegistroSolicitud
                    string sPars = "";
                    lCodRegistro = this.InsertarRegistroSolicitud("DesperdiciarDefectoPieza", DateTime.Now, sPars);
                    #endregion InsertarRegistroSolicitud
                    #region Connection Configuration
                    dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                    #endregion Connection Configuration
                    #region Query
                    queryString = new StringBuilder();
                    queryString.Append("spDesperdiciarDefectoPieza_Upd");
                    #endregion Query
                    #region Parameters
                    pars = new SqlParameter[7];
                    pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = Convert.ToInt32(drUltimoProcesoPieza["CodPieza"]);//item.CodPieza;
                    pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;
                    pars[2] = new SqlParameter("@CodDefecto", SqlDbType.Int);
                    pars[2].Value = item.ClaveUnica;
                    pars[3] = new SqlParameter("@CodZonaDefecto", SqlDbType.Int);
                    pars[3].Value = item.CodZona;
                    pars[4] = new SqlParameter("@CodEstadoDefecto", SqlDbType.Int);
                    pars[4].Value = item.CodAccion;
                    pars[5] = new SqlParameter("@CodPiezaDefectoDetalle", SqlDbType.Int);
                    pars[5].Value = item.CodPiezaDefectoDetalle;
                    pars[6] = new SqlParameter("@EstatusOperacion", SqlDbType.Int);
                    pars[6].Value = 0;
                    #endregion
                    #region Query Execution
                    dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                    #endregion Query Execution
                }
                return true;
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", DesperdiciarDefectoPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region LiberarDefectoPieza
        public bool LiberarDefectoPieza(IList<BE.DefectoPieza> list, int iCodProceso, int iCodPlanta)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            DataRow drUltimoProcesoPieza = null;
            try
            {
                if (list == null) return false;
                foreach (BE.DefectoPieza item in list)
                {
                    if (drUltimoProcesoPieza == null)
                        drUltimoProcesoPieza = ObtenerUltimoProcesoPieza(item.CodBarra, iCodPlanta);
                    if (drUltimoProcesoPieza == null) return false;
                    if (drUltimoProcesoPieza["CodProceso"] == DBNull.Value) return false;
                    #region InsertarRegistroSolicitud
                    string sPars = "";
                    lCodRegistro = this.InsertarRegistroSolicitud("LiberarDefectoPieza", DateTime.Now, sPars);
                    #endregion InsertarRegistroSolicitud
                    #region Connection Configuration
                    dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                    #endregion Connection Configuration
                    #region Query
                    queryString = new StringBuilder();
                    queryString.Append("spLiberarDefectoPieza_Upd");
                    #endregion Query
                    #region Parameters
                    pars = new SqlParameter[7];
                    pars[0] = new SqlParameter("@CodPieza", SqlDbType.Int);
                    pars[0].Value = Convert.ToInt32(drUltimoProcesoPieza["CodPieza"]);//item.CodPieza;
                    pars[1] = new SqlParameter("@CodProceso", SqlDbType.Int);
                    pars[1].Value = iCodProceso;
                    pars[2] = new SqlParameter("@CodDefecto", SqlDbType.Int);
                    pars[2].Value = item.ClaveUnica;
                    pars[3] = new SqlParameter("@CodZonaDefecto", SqlDbType.Int);
                    pars[3].Value = item.CodZona;
                    pars[4] = new SqlParameter("@CodEstadoDefecto", SqlDbType.Int);
                    pars[4].Value = item.CodAccion;
                    pars[5] = new SqlParameter("@CodPiezaDefectoDetalle", SqlDbType.Int);
                    pars[5].Value = item.CodPiezaDefectoDetalle;
                    pars[6] = new SqlParameter("@EstatusOperacion", SqlDbType.Int);
                    pars[6].Value = 0;
                    #endregion
                    #region Query Execution
                    dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                    #endregion Query Execution
                }
                return true;
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", LiberarDefectoPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region GuardarClasificacion
        public bool GuardarClasificacion(BE.Clasificacion clasificacion)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardarClasificacion", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spClasificacion_Ins");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[14];
                pars[0] = new SqlParameter("@CodTurno", SqlDbType.Int);
                pars[0].Value = clasificacion.CodTurno.Value;
                pars[1] = new SqlParameter("@CodUsuario", SqlDbType.Int);
                pars[1].Value = clasificacion.CodUsuario.Value;
                pars[2] = new SqlParameter("@CodSupervisor", SqlDbType.Int);
                pars[2].Value = clasificacion.CodSupervisor.Value;
                pars[3] = new SqlParameter("@CodOperador", SqlDbType.Int);
                pars[3].Value = clasificacion.CodOperador.Value;
                pars[4] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                pars[4].Value = clasificacion.CodConfigBanco.Value;
                pars[5] = new SqlParameter("@CodProceso", SqlDbType.Int);
                pars[5].Value = clasificacion.CodProceso.Value;
                pars[6] = new SqlParameter("@FechaRegistro", SqlDbType.DateTime);
                pars[6].Value = clasificacion.Fecha.Value;
                pars[7] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[7].Value = clasificacion.CodPlanta.Value;
                pars[8] = new SqlParameter("@CodPieza", SqlDbType.Int);
                pars[8].Value = clasificacion.CodPieza.Value;
                pars[9] = new SqlParameter("@ClaveCalidad", SqlDbType.NVarChar, 255);
                pars[9].Value = clasificacion.ClaveCalidad;
                pars[10] = new SqlParameter("@CodMaquina", SqlDbType.Int);
                pars[10].Value = clasificacion.CodMaquina.Value;
                pars[11] = new SqlParameter("@CodCentroTrabajo", SqlDbType.Int);
                pars[11].Value = clasificacion.CodCentroTrabajo.Value;
                pars[12] = new SqlParameter("@CodMaquinaHorno", SqlDbType.Int);
                if (clasificacion.CodMaquinaHorno.HasValue)
                    pars[12].Value = clasificacion.CodMaquinaHorno.Value;
                else
                    pars[12].Value = DBNull.Value;
                pars[13] = new SqlParameter("@EstatusOperacion", SqlDbType.Bit);
                pars[13].Value = 0;
                pars[13].Direction = ParameterDirection.InputOutput;
                #endregion
                #region Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if (!Convert.ToBoolean(pars[13].Value))
                    throw new Exception(this.sClassName + ", GuardarClasificacion: No se puede guardar la clasificacion, favor de contactar asu administrador.");
                #endregion Query Execution
                return true;
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardarClasificacion: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        public bool GuardarCalidadClasificacion(BE.Clasificacion clasificacion)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            try
            {
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spGuardarCalidadClasificacion");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodigoPieza", SqlDbType.Int);
                pars[0].Value = clasificacion.CodPieza;
                pars[1] = new SqlParameter("@ClaveCalidad", SqlDbType.VarChar);
                pars[1].Value = clasificacion.ClaveCalidad;
                #endregion
                #region Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarCalidadClasificacion: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion

        #region GuardaInventario
        public void GuardaInventario(BE.Inventario inv)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "inventario=" + inv.CodInventario.ToString() + "," + inv.DesInventario + ",planta=" + inv.DesPlanta;
                lCodRegistro = this.InsertarRegistroSolicitud("GuardaInventario", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Inventario_Sav");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[5];
                pars[0] = new SqlParameter("@cod_inventario", SqlDbType.Int);
                pars[0].Value = inv.CodInventario;
                pars[1] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[1].Value = inv.CodPlanta;
                pars[2] = new SqlParameter("@activo", SqlDbType.Bit);
                pars[2].Value = inv.Activo ? 1 : 0;
                pars[3] = new SqlParameter("@descripcion", SqlDbType.NVarChar, 50);
                pars[3].Value = inv.DesInventario;
                pars[4] = new SqlParameter("@clave", SqlDbType.NVarChar, 10);
                pars[4].Value = inv.ClaveInventario;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardaInventario: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region GuardarMetasProd
        public void GuardarMetasProd(BE.MetasProd CE)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("GuardarMetasProd", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_MetasProd_Sav");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[23];
                pars[0] = new SqlParameter("@cod_planta", SqlDbType.Int);
                pars[0].Value = CE.Planta;
                pars[1] = new SqlParameter("@cant_procesadas", SqlDbType.Int);
                pars[1].Value = CE.CantProc;
                pars[2] = new SqlParameter("@cant_inventario", SqlDbType.Int);
                pars[2].Value = CE.CantInv;
                pars[3] = new SqlParameter("@cant_desperdicio", SqlDbType.Int);
                pars[3].Value = CE.CantDesp;

                pars[4] = new SqlParameter("@cant_desp_verde", SqlDbType.Int);
                pars[4].Value = CE.CantVerde;

                pars[5] = new SqlParameter("@cant_desp_quemado", SqlDbType.Int);
                pars[5].Value = CE.CantQuemado;
                pars[6] = new SqlParameter("@calidad1", SqlDbType.Int);
                pars[6].Value = CE.ICalidad1;
                pars[7] = new SqlParameter("@porcentaje_cal1", SqlDbType.Int);
                pars[7].Value = CE.PorcCal1;
                pars[8] = new SqlParameter("@calidad2", SqlDbType.Int);
                pars[8].Value = CE.ICalidad2;
                pars[9] = new SqlParameter("@porcentaje_cal2", SqlDbType.Int);
                pars[9].Value = CE.PorcCal2;
                pars[10] = new SqlParameter("@calidad3", SqlDbType.Int);
                pars[10].Value = CE.ICalidad3;
                pars[11] = new SqlParameter("@porcentaje_cal3", SqlDbType.Int);
                pars[11].Value = CE.PorcCal3;
                pars[12] = new SqlParameter("@calidad4", SqlDbType.Int);
                pars[12].Value = CE.ICalidad4;
                pars[13] = new SqlParameter("@porcentaje_cal4", SqlDbType.Int);
                pars[13].Value = CE.PorcCal4;
                pars[14] = new SqlParameter("@tipo_procesadas", SqlDbType.Int);
                pars[14].Value = CE.TipoProc;
                pars[15] = new SqlParameter("@tipo_inventario", SqlDbType.Int);
                pars[15].Value = CE.TipoInv;
                pars[16] = new SqlParameter("@tipo_desperdicio", SqlDbType.Int);
                pars[16].Value = CE.TipoDesp;
                pars[17] = new SqlParameter("@tipo_desp_verde", SqlDbType.Int);
                pars[17].Value = CE.TipoVerde;
                pars[18] = new SqlParameter("@tipo_desp_quemado", SqlDbType.Int);
                pars[18].Value = CE.TipoQuemado;
                pars[19] = new SqlParameter("@tipo_porcent_cal1", SqlDbType.Int);
                pars[19].Value = CE.TipoCal1;
                pars[20] = new SqlParameter("@tipo_porcent_cal2", SqlDbType.Int);
                pars[20].Value = CE.TipoCal2;
                pars[21] = new SqlParameter("@tipo_porcent_cal3", SqlDbType.Int);
                pars[21].Value = CE.TipoCal3;
                pars[22] = new SqlParameter("@tipo_porcent_cal4", SqlDbType.Int);
                pars[22].Value = CE.TipoCal4;




                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardarMetasProd: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion

        #region IniciarTerminarInventario
        public void IniciarTerminarInventario(int codInventario, bool enProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "codinventario=" + codInventario.ToString() + ", enproceso=" + enProceso.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("IniciarTerminarInventario", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Inventario_Ini");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_inventario", SqlDbType.Int);
                pars[0].Value = codInventario;
                pars[1] = new SqlParameter("@enproceso", SqlDbType.Bit);
                pars[1].Value = enProceso ? 1 : 0;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", IniciarTerminarInventario: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region AutorizaConfigBanco
        public void AutorizaConfigBanco(BE.ConfigBancos AU)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {
                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("AutorizaConfigBanco", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_ConfigBanco_Aut");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_config_banco", SqlDbType.Int);
                pars[0].Value = AU.CodConfigBanco;
                pars[1] = new SqlParameter("@UsuarioAut", SqlDbType.Int);
                pars[1].Value = AU.CodUsuarioAutoriza;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", AutorizaConfigBanco: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region AutorizaCondicionEsmalte
        public void AutorizaCondicionEsmalte(BE.CondicionEsmalteAutoriza AC)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {
                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("AutorizaCondicionEsmalte", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_CondicionEsmalte_Aut");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_condicion_esmalte", SqlDbType.Int);
                pars[0].Value = AC.CodCondicionEsmalte;
                pars[1] = new SqlParameter("@UsuarioAut", SqlDbType.Int);
                pars[1].Value = AC.UsuarioAutoriza;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", AutorizaCondicionEsmalte: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region AutorizaCondicionPasta
        public void AutorizaCondicionPasta(BE.CondicionPastaAutoriza AC)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {
                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("AutorizaCondicionPasta", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_CondicionPasta_Aut");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_condicion_pasta", SqlDbType.Int);
                pars[0].Value = AC.CodCondicionPasta;
                pars[1] = new SqlParameter("@UsuarioAut", SqlDbType.Int);
                pars[1].Value = AC.UsuarioAutoriza;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", AutorizaCondicionPasta: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region AutorizaCondicionOperacion
        public void AutorizaCondicionOperacion(BE.CondicionOperacionAutoriza AC)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {
                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("AutorizaCondicionOperacion", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_CondicionOperacion_Aut");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_condicion_operacion", SqlDbType.Int);
                pars[0].Value = AC.CodCondicionOperacion;
                pars[1] = new SqlParameter("@UsuarioAut", SqlDbType.Int);
                pars[1].Value = AC.UsuarioAutoriza;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", AutorizaCondicionOperacion: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion

        #region EliminaRutaProceso
        public BE.RutaProceso EliminaRutaProceso(BE.RutaProceso RutaProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {
                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("EliminaRutaProceso", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_RutaProceso_Del");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[3];
                pars[0] = new SqlParameter("@planta", SqlDbType.Int);
                pars[0].Value = RutaProceso.CodPlanta;
                pars[1] = new SqlParameter("@proceso", SqlDbType.Int);
                pars[1].Value = RutaProceso.CodProceso;
                pars[2] = new SqlParameter("@Mensaje", SqlDbType.NVarChar, 255);
                pars[2].Direction = ParameterDirection.Output;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                RutaProceso.ExceptionMessage = Convert.ToString(pars[2].Value);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", EliminaRutaProceso: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return RutaProceso;
        }
        #endregion
        #region EliminaConfigBanco
        public BE.ConfigBancos EliminaConfigBanco(BE.ConfigBancos CB)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {
                #region InsertarRegistroSolicitud

                string sPars = CB.CodConfigBanco.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("EliminaConfigBanco", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_ConfigBanco_Del");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_config_banco", SqlDbType.Int);
                pars[0].Value = CB.CodConfigBanco;
                pars[1] = new SqlParameter("@mensaje", SqlDbType.NVarChar, 255);
                pars[1].Direction = ParameterDirection.Output;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                CB.ExceptionMessage = Convert.ToString(pars[1].Value);

                #endregion Query Execution

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", EliminaConfigBanco: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return CB;
        }
        #endregion
        #region EliminaTurno
        public void EliminaTurno(int CodTurno)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("EliminaTurno", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Turno_Upd");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@cod_turno", SqlDbType.Int);
                pars[0].Value = CodTurno;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", EliminaTurno: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region EliminaRol
        public void EliminaRol(int CodRol)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("EliminaRol", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Rol_Upd");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@cod_rol", SqlDbType.Int);
                pars[0].Value = CodRol;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", EliminaRol: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion
        #region EliminaPrueba
        public void EliminaPrueba(int CodPrueba)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("EliminaPrueba", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Prueba_Del");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@cod_prueba", SqlDbType.Int);
                pars[0].Value = CodPrueba;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", EliminaPrueba: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion

        #region EliminaUsuario
        public BE.Usuario EliminaUsuario(BE.Usuario Usuario)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("EliminaUsuario", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Usuario_Upd");
                #endregion Query

                #region Parameters
                pars = new SqlParameter[1];

                pars[0] = new SqlParameter("@cod_usuario", SqlDbType.Int);
                pars[0].Value = Usuario.CodUsuario;


                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", EliminaUsuario: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return Usuario;
        }
        #endregion

        #region GuardaConfig
        public void GuardaConfig(BE.Configuracion conf)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "GuardaConfig=" + conf.CodConfiguracion.ToString() + "," + conf.ValorConfiguracion.ToString();
                lCodRegistro = this.InsertarRegistroSolicitud("GuardaConfig", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Configuracion_Upd");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@cod_configuracion", SqlDbType.Int);
                pars[0].Value = conf.CodConfiguracion;
                pars[1] = new SqlParameter("@valor", SqlDbType.Int);
                pars[1].Value = conf.ValorConfiguracion;

                #endregion

                #region Query Execution

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution


            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", GuardaConfig: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
        }
        #endregion

        #region ObtenerCodigoBarras
        public SolutionEntityList<BE.CodigoBarra> ObtenerCodigoBarras(int planta, int centroTrabajo, int banco, int empleado)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtObj = null;
            long lCodRegistro = -1;
            SolutionEntityList<BE.CodigoBarra> l_Res = new SolutionEntityList<BE.CodigoBarra>();
            //
            try
            {

                #region InsertarRegistroSolicitud

                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("ObtenerCodigoBarras", DateTime.Now, sPars);

                #endregion InsertarRegistroSolicitud

                #region Connection Configuration

                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                #endregion Connection Configuration

                #region Query
                queryString = new StringBuilder();
                queryString.Append("sp_Codigo_Barras_Lis");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@planta", SqlDbType.Int);
                pars[0].Value = planta;
                pars[1] = new SqlParameter("@centroTrabajo", SqlDbType.Int);
                pars[1].Value = centroTrabajo;
                pars[2] = new SqlParameter("@banco", SqlDbType.Int);
                pars[2].Value = banco;
                pars[3] = new SqlParameter("@empleado", SqlDbType.Int);
                pars[3].Value = empleado;

                #endregion


                #region Query Execution

                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);

                #endregion Query Execution

                #region Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

                foreach (DataRow dr in dtObj.Rows)
                {
                    l_Res.Load(new BE.CodigoBarra(dr));
                }

                #endregion Mapear el DataTable en un BusinessEntity o lista de BusinessEntity

            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", ObtenerUsuarios: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return l_Res;
        }
        #endregion

        #region CambiarContrasena
        public BE.ContrasenaC CambiarContrasena(BE.ContrasenaC Contrasena)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            DataTable dtUsuario = null;
            //
            try
            {
                if (!ValidarPoliticaContrasena(Contrasena.ContrasenaNueva))
                {
                    string sMensaje = "La contraseña no cumple con las politicas de seguridad, por favor verifique:\n";
                    sMensaje += "\t- Longitud minima de la contraseña.\n";
                    sMensaje += "\t- La contraseña debe incluir almenos uno de los caracteres entre A-Z, a-z, 0-9.";
                    Contrasena.ExceptionMessage = sMensaje;
                    return Contrasena;
                }
                dtUsuario = ObtenerUsuario(Contrasena.codUsuario, null, null, null);
                if (dtUsuario == null || dtUsuario.Rows.Count == 0 || dtUsuario.Rows.Count > 1)
                    throw new Exception("No existe el usuario ó el usuario esta duplicado.");
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("CambiarContrasena", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spCambioContrasena");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@CodUsuario", SqlDbType.Int);
                pars[0].Value = Contrasena.codUsuario;
                //
                if (Contrasena.Contrasena == "Lamosa06")
                {
                    pars[1] = new SqlParameter("@ContrasenaAnt", SqlDbType.VarChar, 255);
                    pars[1].Value = EncriptarContrasenaUsuario(Contrasena.Contrasena, Contrasena.Contrasena);
                }
                else
                {
                    pars[1] = new SqlParameter("@ContrasenaAnt", SqlDbType.VarChar, 255);
                    pars[1].Value = EncriptarContrasenaUsuario(dtUsuario.Rows[0]["login"].ToString(), Contrasena.Contrasena);
                }
                if (Contrasena.ContrasenaNueva == "Lamosa06")
                {
                    pars[2] = new SqlParameter("@ContrasenaNueva", SqlDbType.VarChar, 255);
                    pars[2].Value = EncriptarContrasenaUsuario(Contrasena.ContrasenaNueva, Contrasena.ContrasenaNueva);
                }
                else
                {
                    pars[2] = new SqlParameter("@ContrasenaNueva", SqlDbType.VarChar, 255);
                    pars[2].Value = EncriptarContrasenaUsuario(dtUsuario.Rows[0]["login"].ToString(), Contrasena.ContrasenaNueva);
                }
                //
                pars[3] = new SqlParameter("@mensaje", SqlDbType.VarChar, 255);
                pars[3].Direction = ParameterDirection.Output;
                #endregion
                #region Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                Contrasena.ExceptionMessage = pars[3].Value.ToString();
                #endregion Query Execution
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud

                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);

                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", CambiarContrasena: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
            return Contrasena;
        }
        public DataTable ObtenerUsuario(int? CodigoUsuario, string NombreUsuario, int? CodigoEmpleado, int? CodigoRol)
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
                queryString.Append("spUsuarioSel");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[4];
                pars[0] = new SqlParameter("@CodigoUsuario", SqlDbType.Int);
                if (CodigoUsuario.HasValue)
                    pars[0].Value = CodigoUsuario.Value;
                pars[1] = new SqlParameter("@NombreUsuario", SqlDbType.VarChar);
                if (!string.IsNullOrEmpty(NombreUsuario))
                    pars[1].Value = NombreUsuario;
                pars[2] = new SqlParameter("@CodigoEmpleado", SqlDbType.Int);
                if (CodigoEmpleado.HasValue)
                    pars[2].Value = CodigoEmpleado.Value;
                pars[3] = new SqlParameter("@CodigoRol", SqlDbType.Int);
                if (CodigoRol.HasValue)
                    pars[3].Value = CodigoRol;
                #endregion
                #region Query Execution
                dtObj = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if (dtObj == null || dtObj.Rows.Count == 0)
                    throw new Exception("No existe usuarios con los parámetros de busqueda indicados.");
                foreach (DataRow row in dtObj.Rows)
                {
                    if (row["password"].ToString() == ObtenerContrasenaDefault())
                        row["password"] = DesencriptarContrasenaUsuario("Lamosa06", row["password"].ToString());
                    else
                        row["password"] = DesencriptarContrasenaUsuario(row["login"].ToString(), row["password"].ToString()); 
                }
                #endregion Query Execution
                return dtObj;
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
        #endregion
        #region CambiarContrasenaLogin
        public BE.ContrasenaL CambiarContrasenaLogin(BE.ContrasenaL Contrasena)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lCodRegistro = -1;
            //
            try
            {
                if (!ValidarPoliticaContrasena(Contrasena.ContrasenaNueva))
                {
                    string sMensaje = "La contraseña no cumple con las politicas de seguridad, por favor verifique:\n";
                    sMensaje += "\t- Longitud minima de la contraseña.\n";
                    sMensaje += "\t- La contraseña debe incluir almenos uno de los caracteres entre A-Z, a-z, 0-9.";
                    Contrasena.ExceptionMessage = sMensaje;
                    return Contrasena;
                }
                #region InsertarRegistroSolicitud
                string sPars = "";
                lCodRegistro = this.InsertarRegistroSolicitud("CambiarContrasenaLogin", DateTime.Now, sPars);
                #endregion InsertarRegistroSolicitud
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
                pars[0].Value = Contrasena.Usuario;
                //
                if (Contrasena.Contrasena == "Lamosa06")
                {
                    pars[1] = new SqlParameter("@ContrasenaAnt", SqlDbType.VarChar, 255);
                    pars[1].Value = EncriptarContrasenaUsuario(Contrasena.Contrasena, Contrasena.Contrasena);
                }
                else
                {
                    pars[1] = new SqlParameter("@ContrasenaAnt", SqlDbType.VarChar, 255);
                    pars[1].Value = EncriptarContrasenaUsuario(Contrasena.Usuario, Contrasena.Contrasena);                    
                }
                if (Contrasena.ContrasenaNueva == "Lamosa06")
                {
                    pars[2] = new SqlParameter("@ContrasenaNueva", SqlDbType.VarChar, 255);
                    pars[2].Value = EncriptarContrasenaUsuario(Contrasena.ContrasenaNueva, Contrasena.ContrasenaNueva);
                }
                else
                {
                    pars[2] = new SqlParameter("@ContrasenaNueva", SqlDbType.VarChar, 255);
                    pars[2].Value = EncriptarContrasenaUsuario(Contrasena.Usuario, Contrasena.ContrasenaNueva);
                }
                //
                pars[3] = new SqlParameter("@mensaje", SqlDbType.VarChar, 255);
                pars[3].Direction = ParameterDirection.Output;
                #endregion
                #region Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                Contrasena.ExceptionMessage = pars[3].Value.ToString();
                #endregion Query Execution
            }
            catch (Exception ex)
            {
                #region ActualizarRegistroSolicitud
                this.ActualizarRegistroSolicitud(lCodRegistro, false, ex.Message);
                #endregion ActualizarRegistroSolicitud
                throw new Exception(this.sClassName + ", CambiarContrasenaLogin: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
            return Contrasena;
        }
        public string CambiarContrasena(string sUsuario, string sContrasenaActual, string sContrasenaNueva)
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
                if (sContrasenaActual == "Lamosa06")
                {
                    pars[1] = new SqlParameter("@ContrasenaAnt", SqlDbType.VarChar, 255);
                    pars[1].Value = EncriptarContrasenaUsuario(sContrasenaActual, sContrasenaActual);
                }
                else
                {
                    pars[1] = new SqlParameter("@ContrasenaAnt", SqlDbType.VarChar, 255);
                    pars[1].Value = EncriptarContrasenaUsuario(sUsuario, sContrasenaActual);
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
                throw new Exception(this.sClassName + ", CambiarContrasena: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
        }
        #endregion

        #region ObtenerInfoDashboard
        public DataSet ObtenerInfoDashboard(int iCodPlanta, int iRol)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet dsRes = new DataSet();
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                int i = 0;
                queryString = new StringBuilder();
                queryString.Append("spObtenerInfoDashboard");
                pars = new SqlParameter[2];
                pars[i] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                pars[i++].Value = iCodPlanta;
                pars[i] = new SqlParameter("@CodRol", SqlDbType.Int);
                pars[i++].Value = iRol;
                dsRes = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerInfoDashboard: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return dsRes;
        }
        #endregion
        #region ObtenerKardexExportar
        public DataSet ObtenerKardexExportar(int iCodigo)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet dtRes = new DataSet();
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                int i = 0;
                queryString = new StringBuilder();
                queryString.Append("sp_KardexProductoExportar_Lis");
                pars = new SqlParameter[1];
                pars[i] = new SqlParameter("@codigo", SqlDbType.Int);
                pars[i++].Value = iCodigo;
                dtRes = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerKardexExportar: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return dtRes;
        }
        #endregion

        public IList<Maquina> ObtenerHornoClasificado(int iPlanta, int iProceso)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataSet dsRes = new DataSet();
            IList<Maquina> lMaquina = null;
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                int i = 0;
                queryString = new StringBuilder();
                queryString.Append("spObtenerHornoClasificado");
                pars = new SqlParameter[2];
                pars[i] = new SqlParameter("@CodigoPlanta", SqlDbType.Int);
                pars[i++].Value = iPlanta;
                pars[i] = new SqlParameter("@CodigoProceso", SqlDbType.Int);
                pars[i++].Value = iProceso;
                dsRes = dbObj.ObtenerRegistrosDS(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if (dsRes != null && dsRes.Tables.Count > 0 && dsRes.Tables[0].Rows.Count > 0)
                {
                    lMaquina = new List<Maquina>();
                    foreach (DataRow row in dsRes.Tables[0].Rows)
                    {
                        Maquina maquina = new Maquina();
                        maquina.CodMaquina = Convert.ToInt32(row["Codigo"]);
                        maquina.ClaveMaquina = row["Clave"].ToString();
                        maquina.DesMaquina = row["Descripcion"].ToString();
                        maquina.CodTipoMaquina = Convert.ToInt32(row["CodigoTipoMaquina"]);
                        maquina.DesTipoMaquina = row["DescripcionTipoMaquina"].ToString();
                        maquina.CodProceso = Convert.ToInt32(row["CodigoProceso"]);
                        maquina.DesProceso = row["DescripcionProceso"].ToString();
                        maquina.CodCentroTrabajo = Convert.ToInt32(row["CodigoCentroTrabajo"]);
                        maquina.DesCentroTrabajo = row["DescripcionCentroTrabajo"].ToString();
                        lMaquina.Add(maquina);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerHornoClasificado: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return lMaquina;
        }

        public int ExisteSKU(string sku)
        {
            int res = 0;
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("HHVExisteModelo");
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@ClaveArticulo", SqlDbType.NVarChar, 20);
                parameters[i++].Value = sku;
                parameters[i] = new SqlParameter("@Cod", SqlDbType.Int);
                parameters[i].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
                res = int.Parse(parameters[i].Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExisteSKU: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return res;
        }

        public int InsertarPieza(string sCodigoBarras, int iCodigoArticulo, int iCodigoColor, int iCodigoCalidad,
                            int iCodigoPlanta, int iCodigoProceso, int iCodigoEstado, int iConfigBanco)
        {
            int res = 0;
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("spInsertarPieza");
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[14];
                parameters[i] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                parameters[i++].Value = iCodigoPlanta;
                parameters[i] = new SqlParameter("@CodBarras", SqlDbType.NVarChar, 15);
                parameters[i++].Value = sCodigoBarras;
                parameters[i] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                parameters[i++].Value = iConfigBanco;
                parameters[i] = new SqlParameter("@CodConsecutivo", SqlDbType.Int);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@Posicion", SqlDbType.Int);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@CodArticulo", SqlDbType.Int);
                parameters[i++].Value = iCodigoArticulo;
                parameters[i] = new SqlParameter("@CodUltimoEstado", SqlDbType.Int);
                parameters[i++].Value = iCodigoEstado;
                parameters[i] = new SqlParameter("@CodUltimoProceso", SqlDbType.Int);
                parameters[i++].Value = iCodigoProceso;
                parameters[i] = new SqlParameter("@CodMolde", SqlDbType.Int);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@IdBase", SqlDbType.Int);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@FechaRegistro", SqlDbType.DateTime);
                parameters[i++].Value = DateTime.Now;

                parameters[i] = new SqlParameter("@CodColor", SqlDbType.Int);
                parameters[i++].Value = iCodigoColor;
                parameters[i] = new SqlParameter("@CodCalidad", SqlDbType.Int);
                parameters[i++].Value = iCodigoCalidad;

                parameters[i] = new SqlParameter("@CodPieza", SqlDbType.Int);
                parameters[i].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
                res = int.Parse(parameters[i].Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarPieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return res;
        }
        public bool InsertarPiezaReemplazo(int iCodPieza, int iCodProcesoPiezaReem)
        {
            int res = 0;
            bool bRes = false;
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("spInsertarEtiquetaReemplazo");
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[12];
                parameters[i] = new SqlParameter("@CodPlanta", SqlDbType.Int);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@CodBarras", SqlDbType.NVarChar, 15);
                parameters[i++].Value = iCodPieza;
                parameters[i] = new SqlParameter("@CodModelo", SqlDbType.Int);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@CodColor", SqlDbType.Int);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@CodCalidad", SqlDbType.Int);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@CodUltimoProceso", SqlDbType.Int);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@CodUltimoEstado", SqlDbType.Int);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@CodConfigHandheld", SqlDbType.BigInt);
                parameters[i++].Value = -1;
                parameters[i] = new SqlParameter("@FechaRegistro", SqlDbType.DateTime);
                parameters[i++].Value = DateTime.Now;
                parameters[i] = new SqlParameter("@CodProcesoPiezaReem", SqlDbType.Int);
                parameters[i++].Value = iCodProcesoPiezaReem;

                parameters[i] = new SqlParameter("@CodPieza", SqlDbType.Int);
                parameters[i++].Direction = ParameterDirection.Output;
                parameters[i] = new SqlParameter("@CodPiezaTransaccion", SqlDbType.BigInt);
                parameters[i].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
                res = int.Parse(parameters[--i].Value.ToString());
                bRes = res > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExisteSKU: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return bRes;
        }
        public bool ExistePieza(string sCodBarras)
        {
            bool bRes = false;
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            try
            {
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                queryString = new StringBuilder();
                queryString.Append("spExistePieza");
                int i = 0;
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[i] = new SqlParameter("@CodBarras", SqlDbType.NVarChar, 15);
                parameters[i++].Value = sCodBarras;
                parameters[i] = new SqlParameter("@Existe", SqlDbType.Bit);
                parameters[i].Direction = ParameterDirection.Output;

                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), parameters);
                bRes = bool.Parse(parameters[i].Value.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ExistePieza: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return bRes;
        }
        public long InsertarConfigHandHeld(int iCodUsuario, int iCodOperador, int iClaveMFGSupervisor,
                                            DateTime dtFecha, int iCodTurno, int iCodPlanta, int iCodProceso,
                                            int iCodConfigBanco, DateTime dtFechaRegistro)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lRes = -1;
            //
            try
            {
                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);

                // Query
                queryString = new StringBuilder();
                queryString.Append("spInsertarConfigHandHeld");

                // Parameters
                int index = 0;
                pars = new SqlParameter[10];
                pars[index] = new SqlParameter("@CodUsuario", SqlDbType.Int);
                pars[index++].Value = iCodUsuario;
                pars[index] = new SqlParameter("@CodOperador", SqlDbType.Int);
                pars[index++].Value = iCodOperador;
                pars[index] = new SqlParameter("@ClaveMFGSupervisor", SqlDbType.Int);
                pars[index++].Value = iClaveMFGSupervisor;
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
                throw new Exception(this.sClassName + ", InsertarConfigHandHeld: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return lRes;
        }
        public long InsertarPiezaTransaccion(long lCodConfigHandheld, int iCodPieza, DateTime dtFechaRegistro)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            long lRes = -1;
            //
            try
            {
                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                // Query
                queryString = new StringBuilder();
                queryString.Append("spInsertarPiezaTransaccion");
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
                throw new Exception(this.sClassName + ", InsertarPiezaTransaccion: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return lRes;
        }
        public int ObtenerCodigoConfigBanco(int iCodMaquina)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            int iRes = -1;
            try
            {
                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                // Query
                queryString = new StringBuilder();
                queryString.Append("spObtenerCodigoConfigBanco");
                // Parameters
                pars = new SqlParameter[2];
                pars[0] = new SqlParameter("@CodMaquina", SqlDbType.BigInt);
                pars[0].Value = iCodMaquina;
                pars[1] = new SqlParameter("@CodConfigBanco", SqlDbType.Int);
                pars[1].Direction = ParameterDirection.Output;
                // Query Execution
                dbObj.EjecutarConsulta(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                iRes = Convert.ToInt32(pars[1].Value);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", InsertarPiezaTransaccion: " + ex.Message);
            }
            finally
            {
                dbObj.Dispose();
            }
            return iRes;
        }
        public bool GrabarPiezaReemplazo(string sCodigoBarras, int iCodigoArticulo, int iCodigoColor,
                            int iCodigoCalidad, int iCodigoPlanta, int iCodigoProceso, int iCodigoEstado,
                            int iCodUsuario, int iCodOperador, int iClaveMFGSupervisor, int iCodTurno, int iCodMaquina)
        {

            try
            {
                using (TransactionScope transactionScope = new TransactionScope())
                {
                    Proceso procesoAnterior = ObtenerProcesoAnterior(iCodigoProceso);
                    int iCodConfigBanco = ObtenerCodigoConfigBanco(iCodMaquina);
                    if (iCodConfigBanco < 1) throw new Exception("No se pudo obtener una Configuracion de Banco Valida");
                    int iCodPieza = InsertarPieza(sCodigoBarras, iCodigoArticulo, iCodigoColor, iCodigoCalidad, iCodigoPlanta, procesoAnterior.CodProceso, iCodigoEstado, iCodConfigBanco);
                    long lCodConfigHandHeld = InsertarConfigHandHeld(iCodUsuario, iCodOperador, iClaveMFGSupervisor, DateTime.Now, iCodTurno, iCodigoPlanta, procesoAnterior.CodProceso, iCodConfigBanco, DateTime.Now);
                    long lCodPiezaTransaccion = -1;
                    bool bPiezaReemplazo = false;
                    if (iCodPieza > 0 && lCodConfigHandHeld > 0)
                    {
                        lCodPiezaTransaccion = InsertarPiezaTransaccion(lCodConfigHandHeld, iCodPieza, DateTime.Now);
                        bPiezaReemplazo = InsertarPiezaReemplazo(iCodPieza, iCodigoProceso);
                        if (lCodPiezaTransaccion > 0 && bPiezaReemplazo)
                        {
                            transactionScope.Complete();
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return false;
        }


        public IList<ArticuloCbo> ObtenerModelos(int iTipoModelo)
        {
            IList<ArticuloCbo> lArticulo = null;
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            DataTable dtRes = null;
            try
            {
                // Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                // Query
                queryString = new StringBuilder();
                queryString.Append("HHVObtenerModelos");
                // Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodTipoModelo", SqlDbType.Int);
                pars[0].Value = iTipoModelo;
                // Query Execution
                dtRes = dbObj.ObtenerRegistros(DA01.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                if (dtRes != null)
                {
                    lArticulo = new List<ArticuloCbo>();
                    foreach (DataRow row in dtRes.Rows)
                    {
                        ArticuloCbo articulo = new ArticuloCbo();
                        articulo.CodArticulo = Convert.ToInt32(row["CodModelo"]);
                        articulo.ClaveArticulo = row["ClaveModelo"].ToString();
                        articulo.DesArticulo = row["DesModelo"].ToString();
                        lArticulo.Add(articulo);
                    }
                }
            }
            catch (Exception e)
            { throw e; }
            return lArticulo;
        }


        #region ObtenerAlerta
        public DataTable ObtenerAlerta(int iCodigoAlerta, int iCodigoTipoAlerta, int iCodigoPlanta, int iCodigoProceso)
        {
            DataTable dtAlerta = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spGetAlerta";
                        if (iCodigoAlerta > 0) cmd.Parameters.AddWithValue("@Codigo", iCodigoAlerta);
                        if (iCodigoTipoAlerta > 0) cmd.Parameters.AddWithValue("@TipoAlerta", iCodigoTipoAlerta);
                        if (iCodigoPlanta > 0) cmd.Parameters.AddWithValue("@Planta", iCodigoPlanta);
                        if (iCodigoProceso > 0) cmd.Parameters.AddWithValue("@Proceso", iCodigoProceso);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtAlerta = new DataTable("Alerta");
                            da.Fill(dtAlerta);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerAlerta: " + ex.Message);
            }
            return dtAlerta == null ? new DataTable("Alerta") : dtAlerta;
        }
        public DataTable ObtenerAlerta(int iCodigoAlerta, int iCodigoTipoAlerta, int iCodigoPlanta, int iCodigoProceso, int iCodigoEmpleado)
        {
            DataTable dtAlerta = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spGetAlerta";
                        if (iCodigoAlerta > 0) cmd.Parameters.AddWithValue("@Codigo", iCodigoAlerta);
                        if (iCodigoTipoAlerta > 0) cmd.Parameters.AddWithValue("@TipoAlerta", iCodigoTipoAlerta);
                        if (iCodigoPlanta > 0) cmd.Parameters.AddWithValue("@Planta", iCodigoPlanta);
                        if (iCodigoProceso > 0) cmd.Parameters.AddWithValue("@Proceso", iCodigoProceso);
                        if (iCodigoEmpleado > 0) cmd.Parameters.AddWithValue("@CodigoOperador", iCodigoEmpleado);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtAlerta = new DataTable("Alerta");
                            da.Fill(dtAlerta);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerAlerta: " + ex.Message);
            }
            return dtAlerta == null ? new DataTable("Alerta") : dtAlerta;
        }
        #endregion
        #region ObtenerPiezasConResidencia
        public DataTable ObtenerPiezasConResidencia(int iCodigoAlerta, int iCodigoPlanta, int iCodigoProceso, int iCodigoTipoArticulo, int iCodigoArticulo, int iCodigoMaquina, int iCodigoColor, int iCodigoEmpleado, int iCodigoTurno, int iDiasResidencia)
        {
            DataTable dtPiezasConResidencia = null;
            try
            {
                if (iCodigoAlerta < 1) throw new Exception("No se proporciono una alerta valida.");
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerPiezasResidencia";
                        cmd.Parameters.AddWithValue("@CodigoAlerta", iCodigoAlerta);
                        if (iCodigoPlanta > 0) cmd.Parameters.AddWithValue("@CodigoPlanta", iCodigoPlanta);
                        if (iCodigoProceso > 0) cmd.Parameters.AddWithValue("@CodigoProceso", iCodigoProceso);
                        if (iCodigoTipoArticulo > 0) cmd.Parameters.AddWithValue("@CodigoTipoArticulo", iCodigoTipoArticulo);
                        if (iCodigoArticulo > 0) cmd.Parameters.AddWithValue("@CodigoArticulo", iCodigoArticulo);
                        if (iCodigoMaquina > 0) cmd.Parameters.AddWithValue("@CodigoMaquina", iCodigoMaquina);
                        if (iCodigoColor > 0) cmd.Parameters.AddWithValue("@CodigoColor", iCodigoColor);
                        if (iCodigoEmpleado > 0) cmd.Parameters.AddWithValue("@ClaveEmpleado", iCodigoEmpleado);
                        if (iCodigoTurno > 0) cmd.Parameters.AddWithValue("@CodigoTurno", iCodigoTurno);
                        if (iDiasResidencia > 0) cmd.Parameters.AddWithValue("@DiasResidencia", iDiasResidencia);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtPiezasConResidencia = new DataTable("PiezasConResidencia");
                            da.Fill(dtPiezasConResidencia);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasConResidencia: " + ex.Message);
            }
            return dtPiezasConResidencia == null ? new DataTable("PiezasConResidencia") : dtPiezasConResidencia;
        }
        #endregion

        #region PiezaBajaPorResidencia
        public bool PiezaBajaPorResidencia(DataTable dtPiezaParaBaja, int iCodigoUsuario)
        {
            bool bBajaPieza = true;
            DateTime dtFechaInsercion = DateTime.Now;
            try
            {
                if (dtPiezaParaBaja == null) throw new Exception("No se ha indicado Informacion para procesar.");
                foreach (DataRow drPiezasResidencia in dtPiezaParaBaja.Rows)
                {
                    using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "spPriezaBajaPorResidencia";
                            cmd.Parameters.AddWithValue("@CodigoBarras", drPiezasResidencia[0]);
                            cmd.Parameters.AddWithValue("@CodigoAlerta", drPiezasResidencia[1]);
                            cmd.Parameters.AddWithValue("@FechaInsercion", dtFechaInsercion);
                            cmd.Parameters.AddWithValue("@CodigoUsuario", iCodigoUsuario);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dtRespuestaBaja = new DataTable("RespuestaBaja");
                                da.Fill(dtRespuestaBaja);
                                if (!bBajaPieza)
                                    bBajaPieza = Boolean.Parse(dtRespuestaBaja.Rows[0][0].ToString());
                            }
                        }
                    }
                }
                    if(dtPiezaParaBaja.Rows.Count > 0)
                    EnviarMailPorBajaResidencia(Convert.ToInt32(dtPiezaParaBaja.Rows[0][1].ToString()), dtFechaInsercion);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasConResidencia: " + ex.Message);
            }
            return bBajaPieza;
        }
        #endregion
        #region EnviarMailPorBajaResidencia
        private bool EnviarMailPorBajaResidencia(int iCodigoAlerta, DateTime dtFechaInsercion)
        {
            bool bEnviarMail = false;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spEnviarMailPorBajaResidencia";
                        cmd.Parameters.AddWithValue("@CodigoAlerta", iCodigoAlerta);
                        cmd.Parameters.AddWithValue("@FechaInsercion", dtFechaInsercion);
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        bEnviarMail = true;
                        //using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        //{
                        //    DataTable dtRespuestaBaja = new DataTable("RespuestaEnvio");
                        //    da.Fill(dtRespuestaBaja);
                        //    bEnviarMail = Boolean.Parse(dtRespuestaBaja.Rows[0][0].ToString());
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", EnviarMailPorBajaResidencia: " + ex.Message);
            }
            return bEnviarMail;
        }
        #endregion
        #region ObtenerPiezasConResidencia
        public DataTable ObtenerPiezasBajaResidencia(int iCodigoAlerta, int iCodigoPlanta, int iCodigoProceso, int iCodigoTipoArticulo, int iCodigoArticulo, int iCodigoMaquina, int iCodigoColor, int iCodigoEmpleado, int iCodigoTurno, int iDiasResidencia)
        {
            DataTable dtPiezasConResidencia = null;
            try
            {
                if (iCodigoAlerta < 1) throw new Exception("No se proporciono una alerta valida.");
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerPiezasBajaResidencia";
                        cmd.Parameters.AddWithValue("@CodigoAlerta", iCodigoAlerta);
                        if (iCodigoPlanta > 0) cmd.Parameters.AddWithValue("@CodigoPlanta", iCodigoPlanta);
                        if (iCodigoProceso > 0) cmd.Parameters.AddWithValue("@CodigoProceso", iCodigoProceso);
                        if (iCodigoTipoArticulo > 0) cmd.Parameters.AddWithValue("@CodigoTipoArticulo", iCodigoTipoArticulo);
                        if (iCodigoArticulo > 0) cmd.Parameters.AddWithValue("@CodigoArticulo", iCodigoArticulo);
                        if (iCodigoMaquina > 0) cmd.Parameters.AddWithValue("@CodigoMaquina", iCodigoMaquina);
                        if (iCodigoColor > 0) cmd.Parameters.AddWithValue("@CodigoColor", iCodigoColor);
                        if (iCodigoEmpleado > 0) cmd.Parameters.AddWithValue("@ClaveEmpleado", iCodigoEmpleado);
                        if (iCodigoTurno > 0) cmd.Parameters.AddWithValue("@CodigoTurno", iCodigoTurno);
                        if (iDiasResidencia > 0) cmd.Parameters.AddWithValue("@DiasResidencia", iDiasResidencia);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtPiezasConResidencia = new DataTable("PiezasBajaResidencia");
                            da.Fill(dtPiezasConResidencia);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasConResidencia: " + ex.Message);
            }
            return dtPiezasConResidencia == null ? new DataTable("PiezasBajaResidencia") : dtPiezasConResidencia;
        }
        #endregion
        #region PriezaReestablecerDeResidencia
        public bool PriezaReestablecerResidencia(DataTable dtPiezaParaBaja)
        {
            bool bBajaPieza = true;
            try
            {
                if (dtPiezaParaBaja == null) throw new Exception("No se ha indicado Informacion para procesar.");
                foreach (DataRow drPiezasResidencia in dtPiezaParaBaja.Rows)
                {
                    using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                    {
                        using (SqlCommand cmd = cnn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "spPriezaReestablecerDeResidencia";
                            cmd.Parameters.AddWithValue("@CodigoBarras", drPiezasResidencia[0]);
                            cmd.Parameters.AddWithValue("@CodigoAlerta", drPiezasResidencia[1]);
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dtRespuestaBaja = new DataTable("RespuestaReestablecer");
                                da.Fill(dtRespuestaBaja);
                                if (!bBajaPieza)
                                    bBajaPieza = Boolean.Parse(dtRespuestaBaja.Rows[0][0].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerPiezasConResidencia: " + ex.Message);
            }
            return bBajaPieza;
        }
        #endregion
        #endregion Common
        public DateTime ObtenerFechaServidor()
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerFechaServidor";
                        SqlParameter parameter = new SqlParameter("@Fecha",SqlDbType.DateTime);
                        parameter.Direction= ParameterDirection.Output;
                        parameter.Value = DateTime.Now;
                        cmd.Parameters.Add(parameter);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        return Convert.ToDateTime(parameter.Value);
                    }
                }
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
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
        public DataTable ObtenerEtiqueta(string sClave)
        {
            DataTable dt = null;
            try
            {
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spObtenerEtiqueta";
                        if (!string.IsNullOrEmpty(sClave))
                            cmd.Parameters.AddWithValue("@Clave", sClave);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dt = new DataTable("Etiqueta");
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ObtenerEtiqueta: " + ex.Message);
            }
            return dt == null ? new DataTable("Etiqueta") : dt;
        }
        public DataTable ObtenerConfiguracionImpresionEtiqueta(string sClave)
        {
            DataTable dt = null;
            try
            {
                dt = ObtenerEtiqueta(sClave);
                if (dt == null) return new DataTable("ImpresionEtiquetas");
                if (dt.Columns.Contains("IdEtiqueta"))
                    dt.Columns.Remove("IdEtiqueta");
                if (dt.Columns.Contains("Ubicacion"))
                {
                    dt.Columns["Ubicacion"].ColumnName = "Modelo";
                    if (dt.Columns.Contains("Clave"))
                    {
                        dt.Columns["Clave"].ColumnName = "Calidad";
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["Calidad"].ToString().IndexOf('-') > 0)
                            {
                                row["Modelo"] = row["Calidad"].ToString().Substring(0, row["Calidad"].ToString().IndexOf('-'));
                                if (row["Calidad"].ToString().IndexOf('-') == (row["Calidad"].ToString().Length - 1))
                                    row["Calidad"] = row["Calidad"].ToString().Substring(row["Calidad"].ToString().IndexOf('-'));
                                else
                                    row["Calidad"] = row["Calidad"].ToString().Substring(row["Calidad"].ToString().IndexOf('-') + 1);
                            }
                            else
                                row["Modelo"] = row["Calidad"];
                        }
                    }
                }
                if (dt.Columns.Contains("NumeroImpresiones"))
                {
                    dt.Columns["NumeroImpresiones"].ColumnName = "Impresiones";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ImpresionEtiqueta: " + ex.Message);
            }
            return dt == null ? new DataTable("ImpresionEtiqueta") : dt;
        }
        public bool ActualizarConfiguracionImpresionEtiqueta(string sClave, int iNumeroImpresion)
        {
            bool bEstatusOperacion = false;
            try
            {
                if (string.IsNullOrEmpty(sClave)) return bEstatusOperacion;
                using (SqlConnection cnn = new SqlConnection(this.sMSSQLServer_ConnectionString))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "spActualizarNumeroImpresionEtiqueta";
                        cmd.Parameters.AddWithValue("@Clave", sClave);
                        cmd.Parameters.AddWithValue("@NumeroImpresiones", iNumeroImpresion);
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dtEstatusOperacion = new DataTable("EstatusOperacion");
                            da.Fill(dtEstatusOperacion);
                            if (dtEstatusOperacion == null || dtEstatusOperacion.Rows.Count < 1) return bEstatusOperacion;
                            bEstatusOperacion = Convert.ToBoolean(dtEstatusOperacion.Rows[0]["EstatusOperacion"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", ActualizarConfiguracionImpresionEtiqueta: " + ex.Message);
            }
            return bEstatusOperacion;
        }
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
        public bool DesbloquearUsuario(int CodigoUsuario)
        {
            DA01.MSSQLServer dbObj = null;
            StringBuilder queryString = null;
            SqlParameter[] pars = null;
            try
            {
                #region Connection Configuration
                dbObj = new DA01.MSSQLServer(this.sMSSQLServer_ConnectionString);
                #endregion Connection Configuration
                #region Query
                queryString = new StringBuilder();
                queryString.Append("spDesbloquearUsuarioUpd");
                #endregion Query
                #region Parameters
                pars = new SqlParameter[1];
                pars[0] = new SqlParameter("@CodigoUsuario", SqlDbType.Int);
                pars[0].Value = CodigoUsuario;
                #endregion
                #region Query Execution
                dbObj.EjecutarConsulta(Common.DataAccess.CommandTypes.StoredProcedure, queryString.ToString(), pars);
                #endregion Query Execution
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", DesbloquearUsuario: " + ex.Message);
            }
            finally
            {
                if (dbObj != null) dbObj.Dispose();
            }
        }
        #endregion Methods

    } // class 
}
