using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCognitoIdentity();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        //                   https://cognito-idp.{region}.amazonaws.com/{user-pool-id}
        options.Authority = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_Vol2qyQFn";
        options.TokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", [Authorize] () =>
{
    return Results.Ok(true);
});

app.Run();