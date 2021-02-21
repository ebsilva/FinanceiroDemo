//window.setTimeout('location.reload()', 1000);
function SaveNewContato(clienteid) {

    var nomecontato = $("#nomecontato").val();
    var emailcontato = $("#emailcontato").val();
    var fonecontato = $("#fonecontato").val();
    var celularcontato = $("#celularcontato").val();
    var sexocontato = $("#sexocontato").val();


    if (clienteid && nomecontato && emailcontato && fonecontato) {
        $.ajax({
            cache: false,
            type: "POST",
            url: '/Clientes/SaveNewContato',
            data: {
                "clientid": clienteid,
                "nome": nomecontato,
                "email": emailcontato,
                "fone": fonecontato,
                "celular": celularcontato,
                "sexo": sexocontato
            },
            success: function (data) {
                /* Update clientList*/
                $("#nomecontato").val('')
                $("#emailcontato").val('');
                $("#fonecontato").val('');
                $("#celularcontato").val('');
                $("#sexocontato").val('');
                msg("Contato cadastrado com sucesso")
              
            },
            error: function (xhr, ajaxOptions, thrownError) {

                msg('Falha ao cadastrar lista de contatos');
            }
        });
    }
    else {

        alert("Dados incompletos.Por favor preecher todos os campos!")
    }
}
function ShowClientDetails(id) {
  
    var element = "#row_" + id;
    if (!$(element).is(":visible")) {
        $(element).show();
    }
    else {
        $(element).hide();
    }

}
function SetClientID(id) {
    jsclientid = id;
}

function ShowContatos(clienteid) {

    var element = "#contatos_" + clienteid;
    if (!$(element).is(":visible")) {
        var container = $('#divcontatos_' + clienteid);
        $(container).load("/Clientes/ShowContatos", {
            "clienteid": clienteid,
            viewName: "_Contatos"
        });
        $("#contatos_" + clienteid).slideDown(500);
    }
    else {
        $("#contatos_" + clienteid).hide();
    }



}
