using CashFlow.Api;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices();

WebApplication app = builder.Build();
app.ConfigurePipeline();
app.Run();