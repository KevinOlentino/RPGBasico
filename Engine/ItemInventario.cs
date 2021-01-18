using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class ItemInventario
    {
        public Item Details { get; set; }
        public int Quantidade { get; set; }

        public ItemInventario (Item details, int quantidade)
        {
            Details = details;
            Quantidade = quantidade;
        }
    }
}
