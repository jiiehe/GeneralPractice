// Author: Daniel Kopta, Fall 2017
// Staff solution for CS 3500 SpaceWars project
// University of Utah

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;
using System.Drawing;

/// <summary>
/// A class that represents the "world", i.e. the ships, stars, projectiles.
/// Both the server and the client make use of different parts of this class.
/// </summary>
namespace SpaceWars
{
    public class World
    {
        private int universeSize;

        private int respawnRate;

        private uint framesPerShot;

        private uint time;

        Dictionary<int, Ship> ships;

        Dictionary<int, Ship> discontinuedShips;

        Dictionary<int, Projectile> projectiles;

        Dictionary<int, Star> stars;

        // Used by the client only, maps ID -> draw color
      //  IEnumerator<Color> colorPicker;

       // Random rand;

        // Some of these constructors are leftover from various different types of unique clients.
        // Ideally they would be cleaned out.
        public World()
        {
            universeSize = 750;
            framesPerShot = 15;
            ships = new Dictionary<int, Ship>();
            discontinuedShips = new Dictionary<int, Ship>();
            projectiles = new Dictionary<int, Projectile>();
            stars = new Dictionary<int, Star>();
          //  colorPicker = AllColors().GetEnumerator();
          //  rand = new Random();
            time = 0;
        }

        public World(int size) : this()
        {
            universeSize = size;
        }

        public World(int size, IEnumerable<Star> _stars, int respawn, uint fire) : this()
        {
            universeSize = size;
            respawnRate = respawn;
            framesPerShot = fire;
            foreach (Star s in _stars)
                stars[s.GetID()] = s;
        }

        public int GetSize()
        {
            return universeSize;
        }

        public uint GetTime()
        {
            return time;
        }

        public Dictionary<int, Ship> GetShips()
        {
            return ships;
        }

        public Dictionary<int, Ship> GetDiscontinuedShips()
        {
            return discontinuedShips;
        }

      

        public Dictionary<int, Star> GetStars()
        {
            return stars;
        }

        /// <summary>
        /// Used by the server only. Adds a ship.
        /// </summary>
        /// <param name="position"></param>
        public Ship addShip(string name, Vector2D position, Vector2D velocity, Vector2D direction)
        {
            Ship newShip = new Ship(name, position, velocity, direction, framesPerShot);
            ships.Add(newShip.GetID(), newShip);
            return newShip;
        }

        /// <summary>
        /// Adds a random ship
        /// Used by the server only
        /// </summary>
     

       

  

     

       


     

    }

}
