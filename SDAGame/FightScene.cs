using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SDAGame
{
    public delegate void ActionResolvedNotification(Actor actor, Action action);
    public delegate void FightEndNotification(FightScene scene);

    public class FightScene
    {

        public static Random Random = new Random();

        private int liveEnemies;
        private int livePCs;

        #region events
        public event DeathNotification OnActorDeath;

        public event DamageNotification OnActorHealthChanged;

        public event ActionResolvedNotification OnActionResolved;

        public event FightEndNotification OnWin;

        public event FightEndNotification OnLose;

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

        private void RaiseOnActionResolved(Actor actor, Action action)
        {
            if (OnActionResolved != null)
            {
                OnActionResolved(actor, action);
            }
        }

        private void RaiseOnWin()
        {
            if (OnWin != null)
            {
                OnWin(this);
            }
        }

        private void RaiseOnLose()
        {
            if (OnLose != null)
            {
                OnLose(this);
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
                ankleBiter.OnDeath += EnemyDeathListener;
            }

            foreach(PlayerCharacter pc in PCS)
            {
                pc.OnHealthChanged += RaiseOnActorHealthChanged;
                pc.OnDeath += RaiseOnActorDeath;
                pc.OnDeath += PCDeathListener;
            }

            this.liveEnemies = Enemies.Count;
            this.livePCs = PCS.Count;
        }

        public void AddAction(Action action, Actor[] targets, int SPD)
        {
            actionQueue.Add(new PendingAction(action, targets, SPD+action.SPDMod));
        }

        public void ResolveActions()
        {
            //get monster actions
            foreach(Enemy enemy in Enemies)
            {
                if(!enemy.isDead())
                {
                    actionQueue.Add(enemy.Act());
                }
            }

            //resolve actions
            foreach(PendingAction pendingAction in actionQueue)
            {
                pendingAction.Execute();
                RaiseOnActionResolved(pendingAction.actor, pendingAction.action);
            }
            actionQueue.Clear();

            //update effects
            foreach(Enemy en in Enemies)
            {
                en.Update();
            }

            foreach(PlayerCharacter pc in PCS)
            {
                pc.Update();
            }
        }

        private void EnemyDeathListener(Actor deadGuy)
        {

            --liveEnemies;
            if(liveEnemies == 0)
            {
                RaiseOnWin();
            }

        }

        private void PCDeathListener(Actor deadGuy)
        {

            --livePCs;
            if (livePCs == 0)
            {
                RaiseOnLose();
            }

        }


    }

}
