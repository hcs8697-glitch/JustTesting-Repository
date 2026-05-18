using Game.Characters;
using Game.Core;
using Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldHomework
{
    internal class TempCode
    {

        //public void Battle()
        //{
        //    //while (isBattle)
        //    //{

        //    //    Console.Clear();
        //    //    ShowStatus(characters[0]);
        //    //    ShowStatus(characters[1]);

        //    //    Console.WriteLine("행동을 입력하세요.");

        //    //    if (!int.TryParse(Console.ReadLine(), out int choice))
        //    //    {
        //    //        continue;
        //    //    }
        //    //    switch ((BattleOption)choice) //이 방식은 근데, 새로운 기능이 추가되면 계속 바꿔야 함.
        //    //    {
        //    //        case BattleOption.Attack:
        //    //            characters[0].Attack();
        //    //            int a = hitrand.Next(0, 100); // 명중률이 60이니, 60까지면 적중하고 61부터 100까지면 미스?
        //    //            if (characters[0].HitRate < a) //랜덤으로 돌린 값이 명중률을 넘어버리면
        //    //            {
        //    //                Console.Clear();
        //    //                ShowStatus(characters[0]);
        //    //                ShowStatus(characters[1]);
        //    //                Console.WriteLine("빗나갔습니다.");
        //    //            }
        //    //            else
        //    //            {
        //    //                characters[1].TakeDamage(characters[0].Atk); // 이 상태에서, 플레이어의 명중률이 올라가거나 공격력이 증가해도
        //    //                Console.Clear();                                        //기존 코드에는 아무런 문제가 없다.
        //    //                ShowStatus(characters[0]);                          //근데... 길어.
        //    //                ShowStatus(characters[1]);
        //    //                Console.WriteLine("적중했습니다.");
        //    //            }
        //    //            break;
        //    //        case BattleOption.SpecialAttack:
        //    //            break;
        //    //        case BattleOption.Evasive:
        //    //            break;
        //    //        case BattleOption.Escape:
        //    //            break;

        //    //    }
        //    //    Console.ReadLine();

        //    //    Console.WriteLine("적의 턴입니다."); //이것도 적이 여러마리 나온다면 다르게 빼야 할 듯.
        //    //    Console.ReadLine();

        //    //    int i = hitrand.Next(0, 100); // 명중률이 60이니, 60까지면 적중하고 61부터 100까지면 미스?
        //    //    if (characters[1].HitRate < i) //랜덤으로 돌린 값이 명중률을 넘어버리면
        //    //    {
        //    //        Console.WriteLine("빗나갔습니다.");
        //    //    }
        //    //    else
        //    //    {
        //    //        characters[0].TakeDamage(characters[1].Atk);
        //    //        Console.Clear();
        //    //        ShowStatus(characters[0]);
        //    //        ShowStatus(characters[1]);
        //    //        Console.WriteLine("피탄됐습니다.");
        //    //    }
        //    //    Console.ReadLine();

        //    //    if (characters[0].Hp < 0 || characters[1].Hp < 0)
        //    //    {
        //    //        break;
        //    //    }


        //    //}//End of while
        //}




        //public void Battle(Player player) //여기까진 어떻게... 자력으로 했었다.
        //{ //배틀이 시작됐다는 건, 최소 1놈 이상의 적이 생긴다는 것.            
        //    Console.WriteLine("전투를 개시합니다.");
        //    Console.ReadLine();

        //    //여기에서 아예 적 객체를 생성해버리는 편이 편할 수도 있다. 어차피 전투에서만 쓸 거잖아?
        //    //적 스폰에 관한 메서드를 여기서 받고, Queue에 담는 것이 맞는 것 같다.       

        //    Queue<Character> turnQueue = new Queue<Character>(); //Queue를 만든 다음 플레이어와 적을 담아서 턴 구현
        //    turnQueue.Enqueue(player);
        //    turnQueue.Enqueue(new Enemy("적 탱크 A", 200, 15, 50)); //for문 돌려서 적 개채 수, 타입 등도 만들 수 있을 듯.
        //    //turnQueue.Enqueue(new Enemy("적 탱크 B", 300, 15, 50)); 

        //    //이 turnQueue라는 것에 실질적으로 들어있는 것은 player와 new Enemy 인스턴스다...
        //    // Queue는 턴 순서만 관리하게 하는 게 좋다... 이걸 다른 걸 관리하게 하는 순간 꼬인다.
        //    // 하나는 하나의 기능만 담당하게 하자.

        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("전투 시작");
        //        MainGame.ShowStatus(turnQueue.Peek());

        //        Character curCharacter = turnQueue.Peek(); //현재 행동 캐릭터

        //        Character attacker = curCharacter; //공격자
        //        Character target = turnQueue.ElementAt(1); //공격받을 캐릭터

        //        Console.WriteLine($"{curCharacter.Name}의 차례");

        //        if (curCharacter == player)
        //        {
        //            Console.WriteLine("2. 기본공격, 3. 특수공격");
        //            if (!int.TryParse(Console.ReadLine(), out int choice))
        //            {
        //                continue;
        //            }
        //            switch ((BattleOption)choice) //이 방식은 근데, 새로운 기능이 추가되면 계속 바꿔야 함.
        //            {                                       //이 것도 너무 길다. AttackChoice와 같은 메서드로 분리 고려.
        //                case BattleOption.Attack: //공격 부분은 분명... 델리게이트를 써서 할 수 있을 것인데...
        //                    attacker.Attack(target); //여기가 매개변수 공격자, 목표 두 개를 받으면 되는 거 아님?
        //                    break;
        //                case BattleOption.SpecialAttack:
        //                    if (attacker.Cannon > 0)
        //                    {
        //                        attacker.Cannon--;
        //                        attacker.SpecialAttack(target);
        //                        break;
        //                    }
        //                    else
        //                    {
        //                        continue;
        //                    }
        //                case BattleOption.Evasive:
        //                    //추후 구현
        //                    break;
        //                case BattleOption.Escape:
        //                    //추후 구현
        //                    break;
        //            }
        //        }
        //        else //curCharacter가 player가 아니라면
        //        {
        //            Console.WriteLine("아마도 여기가 적의 턴?");
        //            Console.ReadKey();
        //            //내가 입력할 부분은 없으니, 랜덤 돌려서 행동 고르게 하면 될 것?

        //            attacker.Attack(target); //적이 어차피 나만 공격하잖아. 근데 굳이 target이라고 해야 할까?
        //        }

        //        //아마도... 여기에서 if를 통해 break를 거는 게 좋을 듯?
        //        //여기에 적을 격파했는지 판정을 넣으면 될 것 같지만...

         //        Character finishCharacter = turnQueue.Dequeue();

        //        Console.WriteLine($"{finishCharacter.Name}의 행동이 완료되었다.");

        //        turnQueue.Enqueue(finishCharacter);

        //        Console.WriteLine($"{finishCharacter.Name}이 턴 맨 뒤로 이동.");
        //        Console.ReadKey();

        //    }
        //}









        static class TemporaryBackup
        {
            public void Battle(Player player, int PassedTurn, int MaxTurn)  //작동은 되긴 되는데, Queue에만 의존해야 해서 정신이 없다.... List<Character>를 따로 만들 것.
            { //배틀이 시작됐다는 건, 최소 1놈 이상의 적이 생긴다는 것.            
                Console.WriteLine("전투를 개시합니다.");
                Console.ReadLine();

                //List의 Contains를 사용하면, 특정 객체가 있는지 없는지 확인할 수 있다.

                //여기에서 아예 적 객체를 생성해버리는 편이 편할 수도 있다. 어차피 전투에서만 쓸 거잖아?
                //적 스폰에 관한 메서드를 여기서 받고, Queue에 담는 것이 맞는 것 같다.       

                int number = rand.Next(1, 4);
                SpawnEnemy(number, PassedTurn, MaxTurn); //적을 랜덤 수만큼 소환.



                Queue<Character> turnQueue = new Queue<Character>(); //Queue를 만든 다음 플레이어와 적을 담아서 턴 구현
                turnQueue.Enqueue(player);
                turnQueue.Enqueue(new Enemy("적 탱크 A", 200, 15, 50)); //for문 돌려서 적 개채 수, 타입 등도 만들 수 있을 듯.
                                                                     //turnQueue.Enqueue(new Enemy("적 탱크 B", 300, 15, 50)); 

                //엄... SpawnEnemy가 List를 반환했는데, 그래서...? 여기서 아예 enemylist로 만들어서 깊은 복사를 해야 하나?

                //이 turnQueue라는 것에 실질적으로 들어있는 것은 player와 new Enemy 인스턴스다...
                // Queue는 턴 순서만 관리하게 하는 게 좋다... 이걸 다른 걸 관리하게 하는 순간 꼬인다.
                // 하나는 하나의 기능만 담당하게 하자.



                while (true)
                {
                    Console.Clear();

                    MainGame.ShowStatus(turnQueue.Peek());

                    Character curCharacter = turnQueue.Peek(); //현재 행동 캐릭터

                    Character attacker = curCharacter; //공격자
                    Character target = turnQueue.ElementAt(1); //공격받을 캐릭터

                    Console.WriteLine($"{curCharacter.Name}의 차례");

                    //그니까... enum BattleOption 내부에 실제로 존재하는 값이냐 아니냐를 여기서 검증해야 함.

                    if (curCharacter == player)
                    {
                        Console.WriteLine("2. 기본공격, 3. 특수공격");
                        if (!int.TryParse(Console.ReadLine(), out int choice)) //숫자만 입력으로 치겠다.
                        {
                            Console.WriteLine("숫자만 입력하시오.");
                            Console.ReadKey();
                            continue;
                        }
                        else if (!Enum.IsDefined(typeof(BattleOption), (BattleOption)choice)) //choice를 BattleOption으로 형변환 때려서
                        {                                                                                       //BattleOption 안에 없다면 false 반환
                            Console.WriteLine("할 수 없는 행동입니다.");
                            Console.ReadKey();
                            continue;
                        }
                        //여기에서 포탄이 없는 경우도 체크하면 되려나...?                    
                        else if ((BattleOption)choice == BattleOption.SpecialAttack && player.Cannon == 0)
                        {
                            Console.WriteLine("포탄이 없습니다.");
                            Console.ReadKey();
                            continue;
                        }
                        else
                        {
                            AttackManager(choice, attacker, target); //이거 player로 받아도 되나?
                        }
                    }
                    else //curCharacter가 player가 아니라면
                    {
                        Console.WriteLine("아마도 여기가 적의 턴?");
                        Console.ReadKey();
                        //내가 입력할 부분은 없으니, 랜덤 돌려서 행동 고르게 하면 될 것?

                        attacker.Attack(target); //적이 어차피 나만 공격하잖아. 근데 굳이 target이라고 해야 할까?
                    }

                    //아마도... 여기에서 if를 통해 break를 거는 게 좋을 듯?
                    //여기에 적을 격파했는지 판정을 넣으면 될 것 같지만...

                    if (player.Hp <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("패배했습니다!");

                        Console.ReadKey();
                        break;
                    }

                    bool hasEnemyAlive = false;

                    foreach (Character character in turnQueue)
                    {
                        if (character is Enemy && character.Hp > 0)
                        {
                            hasEnemyAlive = true;
                            break;
                        }
                    }

                    if (!hasEnemyAlive)
                    {
                        Console.WriteLine("적을 모두 격파했습니다.");
                        Console.ReadKey();
                        break;
                    }

                    Character finishCharacter = turnQueue.Dequeue();

                    Console.WriteLine($"{finishCharacter.Name}의 행동이 완료되었다.");

                    // 살아있는 경우만 다시 Queue에 추가
                    if (finishCharacter.Hp > 0)
                    {
                        turnQueue.Enqueue(finishCharacter);

                        Console.WriteLine($"{finishCharacter.Name}이 턴 맨 뒤로 이동.");
                    }
                    else
                    {
                        Console.WriteLine($"{finishCharacter.Name}은(는) 전투 불능 상태입니다.");
                    }
                    Console.ReadKey();

                }

            }

    }
}
