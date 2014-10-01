'use strict';

/** jQuery.validator **/
if ($.validator && $.validator.unobtrusive) {

    /**
        Customiza o layout ao validar um campo de acordo com o Twitter.Bootstrap.
    **/
    jQuery.validator.setDefaults({
        highlight: function (element, errorClass, validClass) {
            if (element.type === 'radio') {
                this.findByName(element.name).addClass(errorClass).removeClass(validClass);
            } else {
                $(element).addClass(errorClass).removeClass(validClass);
                $(element).closest('.control-group').removeClass('success').addClass('error');
            }
        },
        unhighlight: function (element, errorClass, validClass) {
            if (element.type === 'radio') {
                this.findByName(element.name).removeClass(errorClass);
            } else {
                $(element).removeClass(errorClass);
                $(element).closest('.control-group').removeClass('error');
            }
        }
    });
    
    /**
        Verifica se o campo do tipo numérico é válido.
    **/
    $.validator.methods.number = function (value, element) {

        if (value == '') {
            return true;
        }

        return !isNaN(Globalize.parseFloat(value));
    };

    /**
        Valida o intervalo entre dois números.
    **/
    $.validator.methods.range = function (value, element, param) {

        if (value === '') {
            return true;
        }

        var globalizedValue = Globalize.parseFloat(value);
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    };
    
}

/** CPF
    Verifica se o CPF é válido.
    parâmetros: String cpf
**/
function verificarCPF(cpf) {

    if (cpf == null ||
        cpf == "" ||
        $.trim(cpf) == "" ||
        cpf == "11111111111" ||
        cpf == "22222222222" ||
        cpf == "33333333333" ||
        cpf == "44444444444" ||
        cpf == "55555555555" ||
        cpf == "66666666666" ||
        cpf == "77777777777" ||
        cpf == "88888888888" ||
        cpf == "99999999999" ||
        cpf == "00000000000") {
        return false;
    }

    var digitoDigitado = eval(cpf.charAt(9) + cpf.charAt(10));
    var soma1 = 0, soma2 = 0;
    var vlr = 11;
    for (var i = 0; i < 9; i++) {
        soma1 += eval(cpf.charAt(i) * (vlr - 1));
        soma2 += eval(cpf.charAt(i) * vlr);
        vlr--;
    }
    soma1 = (((soma1 * 10) % 11) == 10 ? 0 : ((soma1 * 10) % 11));
    soma2 = (((soma2 + (2 * soma1)) * 10) % 11);
    var digitoGerado = (soma1 * 10) + soma2;

    if (digitoGerado != digitoDigitado) {
        return false;
    }

    return true;
}

/** CMC7
    Valida o CMC7 do cheque.
    parâmetros: String numero
**/
function ValidarCMC7(numero) {
    var tam = numero.length;

    if (tam < 30) {
        return false;
    }

    var num1 = numero.substring(8, 18);
    var digCalc1 = CalcDigMod10(num1);
    var num2 = '000' + numero.substring(0, 7);
    var digCalc2 = CalcDigMod10(num2);
    var num3 = numero.substring(19, 29);
    var digCalc3 = CalcDigMod10(num3);

    return numero == numero.substring(0, 7) + digCalc1 + num1 + digCalc2 + num3 + digCalc3;;
}

/** CMC7
    Calcula o dígito pelo módulo 10.
    parâmetros: String numero
**/
function CalcDigMod10(numero) {
    var tam = numero.length;
    var soma = 0;
    var pos = true;

    for (var cont = 0; cont < tam; cont++) {
        var aux = numero.substring(cont, cont + 1);

        if (pos) {
            pos = false;
            soma = new Number(soma) + new Number(aux);
        }
        else {
            pos = true;
            soma = aux > 4 ? soma + (aux * 2) - 9 : soma + (aux * 2);
        }
    }

    var resto = soma % 10;
    return resto == 0 ? 0 : 10 - resto;
}