using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public class EffectList : List<Effect>
    {
        public new void Add(Effect effect)
        {
            if ((this.Count(e => e.Name == effect.Name) > 0) && !effect.CanStack)
            {
                return;
            }
            base.Add(effect);
        }

        public void Remove(string name)
        {
            foreach (Effect effect in this)
            {
                if (effect.Name == name)
                {
                    this.Remove(effect);
                }
            }
        }

        public void Update(int delta = 1)
        {
            foreach (Effect effect in this)
            {
                effect.Update();
                if (effect.Finished)
                {
                    this.Remove(effect);
                }
            }
        }
    }

}
