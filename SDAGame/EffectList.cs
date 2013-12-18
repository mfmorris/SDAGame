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

        public void Remove(List<Effect> removes)
        {
            foreach (Effect effect in removes)
            {
                base.Remove(effect);
            }

        }

        public void Update(int delta = 1)
        {
            List<Effect> removes = new List<Effect>();
            foreach (Effect effect in this)
            {
                effect.Update();
                if (effect.Finished)
                {
                    removes.Add(effect);
                }
            }

            this.Remove(removes);
        }
    }

}
