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
            this.lbTitulo = new System.Windows.Forms.Label();
            this.pnLogin = new System.Windows.Forms.Panel();
            this.btAceptar = new System.Windows.Forms.Button();
            this.btSalir = new System.Windows.Forms.Button();
            this.lbMensaje = new System.Windows.Forms.Label();
            this.pnLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbUsuario
            // 
            this.lbUsuario.Location = new System.Drawing.Point(5, 38);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(80, 23);
            this.lbUsuario.Text = "Usuario:";
            // 
            // tbUsuario
            // 
            this.tbUsuario.Location = new System.Drawing.Point(91, 38);
            this.tbUsuario.Name = "tbUsuario";
            this.tbUsuario.Size = new System.Drawing.Size(154, 23);
            this.tbUsuario.TabIndex = 1;
            // 
            // tbContrasena
            // 
            this.tbContrasena.Location = new System.Drawing.Point(91, 71);
            this.tbContrasena.Name = "tbContrasena";
            this.tbContrasena.PasswordChar = '*';
            this.tbContrasena.Size = new System.Drawing.Size(154, 23);
            this.tbContrasena.TabIndex = 3;
            // 
            // lbContrasena
            // 
            this.lbContrasena.Location = new System.Drawing.Point(5, 71);
            this.lbContrasena.Name = "lbContrasena";
            this.lbContrasena.Size = new System.Drawing.Size(80, 23);
            this.lbContrasena.Text = "Contraseña:";
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(198, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(100, 22);
            // 
            // lbTitulo
            // 
            this.lbTitulo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lbTitulo.Location = new System.Drawing.Point(5, 5);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(240, 23);
            this.lbTitulo.Text = "Login";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnLogin
            // 
            this.pnLogin.Controls.Add(this.lbMensaje);
            this.pnLogin.Controls.Add(this.btSalir);
            this.pnLogin.Controls.Add(this.tbUsuario);
            this.pnLogin.Controls.Add(this.btAceptar);
            this.pnLogin.Controls.Add(this.lbTitulo);
            this.pnLogin.Controls.Add(this.lbUsuario);
            this.pnLogin.Controls.Add(this.lbContrasena);
            this.pnLogin.Controls.Add(this.tbContrasena);
            this.pnLogin.Location = new System.Drawing.Point(5, 30);
            this.pnLogin.Name = "pnLogin";
            this.pnLogin.Size = new System.Drawing.Size(250, 172);
            // 
            // btAceptar
            // 
            this.btAceptar.Location = new System.Drawing.Point(40, 144);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(75, 23);
            this.btAceptar.TabIndex = 9;
            this.btAceptar.Text = "Aceptar";
            // 
            // btSalir
            // 
            this.btSalir.Location = new System.Drawing.Point(135, 144);
            this.btSalir.Name = "btSalir";
            this.btSalir.Size = new System.Drawing.Size(75, 23);
            this.btSalir.TabIndex = 10;
            this.btSalir.Text = "Salir";
            // 
            // lbMensaje
            // 
            this.lbMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbMensaje.Location = new System.Drawing.Point(5, 104);
            this.lbMensaje.Name = "lbMensaje";
            this.lbMensaje.Size = new System.Drawing.Size(240, 35);
            this.lbMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // a01_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(298, 249);
            this.Controls.Add(this.pnLogin);
            this.Controls.Add(this.pbLogo);
            this.Name = "a01_Login";
            this.Text = "a01_Login";
            this.pnLogin.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.TextBox tbUsuario;
        private System.Windows.Forms.TextBox tbContrasena;
        private System.Windows.Forms.Label lbContrasena;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Panel pnLogin;
        private System.Windows.Forms.Button btAceptar;
        private System.Windows.Forms.Button btSalir;
        private System.Windows.Forms.Label lbMensaje;
    }
}