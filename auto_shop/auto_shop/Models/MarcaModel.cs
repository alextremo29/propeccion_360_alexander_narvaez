using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace auto_shop.Models
{
    [Table("marcas")]
    public class MarcasModel
    {
        [PrimaryKey]
        public int idserial { get; set; }
        public string descripcion { get; set; }

    }
}
