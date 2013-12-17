using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{

    public class DefendAction : Action
    {

        public DefendAction(int numTargets) : base(numTargets)
        {
            this.Name = "Defend";
        }

        public override void Resolve(Actor actor, Actor[] targets)
        {
            actor.AddEffect(new DefendEffect(actor));
        }

    }
}
