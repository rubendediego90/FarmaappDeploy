using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entidades
{
    public class ProveedorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProveedorId { get; set; }
        [Required]
        public string Nombre { get; set; }

        public string Direccion { get; set; }
        [StringLength(20)]
        public string Telefono { get; set; }
        public string Email { get; set; }

        public Boolean Estado { get; set; } = true;
        //Relación
        public IEnumerable<ArticuloEntity> Articulo { get; set; } = new List<ArticuloEntity>();


    }
}

