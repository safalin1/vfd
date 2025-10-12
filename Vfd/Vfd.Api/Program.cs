using System.IO.Ports;
using Vfd.Api.CommandSetTables;
using Vfd.Api.Services;
using Vfd.Api.Services.Displays;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

switch (builder.Configuration.GetValue<string>("Display:Type"))
{
    case "Serial":
        builder.Services
            .AddSingleton<SerialPort>(_ =>
                new SerialPort(builder.Configuration.GetValue<string>("Display:Serial:ComPort"))
                {
                    WriteTimeout = builder.Configuration.GetValue<int>("Display:Serial:WriteTimeout"),
                    BaudRate = builder.Configuration.GetValue<int>("Display:Serial:BaudRate")
                })
            .AddSingleton<IDisplayHardware, SerialPortDisplayHardware>();
        
        break;
    
    default:
        throw new NotImplementedException("Display Type not supported");
}

switch (builder.Configuration.GetValue<string>("Display:CommandSet"))
{
    case "EPSON":
        builder.Services.AddSingleton<IVfdCommandSetTable, EpsonCommandSetTable>();
        break;
    
    default:
        throw new NotImplementedException($"CommandSet not supported");
}

builder.Services.AddHostedService<DisplayBufferBackgroundService>();
builder.Services.AddSingleton<DisplayBuffer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();