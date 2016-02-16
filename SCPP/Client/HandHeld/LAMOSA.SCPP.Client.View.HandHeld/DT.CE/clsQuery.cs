using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DT.CE
{
    /// <summary>
    /// clase que ejecuta una sentencia / sentencias de un manejador de BD
    /// </summary>
    public class clsQuery
    {
        /// <summary>
        /// conexión a la BD
        /// </summary>
        clsConexion ocnx = null;
        IsolationLevel nivelbloqueo = IsolationLevel.ReadCommitted;
        int nQueryTimeOut = 120;

        /// <summary>
        /// time out para la ejecución de un query
        /// </summary>
        public int QueryTimeOut { get { return nQueryTimeOut; } set { nQueryTimeOut = value; } }
        /// <summary>
        /// Modo de Transacción
        /// </summary>
        public IsolationLevel TipoBloqueo { get { return nivelbloqueo; } set { nivelbloqueo = value; } }

        /// <summary>
        /// Crea un objeto de clase Query
        /// </summary>
        /// <param name="conexion">Conexión ya establecida a la BD</param>
        public clsQuery(clsConexion oconexion)
        {
            this.ocnx = oconexion;
        }

        /// <summary>
        /// Ejecuta el query indicado en la base de datos
        /// </summary>
        /// <param name="query">query a jecutarr</param>
        /// <returns>Un dataset con el resultado de la ejecución.</returns>
        public DataSet exec(string str_query)
        {
            DataSet ods = null;
            if (ocnx.conexion == null)
            {
                ocnx.conecta();
                ods = ocnx.conexion.execquery(str_query, nQueryTimeOut);
                ocnx.desconecta();
            }
            else
            {
                ods = ocnx.conexion.execquery(str_query, nQueryTimeOut);
            }
            return ods;
        }

        #region "   transacciones  "
            /// <summary>
            /// Inicia una transacción en la conexion de bd
            /// </summary>
            public void BeginTransaction()
            {
                ocnx.conexion.BeginTransaction(TipoBloqueo);
            }
            /// <summary>
            /// Termina una transacción en la conexion de bd 
            /// guardando los cambios realizados
            /// </summary>
            public void CommitTransaction()
            {
                ocnx.conexion.CommitTransaction();
            }
            /// <summary>
            /// Termina una transacción en la conexion de bd 
            /// descartando los cambios realizados
            /// </summary>
            public void RollBackTransaction()
            {
                ocnx.conexion.RollBackTransaction();
            }

        #endregion
    }
}
