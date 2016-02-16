

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "CondicionOperacionAutoriza", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class CondicionOperacionAutoriza : BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodCondicionOperacion = -1;
        private int iUsuarioAutoriza = -1;
       

        #endregion

        #region Properties
        [DataMember(Name = "CodCondicionOperacion")]
        public int CodCondicionOperacion { get { return iCodCondicionOperacion; } set { iCodCondicionOperacion = value; } }
        
        [DataMember(Name = "UsuarioAutoriza")]
        public int UsuarioAutoriza { get { return iUsuarioAutoriza; } set { iUsuarioAutoriza = value; } }
    
        #endregion

        #region Methods
        public CondicionOperacionAutoriza(int iCodCondicionOperacion,
              
                int iUsuarioAutoriza
        )
        {
            this.iCodCondicionOperacion = iCodCondicionOperacion;
           
            this.iUsuarioAutoriza = iUsuarioAutoriza;
         
        }
        public CondicionOperacionAutoriza()
        { }
        public CondicionOperacionAutoriza(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~CondicionOperacionAutoriza()
        { }

        public object[] ToObjectArray()
        {
            return ToObjectArray(this);
        }
        /// <summary>
        /// Obtiene un arreglo con los nombres solamente de las propiedades
        /// </summary>
        /// <returns></returns>
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new CondicionEsmalte());
        }

        #endregion
    }
}
