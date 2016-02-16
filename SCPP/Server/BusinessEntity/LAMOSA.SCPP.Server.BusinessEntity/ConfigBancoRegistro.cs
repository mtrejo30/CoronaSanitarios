using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace LAMOSA.SCPP.Server.BusinessEntity{
	[DataContract(Name="ConfigBancoResgistro", Namespace="http://LAMOSA/SCPP/BE")]
	[Serializable]
	public class ConfigBancoResgistro
	{
	#region PrivateFields
		private int icodMaquina = -1;
		private int icodMolde = -1;
		private int ilimitevaciadas = -1;
		private int ivaciadasdia = -1;
		private int iCantMoldes = -1;
		private int icodUsuarioAlta = -1;
		private bool iActivo = false;
		private int icodConfigBanco = -1;
        private int iNumeroImpresiones = -1;

#endregion

#region Properties
		[DataMember(Name = "CodMaquina")]
		 public int CodMaquina { get { return icodMaquina; } set { icodMaquina = value; } }
		[DataMember(Name = "CodMolde")]
		 public int CodMolde { get { return icodMolde; } set { icodMolde = value; } }
		[DataMember(Name = "Limitevaciadas")]
		 public int Limitevaciadas { get { return ilimitevaciadas; } set { ilimitevaciadas = value; } }
		[DataMember(Name = "Vaciadasdia")]
		 public int Vaciadasdia { get { return ivaciadasdia; } set { ivaciadasdia = value; } }
		[DataMember(Name = "CantMoldes")]
		 public int CantMoldes { get { return iCantMoldes; } set { iCantMoldes = value; } }
		[DataMember(Name = "CodUsuarioAlta")]
		 public int CodUsuarioAlta { get { return icodUsuarioAlta; } set { icodUsuarioAlta = value; } }
		[DataMember(Name = "Activo")]
		 public bool Activo { get { return iActivo; } set { iActivo = value; } }
		[DataMember(Name = "CodConfigBanco")]
		 public int CodConfigBanco { get { return icodConfigBanco; } set { icodConfigBanco = value; } }
        [DataMember(Name = "NumeroImpresiones")]
        public int NumeroImpresiones { get { return iNumeroImpresiones; } set { iNumeroImpresiones = value; } }

	#endregion

	#region Methods
		public ConfigBancoResgistro()
		{ }
        ~ConfigBancoResgistro()
		{ }

	#endregion
	}
}

