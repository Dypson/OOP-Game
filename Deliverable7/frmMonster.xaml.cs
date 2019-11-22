// Jon Holmes
// CS / INFO 1182
// Description - Show Monster found
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

namespace Deliverable7 {
    /// <summary>
    /// Interaction logic for frmMonster.xaml
    /// </summary>
    public partial class frmMonster : Window {
        public frmMonster() {
            ResizeMode = ResizeMode.NoResize;
            InitializeComponent();
            FighterUpdate();
            MainWindow.PlaySound("evilLaugh.wav");
        }

        private void btnAttack_Click(object sender, RoutedEventArgs e)
        {
            Game.Map.Adventurer.DamageMe(Game.Map.CurrentLocation.Monster.AttackValue);
            bool fight = Game.Map.Adventurer + Game.Map.CurrentLocation.Monster;
            Window wnd = null;
            if (Game.Map.Adventurer.IsAlive==false)
            {
                Game.GameState = Game.State.Lost;
                wnd = new frmGameOver();
                wnd.Show();
                this.Close();
            }
            else if (Game.Map.CurrentLocation.Monster.IsAlive==false)
            {
                Game.Map.Cells[Game.Map.Adventurer.PositionY, Game.Map.Adventurer.PositionX].Monster = null;
                this.Close();
            }
            else
            {
                FighterUpdate();
            }
            
        }
        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
                Game.Map.Adventurer.IsRunningAway = true;
                bool alive = Game.Map.Adventurer + Game.Map.CurrentLocation.Monster;
                if (alive == false)
                {
                    Game.GameState = Game.State.Lost;
                }
            FighterUpdate();
            this.Close();
        }
        private void FighterUpdate()
        {
            txtFound.Text = String.Format("You encountered a {0}", Game.Map.CurrentLocation.Monster.Name(false));
            lblHero.Content = Game.Map.Adventurer.Name(false) + "\r\n" +
                +Game.Map.Adventurer.CurrentHitPoints + "/" + 
                Game.Map.Adventurer.MaximumHitPoints;
            lblMonster.Content = Game.Map.CurrentLocation.Monster.Name(false)+"\r\n"
                +Game.Map.CurrentLocation.Monster.CurrentHitPoints+"/"
                +Game.Map.CurrentLocation.Monster.MaximumHitPoints;
            Game.GameState = Game.State.Lost;
        }
        /// <summary>
        /// Close the window
        /// </summary>

    }
}
