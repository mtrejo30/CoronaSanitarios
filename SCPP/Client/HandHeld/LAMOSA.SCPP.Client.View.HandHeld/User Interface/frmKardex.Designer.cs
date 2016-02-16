namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class frmKardex
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
            this.txtEtiqueta = new System.Windows.Forms.TextBox();
            this.lbOperador = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtModelo = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCalidad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btRegresar = new System.Windows.Forms.Button();
            this.dgDetalle = new System.Windows.Forms.DataGrid();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.SuspendLayout();
            // 
            // txtEtiqueta
            // 
            this.txtEtiqueta.Location = new System.Drawing.Point(100, 88);
            this.txtEtiqueta.MaxLength = 15;
            this.txtEtiqueta.Name = "txtEtiqueta";
            this.txtEtiqueta.Size = new System.Drawing.Size(135, 23);
            this.txtEtiqueta.TabIndex = 2;
            this.txtEtiqueta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEtiqueta_KeyPress);
            // 
            // lbOperador
            // 
            this.lbOperador.Location = new System.Drawing.Point(13, 86);
            this.lbOperador.Name = "lbOperador";
            this.lbOperador.Size = new System.Drawing.Size(70, 23);
            this.lbOperador.Text = "Etiqueta:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(13, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 23);
            this.label1.Text = "Modelo:";
            // 
            // txtModelo
            // 
            this.txtModelo.Enabled = false;
            this.txtModelo.Location = new System.Drawing.Point(100, 112);
            this.txtModelo.Name = "txtModelo";
            this.txtModelo.ReadOnly = true;
            this.txtModelo.Size = new System.Drawing.Size(135, 23);
            this.txtModelo.TabIndex = 6;
            // 
            // txtColor
            // 
            this.txtColor.Enabled = false;
            this.txtColor.Location = new System.Drawing.Point(100, 136);
            this.txtColor.Name = "txtColor";
            this.txtColor.ReadOnly = true;
            this.txtColor.Size = new System.Drawing.Size(135, 23);
            this.txtColor.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 23);
            this.label2.Text = "Color:";
            // 
            // txtCalidad
            // 
            this.txtCalidad.Enabled = false;
            this.txtCalidad.Location = new System.Drawing.Point(100, 160);
            this.txtCalidad.Name = "txtCalidad";
            this.txtCalidad.ReadOnly = true;
            this.txtCalidad.Size = new System.Drawing.Size(135, 23);
            this.txtCalidad.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 23);
            this.label3.Text = "Calidad:";
            // 
            // btRegresar
            // 
            this.btRegresar.Location = new System.Drawing.Point(165, 272);
            this.btRegresar.Name = "btRegresar";
            this.btRegresar.Size = new System.Drawing.Size(70, 23);
            this.btRegresar.TabIndex = 79;
            this.btRegresar.Text = "Regresar";
            this.btRegresar.Click += new System.EventHandler(this.btRegresar_Click);
            // 
            // dgDetalle
            // 
            this.dgDetalle.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.dgDetalle.Location = new System.Drawing.Point(2, 186);
            this.dgDetalle.Name = "dgDetalle";
            this.dgDetalle.RowHeadersVisible = false;
            this.dgDetalle.Size = new System.Drawing.Size(234, 82);
            this.dgDetalle.TabIndex = 84;
            this.dgDetalle.TabStop = false;
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
            // frmKardex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.dgDetalle);
            this.Controls.Add(this.btRegresar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCalidad);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.txtModelo);
            this.Controls.Add(this.lbOperador);
            this.Controls.Add(this.txtEtiqueta);
            this.Controls.Add(this.encabezado);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKardex";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmKardex_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmKardex_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private Encabezado encabezado;
        private System.Windows.Forms.TextBox txtEtiqueta;
        private System.Windows.Forms.Label lbOperador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtModelo;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCalidad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btRegresar;
        private System.Windows.Forms.DataGrid dgDetalle;
    }
}