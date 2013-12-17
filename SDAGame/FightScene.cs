using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{

    public class FightScene
    {

        #region events
        public event DeathNotification OnActorDeath;
        public event DamageNotification OnActorDamaged;

        private void RaiseOnActorDeath(Actor deadGuy)
        {
            if (OnActorDeath != null)
            {
                OnActorDeath(deadGuy);
            }
        }

        private void RaiseOnActorDamaged(Actor sender, int damageTaken)
        {
            if (OnActorDamaged != null)
            {
                OnActorDamaged(sender, damageTaken);
            }
        }

        #endregion

        System.Collections.Generic.SortedList<int,PendingAction> actionQueue;

        public List<PlayerCharacter> PCS
        {
            get;
            private set;
        }

        public List<Enemy> Enemies
        {
            get;
            private set;
        }

        public static Random Random = new Random();

        public FightScene(params PlayerCharacter[] players)
        {
            //set up data
            actionQueue = new SortedList<int, PendingAction>(10);
            PCS = new List<PlayerCharacter>(players);

            //get some monsters
            MonsterBuilder dungeonMaster = new MonsterBuilder(PCS);
            Enemies = dungeonMaster.GetEncounter();

            //subscribe to events so they can be forwarded
            foreach(Enemy ankleBiter in Enemies)
            {
                ankleBiter.OnDamaged += RaiseOnActorDamaged;
                ankleBiter.OnDeath += RaiseOnActorDeath;
            }
        }

        public void AddAction(Action action, Actor[] target)
        {
            actionQueue.add
        }

        public void ResolveActions()
        {
            actionQueue.Clear();
        }

    }

}
