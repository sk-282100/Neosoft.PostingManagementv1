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
    ShowRecords();
};

//$(document).ready(function () {
//    $("#recordsDiv").hide();
//});
function ShowRecords() {
    $("#recordsDiv").show();
}