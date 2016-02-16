using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Common.SolutionEntityFramework;
using BE = LAMOSA.SCPP.Server.BusinessEntity;
using LAMOSA.SCPP.Server.BusinessEntity;

namespace LAMOSA.SCPP.Server.Service
{

    // NOTE: If you change the interface name "ISCPP" here, you must also update the reference to "ISCPP" in App.config.
    [ServiceContract]
    [ServiceKnownType(typeof(BE.Calidad)), ServiceKnownType(typeof(Maquina))]

    public interface ISCPP
    {

        // TODO: Add your service operations here
        #region Methods

        #region Common

        #region ObtenerTurnos

        [OperationContract]
        [ServiceKnownType(typeof(BE.Turno))]
        SolutionEntityList<BE.Turno> ObtenerTurnos();

        #endregion ObtenerTurnos
        #region ObtenerCalidades

        [OperationContract]
        [ServiceKnownType(typeof(BE.Calidad))]
        SolutionEntityList<BE.Calidad> ObtenerCalidades();

        #endregion ObtenerCalidades
        #region ObtenerCalidadesCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.Calidad))]
        SolutionEntityList<BE.Calidad> ObtenerCalidadesCbo();

        #endregion ObtenerCalidadesCbo
        #region ObtenerPlantaCalidadCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.PlantaCalidad))]
        SolutionEntityList<BE.PlantaCalidad> ObtenerPlantaCalidadCbo(int CodPlanta);

        #endregion ObtenerPlantaCalidadCbo

        #region ObtenerDefectosCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.DefectoCbo))]
        SolutionEntityList<BE.DefectoCbo> ObtenerDefectosCbo(int CodProceso, int CodTipoDefecto);

        #endregion ObtenerDefectosCbo

        #region ObtenerZonaDefectoCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.ZonaDefectoCbo))]
        SolutionEntityList<BE.ZonaDefectoCbo> ObtenerZonaDefectoCbo();

        #endregion ObtenerZonaDefectoCbo


        #region ObtenerColores

        [OperationContract]
        [ServiceKnownType(typeof(BE.Color))]
        SolutionEntityList<BE.Color> ObtenerColores();

        #endregion ObtenerColores
        #region ObtenerArticulos

        [OperationContract]
        [ServiceKnownType(typeof(BE.Articulo))]
        [ServiceKnownType(typeof(BE.ArticuloPars))]
        SolutionEntityList<BE.Articulo> ObtenerArticulos(BE.ArticuloPars articuloPars);

        #endregion ObtenerArticulos
        #region ObtenerArticulosCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.ArticuloCbo))]
        SolutionEntityList<BE.ArticuloCbo> ObtenerArticulosCbo(int tipoArticulo);

        #endregion
        #region ObtenerModelosCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.ArticuloCbo))]
        SolutionEntityList<BE.ArticuloCbo> ObtenerModelosCbo(int tipoArticulo);

        #endregion
        #region ObtenerTiposArticuloCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.TipoArticuloCbo))]
        SolutionEntityList<BE.TipoArticuloCbo> ObtenerTiposArticuloCbo();

        #endregion ObtenerTiposArticuloCbo
        #region ObtenerMoldesCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.MoldeCbo))]
        [ServiceKnownType(typeof(BE.ArticuloPars))]
        SolutionEntityList<BE.MoldeCbo> ObtenerMoldesCbo(BE.ArticuloPars articuloPars);

        #endregion ObtenerMoldesCbo
        #region ObtenerMoldes

        [OperationContract]
        [ServiceKnownType(typeof(BE.Molde))]
        [ServiceKnownType(typeof(BE.ArticuloPars))]
        SolutionEntityList<BE.Molde> ObtenerMoldes(BE.ArticuloPars articuloPars);

        #endregion ObtenerMoldes
        #region Login

        [OperationContract]
        [ServiceKnownType(typeof(BE.LoginUsuario))]
        BE.LoginUsuario Login(BE.LoginUsuario lu);

        #endregion Login

        #region ObtenerPruebas

        [OperationContract]
        [ServiceKnownType(typeof(BE.Prueba))]
        SolutionEntityList<BE.Prueba> ObtenerPruebas(int Proceso);

        #endregion ObtenerPruebas
        #region ObtenerDefectos

        [OperationContract]
        [ServiceKnownType(typeof(BE.Defecto))]
        SolutionEntityList<BE.Defecto> ObtenerDefectos(int CodProceso, int CodPlanta);

        #endregion ObtenerDefectos
        #region ObtenerTiposDefecto

        [OperationContract]
        [ServiceKnownType(typeof(BE.TipoDefecto))]
        SolutionEntityList<BE.TipoDefecto> ObtenerTiposDefecto();

        #endregion ObtenerTiposDefecto
        #region ObtenerZonaDefecto

        [OperationContract]
        [ServiceKnownType(typeof(BE.ZonaDefecto))]
        SolutionEntityList<BE.ZonaDefecto> ObtenerZonaDefecto(int CodTipoArticulo);

        #endregion ObtenerZonaDefecto
        #region ObtenerAlmacen

        [OperationContract]
        [ServiceKnownType(typeof(BE.Almacen))]
        SolutionEntityList<BE.Almacen> ObtenerAlmacen();

        #endregion ObtenerAlmacen
        #region ObtenerAlmacenCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.Almacen))]
        SolutionEntityList<BE.Almacen> ObtenerAlmacenCbo();

        #endregion ObtenerAlmacenCbo

        #region ObtenerPlanta

        [OperationContract]
        [ServiceKnownType(typeof(BE.Planta))]
        SolutionEntityList<BE.Planta> ObtenerPlanta(int Almacen);

        #endregion ObtenerPlanta

        #region ObtenerCentroTrabajo

        [OperationContract]
        [ServiceKnownType(typeof(BE.CentroTrabajo))]
        SolutionEntityList<BE.CentroTrabajo> ObtenerCentroTrabajo(int Planta, int Proceso);

        #endregion ObtenerCentroTrabajo
        #region ObtenerCentroTrabajoCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.CentroTrabajo))]
        SolutionEntityList<BE.CentroTrabajo> ObtenerCentroTrabajoCbo(int Planta, int Proceso);

        #endregion ObtenerCentroTrabajoCbo
        #region ObtenerArea

        [OperationContract]
        [ServiceKnownType(typeof(BE.Area))]
        SolutionEntityList<BE.Area> ObtenerArea(int CT);

        #endregion ObtenerArea


        #region ObtenerEstructuraPlanta

        [OperationContract]
        [ServiceKnownType(typeof(BE.EstructuraPlanta))]
        SolutionEntityList<BE.EstructuraPlanta> ObtenerEstructuraPlanta(int Planta);

        #endregion ObtenerEstructuraPlanta
        #region ObtenerProcesoCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.ProcesoCbo))]
        SolutionEntityList<BE.ProcesoCbo> ObtenerProcesoCbo(int Planta);

        #endregion

        #region ObtenerProceso

        [OperationContract]
        [ServiceKnownType(typeof(BE.Proceso))]
        SolutionEntityList<BE.Proceso> ObtenerProceso();

        #endregion

        #region ObtenerRutaProceso

        [OperationContract]
        [ServiceKnownType(typeof(BE.RutaProceso))]
        SolutionEntityList<BE.RutaProceso> ObtenerRutaProceso(int Planta);

        #endregion
        #region ObtenerRol

        [OperationContract]
        [ServiceKnownType(typeof(BE.Rol))]
        SolutionEntityList<BE.Rol> ObtenerRol();

        #endregion
        #region ObtenerRolCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.Rol))]
        SolutionEntityList<BE.Rol> ObtenerRolCbo();

        #endregion
        #region ObtenerEmpleado

        [OperationContract]
        [ServiceKnownType(typeof(BE.Empleado))]
        SolutionEntityList<BE.Empleado> ObtenerEmpleado(int CodPlanta, int CodPuesto);

        #endregion
        #region ObtenerPlantaCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.PlantaCbo))]
        SolutionEntityList<BE.PlantaCbo> ObtenerPlantaCbo();

        #endregion
        #region ObtenerMaquinaCbo

        [OperationContract]
        [ServiceKnownType(typeof(BE.MaquinaCbo))]
        SolutionEntityList<BE.MaquinaCbo> ObtenerMaquinaCbo(int codArea, int codCT);
        [OperationContract]
        [ServiceKnownType(typeof(BE.MaquinaCbo))]
        SolutionEntityList<BE.MaquinaCbo> ObtenerMaquinas(int codigoArea, int codigoCentroTrabajo, int codigoPlanta, int codigoProceso);

        #endregion
        #region ObtenerMaquina

        [OperationContract]
        [ServiceKnownType(typeof(BE.Maquina))]
        SolutionEntityList<BE.Maquina> ObtenerMaquina(int codPlanta, int codProceso);

        #endregion
        #region ObtenerPuesto

        [OperationContract]
        [ServiceKnownType(typeof(BE.Puesto))]
        SolutionEntityList<BE.Puesto> ObtenerPuesto();

        #endregion
        #region ObtenerUsuarios

        [OperationContract]
        [ServiceKnownType(typeof(BE.Usuario))]
        SolutionEntityList<BE.Usuario> ObtenerUsuarios(int Planta, int Rol, string Usuario);

        #endregion
        #region ObtenerEmpleadoBusqueda

        [OperationContract]
        [ServiceKnownType(typeof(BE.EmpleadoBusqueda))]
        SolutionEntityList<BE.EmpleadoBusqueda> ObtenerEmpleadoBusqueda(int CodEmpleado, string NombreEmpleado, int Rol);

        #endregion
        #region ObtenerMFGEmpleado
        [OperationContract]
        string ObtenerMFGEmpleado(int CodEmpleado);
        #endregion
        #region ObtenerCondicionEsmalte

        [OperationContract]
        [ServiceKnownType(typeof(BE.CondicionEsmalte))]
        SolutionEntityList<BE.CondicionEsmalte> ObtenerCondicionEsmalte(int CodPlanta, DateTime FechaInicio, DateTime FechaFin);

        #endregion
        #region ObtenerCondicionOperacion

        [OperationContract]
        [ServiceKnownType(typeof(BE.CondicionOperacion))]
        SolutionEntityList<BE.CondicionOperacion> ObtenerCondicionOperacion(int CodPlanta, int CodProceso, int CodArea, DateTime FechaInicio, DateTime FechaFin);

        #endregion
        #region ObtenerCondicionPasta

        [OperationContract]
        [ServiceKnownType(typeof(BE.CondicionPasta))]
        SolutionEntityList<BE.CondicionPasta> ObtenerCondicionPasta(int CodPlanta, DateTime FechaInicio, DateTime FechaFin);

        #endregion
        #region ObtenerCondicionPastaExportar

        [OperationContract]
        DataTable ObtenerCondicionPastaExportar(int CodPlanta, DateTime FechaInicio, DateTime FechaFin);

        #endregion
        #region ObtenerCondicionEsmalteExportar
        [OperationContract]
        DataTable ObtenerCondicionEsmalteExportar(int CodPlanta, DateTime FechaInicio, DateTime FechaFin);
        #endregion
        #region ObtenerConfiguracionBancoDetalle

        [OperationContract]
        [ServiceKnownType(typeof(BE.ConfigBancoDetalle))]
        SolutionEntityList<BE.ConfigBancoDetalle> ObtenerConfiguracionBancoDetalle(int CodConfigBanco);

        #endregion
        #region ObtenerConfiguracionBanco

        [OperationContract]
        [ServiceKnownType(typeof(BE.ConfigBancos))]
        SolutionEntityList<BE.ConfigBancos> ObtenerConfiguracionBanco(int planta, int ct, int maquina, int activo);

        #endregion
        #region ObtenerInventarioEnProceso

        [OperationContract]
        [ServiceKnownType(typeof(BE.InventarioEnProceso))]
        SolutionEntityList<BE.InventarioEnProceso> ObtenerInventarioEnProceso(int CodAlmacen, int CodPlanta, int CodProceso, int CodTipoArticulo, int CodArticulo, DateTime FechaInicio, DateTime FechaFin, int Opcion);

        #endregion
        #region ObtenerInventario

        [OperationContract]
        [ServiceKnownType(typeof(BE.Inventario))]
        SolutionEntityList<BE.Inventario> ObtenerInventario(int CodPlanta, DateTime FechaInicio, DateTime FechaFin);

        #endregion
        #region ObtenerPiezasReemplazo

        [OperationContract]
        [ServiceKnownType(typeof(BE.PiezaReemplazo))]
        SolutionEntityList<BE.PiezaReemplazo> ObtenerPiezasReemplazo(bool SinPiezasreemplazadas);

        #endregion

        #region ObtenerConfig

        [OperationContract]
        [ServiceKnownType(typeof(BE.Configuracion))]
        SolutionEntityList<BE.Configuracion> ObtenerConfig(int CodConfiguracion);

        #endregion

        #region ObtenerRepCapInstalada

        [OperationContract]
        [ServiceKnownType(typeof(BE.RepCapInstalada))]
        SolutionEntityList<BE.RepCapInstalada> ObtenerRepCapInstalada(int Opcion, int Planta, int CT, int Banco, int TipoArt, int Molde);

        #endregion
        #region ObtenerMetasProd

        [OperationContract]
        [ServiceKnownType(typeof(BE.MetasProd))]
        SolutionEntityList<BE.MetasProd> ObtenerMetasProd(int CodPlanta, DateTime FechaInicio, DateTime FechaFin);

        #endregion
        #region ObtenerKardexProductoBusqueda

        [OperationContract]
        [ServiceKnownType(typeof(BE.KardexProductoBusqueda))]
        SolutionEntityList<BE.KardexProductoBusqueda> ObtenerKardexProductoBusqueda(string Codigo);

        #endregion
        #region ObtenerKardexProducto

        [OperationContract]
        [ServiceKnownType(typeof(BE.KardexProducto))]
        SolutionEntityList<BE.KardexProducto> ObtenerKardexProducto(int Codigo);

        #endregion
        #region ObtenerKardexProductoDefecto

        [OperationContract]
        [ServiceKnownType(typeof(BE.KardexProductoDefecto))]
        SolutionEntityList<BE.KardexProductoDefecto> ObtenerKardexProductoDefecto(int Codigo);

        #endregion

        #region GuardarPlanta

        [OperationContract]
        [ServiceKnownType(typeof(BE.Planta))]
        void GuardarPlanta(BE.Planta Planta);

        #endregion
        #region GuardarPrueba

        [OperationContract]
        [ServiceKnownType(typeof(BE.Prueba))]
        void GuardarPrueba(BE.Prueba Prueba, int iCodPlanta);

        #endregion
        #region GuardarUsuario

        [OperationContract]
        [ServiceKnownType(typeof(BE.Usuario))]
        BE.Usuario GuardarUsuario(BE.Usuario Usuario);

        #endregion
        #region GuardarTurno

        [OperationContract]
        [ServiceKnownType(typeof(BE.Turno))]
        void GuardarTurno(BE.Turno Turno);

        #endregion
        #region GuardarCondicionPasta

        [OperationContract]
        [ServiceKnownType(typeof(BE.CondicionPasta))]
        void GuardarCondicionPasta(BE.CondicionPasta CP);

        #endregion
        #region GuardarCondicionOperacion

        [OperationContract]
        [ServiceKnownType(typeof(BE.CondicionOperacionGuarda))]
        void GuardarCondicionOperacion(BE.CondicionOperacionGuarda CO);

        #endregion
        #region GuardaRol

        [OperationContract]
        [ServiceKnownType(typeof(BE.rolplanta))]
        void GuardaRol(BE.rolplanta RolPlanta);

        #endregion
        #region GuardarConfig

        [OperationContract]
        [ServiceKnownType(typeof(BE.Configuracion))]
        void GuardarConfig(BE.Configuracion Conf);

        #endregion
        #region GuardarCondicionEsmalte

        [OperationContract]
        [ServiceKnownType(typeof(BE.CondicionEsmalte))]
        void GuardarCondicionEsmalte(BE.CondicionEsmalte CE);

        #endregion
        #region GuardarConfigBanco

        [OperationContract]
        [ServiceKnownType(typeof(BE.ConfigBancoResgistro))]
        int GuardarConfigBanco(BE.ConfigBancoResgistro CB);

        #endregion
        //#region GuardarConfigBancoDetalle

        //[OperationContract]
        //[ServiceKnownType(typeof(BE.ConfigBancoDetalle))]
        //void GuardarConfigBancoDetalle(BE.ConfigBancoDetalle CBD);

        //#endregion
        #region GuardaRutaProceso

        [OperationContract]
        [ServiceKnownType(typeof(BE.RutaProceso))]
        BE.RutaProceso GuardaRutaProceso(BE.RutaProceso RutaProceso);

        #endregion
        #region GuardaInventario

        [OperationContract]
        [ServiceKnownType(typeof(BE.Inventario))]
        void GuardaInventario(BE.Inventario inv);

        #endregion
        #region GuardarMetasProd

        [OperationContract]
        [ServiceKnownType(typeof(BE.MetasProd))]
        void GuardarMetasProd(BE.MetasProd MP);

        #endregion
        #region IniciarTerminarInventario

        [OperationContract]
        void IniciarTerminarInventario(int codInventario, bool enProceso);

        #endregion
        #region EliminaRutaProceso

        [OperationContract]
        [ServiceKnownType(typeof(BE.RutaProceso))]
        BE.RutaProceso EliminaRutaProceso(BE.RutaProceso RutaProceso);

        #endregion
        #region EliminaConfigBanco

        [OperationContract]
        [ServiceKnownType(typeof(BE.ConfigBancos))]
        BE.ConfigBancos EliminaConfigBanco(BE.ConfigBancos CB);

        #endregion
        #region EliminaUsuario

        [OperationContract]
        [ServiceKnownType(typeof(BE.Usuario))]
        BE.Usuario EliminaUsuario(BE.Usuario U);

        #endregion
        #region EliminaTurno

        [OperationContract]
        void EliminaTurno(int codTurno);

        #endregion
        #region EliminaRol

        [OperationContract]
        void EliminaRol(int codRol);

        #endregion
        #region EliminaPrueba

        [OperationContract]
        void EliminaPrueba(int CodPrueba);

        #endregion


        #region AutorizaConfigBanco

        [OperationContract]
        [ServiceKnownType(typeof(BE.ConfigBancos))]
        void AutorizaConfigBanco(BE.ConfigBancos AU);

        #endregion

        #region AutorizaCondicionEsmalte

        [OperationContract]
        [ServiceKnownType(typeof(BE.CondicionEsmalteAutoriza))]
        void AutorizaCondicionEsmalte(BE.CondicionEsmalteAutoriza AC);

        #endregion
        #region AutorizaCondicionPasta

        [OperationContract]
        [ServiceKnownType(typeof(BE.CondicionPastaAutoriza))]
        void AutorizaCondicionPasta(BE.CondicionPastaAutoriza AC);

        #endregion
        #region AutorizaCondicionOperacion

        [OperationContract]
        [ServiceKnownType(typeof(BE.CondicionOperacionAutoriza))]
        void AutorizaCondicionOperacion(BE.CondicionOperacionAutoriza AC);

        #endregion


        #region ObtenerCodigoBarras

        [OperationContract]
        [ServiceKnownType(typeof(BE.CodigoBarra))]
        SolutionEntityList<BE.CodigoBarra> ObtenerCodigoBarras(int planta, int centroTrabajo, int banco, int empleado);
        #endregion

        #region CambiarContrasena

        [OperationContract]
        [ServiceKnownType(typeof(BE.ContrasenaC))]
        BE.ContrasenaC CambiarContrasena(BE.ContrasenaC Contrasena);

        #endregion
        #region CambiarContrasenaLogin

        [OperationContract]
        [ServiceKnownType(typeof(BE.ContrasenaL))]
        BE.ContrasenaL CambiarContrasenaLogin(BE.ContrasenaL Contrasena);

        #endregion
        [OperationContract]
        DataSet ObtenerInfoDashboard(int iCodPlanta, int iRol);
        [OperationContract]
        DataSet ObtenerKardexExportar(int iCodigo);
        [OperationContract]
        IList<Maquina> ObtenerHornoClasificado(int iPlanta, int iProceso);
        [OperationContract]
        int ExisteSKU(string sku);
        [OperationContract]
        bool InsertarPiezaReemplazo(int iCodPieza, int iCodProcesoPiezaReem);
        [OperationContract]
        DataTable ObtenerAlerta(int iCodigoAlerta, int iCodigoTipoAlerta, int iCodigoPlanta, int iCodigoProceso);
        [OperationContract]
        DataTable ObtenerAlertaPlanta(int iCodigoAlerta, int iCodigoTipoAlerta, int iCodigoPlanta, int iCodigoProceso, int iCodigoOperador);
        [OperationContract]
        DataTable ObtenerPiezasConResidencia(int iCodigoAlerta, int iCodigoPlanta, int iCodigoProceso, int iCodigoTipoArticulo, int iCodigoArticulo, int iCodigoMaquina, int iCodigoColor, int iCodigoEmpleado, int iCodigoTurno, int iDiasResidencia);
        [OperationContract]
        bool PiezaBajaPorResidencia(DataTable dtPiezaParaBaja, int iCodigoUsuario);
        [OperationContract]
        DataTable ObtenerPiezasBajaResidencia(int iCodigoAlerta, int iCodigoPlanta, int iCodigoProceso, int iCodigoTipoArticulo, int iCodigoArticulo, int iCodigoMaquina, int iCodigoColor, int iCodigoEmpleado, int iCodigoTurno, int iDiasResidencia);
        [OperationContract]
        bool PriezaReestablecerResidencia(DataTable dtPiezaParaBaja);
        #endregion Common
        [OperationContract]
        string ObtenerMensajeInicioSesion();
        [OperationContract]
        bool ValidarPoliticaContrasena(string contrasena);
        [OperationContract]
        string CambiarContrasenaUsuario(string sUsuario, string sContrasenaActual, string sContrasenaNueva);
        [OperationContract]
        bool DesbloquearUsuario(int CodigoUsuario);
        #endregion Methods

    }

}
