using System;
using System.Collections.Generic;

namespace STLProjection
{
	// Data structure holding the geometry data in STL format.
	public class StlModel
	{
		public string name = "";

		public List<Vector> vertices;
		public List<Vector> normals;

		public Vector Min { get; private set; }
		public Vector Max { get; private set; }

		public Vector Size => Max - Min;

		public Vector HalfSize => Size * 0.5;

		public Vector Center => (Min + Max) * 0.5;

		
		public int TriangleCount => normals.Count;

		public StlModel(string name, List<Vector> vertices, List<Vector> normals)
		{
			this.name = name;
			this.vertices = vertices;
			this.normals = normals;
		}


		public void SubdivideDetail(int subdivisions, Vector origin, Vector direction, Vector up, double size)
		{
			for (int i = 0; i < subdivisions; i++)
			{
				SubdivideDetail(origin, direction, up, size);
			}
		}

		public void SubdivideDetail(Vector origin, Vector forward, Vector up, double size)
		{
			var right = Vector.Cross(up, forward);
			Func<Vector, bool> isDetail = v =>
				GeoUtil.RectContains(GeoUtil.ProjectOnPlaneTransformed(v, origin, up, right), size);

			int triCount = TriangleCount;
			for (int i = 0; i < triCount; i++)
			{
				var n = normals[i];

				var v0 = vertices[i * 3 + 0];
				var v1 = vertices[i * 3 + 1];
				var v2 = vertices[i * 3 + 2];

				var dot = Vector.Dot(forward, n);

				bool shouldSubdivide = isDetail(v0) || isDetail(v1) || isDetail(v2);

				shouldSubdivide &= dot < -0.01;

				if (!shouldSubdivide)
					continue;

				var v01 = (v0 + v1) * 0.5;
				var v12 = (v1 + v2) * 0.5;
				var v20 = (v2 + v0) * 0.5;

				vertices[i * 3 + 0] = v01;
				vertices[i * 3 + 1] = v1;
				vertices[i * 3 + 2] = v12;

				vertices.Add(v12);
				vertices.Add(v2);
				vertices.Add(v20);

				vertices.Add(v20);
				vertices.Add(v0);
				vertices.Add(v01);

				vertices.Add(v01);
				vertices.Add(v12);
				vertices.Add(v20);

				var normal = normals[i];
				normals.Add(normal);
				normals.Add(normal);
				normals.Add(normal);
			}
		}

		public void Subdivide(int subdivisions)
		{
			for (int i = 0; i < subdivisions; i++)
			{
				Subdivide();
			}
		}

		// TODO: No need to subdivide the whole mesh, just focus on parts where detail is needed
		public void Subdivide()
		{
			int triCount = TriangleCount;
			for (int i = 0; i < triCount; i++)
			{
				var v0 = vertices[i * 3 + 0];
				var v1 = vertices[i * 3 + 1];
				var v2 = vertices[i * 3 + 2];

				var v01 = (v0 + v1) * 0.5;
				var v12 = (v1 + v2) * 0.5;
				var v20 = (v2 + v0) * 0.5;

				vertices[i * 3 + 0] = v01;
				vertices[i * 3 + 1] = v1;
				vertices[i * 3 + 2] = v12;

				vertices.Add(v12);
				vertices.Add(v2);
				vertices.Add(v20);

				vertices.Add(v20);
				vertices.Add(v0);
				vertices.Add(v01);

				vertices.Add(v01);
				vertices.Add(v12);
				vertices.Add(v20);

				var normal = normals[i];
				normals.Add(normal);
				normals.Add(normal);
				normals.Add(normal);
			}
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
