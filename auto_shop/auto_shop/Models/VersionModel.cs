using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace auto_shop.Models
{
    [Table("version")]
    public class VersionModel
    {
        [PrimaryKey]
        public int idserial { get; set; }
        public int id_modelo { get; set; }
        public string descripcion { get; set; }
    }
}
