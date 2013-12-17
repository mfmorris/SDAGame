using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    class CleaveAction : Action
    {
        private Random random;

        public CleaveAction(int numTargets, Random random)
            : base(numTargets)
        {
            this.random = random;
            this.Name = "Cleave";
        }

        public override void Resolve(Actor actor, Actor[] targets)
        {
            for (int i = 0; i < NumTargets; i++)
            {
                Actor target = targets[i];

                int attackRoll = actor.ATK + random.Next(20)/(i+1) + 1;
                int defenseRoll = target.DEF + random.Next(20) + 1;

                int damageDealt = attackRoll - defenseRoll;

                if (damageDealt > 0)
                {
                    target.TakeDamage(damageDealt);
                }
            }
        }
    }
}
