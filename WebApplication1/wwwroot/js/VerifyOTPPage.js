$(function () {
    if ($('#VerifyOTPReponseMessage').val() != "") {
        $.notify($('#VerifyOTPReponseMessage').val(), "error");
    }
    
    var myVar = setInterval(
        () => {
            var currentDate = new Date();
            var expstring = $('#otpExpiryTime').val();
            var expiryDate = new Date(expstring);
            var t1 = currentDate.getTime();
            var t2 = expiryDate.getTime();
            if (expiryDate < currentDate) {
                $('#showOtpExpiryTime h6').html('Your OTP has expired');
                $('#VerifyOTPBtn').val("Send OTP");
                $('#showOtpExpiryTime').attr('class', 'd-flex flex-column text-danger text-center border border-danger');
                $("#otp1").prop("disabled", true);
                $("#otp2").prop("disabled", true);
                $("#otp3").prop("disabled", true);
                $("#otp4").prop("disabled", true);
                clearInterval(myVar);
            }

            var formatted = parseInt((t2 - t1) / 60000) + ":" + parseInt(((t2 - t1) / 1000) - parseInt((t2 - t1) / 60000) * 60);

            $('#showOtpExpiryTime h3').html(formatted);
        }
        , 1000);
    

    $('#VerifyOTPBtn').click(function () {

        if ($('#VerifyOTPBtn').val() == 'Verify OTP') {
            if ($('#otp1').val() == '' || $('#otp2').val() == '' || $('#otp3').val() == '' || $('#otp4').val() == '') {
                $.notify("Please enter the OTP", "error");
            }
            else {
                var otpValue = $('#otp1').val() + $('#otp2').val() + $('#otp3').val() + $('#otp4').val();
                $('#OTP').val(parseInt(otpValue));
                $.ajax({
                    type: "GET",
                    url: "/Login/VerifyOTP?username=" + $('#Username').val() + "&otp=" + parseInt(otpValue),
                    data: '{}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        if (!response) {

                            $.notify("OTP Doesn't matched , try again !", "error");
                            var myVar = setInterval(
                                () => {
                                    var currentDate = new Date();
                                    var expstring = $('#otpExpiryTime').val();
                                    var expiryDate = new Date(expstring);
                                    var t1 = currentDate.getTime();
                                    var t2 = expiryDate.getTime();
                                    if (expiryDate < currentDate) {
                                        $('#showOtpExpiryTime h6').html('Your OTP has expired');
                                        $('#VerifyOTPBtn').val("Send OTP");
                                        $('#showOtpExpiryTime').attr('class', 'd-flex flex-column text-danger text-center border border-danger');
                                        $("#otp1").prop("disabled", true);
                                        $("#otp2").prop("disabled", true);
                                        $("#otp3").prop("disabled", true);
                                        $("#otp4").prop("disabled", true);
                                        clearInterval(myVar);
                                    }

                                    var formatted = parseInt((t2 - t1) / 60000) + ":" + parseInt(((t2 - t1) / 1000) - parseInt((t2 - t1) / 60000) * 60);

                                    $('#showOtpExpiryTime h3').html(formatted);
                                }
                                , 1000);
                        }
                        else {
                            $('#VerifyOTPForm').submit();
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
        }
        else if ($('#VerifyOTPBtn').val() == 'Send OTP') {
            $.ajax({
                type: "GET",
                url: "/Login/SendOTP?userName=" + $('#Username').val(),
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.succeeded == true) {
                        $('#otpExpiryTime').val(response.data.otpExpiryTime.split('.')[0]);
                        $.notify("OTP Sent to your Email Successfully", "success");
                        $('#showOtpExpiryTime h6').html('OTP Expires in ');
                        $('#VerifyOTPBtn').val("Verify OTP");
                        $('#showOtpExpiryTime').attr('class', 'd-flex flex-column text-success text-center border border-success');
                        $("#otp1").prop("disabled", false);
                        $("#otp2").prop("disabled", false);
                        $("#otp3").prop("disabled", false);
                        $("#otp4").prop("disabled", false);
                        var myVar = setInterval(
                            () => {
                                var currentDate = new Date();
                                var expstring = $('#otpExpiryTime').val();
                                var expiryDate = new Date(expstring);
                                var t1 = currentDate.getTime();
                                var t2 = expiryDate.getTime();
                                if (expiryDate < currentDate) {
                                    $('#showOtpExpiryTime h6').html('Your OTP has expired');
                                    $('#VerifyOTPBtn').val("Send OTP");
                                    $('#showOtpExpiryTime').attr('class', 'd-flex flex-column text-danger text-center border border-danger');
                                    $("#otp1").prop("disabled", true);
                                    $("#otp2").prop("disabled", true);
                                    $("#otp3").prop("disabled", true);
                                    $("#otp4").prop("disabled", true);
                                    clearInterval(myVar);
                                }

                                var formatted = parseInt((t2 - t1) / 60000) + ":" + parseInt(((t2 - t1) / 1000) - parseInt((t2 - t1) / 60000) * 60);

                                $('#showOtpExpiryTime h3').html(formatted);
                            }
                            , 1000);
                    }
                    else {
                        $('#VerifyOTPReponseMessage').val(response.message);
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
