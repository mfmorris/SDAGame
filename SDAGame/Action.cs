using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    /// <summary>
    /// IActions are just algorithms that know what they need from actors
    /// and how to resolve.
    /// </summary>
    public abstract class Action
    {

        #region properties

        public Actor Owner
        {
            get;
            protected set;
        }

        /// <summary>
        /// The maximum number of targets this action can effect
        /// </summary>
        public int NumTargets
        {
            get;
            protected set;
        }

        /// <summary>
        /// Indicates the domain of this action's targets
        /// </summary>
        public bool isDefensive
        {
            get;
            protected set;
        }

        /// <summary>
        /// This action's friendly name
        /// </summary>
        public virtual string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// This description displays in the UI
        /// </summary>
        public virtual string Description
        {
            get;
            protected set;
        }

        /// <summary>
        /// For supporting effects that disable actions
        /// </summary>
        public virtual bool Enabled
        {
            get;
            set;
        }

        #endregion

        public Action(Actor owner, int numTargets)
        {
            this.NumTargets = numTargets;
        }

        public abstract void Resolve(Actor actor, Actor[] targets);
    }
}
