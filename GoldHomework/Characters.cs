using Game.Core;
using Game.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Characters
{
    abstract class Character //이거 객체 자체가 직접 존재할 이유가 없다... 그러니 추상 클래스가 맞지 않을까?
    {
        public string Name { get; private set; }
        private int hp; //체력에 프로퍼티 만들긴 해야 할 듯. 잘못된 값이 들어갈 수 있으니.
        public CharType Type { get; private set; }

        public int Hp
        {
            get
            {
                return hp;
            }
            set
            {
                if (value < 0) 
                {
                    hp = 0;
                }                
                else
                {
                    hp = value;
                }
            }                    
        }
        public int Atk { get; set; }
        public int HitRate { get; set; }

        public bool IsEvasive {  get; set; }

        private int cannon;

        public int Cannon
        {
            get
            {
                return cannon;
            }
            set
            {
                if (value > 20)
                {
                    Console.WriteLine("포탄은 최대 20개까지 보유 가능합니다.");
                    cannon = 20;
                }
                else if (value < 0)
                {
                    Console.WriteLine("포탄이 부족합니다."); //최종적으로 이 문구는 여기서 나오게 하지 말아야 할 듯.
                    cannon = 0;                                     //아마, 적도 포탄을 소비해서 공격한다면, 적 턴에 이 문구가 나올 수도 있다.
                }
                else
                {
                    cannon = value;
                }
            }
        }

        private int gold;

        public int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                if (gold < 0)
                {
                    gold = 0;
                }
                else
                {
                    gold = value;
                }
            }
        }

        public Random hitrand = new Random();
        public Character(string name, int hp, int atk, int hitRate, CharType type)
        {//생성자랑 인스턴스 생성 진짜 잘못 처리함. 이렇게 복잡하게 할 일인가...? 그리고 플레이어 캐릭터 인스턴스는 왜 메인에서 값을 전달하지?
            //애초에 생성자 쪽에 초기값을 적어도 되는 것 아닌가?
            //이거 개념... 지금 헷깔리기 시작했음. 다시 볼 것.
            Name = name;
            Hp = hp;
            Atk = atk;
            HitRate = hitRate;
            Type = type;
        }

        public abstract void Attack(Character target);

        public void TakeDamage(int atk) //근데, 이걸 굳이 추상 메서드로 할 필요가 있나? 어차피 재정의 할 필요 없잖아?
        {
            Hp = Hp - atk;
        }

        public virtual void SpecialAttack(Character target) // 이러면 선택구현 아닌감?
        {

        }
        public virtual void Evasive()
        {
            IsEvasive = true; //메서드가 실행될 때, true로 전환.
        }
        public virtual void AfterAttack()
        {
            if(IsEvasive)
            {
                IsEvasive = false;
            }            
        }

        public virtual int EvasionBonus()
        { return 0; }

    }
    class Player : Character
    {
        private int fuel;

        public int Fuel //왜 갑자기 프로퍼티 생성이 안 되는 걸까...
        {
            get //제한 로직을 넣고 싶다면 전체를 전부 구현해야 한다...
            {
                return fuel;
            }
            set
            { 
                fuel = value;                           
            }
        }       

        public Player (string name, int hp, int atk, int hitRate, CharType type) : base (name, hp, atk, hitRate, type)
        {
            Fuel = 8; //초기연료 8
            Cannon = 5; //생성될 때, 초기 캐논 5발
            Gold = 100; //100원 들고 시작.
        }

        public override void Attack(Character target) //또 고민인게... 어차피 로직이 비슷하면 굳이 이걸 추상으로 상속할 이유가?
        { //this. 를 사용하면 매개변수 attacker를 받을 필요가 없어진댄다...
            Console.WriteLine("( ㅡ _ㅡ) : 공축기관총 사격!");
            int miss = hitrand.Next(0, 100); // 명중률이 60이니, 60까지면 적중하고 61부터 100까지면 미스?

            Console.ReadKey();

            if (this.HitRate < miss) //명중률이 랜덤값을 넘지 못하면
            {                                //아니 근데... 어차피 그냥 HitRate써도 될 것 같은데?
                //Console.Clear();        //근데... 적을 매개변수로 받잖아? 그러면 그거에 따라 확률을 다르게 설계할 수도 있을 듯?
                Console.WriteLine("( ㅁ _ㅁ) : 빗나갔습니다!");
            }                              //공격과 특수공격은 아예 설계를 잘못했을 수도 있다. 리스코프 치환 원칙 생각하면 인터페이스로 해야?
            else
            {
                int finalAtk = this.Atk;
                if(this.IsEvasive)
                {
                    Console.WriteLine("회피기동 보너스 적용중");
                    finalAtk = (int)(finalAtk * 1.75f); //아쉬운 게 뭐냐면, 미스 아니면 정직하게 대미지가 들어가는 점.
                }                                                 //스치거나 제대로 적중하거나 이런 게 없음.
                target.TakeDamage(finalAtk); 
                //Console.Clear();                                  
                Console.WriteLine("( ㅁ _ㅁ) : 적중했습니다!");
                Console.WriteLine($"피해량 : {finalAtk}");


            }
        }
        public override void SpecialAttack(Character target) //모든 클래스가 가질 것 같진 않음.
        {
            Console.WriteLine("(# > _<) : 장전 끝! ( ㅁ _ㅁ) : 조준 끝!");
            Console.ReadLine();
            Console.WriteLine("( ㅡ _ㅡ) : 쏴!");
            int miss = hitrand.Next(0, 100) - target.EvasionBonus(); // 전차는 더 잘 맞아야 한다. 보병은 더 잘 피한다.
            //이렇게 하면, 실질적으로는 miss의 숫자가 작아진다 = 더 맞기 쉬워진다.

            Console.ReadKey();

            Console.WriteLine($"명중률 : {this.HitRate}, 타입 : {target.Type}, 명중 보정치 : {target.EvasionBonus()}");
            if(this.IsEvasive)
            {
                int finalAtk = (int)(this.Atk * 1.75f);
            }
            else
            {
                int finalAtk = this.Atk;
            }

            if (this.HitRate < miss)
            {
                //Console.Clear();
                Random rand = new Random();
                int randQuote = rand.Next(0, 4);
                switch (randQuote)
                {
                    case 0:
                        Console.WriteLine("( ㅡ _ㅡ) : 좌측 이탈! 2밀!");
                        break;
                    case 1:
                        Console.WriteLine("( ㅡ _ㅡ) : 우측 이탈! 3밀!");
                        break;
                    case 2:
                        Console.WriteLine("( ㅡ _ㅡ) : 상단 이탈! 1밀!");
                        break;
                    case 3:
                        Console.WriteLine("( ㅡ _ㅡ) : 하단 이탈! 4밀!");
                        break;
                    default:
                        Console.WriteLine("( ㅡ _ㅡ) : 좌측 이탈! 1밀!");
                        break;
                }
            }
            else
            {
                int finalAtk = this.Atk*2;
                if (this.IsEvasive)
                {
                    Console.WriteLine("회피기동 보너스 적용중");
                    finalAtk = (int)(finalAtk * 1.75f);
                }
                target.TakeDamage(finalAtk);  //나중에 주포 대미지에 관한 필드로 변경하는 게 좋을 것.                                                       
                Console.WriteLine("( ㅡ _ㅡ) : 명중!");
                Console.WriteLine($"피해량 : {finalAtk}");
            }
        }

        public override void Evasive() //코드 상 문제는 없어보이는데, 완전히 잘못 설계했음.
        {
            base.Evasive();
            Console.WriteLine("( ㅡ _ㅡ) : 전속! 차체 숨겨!");
            Console.WriteLine("(# > _<) : 라져!");
            Console.WriteLine("다음 턴까지 피해량이 1.75배 증가하고 피격 확률이 10%p 감소합니다.");
        }

    }

    class Enemy : Character
    {
        
        public Enemy(string name, int hp, int atk, int hitRate, CharType type) : base(name, hp, atk, hitRate, type)
        {
            
        }

        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} 이/가 기본 공격합니다.");            
            int miss = hitrand.Next(0, 100);

            Console.ReadKey();

            if (target.IsEvasive)
            {
                Console.WriteLine($"{target.Name}은/는 회피기동 적용중입니다.");
                miss = miss + 10; //이게 뭐니 이게... 진짜 구현 못했다.
            }
            if (this.HitRate < miss) //명중률이 랜덤값을 넘지 못하면
            {                
                Console.WriteLine("( ㅡ _ㅡ) : 피해 없음!");
            }
            else
            {
                target.TakeDamage(this.Atk);                                                  
                Console.WriteLine("( ㅁ _ㅁ) : 피탄당했습니다!");
            }
        }
        public override void SpecialAttack(Character target)
        {
            Console.WriteLine($"{Name} 이/가 주포로 공격합니다.");
            int miss = hitrand.Next(0, 100) - target.EvasionBonus(); 

            Console.ReadKey();

            if (target.IsEvasive)
            {
                Console.WriteLine($"{target.Name}은/는 회피기동 적용중입니다.");
                miss = miss + 10; 
            }
            if (this.HitRate < miss)
            {
                //Console.Clear();
                Console.WriteLine("( ㅡ _ㅡ) : 피해 없음!");
            }
            else
            {
                int finalAtk = this.Atk;
                //if (this.IsEvasive)
                //{
                //    Console.WriteLine("회피기동 보너스 적용중");
                //    finalAtk = (int)(finalAtk * 1.75f); 적도 회피기동을 할 수 있다면 이걸 잘 활용하면 된다.
                //}
                target.TakeDamage(finalAtk * 2);  //나중에 주포 대미지에 관한 필드로 변경하는 게 좋을 것.                                                       
                Console.WriteLine("( ㅡ _ㅡ) : 피탄! 비관통!");
                Console.WriteLine("(# > _<) : 젠장할!");
            }
        }


    }

    class LightTank : Enemy
    {
        public LightTank() : base("적 경전차", 220, 20, 35, CharType.Tank) //자식 클래스의 생성자 ()에 매개변수를 받지 않아도 상관없다.
        {
            
        }
        public override int EvasionBonus() //아니 근데 이걸 굳이 메서드로? 그냥 필드로 해도 되는 것 아님?
        { //Bonus의 값이 크면 클수록, 더 잘 피탄된다.
            return 10;
        }
    }

    class MidTank : Enemy
    {
        public MidTank() : base("적 中전차", 300, 30, 40, CharType.Tank)
        {
            
        }
        public override int EvasionBonus()
        {
            return 10;
        }
    }

    class HeavyTank : Enemy
    {
        public HeavyTank() : base("적 重전차", 400, 45, 45, CharType.Tank)
        {
            
        }
        public override int EvasionBonus()
        {
            return 15;
        }
    }
    class Boss : Enemy
    {
        public Boss() : base("적 초重전차", 1200, 65, 70, CharType.Boss) //오히려 보스가 쉬워버림. 한 마리만 나와서.
        {

        }
        public override int EvasionBonus()
        {
            return 15;
        }
    }
}
