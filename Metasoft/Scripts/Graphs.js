function drawpie(url, tipo, title, wid, hei, ele, is3d, mes, ano) {


    $.ajax({
        cache: false,
        type: "POST",
        url: url,
        data: { 
            "tipo": tipo,
            "mes": mes,
            "ano": ano
        },
        success: function (data) {

            // Create the data table.
            var dt = new google.visualization.DataTable();
            dt.addColumn('string', 'Topping');
            dt.addColumn('number', 'Slices');

            $.each(data, function (id, option) {
                dt.addRows([["'" + option.x + "'", option.y]]);
            });

            // Set chart options
            var options = {
                'title': title,
                'width': wid,
                'height': hei,
                'is3D': is3d,
                legendTextStyle: { fontName: 'Lucida Sans', fontSize: 10 },
                pieSliceText: 'none',
                titleTextStyle: { fontName:'Lucida Sans', color: '#0d0726', fontSize: 14, bold: false, italic: true },
                tootip: { ignoreBounds :true}
  
            };

            // Instantiate and draw our chart, passing in some options.
            var chart = new google.visualization.PieChart(document.getElementById(ele));
            chart.draw(dt, options);

        },
        error: function (xhr, ajaxOptions, thrownError) {

        }
    });
}

function drawbar(res,title, wid, hei, ele) {
    // Create the data table.
    var dt = google.visualization.arrayToDataTable([
          ['Mês', 'Receber', 'Pagar'],
          ['Jan', res.rjan,res.pjan],
          ['Fev', res.rfev, res.pfev],
          ['Mar', res.rmar, res.pmar],
          ['Abr', res.rabr, res.pabr],
          ['Mai', res.rmai, res.pmai],
          ['Jun', res.rjun, res.pjun],
          ['Jul', res.rjul, res.pjul],
          ['Ago', res.rago, res.pago],
          ['Set', res.rset, res.pset],
          ['Out', res.rout, res.pout],
          ['Nov', res.rnov, res.pnov],
          ['Dez', res.rdez, res.pdez]
    ]);

    // Set chart options
    var options = {
        'title': title,
        'width': wid,
        'height': hei,
        legend: { alignment: 'start' },
        pieSliceText: 'none',
        titleTextStyle: { fontName: 'Lucida Sans', color: '#0d0726', fontSize: 14, bold: false, italic: true }

    };

    // Instantiate and draw our chart, passing in some options.
    var chart = new google.visualization.BarChart(document.getElementById(ele));
    chart.draw(dt, options);
  
}