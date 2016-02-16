using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "RepCapInstalada", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class RepCapInstalada : BaseSolutionEntity
    {

        #region Fields

        private string sTipoArticulo = String.Empty;
        private string sBanco = String.Empty;
        private string sModelo = String.Empty;
        private string sDescripcion = String.Empty;
        private int iCantidadMoldes = -1;
        private int iNumImpresiones = -1;
        private int iVaciadasXDia = -1;
        private int iPiezasVaciadasDia = -1;
        private int iPiezasVaciadasAcumuladas = -1;
        private int iSeis = -1;

        #endregion Fields

        #region Properties

        [DataMember(Name = "TipoArticulo")]
        public string TipoArticulo { get { return this.sTipoArticulo; } set { this.sTipoArticulo = value; } }
        [DataMember(Name = "Banco")]
        public string Banco { get { return this.sBanco; } set { this.sBanco = value; } }
        [DataMember(Name = "Modelo")]
        public string Modelo { get { return this.sModelo; } set { this.sModelo = value; } }
        [DataMember(Name = "Descripcion")]
        public string Descripcion { get { return this.sDescripcion; } set { this.sDescripcion = value; } }
        [DataMember(Name = "CantidadMoldes")]
        public int CantidadMoldes { get { return this.iCantidadMoldes; } set { this.iCantidadMoldes = value; } }
        [DataMember(Name = "NumImpresiones")]
        public int NumImpresiones { get { return this.iNumImpresiones; } set { this.iNumImpresiones = value; } }
        [DataMember(Name = "VaciadasXDia")]
        public int VaciadasXDia { get { return this.iVaciadasXDia; } set { this.iVaciadasXDia = value; } }
        [DataMember(Name = "PiezasVaciadasDia")]
        public int PiezasVaciadasDia { get { return this.iPiezasVaciadasDia; } set { this.iPiezasVaciadasDia = value; } }
        [DataMember(Name = "PiezasVaciadasAcumuladas")]
        public int PiezasVaciadasAcumuladas { get { return this.iPiezasVaciadasAcumuladas; } set { this.iPiezasVaciadasAcumuladas = value; } }
        [DataMember(Name = "Seis")]
        public int Seis { get { return this.iSeis; } set { this.iSeis = value; } }

        #endregion Properties

        #region Methods

        public RepCapInstalada()
        {

        }
        public RepCapInstalada(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }

        /// <summary>
        /// Obtiene un arreglo de objetos con los valores de las propiedades
        /// </summary>
        /// <returns>objetct[]</returns>
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
            return GetPropertyNamesArray(new RepCapInstalada());
        }

        public RepCapInstalada
        (
            string sTipoArticulo,
            string sBanco,
            string sModelo,
            string sDescripcion,
            int iCantidadMoldes,
            int iNumImpresiones,
            int iVaciadasXDia,
            int iPiezasVaciadasDia,
            int iPiezasVaciadasAcumuladas,
            int iSeis
        )
        {
            this.sTipoArticulo = sTipoArticulo;
            this.sBanco = sBanco;
            this.sModelo = sModelo;
            this.sDescripcion = sDescripcion;
            this.iCantidadMoldes = iCantidadMoldes;
            this.iNumImpresiones = iNumImpresiones;
            this.iVaciadasXDia = iVaciadasXDia;
            this.iPiezasVaciadasDia = iPiezasVaciadasDia;
            this.iPiezasVaciadasAcumuladas = iPiezasVaciadasAcumuladas;
            this.iSeis = iSeis;
        }
        ~RepCapInstalada()
        {

        }

        #endregion Methods

    }
}
