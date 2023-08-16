namespace UsuariosApp.Application.Models.RecuperarSenha
{
    /// <summary>
    /// Modelo de dados para a resposta do serviço de recuperação de senha do usuário
    /// </summary>
    public class RecuperarSenhaResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraRecuperacaoDeSenha { get; set; }
    }
}
