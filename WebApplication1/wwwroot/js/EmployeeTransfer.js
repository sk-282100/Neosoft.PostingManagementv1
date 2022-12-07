$(document).ready(function () {
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
    var table = $("#employeeTransfer").DataTable(
        {

            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [
                
                {
                    targets:0,
                    className: 'dt-control',
                    orderable: false,
                    data: null,
                    defaultContent: '',
                },
                {
                    'targets': 1,
                    'searchable': false,
                    'orderable': false,
                    'className': 'dt-body-center',
                    'render': function (data, type, full, meta) {
                        return '<input type="checkbox" name="id[]" value="' + $('<div/>').text(data).html() + '">';
                    }
                },
                { data: 'employeeId' },  
                { data: 'employeeName' },   
                { data: 'scale' },  
                { data: 'scaleCode' },
                { data: 'ubijobRole' },  
                { data: 'regionName' },  
                { data: 'zoneName' },  
                { data: 'movementType' },  


            ],


        });


    // Handle click on "Select all" control
    $('#example-select-all').on('click', function () {
        // Get all rows with search applied
        var rows = table.rows({ 'search': 'applied' }).nodes();
        // Check/uncheck checkboxes for all rows in the table
        $('input[type="checkbox"]', rows).prop('checked', this.checked);
    });

    // Handle click on checkbox to set state of "Select all" control
    $('#example tbody').on('change', 'input[type="checkbox"]', function () {
        // If checkbox is not checked
        if (!this.checked) {
            var el = $('#example-select-all').get(0);
            // If "Select all" control is checked and has 'indeterminate' property
            if (el && el.checked && ('indeterminate' in el)) {
                // Set visual state of "Select all" control
                // as 'indeterminate'
                el.indeterminate = true;
            }
        }
    });

    // Add event listener for opening and closing details
    $('#employeeTransfer tbody').on('click', 'td.dt-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            //old one
            /* row.child.hide();*/

            //new one
            destroyChild(row);
            tr.removeClass('shown');
        } else {
            // Open this row
            //row.child(format(row.data())).show();

            GetEmployeeDetails(row);
            tr.addClass('shown');
        }
    });

    function destroyChild(row) {
        var table = $("table", row.child());
        table.detach();
        /*table.destroy();*/

        // And then hide the row
        row.child.hide();
    }


    function GetEmployeeDetails(row) {
        var d = row.data()
        $.ajax({
            type: "GET",
            url: "/Transfer/GetAdditionalEmployeeDetails?employeeId=" + parseInt(d.employeeId) + "&movementType=" + d.movementType,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                //cellpadding = "5" cellspacing = "0" border = "0" style = "padding-left:50px;"
                var table = $(
                    '<table class="table table-striped">' +
                    '<tr>' +
                    '<th>' +
                    '<table border="2px" class="table table-striped">' +
                    '<thead >' +
                    '<tr>' +
                    '<th colspan="2">Employee details</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody>' +
                    '<tr>' +
                    '<td>' + 'Age/Gender' + '</td>' +
                    '<td>' + response.age + '/' + response.gender + '</td >' +
                    '</tr>' +
                    '<tr>' +
                    '<td>' + 'Job Family' + '</td>' +
                    '<td>' + response.jobRole + '</td >' +
                    '</tr>' +
                    '<tr>' +
                    '<td>' + 'Disability' + '</td>' +
                    '<td>' + response.disability + '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>' + 'Transfer Type' + '</td>' +
                    '<td>' + response.transferType + '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>' + 'Transfer Reason' + '</td>' +
                    '<td>' + response.transferReason + '</td >' +
                    '</tr>' +
                    '</tbody>' +
                    '</table>' +
                    '</th>' +
                    '<th>' +
                    '<table border="1px" class="table table-striped">' +
                    '<thead>' +
                    '<tr>' +
                    '<th colspan="5">' + 'Location Preferences' + '</th>' +
                    '</tr>' +
                    '<tr>' +
                    '<th>' + 'Sr No.' + '</th>' +
                    '<th>' + 'Zone' + '</th>' +
                    '<th>' + 'Region 1' + '</th>' +
                    '<th>' + 'Region 2' + '</th>' +
                    '<th>' + 'Region 3' + '</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody>' +
                    '<tr>' +
                    '<td>' + '1' + '</td>' +
                    '<td>' + response.zonePreference1 + '</td>' +
                    '<td>' + response.zone1RegionPreference1 + '</td >' +
                    '<td>' + response.zone1RegionPreference2 + '</td>' +
                    '<td>' + response.zone1RegionPreference3 + '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>' + '2' + '</td>' +
                    '<td>' + response.zonePreference2 + '</td>' +
                    '<td>' + response.zone2RegionPreference1 + '</td>' +
                    '<td>' + response.zone2RegionPreference2 + '</td>' +
                    '<td>' + response.zone2RegionPreference3 + '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>' + '3' + '</td>' +
                    '<td>' + response.zonePreference3 + '</td>' +
                    '<td>' + response.zone3RegionPreference1 + '</td>' +
                    '<td>' + response.zone3RegionPreference2 + '</td>' +
                    '<td>' + response.zone3RegionPreference3 + '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>' + '4' + '</td>' +
                    '<td>' + response.zonePreference4 + '</td>' +
                    '<td>' + response.zone4RegionPreference1 + '</td>' +
                    '<td>' + response.zone4RegionPreference2 + '</td>' +
                    '<td>' + response.zone4RegionPreference3 + '</td>' +
                    '</tr>' +
                    '<tr>' +
                    '<td>' + '5' + '</td>' +
                    '<td>' + response.zonePreference5 + '</td>' +
                    '<td>' + response.zone5RegionPreference1 + '</td>' +
                    '<td>' + response.zone5RegionPreference2 + '</td>' +
                    '<td>' + response.zone5RegionPreference3 + '</td>' +
                    '</tr>' +
                    '</tbody>' +
                    '</table>' +
                    '</th>' +
                    '</tr>' +
                    '<tr>' +
                    '<th>' +
                    '<table border="2px" class= "table table-striped">' +
                    '<thead>' +
                    '<tr>' +
                    '<th>' + ' ' +'</th>' +
                    '<th>' + 'Current Posting' + '</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tr>' +
                    '<td>' + 'Role' + '</td>' +
                    '<td>' + response.currentRole + '</td >' +
                    '</tr>' +
                    '<tr>' +
                    '<td>' + 'Region' + '</td>' +
                    '<td>' + response.currentRegion + '</td >' +
                    '</tr>' +
                    '<tr>' +
                    '<td>' + 'Zone' + '</td>' +
                    '<td>' + response.currentZone + '</td >' +
                    '</tr>' +
                    '</table>'+
                        '</th>' +
                    '</tr>' +
                    '</table>'
                );

                // Display it the child row
                row.child(table).show();
                
            },
            failure: function (response) {
                window.location.replace("/Error/Error500");
            },
            error: function (response) {
                window.location.replace("/Error/Error500");
            }
        });


    }

}