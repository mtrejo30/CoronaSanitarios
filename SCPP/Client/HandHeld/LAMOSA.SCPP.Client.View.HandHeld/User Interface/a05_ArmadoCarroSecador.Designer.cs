using LAMOSA.SCPP.Client.View.HandHeld;
namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class a05_ArmadoCarroSecador
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
            this.pnControles = new System.Windows.Forms.Panel();
            this.cmbTransporte = new System.Windows.Forms.ComboBox();
            this.btTerminar = new System.Windows.Forms.Button();
            this.btCancelar = new System.Windows.Forms.Button();
            this.btAceptar = new System.Windows.Forms.Button();
            this.txEtiqueta = new System.Windows.Forms.TextBox();
            this.lbEtiqueta = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txCarro = new System.Windows.Forms.TextBox();
            this.lbCarro = new System.Windows.Forms.Label();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.cmbTransporte);
            this.pnControles.Controls.Add(this.btTerminar);
            this.pnControles.Controls.Add(this.btCancelar);
            this.pnControles.Controls.Add(this.btAceptar);
            this.pnControles.Controls.Add(this.txEtiqueta);
            this.pnControles.Controls.Add(this.lbEtiqueta);
            this.pnControles.Controls.Add(this.label1);
            this.pnControles.Controls.Add(this.txCarro);
            this.pnControles.Controls.Add(this.lbCarro);
            this.pnControles.Location = new System.Drawing.Point(0, 85);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 133);
            // 
            // cmbTransporte
            // 
            this.cmbTransporte.Location = new System.Drawing.Point(75, 12);
            this.cmbTransporte.Name = "cmbTransporte";
            this.cmbTransporte.Size = new System.Drawing.Size(150, 23);
            this.cmbTransporte.TabIndex = 20;
            this.cmbTransporte.SelectedValueChanged += new System.EventHandler(this.cmbTransporte_SelectedValueChanged);
            // 
            // btTerminar
            // 
            this.btTerminar.Location = new System.Drawing.Point(151, 107);
            this.btTerminar.Name = "btTerminar";
            this.btTerminar.Size = new System.Drawing.Size(70, 23);
            this.btTerminar.TabIndex = 16;
            this.btTerminar.Text = "Terminar";
            // 
            // btCancelar
            // 
            this.btCancelar.Location = new System.Drawing.Point(75, 107);
            this.btCancelar.Name = "btCancelar";
            this.btCancelar.Size = new System.Drawing.Size(70, 23);
            this.btCancelar.TabIndex = 13;
            this.btCancelar.Text = "Cancelar";
            // 
            // btAceptar
            // 
            this.btAceptar.Location = new System.Drawing.Point(0, 107);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(70, 23);
            this.btAceptar.TabIndex = 12;
            this.btAceptar.Text = "Aceptar";
            // 
            // txEtiqueta
            // 
            this.txEtiqueta.Location = new System.Drawing.Point(75, 66);
            this.txEtiqueta.Name = "txEtiqueta";
            this.txEtiqueta.Size = new System.Drawing.Size(150, 23);
            this.txEtiqueta.TabIndex = 10;
            // 
            // lbEtiqueta
            // 
            this.lbEtiqueta.Location = new System.Drawing.Point(5, 66);
            this.lbEtiqueta.Name = "lbEtiqueta";
            this.lbEtiqueta.Size = new System.Drawing.Size(70, 23);
            this.lbEtiqueta.Text = "Etiqueta:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.Text = "Transporte:";
            // 
            // txCarro
            // 
            this.txCarro.Location = new System.Drawing.Point(75, 41);
            this.txCarro.Name = "txCarro";
            this.txCarro.Size = new System.Drawing.Size(150, 23);
            this.txCarro.TabIndex = 8;
            // 
            // lbCarro
            // 
            this.lbCarro.Location = new System.Drawing.Point(5, 41);
            this.lbCarro.Name = "lbCarro";
            this.lbCarro.Size = new System.Drawing.Size(70, 23);
            this.lbCarro.Text = "Carro:";
            // 
            // encabezado
            // 
            this.encabezado.BackColor = System.Drawing.Color.Transparent;
            this.encabezado.Conexion = LAMOSA.SCPP.Client.View.HandHeld.EstadoConexion.Indeterminado;
            this.encabezado.Location = new System.Drawing.Point(0, 0);
            this.encabezado.Mensaje = "";
            this.encabezado.Name = "encabezado";
            this.encabezado.Operador = "";
            this.encabezado.Planta = "";
            this.encabezado.PuestoTurno = "";
            this.encabezado.Size = new System.Drawing.Size(238, 85);
            this.encabezado.TabIndex = 30;
            this.encabezado.Titulo = "";
            // 
            // a05_ArmadoCarroSecador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.pnControles);
            this.Name = "a05_ArmadoCarroSecador";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.TextBox txCarro;
        private System.Windows.Forms.Label lbCarro;
        private System.Windows.Forms.TextBox txEtiqueta;
        private System.Windows.Forms.Label lbEtiqueta;
        private System.Windows.Forms.Button btCancelar;
        private System.Windows.Forms.Button btAceptar;
        private System.Windows.Forms.Button btTerminar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbTransporte;
        private Encabezado encabezado;
    }
}