using dev_proxy_worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<Worker>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
