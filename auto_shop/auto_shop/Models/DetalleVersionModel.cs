using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace auto_shop.Models
{
    [Table("detalleVersion")]
    public class DetalleVersionModel
    {
        [PrimaryKey]
        public int idserial { get; set; }
        public int id_version { get; set; }
        public string motor { get; set; }
        public int anho { get; set; }
        public int costo { get; set; }

        public string detalleMostrar
        {
            get
            {
                return string.Format("Motor: {0} - Año: {1} - Costo: $ {2:N0}", this.motor, this.anho, this.costo);

            }
        }
    }


}
