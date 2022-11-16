﻿using Microsoft.AspNetCore.Mvc;

namespace PostingManagement.UI.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [Route("ErrorHandler/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            _logger.LogInformation("HttpStatusCode action method initiated");
            var statusCodeResult = HttpContext.Response.StatusCode;
            switch (statusCode)
            {
                case 401:
                    return RedirectToAction("Error401");
                    break;
                case 404:
                    return RedirectToAction("Error404");
                    break;
                case 500:
                    return RedirectToAction("Error500");
                    break;
                case 503:
                    return RedirectToAction("Error503");
                    break;
            }
            return View("Default");
        }
        public IActionResult Eror401()
        {
            _logger.LogInformation("Error401 action method initiated");
            return View("Error401");
        }
        public IActionResult Eror404()
        {
            _logger.LogInformation("Error404 action method initiated");
            return View("Error404");
        }
        public IActionResult Eror500()
        {
            _logger.LogInformation("Error405 action method initiated");
            return View("Error500");
        }
        public IActionResult Error503()
        {
            _logger.LogInformation("Error action method initiated");
            return View("Error503");
        }
        public IActionResult Default()
        {
            _logger.LogInformation("Default action method initiated");
            return View("Default");
        }
    }
}
