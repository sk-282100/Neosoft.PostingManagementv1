$(function () {

    $('#ResetPasswordBtn').click(function () {

        var isPasswordValid = true;
        if ($('#ConfirmPassword').val().trim() == '') {
            $('#ConfirmPassword').notify("this Field is required", "error");
            isPasswordValid = false;
        }
        else if ($('#ConfirmPassword').val() != $('#NewPassword').val()) {
            $('#ConfirmPassword').notify("Confirm Password is not Matched", "error");
            isPasswordValid = false;
        }

        var passwordValue = $('#NewPassword').val().trim();
        var regx = new RegExp(/^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&-+=()])(?=\S+$).{7,20}$/);

        if (passwordValue == '') {
            $('#NewPassword').notify("new Password is required !!!", "error");
            isPasswordValid = false;
        }
        else if (passwordValue.length < 7) {
            $('#NewPassword').notify("Password should contain more than 7 characters", "error");
            isPasswordValid = false;
        }
        else if (passwordValue.length > 20) {
            $('#NewPassword').notify("Password should not contain more than 20 characters", "error");
            isPasswordValid = false;
        }
        else if (!regx.test(passwordValue)) {
            $('#NewPassword').notify("Password should contains aleast one Uppercase and one lowercase alphabate and \n one numeric character and \n one spacial character @#$%^&-+=() ", "error");
            isPasswordValid = false;
        }
        if (isPasswordValid) {
            $('#ResetPasswordForm').submit();
        }
        //if ($('#UserName').val().trim() == '') {
        //    $('#UserName').notify("Please Enter the Username", "error");
        //}
        //else if ($('#UserName').val().trim() != "") {

        //    $.ajax({
        //        type: "GET",
        //        url: "/AccountView/IsUserNamePresent?userName=" + $('#UserName').val().trim(),
        //        data: '{}',
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        success: function (response) {
        //            if (response == false) {
        //                $('#UserName').notify(" This User name is not Present !!!", "error");
        //            }
        //            else if (response == true) {
        //                if (isPasswordValid) {
        //                    $('#ResetPasswordForm').submit();
        //                }

        //            }
        //        },
        //        failure: function (response) {
        //            alert(response.d);
        //        },
        //        error: function (response) {
        //            alert(response.d);
        //        }
        //    });
        //}

    });
})