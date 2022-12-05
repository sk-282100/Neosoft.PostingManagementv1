$(document).ready(function () {
    debugger
    $.ajax({
        type: "GET",
        url: "/Transfer/GetEmployeesDataForTransfer/",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: GetEmployeesListTransfer,
        failure: function (response) {
            window.location.replace("/Error/Error500");
        },
        error: function (response) {
            window.location.replace("/Error/Error500");
        }
    });

    
});

function GetEmployeesListTransfer(response) {
    $("#employeeTransfer").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [

                { data: 'employeeId' },  
                { data: 'name' },  
                { data: 'scaleName' },  
                { data: 'scale' },  
                { data: 'designation' },  
                { data: 'region' },  
                { data: 'zone' },  
                { data: 'movementType' },  
               
            ],


        });

}