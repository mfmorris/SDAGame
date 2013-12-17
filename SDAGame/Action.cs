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
        /// <summary>
        /// The maximum number of targets this action can effect
        /// </summary>
        public int NumTargets
        {
            get;
            protected set;
        }

        public bool isDefensive
        {
            get;
            private set;
        }

        public virtual string Name
        {
            get;
            protected set;
        }

        public virtual string Description
        {
            get;
            protected set;
        }

        public virtual bool Enabled
        {
            get;
            set;
        }

        #endregion

        public Action(int numTargets)
        {
            this.NumTargets = numTargets;
        }

        public abstract void Resolve(Actor actor, Actor[] targets);
    }
}
