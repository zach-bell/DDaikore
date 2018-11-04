﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDaikontin
{
    public class DCollider
    {
#if DEBUG
        public //So we can draw the hitboxes
#else
        protected
#endif
            List<DCircle> dCircles = new List<DCircle>();

        /// <summary>
        /// Create a collider with the given circles (a list of x, y, rad, [x, y, rad, [...]])
        /// </summary>
        /// <param name="xyr"></param>
        public DCollider(params double[] xyr)
        {
            if (xyr.Length % 3 != 0 || xyr.Length == 0) throw new Exception("Needs to be a multiple of 3 inputs (x, y, z)");
            for (int x = 0; x < xyr.Length; x += 3)
            {
                dCircles.Add(new DCircle() { X = xyr[x], Y = xyr[x + 1], Radius = xyr[x + 2] });
            }
        }

        public DCollider(UnitGraphics uGraphics)
        {
            dCircles.Add(new DCircle() {Radius = 10});
            //throw new NotImplementedException("Need to make a collision shape from the graphics");
        }

        public bool CollidesWith(double x, double y, DCollider other, double otherX, double otherY)
        {
            //TODO: Account for rotation of the object that owns the collider

            foreach (var c in dCircles)
            {
                var cX = x + c.X - otherX;
                var cY = y + c.Y - otherY;
                foreach (var d in other.dCircles)
                {
                    var distX = cX - d.X;
                    var distY = cY - d.Y;
                    var rad = c.Radius + d.Radius;
                    if (distX * distX + distY * distY < rad * rad) return true;
                }
            }

            return false;
        }
    }
}
