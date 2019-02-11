using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace auto_shop.Models
{
    [Table("modelos")]
    public class ModeloModel
    {
        [PrimaryKey]
        public int idserial { get; set; }
        public int id_marca { get; set; }
        public string descripcion { get; set; }
    }
}
