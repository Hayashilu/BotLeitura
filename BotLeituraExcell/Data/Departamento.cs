﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("Departamento")]
   public class Departamento
    {
        [Key]
        public int idDepartamento { get; set; }

        public string infoDepartamento { get; set; }
    }
}