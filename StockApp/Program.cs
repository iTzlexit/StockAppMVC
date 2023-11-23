using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);



// Services 
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions")); // binds the configuration section named "TradingOptions" from your app settings to a TradingOptions class.

builder.Services.AddSingleton<IFinhubService, FinhubService>();

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.



app.UseStaticFiles();

app.UseRouting();


app.MapControllers();



app.Run();
