using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.Services.Models.AtualizarDados
{
    /// <summary>
    /// Modelo de dados para a requisição do serviço de atualização de dados de conta do usuário
    /// </summary>
    public class AtualizarDadosRequestModel
    {
        [MinLength(8, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Informe no máximo {1} caracteres.")]
        public string? Nome { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Informe uma senha com letras maísculas, letras minúsculas, números, símbolos e no mínimo 8 caracteres.")]
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Senhas não conferem, por favor verifique.")]
        public string? SenhaConfirmacao { get; set; }
    }
}
