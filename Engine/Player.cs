using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Player : LivingCreatures
    {
        public string Nome { get; set; }     
        public int Experiencia { get; set; }
        public int Dinheiro { get; set; }
        public int Level { get; set; }
        public Location CurrentLocation { get; set; }
        public List<ItemInventario> Inventario { get; set; }
        public List<PlayerQuest> Quest { get; set; }

        public Player(string nome, int vida, int vidamaxima, int dinheiro, int experiencia, int level) : base(vida, vidamaxima)
        {
            Nome = nome;
            Experiencia = experiencia;
            Level = level;
            Dinheiro = dinheiro;

            Inventario = new List<ItemInventario>();
            Quest = new List<PlayerQuest>();
        }
    }
}
