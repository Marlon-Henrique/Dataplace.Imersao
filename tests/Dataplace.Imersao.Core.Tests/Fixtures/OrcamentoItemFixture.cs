using Dataplace.Imersao.Core.Domain.Orcamentos;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Tests.Fixtures
{
    public class OrcamentoItemFixture
    {
        internal string CdEmpresa = "IMS";
        internal string CdFilial = "01";
        internal int NumOrcamento = 1000;
        internal OrcamentoProduto Produto = new OrcamentoProduto(TpRegistroEnum.ProdutoFinal, "IMS01");
        internal OrcamentoItemPrecoTotal ItemPrecoTotal = new OrcamentoItemPrecoTotal(100, 250);

        public OrcamentoItem NovoItem()
        {
            return OrcamentoItem.Factory.OrcamentoItem(CdEmpresa, CdFilial, NumOrcamento, Produto, 10, ItemPrecoTotal);
        }
    }
}
