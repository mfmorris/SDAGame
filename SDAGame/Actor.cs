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

    public abstract class Actor
    {
        #region events
        event DeathNotification OnDeath;

        event DamageNotification OnDamaged;
        #endregion

        public ActionList Actions { get; private set; }

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

        public int HP { get; set; }
        public int MaxHP 
        {
            get { return _MaxHP; }
            set
            {
                _MaxHP = value;
                HP = value;
            }
        }

        public int ATK { get; set; }
        public int BaseATK
        {
            get { return _BaseATK; }
            set
            {
                _BaseATK = value;
                ATK = value;
            }
        }

        public int DEF { get; set; }
        public int BaseDEF
        {
            get { return _BaseDEF; }
            set
            {
                _BaseDEF = value;
                DEF = value;
            }
        }

        public int SPD { get; set; }
        public int BaseSPD
        {
            get { return _BaseSPD; }
            set
            {
                _BaseSPD = value;
                SPD = value;
            }
        }

        public int WIS { get; set; }
        public int BaseWIS
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

        private void RaiseOnDamaged(int damageTaken)
        {
            if (OnDamaged != null)
            {
                OnDamaged(this, damageTaken);
            }
        }

        private void RaiseOnDeath()
        {
            if (OnDeath != null)
            {
                OnDeath(this);
            }
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

        public bool TakeDamage(int amount)
        {
            HP -= amount;
            if (HP <= 0)
            {
                RaiseOnDeath();
                return true;
            }
            return false;
        }

        public void Heal(int amount)
        {
            HP += amount;
            if (HP > MaxHP)
            {
                HP = MaxHP;
            }
        }

        public override string ToString()
        {
            return string.Format("MAXHP: {0}, HP: {1}, ATK: {2}, DEF: {3}, SPD {4}, WIS: {5}", MaxHP, HP, ATK, DEF, SPD, WIS);
        }

    }
}
