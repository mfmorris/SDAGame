using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public class PendingAction
    {
        private Action action;
        private Actor actor;
        private Actor[] targets;

        public PendingAction(Action action, Actor actor, Actor[] targets)
        {
            this.actor = actor;
            this.action = action;
            this.targets = targets;
        }

        public void Execute()
        {
            action.Resolve(actor, targets);
        }
    }
}
