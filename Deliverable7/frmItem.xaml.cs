// Jon Holmes
// CS / INFO 1182
// Description - Show Item found
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
    /// Interaction logic for frmMonster.xaml
    /// </summary>
    public partial class frmItem : Window
    {
        public frmItem()
        {
            InitializeComponent();
            ResizeMode = ResizeMode.NoResize;
            if (Game.Map.CurrentLocation.Item.GetType() == typeof(Potion))
            { 
                txtFound.Text = String.Format("You found a {0}", Game.Map.CurrentLocation.Item.Name + "\r\n" + "Do you wanna drink this?");
                txtFound.TextAlignment = TextAlignment.Center;
            }
            if (Game.Map.CurrentLocation.Item.GetType() == typeof(Weapon))
            {
                txtFound.Text = String.Format("You found a {0}", Game.Map.CurrentLocation.Item.Name + "\r\n" + "Do you wanna Equip this?");
                txtFound.TextAlignment = TextAlignment.Center;
            }
            if (Game.Map.CurrentLocation.Item.GetType() == typeof(DoorKey))
            {
                txtFound.Text = String.Format("You found a {0}", Game.Map.CurrentLocation.Item.Name) + "\r\n" + "Do you wanna unlock the door?";
                txtFound.TextAlignment = TextAlignment.Center;
            }

        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            Game.Map.CurrentLocation.Item = Game.Map.Adventurer.Apply(Game.Map.CurrentLocation.Item);
            this.Close();
        }

        /// <summary>
        /// Close the window
        /// </summary>
    }
}
