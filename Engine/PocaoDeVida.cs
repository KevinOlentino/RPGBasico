using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class PocaoDeVida : Item
    {
        public int QuantidadeDeVida { get; set; }
        
        public PocaoDeVida(int id, string nome, string nomeplural, int quantidadedeheal) : base(id,nome, nomeplural)
        {
            QuantidadeDeVida = quantidadedeheal;
        }
    }

}
