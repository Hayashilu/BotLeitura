using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("UsuarioFinal")]
    public class UsuarioFinal
    {
        [Key]
        public int idUsuarioFinal { get; set; }

        public string nomeUsuarioFinal { get; set; }
    }
}
