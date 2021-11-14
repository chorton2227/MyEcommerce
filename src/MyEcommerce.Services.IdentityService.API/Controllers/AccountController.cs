namespace MyEcommerce.Services.IdentityService.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using MyEcommerce.Services.IdentityService.API.Configurations;
    using MyEcommerce.Services.IdentityService.API.Dtos;
    using MyEcommerce.Services.IdentityService.API.Models;
    using MyEcommerce.Services.IdentityService.API.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IEmailService _emailService;

        private readonly IOptionsMonitor<JwtConfig> _jwtConfig;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailService emailService,
            IOptionsMonitor<JwtConfig> jwtConfig
        ) {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _jwtConfig = jwtConfig;
        }

        [HttpPost("ResetPassword", Name=nameof(ResetPassword))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ResetPasswordResponseDto> ResetPassword(
            [FromBody] ResetPasswordRequestDto request
        )
        {
            // Validate confirm password
            if (request.Password != request.ConfirmPassword)
            {
                return new ResetPasswordResponseDto {
                    Success = false,
                    FieldErrors = new List<FieldErrorDto> {
                        new FieldErrorDto {
                            Field = "Confirm password",
                            Message = "Passwords do not match."
                        }
                    }
                };
            }
            
            // Retrieve user
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ResetPasswordResponseDto {
                    Success = false,
                    FieldErrors = new List<FieldErrorDto> {
                        new FieldErrorDto {
                            Field = "Email",
                            Message = "Invalid email"
                        }
                    }
                };
            }

            // Reset password
            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
            if (!result.Succeeded)
            {
                return new ResetPasswordResponseDto {
                    Success = false,
                    FieldErrors = result.Errors.Select(error =>
                        new FieldErrorDto {
                            Field = error.Code,
                            Message = error.Description
                        }
                    )
                };
            }

            // Login user
            var authProps = GetAuthProps();
            await _signInManager.SignInAsync(user, authProps);

            // User logged in
            return new ResetPasswordResponseDto {
                Success = true,
                Jwt = GenerateJwt(user)
            };
        }

        [HttpPost("ForgotPassword", Name=nameof(ForgotPassword))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task ForgotPassword(
            [FromBody] ForgotPasswordRequestDto request
        ) {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return;
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _emailService.SendForgotPasswordAsync(user, token);
        }

        [HttpGet("Me", Name=nameof(Me))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<MeResponseDto> Me()
        {
            if (User == null || !User.Identity.IsAuthenticated)
            {
                return new MeResponseDto { LoggedIn = false };
            }

            var identity = User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return new MeResponseDto { LoggedIn = false };
            }
            
            var user = await _userManager.FindByNameAsync(identity.Name);
            if (user == null)
            {
                return new MeResponseDto { LoggedIn = false };
            }

            return new MeResponseDto {
                LoggedIn = true,
                Jwt = GenerateJwt(user)
            };
        }

        [HttpPost("Logout", Name=nameof(Logout))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            await _signInManager.SignOutAsync();
        }

        [HttpPost("Login", Name=nameof(Login))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<LoginResponseDto> Login(
            [FromBody] LoginRequestDto request
        ) {
            // Verify user exists
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return new LoginResponseDto {
                    Success = false,
                    FieldErrors = new List<FieldErrorDto> {
                        new FieldErrorDto {
                            Field = "Username",
                            Message = "Invalid login."
                        }
                    }
                };
            }

            // Validate password
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!isPasswordValid)
            {
                return new LoginResponseDto {
                    Success = false,
                    FieldErrors = new List<FieldErrorDto> {
                        new FieldErrorDto {
                            Field = "Password",
                            Message = "Invalid password."
                        }
                    }
                };
            }

            // Login user
            var authProps = GetAuthProps();
            await _signInManager.SignInAsync(user, authProps);

            // User logged in
            return new LoginResponseDto {
                Success = true,
                Jwt = GenerateJwt(user)
            };
        }

        [HttpPost("Register", Name=nameof(Register))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<RegisterResponseDto> Register(
            [FromBody] RegisterRequestDto request
        ) {
            // Validate confirm password
            if (request.Password != request.ConfirmPassword)
            {
                return new RegisterResponseDto {
                    Success = false,
                    FieldErrors = new List<FieldErrorDto> {
                        new FieldErrorDto {
                            Field = "Confirm password",
                            Message = "Passwords do not match."
                        }
                    }
                };
            }

            // Register user
            var user = new User {
                UserName = request.Username,
                Email = request.Email,
            };
            var identityResult = await _userManager.CreateAsync(
                user,
                request.Password
            );

            // Handle errors
            if (identityResult.Errors.Any())
            {
                var fieldErrors = new List<FieldErrorDto>();
                foreach (var error in identityResult.Errors)
                {
                    fieldErrors.Add(new FieldErrorDto {
                        Field = error.Code,
                        Message = error.Description
                    });
                }

                return new RegisterResponseDto {
                    Success = false,
                    FieldErrors = fieldErrors
                };
            }

            // Login user after registering
            var authProps = GetAuthProps();
            await _signInManager.SignInAsync(user, authProps);

            // Registered user
            return new RegisterResponseDto {
                Success = true,
                Jwt = GenerateJwt(user)
            };
        }

        private AuthenticationProperties GetAuthProps()
        {
            return new AuthenticationProperties {
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7),
                AllowRefresh = true,
            };
        }

        private string GenerateJwt(User user)
        {
            var secret = Encoding.ASCII.GetBytes(_jwtConfig.CurrentValue.Secret);
            var signingCredentials =  new SigningCredentials(
                new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature
            );

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddYears(1),
                SigningCredentials = signingCredentials,
                Subject = new ClaimsIdentity(new [] {
                    new Claim("Username", user.UserName),
                    new Claim(JwtRegisteredClaimNames.Iss, _jwtConfig.CurrentValue.Issuer),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
            };
            
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var jwt = jwtSecurityTokenHandler.WriteToken(token);
            return jwt;
        }
    }
}