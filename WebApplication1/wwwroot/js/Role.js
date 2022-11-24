//$(document).ready(function () {
//    $("#addRoleId").click(function () {      //There another attribute should be used
        
//        $.ajax({
//            url: "@Url.Action("GetAllRoles", "Home")",
//            type: "post",
//            data: { id: getId },

//            success: function (data) {
//                console.log(data);
//                // $("#test").load("/Home/GetAllAssignedRole");
//                $("#assign").replaceWith(data)
//            },
//            error: function () {
//                alert("Fails 1");
//            }
//        });
//    });
//});

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
    $('#RoleName').blur(function () {
        if ($(this).val() == "") {
            $(this).notify("Role name is required !!!", "error");
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

                        return '<a class="btn btn-warning btn-sm mx-4" href="/Role/EditRole?id=' + data.roleId + '" >Edit</a><button class="btn btn-danger btn-sm" id="delete"  onclick="Delete(' + id + ')"  >Delete</a> ';

                    }
                }
            ],
        });


};

$(function () {
    let message = $('#roleResponse').val();
    if (message != "noResponse") {
        $.notify(message, "success");
    }
});





