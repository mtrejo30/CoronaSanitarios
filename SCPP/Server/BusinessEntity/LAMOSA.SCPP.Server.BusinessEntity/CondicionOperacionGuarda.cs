


using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "CondicionOperacionGuarda", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class CondicionOperacionGuarda:BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodCondicionOperacion = -1;   
        private int iCodArea = -1;
        private double dTemperatura = -1;
        private double dHumedad = -1;
       
        #endregion

        #region Properties
        [DataMember(Name = "CodCondicionOperacion")]
        public int CodCondicionOperacion { get { return iCodCondicionOperacion; } set { iCodCondicionOperacion = value; } }
        [DataMember(Name = "CodArea")]
        public int CodArea { get { return iCodArea; } set { iCodArea = value; } }
        [DataMember(Name = "Temperatura")]
        public double Temperatura { get { return dTemperatura; } set { dTemperatura = value; } }
        [DataMember(Name = "Humedad")]
        public double Humedad { get { return dHumedad; } set { dHumedad = value; } }
       

        #endregion

        #region Methods
        public CondicionOperacionGuarda(int iCodCondicionOperacion, int iCodArea, double dTemperatura,
                double dHumedad
        )
        {
            this.iCodCondicionOperacion = iCodCondicionOperacion;
           
            this.iCodArea = iCodArea;
           
            this.dTemperatura = dTemperatura;
            this.dHumedad = dHumedad;
           
        }
        public CondicionOperacionGuarda()
        { }
        public CondicionOperacionGuarda(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~CondicionOperacionGuarda()
        { }

        #endregion
    }
}
