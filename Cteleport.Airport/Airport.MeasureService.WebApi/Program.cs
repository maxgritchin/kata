using Airport.Measure.Domain.Repositories;
using Airport.Measure.Domain.Services;
using Airport.Measure.Implementation.Repositories;
using Airport.Measure.Implementation.Repositories.Cache;
using Airport.Measure.Implementation.Repositories.Web;
using Airport.Measure.Implementation.Repositories.Web.Http;
using Airport.Measure.Implementation.Repositories.Web.Json;
using Airport.Measure.Implementation.Services.DistanceCalculators;
using Airport.Measure.Implementation.Services.Validators;
using Asp.Versioning;

var builder = WebApplication.CreateBuilder(args);

// ADD SERVICES
builder.Services.AddSingleton<IIataCodeValidator, IataCodeValidator>();
builder.Services.AddSingleton<IRepositoryCache, InMemoryRepositoryCache>();
builder.Services.AddSingleton<IJsonParser, CteleportIataJsonParser>();
builder.Services.AddSingleton<IDistanceCalculator, HaversineFormula>();

builder.Services.AddSingleton<IHttpGet>(_ =>
{
    var url = builder.Configuration["Repository:Url"];
    if (string.IsNullOrWhiteSpace(url))
        throw new ArgumentException("Repository base URL is not provided. Please, update configuration");

    return new HttpGetService(url);
});

builder.Services.AddSingleton<IAirportCodesRepository>(provider =>
{
    var codesRepository = new WebIataCodeRepository(
        provider.GetService<IHttpGet>(),
        provider.GetService<IJsonParser>());
    var cache = provider.GetRequiredService<IRepositoryCache>();

    return new CachingIataCodeRepository(codesRepository, cache);
});
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