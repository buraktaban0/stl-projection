using System.Collections.Generic;

namespace STLProjection
{
	
	// Data structure holding the geometry data in STL format.
	public class StlModel
	{
		public string name = "";

		public List<Vector> vertices;
		public List<Vector> normals;

		public int TriangleCount => normals.Count;

		public StlModel(string name, List<Vector> vertices, List<Vector> normals)
		{
			this.name = name;
			this.vertices = vertices;
			this.normals = normals;
		}


		public void Subdivide(int subdivisions)
		{
			for (int i = 0; i < subdivisions; i++)
			{
				Subdivide();
			}
		}

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

	}
}
