namespace UsuariosApp.Services.Models.CriarConta
{
    /// <summary>
    /// Modelo de dados para a resposta do serviço de criação de conta do usuário
    /// </summary>
    public class CriarContaResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraCriacao { get; set; }
    }
}
