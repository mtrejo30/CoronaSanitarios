using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;
using LAMOSA.SCPP.Server.BusinessEntity.Enums;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "Etiqueta", Namespace = "http://LAMOSA/SCPP/BE")]
    [KnownType(typeof(Campo))]
    [Serializable]
    public class Etiqueta : BaseSolutionEntity
    {
        private string sClave = string.Empty;
        private int iCod = -1;
        private string sTemplate;
        private string sUPC = string.Empty;
        private IList<Campo> cCampo = null;
        private string sPieza = string.Empty;
        private TipoEtiqueta enumTipoEtiqueta;
        private string sTarima = string.Empty;

        [DataMember(Name = "Clave")]
        public string Clave { get { return sClave; } set { sClave = value; } }
        [DataMember(Name = "Cod")]
        public int Cod { get { return iCod; } set { iCod = value; } }
        [DataMember(Name = "Template")]
        public string Template { get { return sTemplate; } set { sTemplate = value; } }
        [DataMember(Name = "Campo")]
        public IList<Campo> Campo { get { return cCampo; } set { cCampo = value; } }
        [DataMember(Name = "UPC")]
        public string UPC { get { return sUPC; } set { sUPC = value; } }
        [DataMember(Name = "Pieza")]
        public string Pieza { get { return sPieza; } set { sPieza = value; } }
        [DataMember(Name = "Tarima")]
        public string Tarima { get { return sTarima; } set { sTarima = value; } }
        [DataMember(Name = "TipoEtiqueta")]
        public int TipoEtiqueta
        { get { return (int)enumTipoEtiqueta; } set { enumTipoEtiqueta = (TipoEtiqueta)value; } }
    }
}
