namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class a03_ConfiguracionInicial
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
            this.btnTransaccional = new System.Windows.Forms.Button();
            this.btnCatalogo = new System.Windows.Forms.Button();
            this.cbxProceso = new System.Windows.Forms.ComboBox();
            this.btContinuar = new System.Windows.Forms.Button();
            this.cbxOpcion = new System.Windows.Forms.ComboBox();
            this.lbOpcion = new System.Windows.Forms.Label();
            this.lbProceso = new System.Windows.Forms.Label();
            this.cbxTurno = new System.Windows.Forms.ComboBox();
            this.lbTurno = new System.Windows.Forms.Label();
            this.dtpFecha = new System.Windows.Forms.DateTimePicker();
            this.lbFecha = new System.Windows.Forms.Label();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.btnTransaccional);
            this.pnControles.Controls.Add(this.btnCatalogo);
            this.pnControles.Controls.Add(this.cbxProceso);
            this.pnControles.Controls.Add(this.btContinuar);
            this.pnControles.Controls.Add(this.cbxOpcion);
            this.pnControles.Controls.Add(this.lbOpcion);
            this.pnControles.Controls.Add(this.lbProceso);
            this.pnControles.Controls.Add(this.cbxTurno);
            this.pnControles.Controls.Add(this.lbTurno);
            this.pnControles.Controls.Add(this.dtpFecha);
            this.pnControles.Controls.Add(this.lbFecha);
            this.pnControles.Location = new System.Drawing.Point(0, 85);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 207);
            // 
            // btnTransaccional
            // 
            this.btnTransaccional.Location = new System.Drawing.Point(5, 173);
            this.btnTransaccional.Name = "btnTransaccional";
            this.btnTransaccional.Size = new System.Drawing.Size(147, 20);
            this.btnTransaccional.TabIndex = 24;
            this.btnTransaccional.Text = "Act. Transaccionales";
            this.btnTransaccional.Click += new System.EventHandler(this.btnTransaccional_Click);
            // 
            // btnCatalogo
            // 
            this.btnCatalogo.Location = new System.Drawing.Point(5, 147);
            this.btnCatalogo.Name = "btnCatalogo";
            this.btnCatalogo.Size = new System.Drawing.Size(147, 20);
            this.btnCatalogo.TabIndex = 24;
            this.btnCatalogo.Text = "Act. Catalogos";
            this.btnCatalogo.Click += new System.EventHandler(this.btnCatalogo_Click);
            // 
            // cbxProceso
            // 
            this.cbxProceso.Location = new System.Drawing.Point(75, 50);
            this.cbxProceso.Name = "cbxProceso";
            this.cbxProceso.Size = new System.Drawing.Size(150, 23);
            this.cbxProceso.TabIndex = 19;
            // 
            // btContinuar
            // 
            this.btContinuar.Location = new System.Drawing.Point(77, 108);
            this.btContinuar.Name = "btContinuar";
            this.btContinuar.Size = new System.Drawing.Size(75, 23);
            this.btContinuar.TabIndex = 11;
            this.btContinuar.Text = "Continuar";
            // 
            // cbxOpcion
            // 
            this.cbxOpcion.Location = new System.Drawing.Point(75, 75);
            this.cbxOpcion.Name = "cbxOpcion";
            this.cbxOpcion.Size = new System.Drawing.Size(150, 23);
            this.cbxOpcion.TabIndex = 9;
            // 
            // lbOpcion
            // 
            this.lbOpcion.Location = new System.Drawing.Point(5, 75);
            this.lbOpcion.Name = "lbOpcion";
            this.lbOpcion.Size = new System.Drawing.Size(70, 23);
            this.lbOpcion.Text = "Opción:";
            // 
            // lbProceso
            // 
            this.lbProceso.Location = new System.Drawing.Point(5, 50);
            this.lbProceso.Name = "lbProceso";
            this.lbProceso.Size = new System.Drawing.Size(70, 23);
            this.lbProceso.Text = "Proceso:";
            // 
            // cbxTurno
            // 
            this.cbxTurno.Location = new System.Drawing.Point(75, 25);
            this.cbxTurno.Name = "cbxTurno";
            this.cbxTurno.Size = new System.Drawing.Size(150, 23);
            this.cbxTurno.TabIndex = 4;
            // 
            // lbTurno
            // 
            this.lbTurno.Location = new System.Drawing.Point(5, 25);
            this.lbTurno.Name = "lbTurno";
            this.lbTurno.Size = new System.Drawing.Size(70, 23);
            this.lbTurno.Text = "Turno:";
            // 
            // dtpFecha
            // 
            this.dtpFecha.CustomFormat = "dd-MMM-yyyyy HH:mm";
            this.dtpFecha.Enabled = false;
            this.dtpFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFecha.Location = new System.Drawing.Point(75, 0);
            this.dtpFecha.Name = "dtpFecha";
            this.dtpFecha.Size = new System.Drawing.Size(150, 24);
            this.dtpFecha.TabIndex = 1;
            // 
            // lbFecha
            // 
            this.lbFecha.Location = new System.Drawing.Point(5, 0);
            this.lbFecha.Name = "lbFecha";
            this.lbFecha.Size = new System.Drawing.Size(70, 24);
            this.lbFecha.Text = "Fecha:";
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
            this.encabezado.TabIndex = 32;
            this.encabezado.Titulo = "";
            // 
            // a03_ConfiguracionInicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.pnControles);
            this.Name = "a03_ConfiguracionInicial";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.DateTimePicker dtpFecha;
        private System.Windows.Forms.Label lbFecha;
        private System.Windows.Forms.ComboBox cbxTurno;
        private System.Windows.Forms.Label lbTurno;
        private System.Windows.Forms.Button btContinuar;
        private System.Windows.Forms.ComboBox cbxOpcion;
        private System.Windows.Forms.Label lbOpcion;
        private System.Windows.Forms.Label lbProceso;
        private System.Windows.Forms.ComboBox cbxProceso;
        private Encabezado encabezado;
        private System.Windows.Forms.Button btnTransaccional;
        private System.Windows.Forms.Button btnCatalogo;
    }
}