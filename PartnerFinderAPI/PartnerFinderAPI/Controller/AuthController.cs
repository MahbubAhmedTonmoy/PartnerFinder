﻿

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PartnerFinderAPI.DTO;
using PartnerFinderAPI.JWTToken;
using PartnerFinderAPI.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PartnerFinderAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtGenerator _jwtGenerator;
        public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,
            SignInManager<AppUser> signInManager, IConfiguration config, ILogger<AuthController> logger,
             IJwtGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _config = config;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }

        [HttpPost("registration")]
        public async Task<IActionResult> Registration(UserForRegistrationDTO userRegistrationDto)
        {
            //role create
            if (!await _roleManager.RoleExistsAsync(Role.SuperAdmin))
            {
                await _roleManager.CreateAsync(new IdentityRole(Role.SuperAdmin));
            }
            if (!await _roleManager.RoleExistsAsync(Role.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(Role.User));
            }

            //create claim

            //create user
            var userForCreate = new AppUser
            {
                UserName = userRegistrationDto.UserName,
                Name = userRegistrationDto.UserName,
                Email = userRegistrationDto.Email
            };
            if (await _userManager.FindByEmailAsync(userRegistrationDto.Email) != null)
            {
                return StatusCode(400, $"{userRegistrationDto.Email} email already used");
            }
            if (userRegistrationDto.Role.Contains("Admin") || userRegistrationDto.Role.Contains("User"))
            {
                var createdUser = await _userManager.CreateAsync(userForCreate, userRegistrationDto.Password);

                // role assign
                if (createdUser.Succeeded)
                {
                    if (userRegistrationDto.Role.Contains("Admin"))
                    {
                        await _userManager.AddToRoleAsync(userForCreate, Role.SuperAdmin);
                    }
                    if (userRegistrationDto.Role.Contains("User"))
                    {
                        await _userManager.AddToRoleAsync(userForCreate, Role.User);
                        //  await _userManager.AddClaimAsync(userForCreate, new Claim("Create Role", "Create Role"));
                    }
                    else
                    {
                        return BadRequest("role is not matched");
                    }
                    return StatusCode(200, createdUser);
                }
                else
                {
                    return StatusCode(400, createdUser.Errors);
                }
            }

            return StatusCode(500, "Internal server error");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDTO)
        {
            try
            {
                var userExist = await _userManager.FindByEmailAsync(userForLoginDTO.Email);
                if (userExist == null)
                {
                    return BadRequest($"this {userForLoginDTO.Email} is not resisrered");
                }
                var result = await _signInManager.CheckPasswordSignInAsync(userExist, userForLoginDTO.Password, false);

                if (result.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(userExist);
                    string[] roleAssigned = role.ToArray();

                    return Ok(
                    
                        _jwtGenerator.CreateToken(userExist, roleAssigned)
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.StackTrace} {ex.Message}");
                throw;
            }
            return StatusCode(500, "Internal server error");
        }



    }
}
