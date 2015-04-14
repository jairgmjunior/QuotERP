function iconControl() {
    $(".btn-edit").prepend("<i class='fa fa-pencil fa-fw'></i>"); //botão editar
    $(".btn-inativar").prepend("<i class='fa fa-ban fa-fw'></i>"); //botão inativar
    $(".btn-ativar").prepend("<i class='fa fa-power-off fa-fw'></i>"); //botão ativar
    
    $("#menu > li:nth-child(2) > a:first-child").prepend("<i class='fa fa-archive fa-fw'></i>"); //Menu
    $("#menu > li:nth-child(3) > a:first-child").prepend("<i class='fa fa-edit fa-fw'></i>"); //Menu
    $("#menu > li:nth-child(4) > a:first-child").prepend("<i class='fa fa-shopping-cart fa-fw'></i>"); //Menu
    $("#menu > li:nth-child(5) > a:first-child").prepend("<i class='fa fa-cogs fa-fw'></i>"); //Menu
    $("#menu > li:nth-child(6) > a:first-child").prepend("<i class='fa fa-money fa-fw'></i>"); //Menu
}


