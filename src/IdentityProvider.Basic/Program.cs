using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication()
				.AddCookie(option =>
				{
					option.Cookie.Name = "MehrAccounting.com";
				});

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
app.UseAuthentication();



app.MapGet("/Login", async (HttpContext httpContext) =>
{
	var claims = new List<Claim>()
	{
		new Claim(ClaimTypes.Name, "Alireza")
	};
	var identityClaims = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
	var userPrincipal = new ClaimsPrincipal(identityClaims);

	await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
	return Results.Ok("Thank You");
});

app.MapGet("/UserName", (HttpContext httpContext, IDataProtectionProvider provider) =>
{
	return httpContext.User.FindFirst("username").Value;
});

app.Run();

public class ApiKeyAuthenticationOptions: AuthenticationSchemeOptions
{
	public string Head
}
