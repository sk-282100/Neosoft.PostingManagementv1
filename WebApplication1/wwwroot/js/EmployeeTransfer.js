var table
$(document).ready(function () {
    GetEmployeesListTransfer();
});

function GetEmployeesListTransfer() {
    table = $("#employeeTransfer").DataTable(
        {
            clear: true,
            serverSide: true,            
            destroy: true,
            pageLength: 5,
            lengthMenu: [[5, 10, 25, 50, 1000000], [5, 10, 25, 50, 'all']],
            autoFill: false,
            bFilter: true,
            /*bSort: true,*/
            bPaginate: true,
            initComplete: function (settings, json) {
                $(this.api().table().container()).find('input').attr('autocomplete', 'off');
            },
            ajax: {
                url: "/Transfer/GetEmployeesDataForTransfer",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: function (d) {
                    var data = { data: d };
                   return JSON.stringify(data);
                },
                AutoWidth: false,
                dataSrc: function (json) {
                    var jsonData = json;
                    json.draw = jsonData.draw;
                    json.recordsTotal = jsonData.recordsTotal;
                    json.recordsFiltered = jsonData.recordsFiltered;
                    //json.data = JSON.parse(jsonData.data);   
                    return json.data;
                }
            },            
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
            columnDefs: [
                {
                    targets: [2, 3, 4, 5, 6, 7, 8, 9],
                    className: "text-center",
                    searchable: false,
                    orderable: true
                },
                {
                    targets: [0, 1],
                    orderable: false
                }
            ],
            columns: [
                {
                    targets: 0,
                    className: 'dt-control',
                    searchable:false,
                    data: null,
                    defaultContent: '',
                },
                {
                    targets: 1,
                    data: 'employeeId',
                    searchable:false,
                    checkboxes: {
                        selectRow: true
                    }
                },
                {  data: 'employeeId' ,name:'EmployeeId'},
                {  data: 'employeeName' ,name:'EmployeeName'},
                {  data: 'scale' ,name:'scale'},
                {  data: 'scaleCode' ,name:'scaleCode'},
                { data: 'ubijobRole', name:'ubijobRole'},
                {  data: 'regionName',name:'regionName' },
                {  data: 'zoneName' ,name:'zoneName'},
                {  data: 'movementType' ,name:'movementType'},


            ],
            'select': {
                'style': 'multi'
            },

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

function MatchVacancy() {
    
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
        
        window.location.assign("/Transfer/MatchingRequestTransferVacancyView/")
        
    }
    
}



