using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using PostingManagement.UI.Models;
using PostingManagement.UI.Models.Responses;
using PostingManagement.UI.Services.LoginService.Contracts;

namespace PostingManagement.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _captchaOptions;
        private readonly ILoginService _loginService;
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();

        public LoginController(IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> options, ILoginService loginService)
        {
            _validatorService = validatorService;
            _captchaOptions = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.LoginResponse= null;            
            string[] banks = new string[] { "Union Bank of India", "Coorpation Bank" };
            ViewBag.BankList = new SelectList(banks, "BankName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.SumOfTwoNumbers))
            {
                this.ModelState.AddModelError(_captchaOptions.CaptchaComponent.CaptchaInputName, "Please enter the security code as a number.");
                return View(model);
            }
            LoginResponseModel response = await  _loginService.Login(model);
            if(response.IsAuthenticated == true)
            {
                HttpContext.Session.SetString("Username", response.UserName);
                //HttpContext.Session.SetString("Role", response.Role);
                return RedirectToAction("EmployeeMasterUpload", "Posting");
            }
            else
            {
                ViewBag.LoginResponse = response==null?null:response;
                
                return View(model);
            }                        
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Remove("Username");
            //HttpContext.Session.Remove("Role");
            return RedirectToAction("Login");
        }
    }
}
