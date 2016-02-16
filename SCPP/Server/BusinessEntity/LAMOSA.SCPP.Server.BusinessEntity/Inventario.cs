using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Inventario", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Inventario:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodInventario = -1;
        private string sClaveInventario = String.Empty;
        private string sDesInventario = String.Empty;
        private int iCodPlanta = -1;
        private DateTime dtFechaRegistro = DateTime.MinValue;
        private bool iActivo = false;
        private string sDesPlanta = String.Empty;

        #endregion

        #region Properties
        [DataMember(Name = "CodInventario")]
        public int CodInventario { get { return iCodInventario; } set { iCodInventario = value; } }
        [DataMember(Name = "ClaveInventario")]
        public string ClaveInventario { get { return sClaveInventario; } set { sClaveInventario = value; } }
        [DataMember(Name = "DesInventario")]
        public string DesInventario { get { return sDesInventario; } set { sDesInventario = value; } }
        [DataMember(Name = "CodPlanta")]
        public int CodPlanta { get { return iCodPlanta; } set { iCodPlanta = value; } }
        [DataMember(Name = "FechaRegistro")]
        public DateTime FechaRegistro { get { return dtFechaRegistro; } set { dtFechaRegistro = value; } }
        [DataMember(Name = "Activo")]
        public bool Activo { get { return iActivo; } set { iActivo = value; } }
        [DataMember(Name = "DesPlanta")]
        public string DesPlanta { get { return sDesPlanta; } set { sDesPlanta = value; } }

        #endregion

        #region Methods
        public Inventario(int iCodInventario,
                string sClaveInventario,
                string sDesInventario,
                int iCodPlanta,
                DateTime dtFechaRegistro,
                bool iActivo,
                string sDesPlanta
        )
        {
            this.iCodInventario = iCodInventario;
            this.sClaveInventario = sClaveInventario;
            this.sDesInventario = sDesInventario;
            this.iCodPlanta = iCodPlanta;
            this.dtFechaRegistro = dtFechaRegistro;
            this.iActivo = iActivo;
            this.sDesPlanta = sDesPlanta;
        }
        public Inventario()
        { }
        public Inventario(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~Inventario()
        { }

        #endregion
    }
}

