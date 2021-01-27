using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LivingCreatures : INotifyPropertyChanged
    {
        public LivingCreatures(int vida, int vidamaxima)
        {
            Vida = vida;
            VidaMaxima = vidamaxima;
        }

        public int Vida { 
            get {return _vida; } 
            set{
                _vida = value;
                OnPropertyChanged("Vida"); 
            } 
        }

        private int _vida;
        public int VidaMaxima { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
