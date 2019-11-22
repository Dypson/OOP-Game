/*
Imported From Prof Holmes
CS/INFO 1182 
Deepson Khadka
4/12/2018
Description - Potions are items that can heal a hero
*/
using BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BaseObjects {
    [Serializable]
    public class Potion : Item, IRepeatable<Potion> {
        private Colors _Color;

        /// <summary>
        /// Possible colors of a potion.
        /// </summary>
        public enum Colors {
            Red,Green,Blue,Purple,Orange
        }

        /// <summary>
        /// Overloaded potion constructor
        /// </summary>
        /// <param name="newName">Name of the potion</param>
        /// <param name="value">Amount of healing potion can do</param>
        /// <param name="clr">What color the potion should be</param>
        public Potion(String newName, int value, Colors clr)
            : base(newName, value) {
            _Color = clr;
        }

        /// <summary>
        /// Get and set the color of the potion.
        /// </summary>
        public Colors Color {
            get {
                return _Color;
            }

            set {
                _Color = value;
            }
        }

        /// <summary>
        /// Create a deep copy of this potion.
        /// </summary>
        /// <returns></returns>
        public Potion CreateCopy() {
            return new Potion(this._Name, this._AffectValue, this._Color);
        }
    }
}
