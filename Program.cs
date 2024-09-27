using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LibraryAPIs.Data;
using Microsoft.AspNetCore.Identity;
using LibraryAPIs.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;


namespace Library_API;

public class Program
{
    public static async Task Main(string[] args)
    {
        LibraryAPIsContext _context;
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;
        IdentityRole identityRole;
        ApplicationUser applicationUser;

        var builder = WebApplication.CreateBuilder(args);

        // Configure DbContext with SQL Server
        builder.Services.AddDbContext<LibraryAPIsContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryAPIsContext")
            ?? throw new InvalidOperationException("Connection string 'LibraryAPIsContext' not found.")));

        // Configure Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<LibraryAPIsContext>()
            .AddDefaultTokenProviders();

        // Configure JWT Authentication
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                ValidAudience = builder.Configuration["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
            };
        });

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        // Configure Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
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

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        // Auto migrate
        using (IServiceScope scope = app.Services.CreateScope())
        {
            IServiceProvider scopedProvider = scope.ServiceProvider;

            _context = scopedProvider.GetRequiredService<LibraryAPIsContext>();

            await _context.Database.MigrateAsync();
        }


        //Admin User Config.
        IServiceProvider serviceProvider = app.Services.CreateScope().ServiceProvider;
        _context = serviceProvider.GetRequiredService<LibraryAPIsContext>();
        _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        

        if (await _roleManager.FindByNameAsync("Admin") == null)
        {
            identityRole = new IdentityRole("Admin");
            await _roleManager.CreateAsync(identityRole);
        }
        if (await _userManager.FindByNameAsync("Admin") == null)
        {
            applicationUser = new ApplicationUser { UserName = "Admin" };
            await _userManager.CreateAsync(applicationUser, "Admin123!");
            await _userManager.AddToRoleAsync(applicationUser, "Admin");
        }

        app.Run();

    }
}