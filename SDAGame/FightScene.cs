using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAGame
{

    public class FightScene
    {
        System.Collections.Generic.SortedList<int,PendingAction> actionQueue;

        public List<PlayerCharacter> pcs
        {
            get;
            private set;
        }

        public List<Enemy> enemies
        {
            get;
            private set;
        }

        public Random random { get; private set;}

        public FightScene()
        {
            actionQueue = new SortedList<int, PendingAction>(10);
            pcs = new List<PlayerCharacter>();
            enemies = new List<Enemy>();
            random = new Random();
            pcs.Add(new Viking());
            pcs.Add(new Druid());

            MonsterBuilder dm = new MonsterBuilder(this);

            enemies.Add(dm.GetKobold());
            enemies.Add(dm.GetKobold());
            enemies.Add(dm.GetKobold());
            enemies.Add(dm.GetKobold());
        }

        public void fight()
        {
            while (pcs.Count > 0 && enemies.Count > 0)
            {
                // Get Player Descisions
                foreach (PlayerCharacter p in pcs)
                {
                    Console.WriteLine(p.Name);
                    Console.WriteLine("Choose Action:");
                    for (int i = 0; i < p.Actions.Count; i++)
                    {
                        Console.WriteLine(i + ": " + p.Actions[i].Name);
                    }
                    int input = int.Parse(Console.ReadLine());
                    Actor[] targets = new Actor[p.Actions[input].NumTargets];
                    for (int i = 0; i < p.Actions[input].NumTargets; i++)
                    {
                        Console.WriteLine("Choose Target " + (i + 1) + ":");
                        for (int j = 0; j < enemies.Count; j++)
                        {
                            Console.WriteLine(j + ": " + enemies[j].Name);
                        }
                        targets[i] = enemies[int.Parse(Console.ReadLine())];
                    }
                    actionQueue.Add(p.SPD, new PendingAction(p.Actions[input], p, targets));
                }

                // Generate AI Decisions
                foreach (Enemy e in enemies)
                {
                    actionQueue.Add(e.SPD, (e).Act());
                }

                // Resolve Action Queue
                while (actionQueue.Count > 0)
                {
                    actionQueue[actionQueue.Count - 1].Execute();
                    actionQueue.RemoveAt(actionQueue.Count - 1);
                }
            }

            // Check for Loss/Victory

        }
    }

}
