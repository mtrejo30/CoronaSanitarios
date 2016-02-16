using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Configuracion", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Configuracion : BaseSolutionEntity
    {

        #region PrivateFields
        private int iCodConfiguracion = -1;
        private string sDesConfiguracion = String.Empty;
        private int iValorConfiguracion = -1;

        #endregion

        #region Properties
        [DataMember(Name = "CodConfiguracion")]
        public int CodConfiguracion { get { return iCodConfiguracion; } set { iCodConfiguracion = value; } }
        [DataMember(Name = "DesConfiguracion")]
        public string DesConfiguracion { get { return sDesConfiguracion; } set { sDesConfiguracion = value; } }
        [DataMember(Name = "ValorConfiguracion")]
        public int ValorConfiguracion { get { return iValorConfiguracion; } set { iValorConfiguracion = value; } }

        #endregion

        #region Methods

        #region Constructors and Destructor

        public Configuracion()
        {

        }
        public Configuracion(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        public Configuracion(
            int iCodConfiguracion,
            string sDesConfiguracion,
            int iValorConfiguracion)
        {
            this.iCodConfiguracion = iCodConfiguracion;
            this.sDesConfiguracion = sDesConfiguracion;
            this.iValorConfiguracion = iValorConfiguracion;
        }
        ~Configuracion()
        {

        }

        #endregion Constructors and Destructor
        
        #endregion Methods

    } // class
}
