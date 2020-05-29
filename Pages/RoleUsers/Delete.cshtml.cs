using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dimabeCRUD.Data;
using dimabeCRUD.Models;

namespace dimabeCRUD.Pages.RoleUsers
{
    public class DeleteModel : PageModel
    {
        private readonly dimabeCRUD.Data.DimabeCrudContext _context;

        public DeleteModel(dimabeCRUD.Data.DimabeCrudContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoleUser = await _context.RoleUsers.FindAsync(id);

            if (RoleUser != null)
            {
                _context.RoleUsers.Remove(RoleUser);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
