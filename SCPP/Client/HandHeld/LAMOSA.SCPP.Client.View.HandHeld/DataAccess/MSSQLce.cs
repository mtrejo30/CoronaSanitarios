/*
 * Common.DataAccess.MSSQLce object
 * Author: Erwin Gomez Rivero
 * Date: 10/Nov/2010
 * Description: Objeto para acceso a datos a un manejador de bases de datos MS SQL Server Compact Edition.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;

namespace LAMOSA.SCPP.Client.View.HandHeld.DataAccess
{

    public class MSSQLce : IDisposable
    {

        #region Fields

        // Control de Excepciones.
        private string sClassName = String.Empty;

        private string sData_Source = string.Empty;
        private string sPassword = string.Empty;
        private string sEnlist = string.Empty;
        private string sMax_Buffer_Size = string.Empty;
        private string sMax_Database_Size = string.Empty;
        private string sMode = string.Empty;
        private string sPersist_Security_Info = string.Empty;
        private string sConnectionString = string.Empty;
        //
        private SqlCeConnection cnnConnection = null;
        private SqlCeCommand cmmCommand = null;
        private SqlCeDataAdapter dapDataAdapter = null;
        private SqlCeDataReader drdDataReader = null;
        private SqlCeTransaction trnTransaction = null;

        // Indica si el metodo Dispose ha sido llamado.
        private bool disposed = false;

        #endregion Fields

        #region Properties

        #region Data_Source
        public string Data_Source { get { return this.sData_Source; } set { this.sData_Source = value; this.GenerarCadenaConexion(); } }
        #endregion Data_Source
        #region Password
        public string Password { get { return this.sPassword; } set { this.sPassword = value; this.GenerarCadenaConexion(); } }
        #endregion Password
        #region Enlist
        public string Enlist { get { return this.sEnlist; } set { this.sEnlist = value; this.GenerarCadenaConexion(); } }
        #endregion Enlist
        #region Max_Buffer_Size
        public string Max_Buffer_Size { get { return this.sMax_Buffer_Size; } set { this.sMax_Buffer_Size = value; this.GenerarCadenaConexion(); } }
        #endregion Max_Buffer_Size
        #region Max_Database_Size
        public string Max_Database_Size { get { return this.sMax_Database_Size; } set { this.sMax_Database_Size = value; this.GenerarCadenaConexion(); } }
        #endregion Max_Database_Size
        #region Mode
        public string Mode { get { return this.sMode; } set { this.sMode = value; this.GenerarCadenaConexion(); } }
        #endregion Mode
        #region Persist_Security_Info
        public string Persist_Security_Info { get { return this.sPersist_Security_Info; } set { this.sPersist_Security_Info = value; this.GenerarCadenaConexion(); } }
        #endregion Persist_Security_Info

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public MSSQLce()
        {
            this.CrearObjetos();
        }
        public MSSQLce(string sConnectionString)
        {
            this.CrearObjetos();
            this.cnnConnection.ConnectionString = sConnectionString;
        }
        ~MSSQLce()
        {
            // Este destructor solo se ejecutara si el metodo Dispose no ha sido llamado.
            this.Dispose(false);
        }

        #endregion Constructors and Destructor

        #region Common

        #region Dispose (IDisposable Member)
        public void Dispose()
        {
            this.Dispose(true);
            // Solicita que el sistema no llame al finalizador del objeto especificado.
            GC.SuppressFinalize(this);
        }
        #endregion Dispose (IDisposable Member)
        #region Dispose
        private void Dispose(bool disposing)
        {
            // Verificar si el metodo Dispose no ha sido llamado.
            if (!this.disposed)
            {
                // Si disposing = true, entonces disponer recursos administrados y no administrados.
                if (disposing)
                {
                    // Disponer recursos administrados
                    if (this.cnnConnection != null)
                        this.cnnConnection.Dispose();

                    if (this.cmmCommand != null)
                        this.cmmCommand.Dispose();

                    if (this.dapDataAdapter != null)
                        this.dapDataAdapter.Dispose();

                    if (this.drdDataReader != null)
                        this.drdDataReader.Dispose();

                    if (this.trnTransaction != null)
                        this.trnTransaction.Dispose();
                }

                // Llamar los metodos apropiados para limpiar los recursos no administrados.
                // Si disposing = false, es ejecutado solo el siguiente codigo.

            }
            disposed = true;
        }
        #endregion Dispose

        #region CrearObjetos
        private void CrearObjetos()
        {
            try
            {
                this.sClassName = this.GetType().FullName;
                //
                this.cnnConnection = new SqlCeConnection();
                this.cmmCommand = new SqlCeCommand();
                this.dapDataAdapter = new SqlCeDataAdapter();
                this.cmmCommand.Connection = this.cnnConnection;
            }
            catch (SqlCeException ex)
            {
                throw new Exception(this.sClassName + ", CrearObjetos: " + ex.Message);
            }
        }
        #endregion CrearObjetos
        #region GenerarCadenaConexion
        private void GenerarCadenaConexion()
        {
            try
            {
                this.sConnectionString = @"Data Source = '" + this.sData_Source + @"'; ";
                this.sConnectionString += @"Password = '" + this.sPassword + @"'";
                if (!String.IsNullOrEmpty(this.sEnlist))
                    this.sConnectionString += @"; Enlist = " + this.sEnlist;
                if (!String.IsNullOrEmpty(this.sMax_Buffer_Size))
                    this.sConnectionString += @"; max buffer size = " + this.sMax_Buffer_Size;
                if (!String.IsNullOrEmpty(this.sMax_Database_Size))
                    this.sConnectionString += @"; max database size = " + this.sMax_Database_Size;
                if (!String.IsNullOrEmpty(this.sMode))
                    this.sConnectionString += @"; Mode = '" + this.sMode + @"'";
                if (!String.IsNullOrEmpty(this.sPersist_Security_Info))
                    this.sConnectionString += @"; Persist Security Info = " + this.sPersist_Security_Info;
                //
                this.cnnConnection.ConnectionString = this.sConnectionString;
            }
            catch (SqlCeException ex)
            {
                throw new Exception(this.sClassName + ", GenerarCadenaConexion: " + ex.Message);
            }
        }
        #endregion GenerarCadenaConexion
        #region AbrirConexion
        public void AbrirConexion()
        {
            try
            {
                this.cnnConnection.Open();
            }
            catch (SqlCeException ex)
            {
                throw new Exception(this.sClassName + ", AbrirConexion: " + ex.Message);
            }
        }
        #endregion AbrirConexion
        #region CerrarConexion
        public void CerrarConexion()
        {
            try
            {
                this.cnnConnection.Close();
            }
            catch (SqlCeException ex)
            {
                throw new Exception(this.sClassName + ", CerrarConexion: " + ex.Message);
            }
        }
        #endregion CerrarConexion
        #region CompruebaConexion
        public bool CompruebaConexion()
        {
            if (this.cnnConnection.State == ConnectionState.Open)
            {
                return true;
            }
            return false;
        }
        #endregion CompruebaConexion
        #region ObtenerRegistros
        public DataTable ObtenerRegistros(string sQuery, SqlCeParameter[] arrParameters)
        {
            String p = "ObtenerRegistros, ";
            DataTable dtObj = new DataTable();
            //
            try
            {

                this.cmmCommand.Parameters.Clear();
                foreach (SqlCeParameter Parameter in arrParameters)
                {
                    this.cmmCommand.Parameters.Add(Parameter);
                    p += ", " + Parameter.ParameterName + "=" + Parameter.Value;
                }
                this.cmmCommand.CommandType = CommandType.Text;
                this.cmmCommand.CommandText = sQuery;
                //this.cnnConnection.Open();
                this.drdDataReader = this.cmmCommand.ExecuteReader();
                dtObj.Rows.Clear();
                dtObj.Columns.Clear();
                dtObj.Load(this.drdDataReader);
                //
                return dtObj;
            }
            catch (SqlCeException ex)
            {
                new HHsvc.SCPP_HH().InsertaError(p, sQuery);
                throw new Exception(this.sClassName + ", ObtenerRegistros: " + ex.Message);
            }
            finally
            {
                //this.cnnConnection.Close();
            }
        }
        #endregion ObtenerRegistros
        #region EjecutarConsulta
        public void EjecutarConsulta(string sQuery, SqlCeParameter[] arrParameters)
        {
            try
            {
                this.cmmCommand.Parameters.Clear();
                foreach (SqlCeParameter Parameter in arrParameters)
                    this.cmmCommand.Parameters.Add(Parameter);
                this.cmmCommand.CommandType = CommandType.Text;
                this.cmmCommand.CommandText = sQuery;
                //this.cnnConnection.Open();
                int www = this.cmmCommand.ExecuteNonQuery();
            }
            catch (SqlCeException ex)
            {
                throw new Exception(this.sClassName + ", EjecutarConsulta: " + ex.Message);
            }
            catch (Exception ex) { 
                throw new Exception(this.sClassName + ", EjecutarConsulta: " + ex.Message);
            }
            finally
            {
                //this.cnnConnection.Close();
            }
        }
        #endregion EjecutarConsulta
        #region EjecutarTransaccion
        public void EjecutarTransaccion(string[] sQuerys, SqlCeParameter[][] arrParameters)
        {
            String q = "";
            String p = "ObtenerRegistros, ";
            try
            {
                this.cmmCommand.CommandType = CommandType.Text;
                //this.cnnConnection.Open();
                this.trnTransaction = this.cnnConnection.BeginTransaction();
                this.cmmCommand.Transaction = this.trnTransaction;
                //
                for (int intIndQ = 0; intIndQ < sQuerys.Length; intIndQ++)
                {
                    p = "ObtenerRegistros, ";
                    q = sQuerys[intIndQ];
                    this.cmmCommand.CommandText = sQuerys[intIndQ];
                    this.cmmCommand.Parameters.Clear();
                    foreach (SqlCeParameter Parameter in arrParameters[intIndQ]){
                        this.cmmCommand.Parameters.Add(Parameter);
                        p += ", " + Parameter.ParameterName + "=" + Parameter.Value;
                    }
                    this.cmmCommand.ExecuteNonQuery();
                }
                this.trnTransaction.Commit();
            }
            catch (SqlCeException ex)
            {
                new HHsvc.SCPP_HH().InsertaError(p, q);
                this.trnTransaction.Rollback();
                throw new Exception(this.sClassName + ", EjecutarTransaccion: " + ex.Message);
            }
            finally
            {
                //this.cnnConnection.Close();
            }
        }
        #endregion EjecutarTransaccion

        #endregion Common

        #endregion Methods

    }

}
