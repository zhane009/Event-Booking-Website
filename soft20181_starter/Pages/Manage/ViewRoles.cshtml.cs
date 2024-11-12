using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace soft20181_starter.Pages.Manage
{
    [Authorize(Roles = "Admin")]
    public class ViewRolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        [BindProperty(SupportsGet = true)]
        public bool Edit { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Add { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Delete { get; set; }

        public List<IdentityRole> Roles { get; set; }
        public ViewRolesModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public void OnGet()
        {
            Roles = _roleManager.Roles.ToList();
        }
    }
}
