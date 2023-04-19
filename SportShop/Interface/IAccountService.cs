using SportShop.Models;
using SportShop.ViewModels.Account;

namespace SportShop.Interface
{
    public interface IAccountService
    {
        Task Login(LoginViewModel model);
        Task Logout();
        Task SignUp(AddUserViewModel model);
        Task<List<AppUser>> GetAll();
        Task<AddUserViewModel> GetById(string? id);
        Task Update(AddUserViewModel user);
        Task Delete(string? id);
    }
}
