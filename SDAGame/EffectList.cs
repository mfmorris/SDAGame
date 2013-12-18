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

            try
            {
                Effect firstEffect = this.First(e => e.Name == effect.Name);
            }
            catch(InvalidOperationException ioe)
            {
                base.Add(effect);
            }

            effect.Apply();
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
