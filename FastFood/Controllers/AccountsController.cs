using FastFood.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Controllers
{
    public class AccountsController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<FastFoodUser> _userManager;


        public IActionResult Index()
        {
            CreateRolesandUsers();
            return RedirectToAction("List","Plates");
        }

        private async Task CreateRolesandUsers()
        {
            
                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                await _roleManager.CreateAsync(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new FastFoodUser();
                user.UserName = "default";
                user.Email = "default@default.com";
                string userPWD = "somepassword";

                IdentityResult chkUser = await _userManager.CreateAsync(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = await _userManager.AddToRoleAsync(user, "Admin");
                }
            

        }
    }
}
