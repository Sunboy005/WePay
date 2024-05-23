using wepay.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureEmail(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureSwagger();

builder.Services.AddAuthentication();

builder.Services.ConfigureIdentity();

builder.Services.ConfigureJWT(builder.Configuration);

builder.Services.ConfigureRepositoryManager();

builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.ConfigureServiceMAnager();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
