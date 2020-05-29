
using System;
using dimabeCRUD.Models;
using System.Linq;
using dimabeCRUD.Utils;

namespace dimabeCRUD.Data
{
    public static class CreateAndSeedDb
    {
        public static void Initialize(DimabeCrudContext _context)
        {
            //Creación y Población de la BD
            _context.Database.EnsureCreated();
            var encrypt = new Encrypt();

            if (_context.Users.Any())
            {
                return;
            }
            
            var users = new[]
            {
                new User{Name = "Álvaro", LastName = "Cornejo Vásquez", Email = "s.cornejo@correo.com", Password = encrypt.EncSHA256("pass"), RegistrationDate = DateTime.Parse("2018-12-01 13:12:12")},
                new User{Name = "Diego", LastName = "Parraguez Salas", Email = "parraguez.s@correo.cl", Password = encrypt.EncSHA256("clave"), RegistrationDate = DateTime.Parse("2000-09-05 18:40:00")},
                new User{Name = "Luis", LastName = "Zuñiga Troncoso", Email = "luis_luis@correo.com", Password = encrypt.EncSHA256("password"), RegistrationDate = DateTime.Parse("2019-09-06 02:34:01")},
                new User{Name = "Daniel", LastName = "Saavedra Lopez", Email = "dsl@correo.cl", Password = encrypt.EncSHA256("passw0rd"), RegistrationDate = DateTime.Parse("2019-01-24 00:00:00")},
                new User{Name = "Walter", LastName = "Pérez Castro", Email = "wastro@correo.com", Password = encrypt.EncSHA256("123456"), RegistrationDate = DateTime.Parse("2020-04-02 02:46:05")},
                new User{Name = "Sebastián", LastName = "Morales Rodríguez", Email = "morales.rodriguez@correo.com", Password = encrypt.EncSHA256("contrasenia"), RegistrationDate = DateTime.Parse("2020-01-03 03:13:31")},
                new User{Name = "Kewyn", LastName = "Cornejo Vásquez", Email = "k.cv@correo.cl", Password = encrypt.EncSHA256("p455w0rd"), RegistrationDate = DateTime.Parse("2020-03-12 15:41:02")},
                new User{Name = "Benjamín", LastName = "Undurraga González", Email = "buonzales@correo.com", Password = encrypt.EncSHA256("drOw554P"), RegistrationDate = DateTime.Parse("2010-02-27 00:01:02")}
            };

            _context.Users.AddRange(users);
            _context.SaveChanges();
            
            var roles = new[]
            {
                new Role{Name = "Seguridad"},
                new Role{Name = "Comunicaciones"},
                new Role{Name = "Finanzas"},
                new Role{Name = "Presidencia"},
                new Role{Name = "Visita"},
                new Role{Name = "Usuario"},
                new Role{Name = "Administrador"},
                new Role{Name = "Super Usuario"},
            };

            _context.Roles.AddRange(roles);
            _context.SaveChanges();
            
            var roleUsers = new[]
            {
                new RoleUser{RoleId = 1, UserId = 2},
                new RoleUser{RoleId = 2, UserId = 2},
                new RoleUser{RoleId = 5, UserId = 1},
                new RoleUser{RoleId = 3, UserId = 3},
                new RoleUser{RoleId = 8, UserId = 4},
                new RoleUser{RoleId = 4, UserId = 5},
                new RoleUser{RoleId = 4, UserId = 6},
                new RoleUser{RoleId = 5, UserId = 7},
                new RoleUser{RoleId = 5, UserId = 8},
                new RoleUser{RoleId = 7, UserId = 7},
                new RoleUser{RoleId = 4, UserId = 8},
                new RoleUser{RoleId = 6, UserId = 3},
            };

            _context.RoleUsers.AddRange(roleUsers);
            _context.SaveChanges();
        }
    }
}