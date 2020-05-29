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
    public class DetailsModel : PageModel
    {
        private readonly dimabeCRUD.Data.DimabeCrudContext _context;

        public DetailsModel(dimabeCRUD.Data.DimabeCrudContext context)
        {
            _context = context;
        }

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
    }
}
