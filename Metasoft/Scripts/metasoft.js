/*
  GlobalSettings (
 1) id da tabela,
 2) Altura do painel das linhas,
 3) Coluna para sort inicial,

 4) true para presença de Datepicker De e Até,
 5) true para drop down meses,
6) true para drop down Ano,
7) Se tiver Drop down ClienteFornecer  Letra para Tipo (C ou F ),
8) Se tiver DropDown Categorias letra para tipo (P ou R),
9) true se tiver Dropdown responsaveis,
10) Se tiver dropdown Sitacao ou Status
 11) true se tiver Callback para total
  */

function Tools() { };
function Fmt() { };

Tools.prototype.AnimateIcons = () => {
    AnimateY("#pagetitle");
    $(".panel-default").slideDown(500);
    $(".topicons").fadeIn(1500);
};
Tools.prototype.Hoje = function () {
    var d = new Date();
    let dia = d.getDate();
    let mes = d.getMonth() + 1;
    let ano = d.getFullYear();

    if (dia < 10) { dia =  "0" +dia.toString()  }
    if (mes < 10) { mes = "0" + mes.toString()  }
    console.log(mes)
    var strDate = dia + "/" + mes + "/" + ano;
    
    return strDate;
};
Tools.prototype.Mask = function () {
    for (var i = 0; i < arguments.length; i++) {
         ebs.SetMask(arguments[i]);
    }
};
Tools.prototype.SetMask = element => {
    var elements = [
        { element: 'valor', mask: '###0,00' },
        { element: 'fone', mask: '(00) 0000-0000' },
        { element: 'date', mask: '00/00/0000' },
        { element: 'vencimento', mask: '00/00/0000' },
        { element: 'cel', mask: '(00) 0-0000-0000' },
        { element: 'cnpj', mask: '00.000.000/0000-00' },
        { element: 'ie', mask: '0000000000' },
        { element: 'f0800', mask: '0000-000000' },
        { element: 'cep', mask: '00000-000' },
        { element: 'noordem', mask: '00/00' },
        { element: 'npar', mask: '00' },
        { element: 'he', mask: '00000' },
        { element: 'previsao', mask: '00000' }

    ];
    
    var el = elements.filter(t => t.element === element);
    if (element === 'valor') {
        $("#" + el[0].element).maskMoney({ allowNegative: false, thousands: '.', decimal: ',', affixesStay: false });
    }
    else {
        $("#" + el[0].element).mask(el[0].mask);
    }
  
};
// Tirar tudo isso agora fiaca numa funcao so
Tools.prototype.SetValorMask = element => $(element).maskMoney({ prefix: 'R$ ', allowNegative: false, thousands: '.', decimal: ',', affixesStay: false });
Tools.prototype.SetFoneMask = element => $(element).mask("(00) 0000-0000"); 
Tools.prototype.SetDateMask = element => $(element).mask("00/00/0000"); 
Tools.prototype.SetCelFoneMask = element =>  $(element).mask("(00) 0-0000-0000");
Tools.prototype.SetCnpjMask = element => $(element).mask("00.000.000/0000-00"); 
Tools.prototype.SetCepMask = element => $(element).mask("00000-000"); 
Tools.prototype.SetIeMask = element => $(element).mask("0000000000"); 
Tools.prototype.Set0800Mask = element => $(element).mask("0000-000000"); 
//-------
Tools.prototype.SetSelectValue = (select, value) => document.getElementById(select).value = value.toString();
Tools.prototype.SelectWhenChangeSubmit = function (dropdown, downloadtofalse) {
    $(dropdown).change(
        function () {
            if (downloadtofalse) { DownloadFalse(); }
            this.form.submit();
        }
    );
};
Tools.prototype.PopDropDown = function (dropdown, pdata) {
    var targets = [
        { dropdown: 'clientes', url: '/ContaPrs/GetCliFor' },
        {dropdown: 'clienteid', url: '/Contratos/PopulateClientesWithProposta'},
        { dropdown: 'cliforid', url: '/ContaPrs/GetCliFor' },
        { dropdown: 'fornecedores', url: '/ContaPrs/GetCliFor' },
        { dropdown: 'categorias', url: '/ContaPRs/PopulateCategorias'},
        { dropdown: 'categoriaid', url: '/ContaPRs/PopulateCategorias'},
        { dropdown: 'responsaveis', url: '/Propostas/GetResponsaveis' },
        { dropdown: 'indice', url: '/Contratos/PopulateIndices'}
    ];
    //var target = targets.filter(function (t) {
    //    return t.dropdown === dropdown;
    //});
    var target = targets.filter(t => t.dropdown === dropdown);

        $.ajax({
        cache: false,
        type: "POST",
        url: target[0].url,
        data: pdata[0],
        success: function (data) {
            var dd = $("#" + target[0].dropdown);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option[Object.keys(option)[0]] + '">' + option[Object.keys(option)[1]] + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
};
Tools.prototype.Set_DatePicker = function () {
    $('.input-group.date').datepicker({
        format: 'dd/mm/yyyy',
        language: 'pt-BR',
        weekStart: 0,
        autoclose: true,
        todayHighlight: true
    });
};
Tools.prototype.Search = function (tablename, text, start, end) {
    var table, td;
    table = document.getElementById(tablename);
    for (var i = 0; i < table.rows.length; i++) {
        var s = false;
        for (var c = start; c <= end; c++) {
            var content = table.rows[i].cells[c].innerHTML;
            if (content.indexOf(text) >= 0) {
                s = true;
                break;
            }
        }
        if (s) {
            table.rows[i].style.display = '';
        }
        else {
            table.rows[i].style.display = 'none';
        }

    }
};

Tools.prototype.MesExtenso = function(mes) {
    var meses = ["", "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"];
    return meses[mes];
}
Tools.prototype.MesCorrente = function () {
    return parseInt(new Date().getMonth());
}

Tools.prototype.AnoCorrente = function () {
    return new Date().getFullYear();
}
Tools.prototype.IsNull = (str) => {
    return str.trim().length <1 || str === 'unidefined' ;
}


var ebs = new Tools();

function GlobalSettings(table, scrollY, ordercol, setDeAteFilter, setMesesFilter, setAnoFilter, setCliForFilter, setCategorias, setReponsaveis, setSituacaoFilter, hasCallBack) {
    ebs.AnimateIcons();
    SetDataTable(table, scrollY, ordercol, hasCallBack);

    // De  - Ate
    if (setDeAteFilter) {
        ebs.SetDateMask('#de');
        ebs.SetDateMask('#ate');
        // $("#de").mask("00/00/0000");
        //$("#ate").mask("00/00/0000");
        if (setAnoFilter) {
            $('#de').change(function () {
                DownloadFalse();
                $("#anos").val("0");
                this.form.submit();
            });
        }
        else {
            $('#de').change(function () {
                DownloadFalse(); this.form.submit();
            });
        }
        if (setAnoFilter) {
            $('#ate').change(function () {
                DownloadFalse();
                $("#anos").val("0");
                this.form.submit();
            });
        }
        else {
            ebs.SelectWhenChangeSubmit('#ate', true);
          // $('#ate').change(function () { DownloadFalse(); this.form.submit(); });
        }

        ebs.Set_DatePicker();
    }
    // Cliente ou Fornecedor

    var dropdown;
    if (setCliForFilter !== "") {
        if (setCliForFilter === "C") { dropdown = 'clientes'; } else { dropdown = 'fornecedores'; }
        ebs.PopDropDown(dropdown, [{ "tipo": setCliForFilter }]);
        ebs.SelectWhenChangeSubmit('#' + dropdown,true);
        //$('#'+dropdown).change(function () { DownloadFalse(); this.form.submit(); });
    }

    // Categorias
    if (setCategorias !== "") {
        dropdown = 'categorias';
        ebs.PopDropDown(dropdown, [{ "tipo": setCategorias }]);
        ebs.SelectWhenChangeSubmit('#' + dropdown, true);
        //('#' + dropdown).change(function () { DownloadFalse(); this.form.submit(); });
    }

    //Responsavel
    if (setReponsaveis) {
        ebs.PopDropDown("responsaveis", '');
        ebs.SelectWhenChangeSubmit('#responsaveis', true);
       //$('#responsaveis').change(function () { DownloadFalse(); this.form.submit(); });
    }
    // Meses
    if (setMesesFilter) {
        ebs.SelectWhenChangeSubmit('#meses',false);
       //$('#meses').change(function () {this.form.submit(); });
    }
    //Ano
    if (setAnoFilter) {
        $('#anos').change(function () {
            //DownloadFalse();
            if (setDeAteFilter) {
                ClearDeAte();
            }
            this.form.submit();
        });
    }
    //Situacao
    if (setSituacaoFilter) {
        ebs.SelectWhenChangeSubmit('#situacao', false);
       // $('#situacao').change(function () {  this.form.submit(); });
    }
////
}
function SetDataTable(table, scrollY, ordercol, hasCallBack) {
    var t;
    if (hasCallBack) {
        t = $(table).DataTable({
            "bInfo": false,
            "scrollY": scrollY,
            "scrollX": false,
            'order': [ordercol],
            "paging": false,
            "searching": false,
            "language": {  "emptyTable": "<div class='naocadastrado'><span>Nenhum item cadastrado</span></div>", "zeroRecords": "<div class='naocadastrado'><span>Nenhum item cadastrado</span></div>"},
            "drawCallback": function (settings) { UpdateTotal(); }
        });
        t.columns.adjust().draw();
    }
    else {
        t = $(table).DataTable({
            "info": false,
            "scrollY": scrollY,
            "scrollX": false,
            'order': [ordercol],
            "paging": false,
            "searching": false,
            "language": { "emptyTable": "<div class='naocadastrado'><span>Nenhum item cadastrado</span></div>", "zeroRecords": "<div class='naocadastrado'><span>Nenhum item cadastrado</span></div>" }
        });
        t.columns.adjust().draw();
    }
    $('#loading').hide();
}


function ResetPwdConfirm(id,nome) {

    bootbox.confirm({
        message: "Resetar senha do usuário " + nome + "?",
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
                ResetPwd(id);
            }

        }
    });
}
function ResetPwd(id) {
    $.ajax({
        cache: false,
        type: "POST",
        url: '/Usuarios/ResetPwd',
        data: {
            "id": id
        },
        success: function (data) {
            msg("Senha resetada com sucesso");
            window.setTimeout('location.reload()', 2000);

        },
        error: function (xhr, ajaxOptions, thrownError) {

            msg('Falha ao resetar senha');
        }
    });
}
function Download() {
    $("#download").val("true");

}
function DownloadFalse() {
    $("#download").val("false");
}


function ArquivarPagamento(id) {
    $.ajax({
        cache: false,
        type: "POST",
        url: '/ContaPrs/ArquivarPagamento',
        data: {
            "id": id
        },
        success: function (data) {
            /* Update clientList*/

            msg("Lançamento arquivado com sucesso");
            window.setTimeout('location.reload()', 2000);

        },
        error: function (xhr, ajaxOptions, thrownError) {

            msg('Falha ao confirmar pagamento');
        }
    });
}

function DisableField(nome) {
    $("#" + nome).prop("disabled", true);
}
function EnableField(nome) {
    $("#" + nome).prop("disabled", false);
}
/* Exclusao generica*/
function ExcluirConfirm(item, id, nome) {
    var msg = "Confirma exclusão de " + item + " " + nome;
    if (item === "bloquear") {
        msg === "Confirma bloqueio de " +  nome;
    }
    if (item === "desbloquear") {
        msg = "Confirma desbloqueio de " +  nome;
    }

    bootbox.confirm({
        message:msg,
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
                switch (item.toLowerCase()) {
                    case "reembolso":
                        Excluir("Reembolso",'/Reembolsoes/ExcluirReembolso',id);
                        break;
                    case "contaapagar":
                        Excluir("Conta a Pagar", '/ContaPrs/ExcluirPagamento', id);
                        break;
                    case "contaareceber":
                        Excluir("Conta a Receber", '/ContaPrs/ExcluirPagamento', id);
                        break;
                    case "categoria":
                        Excluir("Categoria", '/Categorias/ExcluirCategoria', id);
                        break;
                    case "cliente":
                        Excluir("Cliente", '/Clientes/ExcluirCliente', id);
                        break;
                    case "fornecedor":
                        Excluir("Fornecedor", '/Clientes/ExcluirFornecedor', id);
                        break;
                    case "usuario":
                        Excluir("Usuário", '/Usuarios/ExcluirUsuario', id);
                        break;
                    case "produto":
                        Excluir("Produto", '/Produto/ExcluirProduto', id);
                        break;
                    case "proposta":
                        Excluir("Proposta", '/Propostas/ExcluirProposta', id);
                        break;
                    case "bloquear":
                        Excluir("bloquear usuário", '/Usuarios/Bloquear', id);
                        break;
                    case "desbloquear":
                        Excluir("desbloquer usuário", '/Usuarios/Desbloquear', id);
                        break;
                    case "notafiscalareceber":
                        Excluir("Nota Fiscal", '/ContaPrs/CancelarNotaFiscalAreceber', id);
                        break;
                    case "tiporeembolso":
                        Excluir("Tipo Reembolso", '/TiposReembolso/ExcluirTipoReembolso', id);
                        break;
                    case "remuneracao":
                        Excluir("Remuneração", '/Remuneracao/ExcluirRemuneracao', id);
                        break;
                    case "comercial":
                        Excluir("Comercial", '/Comercial/ExcluirComercial', id);
                        break;
                    case "indice":
                        Excluir("Indice", '/Indices/ExcluirIndice', id);
                        break;
                    case "tipocontrato":
                        Excluir("Tipo Contrato", '/TiposContratos/ExcluirTipocontrato', id);
                        break;
                    case "contrato":

                        Excluir("contrato", '/Contratos/ExcluirContrato', id);
                        break;
                }
            }
        }
    });
}

function msgsubmit(item, msg) {
    bootbox.alert({
        message: msg,
        className: 'bb-alternate-modal',
        size: 'small',
        callback: function () {
            submitform(item);
       }
    });
}

function Excluir(item, url, id) {

    var msgsuccess = 'Sucesso ao excluir ' + item;
    var msgfail = 'Falha ao ao excluir ' + item;
    if (item === "bloquear usuário" || item === "desbloquer usuário") { msgsuccess = 'Sucesso ao  ' + item; msgfail = "Falha ao  " + item; }

    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: {
            "id": id
        },
        success: function (data) {
            msg(msgsuccess);
            window.setTimeout('location.reload()', 2000);
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msg(msgfail);
        }
    });
}


function GetUsuarios(dropdown) {
    var url = '/Usuarios/GetUsuarios';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        success: function (data) {
            var dd = $("#" + dropdown);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.id + '">' + option.nome + '</option>');
            });

            //ShowSaveButton();

        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}
// Refactor this
function UpdateCategorias(categoria,tipo, id) {
    var url = '/ContaPRs/PopulateCategorias';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: { "tipo": tipo ,
                "id": id },
        success: function (data) {
            var dd = $("#"+ categoria);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.categoriaid + '">' + option.nome + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msg("Erro ao atualizar categorias");
        }
    });
}



function PopulatePropostasCliente(situacaoid, clienteid,dropdown) {
    
    var url = '/ContaPRs/PopulatePropostasCliente';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: {
            "situacaoid": situacaoid,
            "clienteid": clienteid
        },
        success: function (data) {
          
            var dd = $("#propostaid");
            if (dropdown) {
                dd = $(dropdown);
            }
            dd.empty();
            dd.append('<option></option>');
            /* Check if has itens, iof not show message*/
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.propostaid + '">' + option.descricao + '</option>');
            });
            /* Se não tem prostas liberadas ou faturando esconde drop down */
            var options = $('select#propostaid option').length - 1;

            if (options > 0) {
                $("#propostaid").show();
                $("#semproposta").hide();
            } else {
                $("#propostaid").hide();
                $("#semproposta").show();
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msg("Erro ao atualizar propostas clientes");
        }
    });
}
// Refactor this
function PopulatePropostasClienteForContratos(situacaoid, clienteid) {
    var url = '/Contratos/PopulatePropostasCliente';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: {
            "situacaoid": situacaoid,
            "clienteid": clienteid
        },
        success: function (data) {
            var dd = $("#propostaid");
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.propostaid + '">' + option.descricao + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msg("Erro ao atualizar propostas clientes");
        }
    });
}

function GetContatosCliente(clienteid) {
    var url = '/Clientes/ShowContatos';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: {
            "clienteid": clienteid
        },
        success: function (data) {
            var dd = $("#propostaid");
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.propostaid + '">' + option.descricao + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msg("Erro ao atualizar categorias");
        }
    });
}


/*-------------------------------------------------------------------------
  Popula DropDow Perfis
  O perfil funciona diferente use a descrição ao invez de id
  --------------------------------------------------------------------------*/
// Refactor this
function PopulatePerfis(drodpdown, tipo) {
    var url = '/Usuarios/PopulatePerfis';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: { "tipo": tipo },
        success: function (data) {
            var dd = $("#" + drodpdown);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.name + '">' + option.name + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msg("Erro ao carregar Perfis");
        }
    });
}
// Refactor this
function PopulateTiposReembolso(selectelement) {
    var url = '/Reembolsoes/PopulateTiposReembolso';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,

        success: function (data) {
            var dd = $("#" + selectelement);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.tiporeembolsoid + '">' + option.nome + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msg("Erro ao atualizar Tipos de Reembolso");
        }
    });
}
// Refactor this
function PopulateProdutos(selectelement) {
    var url = '/Produtos/PopulateProdutos';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,

        success: function (data) {
            var dd = $("#" + selectelement);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.produtoid + '">' + option.nome + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msg("Erro ao atualizar lista de Produtos");
        }
    });
}

// Refactor this
function PopulateTiposContrato(element) {
    var url = '/TiposContratos/PopulateTiposContrato';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: {},
        success: function (data) {
            var dd = $("#" + element);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.id + '">' + option.nome + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {
            msg("Erro ao atualizar indices");
        }
    });
}
/*-------------------------------------------------------------------------
  Popula Drop PROPOSTAS NO CONTAS A RECEBER
  --------------------------------------------------------------------------*/
// Refactor this
function UpdatePropostas(clienteid,dropdown) {
    var url = '/ContaPrs/GetPropostasCliente';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: { "clienteid": clienteid },
        success: function (data) {
            var dd = $("#" + dropdown);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.cliforid + '">' + option.nome + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

/*-------------------------------------------------------------------------
  Popula Drop PROPOSTAS Renumeracao

--------------------------------------------------------------------------*/
function PopulatePropostasRemuneracao(dropdown) {
    var url = '/Remuneracao/PopulatePropostasRemuneracao';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: {},
        success: function (data) {

            var dd = $("#" + dropdown);
            dd.empty();
            dd.append('<option></option>');
            var maxl = 40;
          

            $.each(data, function (id, option) {
                 id = pad(option.propostaid, 3);
                //var valor = parseFloat(option.valor, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString();
                var valor = parseFloat(option.valor, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString();
                valor = valor.replace(".", "#");
                valor = valor.replace(",", ".");
                valor = valor.replace("#", ",");
                var text = ' Nº ' + (id + '&nbsp;&nbsp;&nbsp;&nbsp;Cliente: ' + option.cliente + '&nbsp;&nbsp;&nbsp;&nbsp;Descrição: ' + option.descricao + '&nbsp;&nbsp;&nbsp;&nbsp;Valor: R$ ' + valor);
                dd.append('<option value="' + option.propostaid + '">' + text + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}
function PopulatePropostasContrato(dropdown,clienteid) {
    var url = '/Contratos/PopulatePropostasCliente';

    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: { clienteid: clienteid},
        success: function (data) {

            var dd = $("#" + dropdown);
            dd.empty();
            dd.append('<option></option>');
            var maxl = 40;
            $.each(data, function (id, option) {
                id = pad(option.propostaid, 3);
                var valor = parseFloat(option.valor, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString();
                var text = ' Nº ' + (id + '&nbsp;&nbsp;&nbsp;&nbsp;Descrição: ' + option.descricao + '&nbsp;&nbsp;&nbsp;&nbsp;Categoria: ' + option.categoria + '&nbsp;&nbsp;&nbsp;&nbsp;Valor: R$ ' + valor);
                dd.append('<option value="' + option.propostaid + '">' + text + '</option>');
            });
          
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}


function FormatSelect() {
    var spacesToAdd = 5;
    var biggestLength = 0;
    $("#proposta option").each(function () {
        var len = $(this).text().length;
        if (len > biggestLength) {
            biggestLength = len;
        }
    });
    var padLength = biggestLength + spacesToAdd;
    $("#proposta option").each(function () {
        var parts = $(this).text().split(';');

        var strLength = parts[0].length;
        for (var x = 0; x < padLength - strLength; x++) {
            parts[0] = parts[0] + ' ';
        }
        $(this).text(parts[0].replace(/ /g, '\u00a0') + '+' + parts[1]).text;
    });
}
function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad("0" + str, max) : str;
}


function PopulateComercialRemuneracao(dropdown) {
    var url = '/Remuneracao/PopulateComercialRemuneracao';
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: { },
        success: function (data) {

            var dd = $("#" + dropdown);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.id + '">' + option.nome + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

/*-------------------------------------------------------------------------
  Popula Drop down
--------------------------------------------------------------------------*/
// Refactor this
function PopulateDropDownCliFor(tipo, targetdropdownid,controleraction) {
    var url = controleraction;
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: { "tipo": tipo },
        success: function (data) {

            var dd = $("#" + targetdropdownid);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.cliforid + '">' + option.nome + '</option>');
            });

            //ShowSaveButton();

        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}
// Refactor this
function PopulateDropDownResponsaveis(targetdropdownid, controleraction) {
    var url = controleraction;
    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        success: function (data) {
            var dd = $("#" + targetdropdownid);
            dd.empty();
            dd.append('<option></option>');
            $.each(data, function (id, option) {
                dd.append('<option value="' + option.Id + '">' + option.Nome + '</option>');
            });
        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}
// Refactor this

function RemoveFiltersCr() {
    $("#de").val("");
    $("#ate").val("");
    
    $("#categorias").val("");
    $("#fornecedores").val("");
    $("#responsaveis").val("");
    SetDropDownValue("0", "anos");
    SetDropDownValue("1", "situacao");
    submitform('ApagarAnalitico');

}
function RemoveFilters() {
    $("#de").val("");
    $("#ate").val("");
    SetDropDownValue("0", "anos");
    $("#categorias").val("");
    $("#fornecedores").val("");
    SetDropDownValue("0", "situacao");
    submitform('ApagarAnalitico');
}

function SetDropDownValue(value, dropdown, tout) {
    
    setTimeout(function () {
        index = 0;
        $("#" + dropdown + " option").each(function () {
            if ($(this).val().trim() === value) {
                $(this).attr('selected', 'selected');
            }
        });
    }, 1500);
  
}
function SetSelectIndex(viebagvalue, selectid) {
    var id = viebagvalue; if (id !== "") { SetDropDownValue(id, selectid); }
}


function ShowContasProposta(propostaid) {
    /* Mostra lancamentos na mesma TR que contatos*/
    var placeholder = "#contas_" + propostaid;
    $(placeholder).empty();
    $(placeholder).load("/Propostas/ShowContasProposta", {
        propostaid: propostaid,
        viewName: "_Lancamentos"
    });
}

/*-------------------------------------------------------------------------
  Formatadores numericos
--------------------------------------------------------------------------*/


Fmt.prototype.ToDecimal = (value) => {
    try {
        return Number(parseFloat(value)).toFixed(2).toString();
    }
    catch(e) {
        return value.toString();
    }

}

Fmt.prototype.StrToDecimal = (value) => {
    try {
        parseFloat((value).replace(".", "").replace(",", ".").toFixed(2));
    }
    catch (e) {
        return value.toString();
    }

}



Fmt.prototype.ToMoney = (value) => {
    try {
        return parseFloat(value).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }).toString().replace("R$", "").trim();
    }
    catch(e) {
        return value.toString();
    }

}

var Nfmt = new Fmt(); 

/*-------------------------------------------------------------------------
  Imprime pagina
--------------------------------------------------------------------------*/


function Imprime() {
    var dt = new Date();
    var month = dt.getMonth() + 1;
    var day = dt.getDate();
    var data = (day < 10 ? '0' : '') + day + '/' +
                (month < 10 ? '0' : '') + month + '/' +
                 dt.getFullYear();
    var time = dt.getHours() + ":" + dt.getMinutes();
  
    $("#footer").show();
    $("#footertext").text("Impresso em : " + data + " - " + time);
    javascript: window.print();
}



/*-------------------------------------------------------------------------
  Submit um formalario
--------------------------------------------------------------------------*/
function submitform(formid) {
   
    //$("#" + formid).submit();
    switch (formid) {
        case "NovoCliente":
        case "AlterarFornecedor":
        case "AlterarCliente":
            if ($("#cidade").val() !== null && $("#cidade").val() !== '') {
                $("#" + formid).submit();
            }
            else{
                msg("Campo cidade não pode estar em branco");
            }
            break;
        case "NovaContaAreceber":
        case "NovaContaApagar":
        case "NovaParcelaAreceberr":
            //EnableField("cliforid");
            //EnableField("categoriaid");
            //EnableField("noordem");

            var n = $("#noordem").val();
            var o = n.split("/");
            var parcela = parseInt(o[0]);
            var qtparcelas = parseInt(o[1]);

          
            if (parcela === 0) {
                msg("Número da parcela não pode ser 0");
                return false;
            }
            else {
                if (qtparcelas===0){
                    msg("Quantidade de parcelas não pode ser 0");
                    return false;
                }
                else {
                    if (parcela > qtparcelas) {
                        msg("Quantidade de parcelas menor que numero da parcela");
                        return false;
                    }   
                }
            }
            $("#" + formid).submit();
            break;

        default:
            $("#" + formid).submit();

    }

  
}

function msg2(msg) {
    bootbox.alert({

        message: msg,
        className: 'bb-alternate-modal',
        size: 'small'

    });
}
function msg(msg) {
    var dialog = bootbox.dialog({
        message: msg,
        closeButton: false
    });
    setTimeout(function () {

        dialog.modal('hide');

    }, 2000);
   
}


function btd(text, ta,wd) {
    var stl= 'border:solid thin lightgray;';

    if (ta === 'r') {
        stl += "text-align:right;padding-right:5px;";
    }
    if (ta === 'c') {
        stl += "text-align:center;";
    }
    if (typeof wd !== "undefined") {

        stl += "width:" + wd + ";";
    }
    if (typeof ta === "undefined") {

        stl += "padding-left:5px;";
    }
    return "<td style='" + stl + "'>" + text + "</td>";
}
function titulo(text) {
    return "<div>" + text + "</div>" +
    "<div style='height:10px;'></div>";
}
function InvertComa(valor) {
    var len = valor.length;
    var start = valor.substr(0, len - 3).replace(',', '.').toString();
    var end = valor.substr(start.length, 3).replace('.', ',').toString();
   
    return start.concat(end);
}

function PrintTable(tabela, title, mes, ano) {
   var finalHtml = titulo(title);
    if (typeof mes !== "undefined") {
        finalHtml = titulo(title + " (" + mes+"/"+ ano + ")");
    }
    finalHtml += "<table style='width:100%;border-collapse:collapse;font-size:10px;'>";

    if (tabela === "#categorias") {
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('th'),
             c1 = $tds.eq(1).text(),
             c2 = $tds.eq(2).text();
            if (c1 !== "") {
                finalHtml += "<tr style='background-color:gray;'>" + btd(c1) + btd(c2, 'c') + "</tr>";
            }
        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),
                c1 = $tds.eq(1).text(),
                c2 = $tds.eq(2).text();
            if (c1 === "") {
                finalHtml += "<tr>" + btd(c1) + btd(c2, 'c') + "</tr>";
            }
        });
    }
    if (tabela === "#clientes") {
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('th'),
             c1 = $tds.eq(1).text(),
             c2 = $tds.eq(2).text(),
             c3 = $tds.eq(4).text(),
             c4 = $tds.eq(5).text(),
             c5 = $tds.eq(6).text();
            if (c1 !== "") {
                finalHtml += "<tr style='background-color:gray;'>" + btd(c1) + btd(c2) + btd(c3) + btd(c4) + btd(c5) + "</tr>";
            }
        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),
                c1 = $tds.eq(1).text(),
                c2 = $tds.eq(2).text(),
                c3 = $tds.eq(4).text(),
                c4 = $tds.eq(5).text(),
                c5 = $tds.eq(6).text();
            if (c2 !== "") {
                finalHtml += "<tr>" + btd(c1) + btd(c2) + btd(c3) + btd(c4) + btd(c5) + "</tr>";
            }
        });
    }
    if (tabela === "#comercial") {
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('th'),
             c1 = $tds.eq(1).text(),
             c2 = $tds.eq(2).text(),
             c3 = $tds.eq(3).text();
            if (c1 !== "") {
                finalHtml += "<tr style='background-color:gray;'>" + btd(c1) + btd(c2) + btd(c3) + "</tr>";
            }
        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),
                c1 = $tds.eq(1).text(),
                c2 = $tds.eq(2).text(),
                c3 = $tds.eq(3).text();
            if (c1 !== "") {
                finalHtml += "<tr>" + btd(c1) + btd(c2) + btd(c3, 'r') + "</tr>";
            }
        });
    }
    if (tabela === "#contaprs") {
        var total = 0.00;
        $(tabela).find('tr').each(function (i) {
           var $tds = $(this).find('th'),
                 c1 = $tds.eq(2).text(),
                 c2 = $tds.eq(3).text(),
                 c3 = $tds.eq(4).text(),
                 c4 = $tds.eq(5).text(),
                 c5 = $tds.eq(6).text(),
                 c6 = $tds.eq(7).text(),
                 c7 = $tds.eq(8).text();
                 c8 = $tds.eq(9).text();
                
            if (c1 !== "") {
                finalHtml += "<tr style='background-color:lightgray;'>" + btd(c1) + btd(c2) + btd(c3) + btd(c4) + btd(c5) + btd(c6, 'c', '6%') + btd(c7, 'c')  + btd(c8)+ "</tr>";
            }

        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),
                c1 = $tds.eq(2).text(),
                c2 = $tds.eq(3).text(),
                c3 = $tds.eq(4).text();
                c4 = $tds.eq(5).text();
                c5 = $tds.eq(6).text();
                c6 = $tds.eq(7).text();
                c7 = $tds.eq(8).text();
                c8 = $tds.eq(9).text();

            if (c4 !== "") {
                var valor =  parseFloat(c4.replace('.', '').replace(',', '').replace('R$', '').replace(' ', ''));
                total += valor/100;
            }
           
            if (c1 !== "") {
                finalHtml += "<tr>" + btd(c1) + btd(c2) + btd(c3) + btd(c4, 'r') + btd(c5, 'c') + btd(c6, 'c') + btd(c7, 'c') + btd(c8)+ "</tr>";
            }
        });
        finalHtml += "<tr>" + btd('') + btd('') + btd('Total', 'r') + btd("R$ " + InvertComa(total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,')), 'r', '12%') + btd('') + btd('') + btd('') + btd('')+ "</tr>";
        
        

    }
    if (tabela === "#contratos") {
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('th'),
            c1 = $tds.eq(1).text(),
            c2 = $tds.eq(2).text(),
            c3 = $tds.eq(3).text();
            c4 = $tds.eq(4).text();
            c5 = $tds.eq(5).text();
            c6 = $tds.eq(6).text();
            c7 = $tds.eq(7).text();
            c8 = $tds.eq(8).text();
            c9 = $tds.eq(9).text();

            if (c1 !== "") {
                finalHtml += "<tr style='background-color:gray;'>" + btd(c1) + btd(c2) + btd(c3) + btd(c4) + btd(c5) + btd(c6) + btd(c7) + btd(c8) + btd(c9) + "</tr>";
            }
        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),
            c1 = $tds.eq(1).text(),
            c2 = $tds.eq(2).text(),
            c3 = $tds.eq(3).text();
            c4 = $tds.eq(4).text();
            c5 = $tds.eq(5).text();
            c6 = $tds.eq(6).text();
            c7 = $tds.eq(7).text();
            c8 = $tds.eq(8).text();
            c9 = $tds.eq(9).text();

            if (c1 !== "") {
                finalHtml += "<tr>" + btd(c1) + btd(c2) + btd(c3) + btd(c4) + btd(c5) + btd(c6) + btd(c7) + btd(c8) + btd(c9) + "</tr>";
            }
        });
    }
    if (tabela === "#perfis" || tabela==="#tiposreembolso") {
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('th'),
             c1 = $tds.eq(1).text();
            if (c1 !== "") {
                finalHtml += "<tr style='background-color:gray;'>" + btd(c1) + "</tr>";
            }
        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),
                c1 = $tds.eq(1).text();
            if (c1 !== "") {
                finalHtml += "<tr>" + btd(c1) + "</tr>";
            }
        });
    }
    if (tabela === "#produtos") {
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('th'),
             c1 = $tds.eq(1).text(),
             c2 = $tds.eq(2).text();
            if (c1 !== "") {
                finalHtml += "<tr style='background-color:gray;'>" + btd(c1) + btd(c2) + "</tr>";
            }
        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),
                c1 = $tds.eq(1).text(),
                c2 = $tds.eq(2).text();
            if (c1 !== "") {
                finalHtml += "<tr>" + btd(c1) + btd(c2, 'r') + "</tr>";
            }
        });
    }
    if (tabela === "#propostas") {
        total = 0.00; var faturado = 0.00;
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('th'),
            c1 = $tds.eq(1).text(),
            c2 = $tds.eq(2).text(),
            c3 = $tds.eq(3).text();
            c4 = $tds.eq(4).text();
            c5 = $tds.eq(5).text();
            c6 = $tds.eq(6).text();
            c7 = $tds.eq(7).text();
            c8 = $tds.eq(8).text();
            if (c1 !== "") {
                finalHtml += "<tr style='background-color:gray;'>" + btd(c1) + btd(c2) + btd(c3) + btd(c4) + btd(c5, '', '15%') + btd(c6) + btd(c7) + "</tr>";
            }
        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),c1 = $tds.eq(1).text(),c2 = $tds.eq(2).text(),c3 = $tds.eq(3).text();
            c4 = $tds.eq(4).text();
            c5 = $tds.eq(5).text();
            c6 = $tds.eq(6).text();
            c7 = $tds.eq(7).text();
            c8 = $tds.eq(8).text();
            if (c5 !== "") {
                var valor = parseFloat(c5.replace('.', '').replace(',', '').replace('R$', '').replace(' ', ''));
                total += valor / 100;
            }
            if (c6!== "") {
                var fvalor = parseFloat(c6.replace('.', '').replace(',', '').replace('R$', '').replace(' ', ''));
                faturado += fvalor / 100;
            }

            if (c1 !== "") {
                finalHtml += "<tr>" + btd(c1) + btd(c2) + btd(c3) + btd(c4) + btd(c5, 'r') + btd(c6, 'r') + btd(c7, 'c') + "</tr>";
            }
        });

        finalHtml += "<tr>" + btd('') + btd('') + btd('') + btd('Total', 'r') + btd("R$ " + InvertComa(total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,')), 'r', '12%', 'r') + btd("R$ " + InvertComa(faturado.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,')), 'r', '12%') + btd('', 'c') + "</tr>";
    }
    if (tabela === "#remuneracao") {
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('th'),
            c1 = $tds.eq(1).text(),
            c2 = $tds.eq(2).text(),
            c3 = $tds.eq(3).text();
            c4 = $tds.eq(4).text();
            c5 = $tds.eq(5).text();
            c6 = $tds.eq(6).text();
            c7 = $tds.eq(7).text();
            c8 = $tds.eq(8).text();
            c9 = $tds.eq(9).text();
            c10 = $tds.eq(10).text();

         
            if (c1 !== "") {
                finalHtml += "<tr style='background-color:gray;'>" + btd(c1, '', '5%') + btd(c2, '', '14%') + btd(c3, '', '14%') + btd(c4, '', '14%') + btd(c5, '', '8%') + btd(c6, '', '8%') + btd(c7, '', '8%') + btd(c8) + btd(c9) + btd(c10, 'c') + "</tr>";
            }
        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),
            c1 = $tds.eq(1).text(),
            c2 = $tds.eq(2).text(),
            c3 = $tds.eq(3).text();
            c4 = $tds.eq(4).text();
            c5 = $tds.eq(5).text();
            c6 = $tds.eq(6).text();
            c7 = $tds.eq(7).text();
            c8 = $tds.eq(8).text();
            c9 = $tds.eq(9).text();
            c10 = $tds.eq(10).text();

            if (c1 !== "") {
                finalHtml += "<tr>" + btd(c1) + btd(c2) + btd(c3) + btd(c4) + btd(c5, 'r') + btd(c6, 'r') + btd(c7, 'r') + btd(c8, 'r') + btd(c9, 'c') + btd(c10, 'c') + "</tr>";
               
            }
        });
    }
    if (tabela === "#usuarios") {
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('th'),
             c1 = $tds.eq(1).text(),
             c2 = $tds.eq(2).text(),
             c3 = $tds.eq(3).text();
            if (c1 !== "") {
                finalHtml += "<tr style='background-color:gray;'>" + btd(c1) + btd(c2) + btd(c3, 'c') + "</tr>";
            }
        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),
                c1 = $tds.eq(1).text(),
                c2 = $tds.eq(2).text(),
                c3 = $tds.eq(3).text();
            if (c1 !== "") {
                finalHtml += "<tr>" + btd(c1) + btd(c2) + btd(c3, 'c') + "</tr>";
            }
        });
    }
    if (tabela === "#tablereembolso") {
        total = 0.00;
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('th'),
                c1 = $tds.eq(3).text(),
                c2 = $tds.eq(4).text(),
                c3 = $tds.eq(5).text(),
                c4 = $tds.eq(6).text(),
                c5 = $tds.eq(7).text();
                c6 = $tds.eq(8).text();
            if (c1 !== "") {
                finalHtml += "<tr style='background-color:gray;'>" + btd(c1) + btd(c2) + btd(c3) + btd(c4) + btd(c5)+ btd(c6) + "</tr>";
            }
        });
        $(tabela).find('tr').each(function (i) {
            var $tds = $(this).find('td'),
                c1 = $tds.eq(3).text(),
                c2 = $tds.eq(4).text(),
                c3 = $tds.eq(5).text(),
                c4 = $tds.eq(6).text(),
                c5 = $tds.eq(7).text();
                c6 = $tds.eq(8).text();
            if (c2 !== "") {
                finalHtml += "<tr>" + btd(c1) + btd(c2) + btd(c3) + btd(c4) + btd(c5,'r') + btd(c6,'c') + "</tr>";
            }
            if (c5 !== "") {
                var valor = parseFloat(c5.replace('.', '').replace(',', '').replace('R$', '').replace(' ', ''));
                total += valor / 100;
            }
        });
        finalHtml += "<tr>" + btd('') + btd('') + btd('')  + btd('Total', 'r') + btd("R$ " + InvertComa(total.toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,')), 'r', '12%', 'r') + btd('', 'c') + "</tr>";
    }


  
    finalHtml += "</table>";
    newWin = window.open("");
  
    newWin.document.write(finalHtml);
    newWin.print();
    newWin.close();
}


