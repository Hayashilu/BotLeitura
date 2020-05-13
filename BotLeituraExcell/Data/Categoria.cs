using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("dbo.Categoria")]
    public class Categoria
    {
        [Key]
        public int idCategoria { get; set; }

        public string infoCategoria { get; set; }
    }
}
