using System;
using System.Collections.Generic;
using AgendAS.Models;
using AgendAS.Models.Enums;

namespace AgendAS.Repositories;

public static class DadosMock
{
    public static List<Usuario> Usuarios { get; } = new()
    {
        new Usuario
        {
            Id = 1,
            NomeUsuario = "admin",
            Senha = "123456",
            NomeCompleto = "Dr. Carlos Eduardo",
            Email = "carlos.eduardo@farmaciapremium.com.br",
            Cargo = "Farmacêutico Chefe"
        }
    };

    public static List<Cliente> Clientes { get; } = new()
    {
        new Cliente { Id = 1, Nome = "Antônio Silva Costa", Cpf = "123.456.789-00", Telefone = "(11) 98765-4321", Email = "antonio.silva@email.com", DataNascimento = new DateTime(1965, 5, 12), Endereco = "Av. Paulista, 1000 - Bela Vista, São Paulo - SP" },
        new Cliente { Id = 2, Nome = "Maria Eduarda Medeiros", Cpf = "234.567.890-11", Telefone = "(11) 97654-3210", Email = "maria.eduarda@email.com", DataNascimento = new DateTime(1988, 8, 24), Endereco = "Rua Oscar Freire, 500 - Jardins, São Paulo - SP" },
        new Cliente { Id = 3, Nome = "João Pedro Santos", Cpf = "345.678.901-22", Telefone = "(11) 96543-2109", Email = "joao.pedro@email.com", DataNascimento = new DateTime(1992, 11, 3), Endereco = "Alameda Lorena, 1200 - Cerqueira César, São Paulo - SP" },
        new Cliente { Id = 4, Nome = "Ana Beatriz Oliveira", Cpf = "456.789.012-33", Telefone = "(11) 95432-1098", Email = "ana.beatriz@email.com", DataNascimento = new DateTime(1975, 4, 18), Endereco = "Rua Tabapuã, 800 - Itaim Bibi, São Paulo - SP" },
        new Cliente { Id = 5, Nome = "Luiz Fernando Souza", Cpf = "567.890.123-44", Telefone = "(11) 94321-0987", Email = "luiz.fernando@email.com", DataNascimento = new DateTime(1950, 1, 30), Endereco = "Rua Clodomiro Amazonas, 350 - Vila Nova Conceição, São Paulo - SP" },
        new Cliente { Id = 6, Nome = "Patrícia Helena Rocha", Cpf = "678.901.234-55", Telefone = "(11) 93210-9876", Email = "patricia.rocha@email.com", DataNascimento = new DateTime(1983, 7, 15), Endereco = "Av. Faria Lima, 2500 - Pinheiros, São Paulo - SP" },
        new Cliente { Id = 7, Nome = "Roberto Albuquerque", Cpf = "789.012.345-66", Telefone = "(11) 92109-8765", Email = "roberto.albu@email.com", DataNascimento = new DateTime(1970, 9, 5), Endereco = "Rua Bela Cintra, 1800 - Consolação, São Paulo - SP" },
        new Cliente { Id = 8, Nome = "Juliana Vasconcelos", Cpf = "890.123.456-77", Telefone = "(11) 91098-7654", Email = "juliana.vasc@email.com", DataNascimento = new DateTime(1995, 3, 22), Endereco = "Av. Europa, 600 - Jardim Europa, São Paulo - SP" },
        new Cliente { Id = 9, Nome = "Carlos Henrique Lima", Cpf = "901.234.567-88", Telefone = "(11) 90987-6543", Email = "carlos.henrique@email.com", DataNascimento = new DateTime(1960, 12, 10), Endereco = "Rua Haddock Lobo, 900 - Cerqueira César, São Paulo - SP" },
        new Cliente { Id = 10, Nome = "Sofia Ramos Guimarães", Cpf = "012.345.678-99", Telefone = "(11) 99876-5432", Email = "sofia.ramos@email.com", DataNascimento = new DateTime(2001, 10, 29), Endereco = "Rua Pamplona, 1400 - Jardim Paulista, São Paulo - SP" }
    };

    public static List<Aplicador> Aplicadores { get; } = new()
    {
        new Aplicador { Id = 1, Nome = "Dr. João Mendes", Crf = "CRF/SP 12345", Especialidade = "Vacinas e Imunobiológicos", Telefone = "(11) 91111-2222", Email = "joao.mendes@farmaciapremium.com.br", StatusDisponibilidade = StatusDisponibilidade.Disponivel },
        new Aplicador { Id = 2, Nome = "Dra. Mariana Costa", Crf = "CRF/SP 23456", Especialidade = "Injetáveis e Hormonioterapia", Telefone = "(11) 92222-3333", Email = "mariana.costa@farmaciapremium.com.br", StatusDisponibilidade = StatusDisponibilidade.Disponivel },
        new Aplicador { Id = 3, Nome = "Dr. Ricardo Prado", Crf = "CRF/SP 34567", Especialidade = "Medicamentos Especiais e Oncologia", Telefone = "(11) 93333-4444", Email = "ricardo.prado@farmaciapremium.com.br", StatusDisponibilidade = StatusDisponibilidade.Disponivel },
        new Aplicador { Id = 4, Nome = "Dra. Gabriela Souza", Crf = "CRF/SP 45678", Especialidade = "Pediatria e Imunobiológicos", Telefone = "(11) 94444-5555", Email = "gabriela.souza@farmaciapremium.com.br", StatusDisponibilidade = StatusDisponibilidade.Ocupado },
        new Aplicador { Id = 5, Nome = "Dr. Felipe Antunes", Crf = "CRF/SP 56789", Especialidade = "Apoio Geral e Injetáveis", Telefone = "(11) 95555-6666", Email = "felipe.antunes@farmaciapremium.com.br", StatusDisponibilidade = StatusDisponibilidade.Ferias }
    };

    public static List<Agendamento> Agendamentos { get; }

    static DadosMock()
    {
        var hoje = DateTime.Today;

        Agendamentos = new List<Agendamento>
        {
            // Hoje
            new Agendamento
            {
                Id = 1,
                Cliente = Clientes[0], // Antônio
                Aplicador = Aplicadores[0], // João Mendes
                Medicamento = "Ozempic 1mg",
                LocalAplicacao = "Sala de Injeção Premium",
                Data = hoje,
                Hora = new TimeSpan(9, 0, 0),
                DetalhesAplicacao = "Aplicação subcutânea no abdômen. Lote: 12345A.",
                Observacoes = "Paciente relata leve náusea nas aplicações anteriores.",
                Status = StatusAgendamento.Concluido,
                DataCriacao = hoje.AddDays(-2),
                DataAtualizacao = hoje
            },
            new Agendamento
            {
                Id = 2,
                Cliente = Clientes[1], // Maria Eduarda
                Aplicador = Aplicadores[1], // Mariana Costa
                Medicamento = "Depo-Provera 150mg",
                LocalAplicacao = "Sala de Injeção Premium",
                Data = hoje,
                Hora = new TimeSpan(10, 0, 0),
                DetalhesAplicacao = "Aplicação intramuscular glútea profunda.",
                Observacoes = "Realizar agendamento recorrente trimestral.",
                Status = StatusAgendamento.EmAndamento,
                DataCriacao = hoje.AddDays(-5),
                DataAtualizacao = hoje
            },
            new Agendamento
            {
                Id = 3,
                Cliente = Clientes[2], // João Pedro
                Aplicador = Aplicadores[0], // João Mendes
                Medicamento = "Vacina Tetravalente Influenza",
                LocalAplicacao = "Sala de Imunização Infantil",
                Data = hoje,
                Hora = new TimeSpan(11, 0, 0),
                DetalhesAplicacao = "Aplicação intramuscular no deltoide esquerdo.",
                Observacoes = "Paciente tem histórico de alergia a ovo leve.",
                Status = StatusAgendamento.Confirmado,
                DataCriacao = hoje.AddDays(-1),
                DataAtualizacao = hoje
            },
            new Agendamento
            {
                Id = 4,
                Cliente = Clientes[3], // Ana Beatriz
                Aplicador = Aplicadores[2], // Ricardo Prado
                Medicamento = "Miacalcic 200 UI",
                LocalAplicacao = "Sala de Injeção Premium",
                Data = hoje,
                Hora = new TimeSpan(14, 30, 0),
                DetalhesAplicacao = "Aplicação intramuscular no glúteo.",
                Observacoes = "Checar pressão arterial antes da aplicação.",
                Status = StatusAgendamento.Agendado,
                DataCriacao = hoje.AddDays(-3),
                DataAtualizacao = hoje
            },
            new Agendamento
            {
                Id = 5,
                Cliente = Clientes[4], // Luiz Fernando
                Aplicador = Aplicadores[1], // Mariana Costa
                Medicamento = "Benegrip Injetável",
                LocalAplicacao = "Sala de Injeção Premium",
                Data = hoje,
                Hora = new TimeSpan(16, 0, 0),
                DetalhesAplicacao = "Aplicação intramuscular profunda.",
                Observacoes = "Recomendar repouso pós-aplicação.",
                Status = StatusAgendamento.Agendado,
                DataCriacao = hoje,
                DataAtualizacao = hoje
            },
            new Agendamento
            {
                Id = 6,
                Cliente = Clientes[5], // Patrícia Helena
                Aplicador = Aplicadores[0], // João Mendes
                Medicamento = "Voltaren 75mg/3ml",
                LocalAplicacao = "Sala de Injeção Premium",
                Data = hoje,
                Hora = new TimeSpan(17, 30, 0),
                DetalhesAplicacao = "Intramuscular profunda no quadrante superior externo do glúteo.",
                Observacoes = "Paciente relata dor aguda na lombar.",
                Status = StatusAgendamento.Cancelado,
                DataCriacao = hoje,
                DataAtualizacao = hoje
            },

            // Amanhã
            new Agendamento
            {
                Id = 7,
                Cliente = Clientes[6], // Roberto
                Aplicador = Aplicadores[1], // Mariana Costa
                Medicamento = "Saxenda 6mg/ml",
                LocalAplicacao = "Sala de Injeção Premium",
                Data = hoje.AddDays(1),
                Hora = new TimeSpan(8, 30, 0),
                DetalhesAplicacao = "Acompanhamento de dosagem. Subcutânea.",
                Observacoes = "Primeira dose supervisionada na farmácia.",
                Status = StatusAgendamento.Confirmado,
                DataCriacao = hoje.AddDays(-1),
                DataAtualizacao = hoje
            },
            new Agendamento
            {
                Id = 8,
                Cliente = Clientes[7], // Juliana
                Aplicador = Aplicadores[2], // Ricardo Prado
                Medicamento = "Stelara 90mg",
                LocalAplicacao = "Sala de Medicamentos Especiais",
                Data = hoje.AddDays(1),
                Hora = new TimeSpan(10, 30, 0),
                DetalhesAplicacao = "Aplicação subcutânea. Medicamento biológico especial.",
                Observacoes = "Manter em rede de frio até o momento da aplicação.",
                Status = StatusAgendamento.Confirmado,
                DataCriacao = hoje.AddDays(-7),
                DataAtualizacao = hoje
            },
            new Agendamento
            {
                Id = 9,
                Cliente = Clientes[8], // Carlos Henrique
                Aplicador = Aplicadores[0], // João Mendes
                Medicamento = "Noripurum Injetável EV",
                LocalAplicacao = "Sala de Injeção Premium",
                Data = hoje.AddDays(1),
                Hora = new TimeSpan(14, 0, 0),
                DetalhesAplicacao = "Aplicação intravenosa lenta.",
                Observacoes = "Realizada em parceria com equipe de enfermagem de apoio.",
                Status = StatusAgendamento.Agendado,
                DataCriacao = hoje.AddDays(-4),
                DataAtualizacao = hoje
            },
            new Agendamento
            {
                Id = 10,
                Cliente = Clientes[9], // Sofia Ramos
                Aplicador = Aplicadores[1], // Mariana Costa
                Medicamento = "Rocefin 1g IM",
                LocalAplicacao = "Sala de Injeção Premium",
                Data = hoje.AddDays(1),
                Hora = new TimeSpan(15, 30, 0),
                DetalhesAplicacao = "Diluir em lidocaína 1%. Intramuscular glútea.",
                Observacoes = "Receita retida de controle especial.",
                Status = StatusAgendamento.Agendado,
                DataCriacao = hoje,
                DataAtualizacao = hoje
            },

            // Depois de amanhã
            new Agendamento
            {
                Id = 11,
                Cliente = Clientes[0],
                Aplicador = Aplicadores[2],
                Medicamento = "Deca-Durabolin 50mg",
                LocalAplicacao = "Sala de Injeção Premium",
                Data = hoje.AddDays(2),
                Hora = new TimeSpan(9, 30, 0),
                DetalhesAplicacao = "Intramuscular profunda.",
                Observacoes = "Receita controlada.",
                Status = StatusAgendamento.Agendado,
                DataCriacao = hoje.AddDays(-1),
                DataAtualizacao = hoje
            },
            new Agendamento
            {
                Id = 12,
                Cliente = Clientes[3],
                Aplicador = Aplicadores[0],
                Medicamento = "Beline 100mcg",
                LocalAplicacao = "Sala de Imunização Infantil",
                Data = hoje.AddDays(2),
                Hora = new TimeSpan(11, 30, 0),
                DetalhesAplicacao = "Subcutânea profunda.",
                Observacoes = "Nenhuma.",
                Status = StatusAgendamento.Agendado,
                DataCriacao = hoje.AddDays(-2),
                DataAtualizacao = hoje
            },

            // Ontem (Histórico)
            new Agendamento
            {
                Id = 13,
                Cliente = Clientes[4],
                Aplicador = Aplicadores[1],
                Medicamento = "Benacetil 1.200.000 UI",
                LocalAplicacao = "Sala de Injeção Premium",
                Data = hoje.AddDays(-1),
                Hora = new TimeSpan(10, 0, 0),
                DetalhesAplicacao = "Intramuscular glútea profunda. Diluição padrão.",
                Observacoes = "Aplicação bastante dolorosa, paciente necessitou de repouso temporário.",
                Status = StatusAgendamento.Concluido,
                DataCriacao = hoje.AddDays(-3),
                DataAtualizacao = hoje.AddDays(-1)
            },
            new Agendamento
            {
                Id = 14,
                Cliente = Clientes[5],
                Aplicador = Aplicadores[0],
                Medicamento = "Vacina Hepatite B",
                LocalAplicacao = "Sala de Imunização Infantil",
                Data = hoje.AddDays(-1),
                Hora = new TimeSpan(16, 0, 0),
                DetalhesAplicacao = "Intramuscular deltoide.",
                Observacoes = "Segunda dose do esquema vacinal.",
                Status = StatusAgendamento.Concluido,
                DataCriacao = hoje.AddDays(-10),
                DataAtualizacao = hoje.AddDays(-1)
            },

            // Outros dias da semana
            new Agendamento { Id = 15, Cliente = Clientes[6], Aplicador = Aplicadores[1], Medicamento = "Ozempic 1mg", LocalAplicacao = "Sala de Injeção Premium", Data = hoje.AddDays(3), Hora = new TimeSpan(9, 0, 0), DetalhesAplicacao = "Subcutânea abdômen.", Status = StatusAgendamento.Agendado, DataCriacao = hoje.AddDays(-1) },
            new Agendamento { Id = 16, Cliente = Clientes[7], Aplicador = Aplicadores[2], Medicamento = "Humira 40mg", LocalAplicacao = "Sala de Medicamentos Especiais", Data = hoje.AddDays(3), Hora = new TimeSpan(14, 0, 0), DetalhesAplicacao = "Subcutânea coxa.", Status = StatusAgendamento.Agendado, DataCriacao = hoje.AddDays(-5) },
            new Agendamento { Id = 17, Cliente = Clientes[8], Aplicador = Aplicadores[0], Medicamento = "Vacina Meningocócica ACWY", LocalAplicacao = "Sala de Imunização Infantil", Data = hoje.AddDays(4), Hora = new TimeSpan(10, 0, 0), DetalhesAplicacao = "Intramuscular deltoide.", Status = StatusAgendamento.Agendado, DataCriacao = hoje.AddDays(-2) },
            new Agendamento { Id = 18, Cliente = Clientes[9], Aplicador = Aplicadores[1], Medicamento = "Clexane 40mg", LocalAplicacao = "Sala de Injeção Premium", Data = hoje.AddDays(4), Hora = new TimeSpan(15, 0, 0), DetalhesAplicacao = "Subcutânea abdômen.", Status = StatusAgendamento.Agendado, DataCriacao = hoje.AddDays(-1) },
            new Agendamento { Id = 19, Cliente = Clientes[1], Aplicador = Aplicadores[2], Medicamento = "Prolia 60mg", LocalAplicacao = "Sala de Medicamentos Especiais", Data = hoje.AddDays(5), Hora = new TimeSpan(11, 0, 0), DetalhesAplicacao = "Subcutânea coxa.", Status = StatusAgendamento.Agendado, DataCriacao = hoje.AddDays(-6) },
            new Agendamento { Id = 20, Cliente = Clientes[2], Aplicador = Aplicadores[0], Medicamento = "Vacina Tríplice Viral", LocalAplicacao = "Sala de Imunização Infantil", Data = hoje.AddDays(5), Hora = new TimeSpan(16, 30, 0), DetalhesAplicacao = "Subcutânea deltoide.", Status = StatusAgendamento.Agendado, DataCriacao = hoje.AddDays(-1) }
        };
    }
}
