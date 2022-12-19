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
});
function OnSuccess(response) {
    $("#getAllJobFamilyId").DataTable(
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
                    "mRender": function (data, type, full) {
                        var id = "'" + data.jobFamilyId + "'";
                        return '<a class="btn btn-warning btn-sm mx-4" href="/JobFamily/EditJobFamily?id=' + data.jobFamilyId + '&currentRole=' + data.jobFamily + '" ><i class="bi bi-pencil-fill" title="Edit"></i></a><button id="delete" class="btn btn-danger btn-sm " onclick="Delete(' + id + ')" ><i class="bi bi-trash-fill" title="Delete"></i></a> ';

                    }
                }
            ]
        });
};

//Swal Alert For Confirmation Of Delete JobFamily
function Delete(id) {
    Swal.fire({
        title: 'Are you sure you want to Delete Job family?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/JobFamily/RemoveJobFamily?id=' + id;
        }
    });
}



