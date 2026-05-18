using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Game.Enums
{
    enum ItemType
    {
        Potion,
        Weapon,
        Resource
    }
    enum IdleOption
    {
        Search = 1,
        Inventory = 2,
        Repair = 3,
        Proceed = 4,
        Exit = 0
    }

    enum BattleOption
    {
        Attack=1,
        SpecialAttack=2,
        Evasive =3,
        Escape =4
    }

    enum ShopOption
    {
        BuyFuel = 1,
        BuyCannon = 2,
        BuyRepair = 3,
        BuyAtkUp = 4,
        BuyHitRateUp =5,
        
    }

    enum CharType
    {
        Tank,
        Infantry,
        Boss
    }

}
