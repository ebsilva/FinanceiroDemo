function AddNewHistory(fid,tipo,texto) {
        $.ajax({
            cache: false,
            type: "POST",
            url: '/ContaPrs/AddNewHistory',
            data: {
                "fid": fid,
                "tipo": tipo,
                "texto":texto,
            },
            success: function (data) {
                /* Update clientList*/
                msg("Historico cadastrado com sucesso");

            },
            error: function (xhr, ajaxOptions, thrownError) {

                msg('Falha ao cadastrar historico');
            }
        });
   
}
function ShowDetails(id) {
    var element = "#row_" + id;
    if (!$(element).is(":visible")) {
        $(element).show();
    }
    else {
        $(element).hide();
    }

}

function ExcluirPagamento(id) {
    $.ajax({
        cache: false,
        type: "POST",
        url: '/ContaPrs/ExcluirPagamento',
        data: {
            "id": id
        },
        success: function (data) {
            /* Update clientList*/

            msg("Exclusão de pagamento feita com sucesso");
            window.setTimeout('location.reload()', 2000);

        },
        error: function (xhr, ajaxOptions, thrownError) {

            msg('Falha ao excluir pagamento');
        }
    });
}
function ResetInputBackcolor(id) {
    document.getElementById(id).style.removeProperty("background-color");
}

