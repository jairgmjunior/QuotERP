//function displayVariacao(id) {
//    var variacaosDicionario = $("#VariacaosDicionario").data("value");
//    var nome = "";
//    $.each(variacaosDicionario, function (index) {
//        if (index == id) {
//            nome = this;
//            return false;
//        }
//        return true;
//    });
//    return nome;
//}

//function displayCor(id) {
//    var corsDicionario = $("#CorsDicionario").data("value");
//    var nome = "";
//    $.each(corsDicionario, function (index) {
//        if (index == id) {
//            nome = this;
//            return false;
//        }
//        return true;
//    });
//    return nome;
//}

//function indexGridFichaTecnicaVariacao(dataItem) {
//    var data = $("#GridFichaTecnicaVariacao").data("kendoGrid").dataSource.data();
//    return data.indexOf(dataItem);
//}

//function onContentLoadBasicos(e) {
//    $('#formBasicos').submit(function (ev) {
//        var dataGridItens = $("#GridFichaTecnicaVariacao").data("kendoGrid").dataSource.data();

//        if (dataGridItens.length == 0) {
//            alert("Insira pelo menos uma variação.");
//            $('#btnSubmitBasicos').button('reset');
//            e.preventDefault();
//            return false;
//        }
        
//        if (!$("#formBasicos").valid()) {
//            $('#btnSubmit').button('reset');
//            return false;
//        }

//        return true;
//    });

//    reparseFormBasicos();
//}

//function reparseFormBasicos() {
//    var form = $("#formBasicos")
//        .removeData("validator")
//        .removeData("unobtrusiveValidation");

//    $.validator.unobtrusive.parse(form);
//}