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
using System.Text.RegularExpressions;
using Code_Challenges.Monty_Hall_Problem;

namespace Code_Challenges
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Strategy stategy = new Strategy();

        public MainWindow()
        {
            InitializeComponent();

            DoorChoice1.ItemsSource = Enum.GetValues(typeof(Strategy.DoorChoice));
            DoorChoice2.ItemsSource = Enum.GetValues(typeof(Strategy.DoorChoiceExp));
            DoorChoice1_Strat2.ItemsSource = Enum.GetValues(typeof(Strategy.DoorChoice));
            DoorChoice2_Strat2.ItemsSource = Enum.GetValues(typeof(Strategy.DoorChoiceExp));
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var ra = float.Parse(RunAmount.Text);
            var play = new Game();
            var winCount = 0.0f;
            var switchStrat = Switcher.IsChecked;
            var usingFirstStrat = true;

            if(ra > 0)
            {
                if(DoorChoice1.SelectedIndex >= 0 && (switchStrat == false || DoorChoice1_Strat2.SelectedIndex >=0))
                {
                    stategy.FirstDoor = (Strategy.DoorChoice)DoorChoice1.SelectedItem;

                    if (DoorChoice2.SelectedIndex >= 0 && (switchStrat == false || DoorChoice2_Strat2.SelectedIndex >= 0))
                    {
                        stategy.SecondDoor = (Strategy.DoorChoiceExp)DoorChoice2.SelectedItem;

                        for(var i = 0; i<ra; i++)
                        {
                            var wonTurn = false;
                            play.PlayerSelect(stategy);
                            play.GameRemoveDoor();
                            play.PlayerFinalDecision(stategy);

                            if (play.PlayerWon())
                            {
                                winCount++;
                                wonTurn = true;
                            }

                            if(switchStrat == true && !wonTurn)
                            {
                                if(usingFirstStrat)
                                {
                                    stategy.FirstDoor = (Strategy.DoorChoice)DoorChoice1_Strat2.SelectedItem;
                                    stategy.SecondDoor = (Strategy.DoorChoiceExp)DoorChoice2_Strat2.SelectedIndex;
                                }
                                else
                                {
                                    stategy.FirstDoor = (Strategy.DoorChoice)DoorChoice1.SelectedItem;
                                    stategy.SecondDoor = (Strategy.DoorChoiceExp)DoorChoice2.SelectedItem;
                                }
                                usingFirstStrat = !usingFirstStrat;
                            }

                            play.Restart();
                        }
                    }
                }
            }

            var successRate = winCount / ra;

            successRate = successRate * 100;

            SuccessRate.Content = successRate + "%";
        }
    }
}
