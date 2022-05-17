using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    internal class RollResult
    {
        private DateTime _whenGenerated;
        private int _generatedNumber;

        public RollResult(int generatedNumber)
        {
            _generatedNumber = generatedNumber;
            _whenGenerated = DateTime.Now;
        }

        public string GetTimestampedResult()
        {
            return ($"{_generatedNumber} Rolled at {_whenGenerated}");
        }
    }
}
