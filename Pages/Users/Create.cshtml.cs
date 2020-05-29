using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using dimabeCRUD.Data;
using dimabeCRUD.Models;
using dimabeCRUD.Utils;

namespace dimabeCRUD.Pages.Users
{
    public class CreateModel : UserRolesPageModel
    {
        private readonly dimabeCRUD.Data.DimabeCrudContext _context;
        Encrypt encrypt = new Encrypt();

        public CreateModel(dimabeCRUD.Data.DimabeCrudContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var user = new User();
            user.RoleUsers = new List<RoleUser>();
            PopulateAssignedRoleData(_context, user);
            return Page();
        }

        [BindProperty]
        public User User { get; set; }
        public string ErrorPasswordMessage { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var neoUser = new User();
            
            if (selectedRoles != null)
            {
                neoUser.RoleUsers = new List<RoleUser>();
                foreach (var role in selectedRoles)
                {
                    var roleToAdd = new RoleUser
                    {
                        RoleId = int.Parse(role)
                    };
                    neoUser.RoleUsers.Add(roleToAdd);
                }
            }

            if (await TryUpdateModelAsync<User>(
                neoUser,
                "user",
                u => u.Name,
                u => u.LastName,
                u => u.Email,
                u => u.RegistrationDate,
                u => u.Password,
                u => u.ValidatePassword
                ))
            {
                
                if (neoUser.Password == neoUser.ValidatePassword)
                {
                    ErrorPasswordMessage = "";
                    neoUser.Password = encrypt.EncSHA256((User.Password));
                    neoUser.RegistrationDate = DateTime.Now;

                    _context.Users.Add(neoUser);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("./Index");
                }
                else
                {
                    ErrorPasswordMessage = "(X) Las contraseñas deben ser iguales.";
                }
                
            }
            PopulateAssignedRoleData(_context, neoUser);
            return Page();
        }
    }
}
