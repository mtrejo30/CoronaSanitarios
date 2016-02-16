using System;
using System.Collections.Generic;
using System.Text;

namespace DT.CE
{
    public class clsParams
    {
        clsConexion oCnx;

        public clsParams(clsConexion oconn)
        {
            oCnx = oconn;
        }
        /// <summary>
        /// Agrega un valor nulo a la lista de parametros
        /// </summary>
        /// <param name="nomParam">Nombre del Parametro</param>
        public void add(String nomParam)
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).add(nomParam);
                        break;
                    }
            }
        }
        /// <summary>
        /// Agrega un parámetro bool a la lista.
        /// </summary>
        /// <param name="nomParam">Nombre del Parametro</param>
        /// <param name="parametro">valor del parámetro (se envía como 1 ó 0)</param>
        public void add(String nomParam, bool parametro)
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).add(nomParam, parametro);
                        break;
                    }
            }
        }
        /// <summary>
        /// Agrega un parámetro DateTime a la lista.
        /// </summary>
        /// <param name="nomParam">Nombre del Parametro</param>
        /// <param name="parametro">valor del parámetro </param>
        public void add(String nomParam, DateTime parametro)
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).add(nomParam, parametro);
                        break;
                    }
            }
        }
        /// <summary>
        /// Agrega un parámetro decimal a la lista.
        /// </summary>
        /// <param name="nomParam">Nombre del Parametro</param>
        /// <param name="parametro">valor del parámetro </param>
        public void add(String nomParam, decimal parametro)
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).add(nomParam, parametro);
                        break;
                    }
            }
        }
        /// <summary>
        /// Agrega un parámetro double a la lista.
        /// </summary>
        /// <param name="nomParam">Nombre del Parametro</param>
        /// <param name="parametro">valor del parámetro </param>
        public void add(String nomParam, double parametro)
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).add(nomParam, parametro);
                        break;
                    }
            }
        }
        /// <summary>
        /// Agrega un parámetro float a la lista.
        /// </summary>
        /// <param name="nomParam">Nombre del Parametro</param>
        /// <param name="parametro">valor del parámetro </param>
        public void add(String nomParam, float parametro)
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).add(nomParam, parametro);
                        break;
                    }
            }
        }
        /// <summary>
        /// Agrega un parámetro Int16 a la lista.
        /// </summary>
        /// <param name="nomParam">Nombre del Parametro</param>
        /// <param name="parametro">valor del parámetro </param>
        public void add(String nomParam, Int16 parametro)
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).add(nomParam, parametro);
                        break;
                    }
            }
        }
        /// <summary>
        /// Agrega un parámetro Int32 a la lista.
        /// </summary>
        /// <param name="nomParam">Nombre del Parametro</param>
        /// <param name="parametro">valor del parámetro </param>
        public void add(String nomParam, Int32 parametro)
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).add(nomParam, parametro);
                        break;
                    }
            }
        }
        /// <summary>
        /// Agrega un parámetro String(Text) a la lista.
        /// </summary>
        /// <param name="nomParam">Nombre del Parametro</param>
        /// <param name="parametro">valor del parámetro </param>
        public void add(String nomParam, String parametro)
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).add(nomParam, parametro);
                        break;
                    }
            }
        }
        /// <summary>
        /// Agrega un parámetro String(Varchar) a la lista.
        /// </summary>
        /// <param name="nomParam">Nombre del Parametro</param>
        /// <param name="parametro">valor del parámetro</param>
        /// <param size="parametro">Tamaño del parametro</param>
        public void add(String nomParam, String parametro, int size)
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).add(nomParam, parametro, size);
                        break;
                    }
            }
        }
        /// <summary>
        /// Limpia la lista de parametros
        /// </summary>
        public void Clear()
        {
            switch (oCnx.TipoBD)
            {
                case edbTipo.dbSQLServer:
                    {
                        ((clsParamsSQL)(oCnx.conexion.parameters)).Clear();
                        break;
                    }
            }
        }
    }
}
