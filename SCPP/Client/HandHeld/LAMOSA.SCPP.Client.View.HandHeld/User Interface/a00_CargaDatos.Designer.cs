namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class a00_CargaDatos
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
            this.cbxProceso = new System.Windows.Forms.ComboBox();
            this.Proceso = new System.Windows.Forms.Label();
            this.cbxPlanta = new System.Windows.Forms.ComboBox();
            this.lbPlanta = new System.Windows.Forms.Label();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btAceptar = new System.Windows.Forms.Button();
            this.pbrProcesando = new System.Windows.Forms.ProgressBar();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.cbxProceso);
            this.pnControles.Controls.Add(this.Proceso);
            this.pnControles.Controls.Add(this.cbxPlanta);
            this.pnControles.Controls.Add(this.lbPlanta);
            this.pnControles.Controls.Add(this.btCancelar);
            this.pnControles.Controls.Add(this.btAceptar);
            this.pnControles.Controls.Add(this.pbrProcesando);
            this.pnControles.Location = new System.Drawing.Point(0, 85);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 128);
            // 
            // cbxProceso
            // 
            this.cbxProceso.Location = new System.Drawing.Point(75, 75);
            this.cbxProceso.Name = "cbxProceso";
            this.cbxProceso.Size = new System.Drawing.Size(150, 23);
            this.cbxProceso.TabIndex = 12;
            this.cbxProceso.Visible = false;
            // 
            // Proceso
            // 
            this.Proceso.Location = new System.Drawing.Point(5, 75);
            this.Proceso.Name = "Proceso";
            this.Proceso.Size = new System.Drawing.Size(70, 23);
            this.Proceso.Text = "Proceso:";
            this.Proceso.Visible = false;
            // 
            // cbxPlanta
            // 
            this.cbxPlanta.Location = new System.Drawing.Point(75, 50);
            this.cbxPlanta.Name = "cbxPlanta";
            this.cbxPlanta.Size = new System.Drawing.Size(150, 23);
            this.cbxPlanta.TabIndex = 9;
            this.cbxPlanta.Visible = false;
            // 
            // lbPlanta
            // 
            this.lbPlanta.Location = new System.Drawing.Point(5, 50);
            this.lbPlanta.Name = "lbPlanta";
            this.lbPlanta.Size = new System.Drawing.Size(70, 23);
            this.lbPlanta.Text = "Planta:";
            this.lbPlanta.Visible = false;
            // 
            // btCancelar
            // 
            this.btCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancelar.Location = new System.Drawing.Point(125, 105);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(100, 23);
            this.btCancelar.TabIndex = 5;
            this.btCancelar.Text = "Cancelar";
            // 
            // btAceptar
            // 
            this.btAceptar.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btAceptar.Location = new System.Drawing.Point(5, 105);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(100, 23);
            this.btAceptar.TabIndex = 2;
            this.btAceptar.Text = "Aceptar";
            this.btAceptar.Visible = false;
            // 
            // pbrProcesando
            // 
            this.pbrProcesando.Location = new System.Drawing.Point(5, 0);
            this.pbrProcesando.Name = "pbrProcesando";
            this.pbrProcesando.Size = new System.Drawing.Size(220, 25);
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
            this.encabezado.TabIndex = 3;
            this.encabezado.Titulo = "";
            // 
            // a00_CargaDatos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.pnControles);
            this.Name = "a00_CargaDatos";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.ProgressBar pbrProcesando;
        private System.Windows.Forms.Button btAceptar;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.ComboBox cbxProceso;
        private System.Windows.Forms.Label Proceso;
        private System.Windows.Forms.ComboBox cbxPlanta;
        private System.Windows.Forms.Label lbPlanta;
        private Encabezado encabezado;

    }
}