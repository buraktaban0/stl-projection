using System.Collections.Generic;

namespace STLProjection
{
	// Convenient mesh data structure with shared vertices. Triangles are identified by the indices list containing integers pointing to vertices.
	public class Mesh
	{
		public string name;
		
		public List<Vector> vertices;

		public List<Vector> normals;

		public List<int> indices;

		public int TriangleCount => indices.Count / 3;
		

		public Mesh(string name, List<Vector> vertices, List<Vector> normals, List<int> indices)
		{
			this.name = name;
			this.vertices = vertices;
			this.normals = normals;
			this.indices = indices;
		}
	}
}
