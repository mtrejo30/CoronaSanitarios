using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Pieza", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Pieza : BaseSolutionEntity
    {

        #region PrivateFields
        private int iCodPieza = -1;
        private int iCodConfigBanco = -1;
        private int iCodMolde = -1;
        private int iPosicion = -1;
        private int iCodArticulo = -1;

        #endregion

        #region Properties
        [DataMember(Name = "CodPieza")]
        public int CodPieza { get { return iCodPieza; } set { iCodPieza = value; } }
        [DataMember(Name = "CodConfigBanco")]
        public int CodConfigBanco { get { return iCodConfigBanco; } set { iCodConfigBanco = value; } }
        [DataMember(Name = "CodMolde")]
        public int CodMolde { get { return iCodMolde; } set { iCodMolde = value; } }
        [DataMember(Name = "Posicion")]
        public int Posicion { get { return iPosicion; } set { iPosicion = value; } }
        [DataMember(Name = "CodArticulo")]
        public int CodArticulo { get { return iCodArticulo; } set { iCodArticulo = value; } }

        #endregion

        #region Methods

        #region Constructors and Destructor

        public Pieza()
        {

        }
        public Pieza(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        public Pieza(
            int iCodPieza,
            int iCodConfigBanco,
            int iCodMolde,
            int iPosicion,
            int iCodArticulo)
        {
            this.iCodPieza = iCodPieza;
            this.iCodConfigBanco = iCodConfigBanco;
            this.iCodMolde = iCodMolde;
            this.iPosicion = iPosicion;
            this.iCodArticulo = iCodArticulo;
        }
        ~Pieza()
        {

        }

        #endregion Constructors and Destructor

        #endregion Methods

    } // class
}
