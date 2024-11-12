using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using soft20181_starter.Models;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();

IronPdf.License.LicenseKey = "IRONSUITE.T0403718.MY.NTU.AC.UK.5246-1E1EC9EB9F-HHFD622IQSWBB2-2UIEWBB7IX4A-NW3E4KMXSFAT-5FD5EWXA7OTM-5CAZFY7L6D3O-JJYDSHPTKOVB-U3OWVH-TD2TYK7FRX6MEA-DEPLOYMENT.TRIAL-ZXYV7T.TRIAL.EXPIRES.02.MAY.2024";

var Configuration = builder.Configuration;
builder.Services.AddDbContext<EventAppDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("Default")));
/*
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<EventAppDbContext>();*/

builder.Services.AddIdentity<User, IdentityRole>()
.AddDefaultTokenProviders()
.AddDefaultUI()
.AddEntityFrameworkStores<EventAppDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
