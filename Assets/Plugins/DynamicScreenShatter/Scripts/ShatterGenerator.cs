using System;
using System.Collections.Generic;
using System.Linq;
using ScreenShatter.Sutherland;
using UnityEngine;
using Random = System.Random;

namespace ScreenShatter
{
    class ShatterGenerator
    {
        private float Width;
        private float Height;

        private Vector2 Origin;
        private int Radials;
        private int Circles;

        private Vector2[,] radialVertices;
        private float[] radii;

        private List<Polygon> polygons;

        private Random rand = new Random();

        private float breakQuadsChance;

        private float jitteryness;

        public IEnumerable<Polygon> Polygons { get { return polygons;} } 

        public ShatterGenerator(int width, int height, Vector2 origin, int radials, int circles, float jitteryness, float breakQuadsChance)
        {
            Radials = radials;
            Width = width;
            Height = height;
            Origin = origin;
            Circles = circles + 1;

            this.jitteryness = jitteryness;
            this.breakQuadsChance = breakQuadsChance;
        }

        private float Distance(Vector2 p, Vector2 p2)
        {
            return (p - p2).magnitude;
        }

        public void GenerateShatter()
        {
            GenerateRadii();
            GenerateRadialVertices();
            GeneratePolygons();
            ClipPolygons();
        }

        private void GenerateRadii()
        {
            radii = new float[Circles];

            float maxRadius = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(0, Height),
                new Vector2(Width, 0),
                new Vector2(Width, Height),
            }.Max(p => Distance(p, Origin));

            maxRadius = (float)Math.Round(maxRadius);

            for (int i = 0; i < Circles; i++)
            {
                radii[i] = ((float)i + 1) / ((float)Circles - 1) * maxRadius;
            }
        }

        private void GenerateRadialVertices()
        {
            radialVertices = new Vector2[Circles, Radials];
            float baseRotation = (float)(rand.NextDouble() * 2.0 * Math.PI);

            for (int i = 0; i < Circles; i++)
            {
                float radius = radii[i];

                for (int r = 0; r < Radials; r++)
                {
                    float maxOffset = (float)r / Radials * 0.9f * jitteryness;
                    float angularOffset = (float)((rand.NextDouble() - 0.5) * maxOffset);
                    float angle = baseRotation + (float)r / Radials * (float)(2.0 * Math.PI);
                    
                    Vector2 p = new Vector2(
                        Origin.x + (float)Math.Cos(angle + angularOffset) * radius,
                        Origin.y + (float)Math.Sin(angle + angularOffset) * radius);
                    radialVertices[i, r] = p;
                }
            }
        }

        private void GeneratePolygons()
        {
            polygons = new List<Polygon>();
            for (int r = 0; r < Radials; r++)
            {
                var p = new Polygon(new Vector2[]
                {
                    Origin,
                    radialVertices[0, r],
                    radialVertices[0, (r + 1) % Radials],
                });

                polygons.Add(p);
            }

            for (int i = 0; i < Circles - 1; i++)
            {
                for (int r = 0; r < Radials; r++)
                {
                    var split = rand.NextDouble() <= breakQuadsChance;
                    if (split)
                    {
                        polygons.Add(new Polygon(new Vector2[]
                        {
                            radialVertices[i, r],
                            radialVertices[i + 1, r],
                            radialVertices[i, (r + 1)%Radials]
                        }));

                        polygons.Add(new Polygon(new Vector2[]
                        {
                            radialVertices[i + 1, (r + 1)%Radials],
                            radialVertices[i + 1, r],
                            radialVertices[i, (r + 1)%Radials]
                        }));
                    }
                    else
                    {
                        var p = new Polygon(new Vector2[]
                        {
                            radialVertices[i, r],
                            radialVertices[i + 1, r],
                            radialVertices[i + 1, (r + 1)%Radials],
                            radialVertices[i, (r + 1)%Radials]
                        });

                        polygons.Add(p);
                    }
                }
            }
        }

        private void ClipPolygons()
        {
            List<Polygon> newPolys = new List<Polygon>();

            Vector2[] clipPoly = new Vector2[]
            {
                new Vector2(0, 0), 
                new Vector2(Width, 0),
                new Vector2(Width, Height),
                new Vector2(0, Height)
            };
            
            Vector2[] verts;
            foreach (var p in polygons)
            {
                // Clip Polygon
                try
                {
                    verts = SutherlandHodgman.GetIntersectedPolygon(p.points, clipPoly);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    continue;
                }
                if (verts.Length < 3)
                    continue;
                
                newPolys.Add(new Polygon(verts));
            }

            polygons = newPolys;
        }
    }
}
