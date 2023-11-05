using Airport.MeasureService.WebApi.Extensions;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES
builder.Services.AddIataCodeOperationsServices();
builder.Services.AddWebIataCodeRepository(builder.Configuration["Repository:Url"]);
builder.Services.AddInMemoryCacheForIataCodeRepository();
//

// HELPERS 
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//

// API versioning
builder.Services.AddApiVersioning(options =>
    {
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    })
    .AddMvc();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();