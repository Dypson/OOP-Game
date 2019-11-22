/*
Imported From Prof Holmes
CS/INFO 1182 
Deepson Khadka
4/12/2018
Description - Heroes are player caracters who can have a weapon
*/
using BaseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseObjects {
    [Serializable]
    public class Hero : Actor, ICombat {

        private Weapon _EquippedWeapon;
        private bool _IsRunningAway;
        private DoorKey _Key;

        /// <summary>
        /// Overloaded Constructor for a hero
        /// </summary>
        /// <param name="newName">Hero's Name</param>
        /// <param name="newTitle">Hero's Title</param>
        /// <param name="atkSpeed">Hero's AttackSpeed</param>
        /// <param name="hitPoints">Hero's Starting HP</param>
        /// <param name="startingPositionX">Hero's Starting X Position</param>
        /// <param name="startingPositionY">Hero's Starting Y Position</param>
        public Hero(String newName, String newTitle, double atkSpeed, int hitPoints, int startingPositionX, int startingPositionY)
            : base(newName, newTitle, atkSpeed, hitPoints, startingPositionX, startingPositionY) {
            _EquippedWeapon = null;
        }

        /// <summary>
        /// Get Hero's attack speed
        /// </summary>
        public override double AttackSpeed {
            get {
                if (HasWeapon) {
                    return base.AttackSpeed - Weapon.SpeedModifier;
                } else {
                    return base.AttackSpeed;
                }
            }
        }
        /// <summary>
        /// Get if the hero has a weapon
        /// </summary>
        public bool HasWeapon {
            get {
                return _EquippedWeapon != null;
            }
        }

        /// <summary>
        /// Get the hero's current weapon
        /// </summary>
        public Weapon Weapon {
            get {
                return _EquippedWeapon;
            }
        }

        /// <summary>
        /// Get the Key the hero is holding.
        /// </summary>
        public DoorKey Key {
            get { return _Key; }
        }

        /// <summary>
        /// Get the attack damage this hero can inflict.
        /// </summary>
        public int AttackDamage {
            get {
                if (HasWeapon) return Weapon.AffectValue;
                else return 1;
            }
        }
        /// <summary>
        /// Get or set whether the hero is running away.
        /// </summary>
        public bool IsRunningAway {
            get {
                return _IsRunningAway;
            }

            set {
                _IsRunningAway = value;
            }
        }

        /// <summary>
        /// Attack a given actor
        /// </summary>
        /// <param name="monst">Actor to attack</param>
        /// <returns>true if the attacked actors is still alive, false if not.</returns>
        public bool Attack(Actor monst) {
            monst.DamageMe(this.AttackDamage);
            return monst.IsAlive;
        }

        /// <summary>
        /// Move the hero.
        /// </summary>
        /// <param name="dirToMove">Direction the hero will move.</param>
        public override void Move(Actor.Direction dirToMove) {
            base.Move(dirToMove);
        }

        /// <summary>
        /// Apply Item to Hero
        /// </summary>
        /// <param name="newItem">Item to Apply to Hero</param>
        /// <returns>Item that hero needs to discard.</returns>
        public Item Apply(Item newItem) {
            Item retItem = null;
            if (newItem.GetType() == typeof(Potion)) {
                this.HealMe(newItem.AffectValue);
            } else if (newItem.GetType() == typeof(Weapon)) {
                retItem = _EquippedWeapon;
                _EquippedWeapon = (Weapon)newItem;
            } else if (newItem.GetType() == typeof(DoorKey)) {
                retItem = _Key;
                _Key = (DoorKey)newItem;
            } else {
                retItem = newItem;
            }
            return retItem;
        }
        
        /// <summary>
        /// Operator for adding a hero to a monster.
        /// </summary>
        /// <param name="h">Hero to add</param>
        /// <param name="m">Monster to add</param>
        /// <returns>true if the hero is still alive</returns>
        public static bool operator +(Hero h, Monster m) {
            if (!h.IsRunningAway) {
                if (h.AttackSpeed > m.AttackSpeed) {
                    if (h.Attack(m)) {
                        m.Attack(h);
                    }
                } else if (h.AttackSpeed < m.AttackSpeed) {
                    if (m.Attack(h)) {
                        h.Attack(m);
                    }
                } else { // Attack Speeds are the same.
                    h.Attack(m);
                    m.Attack(h);
                }
            } else {
                if (h.AttackSpeed <= m.AttackSpeed)
                    m.Attack(h);
            }
            h.IsRunningAway = false;
            return h.IsAlive;
        }

    }
}
