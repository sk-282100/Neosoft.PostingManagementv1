$(function () {
    if ($('#SendOTPReponseMessage').val() != "") {
        $.notify($('#SendOTPReponseMessage').val(), "error");
    }

    $('#SendOTPBtn').click(function () {

        if ($('#Username').val().trim() == '') {
            $('#Username').notify("Please Enter the Username", "error");
        }
        else if ($('#Username').val().trim() != "") {

            $.ajax({
                type: "GET",
                url: "/Login/IsUserNamePresent?userName=" + $('#Username').val().trim(),
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response == false) {
                        $('#Username').notify(" This User name is not Present !!!", "error");
                    }
                    else if (response == true) {
                            $('#ForgotPasswordForm').submit();
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