using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Quest
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int ExperienciaLoot { get; set; }
        public int DinheiroLoot { get; set; }
        public List<QuestCompletionItem> QuestCompletionItems { get; set; }
        public Item Recompensa { get; set; }

        public Quest(int id, string nome, string descricao, int experiencia, int dinheiro)
        {
            ID = id;
            Nome = nome;
            Descricao = descricao;
            ExperienciaLoot = experiencia;
            DinheiroLoot = dinheiro;
            QuestCompletionItems = new List<QuestCompletionItem>();
        }
    }
}
