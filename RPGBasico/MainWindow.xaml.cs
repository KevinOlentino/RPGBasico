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
        private Player _player;
        private Monstro _Monstro;

        private Location location;

        public MainWindow()
        {
            InitializeComponent();

            _player = new Player("Kevin", 1, 2, 3, 4, 5);

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

        private void btnUsePotion_Click(object sender, RoutedEventArgs e)
        {
            _player.Inventario.Add(new ItemInventario(Mundo.ItembyId2(2), 1));
        }

        private void BtnUseWeapon_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MoveTo(Location NewLocation)
        {
            

            _player.CurrentLocation = NewLocation;
            BtnLeste.IsEnabled = (NewLocation.LocalizacaoLeste != null);
            BtnOeste.IsEnabled = (NewLocation.LocalizacaoOeste != null);
            BtnSul.IsEnabled = (NewLocation.LocalizacaoSul != null);
            BtnNorte.IsEnabled = (NewLocation.LocalizacaoNorte != null);

            RbtLocation.Document.Blocks.Clear();
            RbtLocation.Document.Blocks.Add(new Paragraph(new Run(NewLocation.Nome)));
            RbtLocation.Document.Blocks.Add(new Paragraph(new Run(NewLocation.Descricao)));
           
            
            QuestInLocation(NewLocation);
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
                    }
                    if (playerquest.IsCompleted)
                    {
                        PlayerCompleteQuest = true;
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
                    _player.Quest.Add(new PlayerQuest(CurrentLocation.QuestsDisponiveis));
                }
                else
                {
                    QuestCompleteVerify(PlayerCompleteQuest);
                }                                        
            }
            else
            {
                Console.WriteLine("Local não tem Quest");
            }
        }

        private void QuestCompleteVerify(bool QuestIsCompleted)
        {
            bool HasTheItems = false;
            bool FoundItem = true;

            if (!QuestIsCompleted)
            {
                foreach(QuestCompletionItem qci in location.QuestsDisponiveis.QuestCompletionItems)
                {
                    foreach(ItemInventario hqi in _player.Inventario)
                    {
                        if(hqi.Details.ID == qci.Details.ID)
                        {
                            FoundItem = true;

                            if(hqi.Quantidade < qci.Quantidade)
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
                    foreach(QuestCompletionItem qci in location.QuestsDisponiveis.QuestCompletionItems)
                    {
                        foreach(ItemInventario item in _player.Inventario)
                        {
                            if(item.Details.ID == qci.Details.ID)
                            {
                                item.Quantidade -= qci.Quantidade;
                                break;
                            }
                        }
                    }

                    _player.Experiencia += location.QuestsDisponiveis.ExperienciaLoot;
                    _player.Dinheiro += location.QuestsDisponiveis.DinheiroLoot;
                    
                    AddItemToInvetory(location.QuestsDisponiveis.Recompensa);
                    MarkHasCompleted(location.QuestsDisponiveis);
                }
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
    }    
}
