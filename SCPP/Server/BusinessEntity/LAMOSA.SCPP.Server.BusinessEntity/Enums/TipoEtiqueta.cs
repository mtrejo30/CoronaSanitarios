using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity.Enums
{
    [DataContract(Name="TipoEtiqueta", Namespace = "http://LAMOSA/SCPP/BE"), Serializable]
    public enum TipoEtiqueta :int
    { 
        Pieza = 1,
        Tarima = 2
    }
}