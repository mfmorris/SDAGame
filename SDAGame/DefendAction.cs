using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{

    /// <summary>
    /// Action that increases the defense of the actor performing it for one round.
    /// </summary>
    public class DefendAction : Action
    {

        public DefendAction(Actor owner) : base(owner, 0)
        {
            this.Name = "Defend";
            this.Description = "Brace for the assault: increases defense.";
        }

        public override void Resolve(Actor[] targets)
        {
            Owner.AddEffect(new DefendEffect(Owner));
        }

    }
}
