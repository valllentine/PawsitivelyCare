using Microsoft.EntityFrameworkCore;
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

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddScoped<IBaseRepository<User, Guid>, BaseRepository<User, Guid>>();
builder.Services.AddScoped<IBaseRepository<Post, Guid>, BaseRepository<Post, Guid>>();

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(PostProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwaggerUI();
app.UseSwagger();

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthorization();

app.Run();
