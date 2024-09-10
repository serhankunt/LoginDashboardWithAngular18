using AutoMapper;
using FluentValidation.Results;
using Login.Server.DTOs;
using Login.Server.Models;
using Login.Server.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Login.Server.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController(UserManager<AppUser> userManager,
    IMapper mapper,
    JwtProvider jwtProvider) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto request, CancellationToken cancellationToken)
    {
        RegisterDtoValidator validator = new RegisterDtoValidator();
        ValidationResult validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            return BadRequest(new { Message = validationResult.Errors });
        }

        bool userNameIsExist = await userManager.Users.AnyAsync(p => p.UserName == request.UserName);
        if (userNameIsExist)
        {
            return BadRequest("Kullanıcı adı daha önce kullanıldı");
        }

        bool emailIsExist = await userManager.Users.AnyAsync(p => p.Email == request.Email);
        if (emailIsExist)
        {
            return BadRequest(new { Message = "Email adresi daha önce kullanıldı" });
        }

        AppUser user = mapper.Map<AppUser>(request);

        IdentityResult identityResult = await userManager.CreateAsync(user, request.Password);

        if (identityResult.Succeeded)
        {
            return Ok(new { Message = "Kullanıcı kaydı başarılı", Result = true });
        }

        return BadRequest(new { Message = "Kayıt esnasında bir hata ile karşılaşıldı" });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestDto request, CancellationToken cancellationToken)
    {
        var user = await userManager.Users
            .FirstOrDefaultAsync(p => p.UserName == request.UserNameOrEmail ||
                                      p.Email == request.UserNameOrEmail);
        if (user == null)
        {
            return BadRequest(new { Message = "Kullanıcı bulunamadı" });
        }

        var result = await userManager.CheckPasswordAsync(user, request.Password);

        if (!result)
        {
            return BadRequest(new { Message = "Kullanıcı girişi hatalı" });
        }

        return Ok(new { Message = "Kullanıcı girişi başarılı", JWT = jwtProvider.CreateToken(user) });
    }
}
