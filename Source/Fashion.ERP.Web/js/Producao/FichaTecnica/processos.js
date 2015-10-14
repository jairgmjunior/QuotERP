function displayDepartamentoProducao(id) {
    if (id == '' || id == null) {
        return '';
    }
    var departamentoProducaosDicionario = $("#DepartamentoProducaosDicionario").data("value");
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

function displaySetorProducao(id) {
    if (id == '' || id == null) {
        return '';
    }
    var setorProducaosDicionario = $("#SetorProducaosDicionario").data("value");
    var nome = "";
    $.each(setorProducaosDicionario, function (index) {
        if (index == id) {
            nome = this;
            return false;
        }
        return true;
    });
    return nome;
}

function displayOperacaoProducao(id) {
    if (id == '' || id == null) {
        return '';
    }
    var operacaoProducaosDicionario = $("#OperacaoProducaosDicionario").data("value");
    var nome = "";
    $.each(operacaoProducaosDicionario, function (index) {
        if (index == id) {
            nome = this;
            return false;
        }
        return true;
    });
    return nome;
}

function indexGridFichaTecnicaProcessos(dataItem) {
    var data = $("#GridFichaTecnicaProcessos").data("kendoGrid").dataSource.data();
    return data.indexOf(dataItem);
}

function filtroSetores() {
    var grid = $("#GridFichaTecnicaProcessos").data("kendoGrid");
    var editRow = grid.tbody.find("tr:has(.k-edit-cell)");
    var model = grid.dataItem(editRow);
    return {
        IdDepartamento: model.DepartamentoProducao
    };
}

function filtroOperacoes() {
    var grid = $("#GridFichaTecnicaProcessos").data("kendoGrid");
    var editRow = grid.tbody.find("tr:has(.k-edit-cell)");
    var model = grid.dataItem(editRow);
    return {
        setorProducaoId: model.SetorProducao
    };
}

function onEditGrid(e) {

    $('#DepartamentoProducao').change(function (evt) {
        var item = ObtenhaDataItemGridItensProcessos(evt.target);
        item.set("SetorProducao", null);
        item.set("OperacaoProducao", null);
        item.set("Custo", 0);
        item.set("Tempo", 0);
        item.set("PesoProdutividade", 0);
    });

    $('#DepartamentoProducao').change(function (evt) {
        var item = ObtenhaDataItemGridItensProcessos(evt.target);
        item.set("OperacaoProducao", null);
        item.set("Custo", 0);
        item.set("Tempo", 0);
        item.set("PesoProdutividade", 0);
    });

    $('#OperacaoProducao').change(function (evt) {
        var operacaoProducaoId = $("#OperacaoProducao").val();
        if (operacaoProducaoId != '') {
            var url = '/Comum/OperacaoProducao/ObtenhaOperacaoProducao?operacaoProducaoId=' + operacaoProducaoId;
            $.getJSON(url, function (result) {
                var item = ObtenhaDataItemGridItensProcessos(evt.target);
                item.set("Custo", result.Custo);
                item.set("Tempo", result.Tempo);
                item.set("PesoProdutividade", result.PesoProdutividade);
            }).fail(function (jqXhr, textStatus, errorThrown) {
                alert(errorThrown);
            });
        }
    });
}

function ObtenhaDataItemGridItensProcessos(target) {
    var row = $(target).closest("tr");
    var grid = $('#GridFichaTecnicaProcessos').data("kendoGrid");
    return grid.dataItem(row);
}

function onContentLoadProcessos(e) {
    $('#formProcessos').submit(function (ev) {
        var dataGridItens = $("#GridFichaTecnicaProcessos").data("kendoGrid").dataSource.data();

        if (dataGridItens.length == 0) {
            alert("Insira pelo menos um item na grid.");
            $('#btnSubmitProcessos').button('reset');
            e.preventDefault();
            return false;
        }

        var mensagem = "";

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.DepartamentoProducao == '' || dataItem.DepartamentoProducao == null) {
                mensagem += "Um item não tem valor para departamento.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.SetorProducao == '' || dataItem.SetorProducao == null) {
                mensagem += "Um item não tem valor para setor.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.OperacaoProducao == '' || dataItem.OperacaoProducao == null) {
                mensagem += "Um item não tem valor para operação.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Custo == 0 || dataItem.Custo == null) {
                mensagem += "Um item não tem valor na coluna custo.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Tempo == 0 || dataItem.Tempo == null) {
                mensagem += "Um item não tem valor na coluna tempo.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.PesoProdutividade == 0 || dataItem.PesoProdutividade == null) {
                mensagem += "O item de referência: " + dataItem.Referencia + " não tem valor na coluna peso produtividade.<br/>";
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
    
    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var tab = $('li[aria-controls="tabstrip-2"]');

    if ($("#Id").val() != '') {
        tabStrip.enable(tab, true);
    } else {
        tabStrip.enable(tab, false);
    }
}

(function ($, kendo) {
    $.extend(true, kendo.ui.validator, {
        rules: {
            custovalidation: function (input, params) {
                if (input.is("[name='Custo']")) {
                    return input.val() != '' && input.val() != 0;
                }
                return true;
            },
            tempovalidation: function (input, params) {
                if (input.is("[name='Tempo']")) {
                    return input.val() != '' && input.val() != 0;
                }
                return true;
            },
            pesoprodutividadevalidation: function (input, params) {
                if (input.is("[name='PesoProdutividade']")) {
                    return input.val() != '' && input.val() != 0;
                }
                return true;
            },
            operacaoproducaoduplicadavalidation: function (input, params) {
                if (input.is("[name='OperacaoProducao']")) {
                    var dataItem = ObtenhaDataItemGridItensProcessos(input);

                    if (dataItem.OperacaoProducao == null) {
                        return true;
                    }

                    var dadosGrid = $('#GridFichaTecnicaProcessos').data("kendoGrid").dataSource.data();

                    for (var i = 0; dadosGrid.length > i; i++) {
                        var dataItemAtual = dadosGrid[i];
                        if (dataItem.OperacaoProducao == dataItemAtual.OperacaoProducao &&
                            dataItem.SetorProducao == dataItemAtual.SetorProducao &&
                            dataItem.DepartamentoProducao == dataItemAtual.DepartamentoProducao &&
                            dataItem.uid != dataItemAtual.uid) {
                            //dataItemAtual.OperacaoProducao = '';
                            return false;
                        }
                    }
                }
                return true;
            }
        },
        messages: {
            custovalidation: function (input) {
                input.attr("data-custovalidation-msg", "O Custo não pode ser 0.");
                return input.attr("data-custovalidation-msg");
            },
            tempovalidation: function (input) {
                input.attr("data-tempovalidation-msg", "O Tempo não pode ser 0.");
                return input.attr("data-custovalidation-msg");
            },
            pesoprodutividadevalidation: function (input) {
                input.attr("data-pesoprodutividadevalidation-msg", "O Peso Produtividade não pode ser 0.");
                return input.attr("data-pesoprodutividadevalidation-msg");
            },
            operacaoproducaoduplicadavalidation: function (input) {
                input.attr("data-operacaoproducaoduplicadavalidation-msg", "Essa operação já foi informada para esse departamento e setor.");
                return input.attr("data-operacaoproducaoduplicadavalidation-msg");
            }
        }
    });
})(jQuery, kendo);
