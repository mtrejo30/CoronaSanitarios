using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Articulo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Articulo : BaseSolutionEntity
    {

        #region Fields

        private int iCodArticulo = -1;
        private string sClaveArticulo = String.Empty;
        private string sDesArticulo = String.Empty;
        private int iCodTipoArticulo = -1;
        private string sDesTipoArticulo = String.Empty;
        private int iCodMolde = -1;
        private string sDesMolde = String.Empty;
        private int iCodGrupoArticulo = -1;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private DateTime dtFechaBaja = DateTime.MinValue;
        private bool bActivo = false;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodArticulo")]
        public int CodArticulo { get { return this.iCodArticulo; } set { this.iCodArticulo = value; } }
        [DataMember(Name = "ClaveArticulo")]
        public string ClaveArticulo { get { return this.sClaveArticulo; } set { this.sClaveArticulo = value; } }
        [DataMember(Name = "DesArticulo")]
        public string DesArticulo { get { return this.sDesArticulo; } set { this.sDesArticulo = value; } }
        [DataMember(Name = "CodTipoArticulo")]
        public int CodTipoArticulo { get { return this.iCodTipoArticulo; } set { this.iCodTipoArticulo = value; } }
        [DataMember(Name = "DesTipoArticulo")]
        public string DesTipoArticulo { get { return this.sDesTipoArticulo; } set { this.sDesTipoArticulo = value; } }
        [DataMember(Name = "CodMolde")]
        public int CodMolde { get { return this.iCodMolde; } set { this.iCodMolde = value; } }
        [DataMember(Name = "DesMolde")]
        public string DesMolde { get { return this.sDesMolde; } set { this.sDesMolde = value; } }
        [DataMember(Name = "CodGrupoArticulo")]
        public int CodGrupoArticulo { get { return this.iCodGrupoArticulo; } set { this.iCodGrupoArticulo = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return this.dtFechaRegistro; } set { this.dtFechaRegistro = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return this.dtFechaBaja; } set { this.dtFechaBaja = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return this.bActivo; } set { this.bActivo = value; } }

        #endregion Properties

        #region Methods

        public Articulo()
        {

        }
        public Articulo(DataRow row)
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
            return GetPropertyNamesArray(new Articulo());
        }
       
        public Articulo
        (
            int iCodArticulo,
            string sClaveArticulo,
            string sDesArticulo,
            int iCodTipoArticulo,
            string sDesTipoArticulo,
            int iCodMolde,
            string sDesMolde,
            int iCodGrupoArticulo,
            DateTime dtFechaRegistro,
            DateTime dtFechaBaja,
            bool bActivo
        )
        {
            this.iCodArticulo = iCodArticulo;
            this.sClaveArticulo = sClaveArticulo;
            this.sDesArticulo = sDesArticulo;
            this.iCodTipoArticulo = iCodTipoArticulo;
            this.sDesTipoArticulo = sDesTipoArticulo;
            this.iCodMolde = iCodMolde;
            this.sDesMolde = sDesMolde;
            this.iCodGrupoArticulo = iCodGrupoArticulo;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaBaja = dtFechaBaja;
            this.bActivo = bActivo;
        }
        ~Articulo()
        {

        }

        #endregion Methods

    }
}