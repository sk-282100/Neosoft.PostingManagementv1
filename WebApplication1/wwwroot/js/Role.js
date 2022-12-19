var table;
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


    $(document).on('click', 'button.DatatableDeleteBtn', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        Swal.fire({
            title: 'Are you sure you want to Delete Role?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "GET",
                    url: '/Role/RemoveRole?id=' + $(this).val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.data = true) {
                            $.notify("Role Deleted successfully", "success");
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

    $('#addRoleId').click(function () {

        var regx = new RegExp(/^[A-Z][\w_@*]{1,15}$/);
        var inputValue = $('#RoleName').val().trim();
        //Alert If In Role Name Is Not Entered Any Thing
        if (inputValue == '') {
            $('#RoleName').notify("Role name is required !!!", "error");
        }
        //Alert If Role Name Length Is Less Than 2 Character
        else if (inputValue.length < 2) {
            $('#RoleName').notify("Role Name should contain more than 1 characters", "error");
        }
        //Alert If Role Name Length Is More Than 15 
        else if (inputValue.length > 15) {
            $('#RoleName').notify("Role Name should not contain more than 15 characters", "error");
        }
        //Alert for Role Name Usinh Regular Expression
        else if (!regx.test(inputValue)) {
            $('#RoleName').notify("Role Name should start with Uppercase alphabatical character and \n it contain only aplhanumeric and _ @ *", "error");
        }
        
        else if (inputValue != "") {
            //Call RoleAlreadyExist Action 
            $.ajax({
                type: "GET",
                url: "/Role/IsRoleAlreadyExist?roleName=" + inputValue,
                contentType: "application/json; charset=utf-8",
                data: '{}',
                dataType: "json",
                success: function (response) {
                    if (response == true) {
                        //Alert For Role Name Which is Already Exist
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
//Data Table Get All Roles  Using Input Table Id 
function OnSuccess(response){
    table = $("#getallrolesId").DataTable(
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
                   return '<a class="btn btn-warning btn-sm mx-4" href="/Role/EditRole?id=' + data.roleId + '" ><i class="bi bi-pencil-fill" title="Edit"></i></a><button id="delete" class="btn btn-danger btn-sm DatatableDeleteBtn "  value="' + data.roleId +'" ><i class="bi bi-trash-fill" title="Delete"></i></a> ';
    
               }
           }
       ],
    });
};
//Alert If Role Name Added Successfully
$(function () {
    var status = $('#addRoleResponse').val();
    if (status == "true") {
        $.notify("Role Name added Successfully", "success");
    }
})

