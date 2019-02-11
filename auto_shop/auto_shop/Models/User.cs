using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace auto_shop.Models
{
    [Table("usuarios")]
    public class User
    {
        public int idserial { get; set; }
        public string nombreusuario { get; set; }
        public string usuario { get; set; }
        public string password { get; set; }
        public int crear_prospecto { get; set; }
        public int editar_prospecto { get; set; }
        public int asignar_prospecto { get; set; }
    }
}
