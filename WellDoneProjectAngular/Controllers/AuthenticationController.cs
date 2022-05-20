using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using WellDoneProjectAngular.Core.Costants;
using WellDoneProjectAngular.Core.Dtos;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender, 
            ILogger<AuthenticationController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = logger;
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (IsValidLogin(model))
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);
                
                if (result.Succeeded)
                {
                    _logger.LogInformation($"User {model.Username} logged in.");
                    return Ok(new AuthenticationDto()
                    {
                        UserAuthenticated = true,
                        Required2FA = false,
                        LockedOut = false,
                        NotAllowed = false
                    });
                }

                if (result.RequiresTwoFactor)
                {
                    _logger.LogInformation($"2FA required for user {model.Username}");

                    var user = await _userManager.FindByNameAsync(model.Username);
                    var token = await _userManager.GenerateTwoFactorTokenAsync(user, AuthorizationConstants.TokeProvider);

                    await _emailSender.SendEmailAsync(user.Email, "Your Token", token);

                    return Ok(new AuthenticationDto()
                    {
                        UserAuthenticated = true,
                        Required2FA = true,
                        LockedOut = false,
                        NotAllowed = false
                    });
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning($"User {model.Username} account locked out.");
                    return Ok(new AuthenticationDto()
                    {
                        UserAuthenticated = true,
                        Required2FA = false,
                        LockedOut = true,
                        NotAllowed = false
                    });
                }   

                // Sara tipo se non è andata bene con gli altri?
                if (result.IsNotAllowed)
                {
                    _logger.LogWarning($"User {model.Username} not allowed to log-in");
                    return Ok(new AuthenticationDto()
                    {
                        UserAuthenticated = true,
                        Required2FA = false,
                        LockedOut = false,
                        NotAllowed = true
                    });
                }
            }

            _logger.LogWarning($"User {model.Username} - invalid login attempt.");
            return Unauthorized(new AuthenticationDto()
            {
                UserAuthenticated = false,
                Required2FA = false,
                LockedOut = false
            });
        }


        [HttpPost]
        [Route("register")]
        //[Authorize(Roles = AuthorizationConstants.Roles.Admin)]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            //_logger.LogInformation("Admin <{0}> trying to register user <{1}>", User.Identity.Name, model.Email);

            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, "User already exists!" );

            ApplicationUser user = new ApplicationUser()
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = true
            };

     
            //await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            //await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);


            var result = await _userManager.CreateAsync(user, model.Password);

            var roleRes = await _userManager.AddToRoleAsync(user, AuthorizationConstants.Roles.User);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                var userId = await _userManager.GetUserIdAsync(user);
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                var callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    pageHandler: null,
                    values: new { area = "Identity", userId = userId, code = code },
                    protocol: Request.Scheme);


                await _emailSender.SendEmailAsync(user.Email, "Confirm your email",
                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");


                if (_userManager.Options.SignIn.RequireConfirmedEmail)
                {
                    return Ok();
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return Ok();
                }
            }
            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError(string.Empty, error.Description);
            //}

            //var result = await _userManager.CreateAsync(user, model.Password);

            //if (!result.Succeeded)
            //    return StatusCode(StatusCodes.Status500InternalServerError, new AuthResponeModel { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            //if (!await _roleManager.RoleExistsAsync(AuthorizationConstants.Roles.User))
            //{
            //    await _roleManager.CreateAsync(new IdentityRole(AuthorizationConstants.Roles.User));
            //}

            //await _userManager.AddToRoleAsync(user, AuthorizationConstants.Roles.User);

            //return Ok(new AuthResponeModel { Status = "Success", Message = "User created successfully!" });
            return Ok();
        }



        private bool IsValidLogin(LoginDto model)
        {
            return String.IsNullOrEmpty(model.Username) && String.IsNullOrEmpty(model.Password);
        }
    }
}
