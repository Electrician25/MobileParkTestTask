using MobileParkTestTask.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting();
builder.Services.AddControllers();
builder.AddApplicationContext();
builder.Services.AddCategoryCrudServices();
builder.Services.AddRazorPages();

var app = builder.Build();

app.MapControllers();
app.UseRouting();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapRazorPages();
app.Run();