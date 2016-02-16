/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("LAMOSA.SCPP.Client.View.Administrador");

function LoadcmbMachine(cod_CT, cod_machine) {
    cod_machine = cod_machine ? cod_machine : -1;
    LAMOSA.SCPP.Client.View.Administrador.WebServiceSeg.LoadcmbMachine(cod_CT, cod_machine, SucceededCallback, FailedCallback, "");
}

function ObtenerMaquinas(codigoArea, codigoCentroTrabajo, codigoPlanta, codigoProceso, codigoMaquina) {
    codigoMaquina = codigoMaquina ? codigoMaquina : -1;
    LAMOSA.SCPP.Client.View.Administrador.WebServiceSeg.ObtenerMaquinas(codigoArea, codigoCentroTrabajo, codigoPlanta, codigoProceso, codigoMaquina, SucceededCallback, FailedCallback, "");
}

function SucceededCallback(result, eventArgs) {
    // Page element to display feedback.
    fillCmbMachine(result);
}
function FailedCallback(error) {
    // Display the error.
    alert("Error: " + error.get_message());
}
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
