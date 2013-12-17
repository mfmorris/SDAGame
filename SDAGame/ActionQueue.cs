using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public class ActionQueue: List<PendingAction>
    {

        public new void Add(PendingAction action)
        {
            for(int i = 0; i < this.Count; ++i)
            {
                if(action.SPD > this[i].SPD)
                {
                    this.Insert(i, action);
                    return;
                }
            }

            base.Add(action);
        }

    }
}
