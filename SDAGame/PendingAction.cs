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
        #region properties

        public Actor[] targets { get; private set; }

        public Action action { get; private set; }

        public int SPD { get; private set; }

        public Actor actor { get; private set; }

        #endregion

        public PendingAction(Action action, Actor[] targets, int SPD)
        {
            this.action = action;
            this.targets = targets;
            this.SPD = SPD;
            this.actor = action.Owner;
        }

        public void Execute()
        {
            action.Resolve(targets);
        }
    }
}
