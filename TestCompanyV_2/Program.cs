using TestCompanyV_2.DataAccess;
using TestCompanyV_2.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Injections
builder.Services.AddSingleton<IDAEmployee, DAEmployee>();
builder.Services.AddSingleton<IDEmployee, DEmployee>();
#endregion

#region swagger
builder.Services.AddSwaggerGen(m =>
{
    m.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Test company", Version = "v1" });
});
#endregion


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

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test company");
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
