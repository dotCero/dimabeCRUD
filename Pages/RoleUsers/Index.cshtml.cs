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

namespace dimabeCRUD.Pages.RoleUsers
{
    public class IndexModel : PageModel
    {
        private readonly dimabeCRUD.Data.DimabeCrudContext _context;

        public IndexModel(dimabeCRUD.Data.DimabeCrudContext context)
        {
            _context = context;
        }
        
        public string SortByRole { get; set; }
        public string SortByUser { get; set; }
        public string Filter { get; set; }
        public string Sort { get; set; }
        public Paginate<RoleUser> RoleUsers { get;set; }

        public async Task OnGetAsync(string order, string search, string filter, int? indexPage)
        {
            /*RoleUser = await _context.RoleUsers
                .Include(ru => ru.Role)
                .Include(ru => ru.User).ToListAsync();*/
            
            Sort = order;
            
            SortByRole = string.IsNullOrEmpty(order) ? "role_desc" : "";
            SortByUser = order == "user" ? "user_desc" : "user";

            if (search != null)
            {
                indexPage = 1;
            }
            else
            {
                search = filter;
            }
            Filter = search;

            var roleUserQuery = from roleUser in _context.RoleUsers.Include(ru => ru.Role).Include(ru => ru.User)
                select roleUser;
            
            if (!string.IsNullOrEmpty(search))
            {
                roleUserQuery = roleUserQuery.Where(ru => ru.Role.Name.Contains(search)
                                                                || ru.User.Name.Contains(search));
            }

            roleUserQuery = order switch
            {
                "role_desc" => roleUserQuery.OrderByDescending(ru => ru.Role.Name),
                "user" => roleUserQuery.OrderBy(ru => ru.User.Name),
                "user_desc" => roleUserQuery.OrderByDescending(ru => ru.User.Name),
                _ => roleUserQuery.OrderBy(ru => ru.Role.Name)
            };

            const int size = 5;
            
            RoleUsers = await Paginate<RoleUser>.CreateAsync(roleUserQuery.AsNoTracking(), indexPage ?? 1, size);
            
        }
    }
}
