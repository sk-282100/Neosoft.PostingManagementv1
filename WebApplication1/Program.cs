using PostingManagement.UI.Services.ExcelUploadService.Contracts;
using PostingManagement.UI.Services.ExcelUploadService;
using PostingManagement.UI.Services.HomeService;
using PostingManagement.UI.Services.HomeService.Contracts;
using DNTCaptcha.Core;
using PostingManagement.UI.Middleware;
using PostingManagement.UI.Services.LoginService.Contracts;
using PostingManagement.UI.Services.LoginService;
using PostingManagement.UI.Services.AccountServices.Contracts;
using PostingManagement.UI.Services.AccountServices;
using PostingManagement.UI.Services.RoleService;
using PostingManagement.UI.Services.TransferService.Contracts;
using PostingManagement.UI.Services.TransferService;
using PostingManagement.UI.Services.JobFamilyServices;
using PostingManagement.UI.Services.TriggerServices.Contracts;
using PostingManagement.UI.Services.TriggerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IHomeService, Homeservice>();
builder.Services.AddScoped<IExcelUploadService, ExcelUploadService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IAccountService,AccountService>();
builder.Services.AddScoped<IRoleService,RoleService>(); 
builder.Services.AddScoped<IExcelUploadService, ExcelUploadService>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddScoped<IJobFamilyService, JobFamilyService>();
builder.Services.AddScoped<ITriggerService, TriggerService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<HttpClientHandler>();
builder.Services.AddSession();
builder.Services.AddDNTCaptcha(options =>
{
    options.UseCookieStorageProvider(SameSiteMode.Strict)
    //.UseCustomFont(Path.Combine(_env.WebRootPath, "fonts", "IRANSans(FaNum)_Bold.ttf")) // This is optional
    .AbsoluteExpiration(minutes: 7)
    .ShowThousandsSeparators(false)
    .WithNoise(pixelsDensity: 25, linesCount: 3)
    .WithEncryptionKey("This is my secure key!")
    .InputNames(// This is optional. Change it if you don't like the default names.
        new DNTCaptchaComponent
        {
            CaptchaHiddenInputName = "DNTCaptchaText",
            CaptchaHiddenTokenName = "DNTCaptchaToken",
            CaptchaInputName = "DNTCaptchaInputText"
        })
    .Identifier("dntCaptcha")// This is optional. Change it if you don't like its default name.
    ;
}); var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCustomExceptionHandler();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
