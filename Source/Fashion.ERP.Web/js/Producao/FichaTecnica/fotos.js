
function index(dataItem) {
    var data = $("#GridFotos").data("kendoGrid").dataSource.data();
    return data.indexOf(dataItem);
}

function onContentLoadFotos(e) {
    
    var tabStrip = $("#tabstrip").data("kendoTabStrip");
    var tab = $('li[aria-controls="tabstrip-5"]');

    if ($("#Id").val() != '') {
        tabStrip.enable(tab, true);
    } else {
        tabStrip.enable(tab, false);
    }

    if ($(e.contentElement).prop("id") == "tabstrip-5") {
        inicializeFotoUpload();
    }
    
    reparseFormFotos();
    
    $('#incluirfoto').on('click', function () {
        $('#modal-fichatecnicafoto').modal('show').one('hidden', function () {
            var body = $('#modal-fichatecnicafoto .modal-body');
            clearForm(body);
            // Reseta o componente de foto
            $('#imagem-avatar').attr("src", '/Content/images/no_image.jpg');
            $('#FotoNome').val('');
            $('[name="foto"]').val('');
            $('#modal-fichatecnicafoto .fileupload').removeClass('fileupload-exists').addClass('fileupload-new');
            inicializeFotoUpload();
        });
    });

    $('#btn-salvar-fichatecnicafoto').on('click', function () {
        //var body = $('#modal-fichatecnicafoto .modal-body');

        var grid = $('#GridFotos').data("kendoGrid");
        var dataSource = grid.dataSource;

        var impressao = $("[name='impressaomodal']").val() == "true" ? true : false;
        var padrao = $("[name='padraomodal']").val() == "true" ? true : false;
        var fotoNome = $("[name='FotoNome']").val();
        var fotoTitulo = $("#fototitulomodal").val();

        if (fotoNome == '') {
            alert("Selecione uma foto.");
            return;
        }

        if (fotoTitulo == '') {
            alert("Insira um título para a foto");
            return;
        }
        
        var dataGridItens = dataSource.data();
        for (var i = 0; i < dataGridItens.length; i++) {
            var dataItem = dataGridItens[i];
            if (dataItem.FotoTitulo == fotoTitulo) {
                alert("Não é possível inserir fotos com o mesmo título.");
                return;
            }

            if (dataItem.Padrao.toString() == "true" && padrao.toString() == "true") {
                alert("Já existe uma foto padrão.");
                return;
            }
        }

        dataSource.add({ Id: 0, Impressao: impressao, Padrao: padrao, FotoNome: fotoNome, FotoTitulo: fotoTitulo });
        dataSource.sync();

        // Fecha o popup
        $('#modal-fichatecnicafoto').modal('hide');
    });
}

function reparseFormFotos() {
    var form = $("#formFotos")
        .removeData("validator")
        .removeData("unobtrusiveValidation");

    $.validator.unobtrusive.parse(form);
}

function onEditGridFotos(e) {

    if (e.container.index() == 1 || e.container.index() == 2 || e.container.index() == 3 || e.container.index() == 4) {
        this.closeCell();
    }

    registreScriptsGridMaterialConsumoMatriz();
}