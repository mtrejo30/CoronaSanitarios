namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class Encabezado
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Encabezado));
            btEstadoConexion = new System.Windows.Forms.Button();
            this.lbPlanta = new System.Windows.Forms.Label();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.lbPuestoTurno = new System.Windows.Forms.Label();
            this.lbOperador = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.btSalir = new System.Windows.Forms.Button();
            this.lbMensaje = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btEstadoConexion
            // 
            btEstadoConexion.ForeColor = System.Drawing.Color.Black;
            btEstadoConexion.Location = new System.Drawing.Point(138, 22);
            btEstadoConexion.Name = "btEstadoConexion";
            btEstadoConexion.Size = new System.Drawing.Size(50, 18);
            btEstadoConexion.TabIndex = 55;
            // 
            // lbPlanta
            // 
            this.lbPlanta.BackColor = System.Drawing.Color.Transparent;
            this.lbPlanta.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbPlanta.ForeColor = System.Drawing.Color.Maroon;
            this.lbPlanta.Location = new System.Drawing.Point(0, 26);
            this.lbPlanta.Name = "lbPlanta";
            this.lbPlanta.Size = new System.Drawing.Size(138, 13);
            // 
            // lbTitulo
            // 
            this.lbTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lbTitulo.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lbTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbTitulo.Location = new System.Drawing.Point(0, 40);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(238, 15);
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbPuestoTurno
            // 
            this.lbPuestoTurno.BackColor = System.Drawing.Color.Transparent;
            this.lbPuestoTurno.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbPuestoTurno.ForeColor = System.Drawing.Color.Maroon;
            this.lbPuestoTurno.Location = new System.Drawing.Point(0, 13);
            this.lbPuestoTurno.Name = "lbPuestoTurno";
            this.lbPuestoTurno.Size = new System.Drawing.Size(138, 13);
            // 
            // lbOperador
            // 
            this.lbOperador.BackColor = System.Drawing.Color.Transparent;
            this.lbOperador.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbOperador.ForeColor = System.Drawing.Color.Maroon;
            this.lbOperador.Location = new System.Drawing.Point(0, 0);
            this.lbOperador.Name = "lbOperador";
            this.lbOperador.Size = new System.Drawing.Size(138, 13);
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(138, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(100, 22);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // btSalir
            // 
            this.btSalir.ForeColor = System.Drawing.Color.Black;
            this.btSalir.Location = new System.Drawing.Point(188, 22);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(50, 18);
            this.btSalir.TabIndex = 54;
            this.btSalir.Text = "Salir";
            // 
            // lbMensaje
            // 
            this.lbMensaje.Location = new System.Drawing.Point(0, 55);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(238, 30);
            this.lbMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Encabezado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lbMensaje);
            this.Controls.Add(btEstadoConexion);
            this.Controls.Add(this.lbPlanta);
            this.Controls.Add(this.lbTitulo);
            this.Controls.Add(this.lbPuestoTurno);
            this.Controls.Add(this.lbOperador);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.btSalir);
            this.Name = "Encabezado";
            this.Size = new System.Drawing.Size(238, 85);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btEstadoConexion;
        private System.Windows.Forms.Label lbPlanta;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lbPuestoTurno;
        private System.Windows.Forms.Label lbOperador;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Label lbMensaje;

    }
}
