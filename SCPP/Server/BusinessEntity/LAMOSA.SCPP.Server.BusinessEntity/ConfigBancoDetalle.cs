using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity{
    [DataContract(Name = "ConfigBancoDetalle", Namespace = "http://LAMOSA/SCPP/BE")]
	[Serializable]
	public class ConfigBancoDetalle:BaseSolutionEntity
	{
	#region PrivateFields
		private int iCodConfigBanco = -1;
		private int iCodTipoArticulo = -1;
		private string sClaveTipoArticulo = String.Empty;
		private int iCodArticulo = -1;
		private string sClaveArticulo = String.Empty;
		private string sDesArticulo = String.Empty;
		private int iCodMolde = -1;
		private string sClaveMolde = String.Empty;
		private int iNumImpresiones = -1;
		private int iCantidadMoldes = -1;
		private string iPosicion = string.Empty;
		private int iLimiteVaciadas = -1;
		private int iVaciadasDiarias = -1;
		private int iVaciadasAcumuladas = -1;
        private int iNumeroImpresiones = -1;
        

#endregion

#region Properties
		[DataMember(Name = "CodConfigBanco")]
		 public int CodConfigBanco { get { return iCodConfigBanco; } set { iCodConfigBanco = value; } }
		[DataMember(Name = "CodTipoArticulo")]
		 public int CodTipoArticulo { get { return iCodTipoArticulo; } set { iCodTipoArticulo = value; } }
		[DataMember(Name = "ClaveTipoArticulo")]
		 public string ClaveTipoArticulo { get { return sClaveTipoArticulo; } set { sClaveTipoArticulo = value; } }
		[DataMember(Name = "CodArticulo")]
		 public int CodArticulo { get { return iCodArticulo; } set { iCodArticulo = value; } }
		[DataMember(Name = "ClaveArticulo")]
		 public string ClaveArticulo { get { return sClaveArticulo; } set { sClaveArticulo = value; } }
		[DataMember(Name = "DesArticulo")]
		 public string DesArticulo { get { return sDesArticulo; } set { sDesArticulo = value; } }
		[DataMember(Name = "CodMolde")]
		 public int CodMolde { get { return iCodMolde; } set { iCodMolde = value; } }
		[DataMember(Name = "ClaveMolde")]
		 public string ClaveMolde { get { return sClaveMolde; } set { sClaveMolde = value; } }
		[DataMember(Name = "NumImpresiones")]
		 public int NumImpresiones { get { return iNumImpresiones; } set { iNumImpresiones = value; } }
		[DataMember(Name = "CantidadMoldes")]
		 public int CantidadMoldes { get { return iCantidadMoldes; } set { iCantidadMoldes = value; } }
		[DataMember(Name = "Posicion")]
		 public string Posicion { get { return iPosicion; } set { iPosicion = value; } }
		[DataMember(Name = "LimiteVaciadas")]
		 public int LimiteVaciadas { get { return iLimiteVaciadas; } set { iLimiteVaciadas = value; } }
		[DataMember(Name = "VaciadasDiarias")]
		 public int VaciadasDiarias { get { return iVaciadasDiarias; } set { iVaciadasDiarias = value; } }
		[DataMember(Name = "VaciadasAcumuladas")]
		 public int VaciadasAcumuladas { get { return iVaciadasAcumuladas; } set { iVaciadasAcumuladas = value; } }
        [DataMember(Name = "NumeroImpresiones")]
        public int NumeroImpresiones { get { return iNumeroImpresiones; } set { iNumeroImpresiones = value; } }

	#endregion

	#region Methods
public ConfigBancoDetalle(int iCodConfigBanco,
		int iCodTipoArticulo,
		string sClaveTipoArticulo,
		int iCodArticulo,
		string sClaveArticulo,
		string sDesArticulo,
		int iCodMolde,
		string sClaveMolde,
		int iNumImpresiones,
		int iCantidadMoldes,
		string iPosicion,
		int iLimiteVaciadas,
		int iVaciadasDiarias,
		int iVaciadasAcumuladas,
        int iNumeroImpresiones
)
{		this.iCodConfigBanco = iCodConfigBanco;
		this.iCodTipoArticulo = iCodTipoArticulo;
		this.sClaveTipoArticulo = sClaveTipoArticulo;
		this.iCodArticulo = iCodArticulo;
		this.sClaveArticulo = sClaveArticulo;
		this.sDesArticulo = sDesArticulo;
		this.iCodMolde = iCodMolde;
		this.sClaveMolde = sClaveMolde;
		this.iNumImpresiones = iNumImpresiones;
		this.iCantidadMoldes = iCantidadMoldes;
		this.iPosicion = iPosicion;
		this.iLimiteVaciadas = iLimiteVaciadas;
		this.iVaciadasDiarias = iVaciadasDiarias;
		this.iVaciadasAcumuladas = iVaciadasAcumuladas;
        this.iNumeroImpresiones = iNumeroImpresiones;
}
		public ConfigBancoDetalle()
		{ }
		public ConfigBancoDetalle(DataRow row)
		{
 			SetPropertiesFromDataRow(row); 
		}
        ~ConfigBancoDetalle()
		{ }
        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        /// <summary>
        /// Obtiene un arreglo con los nombres solamente de las propiedades
        /// </summary>
        /// <returns></returns>
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new ConfigBancoDetalle());
        }
	#endregion
	}
}
