using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DT.CE
{
    /// <summary>
    /// Encapsula la funcionalidad de un SP
    /// </summary>
    public class clsStoredProcedure
    {
        clsConexion ocnx = null;
        clsParams oParams;
        IsolationLevel nivelbloqueo = IsolationLevel.ReadCommitted;
        int nQueryTimeOut = 120;

        /// <summary>
        /// time out para la ejecución de un sp
        /// </summary>
        public int QueryTimeOut { get { return nQueryTimeOut; } set { nQueryTimeOut = value; } }

        /// <summary>
        /// Modo de Transacción
        /// </summary>
        public IsolationLevel TipoBloqueo { get { return nivelbloqueo; } set { nivelbloqueo = value; } }
        /// <summary>
        /// Colección de parámetros. Esta lista se le pasará en
        /// el orden en que fueron agregados al SP, o bien por el nombre del parametro.
        /// Se realiza la conversión automática de tipos de 
        /// datos a los soportados por la base de datos.
        /// </summary>
        public clsParams parametros { get { return oParams; } }



        /// <summary>
        /// Crea un objeto de clase StoredProcedure
        /// </summary>
        /// <param name="conexion">Conexión ya establecida a la BD</param>
        public clsStoredProcedure(clsConexion oconexion)
        {
            this.ocnx = oconexion;
            oParams = new clsParams(oconexion);
        }
        /// <summary>
        /// Devuelve el SP a la condición inicial:
        /// Sin parámetros
        /// </summary>
        public void reset()
        {
            oParams.Clear();
        }

         /// <summary>
        /// Ejecuta un sp especificado.
        /// </summary>
        /// <param name="nomSP">Nombre del sp a ejecutar</param>
        /// <returns>DataTable con el resultado de la ejecución del SP</returns>
        public DataTable execDT(String nomSP)
        {
            DataTable otbl;
            ocnx.conexion.QueryTimeOut = this.QueryTimeOut;
            if (ocnx.conexion == null)
            {
                ocnx.conecta();
                otbl = ocnx.conexion.ExecDT(nomSP);
                ocnx.desconecta();
            }
            else
            {
                otbl = ocnx.conexion.ExecDT(nomSP);
            }
            return otbl;

        }
        /// <summary>
        /// Ejecuta un sp especificado.
        /// </summary>
        /// <param name="nomSP">Nombre del sp a ejecutar</param>
        /// <returns>DataSet con el resultado de la ejecución del SP</returns>
        public DataSet  execDS(String nomSP)
        {
            DataSet ods;
            ocnx.conexion.QueryTimeOut = this.QueryTimeOut;
            if (ocnx.conexion == null)
            {
                ocnx.conecta();
                ods = ocnx.conexion.ExecDS(nomSP);
                ocnx.desconecta();
            }
            else
            {
                ods = ocnx.conexion.ExecDS(nomSP);
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
