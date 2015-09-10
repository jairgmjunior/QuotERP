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

    $("[name='pesquisarvarios-material-gridmaterialconsumomatriz']").on("click", function () {
        configureParametroPesquisarVariosMateriaisPorTipoItem("2");
        configureIdRequisicao("gridmaterialconsumomatriz");
    });

    $("[name='pesquisarvarios-material-gridmaterialconsumoitem']").on("click", function () {
        configureParametroPesquisarVariosMateriaisPorTipoItem("2");
        configureIdRequisicao("gridmaterialconsumoitem");
    });

    $("[name='pesquisarvarios-material-gridmaterialcomposicaocustoMatriz']").on("click", function () {
        configureIdRequisicao("gridmaterialcomposicaocustoMatriz");
    });

    $("#selecionar-material").on("pesquisar", function (ev, itens) {
        
        if ($("#IdRequisicao").val() == "gridmaterialconsumomatriz") {
            carregueGridMaterialConsumoMatriz(itens);
        } else if ($("#IdRequisicao").val() == "gridmaterialconsumoitem") {
            carregueGridMaterialConsumoItem(itens);
        } else if ($("#IdRequisicao").val() == "gridmaterialcomposicaocustoMatriz") {
            carregueGridMaterialComposicaoCustoMatriz(itens);
        }
    });

    $('#formMaterial').submit(function (e) {
        //e.preventDefault();
        limpeMensagensAlerta();

        var dataGridItens = $("#GridMaterialConsumoMatriz").data("kendoGrid").dataSource.data();

        var mensagem = "";
        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Quantidade == 0 || dataItem.Quantidade == null) {
                mensagem += "Na grid de materiais de consumo geral o item de referência: " + dataItem.Referencia + " não tem valor na coluna quantidade.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Custo == 0 || dataItem.Custo == null) {
                mensagem += "Na grid de materiais de consumo geral o item de referência: " + dataItem.Referencia + " não tem valor na coluna custo unitário.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.DepartamentoProducao == '' || dataItem.DepartamentoProducao == null) {
                mensagem += "Na grid de materiais de consumo geral o item de referência: " + dataItem.Referencia + " não tem valor na coluna departamento.<br/>";
            }
        }

        dataGridItens = $("#GridMaterialConsumoItem").data("kendoGrid").dataSource.data();

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Quantidade == 0 || dataItem.Quantidade == null) {
                mensagem += "Na grid de materiais de consumo por variação o item de referência: " + dataItem.Referencia + " não tem valor na coluna quantidade.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Custo == 0 || dataItem.Custo == null) {
                mensagem += "Na grid de materiais de consumo por variação o item de referência: " + dataItem.Referencia + " não tem valor na coluna custo unitário.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Tamanho == '' || dataItem.Tamanho == null) {
                mensagem += "Na grid de materiais de consumo por variação o item de referência: " + dataItem.Referencia + " não tem valor na coluna tamanho.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Variacao == '' || dataItem.Variacao == null) {
                mensagem += "Na grid de materiais de consumo por variação o item de referência: " + dataItem.Referencia + " não tem valor na coluna variação.<br/>";
            }
        }
        
        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.DepartamentoProducao == '' || dataItem.DepartamentoProducao == null) {
                mensagem += "Na grid de materiais de consumo por variação o item de referência: " + dataItem.Referencia + " não tem valor na coluna departamento.<br/>";
            }
        }

        dataGridItens = $("#GridMaterialComposicaoCustoMatriz").data("kendoGrid").dataSource.data();

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Custo == 0 || dataItem.Custo == null) {
                mensagem += "Na grid de materiais de composição de custo o item de referência: " + dataItem.Referencia + " não tem valor na coluna custo.<br/>";
            }
        }

        if (mensagem != "") {
            e.preventDefault();
            exibaAlertaErro(mensagem);
            $('#btnSubmit').button('reset');
            return false;
        }

        return true;
    });
}

function carregueGridMaterialConsumoMatriz(itens) {
    if (itens.DataItemsSelecionados.length == 0) {
        return;
    }

    var grid = $('#GridMaterialConsumoMatriz').data("kendoGrid");
    var model = grid.dataSource.options.schema.model;
    var dadosAtuais = grid.dataSource.data();
    var novosDados = itens.DataItemsSelecionados;


    for (var i = 0; i < novosDados.length; i++) {
        var dataItemNovo = novosDados[i];

        for (var j = 0; j < dadosAtuais.length; j++) {
            var dataItemAtual = dadosAtuais[j];
            if (dataItemNovo.Referencia == dataItemAtual.Referencia) {
                dataItemNovo.Descartado = true;
            }
        }
        if (!dataItemNovo.Descartado) {
            var dataItemNovoFinal = {
                Referencia: dataItemNovo.Referencia,
                Descricao: dataItemNovo.Descricao,
                UnidadeMedida: dataItemNovo.UnidadeMedida,
                Id: 0,
                Quantidade: 0,
                Custo: 0,
                CustoTotal: 0,
                DepartamentoProducao: '',
                Foto: dataItemNovo.Foto
            };

            dadosAtuais.unshift(dataItemNovoFinal);
        }
    }

    var dataSource = new kendo.data.DataSource({
        data: dadosAtuais,
        schema: {
            model: model
        }
    });
    dataSource.read();
    grid.setDataSource(dataSource);
    grid.refresh();
}

function carregueGridMaterialConsumoItem(itens) {
    if (itens.DataItemsSelecionados.length == 0) {
        return;
    }

    var grid = $('#GridMaterialConsumoItem').data("kendoGrid");
    var model = grid.dataSource.options.schema.model;
    var dadosAtuais = grid.dataSource.data();
    var novosDados = itens.DataItemsSelecionados;


    for (var i = 0; i < novosDados.length; i++) {
        var dataItemNovo = novosDados[i];

        for (var j = 0; j < dadosAtuais.length; j++) {
            var dataItemAtual = dadosAtuais[j];
            if (dataItemNovo.Referencia == dataItemAtual.Referencia) {
                dataItemNovo.Descartado = true;
            }
        }
        if (!dataItemNovo.Descartado) {
            var dataItemNovoFinal = {
                Referencia: dataItemNovo.Referencia,
                Descricao: dataItemNovo.Descricao,
                UnidadeMedida: dataItemNovo.UnidadeMedida,
                Id: 0,
                Quantidade: 0,
                Custo: 0,
                CustoTotal: 0,
                DepartamentoProducao: '',
                Tamanho: '',
                Variacao: '',
                CompoeCusto: '',
                Foto: dataItemNovo.Foto
            };

            dadosAtuais.unshift(dataItemNovoFinal);
        }
    }

    var dataSource = new kendo.data.DataSource({
        data: dadosAtuais,
        schema: {
            model: model
        }
    });
    dataSource.read();
    grid.setDataSource(dataSource);
    grid.refresh();
}

function carregueGridMaterialComposicaoCustoMatriz(itens) {
    if (itens.DataItemsSelecionados.length == 0) {
        return;
    }

    var grid = $('#GridMaterialComposicaoCustoMatriz').data("kendoGrid");
    var model = grid.dataSource.options.schema.model;
    var dadosAtuais = grid.dataSource.data();
    var novosDados = itens.DataItemsSelecionados;


    for (var i = 0; i < novosDados.length; i++) {
        var dataItemNovo = novosDados[i];

        for (var j = 0; j < dadosAtuais.length; j++) {
            var dataItemAtual = dadosAtuais[j];
            if (dataItemNovo.Referencia == dataItemAtual.Referencia) {
                dataItemNovo.Descartado = true;
            }
        }
        if (!dataItemNovo.Descartado) {
            var dataItemNovoFinal = {
                Referencia: dataItemNovo.Referencia,
                Descricao: dataItemNovo.Descricao,
                UnidadeMedida: dataItemNovo.UnidadeMedida,
                Id: 0,
                Quantidade: 0,
                Custo: 0,
                CustoTotal: 0,
                DepartamentoProducao: '',
                Tamanho: '',
                Variacao: '',
                CompoeCusto: '',
                Foto: dataItemNovo.Foto
            };

            dadosAtuais.unshift(dataItemNovoFinal);
        }
    }

    var dataSource = new kendo.data.DataSource({
        data: dadosAtuais,
        schema: {
            model: model
        }
    });
    dataSource.read();
    grid.setDataSource(dataSource);
    grid.refresh();
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

    if (e.container.index() == 1 || e.container.index() == 2 || e.container.index() == 3 || e.container.index() == 6) {
        this.closeCell();
    }

    registreScriptsGridMaterialConsumoMatriz();
}

function registreScriptsGridMaterialConsumoMatriz() {
    $("#formMaterial #Quantidade").blur(function (e) {
        atualizeCustoTotalGridMaterialConsumoMatriz(e.target);
    });

    $("#formMaterial #Custo").blur(function (e) {
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

function indexGridMaterialConsumoItem(dataItem) {
    var data = $("#GridMaterialConsumoItem").data("kendoGrid").dataSource.data();
    return data.indexOf(dataItem);
}

function onEditGridMaterialConsumoItem(e) {

    if (e.container.index() == 3 || e.container.index() == 4 || e.container.index() == 5 || e.container.index() == 8) {
        this.closeCell();
    }
    
    registreScriptsGridMaterialConsumoItem();
}

function registreScriptsGridMaterialConsumoItem() {
    $("#formMaterial #Quantidade.k-input").blur(function (e) {
        atualizeCustoTotalGridMaterialConsumoMatriz(e.target);
    });

    $("#formMaterial #Custo.k-input").blur(function (e) {
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


function onEditGridMaterialComposicaoCustoMatriz(e) {

    if (e.container.index() == 1 || e.container.index() == 2 || e.container.index() == 3) {
        this.closeCell();
    }
}

(function ($, kendo) {
    $.extend(true, kendo.ui.validator, {
        rules: {
            quantidadevalidation: function (input, params) {
                if (input.is("[name='Quantidade']") && input.val()) {
                    return input.val() != 0 && input.val() != '';
                }
                return true;
            },
            custovalidation: function (input, params) {
                if (input.is("[name='Custo']")) {
                    return input.val() != 0 && input.val() != '';
                }
                return true;
            }
        },
        messages: {
            quantidadevalidation: function (input) {
                input.attr("data-quantidadevalidation-msg", "A Qtd. não pode ser 0.");
                return input.attr("data-quantidadevalidation-msg");
            },
            custovalidation: function (input) {
                input.attr("data-custovalidation-msg", "O custo não pode ser 0.");
                return input.attr("data-custovalidation-msg");
            }
        }
    });
})(jQuery, kendo);


function indexGridMaterialComposicaoCustoMatriz(dataItem) {
    var data = $("#GridMaterialComposicaoCustoMatriz").data("kendoGrid").dataSource.data();
    return data.indexOf(dataItem);
}

