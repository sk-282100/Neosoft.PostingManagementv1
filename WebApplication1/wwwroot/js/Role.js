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

    $('#addRoleId').click(function () {

        var regx = new RegExp(/^[A-Z][\w_@*]{1,15}$/);
        var inputValue = $('#RoleName').val().trim();

        if (inputValue == '') {
            $('#RoleName').notify("Role name is required !!!", "error");
        }
        else if (inputValue.length < 2) {
            $('#RoleName').notify("Role Name should contain more than 1 characters", "error");
        }
        else if (inputValue.length > 15) {
            $('#RoleName').notify("Role Name should not contain more than 15 characters", "error");
        }
        else if (!regx.test(inputValue)) {
            $('#RoleName').notify("Role Name should start with Uppercase alphabatical character and \n it contain only aplhanumeric and _ @ *", "error");
        }
        else if (inputValue != "") {

            $.ajax({
                type: "GET",
                url: "/Role/IsRoleAlreadyExist?roleName=" + inputValue,
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                success: function (response) {
                    if (response == true) {
                        $('#RoleName').notify(" This Role name is already Present!!!", "error");
                        isValid = false;
                    }
                    else if (response == false) {
                        if ($('#RoleId').val() != '') {
                            $('#addRoleForm').submit();
                        }
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
});

function OnSuccess(response){
    $("#getallrolesId").DataTable(
    {
       bLengthChange: true,
       lengthMenu: [[5, 10, -1], [5, 10, "All"]],
       bFilter: true,
       bSort: true,
       order:[],
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
    var status = $('#addRoleResponse').val();
    if (status == "true") {
        $.notify("Role Name added Successfully", "success");
    }
})





