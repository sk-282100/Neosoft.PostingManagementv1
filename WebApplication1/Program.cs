using PostingManagement.UI.Services.ExcelUploadService.Contracts;
using PostingManagement.UI.Services.ExcelUploadService;
using PostingManagement.UI.Services.HomeService;
using PostingManagement.UI.Services.HomeService.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IHomeService, Homeservice>();
builder.Services.AddScoped<IExcelUploadService, ExcelUploadService>();
//builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<HttpClientHandler>();
var app = builder.Build();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
