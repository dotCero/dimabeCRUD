using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using dimabeCRUD.Data;
using dimabeCRUD.Models;

namespace dimabeCRUD.Pages.RoleUsers
{
    public class CreateModel : PageModel
    {
        private readonly dimabeCRUD.Data.DimabeCrudContext _context;

        public CreateModel(dimabeCRUD.Data.DimabeCrudContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "FullName");
            return Page();
        }

        [BindProperty]
        public RoleUser RoleUser { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RoleUsers.Add(RoleUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
