using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public class RandomAI : EnemyAI
    {
        private Random random;

        public RandomAI(Enemy subject, List<Enemy> enemies,
            List<PlayerCharacter> players, Random random)
            : base(subject, enemies, players)
        {
            this.random = random;
        }

        public override PendingAction Act()
        {
            int actionIndex = random.Next(this.subject.Actions.Count);
            Action toPerform = subject.Actions[actionIndex];
            Actor[] targets = null;

            if (toPerform.NumTargets > 0)
            {
                targets = new Actor[toPerform.NumTargets];

                if (toPerform.isDefensive)
                {
                    for (int i = 0; i < targets.Length; ++i)
                    {
                        targets[i] = enemies[random.Next(enemies.Count)];
                    }
                }
                else
                {
                    for (int i = 0; i < targets.Length; ++i)
                    {
                        targets[i] = enemies[random.Next(enemies.Count)];
                    }
                }
            }

            return new PendingAction(toPerform, subject, targets);
        }
    }
}
