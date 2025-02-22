using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider.Basic.Controller;

[ApiController]
public class AccountController 
{
	[HttpGet("api/account/register")]
	public async Task Register(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
	{
		try
		{

			var user = new IdentityUser
			{
				UserName = "alireza.Doostdar",
				Email = "Alireza.doostdar@gmail.com",
				EmailConfirmed = true
			};
			await userManager.CreateAsync(user, "Ar.11ssfs");
			await roleManager.CreateAsync(new IdentityRole("Admin"));
			await userManager.AddToRoleAsync(user, "Admin");
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex);
			throw;
		}
	}

	[HttpGet("api/account/login")]
	public async Task<string> Login(SignInManager<IdentityUser> signInManager)
	{
		var result = await signInManager.PasswordSignInAsync("alireza.Doostdar", "Ar.11ssfs", true, false);
        if (result.Succeeded)
        {
			return await Task.FromResult("success");
        }

		return await Task.FromResult("Fail");
    }

	[Authorize(Roles = "Admin")]
	[HttpGet("api/account/protected")]
	public async Task<string> ProtectedAction(SignInManager<IdentityUser> signInManager)
	{
		return await Task.FromResult("Protected");
	}

}
