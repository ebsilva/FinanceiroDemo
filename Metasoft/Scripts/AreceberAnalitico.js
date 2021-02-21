
function ShowDetails(id) {
    var element = "#row_" + id;
    if (!$(element).is(":visible")) {
        $(element).show();
    }
    else {
        $(element).hide();
    }

}
function PagamentoExcluir(id) {

    bootbox.confirm({
        message: "Confirma exclusão deste pagamento ?",
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result === true) {
                ExcluirPagamento(id);
            }

        }
    });
}
function ExcluirPagamento(id) {
    $.ajax({
        cache: false,
        type: "POST",
        url: '/ContaPrs/ExcluirPagamento',
        data: {
            "id": id,
        },
        success: function (data) {
            /* Update clientList*/

            msg("Exclusão de pagamento feita com sucesso")
            window.setTimeout('location.reload()', 2000);

        },
        error: function (xhr, ajaxOptions, thrownError) {

            msg('Falha ao excluir pagamento');
        }
    });
}

