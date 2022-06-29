using Alively.Core.Entities;
using Alively.Core.Repositories;
using Alively.Core.Services;
using Alively.Infrastructure.Data;
using Alively.Infrastructure.Repositories;
using Alively.Infrastructure.Services;
using Alivley.Api.ConfigurationExtensions;
using Alivley.Api.DTOs;
using Alivley.Api.Profiles;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<AlivelyDbContext>(options =>
//    options.UseSqlServer(
//        builder.Configuration.GetConnectionString("DefaultConnection")
//    )
//);

var config = new MapperConfiguration(config =>
    config.AddProfile<UserProfile>()
);

builder.Services.AddDbContext<AlivelyDbContext>();
builder.Services.AddScoped<DbContext, AlivelyDbContext>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IManageUserService, ManageUserService>();
builder.Services.AddScoped<IManageAccountService, ManageAccountService>();
builder.Services.AddAutoMapper(typeof(Program));

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
