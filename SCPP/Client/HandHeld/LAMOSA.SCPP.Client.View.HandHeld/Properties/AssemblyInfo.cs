using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Sistema de Control de Procesos de Producción")]
[assembly: AssemblyDescription("Sistema de Control de Procesos de Producción")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Lamosa S.A. de C.V.")]
[assembly: AssemblyProduct("Sistema de Control de Procesos de Producción")]
[assembly: AssemblyCopyright("Copyright © Lamosa 2011")]
[assembly: AssemblyTrademark("Lamosa S.A. de C.V.")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("9747f5e0-7593-404d-aabc-7e6fc79ebda8")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("3.0.2.5")]

// Below attribute is to suppress FxCop warning "CA2232 : Microsoft.Usage : Add STAThreadAttribute to assembly"
// as Device app does not support STA thread.
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2232:MarkWindowsFormsEntryPointsWithStaThread")]
