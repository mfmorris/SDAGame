using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    class VikingRageAction : Action
    {

        public VikingRageAction(): base(0)
        {
            this.Name = "Viking Rage";
            this.Description = "Fly into a Berserker's fury.\nincreases attack\nDuration: 3 rounds";
        }

        public override void Resolve(Actor actor, Actor[] targets)
        {
            actor.AddEffect(new RageEffect(actor));
        }
    }
}
