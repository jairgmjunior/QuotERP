function displayVariacao_Material(id) {
    var variacaosDicionario = $("#VariacaosDicionario_Material").data("value");
    var nome = "";
    $.each(variacaosDicionario, function (index) {
        if (index == id) {
            nome = this;
            return false;
        }
        return true;
    });
    return nome;
}

function displayTamanho_Material(id) {
    var tamanhosDicionario = $("#TamanhosDicionario_Material").data("value");
    var nome = "";
    $.each(tamanhosDicionario, function (index) {
        if (index == id) {
            nome = this;
            return false;
        }
        return true;
    });
    return nome;
}

function displayDepartamentoProducao_Material(id) {
    var departamentoProducaosDicionario = $("#DepartamentoProducaosDicionario_Material").data("value");
    var nome = "";
    $.each(departamentoProducaosDicionario, function (index) {
        if (index == id) {
            nome = this;
            return false;
        }
        return true;
    });
    return nome;
}

function indexGridMaterialConsumoMatriz(dataItem) {
    var data = $("#GridMaterialConsumoMatriz").data("kendoGrid").dataSource.data();
    return data.indexOf(dataItem);
}

function onContentLoadMaterial(e) {
    
    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var tab = $('li[aria-controls="tabstrip-3"]');

    if ($("#Id").val() != '') {
        tabStrip.enable(tab, true);
    } else {
        tabStrip.enable(tab, false);
    }

    reparseForm();
}

function reparseForm() {
    var form = $("#formMaterial")
        .removeData("validator") 
        .removeData("unobtrusiveValidation");  

    $.validator.unobtrusive.parse(form);
}

function error_handler(e) {
    if (e.errors) {
        var message = "Errors:\n";
        $.each(e.errors, function (key, value) {
            if ('errors' in value) {
                $.each(value.errors, function () {
                    message += this + "\n";
                });
            }
        });
        alert(message);
    }
}

function onEditGridMaterialConsumoMatriz(e) {
    $("#formMaterial #Descricao").attr("readonly", true);
    //$("#Descricao").width('86%');
    $("#formMaterial #UnidadeMedida").attr("readonly", true);
    $("#formMaterial #UnidadeMedida").addClass("input-small");
    
    //$("#Quantidade").data("kendoNumericTextBox").wrapper.width("100px");
    if (!e.model.isNew()) {
        $("#formMaterial #Referencia").attr("readonly", true);
        $("#formMaterial #pesquisar-material").attr("disabled", true);
    }

    $("#formMaterial #CustoTotal").data("kendoNumericTextBox").enable(false);
    $("#formMaterial #CustoTotal").data("kendoNumericTextBox").wrapper
           .find(".k-numeric-wrap")
           .addClass("expand-padding")
           .find(".k-select").hide();

    //$('#Referencia').change(function (evt) {
    //    limpeLinhaGridMaterialConsumoMatriz();
    //});

    registreScriptsGridMaterialConsumoMatriz();
}

function registreScriptsGridMaterialConsumoMatriz() {
    $("#formMaterial #Quantidade.k-input").change(function (e) {
        atualizeCustoTotalGridMaterialConsumoMatriz(e.target);
    });

    $("#formMaterial #Custo.k-input").change(function (e) {
        atualizeCustoTotalGridMaterialConsumoMatriz(e.target);
    });
}

function atualizeCustoTotalGridMaterialConsumoMatriz(target) {
    var item = obtenhaDataItemGridMaterialConsumoMatriz(target);
    var custoTotal = item.Quantidade * item.Custo;
    item.set("CustoTotal", custoTotal);
}

function obtenhaDataItemGridMaterialConsumoMatriz(target) {
    var row = $(target).closest("tr");
    var grid = $('#formMaterial #GridMaterialConsumoMatriz').data("kendoGrid");
    return grid.dataItem(row);
}

//function limpeLinhaGridMaterialConsumoMatriz() {
//    var gridItens = $('#formMaterial #GridMaterialConsumoMatriz').data("kendoGrid");
//    var tr = $("#Referencia").closest("tr");
//    var dataGridItens = gridItens.dataItem(tr);

//    dataGridItens.set("Descricao", null);
//    dataGridItens.set("Referencia", null);
//    dataGridItens.set("UnidadeMedida", null);
//    dataGridItens.set("Custo", null);
//    dataGridItens.set("CustoTotal", null);
//    dataGridItens.set("Quantidade", null);
//    dataGridItens.set("DepartamentoProducao", null);
//}

function indexGridMaterialConsumoItem(dataItem) {
    var data = $("#GridMaterialConsumoItem").data("kendoGrid").dataSource.data();
    return data.indexOf(dataItem);
}

function onEditGridMaterialConsumoItem(e) {
    $("#formMaterial #Descricao").attr("readonly", true);
    //$("#Descricao").width('86%');
    $("#formMaterial #UnidadeMedida").attr("readonly", true);
    $("#formMaterial #UnidadeMedida").addClass("input-small");

    //$("#Quantidade").data("kendoNumericTextBox").wrapper.width("100px");
    if (!e.model.isNew()) {
        $("#formMaterial #Referencia").attr("readonly", true);
        $("#formMaterial #pesquisar-material").attr("disabled", true);
    }

    $("#formMaterial #CustoTotal").data("kendoNumericTextBox").enable(false);
    $("#formMaterial #CustoTotal").data("kendoNumericTextBox").wrapper
           .find(".k-numeric-wrap")
           .addClass("expand-padding")
           .find(".k-select").hide();

    registreScriptsGridMaterialConsumoItem();
}

//function limpeGridMaterialConsumoItem() {

//    var dataGridItem = obtenhaDataItemGridMaterialConsumoItem($("#Referencia"));

//    dataGridItem.set("Descricao", null);
//    dataGridItem.set("Referencia", null);
//    dataGridItem.set("UnidadeMedida", null);
//    dataGridItem.set("Custo", null);
//    dataGridItem.set("CustoTotal", null);
//    dataGridItem.set("Quantidade", null);
//    dataGridItem.set("DepartamentoProducao", null);
//}

function registreScriptsGridMaterialConsumoItem() {
    $("#formMaterial #Quantidade.k-input").change(function (e) {
        atualizeCustoTotalGridMaterialConsumoMatriz(e.target);
    });

    $("#formMaterial #Custo.k-input").change(function (e) {
        atualizeCustoTotalGridMaterialConsumoItem(e.target);
    });
}

function atualizeCustoTotalGridMaterialConsumoItem(target) {
    var item = obtenhaDataItemGridMaterialConsumoItem(target);
    var custoTotal = item.Quantidade * item.Custo;
    item.set("CustoTotal", custoTotal);
}

function obtenhaDataItemGridMaterialConsumoItem(target) {
    var row = $(target).closest("tr");
    var grid = $('#formMaterial #GridMaterialConsumoItem').data("kendoGrid");
    return grid.dataItem(row);
}

(function ($, kendo) {
    $.extend(true, kendo.ui.validator, {
        rules: {
            referenciamaterialvalidation: function (input, params) {
                if (input.is("[name='Referencia']")) {
                    return input.val() != '';
                }
                return true;
            }
        },
        messages: {
            referenciamaterialvalidation: function (input) {
                input.attr("data-referenciamaterialvalidation-msg", "O material deve ser selecionado");
                return input.attr("data-referenciamaterialvalidation-msg");
            }
        }
    });
})(jQuery, kendo);

function indexGridMaterialComposicaoCustoMatriz(dataItem) {
    var data = $("#GridMaterialComposicaoCustoMatriz").data("kendoGrid").dataSource.data();
    return data.indexOf(dataItem);
}

