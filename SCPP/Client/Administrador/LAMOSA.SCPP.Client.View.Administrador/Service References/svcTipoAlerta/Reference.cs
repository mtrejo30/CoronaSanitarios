﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3603
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LAMOSA.SCPP.Client.View.Administrador.svcTipoAlerta {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TipoAlerta", Namespace="Lamosa.SCPP.Common.Entities.ClassImpl")]
    [System.SerializableAttribute()]
    public partial class TipoAlerta : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClaveField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CodigoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescripcionField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Clave {
            get {
                return this.ClaveField;
            }
            set {
                if ((object.ReferenceEquals(this.ClaveField, value) != true)) {
                    this.ClaveField = value;
                    this.RaisePropertyChanged("Clave");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Codigo {
            get {
                return this.CodigoField;
            }
            set {
                if ((this.CodigoField.Equals(value) != true)) {
                    this.CodigoField = value;
                    this.RaisePropertyChanged("Codigo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Descripcion {
            get {
                return this.DescripcionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescripcionField, value) != true)) {
                    this.DescripcionField = value;
                    this.RaisePropertyChanged("Descripcion");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="Lamosa.SCPP.ServiceManager.Interfaces", ConfigurationName="svcTipoAlerta.ITipoAlertaServiceManager")]
    public interface ITipoAlertaServiceManager {
        
        [System.ServiceModel.OperationContractAttribute(Action="Lamosa.SCPP.ServiceManager.Interfaces/ITipoAlertaServiceManager/Obtener", ReplyAction="Lamosa.SCPP.ServiceManager.Interfaces/ITipoAlertaServiceManager/ObtenerResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(LAMOSA.SCPP.Client.View.Administrador.svcTipoAlerta.TipoAlerta))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(System.Collections.Generic.List<object>))]
        System.Collections.Generic.List<object> Obtener(object tipoAlerta);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface ITipoAlertaServiceManagerChannel : LAMOSA.SCPP.Client.View.Administrador.svcTipoAlerta.ITipoAlertaServiceManager, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class TipoAlertaServiceManagerClient : System.ServiceModel.ClientBase<LAMOSA.SCPP.Client.View.Administrador.svcTipoAlerta.ITipoAlertaServiceManager>, LAMOSA.SCPP.Client.View.Administrador.svcTipoAlerta.ITipoAlertaServiceManager {
        
        public TipoAlertaServiceManagerClient() {
        }
        
        public TipoAlertaServiceManagerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TipoAlertaServiceManagerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TipoAlertaServiceManagerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TipoAlertaServiceManagerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Collections.Generic.List<object> Obtener(object tipoAlerta) {
            return base.Channel.Obtener(tipoAlerta);
        }
    }
}
