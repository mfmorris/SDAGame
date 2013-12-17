using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public class Enemy : Actor
    {

        private EnemyAI ai;


        public Enemy(string name, string image, string portrait = null):base(name, image, portrait)
        {
        }

        public virtual PendingAction Act()
        {
            return ai.Act();
        }

        public virtual void setAI(EnemyAI ai)
        {
            ai.subject = this;
            this.ai = ai;
        }

    }
}
