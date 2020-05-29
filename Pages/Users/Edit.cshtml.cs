using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dimabeCRUD.Data;
using dimabeCRUD.Models;
using dimabeCRUD.Utils;

namespace dimabeCRUD.Pages.Users
{
    public class EditModel : UserRolesPageModel
    {
        private readonly dimabeCRUD.Data.DimabeCrudContext _context;
        Encrypt encrypt = new Encrypt();

        public EditModel(dimabeCRUD.Data.DimabeCrudContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }
        public string ErrorPasswordMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //User = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            User = await _context.Users.Include(u => u.RoleUsers).ThenInclude(u => u.Role).AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (User == null)
            {
                return NotFound();
            }
            PopulateAssignedRoleData(_context, User);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (id == null)
            {
                return NotFound();
            }
            
            var currentUser = await _context.Users
                .Include(u => u.RoleUsers)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            /*var currentUser = await _context.Users.FindAsync(id);*/
            
            if (currentUser == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<User>(
                currentUser,
                "user",
                u=> u.Name,
                u => u.Email,
                u => u.LastName,
                u => u.Password,
                u => u.ValidatePassword
                ))
            {
                if (currentUser.Password == currentUser.ValidatePassword)
                {
                    ErrorPasswordMessage = "";
                    currentUser.Password = encrypt.EncSHA256((User.Password));
                    
                    UpdateUserRoles(_context, selectedRoles, currentUser);
                    
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(User.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }

                    return RedirectToPage("./Index");
                }
                else
                {
                    ErrorPasswordMessage = "(X) Las contraseñas deben ser iguales.";
                }
                
            }

            UpdateUserRoles(_context, selectedRoles, currentUser);
            PopulateAssignedRoleData(_context, currentUser);
            return Page();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
