
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
                        return '<a class="btn btn-success btn-sm" href=# data-historyid='+data.historyId+'>View Record</a>';
                        
                    }
                }],
            
        });
};