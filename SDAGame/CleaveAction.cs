using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    /// <summary>
    /// Represents Viking's Cleave, an action that damages two foes.
    /// The first attack is at full power, the second is at half.
    /// </summary>
    class CleaveAction : Action
    {
        private Random random;

        public CleaveAction(Actor owner, int numTargets)
            : base(owner, numTargets)
        {
            this.random = FightScene.Random;
            this.Name = "Cleave";
            this.Description = "Damages two foes with a single strike!";
            this.Enabled = true;
        }

        public override void Resolve(Actor[] targets)
        {
            for (int i = 0; i < NumTargets; i++)
            {
                Actor target = targets[i];

                int attackRoll = Owner.ATK + random.Next(20)/(i+1) + 1;
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
