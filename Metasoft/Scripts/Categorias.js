
/* Verificar se pode excluir este script */
function SaveNewCategoria() {

    var nomecategoria = $("#nome").val();
    var tipo = $("#tipo").val();

    if (nomecategoria) {
        $.ajax({
            cache: false,
            type: "POST",
            url: '/Categorias/SaveNewCategoria',
            data: {
                "nome": nomecategoria,
                "tipo": tipo,
            },
            success: function (data) {
                $("#nome").val('')
                msg("Categoria cadastrada com sucesso");
                window.setTimeout('location.reload()', 1000);
            },
            error: function (xhr, ajaxOptions, thrownError) {

                alert('Falha ao cadastrar Categoria');
            }
        });
    }
    else {

        alert("Dados incompletos.Por favor preecher todos os campos!")
    }
}


