using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO: Consider having a DRAW method

namespace SDAGame
{
    /// <summary>
    /// Effects should have an Apply and Remove.
    /// they should Apply themselves on creation,
    /// and Remove themselves, if necessary, when done.
    /// </summary>
    public abstract class Effect
    {

        #region properties
        public Actor Target { get; protected set; }

        public int Duration { get; protected set; }

        public bool Finished { get; protected set; }

        public virtual string Name { get; protected set; }

        public virtual bool CanStack { get; protected set; }
        #endregion

        public virtual void Update(int delta = 1)
        {
            --this.Duration;
            if (Duration == 0)
            {
                this.Finished = true;
                this.Remove();
            }
        }


        public Effect(Actor target, int duration)
        {
            this.Finished = false;
            this.Target = target;
            Duration = duration;
        }

        public abstract void Apply();

        public abstract void Remove();

    }

    /// <summary>
    /// Defend adds a value equal to double the targets BaseDEF to the target's DEF.
    /// When the duration has expired, Defend removes that value from the target's DEF.
    /// </summary>
    public class DefendEffect : Effect
    {
        private int defMod;

        public DefendEffect(Actor target, int duration = 1)
            : base(target, duration)
        {
            this.defMod = this.Target.BaseDEF;
            this.Name = "Defend";
        }

        public override void Apply()
        {
            this.Target.DEF += defMod;
        }

        public override void Remove()
        {
            this.Target.DEF -= defMod;
        }
    }


    /// <summary>
    /// Rage effect increases the Viking's attack and disables using Viking Rage again
    /// during the time his attack is increased.
    /// </summary>
    public class RageEffect : Effect
    {
        private int atkMod;
        private VikingRageAction associatedAction;

        public RageEffect(Actor target, int duration = 3)
            : base(target, duration)
        {
            this.atkMod = this.Target.BaseATK;
            this.Name = "Viking's Rage";
            foreach (Action action in target.Actions)
            {
                if(action is VikingRageAction)
                {
                    this.associatedAction = action as VikingRageAction;
                    action.Enabled = false;
                }
            }
        }

        public override void Apply()
        {
            this.Target.ATK += atkMod;
            
        }

        public override void Remove()
        {
            this.Target.ATK -= atkMod;
            this.associatedAction.Enabled = true;
        }
    }

    public class DisableAbilityEffect : Effect 
    {
        private string abilityName;
        private Action associatedAction;

        public DisableAbilityEffect(Actor target, string abilityName, int duration) : base(target, duration)
        {
            this.abilityName = abilityName;
            this.Name = "Disable " + abilityName;
        }

        public override void Apply()
        {
            foreach (Action action in Target.Actions)
            {
                if (this.abilityName == action.Name)
                {
                    action.Enabled = false;
                    this.associatedAction = action;
                    break;
                }
            }
        }

        public override void Remove()
        {
            associatedAction.Enabled = true;
        }
    }
}
