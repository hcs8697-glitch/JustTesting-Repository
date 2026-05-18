using Game.Characters;
using Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ShopSystem
{
    class Shop //근데... 이걸 아예 그냥 따로 Encounter 상속 받고 출현 풀에 넣어버려도 되지 않나.
    {             //아니면... 어차피 Encounter 상속 안 받았잖아? 그냥 다른 cs로 분리해버려도 상관 없음.
        public void ShopOpen(Player player)
        {
            bool isShopping = true;
            while (isShopping)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine($"[상점]");
                Console.WriteLine($"[자원 : {player.Gold}, 연료 : {player.Fuel}, 포탄 : {player.Cannon}]");
                Console.WriteLine($"[체력 : {player.Hp}, 공격력 : {player.Atk}, 명중률 : {player.HitRate} %]");
                Console.WriteLine("========================================");

                Console.WriteLine(" [1] 연료 : 자원 3"); //연료를 사는 일이 거의 없어버림.
                Console.WriteLine(" [2] 포탄 : 자원 5 ");
                Console.WriteLine(" [3] 수리 : 자원 10, Hp + 25");
                Console.WriteLine(" [4] 공격력 업그레이드 : 자원 20, 공격력 + 3");
                Console.WriteLine(" [5] 명중률 업그레이드 : 자원 25, 명중률 + 2%p");
                Console.WriteLine(" [그 외] 나가기");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    continue;
                }
                if (!Enum.IsDefined(typeof(ShopOption), (ShopOption)choice))
                {
                    Console.WriteLine("상점을 나갑니다.");
                    Console.ReadKey();
                    isShopping = false;
                    break;
                }
                switch ((ShopOption)choice)
                {

                    case ShopOption.BuyFuel: // 다 일일이 ReadKey() 넣어주는 것도 귀찮고... 너무 길어.
                        if (player.Gold >= 3) //TryBuyFuel, TryBuyCannon 이런 걸로 분리하는 것도 좋음.
                        {
                            player.Gold = player.Gold - 3;
                            player.Fuel++;
                            Console.WriteLine("연료 구매 성공");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("자원이 부족합니다.");
                            Console.ReadKey();
                            break;
                        }
                    case ShopOption.BuyCannon:
                        if (player.Gold >= 5)
                        {
                            player.Gold = player.Gold - 5;
                            player.Cannon++;
                            Console.WriteLine("포탄 구매 성공");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("자원이 부족합니다.");
                            Console.ReadKey();
                            break;
                        }
                    case ShopOption.BuyRepair:
                        if (player.Gold >= 10)
                        {
                            player.Gold = player.Gold - 10;
                            player.Hp += 25;
                            Console.WriteLine("수리받았습니다.");
                            Console.WriteLine("25만큼 체력 증가.");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("자원이 부족합니다.");
                            Console.ReadKey();
                            break;
                        }
                    case ShopOption.BuyAtkUp:
                        if (player.Gold >= 20)
                        {
                            player.Gold = player.Gold - 20;
                            player.Atk += 3;
                            Console.WriteLine("화기들을 강화받았습니다.");
                            Console.WriteLine("공격력 3 증가.");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("자원이 부족합니다.");
                            Console.ReadKey();
                            break;
                        }
                    case ShopOption.BuyHitRateUp:
                        if (player.Gold >= 25)
                        {
                            player.Gold = player.Gold - 25;
                            player.HitRate += 2;
                            Console.WriteLine("가늠좌들을 수리받았습니다.");
                            Console.WriteLine("명중률 2% 증가.");
                            Console.ReadKey();
                            break;
                        }
                        else if (player.HitRate > 100)
                        {
                            Console.WriteLine("명중률이 최대치입니다.");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("자원이 부족합니다.");
                            Console.ReadKey();
                            break;
                        }
                    default:
                        break;
                }
            }
        }
    }
}
