using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "ZonaDefectoCbo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class ZonaDefectoCbo : BaseSolutionEntity
    {
        private int iCodZonaDefecto = -1;
        private string sDesZonaDefecto = string.Empty;

        [DataMember(Name = "CodZonaDefecto")]
        public int CodZonaDefecto
        {
            get { return iCodZonaDefecto; }
            set { iCodZonaDefecto = value; }
        }

        [DataMember(Name = "DesZonaDefecto")]
        public string DesZonaDefecto
        {
            get { return sDesZonaDefecto; }
            set { sDesZonaDefecto = value; }
        }

        public ZonaDefectoCbo()
        {
        }

        public ZonaDefectoCbo(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }

        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new ZonaDefectoCbo());
        }

        public ZonaDefectoCbo(int iCodZonaDefecto, string sDesZonaDefecto)
        {
            this.iCodZonaDefecto = iCodZonaDefecto;
            this.sDesZonaDefecto = sDesZonaDefecto;
        }

        ~ZonaDefectoCbo()
        {

        }
    }
}
