$(function() {
    $('.Entero').live(
    'keypress', function(event) {
        var key = window.event ? event.keyCode : event.which;
        if ((key > 47 && key < 58)) {
            return true;
        }
        return false;
    });


    $('.Decimal').live(
    'keypress', function(event) {
        var key = window.event ? event.keyCode : event.which;
        var caracter = String.fromCharCode(key);
        if (caracter == '.' && $(this).val()) return ($(this).val().indexOf('.') == -1)
        return /\d/.test(caracter);
    });
    $('.Decimal').live(
    'blur', function() {
        var valor = $(this).val()
        var lastCaracter = valor.substring(valor.length - 1)
        if (lastCaracter == '.')
            $(this).val(valor.substring(0, valor.length - 1))
    });
});