using Microsoft.EntityFrameworkCore;
using System.Configuration;
using WebComic.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ComicDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnect")));
//swager
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});
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
//swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    // Không thiết lập RoutePrefix hoặc thiết lập RoutePrefix = "swagger"
    // c.RoutePrefix = "swagger"; (Tùy chọn, có thể bỏ qua)
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
