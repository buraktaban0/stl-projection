using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace STLProjection
{
	public class Embedder
	{
		public static readonly int ROWS = 3;
		public static readonly int COLUMNS = 4;

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
					shape = new Shape(shapes.Count);
					shapes.Add(shape);
					continue;
				}

				line = line.Replace("line", "").Trim();
				var seg = line.Split(' ');

				double[] vals = seg.Select(s => double.Parse(s.Trim())).ToArray();
				var lineSegment = new Line(new Vector(vals[0], vals[1]), new Vector(vals[2], vals[3]));
				shape.lines.Add(lineSegment);
			}

			// var enc = GetEncodedShapes(2);
			// foreach (var shape1 in enc)
			// {
			// 	Console.WriteLine(shape1);
			// }
			//
			// var mat = ConstructMatrix(enc, 1);
			//
			// var plt = new ScottPlot.Plot(200 * COLUMNS, 200 * ROWS);
			// mat.ForEach(l => plt.PlotScatter(new[] {l.p1.x, l.p2.x}, new[] {l.p1.y, l.p2.y}));
			//
			// plt.SaveFig("C:\\Users\\PC1\\Desktop\\495\\term\\matrix.png");
		}


		public List<Shape> GetEncodedShapes(int id)
		{
			var shapes = new List<Shape>();
			int c = ROWS * COLUMNS;
			int k = this.shapes.Count;
			for (int i = c - 1; i >= 0; i--)
			{
				int m = Pow(k, i);
				int t = id / m;
				id -= t * m;

				//Console.WriteLine($"{i} {c} {k} {m} {t} {id}");

				shapes.Add(this.shapes[t]);
			}

			return shapes;
		}


		public List<Line> ConstructMatrix(List<Shape> shapes, double size, double margin)
		{
			var lines = new List<Line>();

			double cellSize;
			if (ROWS >= COLUMNS)
			{
				cellSize = (size - margin * (ROWS - 1)) / ROWS;
			}
			else
			{
				cellSize = (size - margin * (COLUMNS - 1)) / COLUMNS;
			}

			double x, y;

			for (int i = 0; i < ROWS; i++)
			{
				y = cellSize * (i - ROWS * 0.5) + (i - ROWS * 0.5 + 0.5) * margin;
				for (int j = 0; j < COLUMNS; j++)
				{
					x = cellSize * (j - COLUMNS * 0.5) + (j - COLUMNS * 0.5 + 0.5) * margin;
					var pos = new Vector(x, y);
					int k = i * COLUMNS + j;
					var transformedShapeLines = shapes[k].Scale(cellSize).Translate(pos).lines;
					lines.AddRange(transformedShapeLines);
				}
			}

			var plt = new ScottPlot.Plot(200 * COLUMNS, 200 * ROWS);
			lines.ForEach(l => plt.PlotScatter(new[] {l.p1.x, l.p2.x}, new[] {l.p1.y, l.p2.y}));

			plt.SaveFig("C:\\Users\\PC1\\Desktop\\495\\term\\matrix.png");

			return lines;
		}

		public void Embed(int id, Mesh mesh, double size, double margin, double thickness, double displacement,
		                  Vector origin,
		                  Vector forward, Vector up, Vector right)
		{
			var encodedShapes = GetEncodedShapes(id);
			var lines = ConstructMatrix(encodedShapes, size, thickness + margin);

			var vertices = mesh.vertices;
			var normals = mesh.normals;

			for (var i = 0; i < vertices.Count; i++)
			{
				var v = vertices[i];
				var n = normals[i];

				if (Vector.Dot(forward, n) >= -0.01)
				{
					// Not facing the projection plane, no need to continue further.
					continue;
				}

				var vProj = GeoUtil.ProjectOnPlaneTransformed(v, origin, up, right);

				foreach (var line in lines)
				{
					var dist = GeoUtil.PointLineDistance(vProj, line);
					if (dist <= thickness * 0.5)
					{
						// Inside line, modify
						vertices[i] = v + forward * displacement;
						break;
					}
				}
			}
		}


		private int Pow(int a, int b)
		{
			int m = 1;
			for (int i = 0; i < b; i++)
			{
				m *= a;
			}

			return m;
		}


		private int Max(int a, int b)
		{
			return a > b ? a : b;
		}
	}
}
