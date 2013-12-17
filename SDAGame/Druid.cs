using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    class Druid : PlayerCharacter
    {
        public Druid() : base("Druid", "\\druid.png", null)
        {
            MaxHP = 10;
            BaseATK = 4;
            BaseDEF = 2;
            BaseSPD = 8;
            BaseWIS = 5;

            Actions.Add(new AttackAction(new Random()));
            Actions.Add(new DefendAction());
            //Actions.Add(new CureLightAction(new Random()));
        }
    }
}
