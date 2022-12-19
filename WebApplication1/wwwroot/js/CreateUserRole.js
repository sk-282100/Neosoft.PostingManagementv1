var table;
$(function () {
    
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

    $(document).on('click', 'button.DatatableDeleteBtn', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        Swal.fire({
            title: 'Are you sure you want to Delete User?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    type: "GET",
                    url: '/AccountView/DeleteUserName?id=' + $(this).val(),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (response.data = true) {
                            $.notify("user Deleted successfully", "success");
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

    $('#CreateUserBtn').click(function () {
        var isValide = true;
        var inputValue = $('#UserName').val().trim();

        if ($('#RoleId').val() == '') {
            $('#RoleId').notify("Please select the Role", "error");
            isValide = false;
        }
        var emailRegex = new RegExp(/^[a-zA-Z]+[a-zA-Z0-9_.-]+[a-zA-Z0-9]+@([a-zA-Z0-9-]+.)+[a-zA-Z]{2,6}$/)
        if ($('#Email').val() == '') {
            $('#Email').notify("Email is Requried", 'error');
        }
        else if (!emailRegex.test($('#Email').val())) {

            $('#Email').notify("Email should be in correct formate ", "error");
            isValide = false;
        }

        var regx = new RegExp(/^[a-zA-Z][\w.-_]{2,15}$/);
        if (inputValue == '') {
            $('#UserName').notify("User name is required !!!", "error");
        }
        else if (inputValue.length < 3) {
            $('#UserName').notify("Username should contain more than 2 characters", "error");
        }
        else if (inputValue.length > 15) {
            $('#UserName').notify("Username should not contain more than 15 characters", "error");
        }
        else if (!regx.test(inputValue)) {

            $('#UserName').notify("Username should start with alphabatical character \n and it contain only aplhanumeric and . - _", "error");

        }
        else if (inputValue != "") {

            $.ajax({
                type: "GET",
                url: "/AccountView/IsUserNamePresent?userName=" + inputValue,
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response == true) {
                        $('#UserName').notify(" This User name is already Present!!!", "error");

                    }
                    else if (response == false) {
                        if (isValide) {
                            $('#CreateUserNameForm').submit();
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

function OnSuccess(response) {
    table = $("#userRoleDetails").DataTable(
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
                        return '<a class="btn btn-warning btn-sm mx-4" href="/AccountView/EditUserRoleDetails?id=' + data.uId + '&currentRole=' + data.role + '" ><i class="bi bi-pencil-fill" title="Edit"></i></a><button id="delete" class="btn btn-danger btn-sm DatatableDeleteBtn "  value="' + data.uId +'" ><i class="bi bi-trash-fill" title="Delete"></i></button> ';

                    }
                }
            ]
        });
};

$(function () {
    var status = $('#addUserResponse').val();
    if (status == "true") {
        $.notify("User Name added Successfully", "success");
    }
})

