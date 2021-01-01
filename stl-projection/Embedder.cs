using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace STLProjection
{
	public class Embedder
	{
		public List<Shape> shapes;

		public Embedder(string shapesPath)
		{
			ReadShapes(shapesPath);
		}

		public void ReadShapes(string path)
		{
			shapes = new List<Shape>();

			var lines = File.ReadAllLines(path);
			Shape shape = null;
			for (var i = 0; i < lines.Length; i++)
			{
				var line = lines[i].Trim().ToLower();
				if (line.Contains("shape"))
				{
					shape = new Shape();
					shapes.Add(shape);
					continue;
				}

				line = line.Replace("line", "").Trim();
				var seg = line.Split(' ');

				double[] vals = seg.Select(s => double.Parse(s.Trim())).ToArray();
				var lineSegment = new Line(new Vector(vals[0], vals[1]), new Vector(vals[2], vals[3]));
				shape.lines.Add(lineSegment);
			}
			
			foreach (var shape1 in shapes)
			{
				Console.WriteLine(shape1);
			}
		}
	}
}
