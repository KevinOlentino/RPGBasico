using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Mundo
    {
        public static readonly List<Item> Items = new List<Item>();
        public static readonly List<Monstro> Monstro = new List<Monstro>();
        public static readonly List<Quest> Quests = new List<Quest>();
        public static readonly List<Location> Locations = new List<Location>();

        public const int ITEM_ID_RUSTY_SWORD = 1;
        public const int ITEM_ID_RAT_TAIL = 2;
        public const int ITEM_ID_PIECE_OF_FUR = 3;
        public const int ITEM_ID_SNAKE_FANG = 4;
        public const int ITEM_ID_SNAKESKIN = 5;
        public const int ITEM_ID_CLUB = 6;
        public const int ITEM_ID_HEALING_POTION = 7;
        public const int ITEM_ID_SPIDER_FANG = 8;
        public const int ITEM_ID_SPIDER_SILK = 9;
        public const int ITEM_ID_ADVENTURER_PASS = 10;

        public const int MONSTER_ID_RAT = 1;
        public const int MONSTER_ID_SNAKE = 2;
        public const int MONSTER_ID_GIANT_SPIDER = 3;

        public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
        public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;

        public const int LOCATION_ID_HOME = 1;
        public const int LOCATION_ID_TOWN_SQUARE = 2;
        public const int LOCATION_ID_GUARD_POST = 3;
        public const int LOCATION_ID_ALCHEMIST_HUT = 4;
        public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5;
        public const int LOCATION_ID_FARMHOUSE = 6;
        public const int LOCATION_ID_FARM_FIELD = 7;
        public const int LOCATION_ID_BRIDGE = 8;
        public const int LOCATION_ID_SPIDER_FIELD = 9;

        #region Example

        public enum ItemsEnum
        {
            ITEM_ID_RUSTY_SWORD = 1,
            ITEM_ID_RAT_TAIL,
            ITEM_ID_PIECE_OF_FUR,
            ITEM_ID_SNAKE_FANG,
            ITEM_ID_SNAKESKIN,
            ITEM_ID_CLUB,
            ITEM_ID_HEALING_POTION,
            ITEM_ID_SPIDER_FANG,
            ITEM_ID_SPIDER_SILK,
            ITEM_ID_ADVENTURER_PASS
        }

        

        private static readonly Dictionary<ItemsEnum, Item> itemsDictionary = new Dictionary<ItemsEnum, Item>
        {
            {ItemsEnum.ITEM_ID_RUSTY_SWORD, new Weapon(ITEM_ID_RUSTY_SWORD, "Espada enferrujada", "Espadas enferrujada", 0, 5)},
            {ItemsEnum.ITEM_ID_RAT_TAIL, new Item(ITEM_ID_RAT_TAIL, "Rabo de rato", "Rabos de ratos")}
        };

        public static Item ItembyId2(int id)
        {
            if (itemsDictionary.ContainsKey((ItemsEnum)id))
                return itemsDictionary[(ItemsEnum)id];

            return null;

        }

        #endregion

        static Mundo()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuests();
            PopulateLocations();
        }

        private static void PopulateItems()
        {
            Items.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Espada enferrujada", "Espadas enferrujada", 5, 0));
            Items.Add(new Weapon(11, "Espada de Ferro", "Espadas de Ferro", 10, 2));
            Items.Add(new Item(ITEM_ID_RAT_TAIL, "Rabo de rato", "Rabos de ratos"));
            Items.Add(new Item(ITEM_ID_PIECE_OF_FUR, "Pedaço de pele", "Pedaços de pele"));
            Items.Add(new Item(ITEM_ID_SNAKE_FANG, "Presa de cobra", "Presas de cobra"));
            Items.Add(new Item(ITEM_ID_SNAKESKIN, "Pele de Cobra", "Peles de Cobra"));
            Items.Add(new Weapon(ITEM_ID_CLUB, "Club", "Clubs", 3, 10));
            Items.Add(new PocaoDeVida(ITEM_ID_HEALING_POTION, "Poções de Vida", "Poções de Vida", 5));
            Items.Add(new Item(ITEM_ID_SPIDER_FANG, "Presa de Aranha", "Presas de Aranha"));
            Items.Add(new Item(ITEM_ID_SPIDER_SILK, "Teia de Aranha", "Teia de Aranha"));
            Items.Add(new Item(ITEM_ID_ADVENTURER_PASS, "Passe de Aventura", "Passe de Aventura"));

        }

        private static void PopulateMonsters()
        {
            Monstro rat = new Monstro(MONSTER_ID_RAT, "Rato", 5, 3, 10, 3, 3);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_PIECE_OF_FUR), 75, true));

            Monstro snake = new Monstro(MONSTER_ID_SNAKE, "Cobra", 5, 3, 10, 3, 3);
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 75, false));
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 75, true));

            Monstro giantSpider = new Monstro(MONSTER_ID_GIANT_SPIDER, "Aranha Gigante", 20, 5, 40, 10, 10);
            giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_FANG), 75, true));
            giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_SILK), 25, false));

            Monstro.Add(rat);
            Monstro.Add(snake);
            Monstro.Add(giantSpider);
        }

        private static Item ItemByID(int id)
        {
            foreach(Item item in Items)
            {
                if(item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }
        private static void PopulateQuests()
        {
            Quest clearAlchemistGarden =
                new Quest(
                    QUEST_ID_CLEAR_ALCHEMIST_GARDEN,
                    "Limpe o Jardim do Alquimista",
                    "Mate todos os ratos no jardim do alquimista e traga de volta 3 rabos de rato. Sua recompensa sera uma poção de vida e 10 peças de ouro.", 20, 10);

            clearAlchemistGarden.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_RAT_TAIL), 3));

            clearAlchemistGarden.Recompensa = ItemByID(ITEM_ID_HEALING_POTION);

            Quest clearFarmersField =
                new Quest(
                    QUEST_ID_CLEAR_FARMERS_FIELD,
                    "Limpe o campo do fazendeiro",
                    "Mate cobras no campo da fazenda e traga 3 dentes de cobra. Sua recompensa sera um passe de aventura e 20 peças de ouro;", 20, 20);

            clearFarmersField.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SNAKE_FANG), 3));

            clearFarmersField.Recompensa = ItemByID(ITEM_ID_ADVENTURER_PASS);

            Quests.Add(clearAlchemistGarden);
            Quests.Add(clearFarmersField);
        }

        private static void PopulateLocations()
        {
            // Create each location
            Location home = new Location(LOCATION_ID_HOME, "Casa", "Essa é sua Casa. Isso realmente precisa de uma limpeza ~ew.");

            Location townSquare = new Location(LOCATION_ID_TOWN_SQUARE, "Praça da Cidade", "Você avista uma fonte");

            Location alchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Cabana do Alquimista   ", "Há muitas plantas estranhas na prateleira.")
            {
                QuestsDisponiveis = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN)
            };

            Location alchemistsGarden = new Location(LOCATION_ID_ALCHEMISTS_GARDEN, "Jardim do Alquimista", "Há muitas plantas crescendo por aqui.")
            {
                MonstrosNessaArea = MonstroByID(MONSTER_ID_RAT)
            };

            Location farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Casa da Fazenda", "Há uma pequena casa de fazenda com um fazendeiro em frente.")
            {
                QuestsDisponiveis = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD)
            };

            Location farmersField = new Location(LOCATION_ID_FARM_FIELD, "Campo da Fazenda", "Você ve alguns vegetais crescendo por aqui.")
            {
                MonstrosNessaArea = MonstroByID(MONSTER_ID_SNAKE)
            };

            Location guardPost = new Location(LOCATION_ID_GUARD_POST, "Posto de Guardas", "Há um guarda de aparencia bem forte aqui.", ItemByID(ITEM_ID_ADVENTURER_PASS));

            Location bridge = new Location(LOCATION_ID_BRIDGE, "Ponte", "Uma ponte de pedra atravessnado o grande lago.");

            Location spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Floresta", "Você ve teias de aranha cobrindo as arvores da floresta.")
            {
                MonstrosNessaArea = MonstroByID(MONSTER_ID_GIANT_SPIDER)
            };

            // Link the locations together
            home.LocalizacaoNorte = townSquare;

            townSquare.LocalizacaoNorte = alchemistHut;
            townSquare.LocalizacaoSul = home;
            townSquare.LocalizacaoLeste = guardPost;
            townSquare.LocalizacaoOeste = farmhouse;

            farmhouse.LocalizacaoLeste = townSquare;
            farmhouse.LocalizacaoOeste = farmersField;

            farmersField.LocalizacaoLeste = farmhouse;

            alchemistHut.LocalizacaoSul = townSquare;
            alchemistHut.LocalizacaoNorte = alchemistsGarden;

            alchemistsGarden.LocalizacaoSul = alchemistHut;

            guardPost.LocalizacaoLeste = bridge;
            guardPost.LocalizacaoOeste = townSquare;

            bridge.LocalizacaoOeste = guardPost;
            bridge.LocalizacaoLeste = spiderField;

            spiderField.LocalizacaoOeste = bridge;

            // Add the locations to the static list
            Locations.Add(home);
            Locations.Add(townSquare);
            Locations.Add(guardPost);
            Locations.Add(alchemistHut);
            Locations.Add(alchemistsGarden);
            Locations.Add(farmhouse);
            Locations.Add(farmersField);
            Locations.Add(bridge);
            Locations.Add(spiderField);
        }
        public static Monstro MonstroByID (int id)
        {
            foreach(Monstro monstro in Monstro)
            {
                if(monstro.ID == id)
                {
                    return monstro;
                }
            }
            return null;
        }

        public static Quest QuestByID(int id)
        {
            foreach(Quest quest in Quests)
            {
                if(quest.ID == id)
                {
                    return quest;
                }
            }
            return null;
        }

        public static Location LocationById(int id)
        {
            foreach(Location localizacao in Locations)
            {
                if(localizacao.ID == id)
                {
                        return localizacao;
                }
            }
            return null;
        }

       public static Item ItemById(int id)
        {
            foreach(Item item in Items)
            {
                if(item.ID == id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
