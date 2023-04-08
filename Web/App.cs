var builder = WebApplication.CreateBuilder(args);

// IMvcBuilder
builder.Services
    .AddControllers()
    .ConfigureJsonSerializerToNotWriteNull();

// IServiceCollection
builder.Services

#if !RELEASE
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
#endif

    .AddResponseCompression();

var app = builder.Build();

#if !RELEASE
if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}
#endif

// IApplicationBuilder
app
    .UseHttpsRedirection()
    .UseAuthorization()
    .UseResponseCompression();

app.MapControllers();

app.Run();
