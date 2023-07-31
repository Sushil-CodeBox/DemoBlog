using DemoBlogCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllersWithViews();
//builder.Services.AddControllersWithViews()
//    .AddNewtonsoft(options =>
//    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
//);
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
);
//For Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(   
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
//For identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
               // .AddDefaultTokenProviders();


//Adding Athentication
builder.Services.AddAuthentication(options =>
{
    //options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
    //options.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken=true;
        options.RequireHttpsMetadata=false;
        options.TokenValidationParameters=new TokenValidationParameters()
        {
            ValidateIssuer=false,
            ValidateAudience= false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience =configuration["JWT:ValidateAudience"],
            ValidIssuer = configuration["JWT:ValidateIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
    });

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
var app = builder.Build();
if (app.Environment.IsDevelopment())
{

}
app.UseCors();

// Configure the HTTP request pipeline.
app.UseStaticFiles();

app.UseHttpsRedirection();

//app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
