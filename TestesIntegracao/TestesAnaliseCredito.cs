using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using FluentAssertions;

namespace ConsultaCredito.Testes
{
    public class TestesAnaliseCredito
    {
        private readonly Mock<IServicoConsultaCredito> mock;

        private const string CPF_INVALIDO = "123A";
        private const string CPF_ERRO_COMUNICACAO = "76217486300";
        private const string CPF_SEM_PENDENCIAS = "60487583752";
        private const string CPF_INADIMPLENTE = "82226651209";

        public TestesAnaliseCredito()
        {
            mock = new (MockBehavior.Strict);

            mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_INVALIDO))
                .Returns(() => null);

            mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_ERRO_COMUNICACAO))
                .Throws(new ("Testando erro de comunicação"));

            mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_SEM_PENDENCIAS))
                .Returns(() => new List<Pendencia>());

            Pendencia pendencia = new ()
            {
                CPF = CPF_INADIMPLENTE,
                NomePessoa = "Cliente Teste",
                NomeReclamante = "Empresas ACME",
                DescricaoPendencia = "Parcela não paga",
                VlPendencia = 900.50
            };
            List<Pendencia> pendencias = new ();
            pendencias.Add(pendencia);

            mock.Setup(s => s.ConsultarPendenciasPorCPF(CPF_INADIMPLENTE))
                .Returns(() => pendencias);
        }

        private StatusConsultaCredito ObterStatusAnaliseCredito(string cpf)
        {
            AnaliseCredito analise = new (mock.Object);
            return analise.ConsultarSituacaoCPF(cpf);
        }

        [Fact]
        public void TestarCPFInvalidoMoq()
        {
            StatusConsultaCredito status =
                ObterStatusAnaliseCredito(CPF_INVALIDO);
            status.Should().Be(StatusConsultaCredito.ParametroEnvioInvalido,
                "Resultado incorreto para um CPF inválido");
        }

        [Fact]
        public void TestarErroComunicacaoMoq()
        {
            StatusConsultaCredito status =
                ObterStatusAnaliseCredito(CPF_ERRO_COMUNICACAO);
            status.Should().Be(StatusConsultaCredito.ErroComunicacao,
                "Resultado incorreto para um erro de comunicação");
        }

        [Fact]
        public void TestarCPFSemPendenciasMoq()
        {
            StatusConsultaCredito status =
                ObterStatusAnaliseCredito(CPF_SEM_PENDENCIAS);
            status.Should().Be(StatusConsultaCredito.SemPendencias,
                "Resultado incorreto para um CPF sem pendências");
        }

        [Fact]
        public void TestarCPFInadimplenteMoq()
        {
            StatusConsultaCredito status =
                ObterStatusAnaliseCredito(CPF_INADIMPLENTE);
            status.Should().Be(StatusConsultaCredito.Inadimplente,
                "Resultado incorreto para um CPF inadimplente");
        }
    }
}