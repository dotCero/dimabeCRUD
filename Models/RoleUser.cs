using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dimabeCRUD.Models
{
    public class RoleUser
    {
        public int Id { get; set; }
        [Display(Name = "Rol")]
        public int RoleId { get; set; }
        [Display(Name = "Rol Asignado")]
        public Role Role { get; set; }
        [Display(Name = "Usuario")]
        public int UserId { get; set; }
        [Display(Name = "Usuario")]
        public User User { get; set; }
    }
}