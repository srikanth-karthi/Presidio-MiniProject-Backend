
using Job_Portal_Application.Context;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository;
using Job_Portal_Application.Repository.CompanyRepos;
using Job_Portal_Application.Repository.SkillRepos;
using Job_Portal_Application.Repository.UserRepos;
using Job_Portal_Application.Services;
using Job_Portal_Application.Services.CompanyService;
using Job_Portal_Application.Services.UsersServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Job_Portal_Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:JWT"]))
                    };
                });

            #region contexts

            builder.Services.AddDbContext<JobportalContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

            #endregion

            builder.Services.AddDbContext<JobportalContext>();
            builder.Services.AddScoped<IAuthorizeService, AuthorizeService>();
            builder.Services.AddScoped<ITokenService, TokenServices>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IEducationService, EducationService>();
            builder.Services.AddScoped<IAreasOfInterestService, AreasOfInterestService>();
            builder.Services.AddScoped<IExperienceService, ExperienceService>();
            builder.Services.AddScoped<ICompanyService, CompanyService>();
            builder.Services.AddScoped<IJobService, JobService>();
            builder.Services.AddScoped<ISkillService,SkillService > ();
            builder.Services.AddScoped<IJobActivityService, JobActivityService>();
            builder.Services.AddScoped<ITitleService, TitleService>();
            builder.Services.AddScoped<IUserSkillsService, UserSkillsService>();



            builder.Services.AddScoped<IRepository<Guid, Title>, TitleRepository>();
            builder.Services.AddScoped<IJobSkillsRepository, JobSkillsRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IEducationRepository, EducationRepository>();
            builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
            builder.Services.AddScoped<IAreasOfInterestRepository, AreasOfInterestRepository>();
            builder.Services.AddScoped <IJobSkillsRepository, JobSkillsRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IJobRepository, JobRepository>();
            builder.Services.AddScoped<IJobActivityRepository, JobActivityRepository>();
            builder.Services.AddScoped<IUserSkillsRepository, UserSkillsRepository>();



            builder.Services.AddScoped<IRepository<Guid, Title>, TitleRepository>();
            builder.Services.AddScoped<IRepository<Guid, UserSkills>, UserSkillsRepository>();
            builder.Services.AddScoped<IRepository<Guid, Skill>, SkillRepository>();



    

            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
