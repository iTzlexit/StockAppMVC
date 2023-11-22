var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.



app.UseStaticFiles();

app.UseRouting();


app.MapControllers();



app.Run();
