using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public abstract class EnemyAI
    {
        protected List<Enemy> enemies;
        protected List<PlayerCharacter> players;

        public Enemy subject { get; set; }


        public EnemyAI( List<Enemy> enemies, List<PlayerCharacter> players)
        {
            this.enemies = enemies;
            this.players = players;
        }


        public abstract PendingAction Act();
    }
}
