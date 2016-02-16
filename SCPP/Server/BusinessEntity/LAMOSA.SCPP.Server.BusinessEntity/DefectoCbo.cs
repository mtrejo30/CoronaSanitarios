using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "DefectoCbo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class DefectoCbo : BaseSolutionEntity
    {
        private int iCodDefecto = -1;
        private string sDesDefecto = string.Empty;

        [DataMember(Name = "CodDefecto")]
        public int CodDefecto 
        {
            get { return iCodDefecto; }
            set { iCodDefecto = value; }
        }

        [DataMember(Name = "DesDefecto")]
        public string DesDefecto
        {
            get { return sDesDefecto; }
            set { sDesDefecto = value; }
        }

        public DefectoCbo()
        {
        }

        public DefectoCbo(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }

        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new DefectoCbo());
        }

        public DefectoCbo(int iCodDefecto,string sDesDefecto)
        {
            this.iCodDefecto = iCodDefecto;
            this.sDesDefecto = sDesDefecto;
        }

        ~DefectoCbo()
        {

        }
    }
}
