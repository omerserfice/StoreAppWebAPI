using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Business.Abstract;
using Store.Business.Concrete;
using Store.DAL.Context;
using Store.DAL.LoginSecurity.Encryption;
using Store.DAL.LoginSecurity.Helper;
using Store.DAL.MongoEntity;
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


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy("CorsPolicy", builder =>
	builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
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

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
