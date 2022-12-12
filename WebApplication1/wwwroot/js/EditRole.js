$(function () {

    $.ajax({
        type: "GET",
        url: "/Role/GetAllRoles",
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
    $("#getallrolesId").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [
                { data: 'roleName' },

                {
                    data: null,
                    "mRender": function (data, type, full) {
                        var id = "'" + data.roleId + "'";
                        return '<a class="btn btn-warning btn-sm mx-4" href="/Role/EditRole?id=' + data.roleId + '&currentRole=' + data.role + '" >Edit</a><button id="delete" class="btn btn-danger btn-sm " onclick="Delete(' + id + ')" >Delete</a> ';

                    }
                }
            ]
        });
};




