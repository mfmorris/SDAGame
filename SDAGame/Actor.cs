using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace SDAGame
{

    public delegate void DeathNotification(Actor deadGuy);

    public delegate void DamageNotification(Actor sender, int damageTaken);

    /// <summary>
    /// All players and monsters have certain things in common.  This base class
    /// provides that.
    /// </summary>
    public abstract class Actor
    {
        #region events

        /// <summary>
        /// Raised when this Actor dies
        /// </summary>
        public event DeathNotification OnDeath;

        /// <summary>
        /// Raised when this character takes damage or
        /// recieves healing and is not already at full health
        /// </summary>
        public event DamageNotification OnHealthChanged;

        private void RaiseOnHealthChanged(int damageTaken)
        {
            if (OnHealthChanged != null)
            {
                OnHealthChanged(this, damageTaken);
            }
        }

        private void RaiseOnDeath()
        {
            if (OnDeath != null)
            {
                OnDeath(this);
            }
        }
        #endregion

        /// <summary>
        /// This Actor's available actions
        /// </summary>
        public ActionList Actions { get; private set; }

        /// <summary>
        /// Effects currently affecting this player
        /// </summary>
        protected EffectList effects;

        #region properties

        public BitmapImage image { get; private set; }

        public string imageName { get; private set; }

        public string portraitName { get; private set; }

        //See if we can make these setters private and only have
        //modification operations.
        #region stats

        private int _MaxHP;
        private int _BaseATK;
        private int _BaseDEF;
        private int _BaseSPD;
        private int _BaseWIS;

        public string Name { get;  set; }

        public virtual int HP { get; set; }
        public virtual int MaxHP 
        {
            get { return _MaxHP; }
            set
            {
                _MaxHP = value;
                HP = value;
            }
        }

        public virtual int ATK { get; set; }
        public virtual int BaseATK
        {
            get { return _BaseATK; }
            set
            {
                _BaseATK = value;
                ATK = value;
            }
        }

        public virtual int DEF { get; set; }
        public virtual int BaseDEF
        {
            get { return _BaseDEF; }
            set
            {
                _BaseDEF = value;
                DEF = value;
            }
        }

        public virtual int SPD { get; set; }
        public virtual int BaseSPD
        {
            get { return _BaseSPD; }
            set
            {
                _BaseSPD = value;
                SPD = value;
            }
        }

        public virtual int WIS { get; set; }
        public virtual int BaseWIS
        {
            get { return _BaseWIS; }
            set
            {
                _BaseWIS = value;
                WIS = value;
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// More Readable than if(hp<=0)
        /// </summary>
        /// <returns>health <= 0</returns>
        public bool isDead()
        {
            return HP <= 0;
        }


        public Actor(string name, string image, string portrait = null)
        {
            this.Name = name;
            this.imageName = image;
            this.image = new BitmapImage(new Uri(image, UriKind.Relative));
            this.portraitName = portrait ?? image;

            Actions = new ActionList();
        }

        public virtual void Update()
        {
            effects.Update();
        }

        public void AddAction(Action action)
        {
            Actions.Add(action);
        }

        public void AddEffect(Effect effect)
        {
            effects.Add(effect);
        }

        /// <summary>
        /// Subtracts an amount from hit points and raises appropriate events.
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(int amount)
        {
            if (!isDead())
            {
                HP -= amount;
                if (HP <= 0)
                {
                    RaiseOnDeath();
                }
                RaiseOnHealthChanged(amount);
            }
        }

        /// <summary>
        /// Adds an amount from hit points and raises OnHealed
        /// if the character was not already at full health.
        /// </summary>
        /// <param name="amount"></param>
        public void Heal(int amount)
        {
            if (!isDead())
            {
                if (MaxHP - HP < amount)
                {
                    amount = MaxHP - HP;
                }
                if(amount > 0)
                {
                    HP += amount;
                    RaiseOnHealthChanged(amount);
                }
            }
        }

        public override string ToString()
        {
            return string.Format("MAXHP: {0}, HP: {1}, ATK: {2}, DEF: {3}, SPD {4}, WIS: {5}", MaxHP, HP, ATK, DEF, SPD, WIS);
        }

    }
}
