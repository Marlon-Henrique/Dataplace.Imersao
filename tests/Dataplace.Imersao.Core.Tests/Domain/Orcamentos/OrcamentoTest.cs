using Dataplace.Imersao.Core.Domain.Excepions;
using Dataplace.Imersao.Core.Domain.Orcamentos;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using Dataplace.Imersao.Core.Tests.Fixtures;
using System;
using Xunit;

namespace Dataplace.Imersao.Core.Tests.Domain.Orcamentos
{
    [Collection(nameof(OrcamentoCollection))]
    public class OrcamentoTest
    {
        private readonly OrcamentoFixture _fixture;
        public OrcamentoTest(OrcamentoFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void NovoOrcamentoDevePossuirValoresValidos()
        {
            // Arrange Act
            var orcamento = _fixture.NovoOrcamento();

            // Assert
            Assert.True(orcamento.CdEmpresa == _fixture.CdEmpresa);
            Assert.True(orcamento.CdFilial == _fixture.CdFilial);
            Assert.Equal(_fixture.NumOrcamento, orcamento.NumOrcamento);
            Assert.True(orcamento.Cliente.Codigo == _fixture.Cliente.Codigo);
            Assert.True(orcamento.Usuario == _fixture.UserName);
            Assert.True(orcamento.Usuario == _fixture.UserName);
            Assert.True(orcamento.Situacao == Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Aberto);
            Assert.NotNull(orcamento.Validade);
            Assert.NotNull(orcamento.TabelaPreco);
            Assert.Equal(_fixture.TabelaPreco.CdTabela, orcamento.TabelaPreco.CdTabela);
            Assert.Equal(_fixture.TabelaPreco.SqTabela, orcamento.TabelaPreco.SqTabela);
        }

        [Fact]
        public void FecharOrcamentoDeveRetornarStatusFechado()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();

            // Act
            orcamento.FecharOrcamento();

            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Fechado, orcamento.Situacao);
            Assert.NotNull(orcamento.DtFechamento);
        }

        [Fact]
        public void TentarFecharOrcamentoJaFechadoRetornarException()
        {
            // arrange
            var orcamento = _fixture.NovoOrcamento();
            orcamento.FecharOrcamento();

            // act & assert
            Assert.Throws<DomainException>(() => orcamento.FecharOrcamento());
        }

        [Fact]
        public void ReabrirOrcamentoFechadoDeveRetornarStatusAberto()
        {
            // Arrange
            var orcamento = _fixture.OrcamentoFechado();

            // Act
            orcamento.ReabrirOrcamento();

            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Aberto, orcamento.Situacao);
            Assert.Null(orcamento.DtFechamento);
        }

        [Fact]
        public void DefinirPrazoDeValidadeNoOrcamento()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();

            // Act
            orcamento.DefinirValidade(365);

            // Assert
            Assert.NotNull(orcamento.Validade);
        }

        [Fact]
        public void ValidarCamposRequeridosDoOrcamento()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();

            // Act
            orcamento.IsValid();

            // Assert
            Assert.True(orcamento.Validations.Count == 0);
        }

        [Fact]
        public void CancelarOrcamentoComStatusAberto()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();

            // Act
            orcamento.CancelarOrcamento();

            // Assert
            Assert.Equal(Core.Domain.Orcamentos.Enums.OrcamentoStatusEnum.Cancelado, orcamento.Situacao);
        }

        [Fact]
        public void InserirItensNoOrcamentoAberto()
        {
            // Arrange
            var orcamento = _fixture.NovoOrcamento();

            // Act
            orcamento.InserirProduto();

            // Assert
            Assert.True(orcamento.Itens != null);
        }


    }
}
