
using Cryptocop.Software.Worker.Emails;
using Cryptocop.Software.Worker.Emails.Options;
using SendGrid;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<RabbitMqOptions>(builder.Configuration.GetSection("RabbitMq"));
builder.Services.Configure<SendGridOptions>(builder.Configuration.GetSection("SendGrid"));
// builder.Services.AddSendGrid(options =>
// {
//     options.ApiKey = builder.Configuration["SendGrid:ApiKey"];
// });
builder.Services.AddSingleton<ISendGridClient>(sp =>
{
    var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<SendGridOptions>>().Value;
    return new SendGridClient(options.ApiKey);
});
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
