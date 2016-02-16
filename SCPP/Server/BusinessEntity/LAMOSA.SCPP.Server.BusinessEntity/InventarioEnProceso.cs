using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity{
	[DataContract(Name="InventarioEnProceso", Namespace="http://LAMOSA/SCPP/BE")]
	[Serializable]
	public class InventarioEnProceso:BaseSolutionEntity
	{
	#region PrivateFields
        private int iCodAlmacen = -1;
        private int iCodPlanta = -1;
        private string sDesPlanta = String.Empty;
	    private int iCodProceso = -1;
        private string sDesProceso = String.Empty;
        private int iCodTipoArticulo = -1;
        private string sClaveTipoArticulo = String.Empty;
        private string sDesTipoArticulo = String.Empty;
        private int iCodModeloArticulo = -1;
        private string sClaveArticulo = String.Empty;
        private string sCodigoBarras = String.Empty;
        private string sDesArticulo = String.Empty;
        private DateTime dtFechaRegistro = DateTime.MinValue;
	    private string sDesCentroTrabajo = String.Empty;
        private int iCodColor = -1;
		private string sDesColor = String.Empty;
        private int iCodCalidad = -1;
		private string sDesCalidad = String.Empty;
		private int iCantidad = -1;

#endregion

    #region Properties
        [DataMember(Name = "CodAlmacen")]
        public int CodAlmacen { get { return iCodAlmacen; } set { iCodAlmacen = value; } }
        [DataMember(Name = "CodPlanta")]
        public int CodPlanta { get { return iCodPlanta; } set { iCodPlanta = value; } }
        [DataMember(Name = "DesPlanta")]
        public string DesPlanta { get { return sDesPlanta; } set { sDesPlanta = value; } }
     	[DataMember(Name = "CodProceso")]
		 public int CodProceso { get { return iCodProceso; } set { iCodProceso = value; } }
        [DataMember(Name = "DesProceso")]
        public string DesProceso { get { return sDesProceso; } set { sDesProceso = value; } }
        [DataMember(Name = "CodTipoArticulo")]
        public int CodTipoArticulo { get { return iCodTipoArticulo; } set { iCodTipoArticulo = value; } }
        [DataMember(Name = "ClaveTipoArticulo")]
        public string ClaveTipoArticulo { get { return sClaveTipoArticulo; } set { sClaveTipoArticulo = value; } }
        [DataMember(Name = "DesTipoArticulo")]
        public string DesTipoArticulo { get { return sDesTipoArticulo; } set { sDesTipoArticulo = value; } }
        [DataMember(Name = "CodModeloArticulo")]
        public int CodModeloArticulo { get { return iCodModeloArticulo; } set { iCodModeloArticulo = value; } }
        [DataMember(Name = "ClaveArticulo")]
        public string ClaveArticulo { get { return sClaveArticulo; } set { sClaveArticulo = value; } }
        [DataMember(Name = "CodigoBarras")]
        public string CodigoBarras { get { return sCodigoBarras; } set { sCodigoBarras = value; } }
        [DataMember(Name = "DesArticulo")]
		 public string DesArticulo { get { return sDesArticulo; } set { sDesArticulo = value; } }
        [DataMember(Name = "FechaRegistro")]
		 public DateTime FechaRegistro { get { return dtFechaRegistro; } set { dtFechaRegistro = value; } }
		[DataMember(Name = "DesCentroTrabajo")]
		 public string DesCentroTrabajo { get { return sDesCentroTrabajo; } set { sDesCentroTrabajo = value; } }
        [DataMember(Name = "CodColor")]
        public int CodColor { get { return iCodColor; } set { iCodColor = value; } }
		[DataMember(Name = "DesColor")]
        public string DesColor { get { return sDesColor; } set { sDesColor = value; } }
        [DataMember(Name = "CodCalidad")]
        public int CodCalidad { get { return iCodCalidad; } set { iCodCalidad = value; } }
		[DataMember(Name = "DesCalidad")]
		 public string DesCalidad { get { return sDesCalidad; } set { sDesCalidad = value; } }
       [DataMember(Name = "Cantidad")]
        public int Cantidad { get { return iCantidad; } set { iCantidad = value; } }

	#endregion

	#region Methods

		public InventarioEnProceso()
		{
        }

        public InventarioEnProceso(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }

        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }

        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new InventarioEnProceso());
        }

        public InventarioEnProceso
        (
            
            int iCodAlmacen ,
            int iCodPlanta ,
            string sDesPlanta,
            int iCodProceso ,
            string sDesProceso,
            int iCodTipoArticulo,
            string sClaveTipoArticulo,
            string sDesTipoArticulo,
            int iCodModeloArticulo,
            string sClaveArticulo,
            string sCodigoBarras,
            string sDesArticulo ,
            DateTime dtFechaRegistro ,
            string sDesCentroTrabajo ,
            int iCodColor,
            string sDesColor  ,
            int iCodCalidad,
            string sDesCalidad  ,
            int iCantidad  
        )
        {
            this.iCodAlmacen = iCodAlmacen;
            this.iCodPlanta = iCodPlanta;
            this.sDesPlanta = sDesPlanta;
            this.iCodProceso = iCodProceso;
            this.sDesProceso = sDesProceso;
            this.iCodTipoArticulo = iCodTipoArticulo;
            this.sClaveTipoArticulo = sClaveTipoArticulo;
            this.sDesTipoArticulo = sDesTipoArticulo;
            this.iCodModeloArticulo = iCodModeloArticulo;
            this.sClaveArticulo = sClaveArticulo;
            this.sCodigoBarras = sCodigoBarras;
            this.sDesArticulo = sDesArticulo;
            this.dtFechaRegistro = dtFechaRegistro;
                        this.sDesCentroTrabajo  = sDesCentroTrabajo  ;
                        this.iCodColor = iCodColor;
                        this.sDesColor = sDesColor;
                        this.iCodCalidad = iCodCalidad;
                        this.sDesCalidad = sDesCalidad;
            this.iCantidad     = iCantidad     ;
        }


	


		~ InventarioEnProceso()
		{ }

    
	#endregion
	}
}
