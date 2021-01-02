using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace STLProjection
{
	// Embedder class that handles embedding of shape encoded data onto the mesh
	public class Embedder
	{
		// Predefined rows and columns, can be made dynamic through user input
		public static readonly int ROWS = 3;
		public static readonly int COLUMNS = 4;

		public List<Shape> shapes;

		public Embedder(string shapesPath)
		{
			ReadShapes(shapesPath);
		}

		// Read shapes from the data file path provided
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
		}


		// Encode the id which is in base 10, to base k
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

				shapes.Add(this.shapes[t]);
			}

			return shapes;
		}


		// Construct the shape matrix. Scales and transforms the shapes in correct cell placements with the size and margin provided,
		// returns the obtained lines explicitly.
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

			// var plt = new ScottPlot.Plot(200 * COLUMNS, 200 * ROWS);
			// lines.ForEach(l => plt.PlotScatter(new[] {l.p1.x, l.p2.x}, new[] {l.p1.y, l.p2.y}));
			//
			// plt.SaveFig("C:\\Users\\PC1\\Desktop\\495\\term\\matrix.png");

			return lines;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">ID in base 10 to be converted and embedded onto the mesh</param>
		/// <param name="mesh">Mesh to be modified</param>
		/// <param name="size">Size of the largest dimension of the shape matrix, in input file's units</param>
		/// <param name="margin">Space between shape cells. (Closest distance between two crevice vertex minus thickness)</param>
		/// <param name="thickness">Thickness of crevices</param>
		/// <param name="displacement">Displacement of crevice vertices in the forward direction</param>
		/// <param name="origin">Origin of the projection plane</param>
		/// <param name="forward">Direction of the projection</param>
		/// <param name="up">Up direction of the projection plane to determine the matrix orientation</param>
		public void Embed(int id, Mesh mesh, double size, double margin, double thickness, double displacement,
		                  Vector origin,
		                  Vector forward, Vector up)
		{
			var right = Vector.Cross(up, forward);

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

				// TODO: Additional check to see if the face is facing the projection plane but occluded by other faces in the geometry.
				// Required for complex geometry.

				var vProj = GeoUtil.ProjectOnPlaneTransformed(v, origin, up, right);

				// Check for each line if the projected point lies inside it, with the thickness used as the threshold.
				foreach (var line in lines)
				{
					var dist = GeoUtil.PointLineDistance(vProj, line);
					if (dist < thickness * 0.5)
					{
						// Point inside a line, modify the vertex and early exit the loop, no need to check any other line.
						bool smoothDisplacement = true;
						if (smoothDisplacement)
						{
							vertices[i] = v + forward * displacement * Smoothstep(1 - dist / (thickness * 0.5));
						}
						else
						{
							vertices[i] = v + forward * displacement;
						}

						break;
					}
				}
			}
		}


		// Simple integer power implemented for no unknown behaviour
		private int Pow(int a, int b)
		{
			int m = 1;
			for (int i = 0; i < b; i++)
			{
				m *= a;
			}

			return m;
		}


		// Smooth in range [0, 1] (dy/dx @ 0 and 1 = 0)
		private double Smoothstep(double x)
		{
			return 3 * x * x - 2 * x * x * x;
		}
	}
}
