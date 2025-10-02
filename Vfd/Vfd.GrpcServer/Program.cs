using Microsoft.OpenApi.Models;
using Vfd.GrpcServer;
using Vfd.GrpcServer.Services;
using Vfd.GrpcServer.Services.Displays;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc().AddJsonTranscoding();;

builder.Services.AddSingleton<IDisplayHardware, HPVfd220DisplayHardware>();
builder.Services.AddHostedService<DisplayBufferBackgroundService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo { Title = "VFD", Version = "v1" });
});
builder.Services.AddGrpcSwagger();

var app = builder.Build();

app.UseSwagger();
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "VFD API V1");
    });
}

app.MapGrpcService<DisplayBufferGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

await app.RunAsync();