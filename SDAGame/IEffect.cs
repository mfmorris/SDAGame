﻿using System;
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
        public Actor Target
        { 
            get; 
            protected set;
        }

        public int Duration
        {
            get;
            protected set;
        }

        public bool Finished
        {
            get;
            protected set;
        }

        public virtual string Name
        {
            get;
            protected set;
        }

        public virtual bool CanStack
        {
            get;
            protected set;
        }
        #endregion

        public virtual void Update(int delta = 1)
        {
            --this.Duration;
            if (Duration == 0)
            {
                this.Finished = true;
                this.Remove(Target);
            }
        }

        public virtual void Draw() { }//System.Drawing.Graphics g)


        public Effect(Actor target, int duration = 1)
        {
            this.Finished = false;
            this.Target = target;
            Duration = duration;
        }

        //do these need to be public?
        //do they need the argument?
        public abstract void Apply(Actor target);

        public abstract void Remove(Actor target);
    }

    /// <summary>
    /// Defend adds a value equal to double the targets BaseDEF to the target's DEF.
    /// When the duration has expired, Defend removes that value from the target's DEF.
    /// </summary>
    public class DefendEffect : Effect
    {
        private int defMod;

        public DefendEffect(Actor target, int duration = 1)
            : base(target)
        {
            this.defMod = this.Target.BaseDEF;
            this.Apply(target);
        }

        public override void Apply(Actor target)
        {
            this.Target.DEF+= defMod;
        }

        public override void Remove(Actor target)
        {
            this.Target.DEF -= defMod;
        }
    }

    public class RageEffect : Effect
    {
        private int atkMod;

        public RageEffect(Actor target, int duration = 3)
            : base(target)
        {
            this.atkMod = this.Target.BaseATK;
            this.Apply(target);
        }

        public override void Apply(Actor target)
        {
            this.Target.ATK += atkMod;
        }

        public override void Remove(Actor target)
        {
            this.Target.ATK -= atkMod;
        }
    }
}