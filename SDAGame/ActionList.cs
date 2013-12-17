using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public class ActionList : List<Action>
    {

        public new void Add(Action action)
        {
            if (!this.Contains(action))
            {
                base.Add(action);
            }
        }
    }
}
