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

    $('#resetWorkflowBtn').click(function () {
        Swal.fire({
            title: 'Are you sure you want to Reset the WorkFlow?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    url: "/Dashboard/ResetWorkflow",
                    success: function (response) {
                        if (response.succeeded == true && response.data == true) {
                            HideWorkFlow();
                            $.notify("workflow has been Reset successfully", "success");
                            window.setTimeout(() => { location.reload(true); }, 3000);
                        }
                    },
                    failure: function () {
                        $.notify("opration has been failed, try again later ", "error");
                    },
                    error: function () {
                        $.notify("Something is wrong contect to Dev Team", "error");
                    }
                });
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
            $('#demo').show();
            $('#itemModal3').modal('show');
            if (response.data != null) {
                var responseData = response.data;
                
                if (responseData.workFlowstatus == "ZO") {
                    $('#workflowStatus').html("Completed");
                    $('#workflowStatus').addClass('text-success bg-success border-success');
                }
                else {
                    $('#workflowStatus').html("Pending");
                    $('#workflowStatus').addClass('text-warning bg-warning border-warning');
                }
                var status = [responseData.employeeMasterListStatus, responseData.vacancyListStatus, responseData.interRequestStatus,"true","true"]
                if (responseData.workFlowstatus == "ZO") {
                    AnimateProgressBar(1, 5, status);
                    $('#resetWorkflowBtn').prop("disabled", true);
                }
                else if (responseData.generateEmployeeTransfer == "true") {
                    AnimateProgressBar(1, 4, status);
                    $('#resetWorkflowBtn').prop("disabled", false);
                }
                else if (responseData.interRequestStatus == "true") {
                    AnimateProgressBar(1, 3, status);
                    $('#resetWorkflowBtn').prop("disabled", false);
                }
                else if (responseData.vacancyListStatus == "true") {
                    AnimateProgressBar(1, 2, status);
                    $('#resetWorkflowBtn').prop("disabled", false);
                }
                else if (responseData.employeeMasterListStatus == "true") {
                    AnimateProgressBar(1, 1, status);
                    $('#resetWorkflowBtn').prop("disabled", false);
                }
                else {
                    $('#resetWorkflowBtn').prop("disabled", true);
                }
               
            }
        },
        failure: function () {
            $.notify("opration has been failed, try again later ", "error");
        },
        error: function () {
            $.notify("Something is wrong contect to Dev Team", "error");
        }
    });
   
};
function AnimateProgressBar(i, count,status) {
    
    $("#steps" + i + "").empty();
    if (status[i - 1] == "true") {
        $("#steps" + i + "").append('<span><i class="bi bi-check-lg"></i><span>');
        $("#steps" + i + "").addClass("active");
    }
    else {
        $("#steps" + i + "").append('<span><i class="bi bi-x"></i><span>');
        $("#steps" + i + "").addClass("activeError");
    }
   
    i++;
    if (i > 1 && i<=count) {
        $("#line" + (i - 1)+" div").animate({ width: "100%" }, {
            duration: 700,
            easing: "linear",
            complete: function () {
                if (i <= count) {
                    AnimateProgressBar(i, count,status);
                }
            }
        });
    }
}
function HideWorkFlow() {
    $('#demo').hide();
    $('#itemModal3').modal('hide');
 
};