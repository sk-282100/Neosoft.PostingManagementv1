$(function () {
    if ($('#VerifyOTPReponseMessage').val() != "") {
        $.notify($('#VerifyOTPReponseMessage').val(), "error");
    }
    
    var myVar = setInterval(myTimer, 1000);
    function myTimer() {

        var currentDate = new Date();
        var expiryDate = new Date($('#otpExpiryTime').val());
        var t1 = currentDate.getTime();
        var t2 = expiryDate.getTime();
        if (expiryDate < currentDate) {
            $('#showOtpExpiryTime h6').html('Your OTP has expired');
            $('#VerifyOTPBtn').val("Send OTP");
            clearInterval(myVar);
        }

        var formatted = parseInt((t2 - t1) / 60000) + ":" + parseInt(((t2 - t1) / 1000) - parseInt((t2 - t1) / 60000) * 60);

        $('#showOtpExpiryTime h3').html(formatted);
    }


    $('#VerifyOTPBtn').click(function () {

        if ($('#VerifyOTPBtn').val() == 'Verify OTP') {
            if ($('#otp1').val() == '' || $('#otp2').val() == '' || $('#otp3').val() == '' || $('#otp4').val() == '') {
                $.notify("Please enter the OTP", "error");
            }
            else {
                var otpValue = $('#otp1').val() + $('#otp2').val() + $('#otp3').val() + $('#otp4').val()
                $('#OTP').val(parseInt(otpValue));
                $('#VerifyOTPForm').submit();
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
                        $('#otpExpiryTime').val(response.data.otpExpiryTime);
                        $.notify("OTP Sent to your Email Successfully","success")
                        $('#showOtpExpiryTime').show();
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
