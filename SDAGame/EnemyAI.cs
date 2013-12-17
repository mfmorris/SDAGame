using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public abstract class EnemyAI : Enemy
    {
        protected Enemy subject;
        protected List<Enemy> enemies;
        protected List<PlayerCharacter> players;

        public EnemyAI(Enemy subject, List<Enemy> enemies, List<PlayerCharacter> players)
            :base(subject.Name, subject.imageName, subject.portraitName)
        {
            this.subject = subject;
            this.enemies = enemies;
            this.players = players;
        }

        public abstract PendingAction Act();
    }
}
