using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SocialNetwork.Core.Application.Dtos.Email;
using SocialNetwork.Core.Application.Dtos.User;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Infrastructure.Identity.Entities;
using System.Text;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Application.Dtos.Friendship;

namespace SocialNetwork.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountService
            (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailService emailService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                return new AuthenticationResponse { HasError = true, Error = "User not found." };
            }

            if (!user.EmailConfirmed)
            {
                return new AuthenticationResponse { HasError = true, Error = "Inactive user. Check your email to active." };
            }

            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
            if (!result.Succeeded)
            {
                return new AuthenticationResponse { HasError = true, Error = "Invalid credentials." };
            }

            return new AuthenticationResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                ProfilePictureUrl = user.ProfilePictureUrl,
                IsVerified = user.EmailConfirmed,
                HasError = false
            };
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        private async Task<string> GenerateEmailVerificationUri(ApplicationUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "User/ConfirmEmail";
            var uri = new Uri($"{origin}/{route}?userId={user.Id}&token={encodedToken}");
            return uri.ToString();
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin)
        {
            var existingUser = await _userManager.FindByNameAsync(request.UserName);
            if (existingUser != null)
            {
                return new RegisterResponse { HasError = true, Error = "The username is already taken." };
            }

            var existingEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                return new RegisterResponse { HasError = true, Error = "The email is already registered." };
            }

            var newUser = new ApplicationUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Phone = request.Phone,
                ProfilePictureUrl = request.ProfilePictureUrl
            };

            var createResult = await _userManager.CreateAsync(newUser, request.Password);

            if (!createResult.Succeeded)
            {
                string? errorMessage = "";

                foreach(var error in createResult.Errors)
                {
                    switch(error.Code)
                    {
                        case "PasswordTooShort":
                            errorMessage = error.Description;
                            break;

                        case "PasswordRequiresNonAlphanumeric":
                            errorMessage = error.Description;
                            break;

                        case "PasswordRequiresLower":
                            errorMessage = error.Description;
                            break;

                        case "PasswordRequiresUpper":
                            errorMessage = error.Description;
                            break;

                        case "PasswordRequiresDigit":
                            errorMessage = error.Description;
                            break;

                        case "PasswordRequiresUniqueChars":
                            errorMessage = error.Description;
                            break;

                        default:
                            errorMessage = "An error occurred trying to register the user.";
                            break;
                    }
                }

                return new RegisterResponse { HasError = true, Error = errorMessage };
            }

            var verificationUri = await GenerateEmailVerificationUri(newUser, origin);
            try
            {
                await _emailService.SendAsync(new EmailRequest
                {
                    To = newUser.Email,
                    Subject = "Activa tu cuenta",
                    Body = $"Activa tu cuenta haciendo clic en el siguiente enlace: {verificationUri}"
                });
            }
            catch (Exception ex) 
            {
            
            }

            return new RegisterResponse { HasError = false, Id = newUser.Id };
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "User not found.";
            }

            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            if (result.Succeeded)
            {
                return "Account activated succesfully";
            } 
            else
            {
                return "An error occurred while confirming this user.";
            }
        }

        public async Task<ForgotPasswordResponse> ResetPasswordAsync(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return new ForgotPasswordResponse { HasError = true, Error = "User not found" };
            }

            var newPassword = PasswordGeneratorHelper.GenerateRandomPassword();
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (!result.Succeeded)
            {
                return new ForgotPasswordResponse { HasError = true, Error = "An error ocurred while resetting password." };
            }

            try
            {
                await _emailService.SendAsync(new EmailRequest
                {
                    To = user.Email,
                    Subject = "Contraseña Restablecida",
                    Body = $"Tu nueva contraseña es: {newPassword}"
                });
            }
            catch(Exception ex) 
            {
            
            }

            return new ForgotPasswordResponse { HasError = false, Error = "" };
        }

        public async Task UpdateUserProfilePictureAsync(string userId, string profilePictureUrl)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                user.ProfilePictureUrl = profilePictureUrl;
                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<SaveUserViewModel> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var userSaveViewModel = new SaveUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.Phone,
                ProfilePictureUrl = user.ProfilePictureUrl
            };

            return userSaveViewModel;
        }

        public async Task UpdateUserProfileAsync(SaveUserViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.Id);
            if (user != null)
            {
                user.Id = vm.Id;
                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;
                user.UserName = vm.UserName;
                user.Email = vm.Email;
                user.PhoneNumber = vm.Phone;
                user.ProfilePictureUrl = vm.ProfilePictureUrl;

                if (!string.IsNullOrEmpty(vm.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, vm.Password);

                    if (!result.Succeeded)
                    {
                        string errorMessage = "";

                        foreach (var error in result.Errors)
                        {
                            switch (error.Code)
                            {
                                case "PasswordTooShort":
                                    errorMessage = "The password is too short.";
                                    break;

                                case "PasswordRequiresNonAlphanumeric":
                                    errorMessage = "The password must contain at least one non-alphanumeric character.";
                                    break;

                                case "PasswordRequiresLower":
                                    errorMessage = "The password must contain at least one lowercase letter.";
                                    break;

                                case "PasswordRequiresUpper":
                                    errorMessage = "The password must contain at least one uppercase letter.";
                                    break;

                                case "PasswordRequiresDigit":
                                    errorMessage = "The password must contain at least one digit.";
                                    break;

                                case "PasswordRequiresUniqueChars":
                                    errorMessage = "The password must contain at least unique characters.";
                                    break;

                                default:
                                    errorMessage = "An error occurred while updating the password.";
                                    break;
                            }
                        }

                        throw new Exception(errorMessage);
                    }

                }

                await _userManager.UpdateAsync(user);
            }
        }

        public async Task<Friend?> GetFriendByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return null;
            }

            return new Friend
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = username,
                ProfilePictureUrl = user.ProfilePictureUrl
            };
        }

        public async Task<bool> IsEmailRegistered(string email, string username)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null && user.UserName != username)
            {
                return true;
            }

            return false;
        }
    }
}
