using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos
{
    public class OrcamentoItem
    {

        public OrcamentoItem(
                            string cdEmpresa,
                            string cdFilial,
                            int numOrcamento,
                            OrcamentoProduto produto,
                            decimal quantidade,
                            OrcamentoItemPrecoTotal orcamentoItemPrecoTotal)
        {
            CdEmpresa = cdEmpresa;
            CdFilial = cdFilial;
            NumOrcamento = numOrcamento;
            Produto = produto;
            Quantidade = quantidade;
            AtrubuirPreco(orcamentoItemPrecoTotal);

            IsValid();
        }

        public int Seq { get; private set; }
        public string CdEmpresa { get; private set; }
        public string CdFilial { get; private set; }
        public int NumOrcamento { get; private set; }
        public OrcamentoProduto Produto { get; private set; }
        public decimal Quantidade { get; private set; }
        public OrcamentoItemPrecoTotal ItemPrecoTotal { get; private set; }
        public OrcamentoItemPrecoPercentual ItemPrecoPercentual { get; private set; }
        public decimal Total { get; private set; }
        public OrcamentoItemStatusEnum Situacao { get; private set; }


        #region setters
        private void AtrubuirPreco(OrcamentoItemPrecoTotal itemPrecoTotal)
        {
            ItemPrecoTotal = itemPrecoTotal;
            CalcularTotal();
        }

        private void CalcularTotal()
        {
            if (Quantidade < 0)
                throw new ArgumentOutOfRangeException(nameof(Quantidade));

            if (ItemPrecoTotal.PrecoVenda < 0)
                new ArgumentOutOfRangeException(nameof(ItemPrecoTotal.PrecoVenda));

            Total = Quantidade * ItemPrecoTotal.PrecoVenda;
        }
        #endregion

        #region Valitadions
        public List<string> Validations;
        public bool IsValid()
        {
            Validations = new List<string>();

            if (string.IsNullOrWhiteSpace(CdEmpresa))
                Validations.Add("Código da empresa é requirido!");

            if (string.IsNullOrWhiteSpace(CdFilial))
                Validations.Add("Código da filial é requirido!");

            if (Seq <= 0)
                Validations.Add("Sequência é requirido!");

            if (Seq <= 0)
                Validations.Add("Número do Orçamento é requirido!");

            if (ItemPrecoTotal == null)
                Validations.Add("Precificação é requirido!");

            return !(Validations.Count > 0);
        }
        #endregion

        #region Factory Methods
        public static class Factory
        {
            public static OrcamentoItem OrcamentoItem(
                                            string cdEmpresa,
                                            string cdFilial,
                                            int numOrcamento,
                                            OrcamentoProduto produto,
                                            decimal quantidade,
                                            OrcamentoItemPrecoTotal orcamentoItemPrecoTotal)
            {
                return new OrcamentoItem(cdEmpresa, cdFilial, numOrcamento, produto, quantidade, orcamentoItemPrecoTotal);
            }
        }
        #endregion


    }
}
