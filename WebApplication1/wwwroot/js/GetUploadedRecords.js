function ViewRecords(BatchId) {
    $.ajax({
        type: "GET",
        url: "/Posting/GetUploadedDataByBatchId/" + parseInt(BatchId),
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: GetRecords,
        failure: function (response) {
            window.location.replace("/Error/Error500");
        },
        error: function (response) {
            window.location.replace("/Error/Error500");
        }
    });
}
function GetRecords(response) {
    $('#itemModel').modal('show');
    if ($("#excelUploadType").val() == 3) {
        GetEmployeeMasterRecords(response);        
    }
    else if ($("#excelUploadType").val() == 2)
    {        
        GetDepartmentMasterRecords(response);        
    }    
};
function GetDepartmentMasterRecords(response) {
    if ($.fn.dataTable.isDataTable('#departmentMasterRecords')) {
        $('#departmentMasterRecords').DataTable().destroy();
    }
    $("#departmentMasterRecords").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [
                { data: 'batchId' },
                { data: 'departmentCode' },
                { data: 'departmentName' }
            ],

        });
    $("#departmentMasterRecords").show();
    ShowRecords();
}
function GetEmployeeMasterRecords(response) {
    if ($.fn.dataTable.isDataTable('#employeeMasterRecords')) {
        $('#employeeMasterRecords').DataTable().destroy();
    }
    $("#employeeMasterRecords").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [
                { data: 'batchId' },
                { data: 'employeeId' },
                { data: 'employeeName' },
                { data: 'location' },
                { data: 'locationDesired' },
                { data: 'departmentRo' },
                { data: 'regionCode' },
                { data: 'regionName' },
                { data: 'zoneCode' },
                { data: 'zoneName' },
                { data: 'jobCode' },
                { data: 'designation' },
                { data: 'roleStartDate' },
                { data: 'scaleCode' },
                { data: 'scale' },
                { data: 'ubijobRole' },
                { data: 'bankName' },
                { data: 'birthDate' },
                { data: 'sex' },
                { data: 'disability' },
                { data: 'locationStartDate' },
                { data: 'domicileZone' },
                { data: 'rostartDate' },
                { data: 'lastPromotionDate' },
                { data: 'zostartDate' }
            ],

        });
    $("#employeeMasterRecords").show();
    ShowRecords();
}
function ShowRecords() {
    $("#recordsDiv").show();
}