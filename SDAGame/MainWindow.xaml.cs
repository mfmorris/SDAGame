﻿using System;
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

namespace SDAGame
{
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

        //Enemy enemy1 = null;
        //Enemy enemy2 = null;
        //Enemy enemy3 = null;
        //Enemy enemy4 = null;
        //Enemy enemy5 = null;
        
        public MainWindow()
        {
            InitializeComponent();

            actionTargets = new List<Enemy>();

            // Generate Characters
            pc1 = new Druid();
            PlayerCharacter1.Source = pc1.image;

            pc2 = new Viking();
            PlayerCharacter2.Source = pc2.image;

            totalCharacters = 2;

            fightScene = new FightScene(pc1, pc2);
            fightScene.OnActorHealthChanged += UpdateActor;
            fightScene.OnActorDeath += KillActor;

            // Generate Enemies
            //enemy1 = fightScene.Enemies[0];
            EnemyCharacter1.Source = fightScene.Enemies[0].image;
            EnemyHealthBar1.Visibility = Visibility.Visible;

            //enemy2 = fightScene.Enemies[1];
            EnemyCharacter2.Source = fightScene.Enemies[1].image;
            EnemyHealthBar2.Visibility = Visibility.Visible;

            //enemy3 = fightScene.Enemies[2];
            EnemyCharacter3.Source = fightScene.Enemies[2].image;
            EnemyHealthBar3.Visibility = Visibility.Visible;

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
            }
            if (selectedPC.Actions.Count > 1)
            {
                ActionItem2.Content = selectedPC.Actions[1].Name;
                ActionItem2.Visibility = Visibility.Visible;
            }
            if (selectedPC.Actions.Count > 2)
            {
                ActionItem3.Content = selectedPC.Actions[2].Name;
                ActionItem3.Visibility = Visibility.Visible;
            }
            if (selectedPC.Actions.Count > 3)
            {
                ActionItem4.Content = selectedPC.Actions[3].Name;
                ActionItem4.Visibility = Visibility.Visible;
            }
            if (selectedPC.Actions.Count > 4)
            {
                ActionItem5.Content = selectedPC.Actions[4].Name;
                ActionItem5.Visibility = Visibility.Visible;
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

        public void UpdateActor(Actor actor, int damageTaken)
        {
            if (actor is PlayerCharacter)
            {
                if (actor == pc1)
                {

                }
                else if (actor == pc2)
                {

                }
                else if (actor == pc3)
                {

                }
                else if (actor == pc4)
                {

                }
                else if (actor == pc5)
                {

                }
     
            }
            else if (actor is Enemy)
            {
                if (actor == fightScene.Enemies[0])
                {
                    EnemyHealthBar1.Width = ((double)fightScene.Enemies[0].HP / (double)fightScene.Enemies[0].MaxHP) * 100;
                }
                else if (actor == fightScene.Enemies[1])
                {
                    EnemyHealthBar2.Width = ((double)fightScene.Enemies[1].HP / (double)fightScene.Enemies[1].MaxHP) * 100;
                }
                else if (actor == fightScene.Enemies[2])
                {
                    EnemyHealthBar3.Width =( (double)fightScene.Enemies[2].HP / (double)fightScene.Enemies[2].MaxHP) * 100;
                }
                else if (actor == fightScene.Enemies[3])
                {
                    EnemyHealthBar4.Width = ( (double)fightScene.Enemies[3].HP / (double)fightScene.Enemies[3].MaxHP) * 100;
                }
                else if (actor == fightScene.Enemies[4])
                {
                    EnemyHealthBar5.Width = ((double)fightScene.Enemies[4].HP / (double)fightScene.Enemies[4].MaxHP) * 100;
                }
            }
        }

        private void KillActor(Actor actor)
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
