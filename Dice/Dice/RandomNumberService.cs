using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    public class RandomNumberService
    {
        Random rnd = new Random();

        public int GenerateRandomNumberBetween(int min, int max)
        {
            int randomNumber = rnd.Next(min,max+1);
            return randomNumber;
        }
    }
}
