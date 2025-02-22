using JOWheels.Models;
using JOWheels.Models.Repository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<JOWheelsDbContext>(
    options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefultConnections")));


builder.Services.AddScoped<ICRUDAction<User>, UserAction>();
builder.Services.AddScoped<ICRUDAction<Governorate>, GovernorateAction>();
builder.Services.AddScoped<ICRUDAction<Region>, RegionAction>();
builder.Services.AddScoped<ICRUDAction<Brand>, BrandAction>();
builder.Services.AddScoped<ICRUDAction<Body>, BodyAction>();
builder.Services.AddScoped<ICRUDAction<Transmission>, TransmissionAction>();
builder.Services.AddScoped<ICRUDAction<Fule>, FuleAction>();
builder.Services.AddScoped<ICRUDAction<Color>, ColorAction>();
builder.Services.AddScoped<ICRUDAction<Seat>, SeatAction>();
builder.Services.AddScoped<ICRUDAction<Condition>, ConditionAction>();
builder.Services.AddScoped<ICRUDAction<Mileage>, MileageAction>();
builder.Services.AddScoped<ICRUDAction<Custom>, CustomAction>();
builder.Services.AddScoped<ICRUDAction<License>, LicenseAction>();
builder.Services.AddScoped<ICRUDAction<Insurance>, InsuranceAction>();
builder.Services.AddScoped<ICRUDAction<Payment>, PaymentAction>();
builder.Services.AddScoped<ICRUDAction<Year>, YearAction>();
builder.Services.AddScoped<ICRUDAction<Car>, CarAction>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
