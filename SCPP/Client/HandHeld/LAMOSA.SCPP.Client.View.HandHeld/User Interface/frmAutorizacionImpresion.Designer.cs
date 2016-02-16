namespace LAMOSA.SCPP.Client.View.HandHeld.User_Interface
{
    partial class frmAutorizacionImpresion
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
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblContrasena = new System.Windows.Forms.Label();
            this.tbUsuario = new System.Windows.Forms.TextBox();
            this.tbContrasena = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.lblDescOpcion = new System.Windows.Forms.Label();
            this.radbtnPieza = new System.Windows.Forms.RadioButton();
            this.radbtnTarima = new System.Windows.Forms.RadioButton();
            this.panelSeleccionImpresion = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panelSeleccionImpresion.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblUsuario
            // 
            this.lblUsuario.Location = new System.Drawing.Point(16, 197);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(100, 20);
            this.lblUsuario.Text = "Usuario:";
            this.lblUsuario.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblContrasena
            // 
            this.lblContrasena.Location = new System.Drawing.Point(16, 221);
            this.lblContrasena.Name = "lblContrasena";
            this.lblContrasena.Size = new System.Drawing.Size(100, 20);
            this.lblContrasena.Text = "Contraseña:";
            this.lblContrasena.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbUsuario
            // 
            this.tbUsuario.Location = new System.Drawing.Point(122, 194);
            this.tbUsuario.MaxLength = 10;
            this.tbUsuario.Name = "tbUsuario";
            this.tbUsuario.Size = new System.Drawing.Size(100, 23);
            this.tbUsuario.TabIndex = 3;
            this.tbUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressControlEvent);
            // 
            // tbContrasena
            // 
            this.tbContrasena.Location = new System.Drawing.Point(122, 218);
            this.tbContrasena.MaxLength = 15;
            this.tbContrasena.Name = "tbContrasena";
            this.tbContrasena.PasswordChar = '*';
            this.tbContrasena.Size = new System.Drawing.Size(100, 23);
            this.tbContrasena.TabIndex = 4;
            this.tbContrasena.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.KeyPressControlEvent);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(36, 247);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 20);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(122, 247);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 20);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
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
            this.encabezado.TabIndex = 0;
            this.encabezado.Titulo = "";
            // 
            // lblDescOpcion
            // 
            this.lblDescOpcion.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblDescOpcion.ForeColor = System.Drawing.Color.Red;
            this.lblDescOpcion.Location = new System.Drawing.Point(0, 88);
            this.lblDescOpcion.Name = "lblDescOpcion";
            this.lblDescOpcion.Size = new System.Drawing.Size(238, 36);
            this.lblDescOpcion.Text = "Solicitar Autorización de Impresión";
            this.lblDescOpcion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // radbtnPieza
            // 
            this.radbtnPieza.Location = new System.Drawing.Point(3, 30);
            this.radbtnPieza.Name = "radbtnPieza";
            this.radbtnPieza.Size = new System.Drawing.Size(110, 20);
            this.radbtnPieza.TabIndex = 1;
            this.radbtnPieza.Text = "Etiqueta Pieza";
            this.radbtnPieza.CheckedChanged += new System.EventHandler(this.radbtnCheckedChanged);
            // 
            // radbtnTarima
            // 
            this.radbtnTarima.Location = new System.Drawing.Point(112, 30);
            this.radbtnTarima.Name = "radbtnTarima";
            this.radbtnTarima.Size = new System.Drawing.Size(120, 20);
            this.radbtnTarima.TabIndex = 2;
            this.radbtnTarima.Text = "Etiqueta Tarima";
            this.radbtnTarima.CheckedChanged += new System.EventHandler(this.radbtnCheckedChanged);
            // 
            // panelSeleccionImpresion
            // 
            this.panelSeleccionImpresion.Controls.Add(this.label1);
            this.panelSeleccionImpresion.Controls.Add(this.radbtnPieza);
            this.panelSeleccionImpresion.Controls.Add(this.radbtnTarima);
            this.panelSeleccionImpresion.Location = new System.Drawing.Point(0, 128);
            this.panelSeleccionImpresion.Name = "panelSeleccionImpresion";
            this.panelSeleccionImpresion.Size = new System.Drawing.Size(238, 53);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 20);
            this.label1.Text = "Tipo de Impresión de Etiqueta:";
            // 
            // frmAutorizacionImpresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.panelSeleccionImpresion);
            this.Controls.Add(this.lblDescOpcion);
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbContrasena);
            this.Controls.Add(this.tbUsuario);
            this.Controls.Add(this.lblContrasena);
            this.Controls.Add(this.lblUsuario);
            this.Name = "frmAutorizacionImpresion";
            this.Text = "Autorización Impresión";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmAutorizacionImpresion_Load);
            this.panelSeleccionImpresion.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblContrasena;
        private System.Windows.Forms.TextBox tbUsuario;
        private System.Windows.Forms.TextBox tbContrasena;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private Encabezado encabezado;
        private System.Windows.Forms.Label lblDescOpcion;
        private System.Windows.Forms.RadioButton radbtnPieza;
        private System.Windows.Forms.RadioButton radbtnTarima;
        private System.Windows.Forms.Panel panelSeleccionImpresion;
        private System.Windows.Forms.Label label1;
    }
}