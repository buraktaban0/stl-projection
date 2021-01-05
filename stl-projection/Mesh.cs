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


		private Dictionary<int, int> triangleHash;


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


		public void CalculateTriangleHash()
		{
			triangleHash = new Dictionary<int, int>();
			for (int i = 0; i < vertices.Count; i++)
			{
				triangleHash[i] = i / 3;
			}
		}

		public int GetTriangle(int vertexIndex)
		{
			return triangleHash[vertexIndex];
		}

		public void SubdivideTriangle(int t)
		{
			int i0 = indices[t * 3 + 0];
			int i1 = indices[t * 3 + 1];
			int i2 = indices[t * 3 + 2];

			var v0 = vertices[i0];
			var v1 = vertices[i1];
			var v2 = vertices[i2];

			var n0 = normals[i0];
			var n1 = normals[i1];
			var n2 = normals[i2];

			var v01 = (v0 + v1) * 0.5;
			var v12 = (v1 + v2) * 0.5;
			var v20 = (v2 + v0) * 0.5;

			var n01 = (n0 + n1).Normalized;
			var n12 = (n1 + n2).Normalized;
			var n20 = (n2 + n0).Normalized;

			int i01 = vertices.Count;
			int i12 = i01 + 1;
			int i20 = i12 + 1;

			vertices.Add(v01);
			vertices.Add(v12);
			vertices.Add(v20);

			normals.Add(n01);
			normals.Add(n12);
			normals.Add(n20);

			indices[t * 3 + 0] = i20;
			indices[t * 3 + 1] = i0;
			indices[t * 3 + 2] = i01;

			indices.Add(i01);
			indices.Add(i1);
			indices.Add(i12);

			indices.Add(i12);
			indices.Add(i2);
			indices.Add(i20);

			indices.Add(i01);
			indices.Add(i12);
			indices.Add(i20);
		}
	}
}
