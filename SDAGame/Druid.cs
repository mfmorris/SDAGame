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
            MaxHP = 150;
            BaseATK = 4;
            BaseDEF = 12;
            BaseSPD = 7;
            BaseWIS = 8;

            Actions.Add(new AttackAction(this));
            Actions.Add(new DefendAction(this));
            //Actions.Add(new CureLightAction(this));
        }
    }
}
