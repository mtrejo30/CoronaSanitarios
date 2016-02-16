using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity{
	[DataContract(Name="RutaProceso", Namespace="http://LAMOSA/SCPP/BE")]
	[Serializable]
	public class RutaProceso:BaseSolutionEntity
	{
	#region PrivateFields
		private int iCodAlmacen = -1;
		private int iCodPlanta = -1;
		private int iCodProceso = -1;
		private string sProceso = String.Empty;
		private int iCodProcesoPadre = -1;
		private string sProcesoPadre = String.Empty;
		private bool bMFG = false;
		private bool bRequerido = false;
		private int iOrdenOpera = -1;

#endregion

#region Properties
		[DataMember(Name = "CodAlmacen")]
		 public int CodAlmacen { get { return iCodAlmacen; } set { iCodAlmacen = value; } }
		[DataMember(Name = "CodPlanta")]
		 public int CodPlanta { get { return iCodPlanta; } set { iCodPlanta = value; } }
		[DataMember(Name = "CodProceso")]
		 public int CodProceso { get { return iCodProceso; } set { iCodProceso = value; } }
		[DataMember(Name = "Proceso")]
		 public string Proceso { get { return sProceso; } set { sProceso = value; } }
		[DataMember(Name = "CodProcesoPadre")]
		 public int CodProcesoPadre { get { return iCodProcesoPadre; } set { iCodProcesoPadre = value; } }
		[DataMember(Name = "ProcesoPadre")]
		 public string ProcesoPadre { get { return sProcesoPadre; } set { sProcesoPadre = value; } }
		[DataMember(Name = "MFG")]
		 public bool MFG { get { return bMFG; } set { bMFG = value; } }
		[DataMember(Name = "Requerido")]
		 public bool Requerido { get { return bRequerido; } set { bRequerido = value; } }
		[DataMember(Name = "OrdenOpera")]
		 public int OrdenOpera { get { return iOrdenOpera; } set { iOrdenOpera = value; } }

	#endregion

	#region Methods
public RutaProceso(int iCodAlmacen,
		int iCodPlanta,
		int iCodProceso,
		string sProceso,
		int iCodProcesoPadre,
		string sProcesoPadre,
		bool bMFG,
		bool bRequerido,
		int iOrdenOpera
)
{		this.iCodAlmacen = iCodAlmacen;
		this.iCodPlanta = iCodPlanta;
		this.iCodProceso = iCodProceso;
		this.sProceso = sProceso;
		this.iCodProcesoPadre = iCodProcesoPadre;
		this.sProcesoPadre = sProcesoPadre;
		this.bMFG = bMFG;
		this.bRequerido = bRequerido;
		this.iOrdenOpera = iOrdenOpera;
}
		public RutaProceso()
		{ }
		public RutaProceso(DataRow row)
		{
 			SetPropertiesFromDataRow(row); 
		}
		~ RutaProceso()
		{ }
        /// <summary>
        /// Obtiene un arreglo de objetos con los valores de las propiedades
        /// </summary>
        /// <returns>objetct[]</returns>
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
            return GetPropertyNamesArray(new RutaProceso());
        }

	#endregion
	}
}

