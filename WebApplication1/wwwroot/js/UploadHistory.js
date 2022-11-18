﻿
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
                }, {
                    data: null,
                    "mRender": function (data, type, full) {
                        return '<a class="btn btn-success btn-sm" href=# data-historyid='+data.historyId+'>View</a>';
                        
                    }
                }],
            
        });
};

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
        else {
            $.notify("Your File Uploaded Successfully", "success");
            return false;
        }
    });
});