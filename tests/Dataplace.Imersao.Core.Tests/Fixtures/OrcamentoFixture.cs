using Dataplace.Imersao.Core.Domain.Orcamentos;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Dataplace.Imersao.Core.Tests.Fixtures
{
    public class OrcamentoFixture
    {
        internal string CdEmpresa = "IMS";
        internal string CdFilial = "01";
        internal OrcamentoCliente Cliente = new OrcamentoCliente("CLI01");
        internal OrcamentoVendedor Vendedor = new OrcamentoVendedor("VDD01");
        internal Usuario UserName = new Usuario("sym_usuario");
        internal int NumOrcamento = 1000;
        internal OrcamentoTabelaPreco TabelaPreco = new OrcamentoTabelaPreco("2022", 1);
        internal ICollection<OrcamentoItem> Itens = new Collection<OrcamentoItem>() { 
            OrcamentoItem.Factory.OrcamentoItem(
                                                cdEmpresa: "IMS",
                                                cdFilial: "01",
                                                numOrcamento: 1000,
                                                new OrcamentoProduto(TpRegistroEnum.ProdutoFinal, "IMS01"),
                                                10,
                                                new OrcamentoItemPrecoTotal(100, 250)),
            OrcamentoItem.Factory.OrcamentoItem(
                                                cdEmpresa: "IMS",
                                                cdFilial: "01",
                                                numOrcamento: 1000,
                                                new OrcamentoProduto(TpRegistroEnum.ProdutoFinal, "IMS02"),
                                                10,
                                                new OrcamentoItemPrecoTotal(500, 750))
        };


        public Orcamento NovoOrcamento()
        {
            return Orcamento.Factory.Orcamento(
                CdEmpresa, 
                CdFilial,
                NumOrcamento,
                Cliente, 
                UserName,
                Vendedor,
                TabelaPreco,
                OrcamentoStatusEnum.Aberto,
                Itens);
        }

        public Orcamento OrcamentoFechado()
        {
            return Orcamento.Factory.Orcamento(
                CdEmpresa,
                CdFilial,
                NumOrcamento,
                Cliente,
                UserName,
                Vendedor,
                TabelaPreco,
                OrcamentoStatusEnum.Fechado,
                Itens);
        }


    }
}
