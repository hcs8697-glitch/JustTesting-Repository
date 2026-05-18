using Game.Characters;
using Game.Encounters;
using Game.Enums;
using Game.Nogari;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Game.Core
{
    class MainGame
    {       
        private Player player = new Player("플레이어", 1000, 60, 65, CharType.Tank);
        //이거... 여기서 이렇게 초기화할 이유가 있나?

        BattleSystem battleSystem = new BattleSystem();        

        private Random rand = new Random();

        private DialogueManager dialogueManager = new DialogueManager();

        private Encounter[] encounter =
        {
            new EnemyEnc(),
            new AllyEnc(),
        };

        private int turn = 30;
        private int passedTurn = 0;
        private int maxTurn = 30; // 이것의 초기값을... 애초에 turn의 첫 값을 받아오게 하던가 할 순 없으려나?
        private bool isBossDefeated = false;

        public int Turn
        {
            get
            {
                return turn;
            }

            private set
            {
                turn = value;
            }//턴을 조작할 수 있는 건 MainGame 내부의 메서드 뿐. 그 외에는 읽기는 가능하다.
        }                        //난이도에 따라 Turn의 갯수를 조절하는 것도 가능할 것이다.

        public int PassedTurn
        {
            get
            {
                return passedTurn;
            }
                private set
            {
                passedTurn= value;
            }
        }
        public int MaxTurn
        {
            get
            {
                return maxTurn;
            }
            private set
            {
                turn = value; //임시구현
            }
        }

        public bool IsBossDefeated
        {
            get
                { return isBossDefeated; }
            set
            {  isBossDefeated = value; }
        }

        public void PassTurn() //실수로 둘 중 하나만 쓰는 것 방지.
        {
            Turn--;
            PassedTurn++;
        }

        public void Run()
        {
            bool isRunning = Intro();
            while (isRunning && turn >= 0 && player.Hp > 0 && !IsBossDefeated)
            {
                //Intro 메서드를 만든다면, 게임 시작 전에 인트로를 보게 하거나 난이도를 고를 수 있게 하거나 할 수 있을 듯?
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine($"[남은 턴 : {Turn}]");
                Console.WriteLine($"[연료 : {player.Fuel}]");
                Console.WriteLine("========================================");

                ShowStatus(player);

                //상태를 굳이 버튼을 눌러서 봐야 할까? 그냥 여기서 띄우든 메서드 갖고와서 띄우든 해야지.

                Console.WriteLine(" [1] 주변 탐색");
                Console.WriteLine(" [2] 인벤토리 확인");
                Console.WriteLine(" [3] 전차 수리 시도");
                Console.WriteLine(" [4] 전진");
                Console.WriteLine(" [그 외] 동료와 잡담");
                Console.WriteLine(" [0] 게임 종료");

                if (!int.TryParse(Console.ReadLine(), out int choice)) //Day15_1의 인벤토리 제작 시연해주신 것 응용
                {
                    continue;
                }
                if (!Enum.IsDefined(typeof(IdleOption), (IdleOption)choice))
                {
                    dialogueManager.PrintDialogue();
                    continue;
                }
                switch ((IdleOption)choice)
                {
                    case IdleOption.Search: //FTL처럼 탐색을 연료가 떨어졌을 때만 할 수 있게 한다면?
                        Search();
                        break;
                    case IdleOption.Inventory:
                        break;
                    case IdleOption.Repair:
                        Repair();
                        break;
                    case IdleOption.Proceed:
                        Proceed();
                        break;
                    case IdleOption.Exit:
                        isRunning = false;
                        break;

                }
            }
            Console.WriteLine("게임 종료");
        }

        private void Proceed()
        {
            if (player.Fuel == 0)
            {
                Console.WriteLine("연료가 부족합니다!"); //흠... 이 방식이 조금 애매한데?
                Console.ReadKey();
                return;
            }
            PassTurn();
            player.Fuel--;

            Console.WriteLine("전진합니다. (연료 -1)");

            Console.ReadKey();

            Console.Clear();            

            int encRate = rand.Next(0, 100); //이걸 그냥 필드로 갖는 게? 아니 그냥... 랜덤 돌려서 하면 되는 거 아닌가?

            if (encRate > 65) //이거 아마... encManager 이런 거 만들어서 int형을 매개변수로 한다.
                                   //그래서 [i]형 인카운터를 실행하게 하면 끝이지 않을까.
            {
                encounter[1].enc(player);                
            }
            else
            {
                encounter[0].enc(player);
                battleSystem.Battle(player, PassedTurn, MaxTurn); //적과 조우했을 때 확정적으로 불러오는데... 내 선택에 따라서 바꿀 수 있게 하려면?
            }
        }

        //public void EncManager(int number)
        //{
        //    encounter[number].enc(); //대충 이런 식으로 만들면 되려나?
                                                //1부터 100까지의 랜덤 숫자를 받는데, 그걸 어떻게 거르지?
        //}

        public static void ShowStatus(Character character)
        {
            Console.WriteLine($"  [{character.Name}] ");
            Console.WriteLine($"체력 : {character.Hp}");
            Console.WriteLine($"공격력 : {character.Atk}");
            Console.WriteLine($"명중률 : {character.HitRate}%");
            Console.WriteLine($"포탄 수량 : {character.Cannon}발");
            Console.WriteLine($"자원 : {character.Gold}");
        }
        private void Search() //근데 솔직히, 탐색이라는 행동 자체가 손해잖아. 연료가 충분하다면 안 할 일인데?
        {
            Console.WriteLine("주변 물자를 수색합니까?");

            Console.ReadKey(); //여기에서 입력에 따라 뒤로 돌아가게 한다거나... 1. 예, 2. 아니오 해서 return으로 끝내버리면 되잖아?

            PassTurn();

            int searchRate = rand.Next(1, 101); //100%의 확률을 만들려면, 1부터 100까지여야 하는 게?
                                                //1부터 100까지 숫자 사용 가능.

            if (searchRate <= 35) //1~35
            {
                Console.WriteLine("아무 것도 찾지 못했습니다!");                
            }
            else if (searchRate <= 55) //36~55
            {
                int foundFuel = rand.Next(1, 3);
                Console.WriteLine($"연료를 {foundFuel}개 찾았습니다!");
                player.Fuel += foundFuel;
            }
            else if (searchRate <= 75)//56~75
            {
                int foundGold = rand.Next(10, 41);
                Console.WriteLine($"자원을 {foundGold}개 찾았습니다!");
                player.Gold += foundGold;
            }
            else if (searchRate <= 90)//76~90
            {
                int foundCannon = rand.Next(1, 3);
                Console.WriteLine($"포탄을 {foundCannon}개 찾았습니다!");
                player.Cannon += foundCannon;
            }
            else //91~100
            {
                Console.WriteLine($"적군이 버려둔 보급고를 발견했습니다!");

                player.Fuel += 3;
                player.Cannon += 1;
                player.Gold += 50;

            }
            Console.ReadKey();
        }

        private void Repair() //체력에 관한 부분 말인데, 적과 나의 체력 프로퍼티를 분리해야 하지 않을까.
        {
            if (player.Gold < 30)
            {
                Console.WriteLine("자원이 부족해서 수리할 수 없습니다."); //흠... 이 방식이 조금 애매한데?
                Console.ReadKey();
                return;
            }
            //체력 관련 조건도 추가해야 함.

            Console.WriteLine("응급수리를 시도할까요? 비용 : 30");
            Console.WriteLine($"현재 자원 : {player.Gold}");
            Console.WriteLine("1. 예, 그 외. 아니오.");

            if(!int.TryParse(Console.ReadLine(), out int choice) || choice !=1 ) //변환에 실패하거나, 입력이 1이 아니라면
            {
                return;
            }

            PassTurn();
            player.Gold = player.Gold - 50;

            int repairRate = rand.Next(1, 101); //100%의 확률을 만들려면, 1부터 100까지여야 하는 게?
                                                         //1부터 100까지 숫자 사용 가능.

            if (repairRate <=10) //1~10
            {
                Console.WriteLine("수리를 시도하다가 망가졌습니다!");
                Console.WriteLine($"{repairRate * 5} 만큼의 피해를 입습니다.");
                player.Hp = player.Hp - repairRate * 5;
                
            }
            else if (repairRate <= 40) //11~40
            {
                Console.WriteLine("아무 일도 일어나지 않았습니다.");
            }
            else //41~100
            {
                Console.WriteLine("수리에 성공했습니다!");
                Console.WriteLine($"{repairRate} 만큼 체력을 회복합니다.");
                player.Hp = player.Hp + repairRate;
            }
            Console.ReadKey();
        }
        
        private bool Intro()
        {
            Console.Clear();

            Console.WriteLine("========================================");
            Console.WriteLine("            탱크 RPG");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("1. 게임 시작");
            Console.WriteLine("그 외. 게임 종료");
            Console.WriteLine();

            string? input = Console.ReadLine();

            if (input == "1")
            {
                Console.WriteLine("게임을 시작합니다.");
                Console.ReadKey();
                return true;
            }

            Console.WriteLine("게임을 종료합니다.");
            Console.ReadKey();
            return false;
        }
    }

    class BattleSystem //이 녀셕의 Battle 메서드는 인스턴스 메서드다. 즉, 이 Battle 메서드를 쓰려면, BattleSystem의 인스턴스를 생성해야만 한다.
    {
        Random rand = new Random();

        private void BattleUI(Queue<Character> turnQueue) 
        {
            Console.Clear();
            Console.Write("▶");
            foreach(Character c in turnQueue)
            {
                Console.WriteLine($"[{c.Name}] 체력 : {c.Hp} 공격력 : {c.Atk}");
            }
            //Console.WriteLine($"  [{character.Name} ] 체력 : {character.Hp} 공격력 : {character.Atk}");
        }
        
        public List<Character> SpawnEnemy (int number, int PassedTurn, int MaxTurn) //아마도 1에서 3정도의 값을 받을 것. 
        {
            List<Character> list = new List<Character>();// List 만들고

            //PassedTurn을 MaxTurn으로 나눈 값에 따라 진행률을 결정한다.
            //가령, 초반부는 5 / 20으로 0.25,  후반부는 15 / 20으로 0.75

            float progress = (float)PassedTurn / MaxTurn;

            //남은Turn이 몇이냐에 따라 몬스터의 종류 다르게 생성.
            //progress가 1에 가까워질수록 강한 적이 생성된다.       

            //반환값이 Character인 메서드 묶음 Func를 만들고, 그걸 담는 List enemyPool을 만든다...
            //이게 대체 무슨 소리인지 잘 모르겠다...

            //그냥... 전차 혹은 보병만 나오게 한다면, rand 써서 할 순 있겠는데...

            if (progress < 0.4)
            {
                for (int i = 1; i <= number; i++)
                {
                    Character enemies = new LightTank();
                    list.Add(enemies);
                }
            }
            else if (progress < 0.7)
            {
                for (int i = 1; i <= number; i++)
                {
                    Character enemies = new MidTank();
                    list.Add(enemies);
                }
            }
            else if (progress < 0.9)
            {
                for (int i = 1; i <= number; i++)
                {
                    Character enemies = new HeavyTank();
                    list.Add(enemies);
                }
            }
            else if (progress >= 0.9) //number를 2로 한 다음, 아래에 다른 인스턴스 생성하면 다른 종류 두 마리도 가능함.
            {
                Character enemies = new Boss();                
                list.Add(enemies);                
            }
                return list;
        }

        public void Reward(int number, int PassedTurn, int MaxTurn, Player player)
        {
            float progress = (float)PassedTurn / MaxTurn;
            //임시 구현
            //진행도와 처치한 적 마릿수에 따라 보상 획득.
            //처치한 적 타입에 따라 보상 다르게 하는 건 추후 구현
            //그리고... 이렇게 하면 안 된다.
            //적들이 애초에 인스턴스가 생성될 때 랜덤으로 포탄과 연료, 자원을 갖고 생성되게 해야 한다.
            //그리고 내가 처치하면 확률적으로 그 값을 더하는 식으로 가야 한다.

            if (progress < 0.4) 
            {
                int fuelReward = rand.Next(0, 2) * number;
                int goldReward = rand.Next(20, 41) * number;
                int cannonReward = rand.Next(0, 2)  * number;

                Console.WriteLine("[획득 물픔]");
                Console.WriteLine($"연료 : {fuelReward}, 자원 : {goldReward}, 포탄 : {cannonReward}");
                player.Fuel = player.Fuel + fuelReward;
                player.Gold = player.Gold + goldReward;
                player.Cannon = player.Cannon + cannonReward;
            }
            else if (progress < 0.7)
            {
                int fuelReward = rand.Next(0, 3) * number;
                int goldReward = rand.Next(25, 51) * number;
                int cannonReward = rand.Next(1, 3) * number;

                Console.WriteLine("[획득 물픔]");
                Console.WriteLine($"연료 : {fuelReward}, 자원 : {goldReward}, 포탄 : {cannonReward}");
                player.Fuel = player.Fuel + fuelReward;
                player.Gold = player.Gold + goldReward;
                player.Cannon = player.Cannon + cannonReward;
            }
            else //보스 생성은 미구현
            {
                int fuelReward = rand.Next(1, 4) * number;
                int goldReward = rand.Next(41, 61) * number;
                int cannonReward = rand.Next(1, 5) * number;

                Console.WriteLine("[획득 물픔]");
                Console.WriteLine($"연료 : {fuelReward}, 자원 : {goldReward}, 포탄 : {cannonReward}");
                player.Fuel = player.Fuel + fuelReward;
                player.Gold = player.Gold + goldReward;
                player.Cannon = player.Cannon + cannonReward;
            }
        }

        public void AttackManager(int choice, Character attacker, Character target) //입력된 값에 따라, 공격 기능을 실행하게 할 메서드
        {
            switch ((BattleOption)choice)
            {
                case BattleOption.Attack:
                    attacker.Attack(target);
                    attacker.AfterAttack();
                    break;                       //이거... this 키워드로 해결할 수 있댔다.
                case BattleOption.SpecialAttack:
                    attacker.Cannon--;
                    attacker.SpecialAttack(target);
                    attacker.AfterAttack();
                    break;                    
                case BattleOption.Evasive:
                    attacker.Evasive();
                    break;
            }
        }         

        public void Battle(Player player, int PassedTurn, int MaxTurn)  //작동은 되긴 되는데, Queue에만 의존해야 해서 정신이 없다.... List<Character>를 따로 만들 것.
        { //배틀이 시작됐다는 건, 최소 1놈 이상의 적이 생긴다는 것.            

            //이거 그냥... 갈아엎고 다시 만들어야 한다. 진짜 엉망임.

            //List의 Contains를 사용하면, 특정 객체가 있는지 없는지 확인할 수 있다.

            //여기에서 아예 적 객체를 생성해버리는 편이 편할 수도 있다. 어차피 전투에서만 쓸 거잖아?
            //적 스폰에 관한 메서드를 여기서 받고, Queue에 담는 것이 맞는 것 같다.       

            int number = rand.Next(1, 4); //1~3까자의 확률이 동등해서 개빡세다.

            float progress = (float)PassedTurn / MaxTurn;

            if (progress >= 0.9)
            {
                number = 1;
            }

            List<Character> enemyList = SpawnEnemy(number, PassedTurn, MaxTurn); //적을 랜덤 수만큼 소환하고 담는 깊은 복사
            
            //실제 전투 참가자 목록
            List<Character> battleCharacters = new List<Character>();
            battleCharacters.Add(player);
            
            for (int i = 0; i < number; i++)
            {
                int quoteRand = rand.Next(1, 13);
                Console.WriteLine($"( ㅡ _ㅡ) : {enemyList[i].Name}! {quoteRand}시 방향! 거리 800!");
                battleCharacters.Add(enemyList[i]); //생성된 수만큼을 큐에 담는다.
            }          

            //턴 순서 관리용 Queue
            Queue<Character> turnQueue = new Queue<Character>(); //Queue를 만든 다음 플레이어와 적을 담아서 턴 구현            

            foreach (Character battleCharacter in battleCharacters)
            {
                turnQueue.Enqueue(battleCharacter); //생성된 수만큼을 큐에 담는다.
            }

            //이 turnQueue라는 것에 실질적으로 들어있는 것은 player와 new Enemy 인스턴스다...
            // Queue는 턴 순서만 관리하게 하는 게 좋다... 이걸 다른 걸 관리하게 하는 순간 꼬인다.
            // 하나는 하나의 기능만 담당하게 하자.
            Console.WriteLine("전투를 개시합니다."); //너무... 길다. 어떻게 쪼개야 하지?         
            Console.ReadKey();
            while (true)
            {
                //Console.ReadLine() ;
                                
                BattleUI(turnQueue);

                Console.WriteLine($"남은 포탄 수 : {player.Cannon}");

                Character curCharacter = turnQueue.Peek(); //현재 행동 캐릭터

                if(curCharacter.Hp <= 0)
                {
                    turnQueue.Dequeue();
                    continue; //Peek을 해서, 죽은 캐릭터가 들어오면 빼버리고 다시 while문 처음부터 시작한다.
                }

                Character attacker = curCharacter; //공격자                

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"[{curCharacter.Name}의 차례]");

                //그니까... enum BattleOption 내부에 실제로 존재하는 값이냐 아니냐를 여기서 검증해야 함.

                if (curCharacter == player)
                {
                    List<Character> aliveEnemies = new List<Character>();

                    foreach (Character character in battleCharacters)
                    {
                        if (character is Enemy && character.Hp > 0)
                        aliveEnemies.Add(character);
                    }

                    Character target = aliveEnemies[0]; //임시로 맨 앞의 적만 타겟팅

                    Console.WriteLine("1. 기본공격, 2. 특수공격, 3. 회피기동, 4. 도주");
                    if (!int.TryParse(Console.ReadLine(), out int choice)) //숫자만 입력으로 치겠다.
                    {
                        Console.WriteLine("할 수 없는 행동입니다.");
                        Console.ReadKey();
                        continue;
                    }
                    else if (!Enum.IsDefined(typeof(BattleOption), (BattleOption)choice)) //choice를 BattleOption으로 형변환 때려서
                    {                                                                                       //BattleOption 안에 없다면 false 반환
                        Console.WriteLine("할 수 없는 행동입니다."); //위 두 개의 조건을 ||로 엮으면 합칠 수 있긴 함...
                        Console.ReadKey();
                        continue;
                    }             
                    else if ((BattleOption)choice == BattleOption.SpecialAttack && player.Cannon == 0)
                    {
                        Console.WriteLine("(# > _<) : 포탄이 없습니다!");
                        Console.ReadKey();
                        continue;
                    }
                    else if ((BattleOption)choice == BattleOption.Escape) // 보스에서 도주 불가, 적마다 도주확률 다르게 하려면
                    {                                                                       //이렇게 구현하면 안 된다.
                        int escapeRate = rand.Next(0, 101);
                        {
                            if (escapeRate > 40)
                            {
                                Console.WriteLine("( ㅡ _ㅡ) : 전속! 이탈해!");
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("도주에 실패했습니다!");
                                Console.ReadKey();
                            }
                        }
                        

                    }
                    else
                    {
                        AttackManager(choice, attacker, target); //이거 player로 받아도 되나?
                        Console.ReadKey();                        
                        BattleUI(turnQueue);
                    }                          
                }
                else //curCharacter가 player가 아니라면
                {
                    //내가 입력할 부분은 없으니, 랜덤 돌려서 행동 고르게 하면 될 것?

                    Random tempRnd = new Random();
                    int tempChoice = tempRnd.Next(0, 2);
                    switch (tempChoice)
                    {
                        case 0:
                            attacker.Attack(player);//적이 어차피 나만 공격하잖아. 근데 굳이 target이라고 해야 할까?
                            break;
                        case 1:
                            attacker.SpecialAttack(player);
                            break;
                        default:
                            attacker.Attack(player);
                            break;
                    }
                    Console.ReadKey();
                    BattleUI(turnQueue);
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
                    if (character is Enemy && character.Hp >0)
                    {
                        hasEnemyAlive = true;
                        break;
                    }
                }

                if (!hasEnemyAlive)
                {
                    Console.WriteLine("적을 모두 격파했습니다.");
                    Reward(number, PassedTurn, MaxTurn, player);
                    Console.ReadKey();
                    break;
                }                

                Character finishCharacter = turnQueue.Dequeue();

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($"{finishCharacter.Name}의 행동이 완료되었다.");

                // 살아있는 경우만 다시 Queue에 추가
                if (finishCharacter.Hp > 0)
                {
                    turnQueue.Enqueue(finishCharacter);

                    Console.WriteLine($"{finishCharacter.Name}이 턴 맨 뒤로 이동.");
                }                
            }
        }
    }
}
