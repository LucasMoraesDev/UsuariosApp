using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Domain.Entities
{
    public class Usuario
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public string? DataHoraCriacao { get; set; }
        public string? DataHoraUltimaAlteracao { get; set; }
        public string? AccessToken { get; set; }

        //Usuário TEM MUITOS Históricos de atividade
        public List<HistoricoAtividade>? Historicos { get; set; }
    }
}
