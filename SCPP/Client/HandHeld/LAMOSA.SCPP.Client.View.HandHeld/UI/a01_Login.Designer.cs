namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class a01_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(a01_Login));
            this.lbUsuario = new System.Windows.Forms.Label();
            this.tbUsuario = new System.Windows.Forms.TextBox();
            this.tbContrasena = new System.Windows.Forms.TextBox();
            this.lbContrasena = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnControles = new System.Windows.Forms.Panel();
            this.lbMensaje = new System.Windows.Forms.Label();
            this.btAceptar = new System.Windows.Forms.Button();
            this.pbrProcesando = new System.Windows.Forms.ProgressBar();
            this.btSalir = new System.Windows.Forms.Button();
            this.lbProceso = new System.Windows.Forms.Label();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbUsuario
            // 
            this.lbUsuario.Location = new System.Drawing.Point(5, 45);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(80, 23);
            this.lbUsuario.Text = "Usuario:";
            // 
            // tbUsuario
            // 
            this.tbUsuario.Location = new System.Drawing.Point(85, 45);
            this.tbUsuario.Name = "tbUsuario";
            this.tbUsuario.Size = new System.Drawing.Size(140, 23);
            this.tbUsuario.TabIndex = 1;
            // 
            // tbContrasena
            // 
            this.tbContrasena.Location = new System.Drawing.Point(85, 78);
            this.tbContrasena.Name = "tbContrasena";
            this.tbContrasena.PasswordChar = '*';
            this.tbContrasena.Size = new System.Drawing.Size(140, 23);
            this.tbContrasena.TabIndex = 3;
            // 
            // lbContrasena
            // 
            this.lbContrasena.Location = new System.Drawing.Point(5, 78);
            this.lbContrasena.Name = "lbContrasena";
            this.lbContrasena.Size = new System.Drawing.Size(80, 23);
            this.lbContrasena.Text = "Contraseña:";
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(138, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(100, 22);
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.lbMensaje);
            this.pnControles.Controls.Add(this.tbUsuario);
            this.pnControles.Controls.Add(this.btAceptar);
            this.pnControles.Controls.Add(this.lbUsuario);
            this.pnControles.Controls.Add(this.lbContrasena);
            this.pnControles.Controls.Add(this.tbContrasena);
            this.pnControles.Location = new System.Drawing.Point(0, 55);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 143);
            // 
            // lbMensaje
            // 
            this.lbMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbMensaje.Location = new System.Drawing.Point(5, 5);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(220, 35);
            this.lbMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btAceptar
            // 
            this.btAceptar.Location = new System.Drawing.Point(77, 115);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(75, 23);
            this.btAceptar.TabIndex = 9;
            this.btAceptar.Text = "Aceptar";
            // 
            // pbrProcesando
            // 
            this.pbrProcesando.Location = new System.Drawing.Point(138, 22);
            this.pbrProcesando.Name = "pbrProcesando";
            this.pbrProcesando.Size = new System.Drawing.Size(50, 18);
            // 
            // btSalir
            // 
            this.btSalir.Location = new System.Drawing.Point(188, 22);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(50, 18);
            this.btSalir.TabIndex = 13;
            this.btSalir.Text = "Salir";
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
            // a01_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.lbProceso);
            this.Controls.Add(this.pbrProcesando);
            this.Controls.Add(this.btSalir);
            this.Controls.Add(this.pnControles);
            this.Controls.Add(this.pbLogo);
            this.Name = "a01_Login";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.TextBox tbUsuario;
        private System.Windows.Forms.TextBox tbContrasena;
        private System.Windows.Forms.Label lbContrasena;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.Button btAceptar;
        private System.Windows.Forms.Label lbMensaje;
        private System.Windows.Forms.ProgressBar pbrProcesando;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Label lbProceso;
    }
}