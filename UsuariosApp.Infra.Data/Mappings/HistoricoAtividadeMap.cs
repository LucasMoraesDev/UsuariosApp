using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApp.Domain.Entities;

namespace UsuariosApp.Infra.Data.Mappings
{
    public class HistoricoAtividadeMap : IEntityTypeConfiguration<HistoricoAtividade>
    {
        public void Configure(EntityTypeBuilder<HistoricoAtividade> builder)
        {
            //nome da tabela
            builder.ToTable("HISTORICOATIVIDADE");

            //chave primária
            builder.HasKey(h => h.Id);

            //mapeamento dos campos da tabela
            builder.Property(h => h.Id)
                .HasColumnName("ID");

            builder.Property(h => h.DataHora)
                .HasColumnName("DATAHORA")
                .IsRequired();

            builder.Property(h => h.Tipo)
                .HasColumnName("TIPO")
                .IsRequired();

            builder.Property(h => h.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(h => h.UsuarioId)
                .HasColumnName("USUARIO_ID")
                .IsRequired();

            //mapeamento do relacionamento (1 para muitos)
            builder.HasOne(h => h.Usuario) //Historico de Atividade TEM 1 Usuário
                .WithMany(u => u.Historicos) //Usuario TEM MUITOS Históricos
                .HasForeignKey(h => h.UsuarioId); //definindo o campo CHAVE ESTRANGEIRA
        }
    }
}
