using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using soft20181_starter.Models;
using System.ComponentModel.DataAnnotations;

namespace soft20181_starter.Pages.Manage
{
    [Authorize(Roles = "Admin")]
    public class ChangePasswordModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string id { get; set; }

        [BindProperty]
        public User TheUser { get; set; }

/*        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }*/

        [BindProperty]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        private readonly EventAppDbContext dbContext;

        private readonly UserManager<User> _userManager;

        public ChangePasswordModel(EventAppDbContext _db, UserManager<User> userManager)
        {
            _userManager = userManager;
            dbContext = _db;
        }
        public void OnGet()
        {
            TheUser = dbContext.Users.Find(id);
        }

        public IActionResult OnGetDelete()
        {
            TheUser = dbContext.Users.Find(id);
            dbContext.Users.Remove(TheUser);
            dbContext.SaveChanges();
            return RedirectToPage("ViewUsers", new { Delete = true });
        }

        public IActionResult OnPost()
        {
            TheUser = _userManager.FindByEmailAsync(TheUser.Email).Result;
            if (TheUser != null)
            {
               
                if (NewPassword == ConfirmPassword)
                {
                    var result = _userManager.RemovePasswordAsync(TheUser);
                    if (result.Result.Succeeded)
                    {
                        var result2 = _userManager.AddPasswordAsync(TheUser, NewPassword);
                        if (result2.Result.Succeeded)
                        {
                            return RedirectToPage("ViewUsers", new { PasswordChange = true });
                        }
                        else
                        {
                            return Page();
                        }
                    }
                    else
                    {
                        return Page();
                    }
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }   
    }
}
