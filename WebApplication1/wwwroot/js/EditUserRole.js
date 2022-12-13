$(function () {
    //Call Action ShowUserDetails
    $.ajax({
        type: "GET",
        url: "/AccountView/ShowUserRoleDetails/",
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
    //DataTable Show User Details  Using Input Table Id 
    $("#userRoleDetails").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            order:[],
            bPaginate: true,
            data: response,
            columns: [

                { data: 'role' },
                { data: 'userName' },
                { data: 'email'},
                {
                    data: null,
                    "mRender": function (data, type, full) {
                        var id = "'" + data.uId + "'";
                        return '<a class="btn btn-warning btn-sm mx-4" href="/AccountView/EditUserRoleDetails?id=' + data.uId + '&currentRole=' + data.role + '" ><i class="bi bi-pencil-fill" title="Edit"></i></a><button id="delete" class="btn btn-danger btn-sm " onclick="onDelete(' + id + ')" ><i class="bi bi-trash-fill" title="Delete"></i></button> ';

                    }
                }
            ]
        });
};

function onDelete(data) {
    //Swal Alert For Confirmation of Delete User Role
    Swal.fire({
        title: 'Are you sure you want to Delete User?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = '/AccountView/DeleteUserName?id=' + data;
        }
    });
}



