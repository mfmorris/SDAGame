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

        PlayerCharacter pc1 = null;
        PlayerCharacter pc2 = null;
        PlayerCharacter pc3 = null;
        PlayerCharacter pc4 = null;
        PlayerCharacter pc5 = null;
        PlayerCharacter selectedPC = null;
        int charactersSelected = 0;
        int totalCharacters = 0;
        
        Action selectedAction = null;
        int numberOfTargets = 0;

        Enemy enemy1 = null;
        Enemy enemy2 = null;
        Enemy enemy3 = null;
        Enemy enemy4 = null;
        Enemy enemy5 = null;
        
        public MainWindow()
        {
            InitializeComponent();

            FightScene fightScene = new FightScene();
            MonsterBuilder monsterBuilder = new MonsterBuilder(fightScene);

            // Generate Characters
            pc1 = new Druid();
            PlayerCharacter1.Source = pc1.image;
            PCHealthBar1.Visibility = Visibility.Visible;

            pc2 = new Viking();
            PlayerCharacter2.Source = pc2.image;
            PCHealthBar2.Visibility = Visibility.Visible;

            totalCharacters = 2;

            // Generate Enemies
            enemy1 = monsterBuilder.GetKobold();
            EnemyCharacter1.Source = enemy1.image;
            EnemyHealthBar1.Visibility = Visibility.Visible;

            enemy2 = monsterBuilder.GetKobold();
            EnemyCharacter2.Source = enemy2.image;
            EnemyHealthBar2.Visibility = Visibility.Visible;

            enemy3 = monsterBuilder.GetKobold();
            EnemyCharacter3.Source = enemy3.image;
            EnemyHealthBar3.Visibility = Visibility.Visible;

            state = CHARACTER_SELECT;
            StatusLabel.Content = "Select a character.";
        }

        private void ActionItem_Selected(object sender, RoutedEventArgs e)
        {
            // Select an action
            if (state == ACTION_SELECT)
            {
                state = TARGET_SELECT;

                // Update UI
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
        }

        private void Enemy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (state == TARGET_SELECT)
            {
                state = CHARACTER_SELECT;
                if (charactersSelected == totalCharacters)
                {
                    EndTurn();
                }
                else
                {
                    // Update UI
                    numberOfTargets--;
                    if (numberOfTargets <= 0)
                    {
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
                        state = TARGET_SELECT;
                        StatusLabel.Content = "Select " + numberOfTargets + " target.";
                    }
                }
            }
        }

        private void UpdateUIWithPC(Image image)
        {
            ResetUI();

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

            // Disable further selection
            image.IsEnabled = false;
            image.Opacity = .8;

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

        private void UpdateUIWithEnemy(Image image)
        {
            string enemyImage = image.Name;
            Enemy enemy = null;

            if (enemyImage == "EnemyCharacter1")
            {
                enemy = enemy1;
            }
            else if (enemyImage == "EnemyCharacter2")
            {
                enemy = enemy2;
            }
            else if (enemyImage == "EnemyCharacter3")
            {
                enemy = enemy3;
            }
            else if (enemyImage == "EnemyCharacter4")
            {
                enemy = enemy4;
            }
            else if (enemyImage == "EnemyCharacter5")
            {
                enemy = enemy5;
            }

            NameValue.Content = enemy.Name;
            HealthValue.Content = enemy.HP;
            MaxHealthValue.Content = enemy.MaxHP;

            AttackValue.Content = enemy.ATK;
            WisdomValue.Content = enemy.WIS;
            SpeedValue.Content = enemy.SPD;
            DefenseValue.Content = enemy.DEF;
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

            ActionDescriptionBox.Content = selectedAction.Name + "\nNumber of targets: " + selectedAction.NumTargets;
            numberOfTargets = selectedAction.NumTargets;
            if (numberOfTargets == 0)
            {
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
            }
        }

        private void ResetUI()
        {
            selectedPC = null;
            selectedAction = null;

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
        }
    }
}
