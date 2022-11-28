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

    
});
$(document).ready(function () {
    $('#addRoleForm').submit(function () {

        debugger
        var isValid = true;
        var inputValue = $('#RoleName').val().trim();
        var regx = new RegExp(/^[a-zA-Z][\w.-]{3,15}$/);

        if (inputValue == '') {
            isValid = false;
            $('#RoleName').notify("Role name is required !!!", "error");
        }


        //else if (!regx.test('#RoleName')) {
        //    isValid = false;
        //    $('#RoleName').notify("User name should in correct format!!!", "error");
        //}

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
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
        else if (false) {
            console.log("hi");
        }

        return isValid;
    });

});




    //$('#RoleName').blur(function () {
    //    var inputValue = $(this).val().trim();
    //    if (inputValue == "") {
    //        $(this).notify("Role name is required !!!", "error");
    //    }
    //    if (inputValue == '#RoleName') {
    //        $(this).notify("Role name is required !!!", "error");
    //    }
/*});*/

        //var isValid = false;
        //var regx = new RegExp(/^[a-zA-Z][\w.-]{3,15}$/);
        //if (!regx.test('#RoleName')) {
        //    isValid = false;
        //    $('#RoleName').notify("Role name should in correct format!!!", "error");
        //}
        //return isValid;


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





