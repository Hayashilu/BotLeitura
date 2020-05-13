using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("GrupoExecutor")]
    public class GrupoExecutor
    {
        [Key]
        public int idGrupoExec { get; set; }

        public string infoGrupoExec { get; set; }
    }
}
