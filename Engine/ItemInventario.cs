using System.ComponentModel;

namespace Engine
{
    public class ItemInventario : INotifyPropertyChanged
    {
        private Item _details;
        private int _quantidade;

        public Item Details { 
            get {return _details; } 
            set 
            {
                _details = value; 
                OnPropertyChanged("Details"); 
            } 
        }

        public int Quantidade { 
            get {return _quantidade; } 
            set 
            { 
                _quantidade = value;
                OnPropertyChanged("Quantidade");
                OnPropertyChanged("Descricao");
                    
            } 
        }

        public string Descricao
        {
            get { return Quantidade > 1 ? Details.NomePlural : Details.Nome; }
        }

        public ItemInventario (Item details, int quantidade)
        {
            Details = details;
            Quantidade = quantidade;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
