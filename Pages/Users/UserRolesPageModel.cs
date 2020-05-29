using System.Collections.Generic;
using System.Linq;
using dimabeCRUD.Data;
using dimabeCRUD.Models;
using dimabeCRUD.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace dimabeCRUD.Pages.Users
{
    public class UserRolesPageModel : PageModel
    {
        public List<AssignedRoleData> AssignedRoleDataList;

        public void PopulateAssignedRoleData(DimabeCrudContext _context, User user)
        {
            var allRoles = _context.Roles;
            var userRoles = new HashSet<int>(user.RoleUsers.Select(r => r.RoleId));
            AssignedRoleDataList = new List<AssignedRoleData>();

            foreach (var role in allRoles)
            {
                AssignedRoleDataList.Add(new AssignedRoleData
                {
                    RoleID = role.Id,
                    RoleName = role.Name,
                    Assigned = userRoles.Contains(role.Id)
                });
            }
        }

        public void UpdateUserRoles(DimabeCrudContext _context, string[] selectedRoles, User userToUpdate)
        {
            if (selectedRoles == null)
            {
                userToUpdate.RoleUsers = new List<RoleUser>();
                return;
            }
            
            var selectedRolesHashSet = new  HashSet<string>(selectedRoles);
            var userRoles = new HashSet<int>(userToUpdate.RoleUsers.Select(r => r.Role.Id));

            foreach (var role in _context.Roles)
            {
                if (selectedRolesHashSet.Contains(role.Id.ToString()))
                {
                    if (!userRoles.Contains(role.Id))
                    {
                        userToUpdate.RoleUsers.Add(new RoleUser
                        {
                            UserId = userToUpdate.Id,
                            RoleId = role.Id
                        });
                    }
                }
                else
                {
                    if (userRoles.Contains(role.Id))
                    {
                        var roleToRemove = userToUpdate.RoleUsers.SingleOrDefault(u => u.RoleId == role.Id);
                        _context.Remove(roleToRemove);
                    }
                }
            }
        }
    }
}