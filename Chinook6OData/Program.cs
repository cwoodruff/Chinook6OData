using System.Reflection;
using Chinook6OData;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.AspNetCore.OData.NewtonsoftJson;
using Microsoft.AspNetCore.OData.Routing.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

var model = GetEdmModel();

builder.Services.AddControllers()
    .AddODataNewtonsoftJson()
    .AddOData(options =>
    {
        var defaultBatchHandler = new DefaultODataBatchHandler();
        defaultBatchHandler.MessageQuotas.MaxNestingDepth = 2;
        defaultBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;
        defaultBatchHandler.MessageQuotas.MaxReceivedMessageSize = 100;
        
        options.Select()
            .Filter()
            .Count()
            .OrderBy()
            .Expand()
            .SetMaxTop(100);
        options.AddRouteComponents("api/", model);
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ChinookContext>(opt=> 
    opt.UseSqlServer("Server=.;Database=Chinook;Trusted_Connection=True;Application Name=Chinook6OData"));
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Chinook OData API",
        Description = "A simple example ASP.NET Core Web API",
    });
    
    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (apiDesc.RelativePath.Contains('$')) return false;
        if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

        var routeAttrs = methodInfo.DeclaringType
            .GetCustomAttributes(true)
            .OfType<ODataRouteComponentAttribute>();
        var versions = routeAttrs.Select(i => i.RoutePrefix.Split('/').Last());
        return versions.Any(v => string.Equals(v, docName, StringComparison.CurrentCultureIgnoreCase));
    });

    options.CustomSchemaIds(type => type.FullName);
    options.OperationFilter<ODataOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseODataRouteDebug();
}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;

    options.DefaultModelsExpandDepth(-1); // hides schemas dropdown
    options.EnableTryItOutByDefault();
});

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();

IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Album>("Albums");
    builder.EntitySet<Customer>("Customers");
    builder.EntitySet<Employee>("Employees");
    builder.EntitySet<Genre>("Genres");
    builder.EntitySet<Invoice>("Invoices");
    builder.EntitySet<InvoiceLine>("InvoiceLines");
    builder.EntitySet<MediaType>("MediaTypes");
    builder.EntitySet<Artist>("Artists");
    builder.EntitySet<Playlist>("Playlists");
    builder.EntitySet<Track>("Tracks");
    return builder.GetEdmModel();
}