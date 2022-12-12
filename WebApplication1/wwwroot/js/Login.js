// alert for invalid Captcha message
$(function () {
    $.notify($(".CaptchaErrorMessage span").html(), "error");
});
// alert for invalid Password and username 
$(function () {
    let message = $('#loginResponse').val();
    if (message != "noResponse") {
        $.notify("Invalid Username and Password!!", "error");
    }
});

// Validation for Username and Password
$(document).ready(function () {

    $("#loginForm").submit(function () {
        var isValid = true;
        if ($("#username").val().trim().length == 0) {
            isValid = false;
            $("#username").notify("UserName cannot be empty!", "error");
        }
        if ($("#password").val().trim().length == 0) {
            isValid = false;
            $("#password").notify("Password cannot be empty!", "error");
        }
        if ($("#DNTCaptchaInputText").val().trim().length == 0) {
            isValid = false;
            $("#DNTCaptchaInputText").notify("Captcha cannot be empty!", "error");
        }
        return isValid;
    });
});