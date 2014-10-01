'use strict';

$(function () {
    var jcrop;
    var progress = $('.progress');
    var bar = $('.bar');

    var sendFile = function () {

        var $inputFile = $(this);
        var $fileupload = $inputFile.closest('.fileupload');

        // Espera o componente completar a ação
        setTimeout(function () {

            // Verifica se o input está apontando para algum arquivo
            if ($inputFile.val() == '') {
                $('#NomeArquivoUpload').val('');
                return;
            }

            // Cria um formulário em memória para enviar o arquivo
            var form = $('<form action="/Arquivo/UploadArquivoTemp" enctype="multipart/form-data" method="post"></form>');

            // Criar um input=file e deixar no lugar do que será enviado.
            var inputFileClone = $inputFile.clone();
            inputFileClone.one('change', sendFile);
            inputFileClone.insertAfter($inputFile);
            form.append($inputFile);

            // Buscar o input que será guardado o nome do arquivo salvo no disco
            var inputFilename = inputFileClone.siblings('input[name=NomeArquivoUpload]');

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

                    // Guarda o nome do arquivo, para enviá-lo ao servidor
                    inputFilename.val(data.nome);
                },
                error: function (response, status, err) {
                    alert(err);
                }
            }); // ajaxSubmit
        }, 50); // setTimeout
    }; // sendFile

    // Ao mudar a imagem, submeter o formulário
    $('.fileupload input').on('change', sendFile);
})