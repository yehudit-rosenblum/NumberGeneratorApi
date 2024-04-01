using NumberGeneratorApi.Application.Interfaces;
using NumberGeneratorApi.Application.Services;
using NumberGeneratorApi.Extensions;
using NumberGeneratorApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<INumberGeneratorService, NumberGeneratorService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddCorsConfiguration();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    
});
builder.Services.AddHttpContextAccessor();


builder.Services.AddSession(options =>
{
    // Other options...
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use only if your sites are served over HTTPS
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseCors("AllowSpecificOrigins");
app.UseAuthorization();
app.UseSession();
app.MapControllers();

app.Run();
