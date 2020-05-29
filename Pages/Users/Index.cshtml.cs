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

namespace dimabeCRUD.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly dimabeCRUD.Data.DimabeCrudContext _context;

        public IndexModel(dimabeCRUD.Data.DimabeCrudContext context)
        {
            _context = context;
        }
        
        public string SortByName { get; set; }
        public string SortByEMail { get; set; }
        public string SortByRole { get; set; }
        public string SortByDate { get; set; }
        public string Filter { get; set; }
        public string Sort { get; set; }
        public Paginate<User> Users { get; set; }

        public async Task OnGetAsync(string order, string search, string filter, int? indexPage)
        {
            Sort = order;
            
            SortByName = string.IsNullOrEmpty(order) ? "nombre_desc" : "";
            SortByEMail = order == "email" ? "email_desc" : "email";
            SortByRole = order == "role" ? "role_desc" : "role";
            SortByDate = order == "date" ? "date_desc" : "date";
            
            if (search != null)
            {
                indexPage = 1;
            }
            else
            {
                search = filter;
            }
            
            Filter = search;

            var userQuery = from user in _context.Users
                select user;
            
            if (!string.IsNullOrEmpty(search))
            {
                userQuery = userQuery.Where(u => u.Name.Contains(search)
                                                 || u.Email.Contains(search)
                                                 || u.LastName.Contains(search));
            }

            userQuery = order switch
            {
                "nombre_desc" => userQuery.OrderByDescending(u => u.Name),
                "email" => userQuery.OrderBy(u => u.Email),
                "email_desc" => userQuery.OrderByDescending(u => u.Email),
                "role" => userQuery.OrderBy(u => u.RoleUsers),
                "role_desc" => userQuery.OrderByDescending(u => u.RoleUsers),
                "date" => userQuery.OrderBy(u => u.RegistrationDate),
                "date_desc" => userQuery.OrderByDescending(u => u.RegistrationDate),
                _ => userQuery.OrderBy(u => u.Name)
            };

            const int size = 5;
            
            Users = await Paginate<User>.CreateAsync(userQuery.AsNoTracking(), indexPage ?? 1, size);
        }
    }
}
