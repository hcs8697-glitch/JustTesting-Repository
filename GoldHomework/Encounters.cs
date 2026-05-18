using Game.Characters;
using Game.Enums;
using Game.Nogari;
using Game.ShopSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Game.Encounters
{
    abstract class Encounter
    {
        
        public abstract void enc(Player player);
    }

    class EnemyEnc: Encounter
    {
        public override void enc(Player player)
        {
            Console.WriteLine("적과 조우했습니다."); //enc를 강제 구현해야 하잖아? 여기에서 이제 입력부도 받는 거지.
            Console.ReadLine();
        }
    }
    
    class AllyEnc: Encounter
    {
        Random rand = new Random();
        public override void enc(Player player)
        {
            Console.WriteLine("아군과 조우했습니다.");

            int shopAppear = rand.Next(0, 101);

            if (shopAppear > 50)
            {
                Console.ReadKey();
                Console.WriteLine("후방에서 전진해오던 본대와 합류했습니다");
                Console.ReadKey();
                Console.WriteLine("( / _×) : 살아있었군. 정비할텐가?");
                Console.ReadKey();
                Shop shop = new Shop();
                shop.ShopOpen(player);                
            }
            else
            {
                Console.ReadKey();
                Console.WriteLine("그들은 많이 지쳐보입니다. 직전의 전투에서 가까스로 살아돌아온 것 같습니다");
                Console.ReadKey();
                Console.WriteLine("그냥 지나가기로 결심합니다.");
            }
            Console.ReadLine();
        }
    }   
}
