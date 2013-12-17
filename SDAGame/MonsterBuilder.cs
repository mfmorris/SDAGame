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
        private FightScene scene;
        private List<PlayerCharacter> pcs;

        public MonsterBuilder(List<PlayerCharacter> pcs)
        {
            this.pcs = pcs;
        }

        public Enemy GetKobold()
        {
            Kobold kob = new Kobold();
            kob.setAI(new RandomAI(scene.Enemies, scene.PCS));
            return kob;
        }

        public List<Enemy> GetEncounter()
        {
            List<Enemy> monsters = new List<Enemy>(3);
            
            for(int i = 0; i < 3; ++i)
            {
                Kobold kob = new Kobold();
                kob.setAI(new RandomAI(monsters, pcs));
                monsters.Add(kob);
            }

            return monsters;

        }
    }
}
