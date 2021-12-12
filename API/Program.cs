using API.Infra;
using API.Mappers;
using API.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [Database]
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection(nameof(DatabaseSettings)));
builder.Services.AddSingleton<IDatabaseSettings>(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);
#endregion

#region [DI]
builder.Services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));
builder.Services.AddSingleton<NewsService>();
#endregion

#region [AutoMapper]
builder.Services.AddAutoMapper(typeof(EntityToViewModelMapping), typeof(ViewModelToEntityMapping));
#endregion

#region [Cors]
builder.Services.AddCors();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region [Cors]
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();

});
#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
