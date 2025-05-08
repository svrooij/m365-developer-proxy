using dev_proxy_api;
using Microsoft.DevProxy.Abstractions;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<Worker>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddControllers();

if(args.Contains("--mock"))
{
    builder.Services.AddSingleton<IProxyLogger, MockLogger>();
}
else
{
    builder.Services.AddSingleton<IProxyLogger, ProxyLogger>();
}

if (args.Contains("xxx"))
{
    IPluginRegistration plugin = new MyPluginRegistration();
    plugin.Register(builder.Services);

    
}


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
