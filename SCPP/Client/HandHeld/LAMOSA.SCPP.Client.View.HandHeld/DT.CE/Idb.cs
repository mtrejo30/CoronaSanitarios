using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DT.CE
{
    public interface Idb 
    {

        int QueryTimeOut { get;set;}

        /// <summary>
        /// establecer la conexión 
        /// a la bd con los parámetros especificados
        /// </summary>
        /// <returns></returns>
        bool conecta();
        /// <summary>
        /// remueve la conexión hacia la bd
        /// </summary>
        /// <returns></returns>
        bool desconecta();

        /// <summary>
        /// ejecuta un query
        /// </summary>
        /// <param name="query">Cadena SQL a ejecutar</param>
        /// <param name="devolver">devolver un dataset o un datareader o un objeto</param>
        /// <param name="QueryTimeOut">timeout para la ejecución del query</param>
        /// <returns>un datareader ó un dataset, posiblemente vacío</returns>
        DataSet execquery(string query, int QueryTimeOut);

        /// <summary>
        /// Inicia una transacción en las conexiones de los manejadores de bd
        /// </summary>
        void BeginTransaction(IsolationLevel nivelBloqueo);

        /// <summary>
        /// Termina una transacción en las conexiones de los manejadores de bd 
        /// guardando los cambios realizados
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Termina una transacción en las conexiones de los manejadores de bd 
        /// descartando los cambios realizados
        /// </summary>
        void RollBackTransaction();
        /// <summary>
        /// Devuelve la lista de parámetros a ejecutar de un SP
        /// </summary>
        object parameters { get; }
        /// <summary>
        /// Ejecuta un sp especificado.
        /// </summary>
        /// <param name="str_nomSP">Nombre del sp a ejecutar</param>
        /// <returns>DataTable con el resultado de la ejecución del SP</returns>
        DataTable ExecDT(String str_nomSP);
        /// <summary>
        /// Ejecuta un sp especificado.
        /// </summary>
        /// <param name="str_nomSP">Nombre del sp a ejecutar</param>
        /// <returns>DataSet con el resultado de la ejecución del SP</returns>
        DataSet ExecDS(String str_nomSP);
    }
}
