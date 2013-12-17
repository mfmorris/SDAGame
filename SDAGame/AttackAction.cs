using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    /// <summary>
    /// Action that invokes the standard attack of the actor using it.
    /// </summary>
    class AttackAction : Action
    {
        private Random random;

        public AttackAction(Actor owner): base(owner, 1)
        {
            this.random = FightScene.Random;
            this.Name = "Attack";
            this.Description = "This character's standard attack. Damages one enemy.";
            this.Enabled = true;
        }

        public override void Resolve(Actor[] targets)
        {
            Actor target = targets[0];

            int attackRoll = Owner.ATK + random.Next(20) + 1;
            int defenseRoll = target.DEF + random.Next(20) + 1;

            int damageDealt = attackRoll - defenseRoll;

            if (damageDealt > 0)
            {
                target.TakeDamage(damageDealt);
            }
        }
    }
}
