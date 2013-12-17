using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    class VikingRageAction : Action
    {

        public VikingRageAction(int numTargets): base(numTargets)
        {
            this.Name = "Viking Rage";
            this.NumTargets = 0;
        }

        public override void Resolve(Actor actor, Actor[] targets)
        {
            actor.AddEffect(new RageEffect(actor));
        }
    }
}
