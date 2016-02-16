using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "CondicionPastaAutoriza", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class CondicionPastaAutoriza : BaseSolutionEntity
    {
        #region PrivateFields
        private int iCodCondicionPasta = -1;
       
        private int iUsuarioAutoriza = -1;
 
        #endregion

        #region Properties
        [DataMember(Name = "CodCondicionPasta")]
        public int CodCondicionPasta { get { return iCodCondicionPasta; } set { iCodCondicionPasta = value; } }
        
        [DataMember(Name = "UsuarioAutoriza")]
        public int UsuarioAutoriza { get { return iUsuarioAutoriza; } set { iUsuarioAutoriza = value; } }
        

        #endregion

        #region Methods
        public CondicionPastaAutoriza(int iCodCondicionPasta,
              
                int iUsuarioAutoriza
             )
        {
            this.iCodCondicionPasta = iCodCondicionPasta;
            
            this.iUsuarioAutoriza = iUsuarioAutoriza;
           
        }
        public CondicionPastaAutoriza()
        { }
        public CondicionPastaAutoriza(DataRow row)
        {
            SetPropertiesFromDataRow(row);
        }
        ~CondicionPastaAutoriza()
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
            return GetPropertyNamesArray(new CondicionPasta());
        }
        #endregion
    }
}
