
using Cryptocop.Software.Worker.Payments;
using Cryptocop.Software.Worker.Payments.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
