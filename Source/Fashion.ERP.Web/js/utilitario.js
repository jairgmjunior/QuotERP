"use strict";

/* Extende jQuery com um método exists() para verificar se um selector retornou resultados */
$.fn.exists = function() {
    return this.length !== 0;
};

/* Verifica se é um número */
function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}

function isDate(val) {
    var d = new Date(val); // bug: não aceita a data "17/10/2014"
    return !isNaN(d.valueOf());
}

/* Limpa o formulário e restaura os campos para o estado original */
function clearForm(element) {
    $(element).find(':input').each(function () {
        
        switch (this.type) {
            case 'password':
            case 'select-multiple':
            case 'text':
            case 'textarea':
                $(this).val('');
                break;
            case 'select-one':
                $(this)[0].selectedIndex = 0;
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
                break;
        }
    });
    $(element).find(':button').each(function () {
        $(this).button('reset');
    });
    // Apaga o campo de descrição da pesquisa
    $(element).find('span').each(function () {
        var $this = $(this);
        if ($this.hasClass('add-on')) {
            $this.text('');
        }
    });
    // Seleciona o primerio radio
    $('input:radio[disabled=false]:first').attr('checked', true);
}

// Padroniza a função preventDefault/returnValue entre os browsers
//if (!Event.prototype.preventDefault) {
//    Event.prototype.preventDefault = function () {
//        event.returnValue = false;
//    };
//}

$.fn.serializeObject = function () {
    var o = {};
    var a = this.serializeArray();
    $.each(a, function () {
        if (o[this.name] !== undefined) {
            if (!o[this.name].push) {
                o[this.name] = [o[this.name]];
            }
            o[this.name].push(this.value || '');
        } else {
            o[this.name] = this.value || '';
        }
    });
    return o;
};

function ObtenhaModelJsonObject() {
    var parametros = {
        model: $("form").serializeObject()
    };

    return parametros;
}