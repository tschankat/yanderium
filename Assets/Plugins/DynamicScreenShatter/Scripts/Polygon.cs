using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ScreenShatter
{
    internal class Polygon
    {
        public Vector2[] points { get; private set; }
        public Vector2 center { get; private set; }

        public Polygon(params Vector2[] points)
        {
            this.points = points;
            CalculateCenter();
        }

        private void CalculateCenter()
        {
            Vector2 sum = Vector2.zero;

            foreach (var p in points)
            {
                sum += p;
            }

            center = new Vector2(sum.x / points.Length, sum.y / points.Length);
        }
    }
}
