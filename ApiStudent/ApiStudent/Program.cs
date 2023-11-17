using Microsoft.EntityFrameworkCore;
using ApiStudent.Data;
using ApiStudent.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiStudentContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ApiStudentContext") ?? throw new InvalidOperationException("Connection string 'ApiStudentContext' not found.")));

var key = "This is the demo key";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsApi",
        builder => builder.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsApi");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
