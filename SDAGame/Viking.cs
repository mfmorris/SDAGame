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
            MaxHP = 200;
            BaseATK = 6;
            BaseDEF = 14;
            BaseSPD = 5;
            BaseWIS = 2;

            Actions.Add(new AttackAction(this));
            Actions.Add(new DefendAction(this));
            Actions.Add(new VikingRageAction(this));
            Actions.Add(new CleaveAction(this, 2));
        }
    }
}
