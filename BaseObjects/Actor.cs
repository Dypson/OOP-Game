/*
Imported From Prof Holmes
CS/INFO 1182 
Deepson Khadka
4/12/2018
Description - Items in the game that are attackable and can attack.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseObjects {
    [Serializable]
    /// <summary>
    /// This is an attackable thing
    /// </summary>
    public abstract class Actor {
        protected String _Name;
        protected String _Title;
        protected int _CurrentHitPoints;
        protected int _MaximumHitPoints;
        protected double _AttackSpeed;
        protected int _PositionX;
        protected int _PositionY;

        // Used the following page as a reference for what to not capitalize in Title Case.
        //http://www.dailywritingtips.com/rules-for-capitalization-in-titles/
        private static String[] _titleCaseWords =
                            { "in", "the", "a", "an", "as", "of", "to", "at", "with",
                              "and", "but", "or", "is", "by" };

        /// <summary>
        /// What direction the actor needs to move.
        /// </summary>
        public enum Direction {
            Up, Down, Left, Right
        }

        /// <summary>
        /// Constructor for Actor 
        /// </summary>
        /// <param name="myName">Name of the Actor</param>
        /// <param name="myTitle">Title that the Actor will have</param>
        /// <param name="hitPoints">starting number of hitpoints</param>
        /// <param name="xPosition">Starting x Position</param>
        /// <param name="yPosition">Starting y posision</param>
        public Actor(String myName, String myTitle, double myAttackSpeed, int hitPoints, int xPosition, int yPosition) {
            _Name = myName;
            _Title = myTitle;
            _CurrentHitPoints = _MaximumHitPoints = hitPoints;
            _PositionX = xPosition;
            _PositionY = yPosition;
            _AttackSpeed = myAttackSpeed;
        }

      
        /// <summary>
        /// Get actor's name.
        /// </summary>
        /// <param name="withTitle">whether or not to include the title, defaulted to false</param>
        /// <returns>The actor's name with the title is specified. </returns>
        public String Name(bool withTitle = false) {
            if (!withTitle)
                return _Name;
            else
                return NameWithTitle();
            
        }
        /// <summary>
        /// Get Name with title attached
        /// </summary>
        /// <returns></returns>
        private String NameWithTitle() {
            String retName = _Name + " ";
            foreach(String word in _Title.Split(' ')) {
                String wordToAdd = word;
                if (isArticle(word)) {
                    wordToAdd = wordToAdd.ToLower();
                }
                retName += wordToAdd + " ";
            }
            return retName.Trim();
        }

        /// <summary>
        /// Goes through each title word and checks if the given word is in the array.
        /// </summary>
        /// <param name="wordToCheck">word that is checked against the title words; case insensitive</param>
        /// <returns>true if the word is in the list; false, if not in the list</returns>
        private bool isArticle(string wordToCheck) {
            foreach (String word in _titleCaseWords) {
                if (word == wordToCheck.ToLower()) {
                    return true;
                }
            }
            return false;
        }



        /// <summary>
        /// Get the current amount of hit points the actor has.
        /// </summary>
        public int CurrentHitPoints {
            get {
                return _CurrentHitPoints;
            }
        }

        /// <summary>
        /// Get the maximum amount of hit points that the actor can have.
        /// </summary>
        public int MaximumHitPoints {
            get {
                return _MaximumHitPoints;
            }
        }

        /// <summary>
        /// Get the current x coordinate of the actor
        /// </summary>
        public int PositionX {
            get {
                return _PositionX;
            }
        }

        /// <summary>
        /// Get the current y coordinate of the actor
        /// </summary>
        public int PositionY {
            get {
                return _PositionY;
            }
        }

        /// <summary>
        /// Get the attack speed of the actor
        /// </summary>
        public virtual double AttackSpeed {
            get {
                return _AttackSpeed;
            }
        }

        /// <summary>
        /// Move the actor a given direction
        /// </summary>
        /// <param name="d">Direction that the actor will move</param>
        public virtual void Move(Direction d) {
            // this will eventually need to know about the map,
            //   so that the actor cannot run off of the map.
            if (d == Direction.Up)
                _PositionY--;
            else if (d == Direction.Down)
                _PositionY++;
            else if (d == Direction.Left)
                _PositionX--;
            else if (d == Direction.Right)
                _PositionX++;
            else {
                // should not hit this.
            }
        }

        /// <summary>
        /// Increase the current hit points of the actor
        /// </summary>
        /// <param name="affectValue">Amount that the hit points will be increased.</param>
        public void HealMe(int affectValue) {
            _CurrentHitPoints += affectValue;
            // can never have more HP than max
            if (CurrentHitPoints > MaximumHitPoints) _CurrentHitPoints = MaximumHitPoints;
        }

        /// <summary>
        /// Decrease the current hit points of the actor
        /// </summary>
        /// <param name="affectValue">Amount that the hit points will be decreased.</param>
        public void DamageMe(int affectValue) {
            _CurrentHitPoints -= affectValue;
            // can never have less than 0 HP
            if (CurrentHitPoints < 0) _CurrentHitPoints = 0;
        }

        /// <summary>
        /// Whether or not the actor is still alive
        /// </summary>
        /// <returns>true if the actor is still alive; false, if dead</returns>
        public bool IsAlive {
            get {
                return this.CurrentHitPoints > 0;
            }
        }
    }
}
