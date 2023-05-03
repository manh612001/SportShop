using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using SportShop.Interface;
using SportShop.Models;
using SportShop.ViewModels.Account;
using System.Security.Claims;

namespace SportShop.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        public async Task<string> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    throw new Exception("Tài khoản người dùng không đúng");
                }
                if (!await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    throw new Exception("Mật khẩu không đúng");
                }
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
                if (signInResult.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,user.UserName),
                        
                    };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                }
                else if (signInResult.IsLockedOut)
                {
                    throw new Exception("Người dùng đã đăng xuất");
                }
                else
                {
                    throw new Exception("Đăng nhập thất bại");
                }
            }
            catch (Exception)
            {

                throw new Exception("Đăng nhập thất bại");
            }
            return model.ReturnUrl ?? "/";
        }

        public async Task Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
            }
            catch (Exception)
            {

                throw new Exception("logout faile");
            }
        }

        public async Task SignUp(AddUserViewModel model)
        {
            try
            {
                var userExists = await _userManager.FindByNameAsync(model.UserName);
                if (userExists != null)
                {
                    throw new Exception("User already exist");
                }
                AppUser user = new AppUser()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                    Address = model.Address,
                    Dob = model.Dob,
                    Role = model.Role,
                    FullName = model.FullName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("User creation failed");
                }

                if (!await _roleManager.RoleExistsAsync(model.Role))
                    await _roleManager.CreateAsync(new IdentityRole(model.Role));


                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }
            }
            catch (Exception)
            {

                throw new Exception("Lỗi không tạo được tài khoản!");
            }

            

        }
        public async Task<List<AppUser>> GetAll()
        {
            try
            {
                return await _userManager.Users.ToListAsync();
            }
            catch (Exception)
            {

                throw new Exception("Lỗi không tìm thấy danh sách nhân viên");
            }
            
        }

        public async Task<AddUserViewModel> GetById(string? id)
        {
            try
            {
                var user =  await _userManager.FindByIdAsync(id);
                var obj = new AddUserViewModel()
                {
                    Id = id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Dob = user.Dob,
                    Role = user.Role,
                    Address= user.Address,
                   
                };
                return obj;
            }
            catch (Exception)
            {

                throw new Exception("Không tìm thấy nhân viên!");
            }
            
        }

        public async Task Update(AddUserViewModel user)
        {
            try
            {
                var obj = new AppUser()
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Dob = user.Dob,
                    Role = user.Role,
                    Address = user.Address,
                };
                await _userManager.UpdateAsync(obj);
            }
            catch (Exception)
            {

                throw new Exception("Lỗi không cập nhật được thông tin nhân viên!");
            }
            
        }

        public async Task Delete(string? id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
            }
            catch (Exception)
            {

                throw new Exception("Lỗi không xóa được nhân viên");
            }
        }

        public async Task<AddUserViewModel> GetByName(string? name)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(name);
                var obj = new AddUserViewModel()
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Dob = user.Dob,
                    Role = user.Role,
                    Address = user.Address,

                };
                return obj;
            }
            catch (Exception)
            {

                throw new Exception("Không tìm thấy nhân viên!");
            }
        }
    }
}
