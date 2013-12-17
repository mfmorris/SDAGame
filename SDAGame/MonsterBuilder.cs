using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public class MonsterBuilder
    {

        private List<Action> liveActions = new List<Action>();

        private Random random = new Random();

        public Enemy GetKobold(FightScene scene)
        {
            Enemy kobold = new Enemy("Kobold", "\\kobold.png")
            {
                MaxHP = 10,
                BaseATK = 4,
                BaseDEF = 2,
                BaseSPD = 8,
                BaseWIS = 5
            };
            return new RandomAI(kobold, scene.enemies, scene.pcs, scene.random);
        }
    }
}
