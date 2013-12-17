using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{

    public class FightScene
    {
        System.Collections.Generic.SortedList<int,PendingAction> actionQueue;

        public List<PlayerCharacter> pcs
        {
            get;
            private set;
        }

        public List<Enemy> enemies
        {
            get;
            private set;
        }

        public Random random { get; private set;}

        public FightScene()
        {
            actionQueue = new SortedList<int, PendingAction>(10);
            pcs = new List<PlayerCharacter>();
            enemies = new List<Enemy>();
            random = new Random();
            pcs.Add(new Viking());
            pcs.Add(new Druid());

            MonsterBuilder dm = new MonsterBuilder(this);
        }
    }

}
