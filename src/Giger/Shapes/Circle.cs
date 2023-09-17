﻿using System.Xml;
using Giger.Plumbing;

namespace Giger.Shapes
{
    public class Circle : Element<Circle>
    {
        public Circle(double x, double y, double radius) : base(x - radius, y - radius, radius*2, radius*2)
        {
            SetAttr(new
            {
                cx = x,
                cy = y,
                r = radius
            });
        }

        protected override XmlNode GetXmlNode(XmlDocument doc)
        {
            return doc.CreateSvgElement("circle");
        }
    }

    public static partial class BaseElementExtensions
    {
        public static Circle Circle(this BaseElement baseElement, double x, double y, double radius)
        {
            var circle = new Circle(x, y, radius);

            baseElement.AddChild(circle);

            return circle;
        }
    }
}