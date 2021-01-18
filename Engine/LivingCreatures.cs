using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LivingCreatures
    {
        public LivingCreatures(int vida, int vidamaxima)
        {
            Vida = vida;
            VidaMaxima = vidamaxima;
        }

        public int Vida { get; set; }
        public int VidaMaxima { get; set; }

    }
}
