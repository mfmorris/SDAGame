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

        public RandomAI(List<Enemy> enemies,
            List<PlayerCharacter> players)
            : base(enemies, players)
        {
            this.random = FightScene.Random;
        }

        /// <summary>
        /// Randomly chooses an action and a target based on that action's isDefensive.
        /// </summary>
        /// <returns></returns>
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
                        targets[i] = players[random.Next(players.Count)];
                    }
                }
            }

            return new PendingAction(toPerform, targets, subject.SPD);
        }
    }
}
