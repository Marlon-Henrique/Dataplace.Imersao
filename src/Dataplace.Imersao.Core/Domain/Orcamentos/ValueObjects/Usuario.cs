using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dataplace.Imersao.Core.Domain.Orcamentos.ValueObjects
{
    public class Usuario
    {
        public Usuario(string username)
        {
            UserName = username;
        }

        public string UserName { get; }
    }
}
