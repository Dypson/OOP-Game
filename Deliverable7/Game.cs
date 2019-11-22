using BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deliverable7 {
    static class Game {
        static private State _GameState;
        static private Map _Map;
        static private int _Height;
        static private int _Width;

        /// <summary>
        /// All the possible states of our game.
        /// </summary>
        public enum State { Lost, Running, Won }
        /// <summary>
        ///  the current game state of our game.
        /// </summary>
        public static State GameState {
            get {
                if (_Map != null && _Map.Adventurer != null &&
                    _Map.Adventurer != null && !_Map.Adventurer.IsAlive)
                    _GameState = State.Lost;
                return _GameState;
            }
            set {
                _GameState = value;
            }
        }
        public static Map Map {
            get {
                return _Map;
            }
            set
            {
                _Map = value;
            }
        }
        public static void ResetGame(int height, int width) {
            _Height = height;
            _Width = width;
            _Map = new Map(height, width);
            int newX, newY;
            Random rnd = new Random();
            do {
                newX = rnd.Next(0, width);
                newY = rnd.Next(0, height);
            } while (_Map.Cells[newY, newX].HasItem);
            _Map.Adventurer = new Hero("Bob", "the Awesome", 1.0, 200, newX, newY);
            _Map.CurrentLocation.HasBeenSeen = true;
            _GameState = State.Running;
        }
        public static void Reset()
        {
            ResetGame(_Height, _Width);
        }
    }
}
