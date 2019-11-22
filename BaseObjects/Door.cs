using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseObjects {
    [Serializable]
    /// <summary>
    /// Item to be found at the end of the game.
    /// Imported from Prof Holmes
    /// </summary>
    public class Door : Item {
        private string _Code;
        public Door(String name, int value, String code) : base(name, value) {
            _Code = code;
        }
        /// <summary>
        /// Check if a given key matches this door
        /// </summary>
        /// <param name="key">key to check against this door</param>
        /// <returns>true if the key code matches the door code. False otherwise.</returns>
        public bool isMatch(DoorKey key) {
            return key.Code == _Code;
        }


    }
}
