using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Rol", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Rol:BaseSolutionEntity
    {
        #region PrivateFields
        private int iClaveRol = -1;
        private string sDescripcionRol = String.Empty;
        private bool bActivo = false;

        #endregion

        #region Properties
        [DataMember(Name = "ClaveRol")]
        public int ClaveRol { get { return this.iClaveRol; } set { this.iClaveRol = value; } }
        [DataMember(Name = "DescripcionRol")]
        public string DescripcionRol { get { return this.sDescripcionRol; } set { this.sDescripcionRol = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return this.bActivo; } set { this.bActivo = value; } }

        #endregion

        #region Methods
        public Rol(int iClaveRol,
                string sDescripcionRol,
                 bool bActivo
        )
        {
            this.iClaveRol = iClaveRol;
            this.sDescripcionRol = sDescripcionRol;
            this.bActivo = bActivo;
        }
        public Rol()
        { }
        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        /// <summary>
        /// Obtiene un arreglo con los nombres solamente de las propiedades
        /// </summary>
        /// <returns></returns>
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new Rol());
        }

        public Rol(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~Rol()
        { }

        #endregion
    }
}

