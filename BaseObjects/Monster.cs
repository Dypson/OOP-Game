/*
Imported From Prof Holmes
CS/INFO 1182 
Deepson Khadka
4/12/2018
Description - Monsters are attackable actors in the game that fight heroes.
*/
using BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseObjects {
    [Serializable]
   public class Monster:Actor, IRepeatable<Monster>, ICombat {
        private int _AttackValue = 0;
        /// <summary>
        /// Overloaded Monster constructor
        /// </summary>
        /// <param name="newName">Name of the monster</param>
        /// <param name="newTitle">Title of the monster</param>
        /// <param name="myAttackSpeed">Speed at which the monster can attack</param>
        /// <param name="hitPoints">Number of HP the monster will start with</param>
        /// <param name="startingPositionX">Starting X position of the monster</param>
        /// <param name="startingPositionY">Starting Y position of the monster</param>
        /// <param name="attackDamage">Amount of damage that the monster can do to a hero</param>
        public Monster(String newName, String newTitle, double myAttackSpeed, int hitPoints, int startingPositionX, int startingPositionY, int attackDamage) 
            :base(newName,newTitle,myAttackSpeed,hitPoints,startingPositionX,startingPositionY ){
            _AttackValue = attackDamage;
        }
        /// <summary>
        /// Get how much damage the monster can do to a hero
        /// </summary>
        public int AttackValue {
            get {
                return _AttackValue;
            }
        }

        /// <summary>
        /// Attack a given actor
        /// </summary>
        /// <param name="hro">Actor to attack</param>
        /// <returns>true if the attacked actors is still alive, false if not.</returns>
        public bool Attack(Actor hro) {
            hro.DamageMe(this.AttackValue);
            return hro.IsAlive;
        }

        /// <summary>
        /// Create a deep copy of this Monster.
        /// </summary>
        /// <returns></returns>
        public Monster CreateCopy() {
            return new Monster(this.Name(),this._Title, this.AttackSpeed,
                this.MaximumHitPoints, this.PositionX, this.PositionY, AttackValue);
        }
    }
}
