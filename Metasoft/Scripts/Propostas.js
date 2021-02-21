/*-------------------------------------------------------------------------
  Aceitar/Negar Proposta
--------------------------------------------------------------------------*/
function AceitarProposta(propostaid, dataaceite) {

    $.ajax({
        cache: false,
        type: "POST",
        url: '/Propostas/AceitarProposta',
        data: {
            "propostaid": propostaid.toString(),
            "dataaceite": dataaceite
        },
        success: function (data) {
            msg('Proposta aceita com sucesso');
            submitform("ListaPropostas");
        },
        error: function (xhr, ajaxOptions, thrownError) {

            alert('Falha ao atualizar proposta');
        }
    });
}
function NegarProposta(propostaid, dataaceite,motivo) {

    $.ajax({
        cache: false,
        type: "POST",
        url: '/Propostas/NegarProposta',
        data: {
            "propostaid": propostaid.toString(),
            "dataaceite": dataaceite,
            "motivo": motivo,
        },
        success: function (data) {
            msg('Proposta negada com sucesso');
            submitform("ListaPropostas");
        },
        error: function (xhr, ajaxOptions, thrownError) {

            alert('Falha ao atualizar proposta');
        }
    });
}

function LiberarProposta(propostaid) {

    $.ajax({
        cache: false,
        type: "POST",
        url: '/Propostas/LiberarProposta',
        data: {
            "propostaid": propostaid.toString()
        },
        success: function (data) {
            msg('Proposta liberada para faturamento com sucesso');
            window.setTimeout('location.reload()', 2000);
        },
        error: function (xhr, ajaxOptions, thrownError) {

            alert('Falha ao liberar proposta');
        }
    });
}

function AvancarFluxo(propostaid, currentstatus,novostatus) {

    $.ajax({
        cache: false,
        type: "POST",
        url: '/Propostas/AvancarFluxo',
        data: {
            "propostaid": propostaid.toString(),
            "currentstatus": currentstatus.toString()
        },
        success: function (data) {
            msg('Situacao movida para '+novostatus + ' com sucesso');
            window.setTimeout('location.reload()', 2000);
        },
        error: function (xhr, ajaxOptions, thrownError) {

            alert('Falha ao move proposta para ' + novostatus);
        }
    });
}

function VoltarFluxo(propostaid,currentstatus) {

    $.ajax({
        cache: false,
        type: "POST",
        url: '/Propostas/VoltarFluxo',
        data: {
            "propostaid": propostaid.toString(),
            "currentstatus": currentstatus.toString()
        },
        success: function (data) {
            msg('Fluxo da proposta retrocedido com sucesso');
            window.setTimeout('location.reload()', 2000);
        },
        error: function (xhr, ajaxOptions, thrownError) {

            alert('Falha ao retroceder fluxo da proposta');
        }
    });
}

function ShowPropostaDetails(id) {
    
    var element = "#row_" + id;
    if (!$(element).is(":visible")) {
        ShowContasProposta(id);

        $(element).show();
    }
    else {
        $(element).hide();
    }

}
function SetClientID(id) {
    jsclientid = id;
}

function SavePagamentosProposta() {
    var cliid = $("#pagcliid").text();
    var propid = $("#pagpropid").text();
   
    var npar = $("#qtp").val();
    var valor = $("#valpar").val();
    var vencto = $("#vencto").val();
    var valorptbr = parseFloat(valor);

    
    if (cliid && propid && vencto && npar  && valor) {
        $.ajax({
            cache: false,
            type: "POST",
            url: '/Propostas/SavePagamentosProposta',
            data: {
                "np": npar.toString(),
                "clienteid": cliid.toString(),
                "propostaid": propid.toString(),
                "valor": valorptbr.toString(),
                "vencimento": vencto.toString(),
            },
            success: function (data) {
                $("#pagcliid").text('');
                $("#pagpropid").text('');
                $("#qtp").val('');
                $("#valpar").val('0');
                $("#vencto").val(Hoje());
                msg('Pagamentos cadastrados com sucesso');
            },
            error: function (xhr, ajaxOptions, thrownError) {

                alert('Falha ao cadastrar pagamentos');
            }
        });
    }
    else {

        alert("Dados incompletos.Por favor preecher todos os campos!");
    }
}








