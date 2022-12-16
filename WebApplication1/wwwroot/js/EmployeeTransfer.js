var table
$(document).ready(function () {
    GetEmployeesListTransfer();
});

function GetEmployeesListTransfer() {
    table = $("#employeeTransfer").DataTable(
        {
            clear: true,
            serverSide: true,
            searching: false,
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 100], [5, 10, 25, 50, 100]],
            autoFill: false,
            bFilter: true,
            bSort: true,
            bPaginate: true,
            //"initComplete": function (settings, json) {
            //    $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            //},
            ajax: {
                url: "/Transfer/GetEmployeesDataForTransfer",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                    debugger;
                    return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    debugger;
                    return json.data;
                }
            },

            //data: response,
            dom: 'lBfrtip',
            buttons: [
                {
                    extend: 'pdf',
                    title: 'Employee Transfer List',
                    text: '<i class="bi bi-file-earmark-pdf"></i>',
                },
                {
                    extend: 'copy',
                    title: 'Employee Transfer List',
                    text: '<i class="bi bi-clipboard"></i>'
                },
                {
                    extend: 'print',
                    title: 'Employee Transfer List',
                    text: '<i class="bi bi-printer"></i>'
                },
                {
                    extend: 'csv',
                    title: 'Employee Transfer List',
                    text: '<i class="bi bi-file-earmark-excel"></i>'
                }
            ],

            columns: [
                {
                    targets: 0,
                    className: 'dt-control',
                    orderable: false,
                    data: null,
                    defaultContent: '',
                },
                {
                    targets: 1,
                    data: 'employeeId',
                    checkboxes: {
                        selectRow: true
                    }
                },
                { targets: 2, data: 'employeeId' },
                { targets: 3, data: 'employeeName' },
                { targets: 4, data: 'scale' },
                { targets: 5, data: 'scaleCode' },
                { targets: 6, data: 'ubijobRole' },
                { targets: 7, data: 'regionName' },
                { targets: 8, data: 'zoneName' },
                { targets: 9, data: 'movementType' },


            ],
            'select': {
                'style': 'multi'
            },
            'order': [[0, 'asc']]

        });

    // Add event listener for opening and closing details
    $('#employeeTransfer tbody').on('click', 'td.dt-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            destroyChild(row);
            tr.removeClass('shown');
        } else {
            // Open this row
           
            GetEmployeeDetails(row);
            tr.addClass('shown');
        }
    });

}

function GenerateList() {
    
    var rows_selected = table.column(1).checkboxes.selected();

    var employeeIdList = new Array()
    // Iterate over all selected checkboxes
    $.each(rows_selected, function (index, rowId) {
        employeeIdList.push(rowId)        
    });
    if (employeeIdList.length == 0) {
        $.notify("No employees selected, please select at least one employee to generate list", "error");
    }
    else {
        console.log(employeeIdList)
        sessionStorage.setItem("EmployeeIdList", JSON.stringify(employeeIdList));
        
        window.location.assign("/Transfer/FinalizeEmployeeTransferViewCo/")
        
    }
    
}



