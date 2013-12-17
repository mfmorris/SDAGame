using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public class EffectList
    {
        private List<Effect> effects;

        public EffectList()
        {
            effects = new List<Effect>();
        }

        public void Add(Effect effect)
        {
            if ((effects.Count(e => e.Name == effect.Name) > 0) && !effect.CanStack)
            {
                return;
            }
            effects.Add(effect);
        }

        public void Remove(string name)
        {
            foreach (Effect effect in effects)
            {
                if (effect.Name == name)
                {
                    effects.Remove(effect);
                }
            }
        }

        public void Update(int delta = 1)
        {
            foreach (Effect effect in effects)
            {
                effect.Update();
                if (effect.Finished)
                {
                    effects.Remove(effect);
                }
            }
        }
    }

}
