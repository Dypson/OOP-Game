using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Imported from Prof Holmes
namespace BaseObjects {
    /// <summary>
    /// Imported from Prof Holmes
    /// Interface for an Object that can be fought
    /// </summary>
    interface ICombat {
        /// <summary>
        /// Attack an actor
        /// </summary>
        /// <returns>true if the actor is still alive, false if not</returns>
        bool Attack(Actor a);
    }
}
