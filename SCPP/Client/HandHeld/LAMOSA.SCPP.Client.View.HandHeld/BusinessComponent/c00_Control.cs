using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LAMOSA.SCPP.Client.View.HandHeld
{

    public class Validacion
    {

        #region fields

        private bool bValidacionExitosa = false;
        private string sMensajeValidacion = string.Empty;
        private int iCodPieza = -1;

        #endregion fields

        #region properties

        public bool ValidacionExitosa { get { return this.bValidacionExitosa; } set { this.bValidacionExitosa = value; } }
        public string MensajeValidacion { get { return this.sMensajeValidacion; } set { this.sMensajeValidacion = value; } }
        public int CodPieza { get { return this.iCodPieza; } set { this.iCodPieza = value; } }

        #endregion properties

        #region methods

        #region constructors and destructor
        public Validacion()
        {

        }
        public Validacion(  bool bValidacionExitosa, string sMensajeValidacion, int iCodPieza)
        {
            this.bValidacionExitosa = bValidacionExitosa;
            this.sMensajeValidacion = sMensajeValidacion;
            this.iCodPieza = iCodPieza;
        }
        ~Validacion()
        {

        }
        #endregion constructors and destructor

        #endregion methods

    }

    public class ValidacionPieza
    {

        #region fields

        private bool bValidacionExitosa = false;
        private bool bValProcesoExitosa = false;
        private bool bValNoDefDespExitosa = false;
        private string sMensajeValidacion = string.Empty;
        private int iCodPieza = -1;
        private int iCodPproceso = -1;

        #endregion fields

        #region properties

        public bool ValidacionExitosa { get { return this.bValidacionExitosa; } set { this.bValidacionExitosa = value; } }
        public bool ValProcesoExitosa { get { return this.bValProcesoExitosa; } set { this.bValProcesoExitosa = value; } }
        public bool ValNoDefDespExitosa { get { return this.bValNoDefDespExitosa; } set { this.bValNoDefDespExitosa = value; } }
        public string MensajeValidacion { get { return this.sMensajeValidacion; } set { this.sMensajeValidacion = value; } }
        public int CodPieza { get { return this.iCodPieza; } set { this.iCodPieza = value; } }
        public int CodProceso { get { return this.iCodPproceso; } set { this.iCodPproceso = value; } }

        #endregion properties

        #region methods

        #region constructors and destructor
        public ValidacionPieza()
        {

        }
        public ValidacionPieza(bool bValidacionExitosa, bool bValProcesoExitosa, bool bValNoDefDespExitosa, string sMensajeValidacion, int iCodPieza)
        {
            this.bValidacionExitosa = bValidacionExitosa;
            this.bValProcesoExitosa = bValProcesoExitosa;
            this.bValNoDefDespExitosa = bValNoDefDespExitosa;
            this.sMensajeValidacion = sMensajeValidacion;
            this.iCodPieza = iCodPieza;
        }
        public ValidacionPieza(bool bValidacionExitosa, bool bValProcesoExitosa, bool bValNoDefDespExitosa, string sMensajeValidacion, int iCodPieza, int iCodPproceso)
        {
            this.bValidacionExitosa = bValidacionExitosa;
            this.bValProcesoExitosa = bValProcesoExitosa;
            this.bValNoDefDespExitosa = bValNoDefDespExitosa;
            this.sMensajeValidacion = sMensajeValidacion;
            this.iCodPieza = iCodPieza;
            this.iCodPproceso = iCodPproceso;
        }
        ~ValidacionPieza()
        {

        }
        #endregion constructors and destructor

        #endregion methods

    }

}
