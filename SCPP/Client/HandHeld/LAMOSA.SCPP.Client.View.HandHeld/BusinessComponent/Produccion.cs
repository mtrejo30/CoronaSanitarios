using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld.BusinessComponent
{
    public class Produccion
    {
        c00_Common common = new c00_Common();
        public DataTable Obtener(int iCodigoOperador, int iCodigoProceso)
        {
            HHsvc.SCPP_HH proxy = null;
            try
            {
                if (iCodigoOperador == 0)
                    throw new Exception("Valor de parametro Usuario incorrecto.");
                if (common.EstaServicioDisponible())
                {
                    proxy = ConfigDataAccess.ObtenerServiceProxy();
                    return proxy.ObtenerProduccion(iCodigoOperador, true, iCodigoProceso, true);
                }
                else
                    throw new Exception("No es posible establecer comunicación.");
            }
            catch (Exception ex) { throw ex; }
            finally { if (proxy != null)proxy.Dispose(); }
        }
    }
}
