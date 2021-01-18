using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Monstro : LivingCreatures
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public int DanoMaximo { get; set; }
        public int ExperienciaLoot { get; set; }
        public int DinheiroLoot { get; set; }
        public List<LootItem> LootTable { get; set; }

        public Monstro(int id,string nome,int danomaximo, int experiencialoot,int dinheiroloot, int vida,int vidamaxima) : base(vida, vidamaxima)
        {
            ID = id;
            Nome = nome;
            DanoMaximo = danomaximo;
            ExperienciaLoot = experiencialoot;
            DinheiroLoot = dinheiroloot;
            LootTable = new List<LootItem>();
        }
    }
}
