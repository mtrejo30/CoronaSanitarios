namespace LAMOSA.SCPP.Client.View.HandHeld.User_Interface
{
    partial class frmBienvenida
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBienvenida));
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblSCPP = new System.Windows.Forms.Label();
            this.lblNumeroVersion = new System.Windows.Forms.Label();
            this.lblMensajeInicioSesion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pbLogo
            // 
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.Location = new System.Drawing.Point(0, 3);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(238, 60);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // lblVersion
            // 
            this.lblVersion.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblVersion.Location = new System.Drawing.Point(13, 75);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(78, 20);
            this.lblVersion.Text = "Versión: ";
            // 
            // lblSCPP
            // 
            this.lblSCPP.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lblSCPP.Location = new System.Drawing.Point(3, 106);
            this.lblSCPP.Name = "lblSCPP";
            this.lblSCPP.Size = new System.Drawing.Size(231, 52);
            this.lblSCPP.Text = "Sistema de Control de Procesos de Producción";
            this.lblSCPP.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblNumeroVersion
            // 
            this.lblNumeroVersion.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblNumeroVersion.Location = new System.Drawing.Point(115, 75);
            this.lblNumeroVersion.Name = "lblNumeroVersion";
            this.lblNumeroVersion.Size = new System.Drawing.Size(91, 20);
            // 
            // lblMensajeInicioSesion
            // 
            this.lblMensajeInicioSesion.Font = new System.Drawing.Font("Tahoma", 7F, System.Drawing.FontStyle.Regular);
            this.lblMensajeInicioSesion.Location = new System.Drawing.Point(3, 158);
            this.lblMensajeInicioSesion.Name = "lblMensajeInicioSesion";
            this.lblMensajeInicioSesion.Size = new System.Drawing.Size(231, 137);
            this.lblMensajeInicioSesion.Text = "Mensaje de Inicio de Sesión";
            this.lblMensajeInicioSesion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // frmBienvenida
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(238, 295);
            this.ControlBox = false;
            this.Controls.Add(this.lblMensajeInicioSesion);
            this.Controls.Add(this.lblNumeroVersion);
            this.Controls.Add(this.lblSCPP);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.pbLogo);
            this.Name = "frmBienvenida";
            this.Text = "Control Procesos Producción";
            this.Load += new System.EventHandler(this.frmBienvenida_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblSCPP;
        private System.Windows.Forms.Label lblNumeroVersion;
        private System.Windows.Forms.Label lblMensajeInicioSesion;
    }
}