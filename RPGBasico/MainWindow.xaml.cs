using Engine;
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

        
        public MainWindow()
        {
            InitializeComponent();

            _player = new Player("Kevin", 1, 2, 3, 4, 5);

            MoveTo(Mundo.LocationById(Mundo.LOCATION_ID_HOME));
            _player.Inventario.Add(new ItemInventario(Mundo.ItemById(Mundo.ITEM_ID_RUSTY_SWORD), 1));
            _player.Quest.Add(new PlayerQuest(Mundo.QuestByID(Mundo.QUEST_ID_CLEAR_ALCHEMIST_GARDEN)));
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

            if (NewLocation.QuestsDisponiveis != null)
            {
                bool PlayerTemQuest = false;
                bool PlayerCompletouQuest = false;

                foreach (PlayerQuest playerquest in _player.Quest)
                {
                    if (playerquest.Details.ID == NewLocation.QuestsDisponiveis.ID)
                    {
                        PlayerTemQuest = true;

                        if (playerquest.IsCompleted)
                        {
                            PlayerCompletouQuest = true;
                        }
                    }
                }

                if (PlayerTemQuest)
                {
                    if (!PlayerCompletouQuest)
                    {
                        bool PlayerTemOsItens = false;

                        foreach (QuestCompletionItem qci in NewLocation.QuestsDisponiveis.QuestCompletionItems)
                        {
                            bool AchouItemInventario = false;

                            foreach (ItemInventario HasQeIn in _player.Inventario)
                            {
                                if (HasQeIn.Details.ID == qci.Details.ID)
                                {
                                    AchouItemInventario = true;

                                    if (HasQeIn.Quantidade < qci.Quantidade)
                                    {
                                        PlayerTemOsItens = true;
                                        break;
                                    }

                                    break;
                                }
                            }

                            if (!AchouItemInventario)
                            {
                                PlayerTemOsItens = false;

                                break;
                            }
                        }

                        if (PlayerTemOsItens)
                        {
                            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run("Você completou a quest" + NewLocation.QuestsDisponiveis.Nome)));

                            foreach (QuestCompletionItem qci in NewLocation.QuestsDisponiveis.QuestCompletionItems)
                            {
                                foreach (ItemInventario ii in _player.Inventario)
                                {
                                    if (ii.Details.ID == qci.Details.ID)
                                    {
                                        ii.Quantidade -= qci.Quantidade;
                                        break;
                                    }
                                }
                            }
                            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run("Você recebeu:")));
                            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run(NewLocation.QuestsDisponiveis.Recompensa.ToString())));
                            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run(NewLocation.QuestsDisponiveis.ExperienciaLoot.ToString())));
                            RbtMensagem.Document.Blocks.Add(new Paragraph(new Run(NewLocation.QuestsDisponiveis.DinheiroLoot.ToString())));
                            _player.Experiencia += NewLocation.QuestsDisponiveis.ExperienciaLoot;
                            _player.Dinheiro += NewLocation.QuestsDisponiveis.DinheiroLoot;


                            bool AdicionarItemAoInventario = false;

                            foreach (ItemInventario ii in _player.Inventario)
                            {
                                if (ii.Details.ID == NewLocation.QuestsDisponiveis.Recompensa.ID)
                                {
                                    ii.Quantidade++;

                                    AdicionarItemAoInventario = true;
                                    break;
                                }
                            }

                            if (!AdicionarItemAoInventario)
                            {
                                _player.Inventario.Add(new ItemInventario(NewLocation.QuestsDisponiveis.Recompensa, 1));
                            }

                            foreach (PlayerQuest pq in _player.Quest)
                            {
                                if (pq.Details.ID == NewLocation.QuestsDisponiveis.ID)
                                {
                                    pq.IsCompleted = true;
                                    break;
                                }
                            }

                        }


                    }

                }
                else
                {
                    RbtMensagem.Document.Blocks.Add(new Paragraph(new Run("Você recebeu a quest: " + NewLocation.QuestsDisponiveis.Nome)));
                    RbtMensagem.Document.Blocks.Add(new Paragraph(new Run(NewLocation.QuestsDisponiveis.Descricao )));
                    RbtMensagem.Document.Blocks.Add(new Paragraph(new Run("Para completar, retorne com:")));
                    foreach(QuestCompletionItem qci in NewLocation.QuestsDisponiveis.QuestCompletionItems)
                    {
                        if(qci.Quantidade == 1)
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

            }

            if(NewLocation.MonstrosNessaArea != null)
            {
                RbtMensagem.Document.Blocks.Add(new Paragraph(new Run("Você encontra um " + NewLocation.MonstrosNessaArea.Nome)));
                Monstro Monstrinho = Mundo.MonstroByID(NewLocation.MonstrosNessaArea.ID);

                _Monstro = new Monstro(Monstrinho.ID, Monstrinho.Nome, Monstrinho.DanoMaximo, Monstrinho.ExperienciaLoot, Monstrinho.DinheiroLoot, Monstrinho.Vida, Monstrinho.VidaMaxima);

                foreach(LootItem lootitem in Monstrinho.LootTable)
                {
                    _Monstro.LootTable.Add(lootitem);
                }

                CboWeapons.Visibility = Visibility.Visible;
                CboPocoes.Visibility = Visibility.Visible;
                btnUsePotion.Visibility = Visibility.Visible;
                BtnUseWeapon.Visibility = Visibility.Visible;
            }
            else
            {
                _Monstro = null;

                CboWeapons.Visibility = Visibility.Hidden;
                CboPocoes.Visibility = Visibility.Hidden;
                btnUsePotion.Visibility = Visibility.Hidden;
                BtnUseWeapon.Visibility = Visibility.Hidden;
            }





           // dgvInvetory.Items.Clear();


            foreach(ItemInventario iteminvetario in _player.Inventario)
            {
                if (iteminvetario.Quantidade > 0 )
                {
                    //dgvInvetory.Items.Add(new object[] {iteminvetario.  });
                    //dgvInvetory.ItemsSource = _player.Inventario;
                    // dgvInvetory.(new ItemInventario (iteminvetario.Details,iteminvetario.Quantidade));
                    dgvInvetory.ItemsSource = _player.Inventario;
                }
                
            }

            dgvQuest.Items.Clear();
            foreach (PlayerQuest quest in _player.Quest)
            {
                    dgvQuest.Items.Add(new object[] {quest.Details,quest.IsCompleted  });
                    //dgvInvetory.ItemsSource = _player.Inventario;
                    // dgvInvetory.(new ItemInventario (iteminvetario.Details,iteminvetario.Quantidade));
                    //dgvInvetory.ItemsSource = _player.Inventario;
                

            }
            //dgvQuest.ItemsSource = _player.Quest;


        }

    }
}
