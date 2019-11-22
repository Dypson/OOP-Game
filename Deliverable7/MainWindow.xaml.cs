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
using BaseObjects;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.Win32;
using System.Media;

namespace Deliverable7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            PlaySound("Birds_Songs_And_Calls.wav");
            ResizeMode = ResizeMode.NoResize;
            Game.ResetGame(10, 10);
            for (int row = 0; row <= Game.Map.Cells.GetUpperBound(0); row++)
                grdMap.RowDefinitions.Add(new RowDefinition());
            for (int col = 0; col <= Game.Map.Cells.GetUpperBound(1); col++)
                grdMap.ColumnDefinitions.Add(new ColumnDefinition());
            btnDown.Tag = Actor.Direction.Down;
            btnUp.Tag = Actor.Direction.Up;
            btnLeft.Tag = Actor.Direction.Left;
            btnRight.Tag = Actor.Direction.Right;
            DrawMap();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Serialize(FileMode.Create);
        }
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            DeSerialize(FileMode.Open);
        }
        private void Serialize(FileMode fS)
        {
            FileStream fs = null;
            try
            {
                SaveFileDialog serialize = new SaveFileDialog();
                serialize.Filter = "Map Files(*.map)|*.map";
                if (serialize.ShowDialog() == true)
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    fs = new FileStream(serialize.FileName, fS);
                    bf.Serialize(fs, Game.Map);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
       
        private void DeSerialize(FileMode fL)
        {
            FileStream fs = null;
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "MapFiles|*.map|All Files(*.*)|*.*";
                if (ofd.ShowDialog() == true)
                {
                    Map content;
                    fs = File.Open(ofd.FileName, fL);
                    BinaryFormatter bf = new BinaryFormatter();
                   content=(Map)bf.Deserialize(fs);
                    Game.Map = content;
                    DrawMap();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (fs != null) fs.Close();
            }
        }
        public  void DrawMap()
        {
           
            // make sure the grid is cleared first
            grdMap.Children.Clear();
            for (int row = 0; row <= Game.Map.Cells.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= Game.Map.Cells.GetUpperBound(1); col++)
                {
                    MapCell mcToCheck = Game.Map.Cells[row, col]; // get the current mapcell
                    TextBlock tbContents = new TextBlock(); // create a textblock to display contents
                    tbContents.TextAlignment = TextAlignment.Center;
                   if (mcToCheck.HasBeenSeen == false)
                   {
                        tbContents.Text = "";
                        //tbContents.Background = new SolidColorBrush(Colors.Black);
                   }
                    else if (mcToCheck.HasItem)
                    {
                        if (mcToCheck.Item.GetType() == typeof(Weapon))
                        {
                            tbContents.Background = new SolidColorBrush(Colors.Gray);
                        }
                        else if (mcToCheck.Item.GetType() == typeof(Potion))
                        {
                            Potion ptn = (Potion)mcToCheck.Item;
                            tbContents.Background = new SolidColorBrush(Colors.Green);
                        }
                        else if (mcToCheck.Item.GetType() == typeof(Door))
                            tbContents.Background = new SolidColorBrush(Colors.Peru);
                        else if (mcToCheck.Item.GetType() == typeof(DoorKey))
                            tbContents.Background = new SolidColorBrush(Colors.Gold);
                        tbContents.Text = mcToCheck.Item.Name;
                    }
                    else if (mcToCheck.HasMonster)
                    {
                        tbContents.Text = mcToCheck.Monster.Name(false);
                        tbContents.Background = new SolidColorBrush(Colors.DarkRed);
                        tbContents.Foreground = new SolidColorBrush(Colors.White);
                    }
                    if (mcToCheck.HasBeenSeen)
                    {
                       // PlaySound("evilLaugh.wav");  tbContents.Text = "";
                        tbContents.Background = new SolidColorBrush(Colors.WhiteSmoke);
                    }
                    if (Game.Map.Adventurer.PositionX == col
                        && Game.Map.Adventurer.PositionY == row)
                    {
                        tbContents.Text = Game.Map.Adventurer.Name(true);
                        tbContents.FontSize = 12;
                        tbContents.Foreground = new SolidColorBrush(Colors.Tomato);
                        tbContents.Background = new SolidColorBrush(Colors.DarkBlue);
                    }
                    tbContents.TextWrapping = TextWrapping.Wrap;
                    Grid.SetRow(tbContents, row);
                    Grid.SetColumn(tbContents, col);
                    grdMap.Children.Add(tbContents);
                }
            }
            UpdateHero();
        }
        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            Actor.Direction dir = (Actor.Direction)btn.Tag;
            bool needToAct = Game.Map.MoveAdventurer(dir);
            if (needToAct)
            {
                Window wnd = null;
                if (Game.Map.CurrentLocation.HasItem)
                {
                        wnd = new frmItem();
                }
                if (Game.Map.CurrentLocation.Item is Door)
                {
                    Game.GameState = Game.State.Won;
                    wnd = new frmGameOver();
                }
                else if (Game.Map.CurrentLocation.HasMonster)
                {                 
                    wnd = new frmMonster();

                }
                if (wnd != null) wnd.ShowDialog();
            }
            DrawMap();
        }
        private void BtnReset_Click(object sender, RoutedEventArgs e)
        {
            Game.Reset();
            DrawMap();
        }
        private void UpdateHero()
        {
            if (Game.Map.Adventurer.HasWeapon)
            {
                tbWeapon.Text = "Weapon: " + Game.Map.Adventurer.Weapon.Name;
                tbWeapon.Foreground = new SolidColorBrush(Colors.Goldenrod);
            }
            else
            {
                tbWeapon.Foreground = new SolidColorBrush(Colors.Black);
                tbWeapon.Text = "Weapon: None";
            }
            if (Game.Map.Adventurer.Key != null)
            {
                tbKey.Text = "Key: " + Game.Map.Adventurer.Key.Name;
                tbKey.Foreground = new SolidColorBrush(Colors.LightBlue);
            }
            else
            {
                tbKey.Foreground = new SolidColorBrush(Colors.Black);
                tbKey.TextAlignment = TextAlignment.Center;
                tbKey.Text = "Key: None";
            }
            double currenthp = Game.Map.Adventurer.CurrentHitPoints;
            double maxHP = Game.Map.Adventurer.MaximumHitPoints;
            if (currenthp < maxHP / 2)
            {
                tbName.Text = "Name: " + Game.Map.Adventurer.Name(true);
                tbHitPt.Text = "HP: " + Game.Map.Adventurer.CurrentHitPoints + "/" + Game.Map.Adventurer.MaximumHitPoints;
                tbName.Foreground = new SolidColorBrush(Colors.DarkRed);
                tbHitPt.Foreground = new SolidColorBrush(Colors.Red);
            }
            else if (currenthp<maxHP-30)
            {
                tbName.Text = "Name: " + Game.Map.Adventurer.Name(true);
                tbHitPt.Text = "HP: " + Game.Map.Adventurer.CurrentHitPoints + "/" + Game.Map.Adventurer.MaximumHitPoints;
                tbName.Foreground = new SolidColorBrush(Colors.LightYellow);
                tbHitPt.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                tbName.TextAlignment = TextAlignment.Center;
                tbName.Text = "Name: " + Game.Map.Adventurer.Name(true);
                tbHitPt.Text = "HP: " + Game.Map.Adventurer.CurrentHitPoints + "/" + Game.Map.Adventurer.MaximumHitPoints;
                tbName.Foreground = new SolidColorBrush(Colors.Tomato);
                tbHitPt.Foreground = new SolidColorBrush(Colors.GreenYellow);
            }
              
         
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                btnMove_Click(btnUp, null);
            }
            else if (e.Key == Key.Down)
            {
                btnMove_Click(btnDown, null);
            }
            else if (e.Key == Key.Left)
            {
                btnMove_Click(btnLeft, null);
            }
            else if (e.Key == Key.Right)
            {
                btnMove_Click(btnRight, null);
            }
        }
       public static void PlaySound(string path)
        {
           SoundPlayer player = new SoundPlayer(path);
            player.Load();
            player.Play();
        }
    }
}

