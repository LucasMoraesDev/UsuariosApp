namespace UsuariosApp.Services.Models.AtualizarDados
{
    /// <summary>
    /// Modelo de dados para a resposta do serviço de atualização de dados de conta do usuário
    /// </summary>
    public class AtualizarDadosResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }
    }
}
