namespace LAMOSA.SCPP.Client.View.HandHeld.User_Interface
{
    partial class frmCarroPendienteSecadorDetalle
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
            this.dgCarroSecadorDetalle = new System.Windows.Forms.DataGrid();
            this.btRegresar = new System.Windows.Forms.Button();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.SuspendLayout();
            // 
            // dgCarroSecadorDetalle
            // 
            this.dgCarroSecadorDetalle.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgCarroSecadorDetalle.Location = new System.Drawing.Point(1, 87);
            this.dgCarroSecadorDetalle.Name = "dgCarroSecadorDetalle";
            this.dgCarroSecadorDetalle.RowHeadersVisible = false;
            this.dgCarroSecadorDetalle.Size = new System.Drawing.Size(235, 185);
            this.dgCarroSecadorDetalle.TabIndex = 88;
            this.dgCarroSecadorDetalle.TabStop = false;
            // 
            // btRegresar
            // 
            this.btRegresar.Location = new System.Drawing.Point(162, 272);
            this.btRegresar.Name = "btRegresar";
            this.btRegresar.Size = new System.Drawing.Size(75, 23);
            this.btRegresar.TabIndex = 87;
            this.btRegresar.Text = "Regresar";
            this.btRegresar.Click += new System.EventHandler(this.btRegresar_Click);
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
            this.encabezado.TabIndex = 89;
            this.encabezado.Titulo = "";
            // 
            // frmCarroPendienteSecadorDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.dgCarroSecadorDetalle);
            this.Controls.Add(this.btRegresar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCarroPendienteSecadorDetalle";
            this.Load += new System.EventHandler(this.frmCarroPendienteSecadorDetalle_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmCarroPendienteSecadorDetalle_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgCarroSecadorDetalle;
        private System.Windows.Forms.Button btRegresar;
        private Encabezado encabezado;
    }
}