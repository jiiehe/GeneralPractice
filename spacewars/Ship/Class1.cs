// Author: Daniel Kopta, Fall 2017
// Staff solution for CS 3500 SpaceWars project
// University of Utah

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
//using System.Windows.Forms;
using Newtonsoft.Json;
using SpaceWars;

namespace SpaceWars
{

    public struct CommandsRequest
    {

        private int turning;
        private bool thrusting;
        private bool firing;

     

        public bool requestThrusting
        {
            get { return thrusting; }
            set
            {
                thrusting = value;
            }
        }

        public bool requestFiring
        {
            get { return firing; }
            set
            {
                firing = value;
            }
        }

     

    }



    [JsonObject(MemberSerialization.OptIn)]
    public class Ship
    {

        private static int nextShipID = 0;

        [JsonProperty(PropertyName = "ship")]
        private int ID;

        [JsonProperty(PropertyName = "loc")]
        private Vector2D location;

        [JsonProperty(PropertyName = "dir")]
        private Vector2D orientation;

        [JsonProperty(PropertyName = "thrust")]
        private bool accelerating = false;

        [JsonProperty(PropertyName = "name")]
        private string name;

        [JsonProperty(PropertyName = "hp")]
        private int hitPoints = 5;

        [JsonProperty(PropertyName = "score")]
        private int score = 0;

        public bool Alive
        {
            get { return hitPoints > 0; }
        }

        private bool active = true;

        private uint lastFired;

        private uint lastDeath;

        private uint fireRate;

        private uint shots = 0;

        private uint hits = 0;

        private Vector2D velocity;

        private Vector2D thrust = new Vector2D(0, 0);

        private CommandsRequest cmdReqs;

        public bool updated = false;

        // Default constructor needed for JSON
        public Ship()
        {
            ID = 0;
            location = new Vector2D(-1, -1);
            velocity = new Vector2D(0, 0);
            lastFired = 0;
            fireRate = 15;
            hitPoints = 0;
            score = 0;

            //cmdReqs.Clear();
        }

        public Ship(string _name, Vector2D p, Vector2D v, Vector2D d, uint fRate)
        {
            ID = nextShipID++;
            name = _name;
            location = new Vector2D(p);
            velocity = new Vector2D(v);
            orientation = new Vector2D(d);
            fireRate = 15;
           
            hitPoints = 5;
            score = 0;
            fireRate = fRate;
        }

        public void Discontinue(uint time)
        {
            if (Alive)
                Die(time);
            active = false;
        }

        public bool IsActive()
        {
            return active;
        }

        public bool GetAccelerating()
        {
            return accelerating;
        }


        public void Hit(uint time)
        {
            hitPoints--;
            if (hitPoints == 0)
                Die(time);
        }

        public int GetHitPoints()
        {
            return hitPoints;
        }

        public void Die(uint time)
        {
            hitPoints = 0;
            lastDeath = time;
        }

        public uint GetLastDeath()
        {
            return lastDeath;
        }

        public int GetScore()
        {
            return score;
        }

        public void IncreaseScore()
        {
            score++;
        }

        public uint GetShots()
        {
            return shots;
        }

        public uint GetHits()
        {
            return hits;
        }

        public void Respawn(Vector2D newLocation)
        {
            System.Diagnostics.Debug.Assert(!Alive);

            if (!active)
                return;

            hitPoints = 5;
            location = new Vector2D(newLocation);
            velocity = new Vector2D(0, 0);
            orientation = new Vector2D(0, -1);
        }

      

        public void AddHit()
        {
            hits++;
        }

        public int GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return name;
        }

        public Vector2D GetLocation()
        {
            return location;
        }

        public void WrapAroundX()
        {
            location = new Vector2D(location.GetX() * -1, location.GetY());
        }

        public void WrapAroundY()
        {
            location = new Vector2D(location.GetX(), location.GetY() * -1);
        }

        public Vector2D GetOrientation()
        {
            return orientation;
        }

 

   


        public void Update(IEnumerable<Star> stars, uint time)
        {
           // applyCommandRequests();

            Vector2D acceleration = new Vector2D(thrust);

            // Reset the thrust since we are only thrusting if the server says so
            thrust = new Vector2D(0, 0);

            foreach (Star s in stars)
            {
                double mass = s.GetMass();

                // Gravity seems to work better the "arcade" way, with no inverse distance or inverse distance squared term.
                // It's just too hard to set up a good orbit with real gravity, unless I make the scale more realistic, and then it's way too slow and boring.
                //gravity = gravity * (mass / (2.0 * (distance * distance)));

                Vector2D gravity = s.GetLocation() - location;

                // Check for collision with stars
                if (gravity.Length() < 35)
                {
                    Die(time);
                    return;
                }

                gravity.Normalize();

                gravity = gravity * mass;

                acceleration = acceleration + gravity;

            }

            velocity = velocity + acceleration;
            location = location + velocity;

        }

        /// <summary>
        /// Return a JSON representation of this ship
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }


    }
}