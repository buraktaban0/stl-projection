using System.Collections.Generic;

namespace STLProjection
{
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

		public void ApplyNoise(double strength)
		{
			var rand = new System.Random();

			for (int i = 0; i < TriangleCount; i++)
			{
				var n = normals[i];


				for (int j = 0; j < 3; j++)
				{
					var m = (rand.NextDouble() * 2.0 - 1.0) * strength;
					vertices[i * 3 + j] = vertices[i * 3 + j] + n * m;
				}

				var v0 = vertices[i * 3 + 0];
				var v1 = vertices[i * 3 + 1];
				var v2 = vertices[i * 3 + 2];

				var r1 = (v1 - v0).Normalized;
				var r2 = (v2 - v0).Normalized;
				var newN = Vector.Cross(r1, r2);

				var a = Vector.Angle(newN, n);

				normals[i] = a < 90 ? newN : -newN;
			}
		}
	}
}
