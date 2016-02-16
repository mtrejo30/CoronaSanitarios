using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using LAMOSA.SCPP.Client.View.HandHeld.DataAccess;
using System.Data;

namespace LAMOSA.SCPP.Client.View.HandHeld.BusinessComponent
{
    public class CarroPendienteSecador
    {
        c00_Common common = new c00_Common();
        public CarroPendienteSecador() { }
        public DataTable ObtenerCarroPendienteSecador(int iCodigoPlanta)
        {
            try
            {
                if (common.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = ConfigDataAccess.ObtenerServiceProxy();
                    DataTable dtCarrosPendientes = proxy.ObtenerCarrosPendientesSecador(iCodigoPlanta,true);
                    return dtCarrosPendientes;
                }
                else throw new Exception("No se tiene conexion a la red, intente mas tarde.");
            }
            catch (Exception ex) { throw ex; }
        }
        public DataTable ObtenerCarroPendienteSecadorDetalle(int iCarro)
        {
            try
            {
                if (common.EstaServicioDisponible())
                {
                    HHsvc.SCPP_HH proxy = ConfigDataAccess.ObtenerServiceProxy();
                    DataTable dtCarrosPendientesDetalle = proxy.ObtenerCarrosPendientesSecadorDetalle(iCarro, true);
                    return dtCarrosPendientesDetalle;
                }
                else throw new Exception("No se tiene conexion a la red, intente mas tarde.");
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
