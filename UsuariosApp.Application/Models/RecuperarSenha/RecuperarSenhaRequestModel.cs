using System.ComponentModel.DataAnnotations;

namespace UsuariosApp.Application.Models.RecuperarSenha
{
    /// <summary>
    /// Modelo de dados para a requisição do serviço de recuperação de senha do usuário
    /// </summary>
    public class RecuperarSenhaRequestModel
    {
        [EmailAddress(ErrorMessage = "Informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do usuário.")]
        public string? Email { get; set; }
    }
}
