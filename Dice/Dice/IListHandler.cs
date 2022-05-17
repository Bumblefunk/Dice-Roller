using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dice
{
    internal interface IListHandler
    {
        List<string> HandledList(List<string> pulledList);
    }
    class ListFlip : IListHandler
    {
        public List<string> HandledList(List<string> pulledList)
        {
            List<string> preFlip = pulledList;
            List<string> postFlip = new List<string>();

            preFlip.ForEach((item) =>
            {
                postFlip.Add(new string(item));
            });
            postFlip.Reverse();
            return postFlip;
        }
    }
}
