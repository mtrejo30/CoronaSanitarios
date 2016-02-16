using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "PlantaCalidad", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class PlantaCalidad : BaseSolutionEntity
    {
        private int iCodPlanta = -1;
        private string sCalidad = string.Empty;

        [DataMember(Name = "CodPlanta")]
        public int CodPlanta 
        {
            get { return iCodPlanta; }
            set { iCodPlanta = value; }
        }

        [DataMember(Name = "Calidad")]
        public string Calidad
        {
            get { return sCalidad; }
            set { sCalidad = value; }
        }

        public PlantaCalidad()
        {
        }

        public PlantaCalidad(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }

        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new PlantaCalidad());
        }

        public PlantaCalidad(int iCodPlanta,string sCalidad)
        {
            this.iCodPlanta = iCodPlanta;
            this.sCalidad = sCalidad;
        }

        ~PlantaCalidad()
        {

        }
    }
}
