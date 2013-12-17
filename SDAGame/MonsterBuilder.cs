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

        public MonsterBuilder(FightScene scene)
        {
            this.scene = scene;
        }

        public Enemy GetKobold()
        {
            Kobold kob = new Kobold();
            kob.setAI(new RandomAI(scene.enemies, scene.pcs, scene.random));
            return kob;
        }
    }
}
