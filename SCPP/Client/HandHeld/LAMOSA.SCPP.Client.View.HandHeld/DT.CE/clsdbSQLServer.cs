using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace DT.CE
{
    /// <summary>
    /// clase operacional para el manejador de Base de Datos de SQL Server
    /// </summary>
    public class clsdbSQLServer : Idb
    {

        SqlCeConnection ocnx;
        clsConexion oconexionpublica;
        SqlCeTransaction otran;
        bool bit_entransaccion = false;
        bool bit_conectado = false;
        clsParamsSQL parametrosSQL = new clsParamsSQL();

        int nQueryTimeOut = 120;

        /// <summary>
        /// time out para la ejecución de un sp
        /// </summary>
        public int QueryTimeOut { get { return nQueryTimeOut; } set { nQueryTimeOut = value; } }

        public object parameters { get { return parametrosSQL; } }

        //public IsolationLevel TipoBloqueo { get { return nivelbloqueo; } set { nivelbloqueo = value; } }

        public clsdbSQLServer()
        {
        }
        /// <summary>
        /// Crea una nueva conexión a una base de datos de SQL
        /// </summary>
        /// <param name="conexionpublica">configuración de conexión</param>
        public clsdbSQLServer(clsConexion conexionpublica)
        {
            ocnx = new SqlCeConnection();
            this.oconexionpublica = conexionpublica;
            this.parametrosSQL = new clsParamsSQL();
        }
        /// <summary>
        /// Arma la cadena de conexión y establece la conexión fisica a la BD de SQLServer
        /// </summary>
        /// <returns>sise conectó satisfactoriamente o no</returns>
        public bool conecta()
        {
            //armar la cadena de conexión
            StringBuilder scs = new StringBuilder();

            if ((null != this.oconexionpublica.ConnectionString) && (0 != this.oconexionpublica.ConnectionString.Trim().Length))
            {
                scs.AppendFormat("{0:s}", this.oconexionpublica.ConnectionString);
            }
            else
            {
                if ((null != this.oconexionpublica.servidor) && (0 != this.oconexionpublica.servidor.Trim().Length))
                {
                    scs.AppendFormat("server={0:s};", this.oconexionpublica.servidor);
                }
                if ((null != this.oconexionpublica.bd) && (0 != this.oconexionpublica.bd.Trim().Length))
                {
                    scs.AppendFormat("database={0:s};", this.oconexionpublica.bd);
                }
                if (this.oconexionpublica.TrustedConnection == false)
                {
                    if ((null != this.oconexionpublica.usuario) && (0 != this.oconexionpublica.usuario.Trim().Length))
                    {
                        scs.AppendFormat("user id={0:s};", this.oconexionpublica.usuario);
                    }
                    if ((null != this.oconexionpublica.password) && (0 != this.oconexionpublica.password.Trim().Length))
                    {
                        scs.AppendFormat("password={0:s};", this.oconexionpublica.password);
                    }
                }
                else
                {
                    scs.AppendFormat("Integrated Security={0:s};", "SSPI");
                }
                scs.AppendFormat("pooling={0:s}", this.oconexionpublica.Pooling.ToString());
            }

            ocnx = new SqlCeConnection();
            ocnx.ConnectionString = scs.ToString();
            ocnx.Open();
            bit_conectado = true;
            return true;
        }
        /// <summary>
        /// Remueve la conexión física a la base de Datos
        /// </summary>
        /// <returns>si se desconectó satisfactoriamente o no</returns>
        public bool desconecta()
        {
            if (ocnx != null)
            {
                ocnx.Close();
                ocnx.Dispose();
                ocnx = null;
                bit_conectado = false;
            }
            return true;
        }


        /// <summary>
        /// ejecuta un query
        /// </summary>
        /// <param name="query">Cadena SQL a ejecutar</param>
        /// <param name="QueryTimeOut">timeout para la ejecución del query</param>
        /// <returns>un datareader ó un dataset, posiblemente vacío</returns>
        public DataSet execquery(string query, int QueryTimeOut)
        {
            bool bit_tempconect = false;
            if (bit_conectado == false)
            {
                this.conecta();
                bit_tempconect = true;
            }
            SqlCeDataAdapter oda = new SqlCeDataAdapter(query, ocnx);
            oda.SelectCommand.CommandTimeout = 0;// QueryTimeOut;
            if (bit_entransaccion == true)
            {
                oda.SelectCommand.Transaction = otran;
            }

            DataSet ods = new DataSet();
            oda.Fill(ods);

            if (bit_tempconect == true)
            {
                this.desconecta();
                bit_tempconect = false;
            }
            return ods;

        }
        /// <summary>
        /// Inicia una transacción en la conexion de bd de SQL
        /// </summary>
        public void BeginTransaction(IsolationLevel nivelBloqueo)
        {
            otran = ocnx.BeginTransaction(nivelBloqueo);
            bit_entransaccion = true;
        }
        /// <summary>
        /// Termina una transacción en la conexion de bd  de SQL
        /// guardando los cambios realizados
        /// </summary>
        public void CommitTransaction()
        {
            otran.Commit();
            bit_entransaccion = false;
        }
        /// <summary>
        /// Termina una transacción en la conexion de bd  de SQL
        /// descartando los cambios realizados
        /// </summary>
        public void RollBackTransaction()
        {
            otran.Rollback();
            bit_entransaccion = false;
        }
        /// <summary>
        /// Ejecuta un sp especificado.
        /// </summary>
        /// <param name="str_nomSP">Nombre del sp a ejecutar</param>
        /// <returns>DataTable con el resultado de la ejecución del SP</returns>
        public DataTable ExecDT(String str_nomSP)
        {
            DataTable otbl = new DataTable();
            SqlCeCommand ocomand = new SqlCeCommand();
            bool bit_tempconect = false;
            ocomand.CommandText = str_nomSP;
            for(int int_params = 0 ; int_params < this.parametrosSQL.parametros.Count ; int_params ++)
            {
                SqlCeParameter opar = ocomand.Parameters.Add(this.parametrosSQL.parametros[int_params].ParameterName, this.parametrosSQL.parametros[int_params].SqlDbType);
                opar.Value = this.parametrosSQL.parametros[int_params].Value;
            }
            if (bit_conectado == false)
            {
                this.conecta();
                bit_tempconect = true;
            }
            ocomand.Connection = this.ocnx;
            ocomand.CommandType = CommandType.StoredProcedure;
            ocomand.CommandTimeout = this.QueryTimeOut;
            SqlCeDataAdapter oadap = new SqlCeDataAdapter(ocomand);
            oadap.Fill(otbl);
            if (bit_tempconect == true)
            {
                this.desconecta();
                bit_tempconect = false;
            }
            return otbl;
        }
        /// <summary>
        /// Ejecuta un sp especificado.
        /// </summary>
        /// <param name="str_nomSP">Nombre del sp a ejecutar</param>
        /// <returns>DataSet con el resultado de la ejecución del SP</returns>
        public DataSet ExecDS(String str_nomSP)
        {
            DataSet ods = new DataSet();
            SqlCeCommand ocomand = new SqlCeCommand();
            ocomand.CommandText = str_nomSP;
            bool bit_tempconect = false;
            for (int int_params = 0; int_params < this.parametrosSQL.parametros.Count; int_params++)
            {
                SqlCeParameter opar = ocomand.Parameters.Add(this.parametrosSQL.parametros[int_params].ParameterName, this.parametrosSQL.parametros[int_params].SqlDbType);
                opar.Value = this.parametrosSQL.parametros[int_params].Value;
            }
            if (bit_conectado == false)
            {
                this.conecta();
                bit_tempconect = true;
            }
            ocomand.Connection = this.ocnx;
            ocomand.CommandType = CommandType.StoredProcedure;
            ocomand.CommandTimeout = this.QueryTimeOut;
            SqlCeDataAdapter oadap = new SqlCeDataAdapter(ocomand);
            oadap.Fill(ods);
            if (bit_tempconect == true)
            {
                this.desconecta();
                bit_tempconect = false;
            }
            return ods;
        }
    }

    #region "   clase de parámetros    "
        /// <summary>
        /// clase que maneja los parametros de ejecución de un SP de SQL Server
        /// </summary>
        public class clsParamsSQL
        {

            SqlCeParameterCollection oparams;
            SqlCeParameter oparam;
            /// <summary>
            /// Colección de parametros
            /// </summary>
            public SqlCeParameterCollection parametros { get { return oparams; } }
            /// <summary>
            /// Genera una nueva colección
            /// </summary>
            public clsParamsSQL()
            {
                SqlCeCommand ocmd = new SqlCeCommand();
                oparams = ocmd.Parameters;
            }
            /// <summary>
            /// agrega un valor nulo a la listaq de parametros
            /// </summary>
            /// <param name="nomParam">nombre del parametro</param>
            public void add(String nomParam)
            {
                oparam = new SqlCeParameter(nomParam, SqlDbType.VarChar);
                oparam.Value = DBNull.Value;
                oparams.Add(oparam);
            }
            /// <summary>
            /// Agrega un parámetro bool a la lista.
            /// </summary>
            /// <param name="nomParam">Nombre del parametro</param>
            /// <param name="value">Valor del parametro</param>
            public void add(String nomParam, bool value)
            {
                oparam = new SqlCeParameter(nomParam, SqlDbType.Bit);
                if (value == true)
                {
                    oparam.Value = 1;
                }
                else
                {
                    oparam.Value = 0;
                }
                oparams.Add(oparam);
            }
            /// <summary>
            /// Agrega un parámetro DateTime a la lista.
            /// </summary>
            /// <param name="nomParam">Nombre del parametro</param>
            /// <param name="value">Valor del parametro</param>
            public void add(String nomParam, DateTime value)
            {
                oparam = new SqlCeParameter(nomParam, SqlDbType.DateTime);
                oparam.Value = value;
                oparams.Add(oparam);
            }
            /// <summary>
            /// Agrega un parámetro decimal a la lista.
            /// </summary>
            /// <param name="nomParam">Nombre del parametro</param>
            /// <param name="value">Valor del parametro</param>
            public void add(String nomParam, decimal value)
            {
                oparam = new SqlCeParameter(nomParam, SqlDbType.Decimal);
                oparam.Value = value;
                oparams.Add(oparam);
            }
            /// <summary>
            /// Agrega un parámetro double a la lista.
            /// </summary>
            /// <param name="nomParam">Nombre del parametro</param>
            /// <param name="value">Valor del parametro</param>
            public void add(String nomParam, double value)
            {
                oparam = new SqlCeParameter(nomParam, SqlDbType.BigInt);
                oparam.Value = value;
                oparams.Add(oparam);
            }
            /// <summary>
            /// Agrega un parámetro float a la lista.
            /// </summary>
            /// <param name="nomParam">Nombre del parametro</param>
            /// <param name="value">Valor del parametro</param>
            public void add(String nomParam, float value)
            {
                oparam = new SqlCeParameter(nomParam, SqlDbType.Float);
                oparam.Value = value;
                oparams.Add(oparam);
            }
            /// <summary>
            /// Agrega un parámetro Int16 a la lista.
            /// </summary>
            /// <param name="nomParam">Nombre del parametro</param>
            /// <param name="value">Valor del parametro</param>
            public void add(String nomParam, Int16 value)
            {
                oparam = new SqlCeParameter(nomParam, SqlDbType.SmallInt );
                oparam.Value = value;
                oparams.Add(oparam);
            }
            /// <summary>
            /// Agrega un parámetro Int32 a la lista.
            /// </summary>
            /// <param name="nomParam">Nombre del parametro</param>
            /// <param name="value">Valor del parametro</param>
            public void add(String nomParam, Int32 value)
            {
                oparam = new SqlCeParameter(nomParam, SqlDbType.Int);
                oparam.Value = value;
                oparams.Add(oparam);
            }
            /// <summary>
            /// Agrega un parámetro String(Text) a la lista.
            /// </summary>
            /// <param name="nomParam">Nombre del parametro</param>
            /// <param name="value">Valor del parametro</param>
            public void add(String nomParam, String value)
            {
                oparam = new SqlCeParameter(nomParam, SqlDbType.Text);
                oparam.Value = value;
                oparams.Add(oparam);
            }
            /// <summary>
            /// Agrega un parámetro String(Varchar) a la lista.
            /// </summary>
            /// <param name="nomParam">Nombre del parametro</param>
            /// <param name="value">Valor del parametro</param>
            /// <param name="size">Tamaño del parametro</param>
            public void add(String nomParam, String value, int size)
            {
                oparam = new SqlCeParameter(nomParam, SqlDbType.VarChar ,size);
                oparam.Value = value;
                oparams.Add(oparam);
            }
            /// <summary>
            /// Limpia la lista de parametros
            /// </summary>
            public void Clear()
            {
                oparams.Clear();
            }

        }
    #endregion
}
