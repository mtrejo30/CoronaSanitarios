using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld.BusinessComponent
{
    public class Kardex
    {
        c00_Common common = new c00_Common();
        public Kardex() { }

        public DataSet ObtenerKardexPieza(int? iCodigoPieza, string sCodigoBarras ){
            try {
                if (common.EstaServicioDisponible()) {
                    if (!iCodigoPieza.HasValue && string.IsNullOrEmpty(sCodigoBarras)) throw new Exception("Debe proporcionar un valor por lo menos para realizar la consulta.");
                    HHsvc.SCPP_HH proxy = ConfigDataAccess.ObtenerServiceProxy();
                    DataSet dsKardex = proxy.ObtenerKardexPieza(iCodigoPieza, true, sCodigoBarras);
                    if (dsKardex.Tables.Count < 2) { throw new Exception("No se pudo obtener la informacion de esta pieza"); }
                    if (dsKardex.Tables[0] == null || dsKardex.Tables[0].Rows.Count < 1) { throw new Exception("No se pudo obtener la informacion hacerca de esta pieza"); }
                    return dsKardex;
                }
                else throw new Exception("No se tiene conexion a la red, intente mas tarde.");
            }
            catch(Exception ex){throw ex;}
        }
    }
}
