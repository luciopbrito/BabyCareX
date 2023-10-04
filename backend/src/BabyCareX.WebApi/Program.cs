using BabyCareX.Application;
using BabyCareX.Application.Contracts;
using BabyCareX.Repository;
using BabyCareX.Repository.Context;
using BabyCareX.Repository.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext 
var configuration = builder.Configuration;
builder.Services.AddDbContext<BabyCareXContext>(
    context => context.UseSqlite(configuration.GetConnectionString("Default"))
);

builder.Services.Configure<ApiBehaviorOptions>(
    op => op.SuppressModelStateInvalidFilter = true
);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(
    e => e.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
).AddDataAnnotationsLocalization();

// Add Repository
builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<IBabaRepository, BabaRepository>();
builder.Services.AddScoped<IBabaCapacitiesRepository, BabaCapacitiesRepository>();
builder.Services.AddScoped<IBabaCoursesRepository, BabaCoursesRepository>();
builder.Services.AddScoped<IBabaProvideServicesRepository, BabaProvideServicesRepository>();
builder.Services.AddScoped<IChildrenRepository, ChildrenRepository>();
builder.Services.AddScoped<IKindNanniesRepository, KindNanniesRepository>();
builder.Services.AddScoped<ISchedulesRepository, SchedulesRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();

// Add Services
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IBabaService, BabaService>();
builder.Services.AddScoped<IBabaCapacitiesService, BabaCapacitiesService>();
builder.Services.AddScoped<IBabaCoursesService, BabaCoursesService>();
builder.Services.AddScoped<IBabaProvideSService, BabaProvideSService>();
builder.Services.AddScoped<IChildrenService, ChildrenService>();
builder.Services.AddScoped<IKindNanniesService, KindNanniesService>();
builder.Services.AddScoped<ISchedulesService, SchedulesService>();
builder.Services.AddScoped<IStatusService, StatusService>();

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
