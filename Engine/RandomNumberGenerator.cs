using System;
using System.Security.Cryptography;

namespace Engine
{
    public static class RandomNumberGenerator
    {
        public static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();
        
        public static int NumberBetween(int Minimo, int Maximo)
        {
            byte[] randomNumber = new byte[1];

            _generator.GetBytes(randomNumber);

            double asciiValueOfRandomCharater = Convert.ToDouble(randomNumber[0]);

            double multiplier = Math.Max(0, (asciiValueOfRandomCharater / 255d) - 0.00000000001d);

            int range = Maximo - Minimo + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(Maximo + randomValueInRange);
        }
    }
}
