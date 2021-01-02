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

	}
}
