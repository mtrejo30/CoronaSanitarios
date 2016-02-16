using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Common.SolutionEntityFramework;
using BE = LAMOSA.SCPP.Server.BusinessEntity;

namespace LAMOSA.SCPP.Server.ServiceHH
{

    // NOTE: If you change the interface name "ISCPP_HH" here, you must also update the reference to "ISCPP_HH" in App.config.
    [ServiceContract]
    //[ServiceKnownType(typeof(BE.Calidad))]

    public interface ISCPP_HH
    {

        // TODO: Add your service operations here
        #region Methods

        #region Common

        // Control
        #region EstaServicioDisponible
        [OperationContract]
        bool EstaServicioDisponible();
        #endregion EstaServicioDisponible

        // Acceso - Carga Datos
        #region ObtenerPlantas
        [OperationContract]
        DataTable ObtenerPlantas();
        #endregion ObtenerPlantas
        #region ObtenerProcesos
        [OperationContract]
        DataTable ObtenerProcesos();
        #endregion ObtenerProcesos

        // Acceso - Login
        #region Login
        [OperationContract]
        DataTable Login(String user, String password);
        #endregion Login

        // Acceso - Seleccion Planta
        #region ObtenerPlantasRol
        [OperationContract]
        DataTable ObtenerPlantasRol(int iCodRol);
        #endregion ObtenerPlantasRol

        #region ObtenerProcesosPorRol
        [OperationContract]
        DataTable ObtenerProcesosPorRol(int Rol);
        #endregion

        // Acceso - Configuracion Inicial
        #region ObtenerTurnos
        [OperationContract]
        DataTable ObtenerTurnos();
        #endregion ObtenerTurnos
        #region ObtenerProcesos2
        [OperationContract]
        DataTable ObtenerProcesos2();
        #endregion ObtenerProcesos2
        #region ObtenerPantallasProceso
        [OperationContract]
        DataTable ObtenerPantallasProceso(int iCodProceso);
        #endregion ObtenerPantallasProceso
        #region InsertarConfigHandHeld
        [OperationContract]
        long InsertarConfigHandHeld(int iCodUsuario, int iCodOperador, int iCodSupervisor,
                                        DateTime dtFecha, int iCodTurno, int iCodPlanta, int iCodProceso, int iCodConfigBanco, DateTime dtFechaRegistro);
        #endregion InsertarConfigHandHeld
        #region ExisteInventarioProcesoActivo
        [OperationContract]
        int ExisteInventarioProcesoActivo();
        #endregion ExisteInventarioProcesoActivo

        // Varios - Captura Inicial
        #region ObtenerClaveEmpleadoMFG
        [OperationContract]
        int ObtenerClaveEmpleadoMFG(int empleado);
        #endregion ObtenerClaveEmpleadoMFG
        #region ObtenerSupervisorPorDefecto
        [OperationContract]
        DataTable ObtenerSupervisorPorDefecto(int empleado);
        #endregion ObtenerSupervisorPorDefecto
        #region ValidarEmpleadoMFG
        [OperationContract]
        int ValidarEmpleadoMFG(int empleadoMFG);
        #endregion ValidarEmpleadoMFG
        #region ObtenerCentrosTrabajo
        [OperationContract]
        DataTable ObtenerCentrosTrabajo(int planta, int proceso);
        #endregion ObtenerCentrosTrabajo
        #region ObtenerMaquinas
        [OperationContract]
        DataTable ObtenerMaquinas(int planta, int proceso, int centroTrabajo);
        #endregion ObtenerMaquinas
        #region ObtenerPosicionesBanco
        [OperationContract]
        DataTable ObtenerPosicionesBanco(int configBanco);
        #endregion ObtenerPosicionesBanco
        #region ObtenerArticulosMolde
        [OperationContract]
        DataTable ObtenerArticulosMolde(int molde);
        #endregion ObtenerArticulosMolde
        #region ObtenerColores
        [OperationContract]
        DataTable ObtenerColores();
        #endregion ObtenerColores
        #region ObtenerPruebas
        [OperationContract]
        DataTable ObtenerPruebas(int planta, int proceso);
        #endregion ObtenerPruebas
        #region ObtenerPiezaCarroHornos
        [OperationContract]
        DataTable ObtenerPiezaCarroHornos(int planta, int carro);
        #endregion


        // Vaciado - Captura Vaciado
        #region ActualizarVaciadasAcumuladas
        [OperationContract]
        int ActualizarVaciadasAcumuladas(int iCodConfigBanco);
        #endregion ActualizarVaciadasAcumuladas
        #region ObtenerNumPosicionesBanco
        [OperationContract]
        int ObtenerNumPosicionesBanco(int configBanco);
        #endregion ObtenerNumPosicionesBanco
        #region SuperoLimiteVaciadas
        [OperationContract]
        Boolean SuperoLimiteVaciadas(int configBanco);
        #endregion SuperoLimiteVaciadas
        #region ActualizarVaciadasAcumuladas2
        [OperationContract]
        int ActualizarVaciadasAcumuladas2(int iCodConfigBanco, int cant);
        #endregion ActualizarVaciadasAcumuladas2
        // Vaciado - Entrada Carro Secador


        // Secado - Entrada Carro Secador


        // Validaciones
        #region ObtenerCodPiezaCodBarras

        [OperationContract]
        int ObtenerCodPiezaCodBarras(string sCodBarras);

        #endregion ObtenerCodPiezaCodBarras
        #region ObtenerEstadoPieza

        [OperationContract]
        [ServiceKnownType(typeof(BE.HHEstadoPieza))]
        SolutionEntityList<BE.HHEstadoPieza> ObtenerEstadoPieza(int iCodPieza);

        #endregion ObtenerEstadoPieza
        #region ObtenerUltimoProcesoPieza

        [OperationContract]
        [ServiceKnownType(typeof(BE.HHProceso))]
        SolutionEntityList<BE.HHProceso> ObtenerUltimoProcesoPieza(int iCodPieza);

        #endregion ObtenerUltimoProcesoPieza
        #region ObtenerPiezasCarro

        [OperationContract]
        [ServiceKnownType(typeof(BE.HHPieza))]
        SolutionEntityList<BE.HHPieza> ObtenerPiezasCarro(int iCodPlanta, int iCodProceso, int iCodCarro);

        #endregion ObtenerPiezasCarro
        #region ObtenerCodModeloPieza

        [OperationContract]
        int ObtenerCodModeloPieza(int iCodPieza);

        #endregion ObtenerCodModeloPieza
        #region ExistePiezaEnCarro

        [OperationContract]
        int ExistePiezaEnCarro(int iCodPlanta, int iCodProceso, int iCodPieza);

        #endregion ExistePiezaEnCarro
        #region ObtenerCalidadPieza

        [OperationContract]
        [ServiceKnownType(typeof(BE.HHCalidad))]
        SolutionEntityList<BE.HHCalidad> ObtenerCalidadPieza(int iCodPieza);

        #endregion ObtenerCalidadPieza
        #region ObtenerCodMoldePieza

        [OperationContract]
        int ObtenerCodMoldePieza(int iCodPieza);

        #endregion ObtenerCodMoldePieza
        #region ObtenerDefectosPiezaProceso

        [OperationContract]
        [ServiceKnownType(typeof(BE.HHDefecto))]
        SolutionEntityList<BE.HHDefecto> ObtenerDefectosPiezaProceso(int iCodPieza, int iCodProceso);

        #endregion ObtenerDefectosPiezaProceso

        // Transacciones
        #region InsertarPieza

        [OperationContract]
        int InsertarPieza(int iCodPlanta, string sCodBarras, int iCodConfigBanco, int iCodConsecutivo,
                            int iPosicion, int iCodArticulo, int iCodUltimoProceso, int iCodUltimoEstado, DateTime fechaRegistro, int iMolde, int iBase);

        #endregion InsertarPieza
        #region InsertarPiezaTransaccion

        [OperationContract]
        long InsertarPiezaTransaccion(long lCodConfigHandheld, int iCodPieza, DateTime dtFechaRegistro);

        #endregion InsertarPiezaTransaccion
        #region ActulizarUltimoProcesoPieza

        [OperationContract]
        int ActulizarUltimoProcesoPieza(int iCodPieza, int iCodUltimoProceso);

        #endregion ActulizarUltimoProcesoPieza
        #region EliminarCarro

        [OperationContract]
        int EliminarCarro(int iCodPlanta, int iCodProceso, int iCodCarro);

        #endregion EliminarCarro
        #region ActualizarConfigHandHeld

        [OperationContract]
        int ActualizarConfigHandHeld(long lCodConfigHandHeld, int iCodSupervisor, int iCodOperador, int iCodConfigBanco);

        #endregion ActualizarConfigHandHeld
        #region InsertarCarroPieza

        [OperationContract]
        int InsertarCarroPieza(int iCodPlanta, int iCodProceso, int iCodCarro, int iCodPieza, DateTime dFechaRegistro);

        #endregion InsertarCarroPieza
        #region InsertarPiezaTransaccionSecador

        [OperationContract]
        int InsertarPiezaTransaccionSecador(long lCodPiezaTransaccion, DateTime dtHoraInicio, double dHorasSecado);

        #endregion InsertarPiezaTransaccionSecador
        #region ActualizarColorPieza

        [OperationContract]
        int ActualizarColorPieza(int iCodPieza, int iCodColor);

        #endregion ActualizarColorPieza
        #region InsertarCarroZonaPieza

        [OperationContract]
        int InsertarCarroZonaPieza(int iCodPlanta, int iCodPieza, int iCodCarro, string sCodZona);

        #endregion InsertarCarroZonaPieza
        #region InsertarPiezaDefecto

        [OperationContract]
        int InsertarPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto,
                                    int iCodEstadoDefecto, int iCodEmpleado, DateTime dtFechaUltimoMovimiento, DateTime dtFechaRegistro);

        #endregion InsertarPiezaDefecto
        #region EliminarPiezaDefecto

        [OperationContract]
        int EliminarPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto);

        #endregion EliminarPiezaDefecto
        #region ActualizarPiezaDefecto

        [OperationContract]
        int ActualizarPiezaDefecto(int iCodPieza, int iCodProceso, int iCodDefecto, int iCodZonaDefecto,
                                    int iCodEstadoDefecto, int iCodEmpleado, DateTime dtFechaUltimoMovimiento);

        #endregion ActualizarPiezaDefecto
        #region ActualizarPiezaUltimoEstado

        [OperationContract]
        int ActualizarPiezaUltimoEstado(int iCodPieza, int iCodUltimoEstado);

        #endregion ActualizarPiezaUltimoEstado

        // Auditoria
        #region ActualizarPiezaAuditada

        [OperationContract]
        int ActualizarPiezaAuditada(int iCodPieza, bool bAuditada);

        #endregion ActualizarPiezaAuditada
        #region ObtenerPiezasTarima

        [OperationContract]
        [ServiceKnownType(typeof(BE.HHTarimaPieza))]
        SolutionEntityList<BE.HHTarimaPieza> ObtenerPiezasTarima(int iCodTarima);

        #endregion ObtenerPiezasTarima
        #region RechazarTarimaPieza

        [OperationContract]
        int RechazarTarimaPieza(int iCodTarima);

        #endregion RechazarTarimaPieza
        #region ActualizarTarimaPaletizado

        [OperationContract]
        int ActualizarTarimaPaletizado(int iCodTarima, bool bPaletizado);

        #endregion ActualizarTarimaPaletizado

        #region ExistePiezaEnTarima
        [OperationContract]
        int ExistePiezaEnTarima(int iPieza);
        #endregion ExistePiezaEnTarima
        #region InsertarTarimaPieza
        [OperationContract]
        int InsertarTarimaPieza(int iTarima, int iPieza, Boolean iPaletizado, Boolean iRechazado, DateTime FechaRegistro);
        #endregion InsertarTarimaPieza


        // Reemplazo de Etiqueta
        #region InsertarEtiquetaReemplazo
        [OperationContract]
        int InsertarEtiquetaReemplazo(int iCodPlanta, string sCodBarras, int iCodModelo, int iCodColor,
                                        int iCodCalidad, int iCodUltimoProceso, int iCodUltimoEstado,
                                        long lCodConfigHandheld, DateTime dtFechaRegistro, int iCodProcesoPiezaReem);
        #endregion InsertarEtiquetaReemplazo

        // Sincronizacion
        [OperationContractAttribute]
        DateTime ObtenerFechaDepuracionHistoria();
        [OperationContractAttribute]
        DateTime ObtenerFechaDepuracionHistoria2(int iCodigoPlanta, int iCodigoProceso);

        #region Metodos para la Sincronizacion

        [OperationContract]
        DataSet SyncServHH(String NombreSPTabla, DateTime fechaIns, DateTime fechaUpd, DateTime fechaDel);

        [OperationContract]
        DataSet ActualizarCatalogos(string tabla, int planta, int proceso, DateTime fecha);

        [OperationContract]
        DataSet ActualizarCatalogosPorPiezas(DataTable piezas, string tabla, int proceso);

        [OperationContract]
        DataSet ActualizarTransacciones(string tabla, int planta, int proceso, DateTime fecha);

        [OperationContract]
        List<DataSet> SyncServHHLDS(DateTime fechaIns, DateTime fechaUpd, DateTime fechaDel);

        [OperationContract]
        DataTable TablasCatalogosHH();

        [OperationContract]
        DataTable TablasProcesoHH(int planta, int proceso, int pantalla);

        [OperationContract]
        int HHSyncCarroPiezaIns(int codPlanta,
                                int codProceso,
                                int codCarro,
                                int codPieza,
                                int codZona);

        [OperationContract]
        int HHSyncCarroPiezaQuemadoIns(int codPlanta, int codPieza, int codCarro, int codZona);

        [OperationContract]
        int HHSyncConfigHandHeldIns(int codTurno,
                                    int codUsuario,
                                    int codSupervisor,
                                    int codOperador,
                                    int codConfigBanco,
                                    int codProceso,
                                    int codPlanta);

        [OperationContract]
        int HHSyncPiezaIns(int codBarras,
                            int codConfigBanco,
                            int codConsecutivo,
                            int posicion,
                            int codArticulo,
                            int codColor,
                            int codCalidad,
                            int codPlanta,
                            int codUltimoEstado,
                            int codUltimoProceso);

        [OperationContract]
        int HHSyncPiezaReemplazoIns(int codPieza, int codProceso, int codPiezaAnterior);

        [OperationContract]
        int HHSyncPiezaDefectoIns(int codPieza,
                                    int codProceso,
                                    int codDefecto,
                                    int codZonaDefecto,
                                    int codEstadoDefecto,
                                    int codPiezaDefectoDet,
                                    int codPiezaDefectoDetalle,
                                    int codEmpleado);

        [OperationContract]
        int HHSyncPiezaTransaccionIns(int codConfigHH, int CodPieza);

        [OperationContract]
        int HHSyncPiezaTransaccionSecadorIns(int codPiezaTranzaccion, DateTime horaInicio, float horasSecado);

        [OperationContract]
        int BloquearUsuario(int codUsuario);

        [OperationContract]
        int IncrementarContadorIntentos(int codUsuario);

        [OperationContract]
        int ReiniciarContadorIntentos(int codUsuario);

        #endregion Metodos para la Sincronizacion
        #region InsertarTarima
        [OperationContract]
        int InsertarTarima(int codTarima, int codPieza);
        #endregion InsertarTarima
        #region ObtenerPiezaInventario
        [OperationContract]
        int ObtenerPiezaInventario(int codPieza);
        #endregion ObtenerPiezaInventario
        #region ActualizarPiezaInventario
        [OperationContract]
        int ActualizarPiezaInventario(int codPieza);
        #endregion ActualizarPiezaInventario
        #region InsertarPiezaInventario
        [OperationContract]
        int InsertarPiezaInventario(String barras, int planta, int proceso, int configBanco, int consecutivo,
                                int posicion, int articulo, int color, int calidad, int ultimoEstado);
        #endregion InsertarPiezaInventario
        #region InsertaPiezaReemplazo
        [OperationContract]
        int InsertaPiezaReemplazo(int codPieza, int codProceso, int codPiezaAnterior);
        #endregion InsertaPiezaReemplazo
        #region ExisteCambioEnProceso
        [OperationContract]
        Boolean ExisteCambioEnProceso(int proceso, int pantalla, DateTime fechaUltActualizacion);
        #endregion ExisteCambioEnProceso
        #region EnvioDT
        [OperationContract]
        DataSet EnvioDT(DataSet dt);
        #endregion EnvioDT
        #region InsertaError
        [OperationContract]
        void InsertaError(String parameters, String errorMessage);
        #endregion InsertaError


        /////////////////////////////
        //Empaque
        #region ObtenerModelos2
        [OperationContract]
        DataTable ObtenerModelos2(int molde);
        #endregion ObtenerModelos2

        #region ActualizarModeloPieza
        [OperationContract]
        int ActualizarModeloPieza(int pieza, int modelo);
        #endregion ActualizarModeloPieza

        #region ActualizarCalidadPieza
        [OperationContract]
        int ActualizarCalidadPieza(int pieza, int calidad);
        #endregion ActualizarCalidadPieza

        //Defectos
        #region ObtenerDefectos
        [OperationContract]
        DataTable ObtenerDefectos(int iProceso);
        #endregion ObtenerDefectos

        #region ObtenerZonasDefecto
        [OperationContract]
        DataTable ObtenerZonasDefecto(int iTipoArticulo);
        #endregion ObtenerZonasDefecto

        #region ObtenerEstadosDefecto
        [OperationContract]
        DataTable ObtenerEstadosDefecto(int estadoDefecto1, int estadoDefecto2);
        #endregion ObtenerEstadosDefecto

        //Varios
        //#region InsertarPiezaTransaccion2
        //[OperationContract]
        //DataTable InsertarPiezaTransaccion2(String user, String password);
        //#endregion InsertarPiezaTransaccion2

        #region ObtenerCalidades
        [OperationContract]
        DataTable ObtenerCalidades();
        #endregion ObtenerCalidades

        #region ExisteModelo
        [OperationContract]
        int ExisteModelo(String claveArticulo);
        #endregion ExisteModelo

        #region ObtenerDesProceso
        [OperationContract]
        String ObtenerDesProceso(int proceso);
        #endregion ObtenerDesProceso

        #region ObtenerTiposModelo
        [OperationContract]
        DataTable ObtenerTiposModelo();
        #endregion ObtenerTiposModelo

        #region ObtenerModelos
        [OperationContract]
        DataTable ObtenerModelos(int tipoModelo);
        #endregion ObtenerModelos

        #region ExisteModeloHastaRevisado
        [OperationContract]
        int ExisteModeloHastaRevisado(String claveArticulo);
        #endregion ExisteModeloHastaRevisado

        #region ExisteModeloDesdeEsmaltado
        [OperationContract]
        int ExisteModeloDesdeEsmaltado(String claveArticulo);
        #endregion ExisteModeloDesdeEsmaltado

        #region EstaEnInventarioPocesoPieza
        [OperationContract]
        int EstaEnInventarioPocesoPieza(String claveArticulo);
        #endregion EstaEnInventarioPocesoPieza

        /////////////////////////////////////////
        #region ObtenerModeloTipoPieza
        [OperationContract]
        DataTable ObtenerModeloTipoPieza(int articulo);
        #endregion ObtenerModeloTipoPieza
        #region ObtenerModeloTipoPieza2
        [OperationContract]
        DataTable ObtenerModeloTipoPieza2(int iCodPieza);
        #endregion ObtenerModeloTipoPieza2
        #region ObtenerColorPieza
        [OperationContract]
        [ServiceKnownType(typeof(BE.HHColor))]
        SolutionEntityList<BE.HHColor> ObtenerColorPieza(int iCodPieza);
        #endregion ObtenerColorPieza
        #region ValidarPieza
        [OperationContract]
        [ServiceKnownType(typeof(BE.HHValidarPieza))]
        SolutionEntityList<BE.HHValidarPieza> ValidarPieza(string sCodBarras, int iCodProcesoAct);
        #endregion ValidarPieza
        #region ValidarTarimaPieza
        [OperationContract]
        [ServiceKnownType(typeof(BE.HHValidarPieza))]
        SolutionEntityList<BE.HHValidarPieza> ValidarTarimaPieza(int iTarima, int sCodPieza, int sCodPiezaPadre);
        #endregion
        #region DeleteTarimaPieza
        [OperationContract]
        int DeleteTarimaPieza(int iTarima, int iPieza);
        #endregion

        #region EsCasetaTanque
        [OperationContract]
        bool EsCasetaTanque(int iCodCaseta, int iCodTanque, int iCodProceso, int iCodPlanta);
        #endregion EsCasetaTanque
        #region ImprimirEtiqueta
        [OperationContract]
        [ServiceKnownType(typeof(BE.ConfigImpresora)), ServiceKnownType(typeof(BE.Etiqueta)), ServiceKnownType(typeof(BE.Campo)), ServiceKnownType(typeof(BE.Enums.TipoEtiqueta))]
        void ImprimirEtiqueta(BE.ConfigImpresora cConfigImpresora, BE.Etiqueta eEtiqueta);
        #endregion

        #region ObtenerPiezas
        [OperationContract]
        DataTable ObtenerPieza(int iCodPieza);

        [OperationContract]
        DataTable ObtenerPiezasRequeme(int iCodPlanta);
        #endregion
        #region
        [OperationContract]
        DataSet ProcesarBatchEsmaltadoPiezas(DataSet dsParameters);
        #endregion
        #endregion Common
        [OperationContract]
        DataSet ProcesarBatchVaciadoPieza(DataTable dtPieza, DataTable dtPiezaTransaccion, DataTable dtVaciadas, DataTable dtCarroPieza, DataTable dtPiezaDefecto, DataTable dtEstadoPieza, DataTable dtProcesoPieza, DataTable dtPuebaProceso);
        [OperationContract]
        DataSet ProcesarBatchSecadoPieza(DataTable dtPieza, DataTable dtPiezaTransaccion, DataTable dtPiezaTransaccionSecador, DataTable dtCarroPieza);
        [OperationContract]
        DateTime ObtenerFechaServidor();
        [OperationContract]
        DataSet ProcesarBatchRevisadoPieza(DataSet dsRevisado);
        [OperationContract]
        DataSet ProcesarBatchHornosPieza(DataSet dsHornos);
        [OperationContract]
        int ObtenerConfigBancoCasetaTanque(int iCodMaquina, int iCodTanque, int iCodProceso, int iCodPlanta);
        [OperationContract]
        DataTable ObtenerOperador(int iOperador);
        [OperationContract]
        DataSet LogIns(DataSet dsLog);
        [OperationContract]
        DataTable ObtenerTipoArticuloPieza(int iCodigoPieza);
        [OperationContract]
        DataSet ObtenerKardexPieza(int? iCodigoPieza, string sCodigoBarras);
        [OperationContract]
        DataTable ObtenerCarrosPendientesSecador(int iCodigoPlanta);
        [OperationContract]
        DataTable ObtenerCarrosPendientesSecadorDetalle(int iCarro);
        [OperationContract]
        DataTable ObtenerProduccion(int iCodigoOperador, int iCodigoProceso);
        [OperationContract]
        string ObtenerMensajeInicioSesion();
        [OperationContract]
        bool ValidarPoliticaContrasena(string contrasena);
        [OperationContract]
        string CambiarPassword(string sUsuario, string sContrasenaAnterior, string sContrasenaNueva);
        [OperationContract]
        bool HabilitarImpresionEtiqueta(int CodigoPieza);
        [OperationContract]
        bool TienePermisoReImpresion(int CodigoRol);
        [OperationContract]
        bool CerrarTarima(int iCodigoTarima);
        [OperationContract]
        int EnTarimarPieza(string sCodigoBarraPieza, int iCodigoMaquina, int iCodigoCentroTrabajo, bool bImprimeEtiqueta);
        [OperationContract]
        int ObtenerTarimaPieza(string sCodigoBarraPieza);
        [OperationContract]
        bool EsTarimaValida(int iCodigoTarima);
        [OperationContract]
        bool AbrirTarima(int iCodigoTarima);
        [OperationContract]
        bool DesEnTarimar(int iCodigoPieza);
        [OperationContract]
        bool ImportarTarima(int iCodigoTarima01, int iCodigoTarima02, int iCodigoTarimaDestino);
        [OperationContract]
        DataTable ObtenerTarima(int iCodigoTarima, bool AplicaFiltro, int iCodigoPlanta);
        [OperationContract]
        DataTable ObtenerPiezaEnTarima(int iCodigoTarima);
        [OperationContract]
        bool EsValidaTarimaImportar(int iCodigoTarima);
        [OperationContract]
        int EnTarimadoPieza(int iCodigoTarima, string sCodigoBarraPieza);
        [OperationContract]
        int ObtenerTiempoEnMinutosCapturaColor();
        #endregion Methods

    }

}
