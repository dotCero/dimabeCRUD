using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using dimabeCRUD.Data;
using dimabeCRUD.Models;
using dimabeCRUD.Utils;

namespace dimabeCRUD.Pages.Roles
{
    public class IndexModel : PageModel
    {
        private readonly dimabeCRUD.Data.DimabeCrudContext _context;

        public IndexModel(dimabeCRUD.Data.DimabeCrudContext context)
        {
            _context = context;
        }
        
        public string SortByName { get; set; }
        public string Filter { get; set; }
        public string Sort { get; set; }
        public Paginate<Role> Roles { get; set; }

        public async Task OnGetAsync(string order, string search, string filter, int? indexPage)
        {
            //Role = await _context.Roles.ToListAsync();
            
            Sort = order;
            
            SortByName = string.IsNullOrEmpty(order) ? "nombre_desc" : "";

            if (search != null)
            {
                indexPage = 1;
            }
            else
            {
                search = filter;
            }
            Filter = search;

            var roleQuery = from role in _context.Roles
                select role;
            
            if (!string.IsNullOrEmpty(search))
            {
                roleQuery = roleQuery.Where(r => r.Name.Contains(search));
            }

            roleQuery = order switch
            {
                "nombre_desc" => roleQuery.OrderByDescending(r => r.Name),
                _ => roleQuery.OrderBy(r => r.Name)
            };

            const int size = 5;
            
            Roles = await Paginate<Role>.CreateAsync(roleQuery.AsNoTracking(), indexPage ?? 1, size);
        }
    }
}
