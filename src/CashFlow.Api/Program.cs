using CashFlow.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices(builder.Configuration);

WebApplication app = builder.Build();
app.ConfigurePipeline();
app.Run();