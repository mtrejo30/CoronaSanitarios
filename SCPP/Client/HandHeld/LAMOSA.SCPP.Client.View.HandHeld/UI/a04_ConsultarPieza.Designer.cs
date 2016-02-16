namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class a04_ConsultarPieza
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(a04_ConsultarPieza));
            this.pbrProcesando = new System.Windows.Forms.ProgressBar();
            this.lbPlanta = new System.Windows.Forms.Label();
            this.lbProceso = new System.Windows.Forms.Label();
            this.lbPuesto = new System.Windows.Forms.Label();
            this.lbOperador = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btSalir = new System.Windows.Forms.Button();
            this.pnControles = new System.Windows.Forms.Panel();
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
            this.btSalir.TabIndex = 32;
            this.btSalir.Text = "Salir";
            // 
            // pnControles
            // 
            this.pnControles.Location = new System.Drawing.Point(0, 55);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 206);
            // 
            // a04_ConsultarPieza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.pnControles);
            this.Controls.Add(this.pbrProcesando);
            this.Controls.Add(this.lbPlanta);
            this.Controls.Add(this.lbProceso);
            this.Controls.Add(this.lbPuesto);
            this.Controls.Add(this.lbOperador);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.btSalir);
            this.Name = "a04_ConsultarPieza";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbrProcesando;
        private System.Windows.Forms.Label lbPlanta;
        private System.Windows.Forms.Label lbProceso;
        private System.Windows.Forms.Label lbPuesto;
        private System.Windows.Forms.Label lbOperador;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Panel pnControles;
    }
}