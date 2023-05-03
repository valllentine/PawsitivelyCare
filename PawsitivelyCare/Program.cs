using Microsoft.EntityFrameworkCore;
using PawsitivelyCare.BLL.Services.Interfaces;
using PawsitivelyCare.BLL.Services.Realizations;
using PawsitivelyCare.DAL.Contexts;
using PawsitivelyCare.DAL.Entities;
using PawsitivelyCare.DAL.Repositories.Interfaces;
using PawsitivelyCare.DAL.Repositories.Realizations;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<PawsitivelyCareDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("PawsitivelyCareDBConnection")));

builder.Services.AddDbContextFactory<PawsitivelyCareDbContext>(
            options => options.UseSqlServer($"name={builder.Configuration.GetConnectionString("PawsitivelyCareDBConnection")}"));

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBaseRepository<User, int>, BaseRepository<User, int>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();
app.UseSwaggerUI();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger(x => x.SerializeAsV2 = true);
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Run();
