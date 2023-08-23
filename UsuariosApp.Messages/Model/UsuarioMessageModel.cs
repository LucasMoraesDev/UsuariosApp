using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsuariosApp.Messages.Models
{
    /// <summary>
    /// Modelo de dados do conteudo que será gravado na fila
    /// </summary>
    public class UsuarioMessageModel
    {
        public Guid? Id { get; set; }
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}



