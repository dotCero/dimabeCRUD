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

namespace dimabeCRUD.Pages.RoleUsers
{
    public class EditModel : PageModel
    {
        private readonly dimabeCRUD.Data.DimabeCrudContext _context;

        public EditModel(dimabeCRUD.Data.DimabeCrudContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RoleUser RoleUser { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoleUser = await _context.RoleUsers
                .Include(r => r.Role)
                .Include(r => r.User).FirstOrDefaultAsync(m => m.Id == id);

            if (RoleUser == null)
            {
                return NotFound();
            }
           ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(RoleUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoleUserExists(RoleUser.Id))
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

        private bool RoleUserExists(int id)
        {
            return _context.RoleUsers.Any(e => e.Id == id);
        }
    }
}
