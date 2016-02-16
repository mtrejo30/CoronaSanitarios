namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class a04_Defectos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(a04_Defectos));
            this.pnControles = new System.Windows.Forms.Panel();
            this.ltbDefectos = new System.Windows.Forms.ListBox();
            this.ckbDesperdicio = new System.Windows.Forms.CheckBox();
            this.ckbReparacion = new System.Windows.Forms.CheckBox();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btTerminar = new System.Windows.Forms.Button();
            this.txtZona = new System.Windows.Forms.TextBox();
            this.lbZona = new System.Windows.Forms.Label();
            this.txtDefecto = new System.Windows.Forms.TextBox();
            this.lblDefecto = new System.Windows.Forms.Label();
            this.pbrProcesando = new System.Windows.Forms.ProgressBar();
            this.lbPlanta = new System.Windows.Forms.Label();
            this.lbProceso = new System.Windows.Forms.Label();
            this.lbPuesto = new System.Windows.Forms.Label();
            this.lbOperador = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btSalir = new System.Windows.Forms.Button();
            this.lbMensaje = new System.Windows.Forms.Label();
            this.btCapturar = new System.Windows.Forms.Button();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.btCapturar);
            this.pnControles.Controls.Add(this.lbMensaje);
            this.pnControles.Controls.Add(this.ltbDefectos);
            this.pnControles.Controls.Add(this.ckbDesperdicio);
            this.pnControles.Controls.Add(this.ckbReparacion);
            this.pnControles.Controls.Add(this.btCancelar);
            this.pnControles.Controls.Add(this.btTerminar);
            this.pnControles.Controls.Add(this.txtZona);
            this.pnControles.Controls.Add(this.lbZona);
            this.pnControles.Controls.Add(this.txtDefecto);
            this.pnControles.Controls.Add(this.lblDefecto);
            this.pnControles.Location = new System.Drawing.Point(0, 55);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 215);
            // 
            // ltbDefectos
            // 
            this.ltbDefectos.Location = new System.Drawing.Point(5, 100);
            this.ltbDefectos.Name = "ltbDefectos";
            this.ltbDefectos.Size = new System.Drawing.Size(220, 82);
            this.ltbDefectos.TabIndex = 60;
            // 
            // ckbDesperdicio
            // 
            this.ckbDesperdicio.Location = new System.Drawing.Point(110, 75);
            this.ckbDesperdicio.Name = "ckbDesperdicio";
            this.ckbDesperdicio.Size = new System.Drawing.Size(105, 22);
            this.ckbDesperdicio.TabIndex = 59;
            this.ckbDesperdicio.Text = "Desperdicio";
            // 
            // ckbReparacion
            // 
            this.ckbReparacion.Location = new System.Drawing.Point(5, 75);
            this.ckbReparacion.Name = "ckbReparacion";
            this.ckbReparacion.Size = new System.Drawing.Size(105, 22);
            this.ckbReparacion.TabIndex = 58;
            this.ckbReparacion.Text = "Reparacion";
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(155, 187);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(70, 23);
            this.btCancelar.TabIndex = 46;
            this.btCancelar.Text = "Cancelar";
            this.btCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btTerminar
            // 
            this.btTerminar.Location = new System.Drawing.Point(80, 187);
            this.btTerminar.Name = "btTerminar";
            this.btTerminar.Size = new System.Drawing.Size(70, 23);
            this.btTerminar.TabIndex = 18;
            this.btTerminar.Text = "Terminar";
            this.btTerminar.Click += new System.EventHandler(this.btnTerminar_Click);
            // 
            // txtZona
            // 
            this.txtZona.Location = new System.Drawing.Point(80, 50);
            this.txtZona.Name = "txtZona";
            this.txtZona.Size = new System.Drawing.Size(145, 23);
            this.txtZona.TabIndex = 3;
            this.txtZona.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPosicion_KeyUp);
            // 
            // lbZona
            // 
            this.lbZona.Location = new System.Drawing.Point(5, 50);
            this.lbZona.Name = "lbZona";
            this.lbZona.Size = new System.Drawing.Size(80, 23);
            this.lbZona.Text = "Zona:";
            // 
            // txtDefecto
            // 
            this.txtDefecto.Location = new System.Drawing.Point(80, 23);
            this.txtDefecto.Name = "txtDefecto";
            this.txtDefecto.Size = new System.Drawing.Size(145, 23);
            this.txtDefecto.TabIndex = 1;
            this.txtDefecto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDefecto_KeyUp);
            // 
            // lblDefecto
            // 
            this.lblDefecto.Location = new System.Drawing.Point(5, 25);
            this.lblDefecto.Name = "lblDefecto";
            this.lblDefecto.Size = new System.Drawing.Size(75, 23);
            this.lblDefecto.Text = "Defecto:";
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
            this.lbProceso.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
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
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(138, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(100, 22);
            // 
            // btSalir
            // 
            this.btSalir.Location = new System.Drawing.Point(188, 22);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(50, 18);
            this.btSalir.TabIndex = 39;
            this.btSalir.Text = "Salir";
            // 
            // lbMensaje
            // 
            this.lbMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbMensaje.Location = new System.Drawing.Point(5, 5);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(220, 17);
            this.lbMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btCapturar
            // 
            this.btCapturar.Location = new System.Drawing.Point(5, 187);
            this.btCapturar.Name = "btCapturar";
            this.btCapturar.Size = new System.Drawing.Size(70, 23);
            this.btCapturar.TabIndex = 68;
            this.btCapturar.Text = "Capturar";
            // 
            // a04_Defectos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.pbrProcesando);
            this.Controls.Add(this.lbPlanta);
            this.Controls.Add(this.lbProceso);
            this.Controls.Add(this.lbPuesto);
            this.Controls.Add(this.lbOperador);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.pnControles);
            this.Name = "a04_Defectos";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.TextBox txtDefecto;
        private System.Windows.Forms.Label lblDefecto;
        private System.Windows.Forms.TextBox txtZona;
        private System.Windows.Forms.Label lbZona;
        private System.Windows.Forms.Button btTerminar;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.CheckBox ckbReparacion;
        private System.Windows.Forms.CheckBox ckbDesperdicio;
        private System.Windows.Forms.ListBox ltbDefectos;
        private System.Windows.Forms.ProgressBar pbrProcesando;
        private System.Windows.Forms.Label lbPlanta;
        private System.Windows.Forms.Label lbProceso;
        private System.Windows.Forms.Label lbPuesto;
        private System.Windows.Forms.Label lbOperador;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Label lbMensaje;
        private System.Windows.Forms.Button btCapturar;
    }
}