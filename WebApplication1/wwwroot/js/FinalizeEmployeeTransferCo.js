var table
$(document).ready(function () {
    var employeeIdList = JSON.parse(sessionStorage.getItem("EmployeeIdList"))
    $.ajax({
            type: "POST",
            traditional: true,
            url: "/Transfer/GetEmployeesDataForTransferByEmployeeId/",
            data: JSON.stringify(employeeIdList),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: FillDatatTable,
            failure: "",
            error: function (response) {
                window.location.replace("/Transfer/EmployeeTransferView/");
            },
    });


});

function FillDatatTable(response) {
    table = $("#finalizeEmployeeTransfer").DataTable(
        {
            searching: false,
            bLengthChange: false,
            //lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            //bFilter: true,
            //bSort: true,
            //bPaginate: true,
            data: response,
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
                { targets: 1, data: 'employeeId' },
                { targets: 2, data: 'employeeName' },
                { targets: 3, data: 'scale' },
                { targets: 4, data: 'scaleCode' },
                { targets: 5, data: 'ubijobRole' },
                { targets: 6, data: 'regionName' },
                { targets: 7, data: 'zoneName' },
                { targets: 8, data: 'movementType' },
                {
                    targets: 9,
                    data: null,
                    orderable: false,
                    className: "removeEmployee",
                    mRender: function (data) {
                        return '<a class="btn btn-danger btn-sm" ><i class="bi bi-trash-fill" title="Delete"></i></a>'
                    }
                }


            ],
            order: [[2, 'asc']]

        });

    // Add event listener for opening and closing details
    $('#finalizeEmployeeTransfer tbody').on('click', 'td.dt-control', function () {
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



    $('#finalizeEmployeeTransfer tbody').on('click', 'td.removeEmployee', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);
        Swal.fire({
            title: 'Are you sure you want to Remove Employee?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes!'
        }).then((result) => {
            if (result.isConfirmed) {
                row.remove()
                    .draw();
            }
        });

    });

}

function FinalizeList() {
    var finalizeEmployeeListByCo = new Array();
    finalizeEmployeeListByCo = $('#finalizeEmployeeTransfer').DataTable().rows().data().toArray();
    $.ajax({
        type: "POST",
        traditional: true,
        url: "/Transfer/FinalizeEmployeeTransferCo/",
        data: JSON.stringify(finalizeEmployeeListByCo),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response.successCount == finalizeEmployeeListByCo.length) {
                $.notify("Employee Transfer List Generated Successfully", "success");
                sessionStorage.removeItem("EmployeeIdList");
                /*window.location.replace("");*/
            }
        },
        error: function (response) {
            window.location.replace("/Error/Error500");
        },
    });
}