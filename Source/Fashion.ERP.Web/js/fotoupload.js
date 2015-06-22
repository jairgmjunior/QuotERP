'use strict';

function inicializeFotoUpload() {
    var jcrop;
    var progress = $('.progress');
    var bar = $('.bar');

    var sendFile = function () {

        var $inputFile = $(this);
        var $fileupload = $inputFile.closest('.fileupload');
        var inputFilename = $inputFile.siblings('input[name=FotoNome]');

        // Espera o componente completar a ação
        setTimeout(function () {

            // Verifica se o input está apontando para algum arquivo
            if ($inputFile.val() == '') {
                $('#imagem-avatar').attr("src", '/Content/images/no_image.jpg');
                $('#FotoNome').val('');
                return;
            }


            // Buscar o input que será guardado o nome do arquivo salvo no disco
            //inputFilename = $inputFile.siblings('input[name=FotoNome]');

            // Cria um formulário em memória para enviar o arquivo
            var form = $('<form action="/Arquivo/UploadFotoTemp" enctype="multipart/form-data" method="post"></form>');

            // Criar um input=file e deixar no lugar do que será enviado.
            var inputFileClone = $inputFile.clone();
            inputFileClone.one('change', sendFile);
            inputFileClone.insertAfter($inputFile);
            form.append($inputFile);

            // Verificar se é para usar o Crop
            var useCrop = $fileupload.attr('data-crop') == 'crop';


            // Envia a imagem via ajax
            form.ajaxSubmit({
                dataType: 'json',
                uploadProgress: function (event, position, total, percentComplete) {
                    progress.show();
                    var percentVal = percentComplete + '%';
                    bar.width(percentVal);
                    console.log("upload: " + percentVal);
                },
                success: function (data) {
                    progress.hide();
                    bar.width("0%");

                    if (data.Error) {
                        alert(data.Error);
                        $fileupload.fileupload('clear');
                        return;
                    }

                    // Guarda o nome da foto, para enviá-lo ao servidor
                    inputFilename.val(data.fotoNome);

                    if (useCrop) {

                        $('<img/>') // Copia a imagem para a memória
                            .attr('src', data.url)
                            .load(function () {

                                // Verifica o tamanho da imagem
                                if (this.width < 150 || this.height < 150) {
                                    alert('Imagem muito pequena, por favor selecione outra.');
                                    $("#modal-foto").modal('hide');
                                    $fileupload.fileupload('clear');
                                    $('#imagem-avatar').attr("src", '/Content/images/no_image.jpg');
                                    $('#FotoNome').val('');
                                    return;
                                }

                                $('#urlTemp').val(data.url);
                                $('#imagem-crop').attr('src', data.url);
                                $("#modal-foto").modal('show');
                                $('#btn-submit-foto').button('reset');

                                // Ativa o jcrop apenas quando a imagem estiver carregada
                                ativarJCrop(this.width, this.height);
                            });
                    } else {
                        $('#imagem-avatar').attr("src", data.url);
                    }
                },
                error: function (response, status, err) {
                    alert(err);
                }
            }); // ajaxSubmit
        }, 50); // setTimeout
    }; // sendFile

    // Ao mudar a imagem, submeter o formulário
    $('.fileupload input').on('change', sendFile);

    $('#excluir-foto').on('click', function () {
        // todo: excluir $('.fileupload')
        //var fotoId = $("#Foto").val();
        //var form = $('<form action="/Arquivo/Excluir/' + fotoId + '" method="post"></form>');
        //form.ajaxSubmit({
        //    dataType: 'json',
        //    success: function (data) {
        //        if (data.error) {
        //            alert(data.error);
        //            return;
        //        }
        //        alert(data.result);
        //        $("#Foto").val(0);
        //    },
        //    error: function (response, status, err) {
        //        alert(err);
        //    }
        //});
    });

    $('#crop-form').ajaxForm({
        dataType: 'json',
        success: function (data) {
            if (data.error) {
                alert(data.error);
                return;
            }
            jcrop.destroy();
            $('#modal-foto').modal('hide');
            $('#imagem-avatar').attr("src", data.url);
        },
        error: function (response, status, err) {
            alert(err);
        }
    });

    function ativarJCrop(width, height) {

        $('#imagem-crop').Jcrop({
            bgColor: 'black',
            bgOpacity: .4,
            aspectRatio: 1,
            setSelect: [50, 50, 200, 200],
            minSize: [150, 150],
            boxWidth: 520,
            boxHeight: 400,
            trueSize: [width, height],
            onSelect: saveCoords
        }, function () { jcrop = this; });
    }

    function saveCoords(c) {
        $("#x").val(parseInt(c.x));
        $("#y").val(parseInt(c.y));
        $("#w").val(parseInt(c.w));
        $("#h").val(parseInt(c.h));
        //console.log("x: " + c.x + "\r\ny: " + c.y + "\r\nw: " + c.w + "\r\nh: " + c.h);
    }
}

$(function () {
    inicializeFotoUpload();
})