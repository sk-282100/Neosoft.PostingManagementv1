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


    $('#UserName').blur(function () {
        if ($(this).val() == "") {
            $(this).notify("User name is required !!!","error");
        }
        if ($(this).val().length >= 1) {
            var userName = this.value;
            $.ajax({
                type: "GET",
                url: "/AccountView/IsUserNamePresent?userName=" + this.value,
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response == true) {
                        $('#UserName').notify(" This User name is already Present!!!", "error");
                        $('#UserName').val('');
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

    $('#CreateUserNameForm').submit(function () {
        var isValid = true;
        if ($('#UserName').val() == '') {
            isValid = false;
            $('#UserName').notify("User name is required !!!", "error");

        }
        return isValid;
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
                
                { data: 'roleId' },
                { data: 'userName' },
             
                {
                    data: null,
                    "mRender": function (data, type, full) {
                        var id ="'"+ data.uId+"'";
                        return '<a class="btn btn-warning btn-sm mx-4" href="/AccountView/EditUserRoleDetails?id=' + data.uId + '" >Edit</a><button id="delete" class="btn btn-danger btn-sm " onclick="onDelete(' + id + ')" >Delete</button> ';
                       
                    }
                }
            ]
        });
};
function onDelete(data) {
 
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



