using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    /// <summary>
    /// Action that represents a healing spell. Restores a bit of health
    /// to the recipient.
    /// </summary>
    public class CureLightAction : Action
    {
        private Random random;

        public CureLightAction(Random random)
            : base(1)
        {
            this.random = random;
            this.Name = "Cure Light Wounds";
            this.Description = "Heals one ally 5 to 10 hp";
        }

        public override void Resolve(Actor actor, Actor[] targets)
        {
            Actor target = targets[0];
            target.Heal(random.Next(6) + 5);
        }
    }
}
