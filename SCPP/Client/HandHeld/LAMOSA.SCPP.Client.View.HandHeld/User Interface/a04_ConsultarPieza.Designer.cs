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
            this.pnControles = new System.Windows.Forms.Panel();
            this.btTerminar = new System.Windows.Forms.Button();
            this.txEtiqueta = new System.Windows.Forms.TextBox();
            this.lbEtiqueta = new System.Windows.Forms.Label();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.btTerminar);
            this.pnControles.Controls.Add(this.txEtiqueta);
            this.pnControles.Controls.Add(this.lbEtiqueta);
            this.pnControles.Location = new System.Drawing.Point(0, 85);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 206);
            // 
            // btTerminar
            // 
            this.btTerminar.Location = new System.Drawing.Point(80, 183);
            this.btTerminar.Name = "btTerminar";
            this.btTerminar.Size = new System.Drawing.Size(70, 23);
            this.btTerminar.TabIndex = 47;
            this.btTerminar.Text = "Terminar";
            // 
            // txEtiqueta
            // 
            this.txEtiqueta.Location = new System.Drawing.Point(75, 0);
            this.txEtiqueta.Name = "txEtiqueta";
            this.txEtiqueta.Size = new System.Drawing.Size(150, 23);
            this.txEtiqueta.TabIndex = 5;
            // 
            // lbEtiqueta
            // 
            this.lbEtiqueta.Location = new System.Drawing.Point(5, 0);
            this.lbEtiqueta.Name = "lbEtiqueta";
            this.lbEtiqueta.Size = new System.Drawing.Size(70, 23);
            this.lbEtiqueta.Text = "Etiqueta:";
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
            this.encabezado.TabIndex = 1;
            this.encabezado.Titulo = "";
            // 
            // a04_ConsultarPieza
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.pnControles);
            this.Name = "a04_ConsultarPieza";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.TextBox txEtiqueta;
        private System.Windows.Forms.Label lbEtiqueta;
        private System.Windows.Forms.Button btTerminar;
        private Encabezado encabezado;



    }
}