using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{
    class Viking : PlayerCharacter
    {
        public Viking() : base("Viking Warrior", "\\viking.png", null)
        {
            MaxHP = 10;
            BaseATK = 4;
            BaseDEF = 2;
            BaseSPD = 8;
            BaseWIS = 5;

            Actions.Add(new AttackAction(1, new Random()));
            Actions.Add(new DefendAction(0));
            Actions.Add(new VikingRageAction(0));
        }
    }
}
