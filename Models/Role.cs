using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dimabeCRUD.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public string NameForList
        {
            get
            {
                return Name + ", ";
            }
        }
        
        public ICollection<RoleUser> RoleUsers { get; set; }
    }
}