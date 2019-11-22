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
using System.Windows.Shapes;
using BaseObjects;

namespace Deliverable7
{
    /// <summary>
    /// Interaction logic for frmGameOver.xaml
    /// </summary>
    public partial class frmGameOver : Window
    {
        public frmGameOver()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            GameResult();
            Unlock();
        }
        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Game.Reset();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void GameResult()
        {
            if (Game.GameState == Game.State.Won)
            {
                tbMessage.TextAlignment = TextAlignment.Center;
             tbMessage.Text = "Game Over \r\n You have Won the Game!";
            }
           else if (Game.GameState == Game.State.Lost)
           {
                btnOk.Visibility = Visibility.Hidden;
                tbMessage.TextAlignment = TextAlignment.Center;
                tbMessage.Text = "Game Over \r\n Opps! You Lost";
           }

           else if (Game.GameState == Game.State.Running)
            {
                tbMessage.TextAlignment = TextAlignment.Center;
                btnOk.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Hidden;
                btnRestart.Visibility = Visibility.Hidden;
           }
        }
        public void Unlock()
        {
            if (Game.Map.CurrentLocation.HasItem )
              if(Game.Map.CurrentLocation.Item.GetType() == typeof(Door))
                {
                    {
                        if (Game.Map.Adventurer.Key != null)
                        {
                            Door door = (Door)Game.Map.CurrentLocation.Item;
                            if (door.isMatch(Game.Map.Adventurer.Key))
                            {
                                Game.GameState = Game.State.Won;
                                btnOk.Visibility = Visibility.Hidden;
                                btnRestart.Visibility = Visibility.Visible;
                                btnExit.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                tbMessage.Text = "Invalid Key";
                            }
                        }
                        else
                        {
                            tbMessage.TextAlignment = TextAlignment.Center;
                            tbMessage.Text = "Here's the door for you. Now, Find the Key to Unlock";
                            btnOk.Visibility = Visibility.Visible;
                            btnExit.Visibility = Visibility.Hidden;
                           btnRestart.Visibility = Visibility.Hidden;
                        }
                    }
                }
        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
    }
