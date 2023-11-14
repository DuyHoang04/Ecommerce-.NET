using Ecommerce.Dto.Request;
using Ecommerce.Dto.Response;
using Ecommerce.Dto.Wrapper;
using Ecommerce.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Ecommerce.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResponseDto<string>> ChangePassword(string userId, ChangePassRequest request)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if(user == null)
                {
                    return _response.NotFound("User Not Found");
                }

                var passwordCheckResult = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
                if (!passwordCheckResult)
                {
                    return _response.ResponseError("Current password is incorrect");
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

                if (changePasswordResult.Succeeded)
                {
                    return _response.ResponseSuccess("Password changed successfully");

                }
                else
                {
                    _response.Status = 404;
                    _response.Message = "Failed to change password";
                    _response.Success = false;
                    foreach (var error in changePasswordResult.Errors)
                    {
                        _response.Message += $"\n- {error.Description}";
                    }
                    return _response;
                }
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<UserWrapper>> findUserById(string userId)
        {
            var _response = new ResponseDto<UserWrapper>();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if(user == null)
                {
                    return _response.NotFound("User Not Found");
                }
                var result = new UserWrapper
                {
                    UserId = user.Id,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    PhoneNumber = user.PhoneNumber!,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                };

                return _response.ResponseData(result);
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }

        public async Task<ResponseDto<List<UserWrapper>>> GetAllUser()
        {
            var _response = new ResponseDto<List<UserWrapper>>();
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var result = users.Select(user => new UserWrapper
                {
                    UserId = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt,
                }).ToList();
                return _response.ResponseData(result);
            }
            catch (Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }

        }

        public async Task<ResponseDto<string>> UpdateUser(UserRequest request, string userId)
        {
            var _response = new ResponseDto<string>();
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if(user == null)
                {
                   return _response.NotFound("User Not Found");
                }
                user.UserName = request.UserName;
                user.Address = request.Address;
                user.PhoneNumber = request.PhoneNumber;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!await _roleManager.RoleExistsAsync(request.Role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(request.Role));
                }
                var currentRoles = await _userManager.GetRolesAsync(user);
                foreach (var role in currentRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, role);
                }
                if (await _roleManager.RoleExistsAsync(request.Role))
                {
                    await _userManager.AddToRoleAsync(user, request.Role);
                }
                return _response.ResponseSuccess("Successfully");
            } catch(Exception ex)
            {
                return _response.ResponseError(ex.Message);
            }
        }
    }
}
