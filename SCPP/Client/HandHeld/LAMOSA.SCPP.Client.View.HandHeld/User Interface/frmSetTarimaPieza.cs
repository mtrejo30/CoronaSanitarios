using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld.User_Interface
{
    public partial class frmSetTarimaPieza : Form
    {
        #region Atributos
        private a03_ConfiguracionInicial frmConfiguracionInicial;
        private LoginUsuario lu = null;
        private c00_Common oDA0 = new c00_Common();
        private c11_ArmadoTarima oDA = new c11_ArmadoTarima();
        private c12_CapturaAuditoria oDA1 = new c12_CapturaAuditoria();
        private int iCodTarima = -1;
        private int iCodPieza = -1;
        private int iCodEstadoPieza = -1;
        private string sDesEstadoPieza = string.Empty;
        private int iCodUltimoProcesoPieza = -1;
        private string sDesUltimoProcesoPieza = string.Empty;
        private int iCodProcesoAct = -1;
        private static int iTarimaInsertada = -1;
        private static int iPiezaTarimaInsertada = -1;
        private enum eTipoDisplayMensaje
        {
            OK,
            OC,
            YNC,
            ARI,
            YN,
            RC
        }
        private bool flagFiltroCodigoTarima = false;
        private string[] sourceTarimaDestino = new string[3] { "Seleccione Destino", "Tarima 1", "Tarima 2" };
        private bool EstaPiezaSeleccionada = false;
        private bool IsDisplayingMessage = false;
        public a03_ConfiguracionInicial CapturaConfiguracionInicial
        {
            get { return frmConfiguracionInicial; }
            set { frmConfiguracionInicial = value; }
        }
        private HHsvc.SCPP_HH oProxy = null;
        private HHsvc.SCPP_HH Proxy 
        {
            get { return (oProxy == null)?oProxy = new LAMOSA.SCPP.Client.View.HandHeld.HHsvc.SCPP_HH():oProxy;}
        }
        #endregion

        #region Eventos Control Form
        public frmSetTarimaPieza(LoginUsuario lu)
        {
            InitializeComponent();
            this.lu = lu;
            //this.lu.CodPlanta = Proxy.
        }
        public void SetFormCalled(Form forma)
        {
            frmConfiguracionInicial= forma as a03_ConfiguracionInicial;
        }
        private void frmSetTarimaPieza_Load(object sender, EventArgs e)
        {
            this.iCodProcesoAct = this.lu.CodProceso;
            SetFocus(this.tbTarima);
            SetItemsCombo(cmbTarimaDestino, sourceTarimaDestino);
            SetLayoutGrid(dgADPiezaTarima);
            SetLayoutGrid(dgConsultaTarima);
        }
        #endregion
        #region Metodos HelperView
        private void SetLayoutGrid(Control ctrl)
        {
            DataTable dtLayout = null;
            if (ctrl.GetType() == typeof(DataGrid))
                switch (ctrl.Name)
                {
                    case "dgADPiezaTarima":
                        dtLayout = new DataTable(dtADPiezaTarima.MappingName);
                        dtLayout.Columns.Add(new DataColumn(dtColADTarima.MappingName));
                        dtLayout.Columns.Add(new DataColumn(dtColADPieza.MappingName));
                        dtLayout.Columns.Add(new DataColumn(dtColADSKU.MappingName));
                        (ctrl as DataGrid).DataSource = dtLayout;
                        break;
                    case "dgConsultaTarima":
                        dtLayout = new DataTable(dtConsultaTarima.MappingName);
                        dtLayout.Columns.Add(new DataColumn(dtColConsultaTarima.MappingName));
                        dtLayout.Columns.Add(new DataColumn(dtColConsultaLineaEmpaque.MappingName));
                        dtLayout.Columns.Add(new DataColumn(dtColConsultaSKU.MappingName));
                        dtLayout.Columns.Add(new DataColumn(dtColConsultaCantidad.MappingName));
                        dtLayout.Columns.Add(new DataColumn(dtColConsultaCapacidad.MappingName));
                        dtLayout.Columns.Add(new DataColumn(dtColConsultaEstado.MappingName));
                        dtLayout.Columns.Add(new DataColumn(dtColConsultaFecha.MappingName));
                        (ctrl as DataGrid).DataSource = dtLayout;
                        break;
                }
        }
        private void SetFocus(Control ctrl)
        {
            ctrl.Focus();
        }
        private void SetEnable(Control ctrl)
        {
            ctrl.Enabled = true;
            ctrl.Refresh();
        }
        private void SetDisEnable(Control ctrl)
        {
            ctrl.Enabled = false;
            ctrl.Refresh();
        }
        private void DisplayMessage(string sMensaje, eTipoDisplayMensaje enumTipoDisplay)
        {
            string sTitulo = "Proceso Entarimado";
            IsDisplayingMessage = true;
            switch (enumTipoDisplay)
            {
                case eTipoDisplayMensaje.OK:
                    MessageBox.Show(sMensaje, sTitulo, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    break;
            }
        }
        private void InicializarControl(Control ctrl)
        {
            if (ctrl.GetType() == typeof(DataGrid))
                switch (ctrl.Name) 
                {
                    case "dgADPiezaTarima":
                        this.CargarGridADPiezaTarima(ctrl as DataGrid);
                        break;
                    case "dgConsultaTarima":
                        this.CargarGridConsultaTarima(ctrl as DataGrid);
                        break;
                } 
        }
        #endregion
        #region Eventos TabControl
        private void tabControlTarima_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch ((sender as TabControl).SelectedIndex)
            {
                case 0:
                    SetLayoutGrid(dgADPiezaTarima);
                    SetVisible(tbTarima, true);
                    SetVisible(lbTarima, true);
                    SetVisible(dgADPiezaTarima, true);
                    SetVisible(tbDescPiezaEntarimar, false);
                    tbPiezasEnTarima.Text = string.Empty;
                    SetFocus(tbTarima);
                    break;
                case 1:
                    SetLayoutGrid(dgConsultaTarima);
                    SetVisible(tbTarima, true);
                    SetVisible(lbTarima, true);
                    SetFocus(btnConsultarTarima);
                    break;
                case 2:
                    cmbTarimaDestino.SelectedIndex = 0;
                    SetVisible(tbTarima, false);
                    SetVisible(lbTarima, false);
                    SetFocus(tbTarimaNo01);
                    break;
            }
        }
        #endregion
        #region Eventos TextBox
        private void TextBox_LostFocus(object sender, EventArgs e)
        {
            
        }
        private void TextBoxControl_GotFocus(object sender, EventArgs e)
        {
            if (!IsDisplayingMessage)
                (sender as TextBox).Text = string.Empty;
            else
                IsDisplayingMessage = false;
        }
        private void TextBoxControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            Validacion val = null;
            switch (tb.Name)
            {
                case "tbTarima":
                    #region KeyPress Tarima
                    this.tbDescPiezaEntarimar.Text = string.Empty;
                    SetVisible(tbDescPiezaEntarimar, false);
                    SetVisible(dgADPiezaTarima, true);
                    if (e.KeyChar == 13 && tabControlTarima.SelectedIndex == 0)
                    {
                        val = this.ValidarTarima(tb.Text);
                        if (!val.ValidacionExitosa)
                            DisplayMessage(val.MensajeValidacion, eTipoDisplayMensaje.OK);
                        if (val.ValidacionExitosa)
                        {
                            InicializarControl(dgADPiezaTarima);
                            DataTable dtPiezaEnTarima = dgADPiezaTarima.DataSource as DataTable;
                            if (dtPiezaEnTarima != null)
                                tbPiezasEnTarima.Text = dtPiezaEnTarima.Rows.Count.ToString();
                            else
                                tbPiezasEnTarima.Text = (0).ToString();
                            if (!EsTarimaValida(iCodTarima))
                                val.ValidacionExitosa = false;
                        }
                        if (!val.ValidacionExitosa)
                        {
                            tb.SelectAll();
                            //tb.Focus();
                            break;
                        }
                        else
                        {
                            SetFocus(this.tabPageADPiezaTarima);
                            SetFocus(tbEtiqueta);
                        }
                    }
                    else if (e.KeyChar == 13 && tabControlTarima.SelectedIndex == 1)
                        InicializarControl(dgConsultaTarima);
                    else
                        e.Handled = EsDidigo(e.KeyChar);
                    break;
                    #endregion
                case "tbEtiqueta":
                    #region KeyPress Etiqueta
                    if (e.KeyChar == 13 && tabControlTarima.SelectedIndex == 0)
                    {
                        val = this.ValidarPieza(tb.Text);
                        if (!val.ValidacionExitosa)
                        {
                            DisplayMessage(val.MensajeValidacion, eTipoDisplayMensaje.OK);
                            tb.Focus();
                            return;
                        }
                        val = ValidarTarimaPieza(this.iCodPieza);
                        if (!val.ValidacionExitosa)
                        {
                            DisplayMessage(val.MensajeValidacion, eTipoDisplayMensaje.OK);
                            tb.Focus();
                            return;
                        }
                        DataTable dtObj = null;
                        StringBuilder sb = new StringBuilder();
                        // Obtener el Modelo y Tipo de la pieza.
                        int iCodArticulo = this.oDA0.ObtenerCodModeloPieza(this.iCodPieza);
                        dtObj = this.oDA0.ObtenerModeloTipoPieza(iCodArticulo);
                        sb.AppendLine("Tipo: " + Convert.ToString(dtObj.Rows[0]["DesTipoModelo"]));
                        sb.AppendLine("Modelo: " + Convert.ToString(dtObj.Rows[0]["DesModelo"]));
                        // Obtener el Color de la pieza.
                        dtObj = this.oDA0.ObtenerColorPieza(this.iCodPieza);
                        if (dtObj.Rows.Count > 0)
                            sb.AppendLine("Color: " + Convert.ToString(dtObj.Rows[0]["DesColor"]));
                        else
                            sb.AppendLine("Color: " + string.Empty);
                        // Obtener la Calidad de la pieza.
                        dtObj = this.oDA0.ObtenerCalidadPieza(this.iCodPieza);
                        if (dtObj.Rows.Count > 0)
                            sb.AppendLine("Calidad: " + Convert.ToString(dtObj.Rows[0]["DesCalidad"]));
                        else
                            sb.AppendLine("Calidad: " + string.Empty);
                        // Asociar Pieza con la Tarima.
                        //this.iCodPieza = -1;
                        //tb.Text = string.Empty;
                        SetVisible(dgADPiezaTarima, false);
                        SetVisible(tbDescPiezaEntarimar, true);
                        this.tbDescPiezaEntarimar.Text = sb.ToString();
                        SetFocus(btnAceptar);
                    }
                    else
                        e.Handled = EsDidigo(e.KeyChar);
                    break;
                    #endregion
                case "tbTarimaNo01":
                    #region KeyPress Tarima 1
                    if (e.KeyChar == 13 && tabControlTarima.SelectedIndex == 2)
                    {
                        val = this.ValidarTarima(tb.Text);
                        if (!val.ValidacionExitosa)
                        { 
                            DisplayMessage(val.MensajeValidacion, eTipoDisplayMensaje.OK);
                            return;
                        }
                        if(EsValidaTarimaImportar(this.iCodTarima))
                            SetFocus(tbTarimaNo02);
                        this.iCodTarima = -1;
                    }
                    else
                        e.Handled = EsDidigo(e.KeyChar);
                    break;
                    #endregion
                case "tbTarimaNo02":
                    #region KeyPress Tarima 2
                    if (e.KeyChar == 13 && tabControlTarima.SelectedIndex == 2)
                    {
                        val = this.ValidarTarima(tb.Text);
                        if (!val.ValidacionExitosa)
                        {
                            DisplayMessage(val.MensajeValidacion, eTipoDisplayMensaje.OK);
                            return;
                        }
                        if (EsValidaTarimaImportar(this.iCodTarima))
                            SetFocus(cmbTarimaDestino);
                        this.iCodTarima = -1;
                    }
                    else
                        e.Handled = EsDidigo(e.KeyChar);
                    break;
                    #endregion
            }
        }
        #endregion
        #region Eventos DataGrid
        private void dgADPiezaTarima_Click(object sender, EventArgs e)
        {
            DataGrid dg = (sender as DataGrid);
            if (dg.CurrentRowIndex >= 0)
            {
                dg.Select(dg.CurrentRowIndex);
                if (dg.IsSelected(dg.CurrentRowIndex))
                {
                    DataTable dt = dg.DataSource as DataTable;
                    this.iCodPieza = -1;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sCodigoBarra = dt.Rows[dg.CurrentRowIndex]["CodigoBarra"].ToString();
                        this.iCodPieza = this.oDA0.ObtenerCodPiezaCodBarras(sCodigoBarra, false);
                        EstaPiezaSeleccionada = true;
                    }
                }
            }
        }
        private void dgADPiezaTarima_DoubleClick(object sender, EventArgs e)
        {
            DataGrid dg = (sender as DataGrid);
            if (dg.CurrentRowIndex >= 0)
            {
                dg.Select(dg.CurrentRowIndex);
                if (dg.IsSelected(dg.CurrentRowIndex))
                {
                    DataTable dt = dg.DataSource as DataTable;
                    this.iCodPieza = -1;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string sCodigoBarra = dt.Rows[dg.CurrentRowIndex]["CodigoBarra"].ToString();
                        this.iCodPieza = this.oDA0.ObtenerCodPiezaCodBarras(sCodigoBarra, false);
                        EstaPiezaSeleccionada = true;
                        a04_ConsultarPieza frmConsultaPieza = new a04_ConsultarPieza(this.lu);
                        frmConsultaPieza.ShowDialog();
                        frmConsultaPieza.Dispose();
                    }
                }
            }
        }
        #endregion
        #region Metodos Helper Funcional
        private bool EsDidigo(char caracter)
        {
            return (((int)caracter) >= 48 && ((int)caracter) <= 57 || caracter == 8) ? false : true;
        }
        private Validacion ValidarTarima(string sCodTarima)
        {
            Validacion val = new Validacion();
            // Validar el codigo del carro no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sCodTarima))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture número de Tarima";
                this.iCodTarima = -1;
                return val;
            }
            val = oDA0.ValidarEntero(sCodTarima);
            if (!val.ValidacionExitosa) return val;
            this.iCodTarima = Convert.ToInt32(sCodTarima);
            val.ValidacionExitosa = true;
            val.MensajeValidacion = "Capture Piezas";
            return val;
        }
        private Validacion ValidarPieza(string sCodBarras)
        {
            Validacion val = new Validacion();
            DataTable dtObj = null;
            // Validar el codigo de barras no sea una cadena nula o vacia.
            if (string.IsNullOrEmpty(sCodBarras))
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Capture Etiqueta";
                this.iCodPieza = -1;
                return val;
            }
            // Validar exista la pieza.
            this.iCodPieza = this.oDA0.ObtenerCodPiezaCodBarras(sCodBarras, false);
            if (this.iCodPieza == -1)
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza no existe";
                return val;
            }
            // Obtener el estado de la pieza.
            dtObj = this.oDA0.ObtenerEstadoPieza(this.iCodPieza, false);
            this.iCodEstadoPieza = Convert.ToInt32(dtObj.Rows[0]["CodEstadoPieza"]);
            this.sDesEstadoPieza = Convert.ToString(dtObj.Rows[0]["DesEstadoPieza"]);
            // Si la pieza esta 'En Reparacion' o 'En Desperdicio'.
            if (this.iCodEstadoPieza == 2 || this.iCodEstadoPieza == 4)
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza " + this.sDesEstadoPieza;
                this.iCodPieza = -1;
                this.iCodEstadoPieza = -1;
                this.sDesEstadoPieza = string.Empty;
                return val;
            }
            // Obtener el ultimo proceso de la pieza.
            dtObj = this.oDA0.ObtenerUltimoProcesoPieza(this.iCodPieza, false);
            this.iCodUltimoProcesoPieza = Convert.ToInt32(dtObj.Rows[0]["CodProceso"]);
            this.sDesUltimoProcesoPieza = Convert.ToString(dtObj.Rows[0]["DesProceso"]);
            dtObj = this.oDA0.ObtenerCalidadPieza(this.iCodPieza);
            string sCalidadPieza = string.Empty;
            if (dtObj.Rows.Count > 0)
                sCalidadPieza = Convert.ToString(dtObj.Rows[0]["DesCalidad"]);
            // Validar que la pieza solo este en el proceso de Empaque.
            if ((this.iCodUltimoProcesoPieza != this.iCodProcesoAct && iCodUltimoProcesoPieza != oDA0.ObtenerCodProcesoAuditoria()) || string.IsNullOrEmpty(sCalidadPieza))
            {
                val.ValidacionExitosa = false;
                if (string.IsNullOrEmpty(sCalidadPieza))
                    val.MensajeValidacion = "La pieza no tiene asignada calidad\n(pieza en proceso " + this.sDesUltimoProcesoPieza + ").";
                else
                    val.MensajeValidacion = "Pieza en proceso: " + this.sDesUltimoProcesoPieza;
                //
                this.iCodPieza = -1;
                this.iCodEstadoPieza = -1;
                this.sDesEstadoPieza = string.Empty;
                this.iCodUltimoProcesoPieza = -1;
                this.sDesUltimoProcesoPieza = string.Empty;
                return val;
            }
            val.ValidacionExitosa = true;
            val.MensajeValidacion = string.Empty;
            return val;
        }
        private Validacion ValidarTarimaPieza(int iCodPieza)
        {
            //bInsertarPza = true;
            Validacion val = new Validacion();
            int iCodTarima = this.oDA.ExistePiezaEnTarima(iCodPieza);
            if (iCodTarima == -1)
            {
                val.ValidacionExitosa = true;
                val.MensajeValidacion = string.Empty;
            }
            else
            {
                val.ValidacionExitosa = false;
                val.MensajeValidacion = "Pieza asignada a la Tarima: " + iCodTarima.ToString();
            }
            int iTarima = Convert.ToInt32(this.tbTarima.Text);
            int iCodTarimaPadre = oDA.ObtenerPiezaEnTarima(iTarima);
            if (val.ValidacionExitosa)
            {
                ValidacionPieza valPza = this.oDA0.ValidarTarimaPieza(iTarima, iCodPieza, iCodTarimaPadre);
                if (!valPza.MensajeValidacion.ToString().Trim().Equals("OK"))
                {
                    val.ValidacionExitosa = false;
                    val.MensajeValidacion = valPza.MensajeValidacion;

                }
                else
                {
                    val.ValidacionExitosa = true;
                    val.MensajeValidacion = string.Empty;
                    if (valPza.ValNoDefDespExitosa)
                    {
                        //bInsertarPza = false;
                        iTarimaInsertada = iTarima;
                        iPiezaTarimaInsertada = iCodPieza;
                    }
                }
            }
            return val;
        }
        private bool EsTarimaValida(int iCodigoTarima)
        {
            if (iCodigoTarima == 0) return false;
            HHsvc.SCPP_HH proxy = null;
            bool bResult = false, bResultSpecified = true;
            try
            {
                proxy = new HHsvc.SCPP_HH();
                proxy.EsTarimaValida(iCodigoTarima, true, out bResult, out bResultSpecified);
                return bResult;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Tarima cerrada.") || ex.Message.Contains("La tarima ya tiene su capacidad maxima de piezas"))
                {
                    IsDisplayingMessage = true;
                    DialogResult result = MessageBox.Show(ex.Message + "\n ¿Desea entarimar piezas?", "Proceso Entarimado", MessageBoxButtons.YesNo, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                    bResultSpecified = true;
                    bResult = false;
                    proxy = new HHsvc.SCPP_HH();
                    if (result == DialogResult.Yes)
                        proxy.AbrirTarima(iCodigoTarima, true, out bResult, out bResultSpecified);
                    return (bResult) ? true : false;
                }
                else
                {
                    IsDisplayingMessage = true;
                    MessageBox.Show(ex.Message, "Proceso Entarimado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                }
                return false;
            }
            finally
            {
                proxy = null;
            }
        }
        private bool DesEntarimar(int iCodigoPieza)
        {
            HHsvc.SCPP_HH proxy = null;
            bool bResult = false, bResultSpecified = true;
            try
            {
                proxy = new HHsvc.SCPP_HH();
                proxy.DesEnTarimar(iCodPieza, true, out bResult, out bResultSpecified);
                return bResult;
            }
            catch (Exception ex)
            {
                IsDisplayingMessage = true;
                MessageBox.Show(ex.Message, "Proceso Entarimado.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return false;
            }
        }
        private bool EsValidaTarimaImportar(int iCodigoTarima)
        {
            HHsvc.SCPP_HH proxy = null;
            bool EsValidaTarimaImportarResult, EsValidaTarimaImportarResultSpecified;
            try
            {
                proxy = new HHsvc.SCPP_HH();
                proxy.EsValidaTarimaImportar(iCodigoTarima, true, out EsValidaTarimaImportarResult, out EsValidaTarimaImportarResultSpecified);
                return EsValidaTarimaImportarResult;
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, eTipoDisplayMensaje.OK);
                return false;
            }
            finally { proxy = null; }
        }
        private DataTable ObtenerPiezaEnTarima(int iCodigoTarima)
        {
            HHsvc.SCPP_HH proxy = null;
            try
            {
                proxy = new HHsvc.SCPP_HH();
                return proxy.ObtenerPiezaEnTarima(iCodigoTarima, true);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, eTipoDisplayMensaje.OK);
                return null;
            }
            finally { proxy = null; }
        }
        private DataTable ObtenerTarima(int? iCodigoTarima, bool AplicaFiltro, int iCodigoPlanta)
        {
            HHsvc.SCPP_HH proxy = null;
            try
            {
                proxy = new HHsvc.SCPP_HH();
                return (iCodigoTarima.HasValue) ? proxy.ObtenerTarima(iCodigoTarima.Value, true, false, true, iCodigoPlanta, true) : proxy.ObtenerTarima(0, true, true, true, iCodigoPlanta, true);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, eTipoDisplayMensaje.OK);
                return null;
            }
            finally { proxy = null; }
        }
        private void ImportarTarima(int iCodigoTarima01, int iCodigoTarima02, int iCodigoTarimaDestino)
        {
            HHsvc.SCPP_HH proxy = null;
            bool bImportarTarimaResult = false, bImportarTarimaResultSpecified = true;
            try
            {
                proxy = new HHsvc.SCPP_HH();
                proxy.ImportarTarima(iCodigoTarima01, true, iCodigoTarima02, true, iCodigoTarimaDestino, true, out bImportarTarimaResult, out bImportarTarimaResultSpecified);
                if (bImportarTarimaResult)
                    DisplayMessage("La importación se ejecutó con exito.", eTipoDisplayMensaje.OK);
                else
                    DisplayMessage("La importación no se ejecutó con exito.", eTipoDisplayMensaje.OK);
            }
            catch (Exception ex)
            {
                DisplayMessage(ex.Message, eTipoDisplayMensaje.OK);
            }
            finally { proxy = null; }
        }
        private void CargarGridADPiezaTarima(DataGrid dg)
        {
            DataTable dtPiezaEnTarima = null;
            try
            {
                if (!dg.TableStyles.Contains("dtADPiezaTarima") ||
                    !dg.TableStyles["dtADPiezaTarima"].GridColumnStyles.Contains("CodigoTarima") ||
                    !dg.TableStyles["dtADPiezaTarima"].GridColumnStyles.Contains("CodigoBarra") ||
                    !dg.TableStyles["dtADPiezaTarima"].GridColumnStyles.Contains("ClaveSKU"))
                    throw new Exception("Problemas con la estructura \ndel entarimado de piezas.");
                dtPiezaEnTarima = ObtenerPiezaEnTarima(this.iCodTarima);
                if (dtPiezaEnTarima == null || dtPiezaEnTarima.Rows.Count == 0)
                {
                    DisplayMessage("No se tiene registro de entarimado de piezas.", eTipoDisplayMensaje.OK);
                    return;
                }
                dtPiezaEnTarima.TableName = dg.TableStyles["dtADPiezaTarima"].MappingName;
                dtPiezaEnTarima.Columns[0].ColumnName = dg.TableStyles["dtADPiezaTarima"].GridColumnStyles["CodigoTarima"].MappingName;
                dtPiezaEnTarima.Columns[1].ColumnName = dg.TableStyles["dtADPiezaTarima"].GridColumnStyles["CodigoBarra"].MappingName;
                dtPiezaEnTarima.Columns[2].ColumnName = dg.TableStyles["dtADPiezaTarima"].GridColumnStyles["ClaveSKU"].MappingName;
                dg.DataSource = dtPiezaEnTarima;
            }
            catch (Exception ex)
            {
                IsDisplayingMessage = true;
                MessageBox.Show(ex.Message, "Proceso Entarimado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (dtPiezaEnTarima != null) dtPiezaEnTarima.Dispose();
            }
        }
        private void CargarGridConsultaTarima(DataGrid dg)
        {
            DataTable dtTarimas = null;
            try
            {
                if (!dg.TableStyles.Contains("dtConsultaTarima") ||
                    !dg.TableStyles["dtConsultaTarima"].GridColumnStyles.Contains("CodigoTarima") ||
                    !dg.TableStyles["dtConsultaTarima"].GridColumnStyles.Contains("ClaveMaquina") ||
                    !dg.TableStyles["dtConsultaTarima"].GridColumnStyles.Contains("ClaveSKU") ||
                    !dg.TableStyles["dtConsultaTarima"].GridColumnStyles.Contains("Cantidad") ||
                    !dg.TableStyles["dtConsultaTarima"].GridColumnStyles.Contains("Capacidad") ||
                    !dg.TableStyles["dtConsultaTarima"].GridColumnStyles.Contains("Estado") ||
                    !dg.TableStyles["dtConsultaTarima"].GridColumnStyles.Contains("Fecha"))
                    throw new Exception("Problemas con la estructura \nde la consulta de tarima(s).");
                if(this.flagFiltroCodigoTarima)
                    dtTarimas = ObtenerTarima(this.iCodTarima, true, this.lu.CodPlanta);
                else
                    dtTarimas = ObtenerTarima(null, true, this.lu.CodPlanta);
                if (dtTarimas == null || dtTarimas.Rows.Count == 0)
                {
                    DisplayMessage("No se tiene tarima(s) registrada.", eTipoDisplayMensaje.OK);
                    SetLayoutGrid(dgConsultaTarima);
                    return;
                }
                dtTarimas.TableName = dg.TableStyles["dtConsultaTarima"].MappingName;
                dtTarimas.Columns[0].ColumnName = dg.TableStyles["dtConsultaTarima"].GridColumnStyles["CodigoTarima"].MappingName;
                dtTarimas.Columns[2].ColumnName = dg.TableStyles["dtConsultaTarima"].GridColumnStyles["ClaveMaquina"].MappingName;
                dtTarimas.Columns[3].ColumnName = dg.TableStyles["dtConsultaTarima"].GridColumnStyles["ClaveSKU"].MappingName;
                dtTarimas.Columns[4].ColumnName = dg.TableStyles["dtConsultaTarima"].GridColumnStyles["Cantidad"].MappingName;
                dtTarimas.Columns[5].ColumnName = dg.TableStyles["dtConsultaTarima"].GridColumnStyles["Capacidad"].MappingName;
                dtTarimas.Columns[6].ColumnName = dg.TableStyles["dtConsultaTarima"].GridColumnStyles["Estado"].MappingName;
                dtTarimas.Columns[7].ColumnName = dg.TableStyles["dtConsultaTarima"].GridColumnStyles["Fecha"].MappingName;
                dg.DataSource = dtTarimas;
            }
            catch (Exception ex)
            {
                IsDisplayingMessage = true;
                MessageBox.Show(ex.Message, "Proceso Entarimado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                if (dtTarimas != null) dtTarimas.Dispose();
            }
        }
        private int EnTarimarPieza(int iCodigoTarima, string sCodigoBarraPieza)
        {
            if (string.IsNullOrEmpty(sCodigoBarraPieza)) return 0;
            HHsvc.SCPP_HH proxy = null;
            try
            {
                int iResultTarima = 0;
                bool bResultTarima = true;
                proxy = new HHsvc.SCPP_HH();
                proxy.EnTarimadoPieza(this.iCodTarima, true, sCodigoBarraPieza, out iResultTarima, out bResultTarima);
                return iResultTarima;
            }
            catch (Exception ex)
            {
                IsDisplayingMessage = true;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return 0;
            }
            finally
            {
                proxy = null;
            }
        }
        private bool CerrarTarima()
        {
            if (this.iCodTarima <= 0) return false;
            HHsvc.SCPP_HH proxy = null;
            try
            {
                bool bResult = false, bResultSpecified = true;
                proxy = new HHsvc.SCPP_HH();
                proxy.CerrarTarima(this.iCodTarima, true, out bResult, out bResultSpecified);
                return bResult;
            }
            catch (Exception ex)
            {
                IsDisplayingMessage = true;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                return false;
            }
            finally
            {
                proxy = null;
            }
        }
        private HHsvc.ConfigImpresora ObtenerConfiguracionImpresora()
        {
            HHsvc.ConfigImpresora cConfigImpresora = new HHsvc.ConfigImpresora();
            cConfigImpresora.CodPlanta = lu.CodPlanta;
            cConfigImpresora.CodMaquina = this.lu.CodMaquina;
            cConfigImpresora.CodCentroTrabajo = this.lu.CodCentroTrabajo;
            cConfigImpresora.CodCentroTrabajoSpecified = true;
            cConfigImpresora.CodMaquinaSpecified = true;
            cConfigImpresora.CodPlantaSpecified = true;
            return cConfigImpresora;
        }
        private HHsvc.Etiqueta ObtenerConfiguracionEtiqueta()
        {
            string sSKU = ObtenerDatosGrid(dgADPiezaTarima, "ClaveSKU");
            int iCodModelo = this.oDA0.ExisteModelo(sSKU);
            HHsvc.Etiqueta eEtiqueta = new HHsvc.Etiqueta();
            eEtiqueta.Clave = sSKU;
            eEtiqueta.Cod = iCodModelo;
            string sCodigoBarra = ObtenerDatosGrid(dgADPiezaTarima, "CodigoBarra");
            eEtiqueta.Pieza = sCodigoBarra;
            eEtiqueta.Tarima = this.iCodTarima.ToString();
            eEtiqueta.CodSpecified = true;
            return eEtiqueta;
        }
        private void ImprimirEtiquetaTarima()
        {
            HHsvc.Etiqueta eEtiqueta = null;
            HHsvc.ConfigImpresora cConfigImpresora = null;
            HHsvc.SCPP_HH proxy = null;
            try
            {
                eEtiqueta = ObtenerConfiguracionEtiqueta();
                cConfigImpresora = ObtenerConfiguracionImpresora();
                eEtiqueta.TipoEtiqueta = 2;
                eEtiqueta.TipoEtiquetaSpecified = true;
                proxy = new HHsvc.SCPP_HH();
                proxy.ImprimirEtiqueta(cConfigImpresora, eEtiqueta);
            }
            catch (Exception ex)
            {
                IsDisplayingMessage = true;
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                eEtiqueta = null;
                cConfigImpresora = null;
                proxy = null;
            }
        }
        private string ObtenerDatosGrid(DataGrid dg ,string columName)
        {
            DataTable dt = dg.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                return dt.Rows[0][columName].ToString();
            }
            return string.Empty;
        }
        private void SetItemsCombo(ComboBox cmb, object[] source)
        {
            cmb.Items.Clear();
            if (cmb == null || source == null) return;
            foreach (object item in source)
                cmb.Items.Add(item);
        }
        private void ResetearCapturaTarima()
        {
            SetFocus(tbTarima);
            SetVisible(dgADPiezaTarima, true);
            SetVisible(tbDescPiezaEntarimar, false);
            dgADPiezaTarima.DataSource = null;
            tbPiezasEnTarima.Text = string.Empty;
        }
        private void ResetearCapturaConsulta()
        {
            this.tbTarima.Text = string.Empty;
            SetFocus(btnConsultarTodasTarimas);
        }
        private void ResetearCapturaImportar()
        {
            this.tbTarimaNo01.Text = string.Empty;
            this.tbTarimaNo02.Text = string.Empty;
            this.cmbTarimaDestino.SelectedIndex = 0;
            SetFocus(tbTarimaNo01);
        }
        private void SetVisible(Control ctrl, bool EsVisible)
        {
            ctrl.Visible = EsVisible;
        }
        private void ResetearCapturaPieza()
        {
            SetVisible(dgADPiezaTarima, true);
            SetVisible(tbDescPiezaEntarimar, false);
            SetFocus(tbEtiqueta);
        }
        #endregion
        private void mnuItemSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        #region Eventos Control Button
        private void btnDesEntarimar_Click(object sender, EventArgs e)
        {
            if (this.iCodPieza <= 0 || !EstaPiezaSeleccionada)
            {
                IsDisplayingMessage = true;
                MessageBox.Show("Seleccione pieza ha desentarimar.", "Proceso Entarimado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                return;
            }
            if (DesEntarimar(this.iCodPieza))
            {
                IsDisplayingMessage = true;
                MessageBox.Show("La pieza se desasigno de la tarima.", "Proceso Entarimado", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
                InicializarControl(dgADPiezaTarima);
                DataTable dtTarima = dgADPiezaTarima.DataSource as DataTable;
                if (dtTarima == null) return;
                tbPiezasEnTarima.Text = dtTarima.Rows.Count.ToString();
            }
            EstaPiezaSeleccionada = false;
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (this.iCodTarima == -1)
            {
                DisplayMessage("Capture número de tarima.", eTipoDisplayMensaje.OK);
                this.tbTarima.Focus();
                return;
            }
            if (this.iCodPieza == -1 || this.tbEtiqueta.Text.Trim() == string.Empty)
            {
                DisplayMessage("Capture etiqueta.", eTipoDisplayMensaje.OK);
                this.tbEtiqueta.Focus();
                return;
            }
            int iCodigoTarima = EnTarimarPieza(Convert.ToInt32(this.tbTarima.Text), this.tbEtiqueta.Text.Trim());
            if (iCodigoTarima > 0)
                DisplayMessage("Pieza entarimada, tarima: " + iCodigoTarima.ToString(), eTipoDisplayMensaje.OK);
            else
            {
                DisplayMessage("No se puede entarimar ni procesar la pieza, consulte al administrador.", eTipoDisplayMensaje.OK);
                return;
            }
            InicializarControl(dgADPiezaTarima);
            DataTable dt = dgADPiezaTarima.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
                this.tbPiezasEnTarima.Text = dt.Rows.Count.ToString();
            else
                this.tbPiezasEnTarima.Text = (0).ToString();
            DataTable dtTarima = ObtenerTarima(this.iCodTarima, false, this.lu.CodPlanta);
            if (dtTarima == null || dtTarima.Rows.Count == 0) return;
            if (Convert.ToInt32(this.tbPiezasEnTarima.Text) >= Convert.ToInt32(dtTarima.Rows[0]["Capacidad"]))
            {
                DisplayMessage("La tarima ya llego a su capacidad de piezas.", eTipoDisplayMensaje.OK);
                ResetearCapturaTarima();
            }
            else
                ResetearCapturaPieza();
        }
        private void btnCerrarTarima_Click(object sender, EventArgs e)
        {
            IsDisplayingMessage = true;
            DialogResult result = MessageBox.Show("¿Desea cerrar la tarima " + this.iCodTarima.ToString() + ".?", this.Text + ": Cierre de Tarima", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.No) return;
            this.CerrarTarima();
            DataTable dt = dgADPiezaTarima.DataSource as DataTable;
            if (dt != null && dt.Rows.Count > 0)
            {
                IsDisplayingMessage = true;
                result = MessageBox.Show("¿Desea imprimir etiqueta de tarima.?", this.Text + ": Cierre de Tarima", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Yes)
                    this.ImprimirEtiquetaTarima();
            }
            ResetearCapturaTarima();
        }
        private void btnConsultarTarima_Click(object sender, EventArgs e)
        {
            this.flagFiltroCodigoTarima = true;
            InicializarControl(dgConsultaTarima);
        }
        private void btnConsultarTodasTarimas_Click(object sender, EventArgs e)
        {
            this.flagFiltroCodigoTarima = false;
            this.ResetearCapturaConsulta();
            InicializarControl(dgConsultaTarima);
        }
        private void btnCancelarImportar_Click(object sender, EventArgs e)
        {
            this.ResetearCapturaImportar();
        }
        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (this.tbTarimaNo01.Text.Trim() == string.Empty || this.tbTarimaNo02.Text.Trim() == string.Empty)
            {
                DisplayMessage("Favor de ingresar nuevamente la captura para importar tarima.", eTipoDisplayMensaje.OK);
                this.btnCancelarImportar_Click(sender, e);
                return;
            }
            //string sTarimaDestino = this.cmbTarimaDestino.Items[this.cmbTarimaDestino.SelectedIndex].ToString().Split('-')[1].Trim();
            switch (this.cmbTarimaDestino.SelectedIndex)
            {
                case 0:
                    DisplayMessage("Favor de seleccionar la tarima destino.", eTipoDisplayMensaje.OK);
                    break;
                case 1:
                    this.ImportarTarima(Convert.ToInt32(this.tbTarimaNo01.Text), Convert.ToInt32(this.tbTarimaNo02.Text), Convert.ToInt32(this.tbTarimaNo01.Text));
                    break;
                case 2:
                    this.ImportarTarima(Convert.ToInt32(this.tbTarimaNo01.Text), Convert.ToInt32(this.tbTarimaNo02.Text), Convert.ToInt32(this.tbTarimaNo02.Text));
                    break;
            }
            this.ResetearCapturaImportar();
        }
        #endregion
    }
}