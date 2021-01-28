using System.ComponentModel;
using System.Linq;

namespace Engine
{
    public class Vendedor : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public BindingList<ItemInventario> Inventario { get; private set; }

        public Vendedor(string name)
        {
            Name = name;
            Inventario = new BindingList<ItemInventario>();
        }

        public void AddItemToInvetory(Item itemtoadd)
        {
            ItemInventario item = Inventario.SingleOrDefault(i => i.Details.ID == itemtoadd.ID);

            if (item == null)
            {
                Inventario.Add(new ItemInventario(itemtoadd, 1));
            }
            else
            {
                item.Quantidade++;
            }

            OnPropertyChanged("Inventario");
        }

        public void RemoveItem(Item ItemToRemove, int QuantToRemove)
        {
            ItemInventario item = Inventario.SingleOrDefault(i => i.Details.ID == ItemToRemove.ID);
            
            if (item == null)
            {

            }
            else
            {
                item.Quantidade -= QuantToRemove;

                if (item.Quantidade <= 0)
                {
                    Inventario.Remove(item);
                }

                OnPropertyChanged("Inventario");
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string nome)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
