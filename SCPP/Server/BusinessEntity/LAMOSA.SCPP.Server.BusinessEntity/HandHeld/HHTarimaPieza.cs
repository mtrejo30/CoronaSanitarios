using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHTarimaPieza", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHTarimaPieza : BaseSolutionEntity
    {

        #region Fields

        private int iCodPieza = -1;
        private bool bAuditada = false;
        private bool bPaletizado = false;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodPieza")]
        public int CodPieza { get { return this.iCodPieza; } set { this.iCodPieza = value; } }
        [DataMember(Name = "Auditada")]
        public bool Auditada { get { return this.bAuditada; } set { this.bAuditada = value; } }
        [DataMember(Name = "Paletizado")]
        public bool Paletizado { get { return this.bPaletizado; } set { this.bPaletizado = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHTarimaPieza()
        {

        }
        public HHTarimaPieza(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHTarimaPieza
        (
            int iCodPieza,
            bool bAuditada,
            bool bPaletizado
        )
        {
            this.iCodPieza = iCodPieza;
            this.bAuditada = bAuditada;
            this.bPaletizado = bPaletizado;
        }
        ~HHTarimaPieza()
        {

        }

        #endregion Constructors and Destructor

        #region Common

        #region GetPropertyNamesArray
        /// <summary>
        /// Obtiene un arreglo con los nombres solamente de las propiedades
        /// </summary>
        /// <returns>string[]</returns>
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new HHProceso());
        }
        #endregion GetPropertyNamesArray
        #region GetPropertyValuesArray
        /// <summary>
        /// Obtiene un arreglo de objetos con los valores de las propiedades
        /// </summary>
        /// <returns>objetct[]</returns>
        public object[] ToObjectArray()
        {
            return GetPropertyValuesArray(this);
        }
        #endregion GetPropertyValuesArray

        #endregion Common

        #endregion Methods

    }
}
