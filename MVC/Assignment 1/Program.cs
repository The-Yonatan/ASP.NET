using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Assignment_1.Data;
var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<Assignment_1Context>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("Assignment_1Context") ?? throw new InvalidOperationException("Connection string 'Assignment_1Context' not found.")));
builder.Services.AddDbContext<Assignment_1Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Assignment_1ContextLite") ?? throw new InvalidOperationException("Connection string 'Assignment_1Contextlite' not found.")));
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStatusCodePages();
app.Use(async (context, next) =>
{
    await next();
    if(context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/CustomNotFound";
        await next();
    }
});
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
