var table;
$(function () {

    $.ajax({
        type: "GET",
        url: "/JobFamily/GetAllJobFamily",
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

    //Swal Alert For Confirmation Of Delete JobFamily
    $(document).on('click', 'button.DatatableDeleteBtn', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        Swal.fire({
            title: 'Are you sure you want to Delete Job Family?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "GET",
                    url: '/JobFamily/RemoveJobFamily?id=' + $(this).val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.data = true) {
                            $.notify("Job Family Deleted successfully", "success");
                            row.remove().draw();
                        }
                        else {
                            $.notify(" Deletion has failed", "success");

                        }
                    },
                    failure: function (response) {
                        alert(response.d);
                    },
                    error: function (response) {
                        alert(response.d);
                    }
                });
            }
        });
        return false;
    });


});
function OnSuccess(response) {
    table = $("#getAllJobFamilyId").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [

                { data: 'jobFamilyName' },

                {
                    data: null,
                    orderable: false,
                    "mRender": function (data, type, full) {
                        return '<a class="btn btn-warning btn-sm mx-4" href="/JobFamily/EditJobFamily?id=' + data.jobFamilyId + '&currentRole=' + data.jobFamily + '" ><i class="bi bi-pencil-fill" title="Edit"></i></a><button id="delete" class="btn btn-danger btn-sm DatatableDeleteBtn "  value="' + data.jobFamilyId +'" ><i class="bi bi-trash-fill" title="Delete"></i></a> ';

                    }
                }
            ]
        });
};


