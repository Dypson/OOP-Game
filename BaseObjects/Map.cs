
// Imported From Prof Holmes
//CS/INFO 1182 
//Deepson Khadka
//4/12/2018
// Description - Representation of our Map. It needs to know everything about what is on the Map.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseObjects {
    [Serializable]
    public class Map {
        #region "Private Variables"
        private MapCell[,] _Cells = null;
        private List<Monster> _Monsters = new List<Monster>();
        private List<Item> _Items = new List<Item>();
        private Hero _Adventurer;

        /// <summary>
        /// Thing we will want a count of.
        /// </summary>
        private enum ThingToCount {
            Monster,
            Item,
            Discovered
        }
        #endregion

        #region "Constructors"
        /// <summary>
        /// Create and fill the Map
        /// </summary>
        /// <param name="rows">Number of Rows the map should have</param>
        /// <param name="cols">Number of Columns the map should have</param>
        public Map(int rows, int cols) {
            _Cells = new MapCell[rows, cols];
            fillMonsters();
            fillPotions();
            fillWeapons();
            fillMap();
        }
        #endregion
        #region "Private Methods"
        /// <summary>
        /// Fill the List of Potions
        /// </summary>
        private void fillPotions() {
            _Items.Add(new Potion("Small Healing Potion", 25, Potion.Colors.Red));
            _Items.Add(new Potion("Medium Healing Potion", 50, Potion.Colors.Blue));
            _Items.Add(new Potion("Large Healing Potion", 100, Potion.Colors.Green));
            _Items.Add(new Potion("Extreme Healing Potion", 200, Potion.Colors.Purple));
        }
        /// <summary>
        /// Fill the list of Weapons
        /// </summary>
        private void fillWeapons() {
            _Items.Add(new Weapon("Dagger", 10, -1));
            _Items.Add(new Weapon("Club", 20, -3));
            _Items.Add(new Weapon("Sword", 30, -2));
            _Items.Add(new Weapon("Claymore", 40, -4));
        }
        /// <summary>
        /// Fill the List of Monsters
        /// </summary>
        private void fillMonsters() {
            _Monsters.Add(new Monster("Orc", "", 3, 100, 0, 0, 10));
            _Monsters.Add(new Monster("Goblin", "", 1, 30, 0, 0, 5));
            _Monsters.Add(new Monster("Slug", "", 5, 3, 0, 0, 2));
            _Monsters.Add(new Monster("Rat", "", 0, 5, 0, 0, 1));
            _Monsters.Add(new Monster("Skeleton", "", 4, 70, 0, 0, 7));
        }



        /// <summary>
        /// Fill the map with empty MapCells
        /// </summary>
        private void fillMap() {
            Random rnd = new Random();
            int rows = Cells.GetLength(0);
            int cols = Cells.GetLength(1);
            for (int row = 0; row < rows; row++) {
                for (int col = 0; col < cols; col++) {
                    MapCell mc = new MapCell();
                    if (rnd.Next(10) % 10 == 0) {
                        // add a monster
                        mc.Monster = (Monster)Monsters[rnd.Next(Monsters.Count)].CreateCopy();
                    } else if (rnd.Next(10) % 10 == 0) {
                        // add an item
                        Item itm = (Item)_Items[rnd.Next(Items.Count)];
                        if (itm.GetType() == typeof(Weapon))
                            mc.Item = (Weapon)((Weapon)itm).CreateCopy();
                        else if (itm.GetType() == typeof(Potion))
                            mc.Item = (Potion)((Potion)itm).CreateCopy();
                    }
                    Cells[row, col] = mc;
                }
            }
            SetKeyAndDoorLocation(rnd, rows - 1, cols - 1, "AAAA");
        }

        /// <summary>
        /// Set the location of the Key and Door.
        /// </summary>
        /// <param name="rnd">Randomizer to use</param>
        /// <param name="rows">Max number of rows in the map</param>
        /// <param name="cols">Max number of columms in the map</param>
        /// <param name="code">Code to be shared by key and door.</param>
        private void SetKeyAndDoorLocation(Random rnd, int rows, int cols, String code) {
            MapCell keyLocation, doorLocation;
            keyLocation = doorLocation = Cells[rnd.Next(rows), rnd.Next(cols)];
            while (keyLocation.Monster != null || keyLocation.Item != null) // get new location
                keyLocation = Cells[rnd.Next(rows), rnd.Next(cols)];
            while (keyLocation == doorLocation || doorLocation.Monster != null || doorLocation.Item != null) // get new location
                doorLocation = Cells[rnd.Next(rows), rnd.Next(cols)];
            // set key and door.
            keyLocation.Item = new DoorKey("Key", 0, code);
            doorLocation.Item = new Door("Door", 0, code);
        }

        /// <summary>
        /// Hero that the user controls
        /// </summary>
        public Hero Adventurer {
            get {
                return _Adventurer;
            }

            set {
                _Adventurer = value;
            }
        }

        /// <summary>
        /// Gets the mapcell where the adventurer is on the map.
        /// </summary>
        public MapCell CurrentLocation {
            get {
                return Cells[Adventurer.PositionY, Adventurer.PositionX];
            }
        }

        /// <summary>
        /// Move the adventuerer around the map. This method makes sure the user cannot
        /// go off of the map.
        /// </summary>
        /// <param name="dir">Direction the adventurer is expected to move.</param>
        /// <returns></returns>
        public bool MoveAdventurer(Actor.Direction dir) {
            int maxRow = Cells.GetUpperBound(0);
            int maxCol = Cells.GetUpperBound(1);
            Adventurer.Move(dir);
            // move hero back if he has left the board
            if (Adventurer.PositionX < 0) Adventurer.Move(Actor.Direction.Right);
            if (Adventurer.PositionX > maxCol) Adventurer.Move(Actor.Direction.Left);
            if (Adventurer.PositionY < 0) Adventurer.Move(Actor.Direction.Down);
            if (Adventurer.PositionY > maxRow) Adventurer.Move(Actor.Direction.Up);
            CurrentLocation.HasBeenSeen = true;
            return CurrentLocation.HasItem || CurrentLocation.HasMonster;
        }
        #endregion

        #region "Public Properties"
        /// <summary>
        /// Get all of the cells of the map
        /// </summary>
        public MapCell[,] Cells {
            get {
                if (_Cells == null) fillMap();
                return _Cells;
            }

            set {
                _Cells = value;
            }
        }
        /// <summary>
        /// Get the List of Monsters available on our map
        /// </summary>
        private List<Monster> Monsters {
            get {
                return _Monsters;
            }
        }
        /// <summary>
        /// Get the List of Potions and Weapons available on our map
        /// </summary>
        private List<Item> Items {
            get {
                return _Items;
            }
        }

        /// <summary>
        /// Get the count of monster currently on our map.
        /// </summary>
        public int NumberOfMonsters {
            get {
                return countOfThing(ThingToCount.Monster);
            }
        }

        /// <summary>
        /// Get the count of monster currently on our map.
        /// </summary>
        public int NumberOfItems {
            get {
                return countOfThing(ThingToCount.Item);
            }
        }

        /// <summary>
        /// Get the count of monster currently on our map.
        /// </summary>
        public double PercentDiscovered {
            get {
                double foundCells = countOfThing(ThingToCount.Discovered);
                return foundCells / Cells.Length;
            }
        }

        /// <summary>
        /// Count things in the Map.
        /// </summary>
        /// <param name="toCount"></param>
        /// <returns></returns>
        private int countOfThing(ThingToCount toCount) {
            int thingCount = 0;
            for (int r = 0; r < Cells.GetLength(0); r++) {
                for (int c = 0; c < Cells.GetLength(1); c++) {
                    MapCell mc = Cells[r, c];
                    if (toCount == ThingToCount.Monster && mc.HasMonster
                        || toCount == ThingToCount.Item && mc.HasItem
                        || toCount == ThingToCount.Discovered && mc.HasBeenSeen) {
                        thingCount++;
                    }
                }
            }
            return thingCount;
        }

        /// <summary>
        /// Get the Max Hitpoints for all monsters.
        /// </summary>
        /// <returns>the maximum current hitpoints of any monster in the game.</returns>
        public int MaxMonsterHP {
            get {
                return _Monsters.Max(m => m.CurrentHitPoints);
            }
        }

        /// <summary>
        /// Get the Min Damage for all weapons.
        /// </summary>
        /// <returns>the minimum damage of any weapon in the game.</returns>
        public int LeastWeaponDamage {
            get {
                var lst = Items.Where(i => i.GetType() == typeof(Weapon));
                return lst.Min(w => w.AffectValue);
            }
        }

        /// <summary>
        /// Get the Min Damage for all weapons.
        /// </summary>
        /// <returns>the minimum damage of any weapon in the game.</returns>
        public int AveragePotionEffect {
            get {
                var lst = Items.Where(i => i.GetType() == typeof(Potion));
                return lst.Min(w => w.AffectValue);
            }
        }


        #endregion

    }
}
