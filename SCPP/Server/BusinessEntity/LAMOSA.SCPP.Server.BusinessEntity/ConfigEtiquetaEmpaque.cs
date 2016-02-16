using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "ConfigEtiquetaEmpaque", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class ConfigEtiquetaEmpaque : BaseSolutionEntity
    {

	    #region PrivateFields
        //TODO: incluir campos descriptivos
		private string sSkuSegmentoPieza = String.Empty;
		private int iCodTemplateEmpaque = -1;
		private int iCodTextoEmpaque1 = -1;
		private int iCodTextoEmpaque2 = -1;
		private int iCodTextoEmpaque3 = -1;
		private int iCodImagenEmpaque = -1;
		private DateTime dtFechaRegistro = DateTime.MinValue;
		private DateTime dtFechaBaja = DateTime.MinValue;

#endregion

        #region Properties
		[DataMember(Name = "SkuSegmentoPieza")]
		 public string SkuSegmentoPieza { get { return sSkuSegmentoPieza; } set { sSkuSegmentoPieza = value; } }
		[DataMember(Name = "CodTemplateEmpaque")]
		 public int CodTemplateEmpaque { get { return iCodTemplateEmpaque; } set { iCodTemplateEmpaque = value; } }
		[DataMember(Name = "CodTextoEmpaque1")]
		 public int CodTextoEmpaque1 { get { return iCodTextoEmpaque1; } set { iCodTextoEmpaque1 = value; } }
		[DataMember(Name = "CodTextoEmpaque2")]
		 public int CodTextoEmpaque2 { get { return iCodTextoEmpaque2; } set { iCodTextoEmpaque2 = value; } }
		[DataMember(Name = "CodTextoEmpaque3")]
		 public int CodTextoEmpaque3 { get { return iCodTextoEmpaque3; } set { iCodTextoEmpaque3 = value; } }
		[DataMember(Name = "CodImagenEmpaque")]
		 public int CodImagenEmpaque { get { return iCodImagenEmpaque; } set { iCodImagenEmpaque = value; } }
		[DataMember(Name = "FechaRegistro")]
		 public DateTime FechaRegistro { get { return dtFechaRegistro; } set { dtFechaRegistro = value; } }
		[DataMember(Name = "FechaBaja")]
		 public DateTime FechaBaja { get { return dtFechaBaja; } set { dtFechaBaja = value; } }

	#endregion

	    #region Methods

        #region Constructors and Destructor

        public ConfigEtiquetaEmpaque()
        {

        }
        public ConfigEtiquetaEmpaque(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        public ConfigEtiquetaEmpaque(
            string sSkuSegmentoPieza,
		    int iCodTemplateEmpaque,
		    int iCodTextoEmpaque1,
		    int iCodTextoEmpaque2,
	    	int iCodTextoEmpaque3,
		    int iCodImagenEmpaque,
		    DateTime dtFechaRegistro,
		    DateTime dtFechaBaja)
        {
            this.sSkuSegmentoPieza = sSkuSegmentoPieza;
		    this.iCodTemplateEmpaque = iCodTemplateEmpaque;
		    this.iCodTextoEmpaque1 = iCodTextoEmpaque1;
		    this.iCodTextoEmpaque2 = iCodTextoEmpaque2;
		    this.iCodTextoEmpaque3 = iCodTextoEmpaque3;
		    this.iCodImagenEmpaque = iCodImagenEmpaque;
		    this.dtFechaRegistro = dtFechaRegistro;
		    this.dtFechaBaja = dtFechaBaja;
        }
        ~ConfigEtiquetaEmpaque()
        {

        }

        #endregion Constructors and Destructor

        #endregion Methods

    } // class
}
