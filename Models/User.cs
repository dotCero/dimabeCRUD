using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dimabeCRUD.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }
        
        [DataType(DataType.EmailAddress, ErrorMessage = "Debe ser un correo válido!")]
        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [NotMapped]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Campo Requerido")]
        [Display(Name = "Reingresar Contraseña")]
        public string ValidatePassword { get; set; }

        [Required(ErrorMessage = "Campo Requerido")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy hh:mm:ss}")]
        [Display(Name = "Fecha de Registro")]
        public DateTime RegistrationDate { get; set; }

        [Display(Name = "Nombre Completo")]
        public string FullName => Name + ' ' + LastName;

        [Display(Name = "Roles Asignados")]
        public ICollection<RoleUser> RoleUsers { get; set; }
    }
}