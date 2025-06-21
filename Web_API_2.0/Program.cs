using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web_API_2._0.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RoadOfRussiaContext>(opts =>
opts.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddControllers();
builder.Services.AddRateLimiter(opts =>
{
    opts.AddFixedWindowLimiter("fixedWindow", fixOpts =>
    {
        fixOpts.PermitLimit = 1;
        fixOpts.QueueLimit = 0;
        fixOpts.Window = TimeSpan.FromSeconds(15);
    });
});
builder.Services.Configure<MvcNewtonsoftJsonOptions>(opts => {
    opts.SerializerSettings.NullValueHandling
    = Newtonsoft.Json.NullValueHandling.Ignore;
});
builder.Services.AddAuthentication();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = true,
            // строка, представляющая издателя
            ValidIssuer = AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience = true,
            // установка потребителя токена
            ValidAudience = AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime = true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true,
        };
    });
var app = builder.Build();

app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;
    var path = statusCodeContext.HttpContext.Request.Path;

    response.ContentType = "text/plain; charset=UTF-8";
    string mes = "";
    switch (response.StatusCode)
    {
        case 400:
        {
            mes = "неправильно сформирован запрос";
            break;
        }
        case 401:
        {
                mes = "вы не авторизованы";
                break;
        }
        case 403:
        {
                mes = "неправильные авторизационные данные";
                break;
        }
        case 404:
        {
                mes = path + " не найден";
                break;
        }

    }
    Errors newError = new Errors()
    {
        Timestamp = DateTime.Now.Ticks,
        Message = mes,
        ErrorCode = response.StatusCode + 1000
    };
    await response.WriteAsJsonAsync(newError);
});
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.MapControllers();

app.Map("/login/api/v1/SignIn", async (Employee emp, RoadOfRussiaContext db) =>
{
    Employee? employee = await db.Employees.FirstOrDefaultAsync(p => p.Surname == emp.Surname && p.Password == emp.Password);
    if (employee is null) {
        Errors newError = new Errors()
        {
            Timestamp = DateTime.Now.Ticks,
            Message = "неправильные авторизационные данные",
            ErrorCode = 1401
        };
        return Results.Json(newError);
    }
    var claims = new List<Claim> { new Claim(ClaimTypes.Surname, emp.Password) };
    // создаем JWT-токен
    var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
    audience: AuthOptions.AUDIENCE,
    claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = emp.Surname
    };
    return Results.Json(response);
});


app.MapGet("/", [Authorize]() => "Hello World!");

app.MapPost("/api/v1/Document/{id}/Comment", [Authorize] async (int id, Comment com, RoadOfRussiaContext db) =>
{
    Material? material = await db.Materials.FirstOrDefaultAsync(p => p.IdMaterial == id);
    if (material is null) return Results.NotFound();
    else
    {
        Comment newCom = new Comment
        {
            IdMaterial = id,
            CommentText = com.CommentText,
            DateCreated = com.DateCreated,
            DateUpdated = com.DateUpdated,
            AuthorOfComment = com.AuthorOfComment
        };
        db.Comments.Add(newCom);
        Material material1 = (await db.Materials.FirstOrDefaultAsync(p => p.IdMaterial == id))!;
        material1.Comments += 1;
        db.SaveChanges();
    }
    return Results.Ok();
});


app.Run();

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
