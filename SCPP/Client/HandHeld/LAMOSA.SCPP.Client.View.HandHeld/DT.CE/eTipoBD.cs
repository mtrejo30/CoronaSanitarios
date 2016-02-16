using System;
using System.Collections.Generic;
using System.Text;

namespace DT.CE
{
    /// <summary>
    /// tipos de conexiones disponibles
    /// a la fecha a base de datos.
    /// Cada tipo mapea en tiempo a una
    /// dll de acceso a datos secundaria
    /// especializada
    /// </summary>
    public enum edbTipo
    {
        /// <summary>
        /// No se ha definido aún una conexión
        /// </summary>
        dbNinguna = 0,
        /// <summary>
        /// base de datos SQL
        /// Nativa de .NET
        /// </summary>
        dbSQLServer = 1,
        /// <summary>
        /// bd OLEDB
        /// Nativa de .NET
        /// </summary>
        dbOLEDB = 2,
        /// <summary>
        /// base de datos Oracle
        /// requiera la instalación de los
        /// providers de Oracle
        /// </summary>
        dbOracle = 3,
        /// <summary>
        /// bd ODBC
        /// Acceso genérico a bases sin 
        /// soporte directo de .NET
        /// Requiere el cliente ODBC de .NET
        /// </summary>
        dbODBC = 4
    }
}
