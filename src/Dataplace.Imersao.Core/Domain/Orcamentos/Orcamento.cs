using Dataplace.Imersao.Core.Domain.Excepions;
using Dataplace.Imersao.Core.Domain.Orcamentos.Enums;
using Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects;
using System;
using System.Collections.Generic;

namespace Dataplace.Imersao.Core.Domain.Orcamentos
{
    public class Orcamento
    {
        private Orcamento(
                        string cdEmpresa, 
                        string cdFilial, 
                        int numOrcamento, 
                        OrcamentoCliente cliente, 
                        Usuario usuario, 
                        OrcamentoVendedor vendedor, 
                        OrcamentoTabelaPreco tabelaPreco, 
                        OrcamentoStatusEnum situacao,
                        ICollection<OrcamentoItem> itens)
        {

            CdEmpresa = cdEmpresa;
            CdFilial = cdFilial;
            Cliente = cliente;
            NumOrcamento = numOrcamento;
            Usuario = usuario;
            Vendedor = vendedor;
            TabelaPreco = tabelaPreco;
            Situacao = situacao;
            Itens = itens;

            // default
            //Situacao = OrcamentoStatusEnum.Aberto;
            DtOrcamento = DateTime.Now;
            ValorTotal = 0;
            Itens = new List<OrcamentoItem>();
            DefinirValidade(365);
        }

        public string CdEmpresa { get; private set; }
        public string CdFilial { get; private set; }
        public int NumOrcamento { get; private set; }
        public OrcamentoCliente Cliente { get; private set; }
        public DateTime DtOrcamento { get; private set; }
        public decimal ValorTotal { get; private set; }
        public OrcamentoValidade Validade { get; private set; }
        public OrcamentoTabelaPreco TabelaPreco { get; private set; }
        public DateTime? DtFechamento { get; private set; }
        public OrcamentoVendedor Vendedor { get; private set; }
        public Usuario Usuario { get; private set; }
        public OrcamentoStatusEnum Situacao { get; private set; }
        public ICollection<OrcamentoItem> Itens { get; private set; }

        
        public void FecharOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Fechado)
                throw new DomainException("Orçamento já está fechado!");

            Situacao = OrcamentoStatusEnum.Fechado;
            DtFechamento = DateTime.Now.Date;
        }

        public void ReabrirOrcamento()
        {
            if (Situacao == OrcamentoStatusEnum.Aberto)
                throw new DomainException("Orçamento já está aberto!");

            Situacao = OrcamentoStatusEnum.Aberto;
            DtFechamento = null;
        }

        public void CancelarOrcamento()
        {
            if (Situacao != OrcamentoStatusEnum.Aberto)
                throw new DomainException("Orçamento não esta aberto!");
            
            Situacao = OrcamentoStatusEnum.Cancelado;
        }

        public void InserirProduto()
        {
            if (Situacao != OrcamentoStatusEnum.Aberto)
                throw new DomainException("Orçamento não esta aberto!");
        }

        public void DefinirValidade(int diasValidade)
        {
            this.Validade = new OrcamentoValidade(this, diasValidade);
        }

        #region Validations

        public List<string> Validations;
        public bool IsValid()
        {
            Validations = new List<string>();

            if (string.IsNullOrWhiteSpace(CdEmpresa))
                Validations.Add("Código da empresa é requirido!");

            if (string.IsNullOrWhiteSpace(CdFilial))
                Validations.Add("Código da filial é requirido!");

            if (Vendedor == null)
                Validations.Add("Vendedor é requerido!");

            if (Cliente == null)
                Validations.Add("Cliente é requerido!");

            if (Usuario == null)
                Validations.Add("Usuário é requerido!");

            if (Validade == null)
                Validations.Add("Validade é requerido!");

            if (TabelaPreco == null)
                Validations.Add("Tabela de preço é requerido!");

            if (Validade == null)
                Validations.Add("Validade é requerido!");

            if (Itens == null)
                Validations.Add("Item é requerido!");

            return !(Validations.Count > 0);
        }

        #endregion

        #region Factory Methods
        public static class Factory
        {

            public static Orcamento Orcamento(
                                            string cdEmpresa, 
                                            string cdFilial, 
                                            int numOrcamento, 
                                            OrcamentoCliente cliente , 
                                            Usuario usuario, 
                                            OrcamentoVendedor vendedor, 
                                            OrcamentoTabelaPreco tabelaPreco,
                                            OrcamentoStatusEnum situacao,
                                            ICollection<OrcamentoItem> itens)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, cliente, usuario, vendedor, tabelaPreco, situacao, itens);
            }
            public static Orcamento OrcamentoRapido(
                                                    string cdEmpresa, 
                                                    string cdFilial, 
                                                    int numOrcamento, 
                                                    Usuario usuario, 
                                                    OrcamentoVendedor vendedor, 
                                                    OrcamentoTabelaPreco tabelaPreco,
                                                    OrcamentoStatusEnum situacao,
                                                    ICollection<OrcamentoItem> itens)
            {
                return new Orcamento(cdEmpresa, cdFilial, numOrcamento, null, usuario, vendedor, tabelaPreco, situacao, itens);
            }
        }

        #endregion
    }
}
