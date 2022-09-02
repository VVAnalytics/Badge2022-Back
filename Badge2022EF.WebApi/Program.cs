using Badge2022EF.WebApi.JWT_Authentication.JWTWebAuthentication.Repository;
using Badge2022EF.WebApi.JWT_Authentication;
using Badge2022EF.DAL;
using Microsoft.EntityFrameworkCore;
using Badge2022EF.DAL.Repositories;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using Badge2022EF.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
	o=>{
		o.AddPolicy(name: "corsRoute", d =>
										  {
											  d.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
										  });
	});

builder.Services.AddScoped<IJWTManagerRepository, JWTManagerRepository>();
builder.Services.AddScoped<CoursRepository>();
builder.Services.AddScoped<ExamenRepository>();
builder.Services.AddScoped<FormationsRepository>();
builder.Services.AddScoped<NotesElevesRepository>();
builder.Services.AddScoped<PersonneRepository>();
builder.Services.AddScoped<RoleRepository>();

builder.Services.AddDbContext<Badge2022Context>(o => { o.UseSqlServer(builder.Configuration.GetConnectionString("Badge2022Works"));
                                                       o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                                                     }
);

builder.Services.AddIdentity<PersonneEntity, RoleEntity>(
                options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 5;
                    options.Password.RequiredUniqueChars = 1;
                })
                .AddRoles<RoleEntity>()
                .AddEntityFrameworkStores<Badge2022Context>()
                .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Badge2022", Version = "v1" });

    OpenApiSecurityScheme securitySchema = new()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        }
    };
    c.AddSecurityDefinition("Bearer", securitySchema);
    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securitySchema, new[] { "Bearer" } }
    };
    c.AddSecurityRequirement(securityRequirement);
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("corsRoute");

//app.UseAuthentication();
app.UseMiddleware<JwtMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
