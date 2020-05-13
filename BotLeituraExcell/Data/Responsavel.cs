using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("Responsavel")]
    public class Responsavel
    {
        [Key]
        public int idResponsavel { get; set; }

        public string infoResponsavel { get; set; }
    }
}
