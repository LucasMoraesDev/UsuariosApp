namespace UsuariosApp.Services.Models.Autenticar
{
    /// <summary>
    /// Modelo de dados para o retorno do serviço de autenticação de conta do usuário
    /// </summary>
    public class AutenticarResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? DataHoraAcesso { get; set; }
        public DateTime? DataHoraExpiracao { get; set; }
    }
}
