using Game.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Nogari
{
    class DialogueManager
    {
        private Random rand = new Random();

        private List<DialogueP> dialogues = new List<DialogueP>()
        {
            new Dialogue1(),
            new Dialogue2(),
            new Dialogue3(),
            new Dialogue4(),
            new Dialogue5(), //이 방식도... 결국엔 계속 추가해야 하잖아. 이거 어떻게 관리하냐?
            new Dialogue6(),
            new Dialogue7(),
            new Dialogue8(),
            new Dialogue9(), //이것만큼은 어쩔 수 없나?
        };

        public void PrintDialogue()
        {
            int index = rand.Next(dialogues.Count);

            dialogues[index].Dialogue();
        } //체력이 적을 때는 대화가 불가능하다던가, 심각한 이야기가 오간다거나 해도 재밌긴 하겠네.
    }

    class DialogueP
    {
        public string cr1 = "( ㅇ _ㅇ)"; //이 방식도 동작은 하지만, Dialogue 이외에서도 출현하게 하려면 바꾸는 편이 좋다.
        public string cr2 = "( ^ ▽^)";
        public string cr3 = "(# > _<)";
        public string cr4 = "( ㅁ _ㅁ)";
        public string cr5 = "( ㅡ _ㅡ)";

        public virtual void Dialogue()
        {
            Console.WriteLine("동료와 대화를 시작합니다.");
        }

        public void Talk(string cr, string quote) //대사출력 메서드
        {
            Console.ReadKey();
            Console.WriteLine($"{cr} : {quote}");
        }
    }

    class Dialogue1 : DialogueP 
    {                                    
        public override void Dialogue()
        {            
            base.Dialogue();
            Talk(cr1, "전차장님, 야밤중에 뭐하십니까?");

            Talk(cr5, "하늘을 보고 있어.");

            Talk(cr1, "별은 하나도 보이지 않는데 말입니다.");

            Talk(cr5, "그게 아냐.");

            Talk(cr5, "최근 적 폭격이 늘었다고 통신을 받았어.");

            Talk(cr5, "자기들 영역이어도 개의치 않을 정도라더군.");

            Talk(cr1, "그렇습니까.");

            Talk(cr5, "당분간은 야간에 기동해야겠어.");
            Console.ReadKey();
        }
    }
    class Dialogue2 : DialogueP
    {
        public override void Dialogue() //좀 더 명확하게 하려면, ldr, subdr, dr, tgr, cmdr 이렇게 해야할지도?
        {                                        // 이게 맞을 것 같긴 한데, 킹중에 하자...
            base.Dialogue();
            Talk(cr3, "야, ( ㅇ _ㅇ)!");

            Talk(cr1, "일병 ( ㅇ _ㅇ), 부르셨습니까?");

            Talk(cr3, "주워온 레이션 정리 다 했어?");

            Talk(cr1, "아직 안 했습니다.");

            Talk(cr3, "쓸모없는 녀석. 내가 할 테니 포탄이나 더 찾아와!");

            Talk(cr1, "알겠습니다.");

            Talk(cr1, "(포탄이 무슨 바닥에 굴러다니는 줄 아나...)");

            Talk(cr1, "(레이션이면 뭐... 그럴 수도 있지만.)");
            Console.ReadKey();
        }
    }

    class Dialogue3 : DialogueP
    {
        public override void Dialogue()
        {
            base.Dialogue();
            Talk(cr5, "포수, 담배 있나?");

            Talk(cr4, "유감입니다. 다 피웠네요.");

            Talk(cr2, "전차장님! 저 있습니다!");

            Talk(cr5, "고맙군.");

            Talk(cr3, "나도 하나 줘!");

            Talk(cr2, "미안, 그게 마지막이었어!");

            Talk(cr3, "뭐? 쓸모없긴. 형벌부대 출신 답구만.");

            Talk(cr5, "그만둬. 아직 안 피웠으니 괜찮다면 내 거라도 피우도록.");

            Talk(cr3, "다, 당치도 않습니다!");

            Talk(cr1, "(나도 하나밖에 없는데... 숨겨놔야겠다.)");
            Console.ReadKey();
        }
    }

    class Dialogue4 : DialogueP
    {
        public override void Dialogue()
        {
            base.Dialogue();
            Talk(cr1, "포수님, 뭐 읽고 계십니까?");

            Talk(cr4, "프로스트의 The Road Not Taken.");

            Talk(cr4, "두 갈래 길에 놓였던 화자가, 다른 길을 선택했더라면... 하고 돌이켜보는 시지.");

            Talk(cr2, "그냥 다시 가면 되는 거 아님까?");

            Talk(cr4, "아는 것과 실천할 수 있는 건 다르지.");

            Talk(cr2, "그릉가?");

            Talk(cr1, "(뭐... 지금은 갈 수 없긴 하지.)");

            Talk(cr1, "(이 전쟁은 언제 끝날까.)");
            Console.ReadKey();
        }
    }
    class Dialogue5 : DialogueP
    {
        public override void Dialogue()
        {
            base.Dialogue();
            Talk(cr2, "레이션만 먹은 지 며칠 째야...");

            Talk(cr5, "불평할 상황은 아니지만, 정비할 시간이 없군. 미안하다.");

            Talk(cr3, "하, ( ^ ▽^) 이 녀석 봐라. 감히 위대한 조국의 레이션을 모욕해?");
                
            Talk(cr3, "한 번 더 교화소에 가봐야 정신 차리겠네.");

            Talk(cr2, "차라리 교화소가 낫겟어...");

            Talk(cr4, "너는 안 질리나보네.");

            Talk(cr3, "어...? 포수님도 질리십니까?");

            Talk(cr4, "아니.");

            Talk(cr2, "아... 개구리 먹고 싶다.");

            Talk(cr3, "개구리!? 그걸 어떻게 먹어!");

            Talk(cr2, "맛있다고! 고향에 있을 땐 자주 튀겨먹었는데...");

            Talk(cr1, "그러고 보니, 동물들을 본 지가 언젠지 모르겠습니다.");

            Talk(cr5, "전쟁을 가장 민감하게 느끼는 건 동물들이지. 주변 마을들도 기근에 시달리고 있을 거야.");

            Talk(cr5, "이 전쟁은 반드시 끝나야 해.");

            Talk(cr3, "물론입니다! 위대한 조국이 패배할 리가 없잖습니까!");

            Talk(cr5, "그렇지...");
            Console.ReadKey();
        }
    }

    class Dialogue6 : DialogueP
    {
        public override void Dialogue()
        {
            base.Dialogue();
            Talk(cr5, "부조종수.");

            Talk(cr1, "일병 ( ㅇ _ㅇ), 부르셨습니까?");

            Talk(cr5, "저길 좀 봐.");

            Talk(cr1, "...허수아비들이 잔뜩 세워져 있습니다.");

            Talk(cr2, "적국의 선전물이네. 지뢰제거반에 있을 때 자주 봤어.");

            Talk(cr3, "앞에 뭘 매고 있는데, 뭐라고 쓰여 있습니까?");

            Talk(cr4, "\"우린 싸우기를 포기한 비겁자가 되지 않는다.\",");

            Talk(cr4, "\"너희들도 이 허수아비처럼 여기에 세워주겠다.\"");

            Talk(cr3, "더러운 놈들. 전부 다 짓밟아버려야 해.");

            Talk(cr5, "자네라면 그렇게 말할 줄 알았어.");

            Talk(cr3, "당연합니다! 전차장님께서도 그렇게 생각하시잖습니까?");

            Talk(cr5, "...전진하자.");
            Console.ReadKey();
        }
    }

    class Dialogue7 : DialogueP
    {
        public override void Dialogue()
        {
            base.Dialogue();
            Talk(cr3, "야, ( ^ ▽^)!");

            Talk(cr2, "왜?");

            Talk(cr3, "전투도 없었는데 진흙탕은 왜 밟았어! 혼자서 닦느라 애먹었잖아!");

            Talk(cr3, "진흙 굳으면 궤도 정비 빡세지는 거 몰라?");

            Talk(cr2, "아니,");

            Talk(cr3, "?");

            Talk(cr2, "알아.");

            Talk(cr3, "......");
            Console.ReadKey();

        }
    }
    class Dialogue8 : DialogueP
    {
        public override void Dialogue()
        {
            base.Dialogue();
            Talk(cr1, "조종수님도 형벌부대 출신이신 줄 몰랐습니다.");

            Talk(cr2, "그러게. 지금 생각해보면 운이 없었지! 고작 빵 몇 조각 훔쳤을 뿐인데.");

            Talk(cr1, "단순히 '운이 없었다.'라고 하시기엔...");

            Talk(cr2, "뭐 어때! 이미 지난 일인데. 그래도 밥은 적당히 주니 다행이지 뭐.");

            Talk(cr2, "배고픈 채로 사는 것도 질렸고.");

            Talk(cr2, "근데, 넌 뭐 때문에 걸렸어?");

            Talk(cr1, "저는...");

            Talk(cr1, "아닙니다.");

            Talk(cr2, "어어? 사수가 물어보는데 대답도 안 하네?");

            Talk(cr3, "보나마나 뻔하지.");

            Talk(cr2, "뭔데?");

            Talk(cr3, "위대한 조국에 누를 끼칠만한 행동이었을 게 당연하잖아?");

            Talk(cr3, "소매치기든 강도든 상관없이, 다 잡아다 보내버려야 해. 그래야 세상이 깨끗해질 테니까.");

            Talk(cr3, "그렇지 않습니까, 포수님?");

            Talk(cr4, "독서에 방해돼. 떠들 거면 채널 바꿔.");
            Console.ReadKey();

        }
    }
    class Dialogue9 : DialogueP
    {
        public override void Dialogue()
        {
            base.Dialogue();
            Talk(cr2, "한 가지 재밌는 거 알려줄까?");

            Talk(cr1, "뭡니까?");

            Talk(cr2, "사실, 포수님이랑 (# > _<)는 동향 출신이래.");

            Talk(cr2, "같은 학교 선후배 사이였다고 했던가, 가족이라고 했던가 기억은 안 나지만.");

            Talk(cr1, "전혀 그런 느낌이 들지 않던데 말입니다.");

            Talk(cr2, "(# > _<)가 깍듯이 하는 면이 있지.");

            Talk(cr2, "그래도 좋은 녀석이야.");

            Talk(cr1, "가끔... 짜증날 때 있지 않으십니까?");

            Talk(cr1, "저희가 원해서 형벌부대에 가게 됐던 것도 아닌데.");

            Talk(cr2, "살아온 환경이 달라서 그런 건데 어쩌겠어.");

            Talk(cr2, "그리고 (# > _<)도 알긴 알겠지.");

            Talk(cr1, "뭘 말입니까?");

            Talk(cr2, "다 같이 살아남긴 어려워도, 다 같이 죽기는 쉬울 거라는 거.");

            Talk(cr2, "그러니까 그 정도 선까지만 하는 거지.");

            Talk(cr2, "무슨 말인지 알지?");

            Talk(cr1, "...주의하겠습니다.");
            Console.ReadKey();
        }
    }


    /*
    //기본 템플릿
    
    Talk(cr1, );
    Talk(cr2, );
    Talk(cr3, );
    Talk(cr4, );
    Talk(cr5, );
           Console.WriteLine("(ㅇ △ㅇ) : ");
           Console.ReadKey();

           Console.WriteLine("( ^ ▽^) : ");
           Console.ReadKey();

            Console.WriteLine("( ㅁ _ㅁ) : ");
            Console.ReadKey();

            Console.WriteLine("( ㅡ _ㅡ) : ");
            Console.ReadKey();

            Console.WriteLine("(# > _<) : ");
            Console.ReadKey();


     
     
     */




}
