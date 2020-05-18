using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("Problemas")]
    public class Problemas
    {
        [Key]
        public int idProblemas { get; set; }

        public string numeroProblema { get; set; }

        #region Severidade
        [ForeignKey("idPrazo")]
        public virtual Prazo prazo { get; set; }

        public int idPrazo { get; set; }
        #endregion

        public string resumo { get; set; }

        #region Severidade
        [ForeignKey("idPrioridade")]
        public virtual Severidade severidade { get; set; }

        public int idPrioridade { get; set; }
        #endregion

        #region Categoria
        [ForeignKey("idCategoria")]
        public virtual Categoria categoria { get; set; }

        public int idCategoria { get; set; }
        #endregion

        #region Grupo Executor
        [ForeignKey("idGrupoExec")]
        public virtual GrupoExecutor executor { get; set; }

        public int idGrupoExec { get; set; }
        #endregion


        #region Responsavel
        [ForeignKey("idResponsavel")]
        public virtual Responsavel responsavel { get; set; }

        public int idResponsavel { get; set; }
        #endregion

        #region Status
        [ForeignKey("idStatus")]
        public virtual Status status { get; set; }

        public int idStatus { get; set; }
        #endregion

        [DataType(DataType.DateTime)]
        public DateTime dataAbertura { get; set; }

        [DataType(DataType.Date)]
        public DateTime mesAbertura { get; set; }

        #region Atribuido
        [ForeignKey("idAtribuido")]
        public virtual Atribuido atribuido { get; set; }

        public int idAtribuido { get; set; }
        #endregion

        #region Usuario Afetado
        //Coluna 21
        [ForeignKey("idUsuarioFinal")]
        public UsuarioFinal usuarioAfetado { get; set; }

        public int idUsuarioFinal { get; set; }
        #endregion

        public string parent { get; set; }

        public string causedByOrder { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime dataResolucao { get; set; }

        [DataType(DataType.Date)]
        public DateTime mesResolucao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime dataFechamento { get; set; }

        [DataType(DataType.Date)]
        public DateTime mesFechamento { get; set; }

        #region Departamento
        [ForeignKey("idDepartamento")]
        public Departamento departamento { get; set; }

        public int idDepartamento { get; set; }
        #endregion

        [DataType(DataType.Date)]
        public DateTime dataReferencia { get; set; }

    }
}
