using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHValidarPieza", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHValidarPieza : BaseSolutionEntity
    {

        #region Fields

        private bool bValProcesoExitosa = false;
        private bool bValNoDefDespExitosa = false;
        private string sMensajeValidacion = string.Empty;
        private int iCodPieza = -1;
        private int iCodProceso = -1;

        #endregion Fields

        #region Properties

        [DataMember(Name = "VPE")]
        public bool ValProcesoExitosa { get { return this.bValProcesoExitosa; } set { this.bValProcesoExitosa = value; } }
        [DataMember(Name = "VNDDE")]
        public bool ValNoDefDespExitosa { get { return this.bValNoDefDespExitosa; } set { this.bValNoDefDespExitosa = value; } }
        [DataMember(Name = "MV")]
        public string MensajeValidacion { get { return this.sMensajeValidacion; } set { this.sMensajeValidacion = value; } }
        [DataMember(Name = "CP")]
        public int CodPieza { get { return this.iCodPieza; } set { this.iCodPieza = value; } }
        [DataMember(Name = "Proceso")]
        public int CodProceso { get { return this.iCodProceso; } set { this.iCodProceso = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHValidarPieza()
        {

        }
        public HHValidarPieza(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHValidarPieza
       (
           bool bValProcesoExitosa,
           bool bValNoDefDespExitosa,
           string sMensajeValidacion,
           int iCodPieza
       )
        {
            this.bValProcesoExitosa = bValProcesoExitosa;
            this.bValNoDefDespExitosa = bValNoDefDespExitosa;
            this.sMensajeValidacion = sMensajeValidacion;
            this.iCodPieza = iCodPieza;
        }
        public HHValidarPieza
        (
            bool bValProcesoExitosa,
            bool bValNoDefDespExitosa,
            string sMensajeValidacion,
            int iCodPieza,
            int iCodProceso
        )
        {
            this.bValProcesoExitosa = bValProcesoExitosa;
            this.bValNoDefDespExitosa = bValNoDefDespExitosa;
            this.sMensajeValidacion = sMensajeValidacion;
            this.iCodPieza = iCodPieza;
            this.iCodProceso = iCodProceso;
        }
        ~HHValidarPieza()
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
