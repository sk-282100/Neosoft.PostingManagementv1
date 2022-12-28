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
        });
    });
});
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
            name: "Total Uploded rows",
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

function ShowWorkFlow() {

    $.ajax({
        type: "GET",
        url: "/Dashboard/GetWorkFlowStatus",
        contentType: "application/json ; charset = utf-8",
        dataType: "json",
        success: function (response) {
            if (response.data != null) {
                var responseData = response.data;
                if (responseData.interZonalPromotionStatus == "true") {
                    AnimateProgressBar(1,4);
                }
                else if (responseData.interZonalRequestStatus == "true") {
                    AnimateProgressBar(1, 3);
                }
                else if (responseData.vacancyListStatus == "true") {
                    AnimateProgressBar(1, 2);
                }
                else if (responseData.employeeTransferListStatus == "true") {
                    AnimateProgressBar(1, 1);
                }
                console.log(response);
            }
        },
        failure: function () {

        },
        error: function () {

        }
    });
    $('#demo').show();
    $('#itemModal3').modal('show');
};
function AnimateProgressBar(i , count) {
    $("#steps" + i + "").empty();
    $("#steps" + i + "").append('<span><i class="bi bi-check-lg"></i><span>');
    $("#steps" + i + "").addClass("active");
    i++;
    if (i > 1 && i<=count) {
        $("#line" + (i - 1)+" div").animate({ width: "100%" }, {
            duration: 700,
            easing: "linear",
            complete: function () {
                if (i <= count) {
                    AnimateProgressBar(i, count);
                }
            }
        });
    }
}
function HideWorkFlow() {
    $('#demo').hide();
    $('#itemModal3').modal('hide');
};