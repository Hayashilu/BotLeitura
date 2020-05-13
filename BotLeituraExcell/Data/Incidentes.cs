using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLeituraExcell.Data
{
    [Table("Incidentes")]
    public class Incidentes
    {
        [Key]
        public int idInformacaoTabela { get; set; }

        public string numeroIncidente { get; set; }

        public string resumo { get; set; }

        #region Severidade
        [ForeignKey("idSeveridade")]
        public virtual Severidade severidade { get; set; }

        public int idSeveridade { get; set; }
        #endregion

        #region Categoria
        [ForeignKey("idCategoria")]
        public virtual Categoria categoria { get; set; }

        public int idCategoria { get; set; }
        #endregion

        #region Status
        //Coluna 7
        [ForeignKey("idStatus")]
        public virtual Status status { get; set; }

        public int idStatus { get; set; }
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

        public DateTime violacao { get; set; }

        #region Violado
        [ForeignKey("idViolado")]
        public virtual Violado violado{ get; set; }

        public int idViolado { get; set; }
        #endregion

        #region Localidade
        [ForeignKey("idLocalidade")]
        public virtual Localidade localidade { get; set; }

        public int idLocalidade { get; set; }
        #endregion

        //Coluna 13
        [DataType(DataType.DateTime)]
        public DateTime dataAbertura { get; set; }

        [DataType(DataType.Date)]
        public DateTime mesAbertura { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ultimaAtualizacao { get; set; }

        public string retornoChamado { get; set; }

        #region Classificação chamado final
        [ForeignKey("idClassChamadoFinal")]
        public virtual ClassChamadoFinal classificacaoChamado { get; set; }

        public int idClassChamadoFinal { get; set; }
        #endregion

        [DataType(DataType.DateTime)]
        public DateTime dataResolucao { get; set; }

        [DataType(DataType.Date)]
        public DateTime mesResolucao { get; set; }

        public string descricao { get; set; }

        #region Usuario Afetado
        //Coluna 21
        [ForeignKey("idUsuarioFinal")]
        public UsuarioFinal usuarioAfetado { get; set; }

        public int idUsuarioFinal { get; set; }
        #endregion

        #region Departamento
        [ForeignKey("idDepartamento")]
        public Departamento departamento { get; set; }

        public int idDepartamento { get; set; }
        #endregion

        public string problema { get; set; }

        public string parent { get; set; }

        public string causedByOrder { get; set; }

        #region Origem 
        [ForeignKey("idOrigem")]
        public Origem origem { get; set; }

        public int idOrigem { get; set; }
        #endregion

        //Coluna 27
        public string ticketExterno { get; set; }

        [DataType(DataType.Date)]
        public DateTime dataReferencia { get; set; }
    }
}
