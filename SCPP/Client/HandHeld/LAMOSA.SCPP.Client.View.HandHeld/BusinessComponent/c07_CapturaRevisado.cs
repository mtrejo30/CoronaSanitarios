using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.SqlServerCe;
using DA = LAMOSA.SCPP.Client.View.HandHeld.DataAccess;

namespace LAMOSA.SCPP.Client.View.HandHeld
{
    public class c07_CapturaRevisado
    {

        #region fields

        private c00_Common oDA0 = new c00_Common();

        // Control de Excepciones.
        private string sClassName = string.Empty;

        #endregion fields

        #region methods

        #region Constructors and Destructor
        public c07_CapturaRevisado()
        {
            this.sClassName = this.GetType().FullName;
        }
        ~c07_CapturaRevisado()
        {

        }
        #endregion Constructors and Destructor

        #region Common


        
        #endregion common

        #endregion methods

    }
}
