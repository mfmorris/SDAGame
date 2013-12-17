using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public class ActionList
    {
        private List<Action> actions;

        public int Count
        {
            get
            {
                return actions.Count;
            }
        }

        public ActionList(int capacity = 5)
        {
            actions = new List<Action>(capacity);
        }

        public void Add(Action action)
        {
            if (!actions.Contains(action))
            {
                actions.Add(action);
            }
        }

        public void Remove(Action action)
        {
            actions.Remove(action);
        }

        public void Remove(int index)
        {
            actions.Remove(actions[index]);
        }

        public Action this[int i]
        {
            get { return actions[i]; }
        }

    }
}
