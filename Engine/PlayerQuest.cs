﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class PlayerQuest : INotifyPropertyChanged
    {
        private Quest _details { get; set; }
        private bool _isCompleted { get; set; }

        public Quest Details
        {
            get { return _details; }
            set
            {
                _details = value;

            }
        }
        public bool IsCompleted { get { return _isCompleted; }
            set 
            { 
                _isCompleted = value;
                OnPropertyChanged("IsCompleted");
                OnPropertyChanged("Nome");
            } 
        }

        public PlayerQuest (Quest details)
        {
            Details = details;
            IsCompleted = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string nome)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
