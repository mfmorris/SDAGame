using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{

    public class FightScene
    {

        public static Random Random = new Random();

        #region events
        public event DeathNotification OnActorDeath;

        public event DamageNotification OnActorHealthChanged;

        private void RaiseOnActorDeath(Actor deadGuy)
        {
            if (OnActorDeath != null)
            {
                OnActorDeath(deadGuy);
            }
        }

        private void RaiseOnActorHealthChanged(Actor sender, int damageTaken)
        {
            if (OnActorHealthChanged != null)
            {
                OnActorHealthChanged(sender, damageTaken);
            }
        }

        #endregion

        #region properties
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
        #endregion

       // System.Collections.Generic.SortedList<int, PendingAction> actionQueue;
        ActionQueue actionQueue;

        public FightScene(params PlayerCharacter[] players)
        {
            //set up data
            actionQueue = new ActionQueue(); // = new SortedList<int, PendingAction>(10);
            PCS = new List<PlayerCharacter>(players);

            //get some monsters
            MonsterBuilder dungeonMaster = new MonsterBuilder(PCS);
            Enemies = dungeonMaster.GetEncounter();

            //subscribe to events so they can be forwarded
            foreach(Enemy ankleBiter in Enemies)
            {
                ankleBiter.OnHealthChanged += RaiseOnActorHealthChanged;
                ankleBiter.OnDeath += RaiseOnActorDeath;
            }

            foreach(PlayerCharacter pc in PCS)
            {
                pc.OnHealthChanged += RaiseOnActorHealthChanged;
                pc.OnDeath += RaiseOnActorDeath;
            }
        }

        public void AddAction(Action action, Actor[] targets, int SPD)
        {
            actionQueue.Add(new PendingAction(action, targets, SPD));
        }

        public void ResolveActions()
        {
            foreach(Enemy enemy in Enemies)
            {
                if(!enemy.isDead())
                {
                    actionQueue.Add(enemy.Act());
                }
            }
            foreach(PendingAction pendingAction in actionQueue)
            {
                pendingAction.Execute();
            }
            actionQueue.Clear();
        }

    }

}
