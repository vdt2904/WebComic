var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
// Add services to the container.
builder.Services.AddControllersWithViews();
//redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis-14248.c295.ap-southeast-1-1.ec2.redns.redis-cloud.com:14248,password=vrzf1vvbvcqUiTE7I7sFoC5oGdLDzkT9";
    // Thay đổi nếu bạn có cấu hình Redis khác
    options.InstanceName = "SampleInstance";
});
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
