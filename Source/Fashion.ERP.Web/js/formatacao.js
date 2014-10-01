'use strict';

/** Number.ToText
    Converte um número para texto.
**/
Number.prototype.toText = function (n) {
    n = (isNaN(n)) ? 2 : n;

    if (!$.isNumeric(this)) {
        var pad = '';
        for (var i = 0; i < n; i++) {
            pad = pad + '0';
        }
        return pad.length == 0 ? '0' : '0,' + pad; // Retorna 0,00 (se n = 2)
    }
    return new String(this.toFixed(n)).replace('.', ',');
};


/** Text.ToNumber
    Converte um texto para número.
**/
String.prototype.toNumber = function () {
    var valor = Globalize.parseFloat(this);

    return isNaN(valor) ? 0 : valor;
};

/** FormataData
    Formata a data no formato (dd/MM/yyyy)
**/
function formataData(campo) {
    campo.value = filtraCampo(campo);
    var vr = limpaCampo(campo.value, '0123456789');
    var tam = vr.length;
    if (tam <= 1)
        campo.value = vr;
    if (tam > 2 && tam < 5)
        campo.value = vr.substr(0, tam - 2) + '/' + vr.substr(tam - 2, tam);
    if (tam >= 5 && tam <= 10)
        campo.value = vr.substr(0, 2) + '/' + vr.substr(2, 2) + '/' + vr.substr(4, 4);
}

/** FormataData
    Limpa todos os caracteres especiais do campo solicitado
**/
function filtraCampo(campo) {
    var s = '';
    var vr = campo.value;
    var tam = vr.length;
    for (var i = 0; i < tam ; i++) {
        if (vr.substring(i, i + 1) != "/" && vr.substring(i, i + 1) != "-" && vr.substring(i, i + 1) != "." && vr.substring(i, i + 1) != ",") {
            s = s + vr.substring(i, i + 1);
        }
    }
    campo.value = s;
    return campo.value;
}

/** FormataData
    Retira caracteres invalidos da string
**/
function limpaCampo(valor, validos) {
    var result = '';
    var aux;
    for (var i = 0; i < valor.length; i++) {
        aux = validos.indexOf(valor.substring(i, i + 1));
        if (aux >= 0) {
            result += aux;
        }
    }
    return result;
}