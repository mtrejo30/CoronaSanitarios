namespace LAMOSA.SCPP.Client.View.HandHeld
{
    partial class a06_EntradaCarroSecador
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
            this.btAceptar = new System.Windows.Forms.Button();
            this.lbHrs = new System.Windows.Forms.Label();
            this.txTiempoSecado = new System.Windows.Forms.TextBox();
            this.lbTiempoSecado = new System.Windows.Forms.Label();
            this.dtpHoraEntrada = new System.Windows.Forms.DateTimePicker();
            this.lbHoraEntrada = new System.Windows.Forms.Label();
            this.txCodCarro = new System.Windows.Forms.TextBox();
            this.lbCarro = new System.Windows.Forms.Label();
            this.encabezado = new LAMOSA.SCPP.Client.View.HandHeld.Encabezado();
            this.pnControles.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnControles
            // 
            this.pnControles.Controls.Add(this.btTerminar);
            this.pnControles.Controls.Add(this.btAceptar);
            this.pnControles.Controls.Add(this.lbHrs);
            this.pnControles.Controls.Add(this.txTiempoSecado);
            this.pnControles.Controls.Add(this.lbTiempoSecado);
            this.pnControles.Controls.Add(this.dtpHoraEntrada);
            this.pnControles.Controls.Add(this.lbHoraEntrada);
            this.pnControles.Controls.Add(this.txCodCarro);
            this.pnControles.Controls.Add(this.lbCarro);
            this.pnControles.Location = new System.Drawing.Point(0, 85);
            this.pnControles.Name = "pnControles";
            this.pnControles.Size = new System.Drawing.Size(230, 103);
            // 
            // btTerminar
            // 
            this.btTerminar.Location = new System.Drawing.Point(128, 80);
            this.btTerminar.Name = "btTerminar";
            this.btTerminar.Size = new System.Drawing.Size(75, 23);
            this.btTerminar.TabIndex = 22;
            this.btTerminar.Text = "Terminar";
            // 
            // btAceptar
            // 
            this.btAceptar.Location = new System.Drawing.Point(27, 80);
            this.btAceptar.Name = "btAceptar";
            this.btAceptar.Size = new System.Drawing.Size(75, 23);
            this.btAceptar.TabIndex = 21;
            this.btAceptar.Text = "Aceptar";
            // 
            // lbHrs
            // 
            this.lbHrs.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lbHrs.Location = new System.Drawing.Point(181, 50);
            this.lbHrs.Name = "lbHrs";
            this.lbHrs.Size = new System.Drawing.Size(44, 24);
            this.lbHrs.Text = "Hrs. Aprox.";
            // 
            // txTiempoSecado
            // 
            this.txTiempoSecado.Location = new System.Drawing.Point(75, 50);
            this.txTiempoSecado.Name = "txTiempoSecado";
            this.txTiempoSecado.Size = new System.Drawing.Size(100, 23);
            this.txTiempoSecado.TabIndex = 17;
            // 
            // lbTiempoSecado
            // 
            this.lbTiempoSecado.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lbTiempoSecado.Location = new System.Drawing.Point(5, 50);
            this.lbTiempoSecado.Name = "lbTiempoSecado";
            this.lbTiempoSecado.Size = new System.Drawing.Size(70, 23);
            this.lbTiempoSecado.Text = "Tiempo Secado:";
            // 
            // dtpHoraEntrada
            // 
            this.dtpHoraEntrada.CustomFormat = "";
            this.dtpHoraEntrada.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.dtpHoraEntrada.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHoraEntrada.Location = new System.Drawing.Point(75, 25);
            this.dtpHoraEntrada.Name = "dtpHoraEntrada";
            this.dtpHoraEntrada.Size = new System.Drawing.Size(150, 22);
            this.dtpHoraEntrada.TabIndex = 14;
            // 
            // lbHoraEntrada
            // 
            this.lbHoraEntrada.Font = new System.Drawing.Font("Tahoma", 7.5F, System.Drawing.FontStyle.Regular);
            this.lbHoraEntrada.Location = new System.Drawing.Point(5, 25);
            this.lbHoraEntrada.Name = "lbHoraEntrada";
            this.lbHoraEntrada.Size = new System.Drawing.Size(70, 24);
            this.lbHoraEntrada.Text = "Hora Entrada:";
            // 
            // txCodCarro
            // 
            this.txCodCarro.Location = new System.Drawing.Point(75, 0);
            this.txCodCarro.Name = "txCodCarro";
            this.txCodCarro.Size = new System.Drawing.Size(150, 23);
            this.txCodCarro.TabIndex = 11;
            // 
            // lbCarro
            // 
            this.lbCarro.Location = new System.Drawing.Point(5, 0);
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
            this.encabezado.TabIndex = 32;
            this.encabezado.Titulo = "";
            // 
            // a06_EntradaCarroSecador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.Controls.Add(this.encabezado);
            this.Controls.Add(this.pnControles);
            this.Name = "a06_EntradaCarroSecador";
            this.pnControles.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnControles;
        private System.Windows.Forms.TextBox txCodCarro;
        private System.Windows.Forms.Label lbCarro;
        private System.Windows.Forms.DateTimePicker dtpHoraEntrada;
        private System.Windows.Forms.Label lbHoraEntrada;
        private System.Windows.Forms.Label lbHrs;
        private System.Windows.Forms.TextBox txTiempoSecado;
        private System.Windows.Forms.Label lbTiempoSecado;
        private System.Windows.Forms.Button btTerminar;
        private System.Windows.Forms.Button btAceptar;
        private Encabezado encabezado;
    }
}