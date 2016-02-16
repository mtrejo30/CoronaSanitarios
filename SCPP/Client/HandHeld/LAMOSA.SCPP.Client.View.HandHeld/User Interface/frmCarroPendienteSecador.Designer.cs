namespace LAMOSA.SCPP.Client.View.HandHeld.User_Interface
{
    partial class frmCarroPendienteSecador
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
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.dgCarroSecador = new System.Windows.Forms.DataGrid();
            this.btRegresar = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.encabezado.Titulo = "";
            // 
            // dgCarroSecador
            // 
            this.dgCarroSecador.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgCarroSecador.Location = new System.Drawing.Point(1, 86);
            this.dgCarroSecador.Name = "dgCarroSecador";
            this.dgCarroSecador.RowHeadersVisible = false;
            this.dgCarroSecador.Size = new System.Drawing.Size(235, 185);
            this.dgCarroSecador.TabIndex = 86;
            this.dgCarroSecador.TabStop = false;
            this.dgCarroSecador.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgCarroSecador_MouseUp);
            // 
            // btRegresar
            // 
            this.btRegresar.Location = new System.Drawing.Point(163, 272);
            this.btRegresar.Name = "btRegresar";
            this.btRegresar.Size = new System.Drawing.Size(75, 23);
            this.btRegresar.TabIndex = 87;
            this.btRegresar.Text = "Regresar";
            this.btRegresar.Click += new System.EventHandler(this.btRegresar_Click);
            // 
            // frmCarroPendienteSecador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.btRegresar);
            this.Controls.Add(this.dgCarroSecador);
            this.Controls.Add(this.encabezado);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCarroPendienteSecador";
            this.Load += new System.EventHandler(this.frmCarroPendienteSecador__Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmCarroPendienteSecador__KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private Encabezado encabezado;
        private System.Windows.Forms.DataGrid dgCarroSecador;
        private System.Windows.Forms.Button btRegresar;
    }
}