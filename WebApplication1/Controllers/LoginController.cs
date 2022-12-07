using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PostingManagement.UI.Models;
using PostingManagement.UI.Services.AccountServices.Contracts;
using PostingManagement.UI.Services.LoginService.Contracts;

namespace PostingManagement.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _captchaOptions;
        private readonly ILoginService _loginService;
        private readonly IAccountService _accountService;
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();

        public LoginController(IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> options, ILoginService loginService,IAccountService accountService)
        {
            _validatorService = validatorService;
            _captchaOptions = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
            _loginService = loginService;
            _accountService = accountService;
        }

        [HttpGet]
        
        public IActionResult Login()
        {
            ViewBag.LoginResponse= null;            
            return View();
        }

        [HttpPost]
        //Login Different Module.
        public async Task<IActionResult> Login(LoginModel model)
        {
            //Checks The Captcha Is Valid Or Not.
            if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.SumOfTwoNumbers))
            {
                this.ModelState.AddModelError(_captchaOptions.CaptchaComponent.CaptchaInputName, "Please enter the Valid Captcha");
                return View(model);
            }
            //Checking The User Is Authenticated For Role 
            LoginResponseModel response = await  _loginService.Login(model);
            if(response.IsAuthenticated == true)
            {
                HttpContext.Session.SetString("Username", response.UserName);
                HttpContext.Session.SetString("UserRole", response.Role);
                return RedirectToAction("EmployeeMasterUpload", "Posting");
            }
            else
            {
                ViewBag.LoginResponse = response==null?null:response;
                
                return View(model);
            }                        
        }
        [HttpGet]
        //LogOut the User & Clear the Session.
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("UserRole");
            return RedirectToAction("Login");
        }

        [HttpGet]
        //Reset Password view
        public async Task<IActionResult> ResetPassword()
        {
            return View();
        }

        [HttpPost]
        //Reset Password Method
        public async Task<IActionResult> ResetPassword(ResetPasswordRequestModel requestModel)
        {
            var response = await _accountService.ResetPassword(requestModel);
            ViewBag.ResetPasswordResponse = response;
            return View("ResetPasswordStatusView");
        }
    }
}
