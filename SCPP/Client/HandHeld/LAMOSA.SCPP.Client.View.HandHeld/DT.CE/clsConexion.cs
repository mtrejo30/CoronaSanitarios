using System;
using System.Collections.Generic;
using System.Text;

namespace DT.CE
{
    public class clsConexion
    {

        bool bit_TrustedCon = false;
        bool bit_Conectado = false;
        bool bit_pooling = false;

        string str_servidor = "";
        string str_bd = "";
        string str_usuario = "";
        string str_password = "";

        string str_connectionstring = "";

        edbTipo eTipo = edbTipo.dbNinguna;

        Idb ocnx_fisica = null;


        /// <summary>
        /// indica si se encuentra conectado al servidor
        /// </summary>
        public bool conectado { get { return bit_Conectado; } set { bit_Conectado = value; } }
        /// <summary>
        /// indica el modo de conexión segura a través de un usuario autenticado
        /// </summary>
        public bool TrustedConnection { get { return bit_TrustedCon; } set { bit_TrustedCon = value; } }
        /// <summary>
        /// Nombre del servidor de BD
        /// </summary>
        public String servidor { get { return str_servidor; } set { str_servidor = value; } }
        /// <summary>
        /// Nombre de la base de datos del servidor
        /// </summary>
        public String bd { get { return str_bd; } set { str_bd = value; } }
        /// <summary>
        /// Nombre del usuario de acceso a la base de datos (usuario Registrado en el Motor de la Base de Datos)
        /// </summary>
        public String usuario { get { return str_usuario; } set { str_usuario = value; } }
        /// <summary>
        /// Contraseña del usuario de acceso a la base de datos (usuario Registrado en el Motor de la Base de Datos)
        /// </summary>
        public String password { get { return str_password; } set { str_password = value; } }
        /// <summary>
        /// Cadena de conexion a la base de datos
        /// </summary>
        public String ConnectionString { get { return str_connectionstring; } set { str_connectionstring = value; } }
        /// <summary>
        /// Establece si la conexión tendrá un pool de conexion o no.
        /// </summary>
        public bool Pooling { get { return bit_pooling; } set { bit_pooling = value; } }
        /// <summary>
        /// Devuelve el tipo de base de datos al que esta conectado
        /// </summary>
        public edbTipo TipoBD { get { return eTipo; } set { eTipo = value; } }

        public Idb conexion { get { return ocnx_fisica; } }

        /// <summary>
        /// crea una conexion vacía
        /// </summary>
        public clsConexion()
        {

        }

        /// <summary>
        /// crea una conexión en base a un connectionstring
        /// </summary>
        /// <param name="connectionstring">cadena de conexión</param>
        /// <param name="tipo">tipo de conexión a crear</param>
        public clsConexion(string connectionstring, edbTipo tipo)
        {
            this.ConnectionString = connectionstring;
            this.eTipo = tipo;
            switch (this.eTipo)
            {
                case edbTipo.dbSQLServer:
                    {
                        ocnx_fisica = new clsdbSQLServer(this);
                        break;
                    }
            }
        }

        /// <summary>
        /// crea una conexión del tipo especificado.
        /// Adicionalmente trata de establecer la conexión 
        /// a la bd con los parámetros especificados mediante una conexion segura.
        /// </summary>
        /// <param name="servidor">nombre, IP ó instancia del servidor</param>
        /// <param name="bd">identificador de la BD</param>
        /// <param name="tipo">Tipo de la conexión</param>
        public clsConexion(string servidor, string bd, edbTipo tipo)
        {
            this.servidor = servidor.Replace(":", ",");
            this.bd = bd;
            this.TrustedConnection = true;
            this.eTipo = tipo;
            switch (this.eTipo)
            {
                case edbTipo.dbSQLServer:
                    {
                        ocnx_fisica = new clsdbSQLServer(this);
                        break;
                    }
            }
        }


        /// <summary>
        /// crea una conexión del tipo especificado.
        /// Adicionalmente trata de establecer la conexión 
        /// a la bd con los parámetros especificados
        /// </summary>
        /// <param name="servidor">nombre, IP ó instancia del servidor</param>
        /// <param name="bd">identificador de la BD</param>
        /// <param name="usuario">usuario</param>
        /// <param name="password">password</param>
        /// <param name="tipo">Tipo de la conexión</param>
        public clsConexion(string servidor, string bd, string usuario, string password, edbTipo tipo)
        {
            this.servidor = servidor.Replace(":", ",");
            this.bd = bd;
            this.str_password = password;
            this.usuario = usuario;
            this.eTipo = tipo;
            switch (this.eTipo)
            {
                case edbTipo.dbSQLServer:
                    {
                        ocnx_fisica = new clsdbSQLServer(this);
                        break;
                    }
            }
        }
        /// <summary>
        /// Establece una conexión física a la base de Datos
        /// </summary>
        /// <returns>si se conectó satisfactoriamente o no</returns>
        public bool conecta()
        {
            switch (this.eTipo)
            {
                case edbTipo.dbSQLServer:
                    {
                        return ocnx_fisica.conecta();
                    }
            }
            return false;
        }
        /// <summary>
        /// Remueve la conexión física a la base de Datos
        /// </summary>
        /// <returns>si se desconectó satisfactoriamente o no</returns>
        public bool desconecta()
        {
            if (null != ocnx_fisica)
            {
                return ocnx_fisica.desconecta();
            }
            return true;
        }
    }
}
