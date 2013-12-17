using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{

    /// <summary>
    /// Associates an action with its targets. Call Execute()
    /// to apply the action to those targets.
    /// </summary>
    public class PendingAction
    {
        private Action action;
        private Actor[] targets;
        public int SPD { get; private set; }

        public PendingAction(Action action, Actor[] targets, int SPD)
        {
            this.action = action;
            this.targets = targets;
            this.SPD = SPD;
        }

        public void Execute()
        {
            action.Resolve(targets);
        }
    }
}
