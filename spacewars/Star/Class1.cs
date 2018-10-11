// Author: Daniel Kopta, Fall 2017
// Staff solution for CS 3500 SpaceWars project
// University of Utah

using Newtonsoft.Json;
using SpaceWars;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceWars
{
    /// <summary>
    /// A class to represent a star
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Star
    {
        private static int nextStarID = 0;

        [JsonProperty(PropertyName = "star")]
        private int ID;

        [JsonProperty(PropertyName = "loc")]
        private Vector2D location;

        [JsonProperty(PropertyName = "mass")]
        private double mass;

        // Default constructor for JSON, does not result in a valid projectile
        public Star()
        {
            ID = -1;
            mass = 0;
            location = null;
        }

        public Star(Vector2D loc, double m)
        {
            ID = nextStarID++;
            location = new Vector2D(loc);
            mass = m;
        }

        public Vector2D GetLocation()
        {
            return location;
        }

        public int GetID()
        {
            return ID;
        }

        public double GetMass()
        {
            return mass;
        }

        /// <summary>
        /// Return a JSON representation of this star
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}