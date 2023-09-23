using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using Store.Business.Abstract;
using Store.Business.Concrete;
using Store.DAL.Context;
using Store.DAL.LoginSecurity.Encryption;
using Store.DAL.LoginSecurity.Helper;
using Store.DAL.MongoEntity;
using Store.WebAPI.Extensions;
//using Store.DAL.LoginSecurity.Entity.TokenOptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<StoreAppDbContext>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenHelper,TokenHelper>();
builder.Services.AddScoped<IProductCoverImageService,ProductCoverImageService>();
builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();
builder.Services.AddSingleton<ILoggerService, LoggerManager>();

builder.Services.AddSwaggerGen(s =>
{
	s.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "StoreApp",
		Version = "v1",
		Description = "StoreApp Web API Uygulamasý",
		TermsOfService = new Uri("https://storeapp.com/"),
		Contact = new OpenApiContact
		{
			Name = "Ömer Serfice",
			Email = "srfcomr@gmail.com",
			Url = new Uri("https://www.omerserfice.com.tr")
		}
	}) ;
	s.SwaggerDoc("v2", new OpenApiInfo { Title = "StoreApp", Version = "v2" });

	s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "PLace to add JWT with Bearer",
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});

	s.AddSecurityRequirement(new OpenApiSecurityRequirement()
	{
		{
			new OpenApiSecurityScheme
		{
			Reference = new OpenApiReference
			{
				Type = ReferenceType.SecurityScheme,
				Id = "Bearer"
			},
			Name = "Bearer"
		},
		new List<string>()
		}
	});

});

builder.Services.Configure<MongoOptions>(builder.Configuration.GetSection("MongoOptions"));

//JWT AYARI
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<Store.DAL.LoginSecurity.Entity.TokenOptions>();
builder.Services.Configure<Store.DAL.LoginSecurity.Entity.TokenOptions>(builder.Configuration.GetSection("TokenOptions"));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtOption =>
{
	jwtOption.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateAudience = true,
		ValidateIssuer = true,
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		ValidIssuer = tokenOptions.Issuer,
		ValidAudience = tokenOptions.Audience,
		IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
		ClockSkew = TimeSpan.Zero
	};
});


LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();




builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", builder =>
	builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerService>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(s =>
	{
		s.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreApp v1");
		s.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreApp v2");
	});
}

if (app.Environment.IsProduction())
{
	app.UseHsts();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
