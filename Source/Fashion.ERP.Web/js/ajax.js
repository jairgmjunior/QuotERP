'use strict';

/**
    Método chamado ao concluir com sucesso o envio de um formulário via ajax
    parâmetros: modalName = nome do div bootstrap-modal
**/
function onAjaxFormSucess(modalName) {
    $(modalName).modal('hide');

    // Limpa o formulário dentro do popup
    clearForm($(modalName));

    // Refresh no grid
    var gridName = 'grid' + modalName.substr(modalName.indexOf('-'));
    $('#' + gridName).data('kendoGrid').dataSource.read();
}

/**
    Método chamado ao concluir o envio de um formulário via ajax
    parâmetros: jqXHR jqXHR, String textStatus, String errorThrown
**/
function onAjaxFormComplete(jqXhr, textStatus, errorThrown) {
    
    switch (textStatus) {
        case 'success':
            //alert(jqXhr.responseText);
            break;
        case 'error':
            if (errorThrown) {
                alert('A requisição falhou: ' + errorThrown);
            } else {
                alert(jqXhr.responseText);
            }
            $(document).find(':button').each(function () {
                $(this).button('reset');
            });
            break;
        case 'notmodified':
        case 'timeout':
        case 'abort':
        case 'parsererror':
            alert('Ocorreu um erro desconhecido: ' + textStatus);
            break;
    }
}

$(document).ajaxError(function (xhr, status, error) {
    //if (error) {
    //    alert(error);
    //} else {
    //    alert(xhr.responseText);
    //}
});

/**
    Método chamado quando ocorrer algum erro na operação da KendoUi Grid
**/
function onKendoGridError(e) {
    console.log(e.errorThrown);
    this.cancelChanges();
}