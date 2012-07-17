using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotSim
{
    /// <summary>
    /// Robot class.  Receives instructions from the driver, validates them and carries them out.
    /// Maintains internal state for position and facing direction.
    /// </summary>
    public class Robot
    {
        public Robot()
        {
            LastError = "";
        }

        private const int TABLE_SIZE = 5;
        private int? _x;
        private int? _y;
        private Facing _facing;

        public string LastError { get; set; }

        public bool Place(int x, int y, Facing facing)
        {            
            if (MandateIsOnTable(x, y, "placed"))
            {
                _x = x;
                _y = y;
                _facing = facing;
                return true;
            }
            return false;
        }

        public bool Move()
        { 
            if (MandateIsPlaced("move"))
            {
                int newx = GetXAfterMove();
                int newy = GetYAfterMove();
                if (MandateIsOnTable(newx, newy, "moved"))
                {
                    _x = newx;
                    _y = newy;
                    return true;
                }                
            }
            return false;
        }

        private int GetXAfterMove()
        {
            if (_facing == Facing.East)
            {
                return _x.Value + 1;
            }
            else 
            {
                if (_facing == Facing.West) 
                {
                    return _x.Value - 1;
                }
            }
            return _x.Value;
        }

        private int GetYAfterMove()
        {
            if (_facing == Facing.North)
            {
                return _y.Value + 1;
            }
            else
            {
                if (_facing == Facing.South)
                {
                    return _y.Value - 1;
                }
            }
            return _y.Value;
        }

        public bool Left()
        {
            return Turn(Direction.Left);
        }

        public bool Right()
        {
            return Turn(Direction.Right);
        }

        private bool Turn(Direction direction)
        {
            if (MandateIsPlaced("turn"))
            {
                var facingAsNumber = (int)_facing;
                facingAsNumber += 1 * (direction == Direction.Right ? 1 : -1);
                if (facingAsNumber == 5) facingAsNumber = 1;
                if (facingAsNumber == 0) facingAsNumber = 4;
                _facing = (Facing)facingAsNumber;
                return true;
            }
            return false;
        }

        public string Report()
        {
            if (MandateIsPlaced("report it's position"))
            {
                return String.Format("{0},{1},{2}", _x.Value, _y.Value, _facing.ToString().ToUpper());
            }
            return "";
        }

        private bool MandateIsPlaced(string action)
        {
            if (!_x.HasValue || !_y.HasValue)
            {
                LastError = String.Format("Robot cannot {0} until it has been placed on the table.", action);
                return false;
            }
            return true;
        }

        private bool MandateIsOnTable(int x, int y, string action)
        {
            if (x < 0 || y < 0 || x >= TABLE_SIZE || y >= TABLE_SIZE)
            {
                LastError = String.Format("Robot cannot be {0} there.", action);
                return false;
            }
            return true;
        }
    }
}