var bodyh = "230px;"
var bodyhmax = '540px'
var real = { style: "currency", currency: "BRL", minimumFractionDigits: 2, maximumFractionDigits: 2 }

$(document).ready(function () {
    t = $("#nota").DataTable({
        "fixedHeader": { header: true},
        "info": false,
        "scrollY": '200px',
        "scrollX": false,
        'order': 1,
        "paging": false,
        "searching": false
    });
    ebs.SetValorMask('.unit');
    $('.qtd').mask("###0", { reverse: true });
    $('.unit').mask("#.##0,00", { reverse: true });
    $('.tot').mask("#.##0,00");
    UpdateTotal();

});




function NotaFiscal() { }

NotaFiscal.prototype.ImprimeNotaFiscal = (url) => {
    
    document.querySelector('.dataTables_scrollBody').style.height = '640px';
    document.querySelector('.dataTables_scrollBody').style.scrollY = '640px';

    window.print();
    document.location.href = url;
};
NotaFiscal.prototype.ToggleVisibility = (id) => {
    if ($("#" + id).css('opacity') === '1') {

        $("#" + id).css('opacity', "0.3");
        document.querySelector('.dataTables_scrollBody').style.height = bodyhmax
        $("#nfHeader").hide();
    }
    else {
        $("#" + id).css('opacity', '1');
        document.querySelector('.dataTables_scrollBody').style.height = '220px';
        $("#nfHeader").show();
    }
};
NotaFiscal.prototype.HideHeader = () => { $("#nfHeader").hide(1500); };
NotaFiscal.prototype.ShowHeader = () => { $("#nfHeader").show(1500); };
NotaFiscal.prototype.UpdateItemQtd =(nfid, line, qtd) =>{
    $.post("/ContaPrs/NotaFiscalUpdateItemQtd", { nfid: nfid, line: line, qtd: qtd }, function (data) { });
    UpdateItemTotal(nfid, line);
}
NotaFiscal.prototype.UpdateItemDescricao = (nfid, line, descricao) => {
    $.post("/ContaPrs/NotaFiscalUpdateItemDescricao", { nfid: nfid, line: line, descricao: descricao }, function (data) { });
}
NotaFiscal.prototype.UpdateItemUnitario = (nfid, line, unitario) => {
    $.post("/ContaPrs/NotaFiscalUpdateItemUnitario", { nfid: nfid, line: line, unitario: unitario }, function (data) { });
    UpdateItemTotal(nfid, line);
}
NotaFiscal.prototype.GetNumeroNotaFiscal = (nfid) => {
    $.post("/ContaPrs/NotaFiscalGeraNumero", { nfid: nfid }, function (data) {
        return data;
    });
}
NotaFiscal.prototype.GeraNumeroNotaFiscal = (nfid) => {
    $.post("/ContaPrs/NotaFiscalGeraNumero", { nfid: nfid }, function (data) {
        if (data > 0) {
           var numerocontainer = document.getElementById("NumeroNotaContainerContent");
           numerocontainer.classList.remove('bgr');
           numerocontainer.classList.add('nfnumgb');
           NotaNumero = data;
           document.getElementById("NumeroNf").innerText = data;
           document.getElementById("NumeroNfContainer").classList.remove("hidden");
           document.getElementById("GerarNumeroNota").classList.add("hidden")
           AtualizaNotaFiscalUi();

        }
    });

}
NotaFiscal.prototype.AtualizaNumeroNotaFiscalUI = (data) => {
    var numerocontainer = document.getElementById("NumeroNotaContainerContent");
    numerocontainer.innerHTML = " Nº Nota Fiscal: " + data + " ";
    numerocontainer.classList.remove('bgr');
    numerocontainer.classList.add('nfnumgb');
}


UpdateItemTotal = (nfid, line) => {


    var target = "tti_" + line;
    var q = document.getElementById("qtd_" + line).value;
    var u = (document.getElementById("uni_" + line).value).replace("R$", "");

    try {
        var qtd = parseFloat(q);
        var unt = parseFloat(u.replace(".", "").replace(",","."));     //convert para format americano
        if (qtd > 0)
            document.getElementById(target).value = (isNaN(qtd * unt)) ? '' : Nfmt.ToMoney(qtd * unt);

        UpdateTotal()
      
    }
    catch(e){
        console.log('erro');
        return false;
    }
    
}
UpdateTotal = () => {
    var totalnota = 0;
    var qe = document.querySelectorAll("#notabody > tr > td:nth-child(2) > input");
    var ue = document.querySelectorAll("#notabody > tr > td:nth-child(4) > input");

    for (i = 0; i < qe.length; ++i) {

        var iqe = Number(qe[i].value);
        var um = ue[i].value.replace(".", "").replace(".", "").replace("R$ ", "");
        um = um.replace(",", ".");

        var iue = parseFloat(um).toFixed(2)
        if (!isNaN(iqe) && !isNaN(iue)) {
            if (iqe > 0 && iue > 0) {
                totalnota += parseFloat((iqe * iue));
            }
        }
    }

    var td = document.getElementById("tgeral")
    var t = `R$ ${Nfmt.ToMoney(totalnota).toString()}`;
    td.innerText = t;
    document.getElementById("tgeraldiv").innerText = t;
    TotalNota = totalnota >0;
    RefreshAvailableActions();

}

Msg = (text, color) => {
    var msgdiv = document.getElementById("msgdiv")
    msgdiv.classList.remove("msganim");
    
    var msg = document.getElementById("msg")
    if (text) {
        msg.innerText = text;
        if (color) {

            msg.style.color = color;
        }
        else {
            msg.style.color = "red";
        }
       
    }
    else {
        msg.innerText = "";
        msgdiv.style.backgroundColor = "tranparent";
    }
    msgdiv.classList.add("msganim");
}
RefreshAvailableActions = () => {
    Msg();
   
    if (EnderecoCompleto) {
        AtualizaNotaFiscalUi();
    }
    else {
        Msg("Endereco esta Incompleto, campos ausentes : " + Missingfields )
        EscondeGerarNumeroNota();
        EscondeBotaoImprimir();
        EscondeNumeroNota();
    }

}
PodeGerarNumero = () => { return parseInt(document.getElementById("NumeroNf").innerText) === 0 && TotalNota > 0;}
PodeImprimir = () => {
    if (!PodeGerarNumero() && TotalNota > 0) {
        return true;
    }
    return false;
}

EscondeBotaoImprimir = () => {
    var e = document.getElementById("BotaoImprimir");
    e.classList.add('noshow');
}
MostraBotaoImprimir = () => {
    var e = document.getElementById("BotaoImprimir");
     e.classList.remove('noshow');
   
}
MostraGerarNumeroNota = () => {document.getElementById("GerarNumeroNota").classList.remove('hidden');}
EscondeGerarNumeroNota = () => {document.getElementById("GerarNumeroNota").classList.add('hidden');}
MostrarNumeroNota = (forPrint) => {
  
    var ele = document.getElementById("NumeroNfContainer")
    
    ele.classList.remove("hidden");
    if (forPrint) {
        document.getElementById("NumeroNf").style.color = "black"
        ele.classList.remove("nfnumgb")
        ele.classList.add("nfNumForPrint");
    }
    else {
        document.getElementById("NumeroNf").style.color = "white"
        ele.classList.add("nfnumgb")
        ele.classList.remove("nfNumForPrint");
    }
}
MostrarEmissao = (color) => {
    var em = document.getElementById("emissao");

    em.innerText = " DATA DE EMISSÃO : " + Hoje();
    em.style.color = color;
}
EscondeNumeroNota = () => {document.getElementById("NumeroNfContainer").classList.add('hidden');}
AtualizaNotaFiscalUi = () => {
    if (PodeGerarNumero() && !PodeImprimir()) {
        console.log("S N");
        EscondeNumeroNota();
        MostraGerarNumeroNota();
        EscondeBotaoImprimir();
        
        Msg("Gerar número da Nota Fiscal para habilitar impressao")
    }
    if (PodeGerarNumero() && PodeImprimir()) {
        console.log("S S");
        EscondeNumeroNota();
        MostraGerarNumeroNota();
        EscondeBotaoImprimir();
    }
    if (!PodeGerarNumero() && !PodeImprimir()) {
        console.log("N N");
        Msg("Preencher detalhes da nota para gerar Número de Nota Fiscal")
        EscondeNumeroNota();
        EscondeGerarNumeroNota();
        EscondeBotaoImprimir();
    }
    if (!PodeGerarNumero() && PodeImprimir()) {

        Msg("Nota fiscal pronta para impressão","green")
        MostrarNumeroNota();
        EscondeGerarNumeroNota();
        MostraBotaoImprimir();
    }
}
Hoje = () => {
    var d = new Date();
    let dia = d.getDate();
    let mes = d.getMonth() + 1;
    let ano = d.getFullYear();

    if (dia < 10) { dia = "0" + dia.toString() }
    if (mes < 10) { mes = "0" + mes.toString() }
    console.log(mes)
    var strDate = dia + "/" + mes + "/" + ano;

    return strDate;
}






var nfh = new NotaFiscal();      //nota fiscal helpers