namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class CambioPassword
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
            this.txPasswordNew = new System.Windows.Forms.TextBox();
            this.lbContrasena = new System.Windows.Forms.Label();
            this.pnControles = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txUsuario = new System.Windows.Forms.TextBox();
            this.txPasswordOld = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btAceptar = new System.Windows.Forms.Button();
            this.lbUsuario = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txPasswordNewRe = new System.Windows.Forms.TextBox();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // txPasswordNew
            // 
            this.txPasswordNew.Location = new System.Drawing.Point(87, 93);
            this.txPasswordNew.Name = "txPasswordNew";
            this.txPasswordNew.Size = new System.Drawing.Size(140, 23);
            this.txPasswordNew.TabIndex = 3;
            this.txPasswordNew.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txPasswordNew_KeyPress);
            // 
            // lbContrasena
            // 
            this.lbContrasena.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lbContrasena.Location = new System.Drawing.Point(7, 87);
            this.lbContrasena.Name = "lbContrasena";
            this.lbContrasena.Size = new System.Drawing.Size(80, 28);
            this.lbContrasena.Text = "Nueva contraseña:";
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.label1);
            this.pnControles.Controls.Add(this.txUsuario);
            this.pnControles.Controls.Add(this.txPasswordOld);
            this.pnControles.Controls.Add(this.label3);
            this.pnControles.Controls.Add(this.btnCancelar);
            this.pnControles.Controls.Add(this.btAceptar);
            this.pnControles.Controls.Add(this.lbUsuario);
            this.pnControles.Controls.Add(this.label2);
            this.pnControles.Controls.Add(this.txPasswordNewRe);
            this.pnControles.Controls.Add(this.lbContrasena);
            this.pnControles.Controls.Add(this.txPasswordNew);
            this.pnControles.Location = new System.Drawing.Point(0, 85);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 207);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(27, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 24);
            this.label1.Text = "Cambio de Contraseña";
            // 
            // txUsuario
            // 
            this.txUsuario.Location = new System.Drawing.Point(87, 35);
            this.txUsuario.Name = "txUsuario";
            this.txUsuario.Size = new System.Drawing.Size(140, 23);
            this.txUsuario.TabIndex = 1;
            this.txUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txUsuario_KeyPress);
            // 
            // txPasswordOld
            // 
            this.txPasswordOld.Location = new System.Drawing.Point(87, 64);
            this.txPasswordOld.Name = "txPasswordOld";
            this.txPasswordOld.Size = new System.Drawing.Size(140, 23);
            this.txPasswordOld.TabIndex = 1;
            this.txPasswordOld.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txPasswordOld_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(7, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 23);
            this.label3.Text = "Usuario:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(27, 165);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btAceptar
            // 
            this.btAceptar.Location = new System.Drawing.Point(140, 165);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(75, 23);
            this.btAceptar.TabIndex = 9;
            this.btAceptar.Text = "Aceptar";
            this.btAceptar.Click += new System.EventHandler(this.btAceptar_Click_1);
            // 
            // lbUsuario
            // 
            this.lbUsuario.Location = new System.Drawing.Point(7, 64);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(80, 23);
            this.lbUsuario.Text = "Contraseña:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(7, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 28);
            this.label2.Text = "Repetir contraseña:";
            // 
            // txPasswordNewRe
            // 
            this.txPasswordNewRe.Location = new System.Drawing.Point(87, 121);
            this.txPasswordNewRe.Name = "txPasswordNewRe";
            this.txPasswordNewRe.Size = new System.Drawing.Size(140, 23);
            this.txPasswordNewRe.TabIndex = 3;
            this.txPasswordNewRe.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txPasswordNewRe_KeyPress);
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
            // CambioPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.pnControles);
            this.Name = "CambioPassword";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txPasswordNew;
        private System.Windows.Forms.Label lbContrasena;
        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.Button btAceptar;
        private Encabezado encabezado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txPasswordNewRe;
        private System.Windows.Forms.TextBox txPasswordOld;
        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.TextBox txUsuario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCancelar;
    }
}