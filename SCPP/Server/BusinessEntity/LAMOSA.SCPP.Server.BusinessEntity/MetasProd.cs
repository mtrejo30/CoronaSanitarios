using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "MetasProd", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class MetasProd : BaseSolutionEntity
    {
        #region PrivateFields
        private string sPiezasProcesadas = String.Empty;
        private string sInventarios = String.Empty;
        private string sPiezasMalas = String.Empty;
        private string sPiezasMalasVerde = String.Empty;
        private string sPiezasMalasQuemado = String.Empty;
        private string sCalidad1 = String.Empty;
        private string sCalidad2 = String.Empty;
        private string sCalidad3 = String.Empty;
        private string sCalidad4 = String.Empty;
        private bool bActivo = false;
        private int iCodMetas = -1;
        private int iPlanta = -1;
        private int iCantProc = -1;
        private int iTipoProc = -1;
        private int iCantInv = -1;
        private int iTipoInv = -1;
        private int iCantDesp = -1;
        private int iTipoDesp = -1;
        private int iCantVerde = -1;
        private int iTipoVerde = -1;
        private int iCantQuemado = -1;
        private int iTipoQuemado = -1;
        private int iICalidad1 = -1;
        private int iPorcCal1 = -1;
        private int iTipoCal1 = -1;
        private int iICalidad2 = -1;
        private int iPorcCal2 = -1;
        private int iTipoCal2 = -1;
        private int iICalidad3 = -1;
        private int iPorcCal3 = -1;
        private int iTipoCal3 = -1;
        private int iICalidad4 = -1;
        private int iPorcCal4 = -1;
        private int iTipoCal4 = -1;

        #endregion

        #region Properties

        [DataMember(Name = "PiezasProcesadas")]
        public string PiezasProcesadas { get { return sPiezasProcesadas; } set { sPiezasProcesadas = value; } }
        
        [DataMember(Name = "Inventarios")]
        public string Inventarios { get { return sInventarios; } set { sInventarios = value; } }
       
        [DataMember(Name = "PiezasMalas")]
        public string PiezasMalas { get { return sPiezasMalas; } set { sPiezasMalas = value; } }
        
        [DataMember(Name = "PiezasMalasVerde")]
        public string PiezasMalasVerde { get { return sPiezasMalasVerde; } set { sPiezasMalasVerde = value; } }
        
        [DataMember(Name = "PiezasMalasQuemado")]
        public string PiezasMalasQuemado { get { return sPiezasMalasQuemado; } set { sPiezasMalasQuemado = value; } }
        
        [DataMember(Name = "Calidad1")]
        public string Calidad1 { get { return sCalidad1; } set { sCalidad1 = value; } }
        [DataMember(Name = "Calidad2")]
        public string Calidad2 { get { return sCalidad2; } set { sCalidad2 = value; } }
        [DataMember(Name = "Calidad3")]
        public string Calidad3 { get { return sCalidad3; } set { sCalidad3 = value; } }
       
        [DataMember(Name = "Calidad4")]
        public string Calidad4 { get { return sCalidad4; } set { sCalidad4 = value; } }
        
        [DataMember(Name = "Activo")]
        public bool Activo { get { return bActivo; } set { bActivo = value; } }
       
        [DataMember(Name = "CodMetas")]
        public int CodMetas { get { return iCodMetas; } set { iCodMetas = value; } }
       
        [DataMember(Name = "Planta")]
        public int Planta { get { return iPlanta; } set { iPlanta = value; } }
       
        [DataMember(Name = "CantProc")]
        public int CantProc { get { return iCantProc; } set { iCantProc = value; } }
        [DataMember(Name = "TipoProc")]
        public int TipoProc { get { return iTipoProc; } set { iTipoProc = value; } }
      
        [DataMember(Name = "CantInv")]
        public int CantInv { get { return iCantInv; } set { iCantInv = value; } }
       
        [DataMember(Name = "TipoInv")]
        public int TipoInv { get { return iTipoInv; } set { iTipoInv = value; } }
      
        [DataMember(Name = "CantDesp")]
        public int CantDesp { get { return iCantDesp; } set { iCantDesp = value; } }
        
        [DataMember(Name = "TipoDesp")]
        public int TipoDesp { get { return iTipoDesp; } set { iTipoDesp = value; } }

        [DataMember(Name = "CantVerde")]
        public int CantVerde { get { return iCantVerde; } set { iCantVerde = value; } }

        [DataMember(Name = "TipoVerde")]
        public int TipoVerde { get { return iTipoVerde; } set { iTipoVerde = value; } }

        [DataMember(Name = "CantQuemado")]
        public int CantQuemado { get { return iCantQuemado; } set { iCantQuemado = value; } }

        [DataMember(Name = "TipoQuemado")]
        public int TipoQuemado { get { return iTipoQuemado; } set { iTipoQuemado = value; } }

        [DataMember(Name = "ICalidad1")]
        public int ICalidad1 { get { return iICalidad1; } set { iICalidad1 = value; } }

        [DataMember(Name = "PorcCal1")]
        public int PorcCal1 { get { return iPorcCal1; } set { iPorcCal1 = value; } }

        [DataMember(Name = "TipoCal1")]
        public int TipoCal1 { get { return iTipoCal1; } set { iTipoCal1 = value; } }
   
        [DataMember(Name = "ICalidad2")]
        public int ICalidad2 { get { return iICalidad2; } set { iICalidad2 = value; } }
       
        [DataMember(Name = "PorcCal2")]
        public int PorcCal2 { get { return iPorcCal2; } set { iPorcCal2 = value; } }
        [DataMember(Name = "TipoCal2")]
        public int TipoCal2 { get { return iTipoCal2; } set { iTipoCal2 = value; } }

        [DataMember(Name = "ICalidad3")]
        public int ICalidad3 { get { return iICalidad3; } set { iICalidad3 = value; } }
        [DataMember(Name = "PorcCal3")]
        public int PorcCal3 { get { return iPorcCal3; } set { iPorcCal3 = value; } }
        [DataMember(Name = "TipoCal3")]
        public int TipoCal3 { get { return iTipoCal3; } set { iTipoCal3 = value; } }

        [DataMember(Name = "ICalidad4")]
        public int ICalidad4 { get { return iICalidad4; } set { iICalidad4 = value; } }
        [DataMember(Name = "PorcCal4")]
        public int PorcCal4 { get { return iPorcCal4; } set { iPorcCal4 = value; } }
        [DataMember(Name = "TipoCal4")]
        public int TipoCal4 { get { return iTipoCal4; } set { iTipoCal4 = value; } }
       

        #endregion

        #region Methods
        public MetasProd(
        string sPiezasProcesadas,
        string sInventarios,
        string sPiezasMalas,
        string sPiezasMalasVerde,
        string sPiezasMalasQuemado,
        string sCalidad1,
        string sCalidad2,
        string sCalidad3,
        string sCalidad4,
        bool bActivo,
        int iCodMetas,
        int iPlanta,
        int iCantProc,
        int iTipoProc,
        int iCantInv,
        int iTipoInv,
        int iCantDesp,
        int iTipoDesp,
        int iCantVerde,
        int iTipoVerde,
        int iCantQuemado,
        int iTipoQuemado,
        int iICalidad1,
        int iPorcCal1,
        int iTipoCal1,
        int iICalidad2,
        int iPorcCal2,
        int iTipoCal2,
        int iICalidad3,
        int iPorcCal3,
        int iTipoCal3,
        int iICalidad4,
        int iPorcCal4,
        int iTipoCal4)
           
       {   
        this.sPiezasProcesadas = sPiezasProcesadas;
        this.sInventarios = sInventarios;
        this.sPiezasMalas=sPiezasMalas;
        this.sPiezasMalasVerde=sPiezasMalasVerde;
        this.sPiezasMalasQuemado=sPiezasMalasQuemado;
        this.sCalidad1 =sCalidad1;
        this.sCalidad2 =sCalidad2;
        this.sCalidad3 =sCalidad3;
        this.sCalidad4 =sCalidad4;
        this.bActivo =bActivo;
        this.iCodMetas =iCodMetas;
        this.iPlanta =iPlanta;
        this.iCantProc =iCantProc;
        this.iTipoProc =iTipoProc;
        this.iCantInv =iCantInv;
        this.iTipoInv =iTipoInv;
        this.iCantDesp =iCantDesp;
        this.iTipoDesp =iTipoDesp;
        this.iCantVerde =iCantVerde;
        this.iTipoVerde =iTipoVerde;
        this.iCantQuemado =iCantQuemado;
        this.iTipoQuemado =iTipoQuemado;
        this.iICalidad1 =iICalidad1;
        this.iPorcCal1 =iPorcCal1;
        this.iTipoCal1 =iTipoCal1;
        this.iICalidad2 =iICalidad2;
        this.iPorcCal2 =iPorcCal2;
        this.iTipoCal2 =iTipoCal2;
        this.iICalidad3 =iICalidad3;
        this.iPorcCal3 =iPorcCal3;
        this.iTipoCal3 =iTipoCal3;
        this.iICalidad4 =iICalidad4;
        this.iPorcCal4 =iPorcCal4;
        this.iTipoCal4 =iTipoCal4;

        }

        public MetasProd()
        { }
        public MetasProd(DataRow row)
        {
        SetPropertiesFromDataRow(row);
        }
        ~MetasProd()
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
            return GetPropertyNamesArray(new MetasProd());
        }

        #endregion
    }
}
