using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Common.SolutionEntityFramework;
using BE = LAMOSA.SCPP.Server.BusinessEntity;
using BC = LAMOSA.SCPP.Server.BusinessComponent;
using LAMOSA.SCPP.Server.BusinessEntity;

namespace LAMOSA.SCPP.Server.Service
{

    // NOTE: If you change the class name "SCPP" here, you must also update the reference to "SCPP" in App.config.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SCPP : ISCPP
    {

        #region Fields

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion Fields

        #region Methods

        #region Constructors and Destructor

        public SCPP()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~SCPP()
        {

        }

        #endregion Constructors and Destructor

        #region Common

        #region ObtenerTurnos
        public SolutionEntityList<BE.Turno> ObtenerTurnos()
        {
            SolutionEntityList<BE.Turno> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerTurnos();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerTurnos: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerTurnos
        #region ObtenerCalidades
        public SolutionEntityList<BE.Calidad> ObtenerCalidades()
        {
            SolutionEntityList<BE.Calidad> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerCalidades();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerCalidades: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerCalidades
        #region ObtenerCalidadesCbo
        public SolutionEntityList<BE.Calidad> ObtenerCalidadesCbo()
        {
            SolutionEntityList<BE.Calidad> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerCalidadesCbo();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerCalidadesCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerCalidadesCbo
        #region ObtenerPlantaCalidadCbo
        public SolutionEntityList<BE.PlantaCalidad> ObtenerPlantaCalidadCbo(int CodPlanta)
        {
            SolutionEntityList<BE.PlantaCalidad> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerPlantaCalidadCbo(CodPlanta);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerPlantaCalidadCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerPlantaCalidadCbo
        #region ObtenerDefectosCbo
        public SolutionEntityList<BE.DefectoCbo> ObtenerDefectosCbo(int CodProceso, int CodTipoDefecto)
        {
            SolutionEntityList<BE.DefectoCbo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerDefectoCbo(CodProceso, CodTipoDefecto);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerDefectosCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerDefectosCbo
        #region ObtenerZonaDefectoCbo
        public SolutionEntityList<BE.ZonaDefectoCbo> ObtenerZonaDefectoCbo()
        {
            SolutionEntityList<BE.ZonaDefectoCbo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerZonaDefectoCbo();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerZonaDefectoCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerZonaDefectoCbo
        #region ObtenerColores
        public SolutionEntityList<BE.Color> ObtenerColores()
        {
            SolutionEntityList<BE.Color> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerColores();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerColores: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerColores
        #region ObtenerArticulos
        public SolutionEntityList<BE.Articulo> ObtenerArticulos(BE.ArticuloPars articuloPars)
        {
            SolutionEntityList<BE.Articulo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerArticulos(articuloPars);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerArticulos: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerArticulos
        #region ObtenerArticulosCbo
        public SolutionEntityList<BE.ArticuloCbo> ObtenerArticulosCbo(int tipoArticulo)
        {
            SolutionEntityList<BE.ArticuloCbo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerArticulosCbo(tipoArticulo);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerArticulosCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerModelosCbo
        public SolutionEntityList<BE.ArticuloCbo> ObtenerModelosCbo(int tipoArticulo)
        {
            SolutionEntityList<BE.ArticuloCbo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerModelosCbo(tipoArticulo);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerModelosCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerTiposArticuloCbo
        public SolutionEntityList<BE.TipoArticuloCbo> ObtenerTiposArticuloCbo()
        {
            SolutionEntityList<BE.TipoArticuloCbo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerTiposArticuloCbo();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerTiposArticuloCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerTiposArticuloCbo
        #region ObtenerMoldesCbo
        public SolutionEntityList<BE.MoldeCbo> ObtenerMoldesCbo(BE.ArticuloPars articuloPars)
        {
            SolutionEntityList<BE.MoldeCbo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerMoldesCbo(articuloPars);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerMoldesCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerMoldesCbo
        #region ObtenerMoldes
        public SolutionEntityList<BE.Molde> ObtenerMoldes(BE.ArticuloPars articuloPars)
        {
            SolutionEntityList<BE.Molde> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerMoldes(articuloPars);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerMoldes: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerMoldes
        #region Login
        public BE.LoginUsuario Login(BE.LoginUsuario lu)
        {
            BE.LoginUsuario beRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                beRes = bcObj.Login(lu);
            }
            catch (Exception ex)
            {
                beRes.ExceptionMessage = this.sClassName + ", Login: " + ex.Message;
            }
            return beRes;
        }
        #endregion Login

        #region ObtenerPruebas
        public SolutionEntityList<BE.Prueba> ObtenerPruebas(int Proceso)
        {
            SolutionEntityList<BE.Prueba> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerPruebas(Proceso);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerPruebas: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerPruebas
        #region ObtenerDefectos
        public SolutionEntityList<BE.Defecto> ObtenerDefectos(int CodProceso, int CodPlanta)
        {
            SolutionEntityList<BE.Defecto> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerDefectos(CodProceso, CodPlanta);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerDefectos: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerDefectos

        #region ObtenerTipoDefectos
        public SolutionEntityList<BE.TipoDefecto> ObtenerTiposDefecto()
        {
            SolutionEntityList<BE.TipoDefecto> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerTiposDefectoCbo();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerTiposDefecto: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerDefectos

        #region ObtenerZonaDefecto
        public SolutionEntityList<BE.ZonaDefecto> ObtenerZonaDefecto(int CodTipoArticulo)
        {
            SolutionEntityList<BE.ZonaDefecto> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerZonaDefecto(CodTipoArticulo);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerZonaDefecto: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerZonaDefecto
        #region ObtenerAlmacen
        public SolutionEntityList<BE.Almacen> ObtenerAlmacen()
        {
            SolutionEntityList<BE.Almacen> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerAlmacen();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerAlmacen: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerAlmacen
        #region ObtenerAlmacenCbo
        public SolutionEntityList<BE.Almacen> ObtenerAlmacenCbo()
        {
            SolutionEntityList<BE.Almacen> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerAlmacenCbo();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerAlmacenCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerAlmacenCbo
        #region ObtenerPlanta
        public SolutionEntityList<BE.Planta> ObtenerPlanta(int Almacen)
        {
            SolutionEntityList<BE.Planta> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerPlanta(Almacen);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerPlanta: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerPlanta
        #region ObtenerCentroTrabajo
        public SolutionEntityList<BE.CentroTrabajo> ObtenerCentroTrabajo(int Planta, int Proceso)
        {
            SolutionEntityList<BE.CentroTrabajo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerCentroTrabajo(Planta, Proceso);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerCentroTrabajo: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerCentroTrabajo
        #region ObtenerCentroTrabajoCbo
        public SolutionEntityList<BE.CentroTrabajo> ObtenerCentroTrabajoCbo(int Planta, int Proceso)
        {
            SolutionEntityList<BE.CentroTrabajo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerCentroTrabajoCbo(Planta, Proceso);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerCentroTrabajoCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerCentroTrabajoCbo
        #region ObtenerArea
        public SolutionEntityList<BE.Area> ObtenerArea(int CT)
        {
            SolutionEntityList<BE.Area> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerArea(CT);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerArea: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerArea
        #region ObtenerEstructuraPlanta
        public SolutionEntityList<BE.EstructuraPlanta> ObtenerEstructuraPlanta(int Planta)
        {
            SolutionEntityList<BE.EstructuraPlanta> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerEstructuraPlanta(Planta);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerEstructuraPlanta: " + ex.Message;
            }
            return selRes;
        }
        #endregion ObtenerEstructuraPlanta
        #region ObtenerProcesoCbo
        public SolutionEntityList<BE.ProcesoCbo> ObtenerProcesoCbo(int Planta)
        {
            SolutionEntityList<BE.ProcesoCbo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerProcesoCbo(Planta);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerProcesoCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerProceso
        public SolutionEntityList<BE.Proceso> ObtenerProceso()
        {
            SolutionEntityList<BE.Proceso> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerProceso();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerProceso: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerRutaProceso
        public SolutionEntityList<BE.RutaProceso> ObtenerRutaProceso(int Planta)
        {
            SolutionEntityList<BE.RutaProceso> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerRutaProceso(Planta);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerRutaProceso: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerRol
        public SolutionEntityList<BE.Rol> ObtenerRol()
        {
            SolutionEntityList<BE.Rol> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerRol();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerRol: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerRolCbo
        public SolutionEntityList<BE.Rol> ObtenerRolCbo()
        {
            SolutionEntityList<BE.Rol> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerRolCbo();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerRolCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerEmpleado
        public SolutionEntityList<BE.Empleado> ObtenerEmpleado(int CodPlanta, int CodPuesto)
        {
            SolutionEntityList<BE.Empleado> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerEmpleado(CodPlanta, CodPuesto);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerEmpleado: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerEmpleadoBusqueda
        public SolutionEntityList<BE.EmpleadoBusqueda> ObtenerEmpleadoBusqueda(int CodEmpleado, string NombreEmpleado, int Rol)
        {
            SolutionEntityList<BE.EmpleadoBusqueda> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerEmpleadoBusqueda(CodEmpleado, NombreEmpleado, Rol);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerEmpleadoBusqueda: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerEmpleadoMFG
        public string ObtenerMFGEmpleado(int CodEmpleado)
        {
            BC.SCPP bcObj = null;
            try
            {
                bcObj = new BC.SCPP();
                return bcObj.ObtenerMFGEmpleado(CodEmpleado);
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
        #region ObtenerPlantaCbo
        public SolutionEntityList<BE.PlantaCbo> ObtenerPlantaCbo()
        {
            SolutionEntityList<BE.PlantaCbo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerPlantaCbo();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerPlantaCbo: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerMaquinaCbo
        public SolutionEntityList<BE.MaquinaCbo> ObtenerMaquinaCbo(int codArea, int codCT)
        {
            SolutionEntityList<BE.MaquinaCbo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerMaquinaCbo(codArea, codCT);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerMaquinaCbo: " + ex.Message;
            }
            return selRes;
        }
        public SolutionEntityList<BE.MaquinaCbo> ObtenerMaquinas(int codigoArea, int codigoCentroTrabajo, int codigoPlanta, int codigoProceso)
        {
            SolutionEntityList<BE.MaquinaCbo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerMaquinas(codigoArea, codigoCentroTrabajo, codigoPlanta, codigoProceso);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerMaquinas: " + ex.Message;
            }
            return selRes;
        }

        #endregion
        #region ObtenerMaquina
        public SolutionEntityList<BE.Maquina> ObtenerMaquina(int codPlanta, int codProceso)
        {
            SolutionEntityList<BE.Maquina> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerMaquina(codPlanta, codProceso);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerMaquina: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerPuesto
        public SolutionEntityList<BE.Puesto> ObtenerPuesto()
        {
            SolutionEntityList<BE.Puesto> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerPuesto();
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerUsuarios
        public SolutionEntityList<BE.Usuario> ObtenerUsuarios(int Planta, int Rol, string Usuario)
        {
            SolutionEntityList<BE.Usuario> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerUsuarios(Planta, Rol, Usuario);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerUsuarios: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerCondicionEsmalte
        public SolutionEntityList<BE.CondicionEsmalte> ObtenerCondicionEsmalte(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            SolutionEntityList<BE.CondicionEsmalte> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerCondicionEsmalte(CodPlanta, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerCondicionEsmalte: " + ex.Message;
            }
            return selRes;
        }
        #endregion

        #region ObtenerCondicionOperacion
        public SolutionEntityList<BE.CondicionOperacion> ObtenerCondicionOperacion(int CodPlanta, int CodProceso, int CodArea, DateTime FechaInicio, DateTime FechaFin)
        {
            SolutionEntityList<BE.CondicionOperacion> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerCondicionOperacion(CodPlanta, CodProceso, CodArea, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerCondicionOperacion: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerCondicionPasta
        public SolutionEntityList<BE.CondicionPasta> ObtenerCondicionPasta(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            SolutionEntityList<BE.CondicionPasta> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerCondicionPasta(CodPlanta, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerCondicionPasta: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerCondicionPastaExportar
        public DataTable ObtenerCondicionPastaExportar(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            DataTable selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerCondicionPastaExportar(CodPlanta, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return selRes;
        }
        #endregion
        #region ObtenerCondicionEsmalteExportar
        public DataTable ObtenerCondicionEsmalteExportar(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            DataTable selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerCondicionEsmalteExportar(CodPlanta, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return selRes;
        }
        #endregion
        #region ObtenerConfiguracionBancoDetalle
        public SolutionEntityList<BE.ConfigBancoDetalle> ObtenerConfiguracionBancoDetalle(int CodConfigBanco)
        {
            SolutionEntityList<BE.ConfigBancoDetalle> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerConfiguracionBancoDetalle(CodConfigBanco);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerConfiguracionBancoDetalle: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerConfiguracionBanco
        public SolutionEntityList<BE.ConfigBancos> ObtenerConfiguracionBanco(int planta, int ct, int maquina, int activo)
        {
            SolutionEntityList<BE.ConfigBancos> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerConfiguracionBanco(planta, ct, maquina, activo);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerConfiguracionBanco: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerInventarioEnProceso
        public SolutionEntityList<BE.InventarioEnProceso> ObtenerInventarioEnProceso(int CodAlmacen, int CodPlanta, int CodProceso, int CodTipoArticulo, int CodArticulo, DateTime FechaInicio, DateTime FechaFin, int Opcion)
        {
            SolutionEntityList<BE.InventarioEnProceso> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerInventarioEnProceso(CodAlmacen, CodPlanta, CodProceso, CodTipoArticulo, CodArticulo, FechaInicio, FechaFin, Opcion);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerInventarioEnProceso: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerPiezasReemplazo
        public SolutionEntityList<BE.PiezaReemplazo> ObtenerPiezasReemplazo(bool SinPiezasreemplazadas)
        {
            SolutionEntityList<BE.PiezaReemplazo> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerPiezasReemplazo(SinPiezasreemplazadas);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerPiezasReemplazo: " + ex.Message;
            }
            return selRes;
        }
        #endregion

        #region ObtenerRepCapInstalada
        public SolutionEntityList<BE.RepCapInstalada> ObtenerRepCapInstalada(int Opcion, int Planta, int CT, int Banco, int TipoArt, int Molde)
        {
            SolutionEntityList<BE.RepCapInstalada> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerRepCapInstalada(Opcion, Planta, CT, Banco, TipoArt, Molde);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerRepCapInstalada: " + ex.Message;
            }
            return selRes;
        }
        #endregion

        #region ObtenerConfig
        public SolutionEntityList<BE.Configuracion> ObtenerConfig(int CodConfiguracion)
        {
            SolutionEntityList<BE.Configuracion> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerConfig(CodConfiguracion);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerConfig: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerMetasProd
        public SolutionEntityList<BE.MetasProd> ObtenerMetasProd(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            SolutionEntityList<BE.MetasProd> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerMetasProd(CodPlanta, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerMetasProd: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerKardexProductoBusqueda
        public SolutionEntityList<BE.KardexProductoBusqueda> ObtenerKardexProductoBusqueda(string Codigo)
        {
            SolutionEntityList<BE.KardexProductoBusqueda> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerKardexProductoBusqueda(Codigo);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerKardexProductoBusqueda: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerKardexProducto
        public SolutionEntityList<BE.KardexProducto> ObtenerKardexProducto(int Codigo)
        {
            SolutionEntityList<BE.KardexProducto> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerKardexProducto(Codigo);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerKardexProducto: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region ObtenerKardexProductoDefecto
        public SolutionEntityList<BE.KardexProductoDefecto> ObtenerKardexProductoDefecto(int Codigo)
        {
            SolutionEntityList<BE.KardexProductoDefecto> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerKardexProductoDefecto(Codigo);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerKardexProductoDefecto: " + ex.Message;
            }
            return selRes;
        }
        #endregion


        #region GuardarPlanta
        public void GuardarPlanta(BE.Planta Planta)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.GuardarPlanta(Planta);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarPlanta: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion
        #region GuardarPrueba
        public void GuardarPrueba(BE.Prueba Prueba, int iCodPlanta)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.GuardarPrueba(Prueba, iCodPlanta);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarPrueba: " + ex.Message);
            }
        }
        #endregion
        #region GuardarTurno
        public void GuardarTurno(BE.Turno Turno)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.GuardarTurno(Turno);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarTurno: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion
        #region GuardarCondicionOperacion
        public void GuardarCondicionOperacion(BE.CondicionOperacionGuarda CO)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.GuardarCondicionOperacion(CO);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarCondicionOperacion: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion
        #region GuardarCondicionPasta
        public void GuardarCondicionPasta(BE.CondicionPasta CP)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.GuardarCondicionPasta(CP);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarCondicionPasta: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion
        #region GuardarConfigBanco
        public int GuardarConfigBanco(BE.ConfigBancoResgistro CB)
        {
            int res = 0;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                res = bcObj.GuardarConfigBanco(CB);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarConfigBanco: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
            return res;
        }
        #endregion
        //#region GuardarConfigBancoDetalle
        //public void GuardarConfigBancoDetalle(BE.ConfigBancoDetalle CBD)
        //{
        //    try
        //    {
        //        BC.SCPP bcObj = new BC.SCPP();
        //        bcObj.GuardarConfigBancoDetalle(CBD);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(this.sClassName + ", GuardarConfigBancoDetalle: " + ex.Message);
        //        //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
        //    }
        //}
        //#endregion


        #region GuardaRol
        public void GuardaRol(BE.rolplanta RolPlanta)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.GuardaRol(RolPlanta);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardaRol: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion
        #region GuardarCondicionEsmalte
        public void GuardarCondicionEsmalte(BE.CondicionEsmalte CE)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.GuardarCondicionEsmalte(CE);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarCondicionEsmalte: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion
        #region GuardarUsuario
        public BE.Usuario GuardarUsuario(BE.Usuario Usuario)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                Usuario = bcObj.GuardarUsuario(Usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarUsuario: " + ex.Message);
            }
            return Usuario;
        }
        #endregion
        #region GuardaRutaProceso
        public BE.RutaProceso GuardaRutaProceso(BE.RutaProceso RutaProceso)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                RutaProceso = bcObj.GuardaRutaProceso(RutaProceso);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardaRutaProceso: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
            return RutaProceso;
        }
        #endregion
        #region GuardarMetasProd
        public void GuardarMetasProd(BE.MetasProd MP)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.GuardarMetasProd(MP);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarMetasProd: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion

        #region EliminaRutaProceso
        public BE.RutaProceso EliminaRutaProceso(BE.RutaProceso RutaProceso)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                RutaProceso = bcObj.EliminaRutaProceso(RutaProceso);
            }
            catch (Exception ex)
            {

                throw new Exception(this.sClassName + ", EliminaRutaProceso: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
            return RutaProceso;
        }
        #endregion
        #region EliminaConfigBanco
        public BE.ConfigBancos EliminaConfigBanco(BE.ConfigBancos CB)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                CB = bcObj.EliminaConfigBanco(CB);
            }
            catch (Exception ex)
            {

                throw new Exception(this.sClassName + ", EliminaConfigBanco: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
            return CB;
        }
        #endregion

        #region EliminaUsuario
        public BE.Usuario EliminaUsuario(BE.Usuario U)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                U = bcObj.EliminaUsuario(U);
            }
            catch (Exception ex)
            {

                throw new Exception(this.sClassName + ", EliminaConfigBanco: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
            return U;
        }
        #endregion
        #region EliminaTurno
        public void EliminaTurno(int codTurno)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.EliminaTurno(codTurno);
            }
            catch (Exception ex)
            {

                throw new Exception(this.sClassName + ", EliminaTurno: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion
        #region EliminaRol
        public void EliminaRol(int codRol)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.EliminaRol(codRol);
            }
            catch (Exception ex)
            {

                throw new Exception(this.sClassName + ", EliminaRol: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion

        #region EliminaPrueba
        public void EliminaPrueba(int CodPrueba)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.EliminaPrueba(CodPrueba);
            }
            catch (Exception ex)
            {

                throw new Exception(this.sClassName + ", EliminaTurno: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion
        #region ObtenerInventario
        public SolutionEntityList<BE.Inventario> ObtenerInventario(int CodPlanta, DateTime FechaInicio, DateTime FechaFin)
        {
            SolutionEntityList<BE.Inventario> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerInventario(CodPlanta, FechaInicio, FechaFin);
            }
            catch (Exception ex)
            {
                //throw new Exception(this.sClassName + ", GuardaRol: " + ex.Message);
                selRes.ExceptionMessage = this.sClassName + ", ObtenerInventario: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        #region GuardaInventario
        public void GuardaInventario(BE.Inventario inv)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.GuardaInventario(inv);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardaInventario: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion
        #region IniciarTerminarInventario
        public void IniciarTerminarInventario(int codInventario, bool enProceso)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.IniciarTerminarInventario(codInventario, enProceso);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", IniciarTerminarInventario: " + ex.Message);
                //Planta.ExceptionMessage = this.sClassName + ", ObtenerPuesto: " + ex.Message;
            }
        }
        #endregion
        #region GuardarConfig
        public void GuardarConfig(BE.Configuracion Conf)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.GuardaConfig(Conf);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", GuardarConfig: " + ex.Message);
            }
        }
        #endregion
        #region AutorizaConfigBanco
        public void AutorizaConfigBanco(BE.ConfigBancos AU)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.AutorizaConfigBanco(AU);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", AutorizaConfigBanco: " + ex.Message);
            }
        }
        #endregion
        #region AutorizaCondicionEsmalte
        public void AutorizaCondicionEsmalte(BE.CondicionEsmalteAutoriza AC)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.AutorizaCondicionEsmalte(AC);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", AutorizaCondicionEsmalte: " + ex.Message);
            }
        }
        #endregion
        #region AutorizaCondicionOperacion
        public void AutorizaCondicionOperacion(BE.CondicionOperacionAutoriza AC)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.AutorizaCondicionOperacion(AC);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", AutorizaCondicionOperacion: " + ex.Message);
            }
        }
        #endregion
        #region AutorizaCondicionPasta
        public void AutorizaCondicionPasta(BE.CondicionPastaAutoriza AC)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bcObj.AutorizaCondicionPasta(AC);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", AutorizaCondicionPasta: " + ex.Message);
            }
        }
        #endregion

        #region CambiarContrasena
        public BE.ContrasenaC CambiarContrasena(BE.ContrasenaC Contrasena)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                Contrasena = bcObj.CambiarContrasena(Contrasena);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", CambiarContrasena: " + ex.Message);
            }
            return Contrasena;
        }
        #endregion
        #region CambiarContrasenaLogin
        public BE.ContrasenaL CambiarContrasenaLogin(BE.ContrasenaL Contrasena)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                Contrasena = bcObj.CambiarContrasenaLogin(Contrasena);
            }
            catch (Exception ex)
            {
                throw new Exception(this.sClassName + ", CambiarContrasenaLogin: " + ex.Message);
            }
            return Contrasena;
        }
        #endregion

        #region ObtenerCodigoBarras
        public SolutionEntityList<BE.CodigoBarra> ObtenerCodigoBarras(int planta, int centroTrabajo, int banco, int empleado)
        {
            SolutionEntityList<BE.CodigoBarra> selRes = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                selRes = bcObj.ObtenerCodigoBarras(planta, centroTrabajo, banco, empleado);
            }
            catch (Exception ex)
            {
                selRes.ExceptionMessage = this.sClassName + ", ObtenerUsuarios: " + ex.Message;
            }
            return selRes;
        }
        #endregion
        public DataSet ObtenerInfoDashboard(int iCodPlanta, int iRol)
        {
            DataSet dsRes = new DataSet();
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                dsRes = bcObj.ObtenerInfoDashboard(iCodPlanta, iRol);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRes;
        }
        public DataSet ObtenerKardexExportar(int iCodigo)
        {
            DataSet dsRes = new DataSet();
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                dsRes = bcObj.ObtenerKardexExportar(iCodigo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsRes;
        }

        public IList<Maquina> ObtenerHornoClasificado(int iPlanta, int iProceso)
        {
            IList<Maquina> lMaquina = null;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                lMaquina = bcObj.ObtenerHornoClasificado(iPlanta, iProceso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lMaquina;
        }
        public int ExisteSKU(string sku)
        {
            int iRes = -1;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                iRes = bcObj.ExisteSKU(sku);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iRes;
        }
        public bool InsertarPiezaReemplazo(int iCodPieza, int iCodProcesoPiezaReem)
        {
            bool bRes = false;
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                bRes = bcObj.InsertarPiezaReemplazo(iCodPieza, iCodProcesoPiezaReem);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bRes;
        }
        #endregion lMaquina
        public DataTable ObtenerAlerta(int iCodigoAlerta, int iCodigoTipoAlerta, int iCodigoPlanta, int iCodigoProceso)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                return bcObj.ObtenerAlerta(iCodigoAlerta, iCodigoTipoAlerta, iCodigoPlanta, iCodigoProceso);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerAlertaPlanta(int iCodigoAlerta, int iCodigoTipoAlerta, int iCodigoPlanta, int iCodigoProceso, int iCodigoOperador)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                return bcObj.ObtenerAlerta(iCodigoAlerta, iCodigoTipoAlerta, iCodigoPlanta, iCodigoProceso, iCodigoOperador);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerPiezasConResidencia(int iCodigoAlerta, int iCodigoPlanta, int iCodigoProceso, int iCodigoTipoArticulo, int iCodigoArticulo, int iCodigoMaquina, int iCodigoColor, int iCodigoEmpleado, int iCodigoTurno, int iDiasResidencia)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                return bcObj.ObtenerPiezasConResidencia(iCodigoAlerta, iCodigoPlanta, iCodigoProceso, iCodigoTipoArticulo, iCodigoArticulo, iCodigoMaquina, iCodigoColor, iCodigoEmpleado, iCodigoTurno, iDiasResidencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool PiezaBajaPorResidencia(DataTable dtPiezaParaBaja, int iCodigoUsuario)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                return bcObj.PiezaBajaPorResidencia(dtPiezaParaBaja, iCodigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable ObtenerPiezasBajaResidencia(int iCodigoAlerta, int iCodigoPlanta, int iCodigoProceso, int iCodigoTipoArticulo, int iCodigoArticulo, int iCodigoMaquina, int iCodigoColor, int iCodigoEmpleado, int iCodigoTurno, int iDiasResidencia)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                return bcObj.ObtenerPiezasBajaResidencia(iCodigoAlerta, iCodigoPlanta, iCodigoProceso, iCodigoTipoArticulo, iCodigoArticulo, iCodigoMaquina, iCodigoColor, iCodigoEmpleado, iCodigoTurno, iDiasResidencia);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool PriezaReestablecerResidencia(DataTable dtPiezaParaBaja)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                return bcObj.PriezaReestablecerResidencia(dtPiezaParaBaja);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string ObtenerMensajeInicioSesion()
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                return bcObj.ObtenerMensajeInicioSesion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ValidarPoliticaContrasena(string contrasena)
        {
            try
            {
                return BC.SCPP.ValidarPoliticaContrasena(contrasena);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string CambiarContrasenaUsuario(string sUsuario, string sContrasenaActual, string sContrasenaNueva)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                return bcObj.CambiarContrasena(sUsuario, sContrasenaActual, sContrasenaNueva);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DesbloquearUsuario(int CodigoUsuario)
        {
            try
            {
                BC.SCPP bcObj = new BC.SCPP();
                return bcObj.DesbloquearUsuario(CodigoUsuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion Methods

    } // class

}
