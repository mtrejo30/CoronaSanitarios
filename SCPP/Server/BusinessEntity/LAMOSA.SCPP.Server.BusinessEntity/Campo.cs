using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Campo", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class Campo : BaseSolutionEntity
    {
        private int iCod = -1;
        private string sClave = string.Empty;
        private string sNombre = string.Empty;
        private string sDescripcion = string.Empty;
        private int iTipo = -1;
        private string iValor = string.Empty;

        [DataMember(Name = "Cod")]
        public int Cod { get { return iCod; } set { iCod = value; } }
        [DataMember(Name = "Clave")]
        public string Clave { get { return sClave; } set { sClave = value; } }
        [DataMember(Name = "Nombre")]
        public string Nombre { get { return sNombre; } set { sNombre = value; } }
        [DataMember(Name = "Descripcion")]
        public string Descripcion { get { return sDescripcion; } set { sDescripcion = value; } }
        [DataMember(Name = "Tipo")]
        public int Tipo { get { return iTipo; } set { iTipo = value; } }
        [DataMember(Name = "Valor")]
        public string Valor { get { return iValor; } set { iValor = value; } }
    }
}
