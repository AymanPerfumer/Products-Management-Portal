using Application.Extensions;
using Persistence;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddApplicationAuthentication(builder.Configuration.GetSection("JWT").Get<JWTConfiguration>());
builder.Services.AddApplicationServices();
builder.Services.AddDirectoryBrowser();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseStaticFiles();
app.MigrateDatabase();
app.Run();
