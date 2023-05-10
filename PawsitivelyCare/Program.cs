using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PawsitivelyCare.BLL.Common.Auth;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.BLL.Services.Realizations;
using PawsitivelyCare.DAL.Contexts;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.Repositories.Interfaces;
using PawsitivelyCare.DAL.Repositories.Realizations;
using PawsitivelyCare.Mappings;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<PawsitivelyCareDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("PawsitivelyCareDBConnection")));

builder.Services.AddDbContextFactory<PawsitivelyCareDbContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("PawsitivelyCareDBConnection")));

#region "Services"

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();

#endregion Region


#region "Repositories"

builder.Services.AddScoped<IBaseRepository<User, Guid>, BaseRepository<User, Guid>>();
builder.Services.AddScoped<IBaseRepository<Post, Guid>, BaseRepository<Post, Guid>>();

#endregion Region


#region "MapperProfiles"

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(PostProfile));

#endregion Region


#region "Authentication"

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("Auth"));
var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = authOptions.Audience,

            ValidateLifetime = true,

            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(), //HS256
            ValidateIssuerSigningKey = true,
        };
    });

#endregion Region


#region "Swagger"

builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "jwtTocken_Auth_Api",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token with Bearer format"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
        }
    });
});

#endregion Region


#region "Cors"

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .WithMethods("PUT");
    });
});

#endregion Region


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwaggerUI();
app.UseSwagger();

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
