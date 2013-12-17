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
        private Actor[] targets;

        public PendingAction(Action action, Actor[] targets)
        {
            this.action = action;
            this.targets = targets;
        }

        public void Execute()
        {
            action.Resolve(targets);
        }
    }
}
