
using Agriculiture.BLL.Manager.TreeTypeManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();




//Add Connection string
builder.Services.AddDbContext<AgriContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
});
builder.Services.AddSwaggerGen(options =>
{
    options.MapType<DateOnly>(() => new Microsoft.OpenApi.Models.OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new Microsoft.OpenApi.Any.OpenApiString("0000-00-00")
    });
});


//Registers Repo
builder.Services.AddScoped<IAfforestationAgricultureAchievementRepo,
    AfforestationAgricultureAchievementRepo>();
builder.Services.AddScoped<ILocationNameRepo, LocationNameRepo>();
builder.Services.AddScoped<ITreeNameRepo, TreeNameRepo>();
builder.Services.AddScoped<ITreeTypeRepo, TreeTypeRepo>();
builder.Services.AddScoped<ITypeOfLocationRepo, TypeOfLocationRepo>();
builder.Services.AddScoped<IUserRepo, UserRepo>();

//Register Manager

builder.Services.AddScoped<IAfforestationAgricultureAchievementManager,
    AfforestationAgricultureAchievementManager>();

builder.Services.AddScoped<ITreeNameManager, TreeNameManager>();
builder.Services.AddScoped<ILocationManager, LocationManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<ITreeTypeManager, TreeTypeManager>();
builder.Services.AddScoped<ILocationTypeManager, LocationTypeManager>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();

