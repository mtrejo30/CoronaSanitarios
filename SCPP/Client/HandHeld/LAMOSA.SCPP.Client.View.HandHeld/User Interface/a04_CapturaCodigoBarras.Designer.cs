namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class a04_CapturaCodigoBarras
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
            this.txEtiqueta = new System.Windows.Forms.TextBox();
            this.btCancelar = new System.Windows.Forms.Button();
            this.pnControles = new System.Windows.Forms.Panel();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // txEtiqueta
            // 
            this.txEtiqueta.Location = new System.Drawing.Point(35, 20);
            this.txEtiqueta.Name = "txEtiqueta";
            this.txEtiqueta.Size = new System.Drawing.Size(160, 23);
            this.txEtiqueta.TabIndex = 2;
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(80, 53);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(70, 23);
            this.btCancelar.TabIndex = 50;
            this.btCancelar.Text = "Cancelar";
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.btCancelar);
            this.pnControles.Controls.Add(this.txEtiqueta);
            this.pnControles.Location = new System.Drawing.Point(0, 85);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 76);
            // 
            // encabezado
            // 
            this.encabezado.BackColor = System.Drawing.Color.Transparent;
            this.encabezado.Conexion = LAMOSA.SCPP.Client.View.HandHeld.EstadoConexion.Indeterminado;
            this.encabezado.Location = new System.Drawing.Point(0, 0);
            this.encabezado.Mensaje = "";
            this.encabezado.Name = "encabezado";
            this.encabezado.Operador = "";
            this.encabezado.Planta = "";
            this.encabezado.PuestoTurno = "";
            this.encabezado.Size = new System.Drawing.Size(238, 85);
            this.encabezado.TabIndex = 64;
            this.encabezado.Titulo = "";
            // 
            // a04_CapturaCodigoBarras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.pnControles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "a04_CapturaCodigoBarras";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txEtiqueta;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Panel pnControles;
        private Encabezado encabezado;
    }
}