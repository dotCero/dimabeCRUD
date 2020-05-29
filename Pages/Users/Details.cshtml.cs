using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dimabeCRUD.Data;
using dimabeCRUD.Models;

namespace dimabeCRUD.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly dimabeCRUD.Data.DimabeCrudContext _context;

        public DetailsModel(dimabeCRUD.Data.DimabeCrudContext context)
        {
            _context = context;
        }

        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //User = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            
            User = await _context.Users
                .Include(u => u.RoleUsers)
                .ThenInclude(ru => ru.Role)
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
