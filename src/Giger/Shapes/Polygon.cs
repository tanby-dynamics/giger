﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Giger.Plumbing;

namespace Giger.Shapes
{
    public class Polygon : Element<Polygon>
    {
        public Polygon(IEnumerable<double> points)
            : this(points.ToArray())
        {
        }

        public Polygon(params double[] points)
            : base(0, 0, 0, 0)
        {
            SetPoints(points.ToPoints().ToArray());
        }

        public Polygon(IEnumerable<Point> points)
            : this(points.ToArray())
        {
        }

        public Polygon(params Point[] points)
        {
            SetPoints(points);
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("polygon");
        }

        public Polygon SetPoints(Point[] points)
        {
            SetX(points.Min(x => x.X));
            SetY(points.Min(x => x.Y));
            SetWidth(points.Max(x => x.X) - (this.X ?? 0));
            SetHeight(points.Max(x => x.Y) - (this.Y ?? 0));

            return SetAttr(new
            {
                points = string.Join(" ", points.Select(x => $"{x.X},{x.Y}"))
            });
        }
    }

    public static partial class BaseElementExtensions
    {
        public static Polygon Polygon(this BaseElement baseElement, params double[] points)
        {
            var polygon = new Polygon(points);

            baseElement.AddChild(polygon);

            return polygon;
        }

        public static Polygon Polygon(this BaseElement baseElement, IEnumerable<double> points)
        {
            var polygon = new Polygon(points);

            baseElement.AddChild(polygon);

            return polygon;
        }

        public static Polygon Polygon(this BaseElement baseElement, IEnumerable<Point> points)
        {
            var polygon = new Polygon(points);

            baseElement.AddChild(polygon);

            return polygon;
        }

        public static Polygon Polygon(this BaseElement baseElement, params Point[] points)
        {
            var polygon = new Polygon(points);

            baseElement.AddChild(polygon);

            return polygon;
        }
    }

    public struct Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }
    }

    public static class PointExtensions
    {
        public static IEnumerable<Point> ToPoints(this double[] points)
        {
            if (points.Count()%2 != 0)
            {
                throw new ArgumentException("Odd number of points, must by (x,y) pairs", nameof(points));
            }

            double? x = null;
            foreach (var p in points)
            {
                if (x == null)
                {
                    x = p;
                }
                else
                {
                    yield return new Point(x.Value, p);
                    x = null;
                }
            }
        }
    }
}