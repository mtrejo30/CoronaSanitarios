using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Common.SolutionEntityFramework;
using BE = LAMOSA.SCPP.Server.BusinessEntity;
using BC = LAMOSA.SCPP.Server.BusinessComponent;
using System.IO;

namespace LAMOSA.SCPP.Server.ServiceHH
{

    // NOTE: If you change the class name "SCPP_HH" here, you must also update the reference to "SCPP_HH" in App.config.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SCPP_HH : ISCPP_HH
    {

        #region Fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion Fields

        #region Methods

        #region Constructors and Destructor

        public SCPP_HH()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~SCPP_HH()
        {

        }

        #endregion Constructors and Destructor

        #region Common

        // Control
        #region EstaServicioDisponible
        public bool EstaServicioDisponible()
        {
            Boolean bConnectionService = false;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                bConnectionService = bcObj.EstaServicioDisponible();
            }
            catch (Exception e)
            {
                throw e;
            }
            return bConnectionService;
        }
        #endregion EstaServicioDisponible

        // Acceso - Carga Datos
        #region ObtenerPlantas
        public DataTable ObtenerPlantas()
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerPlantas();
                DataSet beRes = new DataSet();
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                dt = beRes.Tables[0];
            }
            catch (Exception)
            {

            }
            return dt;
        }
        #endregion ObtenerPlantas
        #region ObtenerProcesos
        public DataTable ObtenerProcesos()
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerProcesos();
                DataSet beRes = new DataSet();
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                dt = beRes.Tables[0];
            }
            catch (Exception)
            {

            }
            return dt;
        }
        #endregion ObtenerProcesos

        // Acceso - Login
        #region Login
        public DataTable Login(String user, String password)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.Login(user, password);
                DataSet beRes = new DataSet();
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                dt = beRes.Tables[0];
            }
            catch (Exception)
            {

            }
            return dt;
        }
        #endregion Login

        // Acceso - Seleccion Planta
        #region ObtenerPlantasRol
        public DataTable ObtenerPlantasRol(int iCodRol)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerPlantasRol(iCodRol);
                DataSet beRes = new DataSet();
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                dt = beRes.Tables[0];
            }
            catch (Exception)
            {

            }
            return dt;
        }
        #endregion ObtenerPlantasRol
        #region ObtenerProcesosPorRol
        public DataTable ObtenerProcesosPorRol(int rol)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerProcesosPorRol(rol);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion

        // Acceso - Configuracion Inicial
        #region ObtenerTurnos
        public DataTable ObtenerTurnos()
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerTurnos();
                DataSet beRes = new DataSet();
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                dt = beRes.Tables[0];
            }
            catch (Exception)
            {

            }
            return dt;
        }
        #endregion ObtenerTurnos
        #region ObtenerProcesos2
        public DataTable ObtenerProcesos2()
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerProcesos2();
                DataSet beRes = new DataSet();
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                dt = beRes.Tables[0];
            }
            catch (Exception)
            {

            }
            return dt;
        }
        #endregion ObtenerProcesos2
        #region ObtenerPantallasProceso
        public DataTable ObtenerPantallasProceso(int iCodProceso)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerPantallasProceso(iCodProceso);
                DataSet beRes = new DataSet();
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                dt = beRes.Tables[0];
            }
            catch (Exception)
            {

            }
            return dt;
        }
        #endregion ObtenerPantallasProceso
        #region InsertarConfigHandHeld
        public long InsertarConfigHandHeld(int iCodUsuario, int iCodOperador, int iCodSupervisor,
                                            DateTime dtFecha, int iCodTurno, int iCodPlanta, int iCodProceso,
                                            int iCodConfigBanco, DateTime dtFechaRegistro)
        {
            long lRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                lRes = bcObj.InsertarConfigHandHeld(iCodUsuario, iCodOperador, iCodSupervisor, dtFecha, iCodTurno, iCodPlanta, iCodProceso, iCodConfigBanco, dtFechaRegistro);
            }
            catch (Exception)
            {
                lRes = -1;
            }
            return lRes;
        }
        #endregion InsertarConfigHandHeld
        #region ExisteInventarioProcesoActivo
        public int ExisteInventarioProcesoActivo()
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ExisteInventarioProcesoActivo();
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ExisteInventarioProcesoActivo

        // Varios - Captura Inicial
        #region ObtenerClaveEmpleadoMFG
        public int ObtenerClaveEmpleadoMFG(int empleado)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ObtenerClaveEmpleadoMFG(empleado);
            }
            catch (Exception e)
            {
            }
            return res;
        }
        #endregion ObtenerClaveEmpleadoMFG
        #region ObtenerSupervisorPorDefecto
        public DataTable ObtenerSupervisorPorDefecto(int empleado)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerSupervisorPorDefecto(empleado);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerSupervisorPorDefecto
        #region ValidarEmpleadoMFG
        public int ValidarEmpleadoMFG(int empleadoMFG)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ValidarEmpleadoMFG(empleadoMFG);
            }
            catch (Exception e)
            {
            }
            return res;
        }
        #endregion ValidarEmpleadoMFG
        #region ObtenerCentrosTrabajo
        public DataTable ObtenerCentrosTrabajo(int planta, int proceso)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerCentrosTrabajo(planta, proceso);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerCentrosTrabajo
        #region ObtenerMaquinas
        public DataTable ObtenerMaquinas(int planta, int proceso, int centroTrabajo)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerMaquinas(planta, proceso, centroTrabajo);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerMaquinas

        #region ObtenerPiezaCarroHornos
        public DataTable ObtenerPiezaCarroHornos(int planta, int carro)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerPiezaCarroHornos(planta, carro);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion
        //Vaciado
        #region ObtenerNumPosicionesBanco
        public int ObtenerNumPosicionesBanco(int configBanco)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ObtenerNumPosicionesBanco(configBanco);
            }
            catch (Exception e)
            {
            }
            return res;
        }
        #endregion ObtenerNumPosicionesBanco
        #region SuperoLimiteVaciadas
        public Boolean SuperoLimiteVaciadas(int configBanco)
        {
            Boolean res = false;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.SuperoLimiteVaciadas(configBanco);
            }
            catch (Exception e)
            {
            }
            return res;
        }
        #endregion SuperoLimiteVaciadas

        #region ObtenerPosicionesBanco
        public DataTable ObtenerPosicionesBanco(int configBanco)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerPosicionesBanco(configBanco);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerPosicionesBanco
        #region ObtenerArticulosMolde
        public DataTable ObtenerArticulosMolde(int molde)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerArticulosMolde(molde);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerArticulosMolde
        #region ObtenerColores
        public DataTable ObtenerColores()
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerColores();
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerColores
        #region ObtenerPruebas
        public DataTable ObtenerPruebas(int planta, int proceso)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerPruebas(planta, proceso);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerPruebas


        // Vaciado - Captura Vaciado
        #region ActualizarVaciadasAcumuladas
        public int ActualizarVaciadasAcumuladas(int iCodConfigBanco)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ActualizarVaciadasAcumuladas(iCodConfigBanco);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ActualizarVaciadasAcumuladas
        #region ActualizarVaciadasAcumuladas2
        public int ActualizarVaciadasAcumuladas2(int iCodConfigBanco, int cant)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ActualizarVaciadasAcumuladas2(iCodConfigBanco, cant);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ActualizarVaciadasAcumuladas2
        // Vaciado - Entrada Carro Secador


        // Secado - Entrada Carro Secador


        // Validaciones
        #region ObtenerCodPiezaCodBarras
        public int ObtenerCodPiezaCodBarras(string sCodBarras)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ObtenerCodPiezaCodBarras(sCodBarras);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ObtenerCodPiezaCodBarras
        #region EstaEnInventarioPocesoPieza
        public int EstaEnInventarioPocesoPieza(string sCodBarras)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.EstaEnInventarioPocesoPieza(sCodBarras);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion EstaEnInventarioPocesoPieza
        #region ObtenerEstadoPieza
        public SolutionEntityList<BE.HHEstadoPieza> ObtenerEstadoPieza(int iCodPieza)
        {
            SolutionEntityList<BE.HHEstadoPieza> l_Res = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                l_Res = bcObj.ObtenerEstadoPieza(iCodPieza);
            }
            catch (Exception ex)
            {
                l_Res = new SolutionEntityList<BE.HHEstadoPieza>();
                l_Res.ExceptionMessage = this.sClassName + ", ObtenerEstadoPieza: " + ex.Message;
            }
            return l_Res;
        }
        #endregion ObtenerEstadoPieza
        #region ObtenerUltimoProcesoPieza
        public SolutionEntityList<BE.HHProceso> ObtenerUltimoProcesoPieza(int iCodPieza)
        {
            SolutionEntityList<BE.HHProceso> l_Res = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                l_Res = bcObj.ObtenerUltimoProcesoPieza(iCodPieza);
            }
            catch (Exception ex)
            {
                l_Res = new SolutionEntityList<BE.HHProceso>();
                l_Res.ExceptionMessage = this.sClassName + ", ObtenerUltimoProcesoPieza: " + ex.Message;
            }
            return l_Res;
        }
        #endregion ObtenerUltimoProcesoPieza
        #region ObtenerPiezasCarro
        public SolutionEntityList<BE.HHPieza> ObtenerPiezasCarro(int iCodPlanta, int iCodProceso, int iCodCarro)
        {
            SolutionEntityList<BE.HHPieza> l_Res = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                l_Res = bcObj.ObtenerPiezasCarro(iCodPlanta, iCodProceso, iCodCarro);
            }
            catch (Exception ex)
            {
                l_Res = new SolutionEntityList<BE.HHPieza>();
                l_Res.ExceptionMessage = this.sClassName + ", ObtenerPiezasCarro: " + ex.Message;
            }
            return l_Res;
        }
        #endregion ObtenerPiezasCarro
        #region ObtenerCodModeloPieza
        public int ObtenerCodModeloPieza(int iCodPieza)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ObtenerCodModeloPieza(iCodPieza);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ObtenerCodModeloPieza
        #region ExistePiezaEnCarro
        public int ExistePiezaEnCarro(int iCodPlanta, int iCodProceso, int iCodPieza)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ExistePiezaEnCarro(iCodPlanta, iCodProceso, iCodPieza);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ExistePiezaEnCarro
        #region ObtenerCalidadPieza
        public SolutionEntityList<BE.HHCalidad> ObtenerCalidadPieza(int iCodPieza)
        {
            SolutionEntityList<BE.HHCalidad> l_Res = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                l_Res = bcObj.ObtenerCalidadPieza(iCodPieza);
            }
            catch (Exception ex)
            {
                l_Res = new SolutionEntityList<BE.HHCalidad>();
                l_Res.ExceptionMessage = this.sClassName + ", ObtenerCalidadPieza: " + ex.Message;
            }
            return l_Res;
        }
        #endregion ObtenerCalidadPieza
        #region ObtenerCodMoldePieza
        public int ObtenerCodMoldePieza(int iCodPieza)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ObtenerCodMoldePieza(iCodPieza);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ObtenerCodMoldePieza
        #region ObtenerDefectosPiezaProceso
        public SolutionEntityList<BE.HHDefecto> ObtenerDefectosPiezaProceso(int iCodPieza, int iCodProceso)
        {
            SolutionEntityList<BE.HHDefecto> l_Res = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                l_Res = bcObj.ObtenerDefectosPiezaProceso(iCodPieza, iCodProceso);
            }
            catch (Exception ex)
            {
                l_Res = new SolutionEntityList<BE.HHDefecto>();
                l_Res.ExceptionMessage = this.sClassName + ", ObtenerDefectosPiezaProceso: " + ex.Message;
            }
            return l_Res;
        }
        #endregion ObtenerDefectosPiezaProceso

        // Transacciones
        #region InsertarPieza
        public int InsertarPieza(int iCodPlanta, string sCodBarras, int iCodConfigBanco, int iCodConsecutivo,
                                    int iPosicion, int iCodArticulo, int iCodUltimoProceso, int iCodUltimoEstado, DateTime fechaRegistro, int iMolde, int iBase)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.InsertarPieza(iCodPlanta, sCodBarras, iCodConfigBanco, iCodConsecutivo,
                                            iPosicion, iCodArticulo, iCodUltimoProceso, iCodUltimoEstado, fechaRegistro, iMolde, iBase);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion InsertarPieza
        #region InsertarPiezaTransaccion
        public long InsertarPiezaTransaccion(long lCodConfigHandheld, int iCodPieza, DateTime dtFechaRegistro)
        {
            long lRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                lRes = bcObj.InsertarPiezaTransaccion(lCodConfigHandheld, iCodPieza, dtFechaRegistro);
            }
            catch (Exception)
            {
                lRes = -1;
            }
            return lRes;
        }
        #endregion InsertarPiezaTransaccion
        #region ActulizarUltimoProcesoPieza
        public int ActulizarUltimoProcesoPieza(int iCodPieza, int iCodUltimoProceso)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ActulizarUltimoProcesoPieza(iCodPieza, iCodUltimoProceso);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ActulizarUltimoProcesoPieza
        #region EliminarCarro
        public int EliminarCarro(int iCodPlanta, int iCodProceso, int iCodCarro)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.EliminarCarro(iCodPlanta, iCodProceso, iCodCarro);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion EliminarCarro
        #region ActualizarConfigHandHeld
        public int ActualizarConfigHandHeld(long lCodConfigHandHeld, int iCodSupervisor, int iCodOperador, int iCodConfigBanco)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ActualizarConfigHandHeld(lCodConfigHandHeld, iCodSupervisor, iCodOperador, iCodConfigBanco);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ActualizarConfigHandHeld
        #region InsertarCarroPieza
        public int InsertarCarroPieza(int iCodPlanta, int iCodProceso, int iCodCarro, int iCodPieza, DateTime dFechaRegistro)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.InsertarCarroPieza(iCodPlanta, iCodProceso, iCodCarro, iCodPieza, dFechaRegistro);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion InsertarCarroPieza
        #region InsertarPiezaTransaccionSecador
        public int InsertarPiezaTransaccionSecador(long lCodPiezaTransaccion, DateTime dtHoraInicio, double dHorasSecado)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.InsertarPiezaTransaccionSecador(lCodPiezaTransaccion, dtHoraInicio, dHorasSecado);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion InsertarPiezaTransaccionSecador
        #region ActualizarColorPieza
        public int ActualizarColorPieza(int iCodPieza, int iCodColor)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ActualizarColorPieza(iCodPieza, iCodColor);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ActualizarColorPieza
        #region InsertarCarroZonaPieza
        public int InsertarCarroZonaPieza(int iCodPlanta, int iCodPieza, int iCodCarro, string sCodZona)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.InsertarCarroZonaPieza(iCodPlanta, iCodPieza, iCodCarro, sCodZona);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion InsertarCarroZonaPieza
        #region InsertarPiezaDefecto
        public int InsertarPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto,
                                            int iCodEstadoDefecto, int iCodEmpleado, DateTime dtFechaUltimoMovimiento, DateTime dtFechaRegistro)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.InsertarPiezaDefecto(iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto,
                                                    iCodEstadoDefecto, iCodEmpleado, dtFechaUltimoMovimiento, dtFechaRegistro);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion InsertarPiezaDefecto
        #region EliminarPiezaDefecto
        public int EliminarPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.EliminarPiezaDefecto(iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion EliminarPiezaDefecto
        #region ActualizarPiezaDefecto
        public int ActualizarPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto,
                                            int iCodEstadoDefecto, int iCodEmpleado, DateTime dtFechaUltimoMovimiento)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ActualizarPiezaDefecto(iCodPieza, iCodProceso, iCodDefecto, iCodZonaDefecto,
                                                        iCodEstadoDefecto, iCodEmpleado, dtFechaUltimoMovimiento);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ActualizarPiezaDefecto
        #region ActualizarPiezaUltimoEstado
        public int ActualizarPiezaUltimoEstado(int iCodPieza, int iCodUltimoEstado)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ActualizarPiezaUltimoEstado(iCodPieza, iCodUltimoEstado);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ActualizarPiezaUltimoEstado

        // Auditoria
        #region ActualizarPiezaAuditada
        public int ActualizarPiezaAuditada(int iCodPieza, bool bAuditada)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ActualizarPiezaAuditada(iCodPieza, bAuditada);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ActualizarPiezaAuditada
        #region ObtenerPiezasTarima
        public SolutionEntityList<BE.HHTarimaPieza> ObtenerPiezasTarima(int iCodTarima)
        {
            SolutionEntityList<BE.HHTarimaPieza> l_Res = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                l_Res = bcObj.ObtenerPiezasTarima(iCodTarima);
            }
            catch (Exception ex)
            {
                l_Res = new SolutionEntityList<BE.HHTarimaPieza>();
                l_Res.ExceptionMessage = this.sClassName + ", ObtenerPiezasTarima: " + ex.Message;
            }
            return l_Res;
        }
        #endregion ObtenerPiezasTarima
        #region RechazarTarimaPieza
        public int RechazarTarimaPieza(int iCodTarima)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.RechazarTarimaPieza(iCodTarima);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion RechazarTarimaPieza
        #region ActualizarTarimaPaletizado
        public int ActualizarTarimaPaletizado(int iCodTarima, bool bPaletizado)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ActualizarTarimaPaletizado(iCodTarima, bPaletizado);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ActualizarTarimaPaletizado

        #region ExistePiezaEnTarima
        public int ExistePiezaEnTarima(int iPieza)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.ExistePiezaEnTarima(iPieza);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion ExistePiezaEnTarima
        #region InsertarTarimaPieza
        public int InsertarTarimaPieza(int iTarima, int iPieza, Boolean iPaletizado, Boolean iRechazado, DateTime FechaRegistro)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.InsertarTarimaPieza(iTarima, iPieza, iPaletizado, iRechazado, FechaRegistro);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion InsertarTarimaPieza

        // Reemplazo de Etiqueta
        #region InsertarEtiquetaReemplazo
        public int InsertarEtiquetaReemplazo(int iCodPlanta, string sCodBarras, int iCodModelo, int iCodColor,
                                                int iCodCalidad, int iCodUltimoProceso, int iCodUltimoEstado,
                                                long lCodConfigHandheld, DateTime dtFechaRegistro, int iCodProcesoPiezaReem)
        {
            int iRes = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                iRes = bcObj.InsertarEtiquetaReemplazo(iCodPlanta, sCodBarras, iCodModelo, iCodColor, iCodCalidad, iCodUltimoProceso,
                                                        iCodUltimoEstado, lCodConfigHandheld, dtFechaRegistro, iCodProcesoPiezaReem);
            }
            catch (Exception)
            {
                iRes = -1;
            }
            return iRes;
        }
        #endregion InsertarEtiquetaReemplazo

        // Sincronizacion
        public DateTime ObtenerFechaDepuracionHistoria()
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerFechaDepuracionHistoria();
            }
            catch
            {
                throw;
            }
            finally
            {
                bcObj = null;
            }
        }

        public DateTime ObtenerFechaDepuracionHistoria2(int iCodigoPlanta, int iCodigoProceso)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerFechaDepuracionHistoria(iCodigoPlanta, iCodigoProceso);
            }
            catch
            {
                throw;
            }
            finally
            {
                bcObj = null;
            }
        }

        #region Metodos para la Sincronizacion
        #region Actualizacion de datos
        public DataSet ActualizarCatalogos(string tabla, int planta, int proceso, DateTime fecha)
        {
            DataSet beRes = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                beRes = new DataSet();
                StringReader[] xmlDS = bcObj.ActualizarCatalogos(tabla, planta, proceso, fecha);
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
            }
            catch
            {
                beRes = new DataSet();
                //beRes.ExceptionMessage = this.sClassName + ", InsertarPieza: " + ex.Message;
            }
            return beRes;
        }
        public DataSet ActualizarTransacciones(string tabla, int planta, int proceso, DateTime fecha)
        {
            DataSet beRes = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                beRes = new DataSet();
                StringReader[] xmlDS = bcObj.ActualizarTransacciones(tabla, planta, proceso, fecha);
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
            }
            catch
            {
                beRes = new DataSet();
                //beRes.ExceptionMessage = this.sClassName + ", InsertarPieza: " + ex.Message;
            }
            return beRes;
        }

        #endregion

        public DataSet ActualizarCatalogosPorPiezas(DataTable piezas,string tabla, int proceso)
        {
            DataSet beRes = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                beRes = new DataSet();
                StringReader[] xmlDS = bcObj.ActualizarCatalogos(piezas,tabla, proceso);
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
            }
            catch
            {
                beRes = new DataSet();
                //beRes.ExceptionMessage = this.sClassName + ", InsertarPieza: " + ex.Message;
            }
            return beRes;
        }
       
        public DataSet SyncServHH(String NombreSPTabla, DateTime fechaIns, DateTime fechaUpd, DateTime fechaDel)
        {
            DataSet beRes = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                beRes = new DataSet();
                StringReader[] xmlDS = bcObj.SyncServHHSR(NombreSPTabla, fechaIns, fechaUpd, fechaDel);
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
            }
            catch (Exception ex)
            {
                beRes = new DataSet();
                //beRes.ExceptionMessage = this.sClassName + ", InsertarPieza: " + ex.Message;
            }
            return beRes;
        }
        public List<DataSet> SyncServHHLDS(DateTime fechaIns, DateTime fechaUpd, DateTime fechaDel)
        {

            List<DataSet> lds = new List<DataSet>();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                List<StringReader[]> lxmlDS = bcObj.SyncServHH(fechaIns, fechaUpd, fechaDel);
                foreach (StringReader[] xmlDS in lxmlDS)
                {
                    DataSet beRes = new DataSet();
                    beRes.ReadXmlSchema(xmlDS[0]);
                    beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                    lds.Add(beRes);
                }
            }
            catch (Exception ex)
            {
                //beRes.ExceptionMessage = this.sClassName + ", InsertarPieza: " + ex.Message;
            }
            return lds;
        }
        #region Carga las tablas de catalogos
        public DataTable TablasCatalogosHH()
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.TablasHH(-1, -1, -1);
                DataSet beRes = new DataSet();
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                dt = beRes.Tables[0];
            }
            catch (Exception ex)
            {
                //beRes.ExceptionMessage = this.sClassName + ", InsertarPieza: " + ex.Message;
            }
            return dt;
        }
        #endregion Carga las tablas de catalogos

        #region Carga las tablas necesarias para el proceso
        public DataTable TablasProcesoHH(int planta, int proceso, int pantalla)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.TablasHH(planta, proceso, pantalla);
                DataSet beRes = new DataSet();
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                dt = beRes.Tables[0];
            }
            catch (Exception ex)
            {
                //beRes.ExceptionMessage = this.sClassName + ", InsertarPieza: " + ex.Message;
            }
            return dt;
        }
        #endregion Carga las tablas necesarias para el proceso

        public int HHSyncCarroPiezaIns(int codPlanta, int codProceso, int codCarro, int codPieza, int codZona)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.HHSyncCarroPiezaQuemadoIns(codPlanta, codPieza, codCarro, codZona);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int HHSyncCarroPiezaQuemadoIns(int codPlanta, int codPieza, int codCarro, int codZona)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.HHSyncCarroPiezaQuemadoIns(codPlanta, codPieza, codCarro, codZona);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int HHSyncConfigHandHeldIns(int codTurno, int codUsuario, int codSupervisor, int codOperador, int codConfigBanco, int codProceso, int codPlanta)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.HHSyncConfigHandHeldIns(codTurno, codUsuario, codSupervisor, codOperador, codConfigBanco, codProceso, codPlanta);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int HHSyncPiezaIns(int codBarras, int codConfigBanco, int codConsecutivo, int posicion, int codArticulo, int codColor,
                                    int codCalidad, int codPlanta, int codUltimoEstado, int codUltimoProceso)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.HHSyncPiezaIns(codBarras, codConfigBanco, codConsecutivo, posicion, codArticulo, codColor,
                                    codCalidad, codPlanta, codUltimoEstado, codUltimoProceso);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int HHSyncPiezaDefectoIns(int codPieza, int codProceso, int codDefecto, int codZonaDefecto, int codEstadoDefecto,
                                        int codPiezaDefectoDet, int codPiezaDefectoDetalle, int codEmpleado)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.HHSyncPiezaDefectoIns(codPieza, codProceso, codDefecto, codZonaDefecto, codEstadoDefecto,
                                                codPiezaDefectoDet, codPiezaDefectoDetalle, codEmpleado);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int HHSyncPiezaReemplazoIns(int codPieza, int codProceso, int codPiezaAnterior)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.HHSyncPiezaReemplazoIns(codPieza, codProceso, codPiezaAnterior);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int HHSyncPiezaTransaccionIns(int codConfigHH, int CodPieza)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.HHSyncPiezaTransaccionIns(codConfigHH, CodPieza);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int HHSyncPiezaTransaccionSecadorIns(int codPiezaTranzaccion, DateTime horaInicio, float horasSecado)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.HHSyncPiezaTransaccionSecadorIns(codPiezaTranzaccion, horaInicio, horasSecado);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int BloquearUsuario(int codUsuario)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.BloquearUsuario(codUsuario);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int IncrementarContadorIntentos(int codUsuario)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.IncrementarContadorIntentos(codUsuario);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int ReiniciarContadorIntentos(int codUsuario)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ReiniciarContadorIntentos(codUsuario);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }

        #endregion Metodos para la Sincronizacion

        public int InsertarTarima(int codTarima, int codPieza)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.TarimaPiezaInsUpd(codTarima, codPieza, 2);//El numero 2 es para indicar ke es una insercion
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public int ObtenerPiezaInventario(int codPieza)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ObtenerPiezaInventario(codPieza);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        #region ActualizarPiezaInventario
        public int ActualizarPiezaInventario(int codPieza)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ActualizarPiezaInventario(codPieza);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        #endregion ActualizarPiezaInventario
        #region InsertarPiezaInventario
        public int InsertarPiezaInventario(String barras, int planta, int proceso, int configBanco, int consecutivo,
                                int posicion, int articulo, int color, int calidad, int ultimoEstado)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.PiezaInventarioIns(barras, planta, proceso, configBanco, consecutivo,
                                                posicion, articulo, color, calidad, ultimoEstado);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        #endregion InsertarPiezaInventario
        public int InsertaPiezaReemplazo(int codPieza, int codProceso, int codPiezaAnterior)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.InsertaPiezaReemplazo(codPieza, codProceso, codPiezaAnterior);
            }
            catch (Exception ex)
            {
                res = -1;
            }
            return res;
        }
        public Boolean ExisteCambioEnProceso(int proceso, int pantalla, DateTime fechaUltActualizacion)
        {
            Boolean isPrecesChanged = false;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                isPrecesChanged = bcObj.ExisteCambioEnProceso(proceso, pantalla, fechaUltActualizacion);
            }
            catch (Exception ex)
            {
            }
            return isPrecesChanged;
        }
        public DataSet EnvioDT(DataSet dsParameters)
        {
            DataSet dsRes = new DataSet();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = new StringReader[2];
                StringReader[] xmlDSRes = new StringReader[2];
                StringReader xmlSchema = new StringReader(dsParameters.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(dsParameters.GetXml().ToString());

                xmlDSRes = bcObj.EnvioDT(xmlDS);
                StringReader xmlSchemaRes = new StringReader(dsParameters.GetXmlSchema().ToString());
                StringReader xmlInfoRes = new StringReader(dsParameters.GetXml().ToString());

                dsRes.ReadXmlSchema(xmlDS[0]);
                dsRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRes;
        }

        #region InsertaError
        public void InsertaError(String parameters, String errorMessage)
        {
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                bcObj.InsertaError(parameters, errorMessage);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion InsertaError

        //Empaque
        #region ObtenerModelos2
        public DataTable ObtenerModelos2(int molde)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerModelos2(molde);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerModelos2

        #region ActualizarModeloPieza
        public int ActualizarModeloPieza(int pieza, int modelo)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ActualizarModeloPieza(pieza, modelo);
            }
            catch (Exception)
            {
            }
            return res;
        }
        #endregion ActualizarModeloPieza
        #region ActualizarCalidadPieza
        public int ActualizarCalidadPieza(int pieza, int calidad)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ActualizarCalidadPieza(pieza, calidad);
            }
            catch (Exception)
            {
            }
            return res;
        }
        #endregion ActualizarCalidadPieza
        //Defectos
        #region ObtenerDefectos
        public DataTable ObtenerDefectos(int iProceso)
        {
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerDefectos(iProceso);
            }
            catch (Exception e) { throw e; }
        }
        #endregion ObtenerDefectos

        #region ObtenerZonasDefecto
        public DataTable ObtenerZonasDefecto(int iTipoArticulo)
        {
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerZonasDefecto( iTipoArticulo);
            }
            catch (Exception e) { throw e; }
        }
        #endregion ObtenerZonasDefecto

        #region ObtenerEstadosDefecto
        public DataTable ObtenerEstadosDefecto(int estadoDefecto1, int estadoDefecto2)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerEstadosDefecto(estadoDefecto1, estadoDefecto2);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerEstadosDefecto

        //Varios
        #region InsertarPiezaTransaccion2
        //public DataTable InsertarPiezaTransaccion2(int empleado)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        BC.SCPP_HH bcObj = new BC.SCPP_HH();
        //        StringReader[] xmlDS = bcObj.InsertarPiezaTransaccion2(empleado);
        //        dt = ConvertToDatatable(xmlDS);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return dt;
        //}
        #endregion InsertarPiezaTransaccion2

        #region ObtenerCalidades
        public DataTable ObtenerCalidades()
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerCalidades();
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerCalidades

        #region ExisteModelo
        public int ExisteModelo(String claveArticulo)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ExisteModelo(claveArticulo);
            }
            catch (Exception)
            {
            }
            return res;
        }
        #endregion ExisteModelo

        #region ObtenerDesProceso
        public String ObtenerDesProceso(int proceso)
        {
            String desProceso = "";
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                desProceso = bcObj.ObtenerDesProceso(proceso);
            }
            catch (Exception)
            {
            }
            return desProceso;
        }
        #endregion ObtenerDesProceso

        #region ObtenerTiposModelo
        public DataTable ObtenerTiposModelo()
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerTiposModelo();
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerTiposModelo

        #region ObtenerModelos
        public DataTable ObtenerModelos(int tipoModelo)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerModelos(tipoModelo);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {
            }
            return dt;
        }
        #endregion ObtenerModelos

        #region ExisteModeloHastaRevisado
        public int ExisteModeloHastaRevisado(String claveArticulo)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ExisteModeloHastaRevisado(claveArticulo);
            }
            catch (Exception)
            {
            }
            return res;
        }
        #endregion ExisteModeloHastaRevisado

        #region ExisteModeloDesdeEsmaltado
        public int ExisteModeloDesdeEsmaltado(String claveArticulo)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.ExisteModeloDesdeEsmaltado(claveArticulo);
            }
            catch (Exception)
            {
            }
            return res;
        }
        #endregion ExisteModeloDesdeEsmaltado


        /////////////////////////////////////////
        #region ConvertToDatatable
        private DataTable ConvertToDatatable(StringReader[] xmlDS)
        {
            DataTable dt = new DataTable();
            try
            {
                DataSet beRes = new DataSet();
                beRes.ReadXmlSchema(xmlDS[0]);
                beRes.ReadXml(xmlDS[1], XmlReadMode.Auto);
                dt = beRes.Tables[0];
            }
            catch (Exception)
            {
                dt = new DataTable();
            }
            return dt;
        }
        #endregion ConvertToDatatable

        #region ObtenerModeloTipoPieza
        public DataTable ObtenerModeloTipoPieza(int articulo)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerModeloTipoPieza(articulo);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {

            }
            return dt;
        }
        #endregion ObtenerModeloTipoPieza
        #region ObtenerModeloTipoPieza2
        public DataTable ObtenerModeloTipoPieza2(int iCodPieza)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerModeloTipoPieza2(iCodPieza);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception)
            {

            }
            return dt;
        }
        #endregion ObtenerModeloTipoPieza2
        #region ObtenerColorPieza
        public SolutionEntityList<BE.HHColor> ObtenerColorPieza(int iCodPieza)
        {
            SolutionEntityList<BE.HHColor> l_Res = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                l_Res = bcObj.ObtenerColorPieza(iCodPieza);
                l_Res.ExceptionMessage = string.Empty;
            }
            catch (Exception ex)
            {
                l_Res = new SolutionEntityList<BE.HHColor>();
                l_Res.ExceptionMessage = this.sClassName + ", ObtenerColorPieza: " + ex.Message;
            }
            return l_Res;
        }
        #endregion ObtenerColorPieza
        #region ValidarPieza
        public SolutionEntityList<BE.HHValidarPieza> ValidarPieza(string sCodBarras, int iCodProcesoAct)
        {
            SolutionEntityList<BE.HHValidarPieza> l_Res = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                l_Res = bcObj.ValidarPieza(sCodBarras, iCodProcesoAct);
                l_Res.ExceptionMessage = string.Empty;
            }
            catch (Exception ex)
            {
                l_Res = new SolutionEntityList<BE.HHValidarPieza>();
                l_Res.ExceptionMessage = this.sClassName + ", ValidarPieza: " + ex.Message;
            }
            return l_Res;
        }
        #endregion ValidarPieza
        #region ValidarTarimaPieza
        public SolutionEntityList<BE.HHValidarPieza> ValidarTarimaPieza(int iTarima, int sCodPieza, int sCodPiezaPadre)
        {
            SolutionEntityList<BE.HHValidarPieza> l_Res = null;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                l_Res = bcObj.ValidarTarimaPieza(iTarima, sCodPieza, sCodPiezaPadre);
                l_Res.ExceptionMessage = string.Empty;
            }
            catch (Exception ex)
            {
                l_Res = new SolutionEntityList<BE.HHValidarPieza>();
                l_Res.ExceptionMessage = this.sClassName + ", ValidarTarimaPieza: " + ex.Message;
            }
            return l_Res;
        }
        #endregion
        #region ExisteModeloHastaRevisado
        public int DeleteTarimaPieza(int iTarima, int iPieza)
        {
            int res = -1;
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                res = bcObj.DeleteTarimaPieza(iTarima, iPieza);
            }
            catch (Exception)
            {
            }
            return res;
        }
        #endregion

        #region EsCasetaTanque
        public bool EsCasetaTanque(int iCodCaseta, int iCodTanque, int iCodProceso, int iCodPlanta)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.EsCasetaTanque(iCodCaseta, iCodTanque, iCodProceso, iCodPlanta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bcObj = null;
            }
        }
        #endregion EsCasetaTanque

        #region EsCasetaTanque
        public void ImprimirEtiqueta(BE.ConfigImpresora cConfigImpresora, BE.Etiqueta eEtiqueta)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                bcObj.ImprimirEtiqueta(cConfigImpresora, eEtiqueta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bcObj = null;
            }
        }
        #endregion

        #region ObtenerPiezas
        public DataTable ObtenerPieza(int iCodPieza)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerPieza(iCodPieza);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception e)
            {
                throw e;
            }
            return dt;
        }
        
        #endregion
        public DataTable ObtenerPiezasRequeme(int iCodPlanta)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerPiezasRequeme(iCodPlanta);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception e)
            {
                throw e;
            }
            return dt;
        }
        #region ProcesarBatchEsmaltadoPiezas
        public DataSet ProcesarBatchEsmaltadoPiezas(DataSet dsParameters)
        {
            DataSet dsRes = new DataSet();
            StringReader[] xmlDS = new StringReader[2];
            StringReader[] xmlDSRes = new StringReader[2];
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader xmlSchema = new StringReader(dsParameters.GetXmlSchema().ToString());
                StringReader xmlInfo = new StringReader(dsParameters.GetXml().ToString());
                xmlDS[0] = xmlSchema;
                xmlDS[1] = xmlInfo;
                xmlDSRes = bcObj.ProcesarBatchEsmaltadoPiezas(xmlDS);
                dsRes.ReadXmlSchema(xmlDSRes[0]);
                dsRes.ReadXml(xmlDSRes[1], XmlReadMode.Auto);
            }
            catch (Exception ex)
            {
                this.InsertaError("ProcesarBatchEsmaltadoPiezas", ex.Message);
                throw new Exception(this.sClassName + ", ProcesarBatchEsmaltadoPiezas: " + ex.Message);
            }
            return dsRes;
        }
        #endregion

        #endregion Common
        public DataSet ProcesarBatchVaciadoPieza(DataTable dtPieza, DataTable dtPiezaTransaccion, DataTable dtVaciadas, DataTable dtCarroPieza, DataTable dtPiezaDefecto, DataTable dtEstadoPieza, DataTable dtProcesoPieza, DataTable dtPuebaProceso)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ProcesarBatchVaciadoPieza(dtPieza, dtPiezaTransaccion, dtVaciadas, dtCarroPieza, dtPiezaDefecto, dtEstadoPieza, dtProcesoPieza, dtPuebaProceso);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                bcObj = null;
            }
        }
        public DataSet ProcesarBatchSecadoPieza(DataTable dtPieza, DataTable dtPiezaTransaccion, DataTable dtPiezaTransaccionSecador, DataTable dtCarroPieza)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ProcesarBatchSecadoPieza(dtPieza, dtPiezaTransaccion, dtPiezaTransaccionSecador, dtCarroPieza);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                bcObj = null;
            }
        }
        public DataSet ProcesarBatchRevisadoPieza(DataSet dsRevisado)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ProcesarBatchRevisadoPieza(dsRevisado);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                bcObj = null;
            }
        }
        public DataSet ProcesarBatchHornosPieza(DataSet dsHornos)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ProcesarBatchHornosPieza(dsHornos);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                bcObj = null;
            }
        }
        public DateTime ObtenerFechaServidor()
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerFechaServidor();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                bcObj = null;
            }
        }
        public int ObtenerConfigBancoCasetaTanque(int iCodMaquina, int iCodTanque, int iCodProceso, int iCodPlanta)
        {
            int iRes = -1;
            try
            {
                iRes = new BC.SCPP_HH().ObtenerConfigBancoCasetaTanque(iCodMaquina, iCodTanque, iCodProceso, iCodPlanta);
            }
            catch (Exception e)
            {
                throw e;
            }
            return iRes;
        }
        public DataTable ObtenerOperador(int iOperador)
        {
            DataTable dt = new DataTable();
            try
            {
                BC.SCPP_HH bcObj = new BC.SCPP_HH();
                StringReader[] xmlDS = bcObj.ObtenerOperador(iOperador);
                dt = ConvertToDatatable(xmlDS);
            }
            catch (Exception e)
            {
                throw e;
            }
            return dt;
        }
        public DataSet LogIns(DataSet dsLog)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.LogIns(dsLog);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public DataTable ObtenerTipoArticuloPieza(int iCodigoPieza)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerTipoArticuloPieza(iCodigoPieza);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public DataSet ObtenerKardexPieza(int? iCodigoPieza, string sCodigoBarras)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerKardexPieza(iCodigoPieza, sCodigoBarras);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }


        public DataTable ObtenerCarrosPendientesSecador(int iCodigoPlanta)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerCarrosPendientesSecador(iCodigoPlanta);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public DataTable ObtenerCarrosPendientesSecadorDetalle(int iCarro)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerCarrosPendientesSecadorDetalle(iCarro);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public DataTable ObtenerProduccion(int iCodigoOperador, int iCodigoProceso)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerProduccion(iCodigoOperador, iCodigoProceso);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public string ObtenerMensajeInicioSesion()
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerMensajeInicioSesion();
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public bool ValidarPoliticaContrasena(string contrasena)
        {
            try
            {
                return BC.SCPP_HH.ValidarPoliticaContrasena(contrasena);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CambiarPassword(string sUsuario, string sContrasenaAnterior, string sContrasenaNueva)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.CambiarPassword(sUsuario,sContrasenaAnterior, sContrasenaNueva);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public bool HabilitarImpresionEtiqueta(int CodigoPieza)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.HabilitarImpresionEtiqueta(CodigoPieza);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public bool TienePermisoReImpresion(int CodigoRol)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.TienePermisoReImpresion(CodigoRol);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public bool CerrarTarima(int iCodigoTarima)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.CerrarTarima(iCodigoTarima);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public int EnTarimarPieza(string sCodigoBarraPieza, int iCodigoMaquina, int iCodigoCentroTrabajo, bool bImprimeEtiqueta)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.EnTarimarPieza(sCodigoBarraPieza, iCodigoMaquina, iCodigoCentroTrabajo, bImprimeEtiqueta);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public int ObtenerTarimaPieza(string sCodigoBarraPieza)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerTarimaPieza(sCodigoBarraPieza);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public bool EsTarimaValida(int iCodigoTarima)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.EsTarimaValida(iCodigoTarima);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public bool AbrirTarima(int iCodigoTarima)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.AbrirTarima(iCodigoTarima);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public bool DesEnTarimar(int iCodigoPieza)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.DesEnTarimar(iCodigoPieza);
            }
            catch (Exception e) { throw e; }
            finally { bcObj = null; }
        }
        public bool EsValidaTarimaImportar(int iCodigoTarima)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.EsValidaTarimaImportar(iCodigoTarima);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bcObj = null;
            }
        }
        public DataTable ObtenerPiezaEnTarima(int iCodigoTarima)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerPiezaEnTarima(iCodigoTarima);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bcObj = null;
            }
        }
        public DataTable ObtenerTarima(int iCodigoTarima, bool AplicaFiltro, int iCodigoPlanta)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                int? iCodTatima;
                if (iCodigoTarima <= 0)
                    iCodTatima = null;
                else
                    iCodTatima = iCodigoTarima;
                return bcObj.ObtenerTarima(iCodTatima, AplicaFiltro, iCodigoPlanta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bcObj = null;
            }
        }
        public bool ImportarTarima(int iCodigoTarima01, int iCodigoTarima02, int iCodigoTarimaDestino)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ImportarTarima(iCodigoTarima01, iCodigoTarima02, iCodigoTarimaDestino);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally 
            {
                bcObj = null;
            }
        }
        public int EnTarimadoPieza(int iCodigoTarima, string sCodigoBarraPieza)
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.EnTarimadoPieza(iCodigoTarima, sCodigoBarraPieza);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bcObj = null;
            }
        }
        public int ObtenerTiempoEnMinutosCapturaColor()
        {
            BC.SCPP_HH bcObj = null;
            try
            {
                bcObj = new BC.SCPP_HH();
                return bcObj.ObtenerTiempoEnMinutosCapturaColor();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                bcObj = null;
            }
        }
        #endregion Methods
    }

}
