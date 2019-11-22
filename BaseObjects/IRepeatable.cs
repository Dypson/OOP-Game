using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Imported from prof Holmes
namespace BaseObjects {
    /// <summary>
    /// Interface for an Object that needs to be copied
    /// </summary>
    interface IRepeatable<T> {
        /// <summary>
        /// Create a copy of this object
        /// </summary>
        /// <returns>copy of the object</returns>
        T CreateCopy();
    }
}
