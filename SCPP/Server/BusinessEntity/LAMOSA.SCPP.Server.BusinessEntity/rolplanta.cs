using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;




namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "rolplanta", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class rolplanta:BaseSolutionEntity
    {
        #region PrivateFields
        private int iClaveRol = -1;
        private string sDescripcionRol = String.Empty;
        private int iCodPlanta = -1;
        private bool bActivo = false;

        #endregion

        #region Properties
        [DataMember(Name = "ClaveRol")]
        public int ClaveRol { get { return this.iClaveRol; } set { this.iClaveRol = value; } }
        [DataMember(Name = "DescripcionRol")]
        public string DescripcionRol { get { return this.sDescripcionRol; } set { this.sDescripcionRol = value; } }
        [DataMember(Name = "CodPlanta")]
        public int CodPlanta { get { return this.iCodPlanta; } set { this.iCodPlanta = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return this.bActivo; } set { this.bActivo = value; } }

        #endregion

        #region Methods
        public rolplanta(int iClaveRol,
                string sDescripcionRol, int iCodPlanta,
                 bool bActivo
        )
        {
            this.iClaveRol = iClaveRol;
            this.sDescripcionRol = sDescripcionRol;
            this.iCodPlanta = iCodPlanta;
            this.bActivo = bActivo;
        }
        public rolplanta()
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

        public rolplanta(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~rolplanta()
        { }

        #endregion
    }
}

