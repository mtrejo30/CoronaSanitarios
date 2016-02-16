using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Molde", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Molde : BaseSolutionEntity
    {

        #region Fields

        private int iCodMolde = -1;
        private string sClaveMolde = String.Empty;
        private string sDesMolde = String.Empty;
        private int iNumImpresiones = -1;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private DateTime dtFechaBaja = DateTime.MinValue;
        private bool bActivo = false;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodMolde")]
        public int CodMolde { get { return this.iCodMolde; } set { this.iCodMolde = value; } }
        [DataMember(Name = "ClaveMolde")]
        public string ClaveMolde { get { return this.sClaveMolde; } set { this.sClaveMolde = value; } }
        [DataMember(Name = "DesMolde")]
        public string DesMolde { get { return this.sDesMolde; } set { this.sDesMolde = value; } }
        [DataMember(Name = "NumImpresiones")]
        public int NumImpresiones { get { return this.iNumImpresiones; } set { this.iNumImpresiones = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return this.dtFechaRegistro; } set { this.dtFechaRegistro = value; } }
        [DataMember(Name = "FechaBaja")]
        public DateTime FechaBaja { get { return this.dtFechaBaja; } set { this.dtFechaBaja = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return this.bActivo; } set { this.bActivo = value; } }

        #endregion Properties

        #region Methods

        public Molde()
        {

        }
        public Molde(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
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
            return GetPropertyNamesArray(new Molde());
        }
        public Molde
        (
            int iCodMolde,
            string sClaveMolde,
            string sDesMolde,
            int iNumImpresiones,
            DateTime dtFechaRegistro,
            DateTime dtFechaBaja,
            bool bActivo
        )
        {
            this.iCodMolde = iCodMolde;
            this.sClaveMolde = sClaveMolde;
            this.sDesMolde = sDesMolde;
            this.iNumImpresiones = iNumImpresiones;
            this.dtFechaRegistro = dtFechaRegistro;
            this.dtFechaBaja = dtFechaBaja;
            this.bActivo = bActivo;
        }
        ~Molde()
        {

        }

        #endregion Methods

    }
}

