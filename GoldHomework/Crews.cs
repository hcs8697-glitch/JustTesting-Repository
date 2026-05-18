using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Crews
{// 나중 확장성을 위해... 만들기만 함.
    abstract class Crew
    {
        public string Symbol { get; }

        public Crew(string symbol)
        {
            Symbol = symbol;
        }        
    }
}
