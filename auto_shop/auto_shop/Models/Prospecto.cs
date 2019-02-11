using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace auto_shop.Models
{
    [Table("prospectos")]
    public class Prospecto
    {
        [PrimaryKey, AutoIncrement]
        public int idserial { get; set; }
        [MaxLength(80)]
        public string nombre { get; set; }
        [MaxLength(80)]
        public string apellido{ get; set; }
        [MaxLength(80)]
        public string correo { get; set; }
        [MaxLength(10)]
        public string telefono { get; set; }
        public int genero { get; set; }
        public int edad { get; set; }
        public int marca { get; set; }
        public int modelo { get; set; }
        public int version { get; set; }
        public int detalle { get; set; }
        public int usuela { get; set; }
        public int idasesor { get; set; }
        public Boolean financiacion { get; set; }
        public Boolean enviar_cotizacion { get; set; }
        public Boolean ficha_tecnica { get; set; }
        public Boolean pedido{ get; set; }
        public int razon_compra { get; set; }
        public Boolean sincronizado { get; set; }
        public Boolean habeas_data { get; set; }
        public int ciudad { get; set; }
        public string ot_ciudad { get; set; }
        public DateTime fecha_sincronizacion { get; set; }



        public string nombreCompleto
        {
            get
            {
                return string.Format("{0} {1}", this.nombre, this.apellido);
            }
        }
    }
}
