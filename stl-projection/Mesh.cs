using System.Collections.Generic;
using System.Numerics;

namespace STLProjection
{
	// Convenient mesh data structure with shared vertices. Triangles are identified by the indices list containing integers pointing to vertices.
	public class Mesh
	{
		public string name;

		public List<Vector> vertices;

		public List<Vector> normals;

		public List<int> indices;

		public Vector Min { get; private set; }
		public Vector Max { get; private set; }

		public Vector Size => Max - Min;

		public Vector HalfSize => Size * 0.5;

		public Vector Center => (Min + Max) * 0.5;


		public int TriangleCount => indices.Count / 3;


		public Mesh(string name, List<Vector> vertices, List<Vector> normals, List<int> indices)
		{
			this.name = name;
			this.vertices = vertices;
			this.normals = normals;
			this.indices = indices;
		}


		public void CalculateBounds()
		{
			Min = Vector.ONE * double.MaxValue;
			Max = Vector.ONE * double.MinValue;

			foreach (var vertex in vertices)
			{
				Min = Vector.ElementWiseMin(Min, vertex);
				Max = Vector.ElementWiseMax(Max, vertex);
			}
		}

		public void CenterBounds()
		{
			var center = Center;
			for (var i = 0; i < vertices.Count; i++)
			{
				vertices[i] = vertices[i] - center;
			}

			var halfSize = HalfSize;
			Min = -halfSize;
			Max = halfSize;
		}
	}
}
