
$(function () {
    let excelUploadType = $('#excelUploadType').val();
    $.ajax({
        type: "POST",
        url: "/Posting/ShowUploadHistory/" + parseInt(excelUploadType),
        data: '{}',
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

function OnSuccess(response) {
    $("#uploadHistory").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [
                { data: 'fileName' },
                { data: 'date' },
                { data: 'numberOfRows' },
                { data: 'insertedRows' },
                {
                    data: null,
                    "mRender": function (data, type, full) {
                        if (data.uploadStatus.toLowerCase() == "success") {
                            return '<a class="btn btn-success btn-sm" href=#>' + data.uploadStatus +'</a>';
                        } else {
                            return '<a class="btn btn-danger btn-sm" href=#>' + data.uploadStatus +'</a>';
                        }
                    }
                },
                {
                    data: null,
                    "mRender": function (data, type, full) {
                        if (data.uploadStatus.toLowerCase() == "success") {
                            return '<a id="viewButton" class="btn btn-success btn-sm" onclick=ViewRecords(' + data.batchId + ')>View</a>';
                        } else if (data.uploadStatus.toLowerCase() == "failed") {
                            var failedMessage = "'"+ data.reasonOfFailure +"'";
                            return '<a id="viewButton2" class="btn btn-success btn-sm" onclick="ShowFailureReason(' + failedMessage +')">View</a>';
                        }
                    }
                }],
            
        });
};

function ShowFailureReason(ReasonOfFailure) { 
    $("#recordsDiv").show();
    $("#failureReason")[0].innerHTML = ''
    $("#failureReason").append("<h4 class='text-danger'>" + ReasonOfFailure + "</h4>");
    $('#itemModel2').modal('show');
}

$(document).ready(function () {
    $('#ExcelUploadForm').submit(function () {
        var isValid = true;
        if ($('#ExcelFile').val() == '') {
            isValid = false;
            $.notify("Please Upload the Excel File !!!", "error");
        }
        return isValid;
    });

    $('#ExcelFile').change( function () {
        if (this.files[0].size > 5242880 && this.files[0].type != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
            $.notify("Please Upload the File in Excel (.xlsx) formate with less than 5Mb file size", "error")
            this.value = null;
        }
        else if (this.files[0].size > 5242880) {
            $.notify("Please upload the Excel File Which is less than 5MB in size", "error");
            this.files[0] = null;
        }
        else if (this.files[0].type != "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") {
            $.notify("Please Upload the Excel File in .xlsx format", "error")
            this.value = null;
        }
    });
});

$(function () {
    let message = $('#excelResponse').val();
    if (message == "Excel is not in correct format") {
        $.notify("Invalid Excel file! Columns does not match!!", "error");
    }
    else if (message == "File Uploaded Successfully") {
        $.notify("File Uploaded Successfully", "success");
    }
});