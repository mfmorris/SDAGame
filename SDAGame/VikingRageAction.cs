using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    class VikingRageAction : Action
    {

        public VikingRageAction(Actor owner): base(owner, 0)
        {
            this.Name = "Viking Rage";
            this.Description = "Fly into a Berserker's fury.\nincreases attack\nDuration: 3 rounds";
            this.Enabled = true;
        }

        public override void Resolve(Actor[] targets)
        {
            Owner.AddEffect(new RageEffect(Owner));
        }
    }
}
