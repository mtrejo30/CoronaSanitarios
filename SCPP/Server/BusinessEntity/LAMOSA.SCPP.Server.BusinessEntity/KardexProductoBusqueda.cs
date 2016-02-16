using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;


namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "KardexProductoBusqueda", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class KardexProductoBusqueda : BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodPieza = -1;
        private int iCodPlanta = -1;
        private string sDesPlanta = String.Empty;
        private int iTipoArticulo = -1;
        private string sDesTipoArticulo = String.Empty;
        private int iCodArticulo = -1;
        private string sDesArticulo = String.Empty;
        private int iCodColor = -1;
        private string sColor = String.Empty;
        private int iCodCalidad = -1;
        private string sCalidad = String.Empty;
        
     

        #endregion

        #region Properties
        [DataMember(Name = "CodPieza")]
        public int CodPieza { get { return iCodPieza; } set { iCodPieza = value; } }
        [DataMember(Name = "CodPlanta")]
        public int CodPlanta { get { return iCodPlanta; } set { iCodPlanta = value; } }
        [DataMember(Name = "DesPlanta")]
        public string DesPlanta { get { return sDesPlanta; } set { sDesPlanta = value; } }
        [DataMember(Name = "TipoArticulo")]
        public int TipoArticulo { get { return iTipoArticulo; } set { iTipoArticulo = value; } }
        [DataMember(Name = "DesTipoArticulo")]
        public string DesTipoArticulo { get { return sDesTipoArticulo; } set { sDesTipoArticulo = value; } }
        [DataMember(Name = "CodArticulo")]
        public int CodArticulo { get { return iCodArticulo; } set { iCodArticulo = value; } }
        [DataMember(Name = "DesArticulo")]
        public string DesArticulo { get { return sDesArticulo; } set { sDesArticulo = value; } }
        [DataMember(Name = "CodColor")]
        public int CodColor { get { return iCodColor; } set { iCodColor = value; } }
        [DataMember(Name = "Color")]
        public string Color { get { return sColor; } set { sColor = value; } }
        [DataMember(Name = "CodCalidad")]
        public int CodCalidad { get { return iCodCalidad; } set { iCodCalidad = value; } }
        [DataMember(Name = "Calidad")]
        public string Calidad { get { return sCalidad; } set { sCalidad = value; } }



        #endregion

        #region Methods
        

      public KardexProductoBusqueda(int iCodPieza,
                int iCodPlanta,
                string sDesPlanta,
                int iTipoArticulo,
                string sDesTipoArticulo,
                int iCodArticulo,
                string sDesArticulo,
                int iCodColor,
                string sColor,
                int iCodCalidad,
                string sCalidad

        )
     {
            this.iCodPieza = iCodPieza;
            this.iCodPlanta = iCodPlanta;
            this.sDesPlanta = sDesPlanta;
            this.iTipoArticulo = iTipoArticulo;
            this.sDesTipoArticulo = sDesTipoArticulo;
            this.iCodArticulo = iCodArticulo;
            this.sDesArticulo = sDesArticulo;
            this.iCodColor = iCodColor;
            this.sColor = sColor;
            this.iCodCalidad = iCodCalidad;
            this.sCalidad = sCalidad;
        }

       public KardexProductoBusqueda()
        { }

        public KardexProductoBusqueda(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~KardexProductoBusqueda()
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
            return GetPropertyNamesArray(new KardexProductoBusqueda());
        }

        #endregion
    }
}