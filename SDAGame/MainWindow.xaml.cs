using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace SDAGame
{

    struct ActorUIElement
    {
        public Actor Actor;
        public Image Image;
        public Rectangle HealthBar;
        public Label Damage;
        //...
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int CHARACTER_SELECT = 0;
        private const int ACTION_SELECT = 1;
        private const int TARGET_SELECT = 2;
        private const int END_TURN = 3;
        int state;

        FightScene fightScene;

        PlayerCharacter pc1 = null;
        PlayerCharacter pc2 = null;
        PlayerCharacter pc3 = null;
        PlayerCharacter pc4 = null;
        PlayerCharacter pc5 = null;
        PlayerCharacter selectedPC = null;
        Image selectedPCImage = null;
        int charactersSelected = 0;
        int totalCharacters = 0;
        
        Action selectedAction = null;
        int numberOfTargets = 0;

        List<Enemy> actionTargets;

        ActorUIElement[] enemies = new ActorUIElement[5];

        DoubleAnimation damageFade;
        
        public MainWindow()
        {
            InitializeComponent();

            // General Setup
            damageFade = new DoubleAnimation();
            damageFade.From = 0.0;
            damageFade.To = 1.0;
            damageFade.Duration = new Duration(TimeSpan.FromSeconds(2));
            damageFade.AutoReverse = true;

            actionTargets = new List<Enemy>();

            // Generate Characters
            pc1 = new Druid();
            PlayerCharacter1.Source = pc1.image;

            pc2 = new Viking();
            PlayerCharacter2.Source = pc2.image;

            totalCharacters = 2;

            fightScene = new FightScene(pc1, pc2);
            fightScene.OnActorHealthChanged += OnActorHealthChanged;
            fightScene.OnActorDeath += OnActorDeath;

            // Get and Setup Enemies
            enemies[0].Image = EnemyCharacter1;
            enemies[0].HealthBar = EnemyHealthBar1;
            enemies[0].Damage = EnemyDamage1;

            enemies[1].Image = EnemyCharacter2;
            enemies[1].HealthBar = EnemyHealthBar2;
            enemies[1].Damage = EnemyDamage2;

            enemies[2].Image = EnemyCharacter3;
            enemies[2].HealthBar = EnemyHealthBar3;
            enemies[2].Damage = EnemyDamage3;

            enemies[3].Image = EnemyCharacter4;
            enemies[3].HealthBar = EnemyHealthBar4;
            enemies[3].Damage = EnemyDamage4;

            enemies[4].Image = EnemyCharacter5;
            enemies[4].HealthBar = EnemyHealthBar5;
            enemies[4].Damage = EnemyDamage5;

            for (int i = 0; i < fightScene.Enemies.Count; i++)
            {
                enemies[i].Actor = fightScene.Enemies[i];
                enemies[i].Image.Source = fightScene.Enemies[i].image;
                enemies[i].HealthBar.Visibility = Visibility.Visible;
            }

            // Set start state and begin
            state = CHARACTER_SELECT;
            StatusLabel.Content = "Select a character.";
        }

        private void ActionItem_Selected(object sender, RoutedEventArgs e)
        {
            // Select an action
            if (state == ACTION_SELECT)
            {
                // Update UI
                NextButton.IsEnabled = true;
                SelectAction((ListBoxItem)sender);
            }
        }

        private void PlayerCharacter_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Select a character
            if (state == CHARACTER_SELECT)
            {
                state = ACTION_SELECT;

                StatusLabel.Content = "Select an action.";

                // Update UI
                charactersSelected++;
                UpdateUIWithPC((Image)sender);
            }

            if (state == ACTION_SELECT)
            {
                // Update UI
                UpdateUIWithPC((Image)sender);
            }
        }

        private void Enemy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (state == TARGET_SELECT)
            {
                state = CHARACTER_SELECT;
                if (charactersSelected == totalCharacters && numberOfTargets <= 0)
                {
                    EndTurn();
                }
                else
                {
                    // Update UI
                    numberOfTargets--;
                    TargetEnemy((Image)sender);
                    if (numberOfTargets <= 0)
                    {
                        fightScene.AddAction(selectedAction, actionTargets.ToArray(), selectedPC.SPD);
                        if (charactersSelected == totalCharacters)
                        {
                            EndTurn();
                        }
                        else
                        {
                            StatusLabel.Content = "Select a character.";
                            ResetUI();
                        }
                    }
                    else
                    {
                        state = TARGET_SELECT;
                        StatusLabel.Content = "Select " + numberOfTargets + " target.";
                    }
                }
            }
        }

        private void UpdateUIWithPC(Image image)
        {
            ResetUI();

            selectedPCImage = image;
            string character = image.Name;

            if (character == "PlayerCharacter1")
            {
                selectedPC = pc1;
            }
            else if (character == "PlayerCharacter2")
            {
                selectedPC = pc2;
            }
            else if (character == "PlayerCharacter3")
            {
                selectedPC = pc3;
            }
            else if (character == "PlayerCharacter4")
            {
                selectedPC = pc4;
            }
            else if (character == "PlayerCharacter5")
            {
                selectedPC = pc5;
            }

            NameValue.Content = selectedPC.Name;
            HealthValue.Content = selectedPC.HP;
            MaxHealthValue.Content = selectedPC.MaxHP;

            AttackValue.Content = selectedPC.ATK;
            WisdomValue.Content = selectedPC.WIS;
            SpeedValue.Content = selectedPC.SPD;
            DefenseValue.Content = selectedPC.DEF;

            if (selectedPC.Actions.Count > 0)
            {
                ActionItem1.Content = selectedPC.Actions[0].Name;
                ActionItem1.Visibility = Visibility.Visible;
                if (selectedPC.Actions[0].Enabled)
                {
                    ActionItem1.IsEnabled = true;
                }
                else
                {
                    ActionItem1.IsEnabled = false;
                }
            }
            if (selectedPC.Actions.Count > 1)
            {
                ActionItem2.Content = selectedPC.Actions[1].Name;
                ActionItem2.Visibility = Visibility.Visible;
                if (selectedPC.Actions[1].Enabled)
                {
                    ActionItem2.IsEnabled = true;
                }
                else
                {
                    ActionItem2.IsEnabled = false;
                }
            }
            if (selectedPC.Actions.Count > 2)
            {
                ActionItem3.Content = selectedPC.Actions[2].Name;
                ActionItem3.Visibility = Visibility.Visible;
                if (selectedPC.Actions[2].Enabled)
                {
                    ActionItem3.IsEnabled = true;
                }
                else
                {
                    ActionItem3.IsEnabled = false;
                }
            }
            if (selectedPC.Actions.Count > 3)
            {
                ActionItem4.Content = selectedPC.Actions[3].Name;
                ActionItem4.Visibility = Visibility.Visible;
                if (selectedPC.Actions[3].Enabled)
                {
                    ActionItem4.IsEnabled = true;
                }
                else
                {
                    ActionItem4.IsEnabled = false;
                }
            }
            if (selectedPC.Actions.Count > 4)
            {
                ActionItem5.Content = selectedPC.Actions[4].Name;
                ActionItem5.Visibility = Visibility.Visible;
                if (selectedPC.Actions[4].Enabled)
                {
                    ActionItem5.IsEnabled = true;
                }
                else
                {
                    ActionItem5.IsEnabled = false;
                }
            }
        }

        private void TargetEnemy(Image image)
        {
            string enemyImage = image.Name;

            if (enemyImage == "EnemyCharacter1")
            {
                actionTargets.Add(fightScene.Enemies[0]);
            }
            else if (enemyImage == "EnemyCharacter2")
            {
                actionTargets.Add(fightScene.Enemies[1]);
            }
            else if (enemyImage == "EnemyCharacter3")
            {
                actionTargets.Add(fightScene.Enemies[2]);
            }
            else if (enemyImage == "EnemyCharacter4")
            {
                actionTargets.Add(fightScene.Enemies[3]);
            }
            else if (enemyImage == "EnemyCharacter5")
            {
                actionTargets.Add(fightScene.Enemies[4]);
            }
        }

        private void SelectAction(ListBoxItem selection)
        {
            string actionName = selection.Name;

            if (actionName == "ActionItem1")
            {
                selectedAction = selectedPC.Actions[0];
            }
            else if (actionName == "ActionItem2")
            {
                selectedAction = selectedPC.Actions[1];
            }
            else if (actionName == "ActionItem3")
            {
                selectedAction = selectedPC.Actions[2];
            }
            else if (actionName == "ActionItem4")
            {
                selectedAction = selectedPC.Actions[3];
            }
            else if (actionName == "ActionItem5")
            {
                selectedAction = selectedPC.Actions[4];
            }

            ActionDescriptionBox.Content = selectedAction.Name + "\n" + selectedAction.Description;
            numberOfTargets = selectedAction.NumTargets;
        }

        private void ResetUI()
        {
            selectedPC = null;
            selectedAction = null;
            selectedPCImage = null;
            actionTargets.Clear();

            NameValue.Content = "";
            HealthValue.Content = "";
            MaxHealthValue.Content = "";

            AttackValue.Content = "";
            WisdomValue.Content = "";
            SpeedValue.Content = "";
            DefenseValue.Content = "";

            ActionListBox.SelectedIndex = -1;

            ActionItem1.Content = "";
            ActionItem1.Visibility = Visibility.Hidden;
            ActionItem2.Content = "";
            ActionItem2.Visibility = Visibility.Hidden;
            ActionItem3.Content = "";
            ActionItem3.Visibility = Visibility.Hidden;
            ActionItem4.Content = "";
            ActionItem4.Visibility = Visibility.Hidden;
            ActionItem5.Content = "";
            ActionItem5.Visibility = Visibility.Hidden;

            ActionDescriptionBox.Content = "";
        }

        private void EndTurn()
        {            
            StatusLabel.Content = "Select a character.";

            state = CHARACTER_SELECT;
            
            ResetUI();

            charactersSelected = 0;

            // Reset Players
            PlayerCharacter1.IsEnabled = true;
            PlayerCharacter1.Opacity = 1;
            PlayerCharacter2.IsEnabled = true;
            PlayerCharacter2.Opacity = 1;
            PlayerCharacter3.IsEnabled = true;
            PlayerCharacter3.Opacity = 1;
            PlayerCharacter4.IsEnabled = true;
            PlayerCharacter4.Opacity = 1;
            PlayerCharacter5.IsEnabled = true;
            PlayerCharacter5.Opacity = 1;

            fightScene.ResolveActions();
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NextButton.IsEnabled = false;
            if (state == ACTION_SELECT)
            {
                // Disable further selection
                selectedPCImage.IsEnabled = false;
                selectedPCImage.Opacity = .8;

                if (numberOfTargets == 0)
                {
                    fightScene.AddAction(selectedAction, null, selectedPC.SPD);
                    if (charactersSelected == totalCharacters)
                    {
                        EndTurn();
                    }
                    else
                    {
                        state = CHARACTER_SELECT;
                        StatusLabel.Content = "Select a character.";
                        ResetUI();
                    }
                }
                else
                {
                    StatusLabel.Content = "Select " + numberOfTargets + " target.";
                    state = TARGET_SELECT;
                }
            }
        }

        public void OnActorHealthChanged(Actor actor, int damageTaken)
        {
            if (actor is PlayerCharacter)
            {
                if (actor == pc1)
                {
                    Storyboard myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(damageFade);
                    Storyboard.SetTargetName(damageFade, PCDamage1.Name);
                    Storyboard.SetTargetProperty(damageFade, new PropertyPath(Label.OpacityProperty));

                    PCDamage1.Content = damageTaken;

                    myStoryboard.Begin(this);
                }
                else if (actor == pc2)
                {
                    Storyboard myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(damageFade);
                    Storyboard.SetTargetName(damageFade, PCDamage2.Name);
                    Storyboard.SetTargetProperty(damageFade, new PropertyPath(Label.OpacityProperty));

                    PCDamage2.Content = damageTaken;

                    myStoryboard.Begin(this);
                }
                else if (actor == pc3)
                {
                    Storyboard myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(damageFade);
                    Storyboard.SetTargetName(damageFade, PCDamage3.Name);
                    Storyboard.SetTargetProperty(damageFade, new PropertyPath(Label.OpacityProperty));

                    PCDamage3.Content = damageTaken;

                    myStoryboard.Begin(this);
                }
                else if (actor == pc4)
                {
                    Storyboard myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(damageFade);
                    Storyboard.SetTargetName(damageFade, PCDamage4.Name);
                    Storyboard.SetTargetProperty(damageFade, new PropertyPath(Label.OpacityProperty));

                    PCDamage4.Content = damageTaken;

                    myStoryboard.Begin(this);
                }
                else if (actor == pc5)
                {
                    Storyboard myStoryboard = new Storyboard();
                    myStoryboard.Children.Add(damageFade);
                    Storyboard.SetTargetName(damageFade, PCDamage5.Name);
                    Storyboard.SetTargetProperty(damageFade, new PropertyPath(Label.OpacityProperty));

                    PCDamage5.Content = damageTaken;

                    myStoryboard.Begin(this);
                }
     
            }
            else if (actor is Enemy)
            {
                double width;
                for (int i = 0; i < 5; i++)
                {
                    if (actor == enemies[i].Actor)
                    {
                        width = ((double)enemies[i].Actor.HP / (double)enemies[i].Actor.MaxHP) * 100;
                        if (width > 0)
                        {
                            enemies[i].HealthBar.Width = width;
                        }
                        else
                        {
                            enemies[i].HealthBar.Width = 100;
                            enemies[i].HealthBar.Visibility = Visibility.Hidden;
                        }

                        Storyboard myStoryboard = new Storyboard();
                        myStoryboard.Children.Add(damageFade);
                        Storyboard.SetTargetName(damageFade, enemies[i].Damage.Name);
                        Storyboard.SetTargetProperty(damageFade, new PropertyPath(Label.OpacityProperty));

                        enemies[i].Damage.Content = damageTaken;

                        myStoryboard.Begin(this);
                        break;
                    }
                }
            }
        }

        private void OnActorDeath(Actor actor)
        {
            if (actor is PlayerCharacter)
            {
                if (actor == pc1)
                {
                    PlayerCharacter1.Source = null;
                }
                else if (actor == pc2)
                {
                    PlayerCharacter2.Source = null;
                }
                else if (actor == pc3)
                {
                    PlayerCharacter3.Source = null;
                }
                else if (actor == pc4)
                {
                    PlayerCharacter4.Source = null;
                }
                else if (actor == pc5)
                {
                    PlayerCharacter5.Source = null;
                }

            }
            else if (actor is Enemy)
            {
                if (actor == fightScene.Enemies[0])
                {
                    EnemyCharacter1.Source = null;
                }
                else if (actor == fightScene.Enemies[1])
                {
                    EnemyCharacter2.Source = null;
                }
                else if (actor == fightScene.Enemies[2])
                {
                    EnemyCharacter3.Source = null;
                }
                else if (actor == fightScene.Enemies[3])
                {
                    EnemyCharacter4.Source = null;
                }
                else if (actor == fightScene.Enemies[4])
                {
                    EnemyCharacter5.Source = null;
                }
            }
        }
    }
}
