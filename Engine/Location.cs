using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Location
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }    
        public Item ItemRequiridoParaAcesso { get; set; }
        public Quest QuestsDisponiveis { get; set; }
        public Monstro MonstrosNessaArea { get; set; }
        public Location LocalizacaoNorte { get; set; }
        public Location LocalizacaoSul { get; set; }
        public Location LocalizacaoLeste { get; set; }
        public Location LocalizacaoOeste { get; set; }

        public Location(int id, string nome, string descricao, Item itemRequeridoParaAcesso = null, Quest questsDisponiveis = null, Monstro monstrosNaArea = null)
        {
            ID = id;
            Nome = nome;
            Descricao = descricao;
            ItemRequiridoParaAcesso = itemRequeridoParaAcesso;
            QuestsDisponiveis = questsDisponiveis;
            MonstrosNessaArea = monstrosNaArea;
        }
    }


}
