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
            this.lbUsuario = new System.Windows.Forms.Label();
            this.txUsuario = new System.Windows.Forms.TextBox();
            this.txContrasena = new System.Windows.Forms.TextBox();
            this.lbContrasena = new System.Windows.Forms.Label();
            this.pnControles = new System.Windows.Forms.Panel();
            this.btAceptar = new System.Windows.Forms.Button();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.button1 = new System.Windows.Forms.Button();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbUsuario
            // 
            this.lbUsuario.Location = new System.Drawing.Point(5, 0);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(80, 23);
            this.lbUsuario.Text = "Usuario:";
            // 
            // txUsuario
            // 
            this.txUsuario.Location = new System.Drawing.Point(85, 0);
            this.txUsuario.Name = "txUsuario";
            this.txUsuario.Size = new System.Drawing.Size(140, 23);
            this.txUsuario.TabIndex = 1;
            // 
            // txContrasena
            // 
            this.txContrasena.Location = new System.Drawing.Point(85, 35);
            this.txContrasena.Name = "txContrasena";
            this.txContrasena.Size = new System.Drawing.Size(140, 23);
            this.txContrasena.TabIndex = 3;
            // 
            // lbContrasena
            // 
            this.lbContrasena.Location = new System.Drawing.Point(5, 34);
            this.lbContrasena.Name = "lbContrasena";
            this.lbContrasena.Size = new System.Drawing.Size(80, 23);
            this.lbContrasena.Text = "Contraseña:";
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.button1);
            this.pnControles.Controls.Add(this.txUsuario);
            this.pnControles.Controls.Add(this.btAceptar);
            this.pnControles.Controls.Add(this.lbUsuario);
            this.pnControles.Controls.Add(this.lbContrasena);
            this.pnControles.Controls.Add(this.txContrasena);
            this.pnControles.Location = new System.Drawing.Point(0, 85);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 134);
            // 
            // btAceptar
            // 
            this.btAceptar.Location = new System.Drawing.Point(150, 64);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(75, 23);
            this.btAceptar.TabIndex = 9;
            this.btAceptar.Text = "Aceptar";
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Cambiar Contraseña";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // a01_Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.pnControles);
            this.Name = "a01_Login";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.TextBox txUsuario;
        private System.Windows.Forms.TextBox txContrasena;
        private System.Windows.Forms.Label lbContrasena;
        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.Button btAceptar;
        private Encabezado encabezado;
        private System.Windows.Forms.Button button1;
    }
}