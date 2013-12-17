using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    class Kobold : Enemy
    {

        public Kobold(string name = "Kobold", string imageName = "\\kobold.png", string portraitName = null)
            :base(name, imageName, portraitName)
        {
            MaxHP = 100;
            BaseATK = 4;
            BaseDEF = 2;
            BaseSPD = 8;
            BaseWIS = 5;

            AddAction(new AttackAction(this));
            AddAction(new DefendAction(this));
        }
    }
}
