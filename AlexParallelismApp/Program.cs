using AlexParallelismApp;
using AlexParallelismApp.Extensions;
using AlexParallelismApp.Mappings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

SeedDataBase.Init(connectionString);

builder.Services.AddAutoMapper(typeof(XEntityVmMappingProfile), typeof(XEntityDtoMappingProfile),
    typeof(YEntityVmMappingProfile), typeof(YEntityDtoMappingProfile));

builder.Services.InitializeDataComponents(connectionString);

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();