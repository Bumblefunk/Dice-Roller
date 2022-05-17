using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    internal class ResultsList
    {
        List<string> ReturnedResults = new List<string>(10);
        ListFlip handling = new ListFlip();

        public void AddResult(string result)
        {
            ReturnedResults.Add(result);
            if(ReturnedResults.Count >= 11)
            {
                handling.HandledList(ReturnedResults);
                ReturnedResults.RemoveAt(0);
                handling.HandledList(ReturnedResults);
            }
        }

        public List<string> GetResults()
        {
            return ReturnedResults;
        }
    }
}
