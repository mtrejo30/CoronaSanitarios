using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "CondicionEsmalteAutoriza", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class CondicionEsmalteAutoriza : BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodCondicionEsmalte = -1;
     
        private int iUsuarioAutoriza = -1;
       

        #endregion

        #region Properties
        [DataMember(Name = "CodCondicionEsmalte")]
        public int CodCondicionEsmalte { get { return iCodCondicionEsmalte; } set { iCodCondicionEsmalte = value; } }
        
        [DataMember(Name = "UsuarioAutoriza")]
        public int UsuarioAutoriza { get { return iUsuarioAutoriza; } set { iUsuarioAutoriza = value; } }
    
        #endregion

        #region Methods
        public CondicionEsmalteAutoriza(int iCodCondicionEsmalte,
              
                int iUsuarioAutoriza
        )
        {
            this.iCodCondicionEsmalte = iCodCondicionEsmalte;
           
            this.iUsuarioAutoriza = iUsuarioAutoriza;
         
        }
        public CondicionEsmalteAutoriza()
        { }
        public CondicionEsmalteAutoriza(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~CondicionEsmalteAutoriza()
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
