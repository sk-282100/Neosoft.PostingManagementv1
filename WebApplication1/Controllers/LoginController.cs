using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using PostingManagement.UI.Models;

namespace PostingManagement.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly DNTCaptchaOptions _captchaOptions;
        readonly HttpClientHandler _clientHandler = new HttpClientHandler();

        public LoginController(IDNTCaptchaValidatorService validatorService, IOptions<DNTCaptchaOptions> options)
        {
            _validatorService = validatorService;
            _captchaOptions = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
        }

        [HttpGet]
        public IActionResult Login()
        {
            string[] banks = new string[] { "Union Bank of India", "Coorpation Bank" };
            ViewBag.BankList = new SelectList(banks, "BankName");
            return View();
        }

        [HttpPost]

        public IActionResult Login(LoginModel model)
        {
            if (!_validatorService.HasRequestValidCaptchaEntry(Language.English, DisplayMode.SumOfTwoNumbers))
            {
                this.ModelState.AddModelError(_captchaOptions.CaptchaComponent.CaptchaInputName, "Please enter the security code as a number.");
                return View(model);
            }
            return RedirectToAction("EmployeeMasterUpload", "Posting");
            // Save model
            //using (var httpClient = new HttpClient(_clientHandler))
            //{
            //    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            //    using (var response = await httpClient.PutAsync("https://localhost:5000/api/v1/Employee", content))
            //    {
            //        string apiResponse = await response.Content.ReadAsStringAsync();
            //        employee = JsonConvert.DeserializeObject<Employee>(apiResponse);

            //    }
            //}
            //return RedirectToAction(nameof(Login), new { name = model.Username });
        }
    }
}
