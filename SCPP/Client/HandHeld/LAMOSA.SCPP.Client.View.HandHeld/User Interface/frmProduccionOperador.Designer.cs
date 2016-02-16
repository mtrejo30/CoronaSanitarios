namespace LAMOSA.SCPP.Client.View.HandHeld.User_Interface
{
    partial class frmProduccionOperador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgDetalleProduccion = new System.Windows.Forms.DataGrid();
            this.dgtblDetalleProduccion = new System.Windows.Forms.DataGridTableStyle();
            this.dgtblColArticulo = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtblColPiezasBuenas = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dgtblColPiezasDesperdicio = new System.Windows.Forms.DataGridTextBoxColumn();
            this.lblDetalleProduccion = new System.Windows.Forms.Label();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.SuspendLayout();
            // 
            // dgDetalleProduccion
            // 
            this.dgDetalleProduccion.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgDetalleProduccion.Location = new System.Drawing.Point(3, 115);
            this.dgDetalleProduccion.Name = "dgDetalleProduccion";
            this.dgDetalleProduccion.RowHeadersVisible = false;
            this.dgDetalleProduccion.Size = new System.Drawing.Size(231, 151);
            this.dgDetalleProduccion.TabIndex = 0;
            this.dgDetalleProduccion.TableStyles.Add(this.dgtblDetalleProduccion);
            this.dgDetalleProduccion.TabStop = false;
            // 
            // dgtblDetalleProduccion
            // 
            this.dgtblDetalleProduccion.GridColumnStyles.Add(this.dgtblColArticulo);
            this.dgtblDetalleProduccion.GridColumnStyles.Add(this.dgtblColPiezasBuenas);
            this.dgtblDetalleProduccion.GridColumnStyles.Add(this.dgtblColPiezasDesperdicio);
            this.dgtblDetalleProduccion.MappingName = "DetalleProduccion";
            // 
            // dgtblColArticulo
            // 
            this.dgtblColArticulo.Format = "";
            this.dgtblColArticulo.FormatInfo = null;
            this.dgtblColArticulo.HeaderText = "Modelo";
            this.dgtblColArticulo.MappingName = "Articulo";
            this.dgtblColArticulo.NullText = "\"\"";
            this.dgtblColArticulo.Width = 125;
            // 
            // dgtblColPiezasBuenas
            // 
            this.dgtblColPiezasBuenas.Format = "";
            this.dgtblColPiezasBuenas.FormatInfo = null;
            this.dgtblColPiezasBuenas.HeaderText = "Buenas";
            this.dgtblColPiezasBuenas.MappingName = "PiezasBuenas";
            this.dgtblColPiezasBuenas.NullText = "\"\"";
            // 
            // dgtblColPiezasDesperdicio
            // 
            this.dgtblColPiezasDesperdicio.Format = "";
            this.dgtblColPiezasDesperdicio.FormatInfo = null;
            this.dgtblColPiezasDesperdicio.HeaderText = "Malas";
            this.dgtblColPiezasDesperdicio.MappingName = "PiezasDesperdicio";
            this.dgtblColPiezasDesperdicio.NullText = "\"\"";
            // 
            // lblDetalleProduccion
            // 
            this.lblDetalleProduccion.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetalleProduccion.Location = new System.Drawing.Point(4, 92);
            this.lblDetalleProduccion.Name = "lblDetalleProduccion";
            this.lblDetalleProduccion.Size = new System.Drawing.Size(231, 20);
            this.lblDetalleProduccion.Text = "Detalle de Producción";
            this.lblDetalleProduccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnRegresar
            // 
            this.btnRegresar.Location = new System.Drawing.Point(163, 272);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(72, 20);
            this.btnRegresar.TabIndex = 1;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click);
            // 
            // encabezado
            // 
            this.encabezado.BackColor = System.Drawing.Color.Transparent;
            this.encabezado.Conexion = LAMOSA.SCPP.Client.View.HandHeld.EstadoConexion.Online;
            this.encabezado.Location = new System.Drawing.Point(0, 0);
            this.encabezado.Mensaje = "";
            this.encabezado.Name = "encabezado";
            this.encabezado.Operador = "";
            this.encabezado.Planta = "";
            this.encabezado.PuestoTurno = "";
            this.encabezado.Size = new System.Drawing.Size(238, 85);
            this.encabezado.TabIndex = 0;
            this.encabezado.TabStop = false;
            this.encabezado.Titulo = "";
            // 
            // frmProduccionOperador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.lblDetalleProduccion);
            this.Controls.Add(this.dgDetalleProduccion);
            this.Controls.Add(this.encabezado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmProduccionOperador";
            this.Text = "Producción de Operador";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmProduccionOperador_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Encabezado encabezado;
        private System.Windows.Forms.DataGrid dgDetalleProduccion;
        private System.Windows.Forms.Label lblDetalleProduccion;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.DataGridTableStyle dgtblDetalleProduccion;
        private System.Windows.Forms.DataGridTextBoxColumn dgtblColArticulo;
        private System.Windows.Forms.DataGridTextBoxColumn dgtblColPiezasBuenas;
        private System.Windows.Forms.DataGridTextBoxColumn dgtblColPiezasDesperdicio;
    }
}