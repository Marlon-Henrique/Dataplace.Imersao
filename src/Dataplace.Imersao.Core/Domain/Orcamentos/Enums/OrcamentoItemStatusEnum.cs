using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.Enums
{
    public enum OrcamentoItemStatusEnum
    {
        Aberto, Fechado, Cancelado
    }

    public static class OrcamentoItemStatusEnumExtensions
    {
        public static string ToDataValue(this OrcamentoItemStatusEnum value)
        {
            return 
                value == OrcamentoItemStatusEnum.Fechado ? "F" : 
                value == OrcamentoItemStatusEnum.Cancelado ? "C" : "P";
        }
        public static OrcamentoItemStatusEnum ToOrcamentoItemStatusEnum(this string value)
        {
            return 
                string.IsNullOrWhiteSpace(value) ? OrcamentoItemStatusEnum.Aberto : 
                value.Equals('P') ? OrcamentoItemStatusEnum.Fechado : OrcamentoItemStatusEnum.Aberto;
        }
    }
}
