using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseObjects {
    [Serializable]
    /// <summary>
    /// Object needed to unlock a door
    /// Imported from Prof Holmes
    /// </summary>
   public class DoorKey:Item {
        private string _Code;

        public DoorKey(string name, int value, string code):base(name, value) {
            _Code = code;
        }
        /// <summary>
        /// Code to unlock a door
        /// </summary>
        public string Code {
            get {
                return _Code;
            }
        }
    }
}
