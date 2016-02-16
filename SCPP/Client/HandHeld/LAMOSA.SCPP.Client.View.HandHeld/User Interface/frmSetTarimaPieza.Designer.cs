namespace LAMOSA.SCPP.Client.View.HandHeld.User_Interface
{
    partial class frmSetTarimaPieza
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbTarima = new System.Windows.Forms.TextBox();
            this.lbTarima = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCerrarTarima = new System.Windows.Forms.Button();
            this.mnuConfigTarima = new System.Windows.Forms.MainMenu();
            this.mnuItemSalir = new System.Windows.Forms.MenuItem();
            this.tabPageImportarTarima = new System.Windows.Forms.TabPage();
            this.btnCancelarImportar = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.cmbTarimaDestino = new System.Windows.Forms.ComboBox();
            this.lblTarimaDestino = new System.Windows.Forms.Label();
            this.tbTarimaNo02 = new System.Windows.Forms.TextBox();
            this.tbTarimaNo01 = new System.Windows.Forms.TextBox();
            this.lblTarimaNo02 = new System.Windows.Forms.Label();
            this.lblTarimaNo01 = new System.Windows.Forms.Label();
            this.tabPageConsultaTarima = new System.Windows.Forms.TabPage();
            this.btnConsultarTodasTarimas = new System.Windows.Forms.Button();
            this.btnConsultarTarima = new System.Windows.Forms.Button();
            this.dgConsultaTarima = new System.Windows.Forms.DataGrid();
            this.dtConsultaTarima = new System.Windows.Forms.DataGridTableStyle();
            this.dtColConsultaTarima = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtColConsultaLineaEmpaque = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtColConsultaSKU = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtColConsultaCantidad = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtColConsultaCapacidad = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtColConsultaEstado = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtColConsultaFecha = new System.Windows.Forms.DataGridTextBoxColumn();
            this.tabPageADPiezaTarima = new System.Windows.Forms.TabPage();
            this.btnDesEntarimar = new System.Windows.Forms.Button();
            this.tbPiezasEnTarima = new System.Windows.Forms.TextBox();
            this.dgADPiezaTarima = new System.Windows.Forms.DataGrid();
            this.dtADPiezaTarima = new System.Windows.Forms.DataGridTableStyle();
            this.dtColADTarima = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtColADPieza = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dtColADSKU = new System.Windows.Forms.DataGridTextBoxColumn();
            this.tbEtiqueta = new System.Windows.Forms.TextBox();
            this.lblPiezaEnTarima = new System.Windows.Forms.Label();
            this.lblEtiqueta = new System.Windows.Forms.Label();
            this.tbDescPiezaEntarimar = new System.Windows.Forms.TextBox();
            this.tabControlTarima = new System.Windows.Forms.TabControl();
            this.tabPageImportarTarima.SuspendLayout();
            this.tabPageConsultaTarima.SuspendLayout();
            this.tabPageADPiezaTarima.SuspendLayout();
            this.tabControlTarima.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTarima
            // 
            this.tbTarima.Location = new System.Drawing.Point(82, 12);
            this.tbTarima.MaxLength = 15;
            this.tbTarima.Name = "tbTarima";
            this.tbTarima.Size = new System.Drawing.Size(143, 23);
            this.tbTarima.TabIndex = 1;
            this.tbTarima.GotFocus += new System.EventHandler(this.TextBoxControl_GotFocus);
            this.tbTarima.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxControl_KeyPress);
            // 
            // lbTarima
            // 
            this.lbTarima.Location = new System.Drawing.Point(22, 13);
            this.lbTarima.Name = "lbTarima";
            this.lbTarima.Size = new System.Drawing.Size(56, 22);
            this.lbTarima.Text = "Tarima:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(80, 211);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(72, 20);
            this.btnAceptar.TabIndex = 13;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCerrarTarima
            // 
            this.btnCerrarTarima.Location = new System.Drawing.Point(152, 211);
            this.btnCerrarTarima.Name = "btnCerrarTarima";
            this.btnCerrarTarima.Size = new System.Drawing.Size(72, 20);
            this.btnCerrarTarima.TabIndex = 14;
            this.btnCerrarTarima.TabStop = false;
            this.btnCerrarTarima.Text = "Cerrar";
            this.btnCerrarTarima.Click += new System.EventHandler(this.btnCerrarTarima_Click);
            // 
            // mnuConfigTarima
            // 
            this.mnuConfigTarima.MenuItems.Add(this.mnuItemSalir);
            // 
            // mnuItemSalir
            // 
            this.mnuItemSalir.Text = "Salir";
            this.mnuItemSalir.Click += new System.EventHandler(this.mnuItemSalir_Click);
            // 
            // tabPageImportarTarima
            // 
            this.tabPageImportarTarima.Controls.Add(this.btnCancelarImportar);
            this.tabPageImportarTarima.Controls.Add(this.btnImportar);
            this.tabPageImportarTarima.Controls.Add(this.cmbTarimaDestino);
            this.tabPageImportarTarima.Controls.Add(this.lblTarimaDestino);
            this.tabPageImportarTarima.Controls.Add(this.tbTarimaNo02);
            this.tabPageImportarTarima.Controls.Add(this.tbTarimaNo01);
            this.tabPageImportarTarima.Controls.Add(this.lblTarimaNo02);
            this.tabPageImportarTarima.Controls.Add(this.lblTarimaNo01);
            this.tabPageImportarTarima.Location = new System.Drawing.Point(4, 25);
            this.tabPageImportarTarima.Name = "tabPageImportarTarima";
            this.tabPageImportarTarima.Size = new System.Drawing.Size(230, 232);
            this.tabPageImportarTarima.Text = "Importar";
            // 
            // btnCancelarImportar
            // 
            this.btnCancelarImportar.Location = new System.Drawing.Point(32, 180);
            this.btnCancelarImportar.Name = "btnCancelarImportar";
            this.btnCancelarImportar.Size = new System.Drawing.Size(72, 20);
            this.btnCancelarImportar.TabIndex = 97;
            this.btnCancelarImportar.Text = "Cancelar";
            this.btnCancelarImportar.Click += new System.EventHandler(this.btnCancelarImportar_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Location = new System.Drawing.Point(128, 180);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(72, 20);
            this.btnImportar.TabIndex = 96;
            this.btnImportar.Text = "Importar";
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // cmbTarimaDestino
            // 
            this.cmbTarimaDestino.Items.Add("Seleccione Destino");
            this.cmbTarimaDestino.Items.Add("Tarima 1");
            this.cmbTarimaDestino.Items.Add("Tarima 2");
            this.cmbTarimaDestino.Location = new System.Drawing.Point(81, 140);
            this.cmbTarimaDestino.Name = "cmbTarimaDestino";
            this.cmbTarimaDestino.Size = new System.Drawing.Size(143, 23);
            this.cmbTarimaDestino.TabIndex = 95;
            // 
            // lblTarimaDestino
            // 
            this.lblTarimaDestino.Location = new System.Drawing.Point(8, 143);
            this.lblTarimaDestino.Name = "lblTarimaDestino";
            this.lblTarimaDestino.Size = new System.Drawing.Size(70, 20);
            this.lblTarimaDestino.Text = "Destino:";
            // 
            // tbTarimaNo02
            // 
            this.tbTarimaNo02.Location = new System.Drawing.Point(81, 111);
            this.tbTarimaNo02.MaxLength = 15;
            this.tbTarimaNo02.Name = "tbTarimaNo02";
            this.tbTarimaNo02.Size = new System.Drawing.Size(143, 23);
            this.tbTarimaNo02.TabIndex = 92;
            this.tbTarimaNo02.GotFocus += new System.EventHandler(this.TextBoxControl_GotFocus);
            this.tbTarimaNo02.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxControl_KeyPress);
            // 
            // tbTarimaNo01
            // 
            this.tbTarimaNo01.Location = new System.Drawing.Point(81, 85);
            this.tbTarimaNo01.MaxLength = 15;
            this.tbTarimaNo01.Name = "tbTarimaNo01";
            this.tbTarimaNo01.Size = new System.Drawing.Size(143, 23);
            this.tbTarimaNo01.TabIndex = 91;
            this.tbTarimaNo01.GotFocus += new System.EventHandler(this.TextBoxControl_GotFocus);
            this.tbTarimaNo01.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxControl_KeyPress);
            // 
            // lblTarimaNo02
            // 
            this.lblTarimaNo02.Location = new System.Drawing.Point(8, 114);
            this.lblTarimaNo02.Name = "lblTarimaNo02";
            this.lblTarimaNo02.Size = new System.Drawing.Size(70, 20);
            this.lblTarimaNo02.Text = "Tarima 2:";
            // 
            // lblTarimaNo01
            // 
            this.lblTarimaNo01.Location = new System.Drawing.Point(8, 88);
            this.lblTarimaNo01.Name = "lblTarimaNo01";
            this.lblTarimaNo01.Size = new System.Drawing.Size(70, 20);
            this.lblTarimaNo01.Text = "Tarima 1:";
            // 
            // tabPageConsultaTarima
            // 
            this.tabPageConsultaTarima.Controls.Add(this.btnConsultarTodasTarimas);
            this.tabPageConsultaTarima.Controls.Add(this.btnConsultarTarima);
            this.tabPageConsultaTarima.Controls.Add(this.dgConsultaTarima);
            this.tabPageConsultaTarima.Location = new System.Drawing.Point(4, 25);
            this.tabPageConsultaTarima.Name = "tabPageConsultaTarima";
            this.tabPageConsultaTarima.Size = new System.Drawing.Size(230, 232);
            this.tabPageConsultaTarima.Text = "Consulta";
            // 
            // btnConsultarTodasTarimas
            // 
            this.btnConsultarTodasTarimas.Location = new System.Drawing.Point(76, 210);
            this.btnConsultarTodasTarimas.Name = "btnConsultarTodasTarimas";
            this.btnConsultarTodasTarimas.Size = new System.Drawing.Size(72, 20);
            this.btnConsultarTodasTarimas.TabIndex = 1;
            this.btnConsultarTodasTarimas.TabStop = false;
            this.btnConsultarTodasTarimas.Text = "Todas";
            this.btnConsultarTodasTarimas.Click += new System.EventHandler(this.btnConsultarTodasTarimas_Click);
            // 
            // btnConsultarTarima
            // 
            this.btnConsultarTarima.Location = new System.Drawing.Point(151, 210);
            this.btnConsultarTarima.Name = "btnConsultarTarima";
            this.btnConsultarTarima.Size = new System.Drawing.Size(72, 20);
            this.btnConsultarTarima.TabIndex = 0;
            this.btnConsultarTarima.TabStop = false;
            this.btnConsultarTarima.Text = "Consultar";
            this.btnConsultarTarima.Click += new System.EventHandler(this.btnConsultarTarima_Click);
            // 
            // dgConsultaTarima
            // 
            this.dgConsultaTarima.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgConsultaTarima.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dgConsultaTarima.Location = new System.Drawing.Point(8, 49);
            this.dgConsultaTarima.Name = "dgConsultaTarima";
            this.dgConsultaTarima.RowHeadersVisible = false;
            this.dgConsultaTarima.Size = new System.Drawing.Size(216, 155);
            this.dgConsultaTarima.TabIndex = 0;
            this.dgConsultaTarima.TableStyles.Add(this.dtConsultaTarima);
            this.dgConsultaTarima.TabStop = false;
            // 
            // dtConsultaTarima
            // 
            this.dtConsultaTarima.GridColumnStyles.Add(this.dtColConsultaTarima);
            this.dtConsultaTarima.GridColumnStyles.Add(this.dtColConsultaLineaEmpaque);
            this.dtConsultaTarima.GridColumnStyles.Add(this.dtColConsultaSKU);
            this.dtConsultaTarima.GridColumnStyles.Add(this.dtColConsultaCantidad);
            this.dtConsultaTarima.GridColumnStyles.Add(this.dtColConsultaCapacidad);
            this.dtConsultaTarima.GridColumnStyles.Add(this.dtColConsultaEstado);
            this.dtConsultaTarima.GridColumnStyles.Add(this.dtColConsultaFecha);
            this.dtConsultaTarima.MappingName = "dtConsultaTarima";
            // 
            // dtColConsultaTarima
            // 
            this.dtColConsultaTarima.Format = "";
            this.dtColConsultaTarima.FormatInfo = null;
            this.dtColConsultaTarima.HeaderText = "Tarima";
            this.dtColConsultaTarima.MappingName = "CodigoTarima";
            this.dtColConsultaTarima.Width = 55;
            // 
            // dtColConsultaLineaEmpaque
            // 
            this.dtColConsultaLineaEmpaque.Format = "";
            this.dtColConsultaLineaEmpaque.FormatInfo = null;
            this.dtColConsultaLineaEmpaque.HeaderText = "Linea";
            this.dtColConsultaLineaEmpaque.MappingName = "ClaveMaquina";
            this.dtColConsultaLineaEmpaque.Width = 40;
            // 
            // dtColConsultaSKU
            // 
            this.dtColConsultaSKU.Format = "";
            this.dtColConsultaSKU.FormatInfo = null;
            this.dtColConsultaSKU.HeaderText = "SKU";
            this.dtColConsultaSKU.MappingName = "ClaveSKU";
            this.dtColConsultaSKU.Width = 60;
            // 
            // dtColConsultaCantidad
            // 
            this.dtColConsultaCantidad.Format = "";
            this.dtColConsultaCantidad.FormatInfo = null;
            this.dtColConsultaCantidad.HeaderText = "Pzs.";
            this.dtColConsultaCantidad.MappingName = "Cantidad";
            this.dtColConsultaCantidad.Width = 25;
            // 
            // dtColConsultaCapacidad
            // 
            this.dtColConsultaCapacidad.Format = "";
            this.dtColConsultaCapacidad.FormatInfo = null;
            this.dtColConsultaCapacidad.HeaderText = "Cap.";
            this.dtColConsultaCapacidad.MappingName = "Capacidad";
            this.dtColConsultaCapacidad.Width = 28;
            // 
            // dtColConsultaEstado
            // 
            this.dtColConsultaEstado.Format = "";
            this.dtColConsultaEstado.FormatInfo = null;
            this.dtColConsultaEstado.HeaderText = "Estado";
            this.dtColConsultaEstado.MappingName = "Estado";
            this.dtColConsultaEstado.Width = 45;
            // 
            // dtColConsultaFecha
            // 
            this.dtColConsultaFecha.Format = "dd/MM/yyyy";
            this.dtColConsultaFecha.FormatInfo = null;
            this.dtColConsultaFecha.HeaderText = "Fecha";
            this.dtColConsultaFecha.MappingName = "Fecha";
            this.dtColConsultaFecha.Width = 60;
            // 
            // tabPageADPiezaTarima
            // 
            this.tabPageADPiezaTarima.Controls.Add(this.btnAceptar);
            this.tabPageADPiezaTarima.Controls.Add(this.btnCerrarTarima);
            this.tabPageADPiezaTarima.Controls.Add(this.btnDesEntarimar);
            this.tabPageADPiezaTarima.Controls.Add(this.tbPiezasEnTarima);
            this.tabPageADPiezaTarima.Controls.Add(this.dgADPiezaTarima);
            this.tabPageADPiezaTarima.Controls.Add(this.tbEtiqueta);
            this.tabPageADPiezaTarima.Controls.Add(this.lblPiezaEnTarima);
            this.tabPageADPiezaTarima.Controls.Add(this.lblEtiqueta);
            this.tabPageADPiezaTarima.Controls.Add(this.tbDescPiezaEntarimar);
            this.tabPageADPiezaTarima.Location = new System.Drawing.Point(4, 25);
            this.tabPageADPiezaTarima.Name = "tabPageADPiezaTarima";
            this.tabPageADPiezaTarima.Size = new System.Drawing.Size(230, 232);
            this.tabPageADPiezaTarima.Text = "A/D";
            // 
            // btnDesEntarimar
            // 
            this.btnDesEntarimar.Location = new System.Drawing.Point(8, 211);
            this.btnDesEntarimar.Name = "btnDesEntarimar";
            this.btnDesEntarimar.Size = new System.Drawing.Size(72, 20);
            this.btnDesEntarimar.TabIndex = 0;
            this.btnDesEntarimar.TabStop = false;
            this.btnDesEntarimar.Text = "Quitar";
            this.btnDesEntarimar.Click += new System.EventHandler(this.btnDesEntarimar_Click);
            // 
            // tbPiezasEnTarima
            // 
            this.tbPiezasEnTarima.Location = new System.Drawing.Point(126, 184);
            this.tbPiezasEnTarima.MaxLength = 2;
            this.tbPiezasEnTarima.Name = "tbPiezasEnTarima";
            this.tbPiezasEnTarima.ReadOnly = true;
            this.tbPiezasEnTarima.Size = new System.Drawing.Size(34, 23);
            this.tbPiezasEnTarima.TabIndex = 0;
            this.tbPiezasEnTarima.TabStop = false;
            // 
            // dgADPiezaTarima
            // 
            this.dgADPiezaTarima.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgADPiezaTarima.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dgADPiezaTarima.Location = new System.Drawing.Point(10, 66);
            this.dgADPiezaTarima.Name = "dgADPiezaTarima";
            this.dgADPiezaTarima.RowHeadersVisible = false;
            this.dgADPiezaTarima.Size = new System.Drawing.Size(213, 112);
            this.dgADPiezaTarima.TabIndex = 0;
            this.dgADPiezaTarima.TableStyles.Add(this.dtADPiezaTarima);
            this.dgADPiezaTarima.TabStop = false;
            this.dgADPiezaTarima.DoubleClick += new System.EventHandler(this.dgADPiezaTarima_DoubleClick);
            this.dgADPiezaTarima.Click += new System.EventHandler(this.dgADPiezaTarima_Click);
            // 
            // dtADPiezaTarima
            // 
            this.dtADPiezaTarima.GridColumnStyles.Add(this.dtColADTarima);
            this.dtADPiezaTarima.GridColumnStyles.Add(this.dtColADPieza);
            this.dtADPiezaTarima.GridColumnStyles.Add(this.dtColADSKU);
            this.dtADPiezaTarima.MappingName = "dtADPiezaTarima";
            // 
            // dtColADTarima
            // 
            this.dtColADTarima.Format = "";
            this.dtColADTarima.FormatInfo = null;
            this.dtColADTarima.HeaderText = "Tarima";
            this.dtColADTarima.MappingName = "CodigoTarima";
            this.dtColADTarima.Width = 60;
            // 
            // dtColADPieza
            // 
            this.dtColADPieza.Format = "";
            this.dtColADPieza.FormatInfo = null;
            this.dtColADPieza.HeaderText = "Pieza";
            this.dtColADPieza.MappingName = "CodigoBarra";
            this.dtColADPieza.Width = 65;
            // 
            // dtColADSKU
            // 
            this.dtColADSKU.Format = "";
            this.dtColADSKU.FormatInfo = null;
            this.dtColADSKU.HeaderText = "SKU";
            this.dtColADSKU.MappingName = "ClaveSKU";
            this.dtColADSKU.Width = 60;
            // 
            // tbEtiqueta
            // 
            this.tbEtiqueta.Location = new System.Drawing.Point(81, 37);
            this.tbEtiqueta.MaxLength = 15;
            this.tbEtiqueta.Name = "tbEtiqueta";
            this.tbEtiqueta.Size = new System.Drawing.Size(142, 23);
            this.tbEtiqueta.TabIndex = 11;
            this.tbEtiqueta.GotFocus += new System.EventHandler(this.TextBoxControl_GotFocus);
            this.tbEtiqueta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxControl_KeyPress);
            // 
            // lblPiezaEnTarima
            // 
            this.lblPiezaEnTarima.Location = new System.Drawing.Point(14, 184);
            this.lblPiezaEnTarima.Name = "lblPiezaEnTarima";
            this.lblPiezaEnTarima.Size = new System.Drawing.Size(114, 23);
            this.lblPiezaEnTarima.Text = "Piezas en Tarima:";
            // 
            // lblEtiqueta
            // 
            this.lblEtiqueta.Location = new System.Drawing.Point(11, 37);
            this.lblEtiqueta.Name = "lblEtiqueta";
            this.lblEtiqueta.Size = new System.Drawing.Size(70, 23);
            this.lblEtiqueta.Text = "Etiqueta:";
            // 
            // tbDescPiezaEntarimar
            // 
            this.tbDescPiezaEntarimar.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbDescPiezaEntarimar.Location = new System.Drawing.Point(10, 66);
            this.tbDescPiezaEntarimar.Multiline = true;
            this.tbDescPiezaEntarimar.Name = "tbDescPiezaEntarimar";
            this.tbDescPiezaEntarimar.ReadOnly = true;
            this.tbDescPiezaEntarimar.Size = new System.Drawing.Size(213, 110);
            this.tbDescPiezaEntarimar.TabIndex = 0;
            this.tbDescPiezaEntarimar.TabStop = false;
            this.tbDescPiezaEntarimar.Visible = false;
            // 
            // tabControlTarima
            // 
            this.tabControlTarima.Controls.Add(this.tabPageADPiezaTarima);
            this.tabControlTarima.Controls.Add(this.tabPageConsultaTarima);
            this.tabControlTarima.Controls.Add(this.tabPageImportarTarima);
            this.tabControlTarima.Location = new System.Drawing.Point(0, 56);
            this.tabControlTarima.Name = "tabControlTarima";
            this.tabControlTarima.SelectedIndex = 0;
            this.tabControlTarima.Size = new System.Drawing.Size(238, 261);
            this.tabControlTarima.TabIndex = 0;
            this.tabControlTarima.TabStop = false;
            this.tabControlTarima.SelectedIndexChanged += new System.EventHandler(this.tabControlTarima_SelectedIndexChanged);
            // 
            // frmSetTarimaPieza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 320);
            this.ControlBox = false;
            this.Controls.Add(this.lbTarima);
            this.Controls.Add(this.tbTarima);
            this.Controls.Add(this.tabControlTarima);
            this.Menu = this.mnuConfigTarima;
            this.Name = "frmSetTarimaPieza";
            this.Text = "Configuración Tarima";
            this.Load += new System.EventHandler(this.frmSetTarimaPieza_Load);
            this.tabPageImportarTarima.ResumeLayout(false);
            this.tabPageConsultaTarima.ResumeLayout(false);
            this.tabPageADPiezaTarima.ResumeLayout(false);
            this.tabControlTarima.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbTarima;
        private System.Windows.Forms.Label lbTarima;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCerrarTarima;
        private System.Windows.Forms.MainMenu mnuConfigTarima;
        private System.Windows.Forms.TabPage tabPageImportarTarima;
        private System.Windows.Forms.TabPage tabPageConsultaTarima;
        private System.Windows.Forms.Button btnConsultarTarima;
        private System.Windows.Forms.DataGrid dgConsultaTarima;
        private System.Windows.Forms.DataGridTableStyle dtConsultaTarima;
        private System.Windows.Forms.DataGridTextBoxColumn dtColConsultaLineaEmpaque;
        private System.Windows.Forms.DataGridTextBoxColumn dtColConsultaSKU;
        private System.Windows.Forms.DataGridTextBoxColumn dtColConsultaCantidad;
        private System.Windows.Forms.DataGridTextBoxColumn dtColConsultaCapacidad;
        private System.Windows.Forms.DataGridTextBoxColumn dtColConsultaEstado;
        private System.Windows.Forms.DataGridTextBoxColumn dtColConsultaFecha;
        private System.Windows.Forms.TabPage tabPageADPiezaTarima;
        private System.Windows.Forms.TextBox tbDescPiezaEntarimar;
        private System.Windows.Forms.Button btnDesEntarimar;
        private System.Windows.Forms.TextBox tbPiezasEnTarima;
        private System.Windows.Forms.DataGrid dgADPiezaTarima;
        private System.Windows.Forms.DataGridTableStyle dtADPiezaTarima;
        private System.Windows.Forms.DataGridTextBoxColumn dtColADTarima;
        private System.Windows.Forms.DataGridTextBoxColumn dtColADPieza;
        private System.Windows.Forms.DataGridTextBoxColumn dtColADSKU;
        private System.Windows.Forms.TextBox tbEtiqueta;
        private System.Windows.Forms.Label lblPiezaEnTarima;
        private System.Windows.Forms.Label lblEtiqueta;
        private System.Windows.Forms.TabControl tabControlTarima;
        private System.Windows.Forms.Label lblTarimaNo01;
        private System.Windows.Forms.Label lblTarimaDestino;
        private System.Windows.Forms.TextBox tbTarimaNo02;
        private System.Windows.Forms.TextBox tbTarimaNo01;
        private System.Windows.Forms.Label lblTarimaNo02;
        private System.Windows.Forms.ComboBox cmbTarimaDestino;
        private System.Windows.Forms.Button btnCancelarImportar;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.MenuItem mnuItemSalir;
        private System.Windows.Forms.Button btnConsultarTodasTarimas;
        private System.Windows.Forms.DataGridTextBoxColumn dtColConsultaTarima;

    }
}