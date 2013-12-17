using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    class VikingRageAction : Action
    {

        public VikingRageAction(): base(1)
        {
            this.Name = "Viking Rage";
        }

        public override void Resolve(Actor actor, Actor[] targets)
        {
            actor.AddEffect(new RageEffect(actor));
        }
    }
}
