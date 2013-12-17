using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    class Program
    {
        private int _vjay;

        public int vjay
        {
            get { return _vjay; }
            set
            {
                _vjay = value;
                Console.WriteLine("Setting vjay");
            }
        }

        static void Main(string[] args)
        {
            FightScene fight = new FightScene();
            fight.fight();

            Console.ReadLine();

        }

        public static void testRefs()
        {
            List<int> lyst = new List<int>();
            lyst.Add(3);
            lyst.Add(4);
            lyst.Add(5);

            changeLyst(lyst);

            foreach (int i in lyst)
            {
                Console.WriteLine(i);
            }
            Console.ReadLine();
        }

        public static void changeLyst(List<int> lyst)
        {
            lyst.RemoveAt(1);
            lyst[0]--;
        }

        public static void testCharSetup()
        {
            string input = null;

            PlayerCharacter pc;
            Console.Write("Name>");
            pc = new PlayerCharacter(Console.ReadLine(), null, null);

            do
            {

                Console.Write("MAXHP>");
                pc.MaxHP = int.Parse(Console.ReadLine());
                Console.Write("HP>");
                pc.HP = int.Parse(Console.ReadLine());
                Console.Write("ATK>");
                pc.ATK = int.Parse(Console.ReadLine());
                Console.Write("DEF>");
                pc.DEF = int.Parse(Console.ReadLine());
                Console.Write("WIS>");
                pc.WIS = int.Parse(Console.ReadLine());
                Console.Write("SPD>");
                pc.SPD = int.Parse(Console.ReadLine());

                Console.WriteLine(pc.ToString());

                input = Console.ReadLine();
                input = input.ToLower().Trim();

            } while (input != "q");
        }
    }
}
