namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class a00_Defectos
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
            this.pbrProcesando = new System.Windows.Forms.ProgressBar();
            this.lbPlanta = new System.Windows.Forms.Label();
            this.lbProceso = new System.Windows.Forms.Label();
            this.lbPuesto = new System.Windows.Forms.Label();
            this.lbOperador = new System.Windows.Forms.Label();
            this.btSalir = new System.Windows.Forms.Button();
            this.pnControles = new System.Windows.Forms.Panel();
            this.lstDefecto = new System.Windows.Forms.ListBox();
            this.chkReparacion = new System.Windows.Forms.CheckBox();
            this.chkDesperdicio = new System.Windows.Forms.CheckBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnTerminar = new System.Windows.Forms.Button();
            this.lblReparacion = new System.Windows.Forms.Label();
            this.lblDesperdicio = new System.Windows.Forms.Label();
            this.lbMensaje = new System.Windows.Forms.Label();
            this.txtZona = new System.Windows.Forms.TextBox();
            this.lblZona = new System.Windows.Forms.Label();
            this.txtDefecto = new System.Windows.Forms.TextBox();
            this.lblDefecto = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbrProcesando
            // 
            this.pbrProcesando.Location = new System.Drawing.Point(138, 22);
            this.pbrProcesando.Name = "pbrProcesando";
            this.pbrProcesando.Size = new System.Drawing.Size(50, 18);
            // 
            // lbPlanta
            // 
            this.lbPlanta.BackColor = System.Drawing.Color.Transparent;
            this.lbPlanta.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbPlanta.ForeColor = System.Drawing.Color.Maroon;
            this.lbPlanta.Location = new System.Drawing.Point(0, 26);
            this.lbPlanta.Name = "lbPlanta";
            this.lbPlanta.Size = new System.Drawing.Size(138, 13);
            this.lbPlanta.Text = "XXX";
            // 
            // lbProceso
            // 
            this.lbProceso.BackColor = System.Drawing.Color.Transparent;
            this.lbProceso.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lbProceso.ForeColor = System.Drawing.Color.Black;
            this.lbProceso.Location = new System.Drawing.Point(0, 40);
            this.lbProceso.Name = "lbProceso";
            this.lbProceso.Size = new System.Drawing.Size(238, 15);
            this.lbProceso.Text = "XXX";
            this.lbProceso.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbPuesto
            // 
            this.lbPuesto.BackColor = System.Drawing.Color.Transparent;
            this.lbPuesto.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbPuesto.ForeColor = System.Drawing.Color.Maroon;
            this.lbPuesto.Location = new System.Drawing.Point(0, 13);
            this.lbPuesto.Name = "lbPuesto";
            this.lbPuesto.Size = new System.Drawing.Size(138, 13);
            this.lbPuesto.Text = "XXX";
            // 
            // lbOperador
            // 
            this.lbOperador.BackColor = System.Drawing.Color.Transparent;
            this.lbOperador.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbOperador.ForeColor = System.Drawing.Color.Maroon;
            this.lbOperador.Location = new System.Drawing.Point(0, 0);
            this.lbOperador.Name = "lbOperador";
            this.lbOperador.Size = new System.Drawing.Size(138, 13);
            this.lbOperador.Text = "XXX";
            // 
            // btSalir
            // 
            this.btSalir.Location = new System.Drawing.Point(188, 22);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(50, 18);
            this.btSalir.TabIndex = 25;
            this.btSalir.Text = "Salir";
            this.btSalir.Click += new System.EventHandler(this.btSalir_Click);
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.lstDefecto);
            this.pnControles.Controls.Add(this.chkReparacion);
            this.pnControles.Controls.Add(this.chkDesperdicio);
            this.pnControles.Controls.Add(this.btnCancelar);
            this.pnControles.Controls.Add(this.btnTerminar);
            this.pnControles.Controls.Add(this.lblReparacion);
            this.pnControles.Controls.Add(this.lblDesperdicio);
            this.pnControles.Controls.Add(this.lbMensaje);
            this.pnControles.Controls.Add(this.txtZona);
            this.pnControles.Controls.Add(this.lblZona);
            this.pnControles.Controls.Add(this.txtDefecto);
            this.pnControles.Controls.Add(this.lblDefecto);
            this.pnControles.Location = new System.Drawing.Point(0, 55);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(235, 237);
            // 
            // lstDefecto
            // 
            this.lstDefecto.Items.Add("Grieta");
            this.lstDefecto.Items.Add("Esmalte");
            this.lstDefecto.Items.Add("Pelusa");
            this.lstDefecto.Items.Add("Mancha");
            this.lstDefecto.Items.Add("Burbuja");
            this.lstDefecto.Items.Add("Quebrado");
            this.lstDefecto.Items.Add("Aplastado");
            this.lstDefecto.Location = new System.Drawing.Point(0, 152);
            this.lstDefecto.Name = "lstDefecto";
            this.lstDefecto.Size = new System.Drawing.Size(235, 82);
            this.lstDefecto.TabIndex = 60;
            // 
            // chkReparacion
            // 
            this.chkReparacion.Location = new System.Drawing.Point(84, 100);
            this.chkReparacion.Name = "chkReparacion";
            this.chkReparacion.Size = new System.Drawing.Size(102, 22);
            this.chkReparacion.TabIndex = 59;
            // 
            // chkDesperdicio
            // 
            this.chkDesperdicio.Location = new System.Drawing.Point(85, 76);
            this.chkDesperdicio.Name = "chkDesperdicio";
            this.chkDesperdicio.Size = new System.Drawing.Size(102, 22);
            this.chkDesperdicio.TabIndex = 58;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(138, 127);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 46;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnTerminar
            // 
            this.btnTerminar.Location = new System.Drawing.Point(27, 127);
            this.btnTerminar.Name = "btnTerminar";
            this.btnTerminar.Size = new System.Drawing.Size(75, 23);
            this.btnTerminar.TabIndex = 18;
            this.btnTerminar.Text = "Terminar";
            this.btnTerminar.Click += new System.EventHandler(this.btnTerminar_Click);
            // 
            // lblReparacion
            // 
            this.lblReparacion.Location = new System.Drawing.Point(5, 103);
            this.lblReparacion.Name = "lblReparacion";
            this.lblReparacion.Size = new System.Drawing.Size(85, 23);
            this.lblReparacion.Text = "Reparacion:";
            // 
            // lblDesperdicio
            // 
            this.lblDesperdicio.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblDesperdicio.Location = new System.Drawing.Point(5, 79);
            this.lblDesperdicio.Name = "lblDesperdicio";
            this.lblDesperdicio.Size = new System.Drawing.Size(85, 23);
            this.lblDesperdicio.Text = "Desperdicio:";
            // 
            // lbMensaje
            // 
            this.lbMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbMensaje.Location = new System.Drawing.Point(5, 5);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(225, 18);
            this.lbMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtZona
            // 
            this.txtZona.Location = new System.Drawing.Point(90, 51);
            this.txtZona.Name = "txtZona";
            this.txtZona.Size = new System.Drawing.Size(135, 23);
            this.txtZona.TabIndex = 3;
            this.txtZona.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPosicion_KeyUp);
            // 
            // lblZona
            // 
            this.lblZona.Location = new System.Drawing.Point(5, 51);
            this.lblZona.Name = "lblZona";
            this.lblZona.Size = new System.Drawing.Size(85, 23);
            this.lblZona.Text = "Zona:";
            // 
            // txtDefecto
            // 
            this.txtDefecto.Location = new System.Drawing.Point(90, 23);
            this.txtDefecto.Name = "txtDefecto";
            this.txtDefecto.Size = new System.Drawing.Size(135, 23);
            this.txtDefecto.TabIndex = 1;
            this.txtDefecto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDefecto_KeyUp);
            // 
            // lblDefecto
            // 
            this.lblDefecto.Location = new System.Drawing.Point(5, 23);
            this.lblDefecto.Name = "lblDefecto";
            this.lblDefecto.Size = new System.Drawing.Size(85, 23);
            this.lblDefecto.Text = "Defecto";
            // 
            // pbLogo
            // 
            this.pbLogo.Location = new System.Drawing.Point(138, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(100, 22);
            // 
            // a04_Vaciado03
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.pnControles);
            this.Controls.Add(this.pbrProcesando);
            this.Controls.Add(this.lbPlanta);
            this.Controls.Add(this.lbProceso);
            this.Controls.Add(this.lbPuesto);
            this.Controls.Add(this.lbOperador);
            this.Controls.Add(this.btSalir);
            this.Name = "a04_Vaciado03";
            this.Text = "a04_Vaciado03";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbrProcesando;
        private System.Windows.Forms.Label lbPlanta;
        private System.Windows.Forms.Label lbProceso;
        private System.Windows.Forms.Label lbPuesto;
        private System.Windows.Forms.Label lbOperador;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.TextBox txtDefecto;
        private System.Windows.Forms.Label lblDefecto;
        private System.Windows.Forms.TextBox txtZona;
        private System.Windows.Forms.Label lblZona;
        private System.Windows.Forms.Label lbMensaje;
        private System.Windows.Forms.Label lblDesperdicio;
        private System.Windows.Forms.Label lblReparacion;
        private System.Windows.Forms.Button btnTerminar;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.CheckBox chkDesperdicio;
        private System.Windows.Forms.CheckBox chkReparacion;
        private System.Windows.Forms.ListBox lstDefecto;
    }
}