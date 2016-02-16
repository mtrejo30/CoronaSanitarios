﻿using System;
using System.Runtime.Serialization;
using System.Data;
using Common.SolutionEntityFramework;

namespace LAMOSA.SCPP.Server.BusinessEntity
{
    [DataContract(Name = "HHMolde", Namespace = "http://LAMOSA/SCPP/BE")]
    [Serializable]
    public class HHMolde : BaseSolutionEntity
    {

        #region Fields

        private int iCodMolde = -1;

        #endregion Fields

        #region Properties

        [DataMember(Name = "CodMolde")]
        public int CodMolde { get { return this.iCodMolde; } set { this.iCodMolde = value; } }

        #endregion Properties

        #region Methods

        #region Constructors and Destructor

        public HHMolde()
        {

        }
        public HHMolde(DataRow row)
        {
            base.SetPropertiesFromDataRow(row);
        }
        public HHMolde
        (
            int iCodMolde
        )
        {
            this.iCodMolde = iCodMolde;
        }
        ~HHMolde()
        {

        }

        #endregion Constructors and Destructor

        #region Common

        #region GetPropertyNamesArray
        /// <summary>
        /// Obtiene un arreglo con los nombres solamente de las propiedades
        /// </summary>
        /// <returns>string[]</returns>
        public static string[] GetPropertyNamesArray()
        {
            return GetPropertyNamesArray(new HHMolde());
        }
        #endregion GetPropertyNamesArray
        #region GetPropertyValuesArray
        /// <summary>
        /// Obtiene un arreglo de objetos con los valores de las propiedades
        /// </summary>
        /// <returns>objetct[]</returns>
        public object[] ToObjectArray()
        {
            return GetPropertyValuesArray(this);
        }
        #endregion GetPropertyValuesArray

        #endregion Common

        #endregion Methods

    }
}