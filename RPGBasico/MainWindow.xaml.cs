using Engine;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace RPGBasico
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Player _player;
        private Monstro _Monstro;    
        public MainWindow()
        {
            InitializeComponent();

            _player = new Player("Kevin", 10, 10, 0, 0, 1);

            MoveTo(Mundo.LocationById(Mundo.LOCATION_ID_HOME));
            _player.Inventario.Add(new ItemInventario(Mundo.ItembyId2(1), 1));
            lblVida.Content = _player.Vida.ToString();
            lblLevel.Content = _player.Level.ToString();
            lblNome.Content = _player.Nome.ToString();
            lblDinheiro.Content = _player.Dinheiro.ToString();

        }

        private void BtnNorte_Click(object sender, RoutedEventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocalizacaoNorte);
        }

        private void BtnOeste_Click(object sender, RoutedEventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocalizacaoOeste);
        }

        private void BtnSul_Click(object sender, RoutedEventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocalizacaoSul);
        }

        private void BtnLeste_Click(object sender, RoutedEventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocalizacaoLeste);
        }

        private void BtnUsePotion_Click(object sender, RoutedEventArgs e)
        {
            PocaoDeVida pocao = (PocaoDeVida)CboPocoes.SelectedItem;

            _player.Vida += pocao.QuantidadeDeVida;

            if(_player.Vida > _player.VidaMaxima)
            {
                _player.Vida = _player.VidaMaxima;
            }

            RemoveItem(pocao,1);

            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Você bebeu uma {pocao.Nome} e recebeu {pocao.QuantidadeDeVida}")));

            MonsterHit();
            RefreshLabels();
        }

        private void BtnUseWeapon_Click(object sender, RoutedEventArgs e)
        {
            Weapon CurrentWeapon = (Weapon)CboWeapons.SelectedItem;
            if(CurrentWeapon != null)
            {
                int damageToMonster = RandomNumberGenerator.NumberBetween(CurrentWeapon.DanoMinimo, CurrentWeapon.DanoMaximo);

                _Monstro.Vida -= damageToMonster;

                RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Você hitou o {_Monstro.Nome} com {damageToMonster} de dano")));

                if (_Monstro.Vida <= 0)
                {
                    RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"")));
                    RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Você derrotou {_Monstro.Nome}")));

                    RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Você recebeu {_Monstro.ExperienciaLoot} de experiencia")));
                    RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Vôcê recebeu {_Monstro.DinheiroLoot} de Gold")));
                    _player.Experiencia += _Monstro.ExperienciaLoot;
                    _player.Dinheiro += _Monstro.DinheiroLoot;

                    List<ItemInventario> ItemsDroped = new List<ItemInventario>();

                    foreach (LootItem lootitem in _Monstro.LootTable)
                    {
                        if (RandomNumberGenerator.NumberBetween(1, 100) <= lootitem.DropPercentage)
                        {
                            ItemsDroped.Add(new ItemInventario(lootitem.Details, 1));
                        }
                    }

                    if (ItemsDroped.Count == 0)
                    {
                        foreach (LootItem item in _Monstro.LootTable)
                        {
                            if (item.IsDefaultItem)
                            {
                                ItemsDroped.Add(new ItemInventario(item.Details, 1));
                            }
                        }
                    }

                    foreach (ItemInventario item in ItemsDroped)
                    {
                        AddItemToInvetory(item.Details);
                        if (item.Quantidade == 1)
                        {
                            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Você recebeu {item.Quantidade} {item.Details} ")));
                        }
                        else
                        {
                            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Você recebeu {item.Quantidade} {item.Details}")));
                        }
                    }

                    RefreshLabels();

                    MoveTo(_player.CurrentLocation);

                }
                else
                {
                    MonsterHit();
                }
            }
            else
            {
                RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Você não selecionou uma Arma")));
            }            
        }

        private void MoveTo(Location NewLocation)
        {
            Console.Clear();

            _player.CurrentLocation = NewLocation;
            BtnLeste.IsEnabled = (NewLocation.LocalizacaoLeste != null);
            BtnOeste.IsEnabled = (NewLocation.LocalizacaoOeste != null);
            BtnSul.IsEnabled = (NewLocation.LocalizacaoSul != null);
            BtnNorte.IsEnabled = (NewLocation.LocalizacaoNorte != null);

            RbtLocation.Document.Blocks.Clear();
            RbtLocation.Document.Blocks.Add(new Paragraph(new Run(NewLocation.Nome)));
            RbtLocation.Document.Blocks.Add(new Paragraph(new Run(NewLocation.Descricao)));

            RefreshLabels();
            QuestInLocation(NewLocation);
            MonsterAppear(NewLocation);
        }

        private void QuestInLocation(Location CurrentLocation)
        {
            bool PlayerHasQuest = false;
            bool PlayerCompleteQuest = false;

            if(CurrentLocation.QuestsDisponiveis != null)
            {
                foreach (PlayerQuest playerquest in _player.Quest)
                {
                    if (playerquest.Details.ID == CurrentLocation.QuestsDisponiveis.ID)
                    {
                        PlayerHasQuest = true;

                        if (playerquest.IsCompleted)
                        {
                            PlayerCompleteQuest = true;
                        }
                    }
                }

                if (!PlayerHasQuest)
                {
                    _player.Quest.Add(new PlayerQuest(CurrentLocation.QuestsDisponiveis));
                    RbtMensagem.Document.Blocks.Add(new Paragraph(new Run("Você recebeu a Quest: " + CurrentLocation.QuestsDisponiveis.Nome)));
                    RbtMensagem.Document.Blocks.Add(new Paragraph(new Run(CurrentLocation.QuestsDisponiveis.Descricao)));
                    RbtMensagem.Document.Blocks.Add(new Paragraph(new Run("Para concluir retorne com os seguintes itens:")));

                    foreach (QuestCompletionItem qci in CurrentLocation.QuestsDisponiveis.QuestCompletionItems)
                    {
                        if (qci.Quantidade == 1)
                        {
                            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run(qci.Quantidade.ToString() + " " + qci.Details.Nome)));
                            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run(qci.Quantidade.ToString() + " " + qci.Details.NomePlural)));
                        }
                        else
                        {
                            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run(qci.Quantidade.ToString() + " " + qci.Details.NomePlural)));
                        }
                    }                    
                }
                else
                {
                    if (!PlayerCompleteQuest)
                    {
                        QuestCompleteVerify(CurrentLocation.QuestsDisponiveis);
                    }                    
                }                                        
            }
            else
            {
                Console.WriteLine("Local não tem Quest");
            }
        }

        private void QuestCompleteVerify(Quest quest)
        {
            bool HasTheItems = false;
            bool FoundItem = true;

         
                foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
                {
                    foreach(ItemInventario hqi in _player.Inventario)
                    {
                        if(hqi.Details.ID == qci.Details.ID)
                        {
                            FoundItem = true;

                            if(hqi.Quantidade >= qci.Quantidade)
                            {
                                HasTheItems = true;
                                break;
                            }
                            break;
                        }
                    }

                    if (!FoundItem)
                    {
                        break;
                    }
                }

                if (HasTheItems)
                {
                    foreach(QuestCompletionItem qci in quest.QuestCompletionItems)
                    {
                        RemoveItem(qci.Details, qci.Quantidade);
                    }
                     RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Parabéns você completou a quest:{quest.Nome}")));
                     RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Você recebeu os seguintes items:")));
                     RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"{quest.ExperienciaLoot} de experiência")));
                     RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"{quest.DinheiroLoot} de gold")));
                     RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Você recebeu {quest.Recompensa.Nome}")));                    

                    _player.Experiencia += quest.ExperienciaLoot;
                    _player.Dinheiro += quest.DinheiroLoot;
                
                    AddItemToInvetory(quest.Recompensa);
                    MarkHasCompleted(quest);
                    RefreshLabels();
                }            
        }
        private void AddItemToInvetory(Item itemtoadd)
        {
            foreach(ItemInventario item in _player.Inventario)
            {
                if(item.Details.ID == itemtoadd.ID)
                {
                    item.Quantidade++;

                    return;
                }
            }

            _player.Inventario.Add(new ItemInventario(itemtoadd, 1));
        }

        private void MarkHasCompleted(Quest quest)
        {
            foreach(PlayerQuest pq in _player.Quest)
            {
                if(pq.Details.ID == quest.ID)
                {
                    pq.IsCompleted = true;

                    return;
                }
            }
        }

        public void RefreshInventoryUI()
        {
            foreach(ItemInventario item in _player.Inventario)
            {
                Console.WriteLine($"Nome: {item.Details.Nome} Quantidade: {item.Quantidade}");
         
            }

            dgvInvetory.ItemsSource = _player.Inventario;
            dgvInvetory.Items.Refresh();
        }
        public void RefreshQuestUI()
        {
            foreach (PlayerQuest quest in _player.Quest)
            {
                Console.WriteLine($"Nome: {quest.Details.Nome} Foi Completa: {quest.IsCompleted}");

            }
            foreach(PlayerQuest quest in _player.Quest)
            {
                
            }
           // dgvQuest.ItemsSource = _player.Quest;
            dgvQuest.Items.Refresh();
        }

        public void RefreshWeaponUI()
        {
            List<Weapon> weapons = new List<Weapon>();
            weapons.Clear();

            foreach(ItemInventario item in _player.Inventario)
            {
                if(item.Details is Weapon)
                {
                    if(item.Quantidade > 0)
                    {
                        weapons.Add((Weapon)item.Details);
                    }
                }
            }
            if(weapons.Count == 0)
            {
                CboWeapons.Visibility = Visibility.Hidden;
                BtnUseWeapon.Visibility = Visibility.Hidden;
            }
            else
            {
                CboWeapons.Visibility = Visibility.Visible;
                BtnUseWeapon.Visibility = Visibility.Visible;

                
                CboWeapons.ItemsSource = weapons;
               // CboWeapons.DisplayMemberPath = weapons.;
            }

            weapons.ForEach(d => Console.WriteLine($"Nome: {d.Nome}"));
        }

        public void RefreshPotionUI()
        {
            List<PocaoDeVida> pocaodevida = new List<PocaoDeVida>();
            pocaodevida.Clear();

            foreach(ItemInventario item in _player.Inventario)
            {
                if(item.Details is PocaoDeVida)
                {
                    if(item.Quantidade > 0)
                    {
                        pocaodevida.Add((PocaoDeVida)item.Details);
                    }
                }

                if(pocaodevida.Count == 0)
                {
                    CboPocoes.Visibility = Visibility.Hidden;
                    btnUsePotion.Visibility = Visibility.Hidden;
                    CboPocoes.Items.Refresh();
                }
                else
                {
                    CboPocoes.Visibility = Visibility.Visible;
                    btnUsePotion.Visibility = Visibility.Visible;
                    CboPocoes.ItemsSource = pocaodevida;
                }
            }

            pocaodevida.ForEach(d => Console.WriteLine($"Nome: {d.Nome}"));
        }

        private void MonsterAppear(Location CurrentLocation)
        {
            CurrentLocation = _player.CurrentLocation;
            
            if(CurrentLocation.MonstrosNessaArea != null)
            {
                RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Você encontrou um {CurrentLocation.MonstrosNessaArea.Nome}")));
                Monstro monstro = Mundo.MonstroByID(CurrentLocation.MonstrosNessaArea.ID);
                _Monstro = new Monstro(monstro.ID, monstro.Nome, monstro.DanoMaximo, monstro.ExperienciaLoot, monstro.DinheiroLoot, monstro.Vida, monstro.VidaMaxima);

                foreach (LootItem lootitem in monstro.LootTable)
                {
                    _Monstro.LootTable.Add(lootitem);
                }
            }
            else
            {
                _Monstro = null;
                CboWeapons.Visibility = Visibility.Hidden;
                BtnUseWeapon.Visibility = Visibility.Hidden;
                CboPocoes.Visibility = Visibility.Hidden;
                btnUsePotion.Visibility = Visibility.Hidden;
            }
                        
        }

        public void RefreshLabels()
        {
            lblDinheiro.Content = _player.Dinheiro.ToString();
            lblVida.Content = _player.Vida.ToString();
            lblLevel.Content = _player.Level.ToString();

            RefreshWeaponUI();
            RefreshInventoryUI();
            RefreshPotionUI();
            RefreshQuestUI();
        }

        private void RemoveItem(Item ItemToRemove, int QuantToRemove)
        {
            foreach (ItemInventario item in _player.Inventario)
            {
                if (item.Details.ID == ItemToRemove.ID)
                {
                    item.Quantidade -= QuantToRemove;
                    
                    if (item.Quantidade <= 0)
                    {
                        _player.Inventario.Remove(item);
                    }
                    break;                   
                }
            }
        }
        
        private void MonsterHit()
        {
            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _Monstro.DanoMaximo);

            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"O {_Monstro.Nome} te hitou com {damageToPlayer} de dano")));

            _player.Vida -= damageToPlayer;

            lblVida.Content = _player.Vida.ToString();

            if (_player.Vida <= 0)
            {
                RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"Se fudeu o {_Monstro.Nome} te matou otario")));

                MoveTo(Mundo.LocationById(Mundo.LOCATION_ID_HOME));
                RbtMensagem.Document.Blocks.Add(new Paragraph(new Run($"E ve se limpa essa merda agora ja que como guerreira você é inutil")));
                _player.Vida = _player.VidaMaxima;
            }
        }
    }   
}
