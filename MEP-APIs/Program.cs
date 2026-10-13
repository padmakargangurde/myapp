using Infrastructure.DIContainers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string AngularDevCorsPolicy = "AllowAll";

builder.Services.AddCors(options =>
{
    options.AddPolicy(AngularDevCorsPolicy, policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
//if (!builder.Environment.IsDevelopment())
//{
//    builder.Configuration.AddSystemsManager(
//        path: "/ifieldsmart/prod",
//        optional: false,
//        reloadAfter: TimeSpan.FromMinutes(30)
//    );
//}
builder.Services.AddControllers()
    .AddJsonOptions(o =>
        o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Everything data-related lives in Infrastructure.DIContainers,
// so this file stays short as the project grows.
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline. 

    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseCors(AngularDevCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();