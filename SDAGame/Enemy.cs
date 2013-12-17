using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    public class Enemy : Actor
    {
        public int PointValue
        {
            get;
            protected set;
        }

        public Enemy(string name, string image, string portrait = null)
            : base(name, image, portrait) { }
    }


}
