using BasedBaunApi.Models;
using BasedBaunApi.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasedBaunApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ItemContext _context;
    private readonly TokenService _tokenService;
    private readonly UserManager<IdentityUser> _userManager;

    public AuthController(UserManager<IdentityUser> userManager, ItemContext context, TokenService tokenService)
    {
        _userManager = userManager;
        _context = context;
        _tokenService = tokenService;
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register(Auth.Register register)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _userManager.CreateAsync(
            new IdentityUser { UserName = register.Username, Email = register.Email },
            register.Password
        );
        if (result.Succeeded) return NoContent();
        foreach (var error in result.Errors) ModelState.AddModelError(error.Code, error.Description);
        return BadRequest(ModelState);
    }

    [Route("login")]
    [HttpPost]
    public async Task<ActionResult<Auth.LoginResponse>> Authenticate([FromBody] Auth.Login request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var managedUser = await _userManager.FindByEmailAsync(request.Email);
        if (managedUser == null) return BadRequest("Email does not exist.");
        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);
        if (!isPasswordValid) return BadRequest("Incorrect password.");
        var userInDb = _context.Users.FirstOrDefault(u => u.Email == request.Email);
        if (userInDb is null)
            return Unauthorized();
        var accessToken = _tokenService.CreateToken(userInDb);
        await _context.SaveChangesAsync();
        return Ok(new Auth.LoginResponse
        {
            Username = userInDb.UserName,
            Email = userInDb.Email,
            Token = accessToken
        });
    }
}