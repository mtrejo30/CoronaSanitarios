namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class a02_SeleccionPlanta
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
            this.pnControles = new System.Windows.Forms.Panel();
            this.btContinuar = new System.Windows.Forms.Button();
            this.cbxPlanta = new System.Windows.Forms.ComboBox();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.btContinuar);
            this.pnControles.Controls.Add(this.cbxPlanta);
            this.pnControles.Location = new System.Drawing.Point(0, 85);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 91);
            // 
            // btContinuar
            // 
            this.btContinuar.Location = new System.Drawing.Point(77, 63);
            this.btContinuar.Name = "btContinuar";
            this.btContinuar.Size = new System.Drawing.Size(75, 23);
            this.btContinuar.TabIndex = 10;
            this.btContinuar.Text = "Continuar";
            // 
            // cbxPlanta
            // 
            this.cbxPlanta.Location = new System.Drawing.Point(5, 30);
            this.cbxPlanta.Name = "cbxPlanta";
            this.cbxPlanta.Size = new System.Drawing.Size(220, 23);
            this.cbxPlanta.TabIndex = 0;
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
            this.encabezado.TabIndex = 32;
            this.encabezado.Titulo = "";
            // 
            // a02_SeleccionPlanta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.pnControles);
            this.Name = "a02_SeleccionPlanta";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.ComboBox cbxPlanta;
        private System.Windows.Forms.Button btContinuar;
        private Encabezado encabezado;

    }
}