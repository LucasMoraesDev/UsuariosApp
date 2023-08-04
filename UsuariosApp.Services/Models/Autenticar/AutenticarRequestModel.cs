using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.Services.Models.Autenticar
{
    /// <summary>
    /// Modelo de dados para a requisição do serviço de autenticação de conta do usuário
    /// </summary>
    public class AutenticarRequestModel
    {
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string? Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
            ErrorMessage = "Informe uma senha com letras maísculas, letras minúsculas, números, símbolos e no mínimo 8 caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a senha do usuário.")]
        public string? Senha { get; set; }
    }
}
