function displayTamanho_Modelagem(id) {
    var tamanhosDicionario = $("#TamanhosDicionario_Modelagem").data("value");
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

function index(dataItem) {
    var data = $("#GridMedidas").data("kendoGrid").dataSource.data();
    return data.indexOf(dataItem);
}

function onContentLoadModelagem(e) {
    
    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var tab = $('li[aria-controls="tabstrip-4"]');

    if ($("#Id").val() != '') {
        tabStrip.enable(tab, true);
    } else {
        tabStrip.enable(tab, false);
    }
    inicializeArquivoUpload();

    // Ao mudar a imagem, submeter o formulário
    //$('.fileupload input').on('change', sendFile);

    $('#formModelagem').submit(function (ev) {

        var formValido = formularioEhValido();

        if (!$("#formModelagem").valid() || !formValido) {
            e.preventDefault();
            $('#btnSubmit').button('reset');
            return false;
        }

        var dataGridItens = $("#GridMedidas").data("kendoGrid").dataSource.data();

        var mensagem = "";
        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.DescricaoMedida == "" || dataItem.DescricaoMedida == null) {
                mensagem += "Insira a descrição da medida de modelagem.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Tamanho == "" || dataItem.Tamanho == null) {
                mensagem += "Insira o tamanho da medida de modelagem.<br/>";
            }
        }

        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.Medida == 0 || dataItem.Medida == null) {
                mensagem += "Insira o valor da medida de modelagem.<br/>";
            }
        }

        if (mensagem != "") {
            e.preventDefault();
            exibaAlertaErro(mensagem);
            $('#btnSubmit').button('reset');
            return false;
        }

        
        if (!$("#formModelagem").valid()) {
            $('#btnSubmit').button('reset');
            return false;
        }

        return true;
    });

    reparseFormModelagem();
}

function formularioEhValido() {
    var retorno = true;

    if ($("#codigo-funcionario").val() == false) {
        $('#funcionario').val("");
        $('#funcionario').valid();
        $('#descricao-funcionario').text("");
        retorno = false;
    }

    //// Fornecedor 
    //if ($(".fileupload-preview").text() != "" && $("#NomeArquivo").val() == "") {
    //    $('#NomeArquivo').addClass('input-validation-error');
    //    exibaAlertaErro('Insira o nome do arquivo.');
    //    retorno =  false;
    //}

    return retorno;
}

function onEditGridMedidas(e) {
    //correção de um comportamento não esperado do kendo nessa página
    $("#formModelagem #DescricaoMedida").blur(function (e) {
        var row = $(e.target).closest("tr");
        var grid = $('#GridMedidas').data("kendoGrid");
        var item = grid.dataItem(row);
        item.set("DescricaoMedida", $("#formModelagem #DescricaoMedida").val());
    });
}

function reparseFormModelagem() {
    var form = $("#formModelagem")
        .removeData("validator")
        .removeData("unobtrusiveValidation");

    $.validator.unobtrusive.parse(form);
}