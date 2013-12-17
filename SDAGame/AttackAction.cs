using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    class AttackAction : Action
    {
        private Random random;

        public AttackAction(int numTargets, Random random): base(numTargets)
        {
            this.random = random;
            this.Name = "Attack";
            this.NumTargets = 1;
        }

        public override void Resolve(Actor actor, Actor[] targets)
        {
            Actor target = targets[0];

            int attackRoll = actor.ATK + random.Next(20) + 1;
            int defenseRoll = target.DEF + random.Next(20) + 1;

            int damageDealt = attackRoll - defenseRoll;

            if (damageDealt > 0)
            {
                target.TakeDamage(damageDealt);
            }
        }
    }
}
