$(function () {
    
    $.ajax({
        type: "GET",
        url: "/AccountView/ShowUserRoleDetails/" ,
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
    $("#userRoleDetails").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns: [
                { data: 'uId' },
                { data: 'roleId' },
                { data: 'userName' },
             
                {
                    data: 'uId',
                    "mRender": function (data, type, full) {
                       
                        return '<a class="btn btn-warning btn-sm mx-4" href="/AccountView/EditUserRoleDetails?id=' + data +'" >Edit</a><button id="delete" class="btn btn-danger btn-sm " onclick="onDelete('+data+')" >Delete</button> ';
                       
                    }
                }
            ],
        });
};

