
$(function () {
    $.ajax({
        type: "GET",
        url: "/Dashboard/GetUploadHistory?id=1",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });

    $('#ReportFiletype').change(function () {

        var filetypeCode = $('#ReportFiletype').val()

        $.ajax({
            type: "GET",
            url: "/Dashboard/GetUploadHistory?id=" + filetypeCode,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        })
    })
})
function OnSuccess(response) {
    //var records = JSON.parse(response);
    console.log(response);
    
    var totalRow = [];
    var uploadDate = new Array();
    for (records of response) {
        totalRow.push(parseInt(records.numberOfRows));
        uploadDate.push(records.date);
    }
    var options = {
        series: [{
            name:"Total Uploded rows",
            data: totalRow
        }],
        chart: {
            type: 'bar',
            height: 350,
           
        },
        plotOptions: {
            bar: {
                borderRadius: 4,
                horizontal: false,
                
            }
        },
        dataLabels: {
            enabled: false
        },
        xaxis: {
            type: 'datetime',
            categories: uploadDate,
            labels: {
                datetimeFormatter: {
                    year: 'yyyy',
                    month: 'MMM \'yy',
                    day: 'dd MMM',
                    hour: 'HH:mm'
                }
            },
            title: {
                text: 'Uploaded Date'
            }
        },
        yaxis: {
            title: {
                text: 'Number of Uploaded Rows'
            },
            labels: {
                formatter: (value) => { return value.toFixed(0) },
            },
        },
        noData: {
            text: 'No Records...'
        },
        tooltip: {
            x: {
                format: 'dd/MM/yy HH:mm'
            },

        }
    };
    $('#chart').empty();
    var chart = new ApexCharts(document.querySelector("#chart"), options);
    chart.render();

}